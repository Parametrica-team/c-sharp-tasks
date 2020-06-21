using Rhino.Geometry;
using Robot;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace TestPlugin
{
    public class LevelsCreator
    {
        private List<Flat> flats;
        private string combinations;
        private Dictionary<string, List<Flat>> dict;
        private List<List<Flat>> AllFlatCombinations;
        private Stack<Flat> currentFlatStack;

        public List<Level> Levels { get; }
                

        public LevelsCreator(List<Flat> flats, string combinationsTxt)
        {
            this.flats = flats;
            this.combinations = combinationsTxt;

            //добавить квартиру llu
            var lluFlat = new Flat()
            {
                Contour = new Rectangle3d(Plane.WorldXY, 7000, -6093).ToNurbsCurve(),
                TopSteps = 2,
                Id = "llu",
                BasePlane = Plane.WorldXY,
                FlatWindows = new List<Window>() { new Window(Plane.WorldZX)}
            };
            this.flats.Add(lluFlat);

            //создать словать соответсвий квартир и кодов
            dict = MakeCodeDictionary(flats);
            dict.Remove(string.Empty);

            //удалить лишние строки
            var goodCombinations = DeleteExtraRows();            

            //создать уровни из комбинаций
            List<Level> levels = CreateLevelFromCombinations(goodCombinations);

            //сортировать по
            List<List<Level>> sortedLevels = SortLevels(levels);

            int hStep = 100000;
            int vStep = 50000;
            Levels = PlaceLevels(sortedLevels, hStep, vStep);
            //Levels = levels;
        }

        private Dictionary<string, List<Flat>> MakeCodeDictionary(List<Flat> flats)
        {
            var tDict = new Dictionary<string, List<Flat>>();
            foreach (var flat in flats)
            {
                if (flat.Id.ToLower().StartsWith("llu"))
                {
                    if (tDict.ContainsKey("llu"))
                        tDict["llu"].Add(flat);
                    else
                        tDict.Add("llu", new List<Flat> { flat });
                }
                else
                {
                    if (tDict.ContainsKey(flat.Code))
                        tDict[flat.Code].Add(flat);
                    else
                        tDict.Add(flat.Code, new List<Flat> { flat });
                }
            }

            return tDict;
        }
       

        private List<Level> PlaceLevels(List<List<Level>> sortedLevels, int horisontalStep, int verticalStep)
        {
            var resultLevels = new List<Level>();

            for (int i = 0; i < sortedLevels.Count; i++)
            {
                for (int j = 0; j < sortedLevels[i].Count; j++)
                {
                    var level = sortedLevels[i][j];

                    var xform = Transform.Translation(i * horisontalStep, -j * verticalStep, 0);
                    var levelInPlace = level.Transform(xform) as Level;
                    
                    resultLevels.Add(levelInPlace);
                }
            }

            return resultLevels;
        }

        /// <summary>
        /// Сгрупировать уровни по квартирографии
        /// </summary>
        /// <param name="levels"></param>
        /// <returns></returns>
        private List<List<Level>> SortLevels(List<Level> levels)
        {
            /*
            return levels.GroupBy(l => l.AptTypology)
                .ToDictionary(k => k.Key, v => v.ToList())
                .Values
                .ToList();
            */

            var result = new List<List<Level>>();
            var dic = new Dictionary<string, List<Level>>();

            foreach (var level in levels)
            {
                var kvart = level.AptTypology;
                var kvartText = string.Join("_", kvart);  //0_5_2_0_3 - key      List<Level>
                if (dic.ContainsKey(kvartText))
                    dic[kvartText].Add(level);
                else
                    dic.Add(kvartText, new List<Level> { level });
            }

            foreach (var key in dic.Keys)
            {
                result.Add(dic[key]);
            }
            return result;
        }

        /// <summary>
        /// создать уровни из комбинаций
        /// </summary>
        /// <param name="goodCombinations">список комбинаций</param>
        /// <returns></returns>
        private List<Level> CreateLevelFromCombinations(List<string> goodCombinations)
        {
            var levels = new List<Level>();

            foreach (var comb in goodCombinations)
            {
                var codes = comb.Split(',');
                var points = new Point3d[codes.Length];               
                
                //верхние шаги начинаются не с нуля, если у левая нижняя квартира - распашонка
                int topSteps = int.Parse(codes[0].Split('_')[1]);
                int bottomSteps = 0;

                int step = 3500;
                int height = 14760;

                bool topRow = false;

                //Перебор кодов в строке и создание точек вставки
                for (int i = 0; i < codes.Length; i++)
                {
                    //количество верхних и нижних шагов
                    int codeTopSteps;
                    int codeBotSteps;

                    if (codes[i] == "llu")
                    {
                        codeTopSteps = 2;
                        codeBotSteps = 0;
                    }
                    else
                    {
                        codeTopSteps = int.Parse(codes[i].Split('_')[1]);
                        codeBotSteps = int.Parse(codes[i].Split('_')[2]);
                    }
                    

                    if (!topRow) //нижний ряд
                    {
                        if (codes[i].StartsWith("CR")) //крайняя правая квартира нижнего ряда
                        {
                            topRow = true;
                            bottomSteps += codeBotSteps;
                            points[i] = new Point3d(bottomSteps * step, 0, 0);
                        }
                        else //обычные нижние хаты
                        {
                            points[i] = new Point3d(bottomSteps * step, 0, 0);
                            bottomSteps += codeBotSteps;
                        }
                            
                    }
                    else //верхний ряд
                    {
                        if (codes[i].StartsWith("CR")) //крайняя правая квартира верхнего ряда
                        {
                            topSteps += codeTopSteps;
                            points[i] = new Point3d(topSteps * step, height, 0);
                        }
                        else //обычные квартиры сверху и ллу
                        {
                            points[i] = new Point3d(topSteps * step, height, 0);
                            topSteps += codeTopSteps;
                        }    
                            
                    }
                    
                }

                //Собрать все возможные комбинации из доступных квартир для текущего кода уровня
                AllFlatCombinations = new List<List<Flat>>();
                currentFlatStack = new Stack<Flat>();
                GetCombinations(codes, 0);

                //Переместить квартиры на место
                List<List<Flat>> levelFlats = MoveFlats(points);

                foreach (var lf in levelFlats)
                {
                    //временно, потом поправить
                    
                    var lev = new Level(lf);
                    lev.Id = "temp";
                    //lev.Llu = new LLU();

                    var pts = lev.Flats.SelectMany(f => Robot.Util.GetCurveCorners(f.Contour)).ToList();
                    var bbox = new BoundingBox(pts);
                    var ptsCor = bbox.GetCorners().Take(4).ToList();
                    ptsCor.Add(ptsCor[0]);
                    var poly = new Polyline(ptsCor).ToPolylineCurve();

                    lev.Contour = poly;
                    

                    levels.Add(lev);
                }
            }

            return levels;
        }

        private void GetCombinations(string[] codes, int index)
        {
            //если по такому коду нет квартиры, то отбой
            
            if (!dict.ContainsKey(codes[index])) return;
            var flats = dict[codes[index]];

            ++index;
            foreach (var flat in flats)
            {
                currentFlatStack.Push(flat);
                if (index == codes.Length)
                {
                    var temp = currentFlatStack.ToList();
                    temp.Reverse();
                    AllFlatCombinations.Add(temp);
                }
                else
                {
                    GetCombinations(codes, index);                    
                }
                currentFlatStack.Pop();
            }
                
        }

        /// <summary>
        /// Передвигает нужные квартиры на место
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<List<Flat>> MoveFlats(Point3d[] points)
        {
            var result = new List<List<Flat>>();

            foreach (List<Flat> flatComb in AllFlatCombinations)
            {                
                List<Flat> fl = new List<Flat>();
                for (int i = 0; i < points.Length; i++)
                {
                    var flat = flatComb[i];

                    var vec = new Vector3d(points[i].X - flat.BasePlane.OriginX,
                            points[i].Y - flat.BasePlane.OriginY,
                            points[i].Z - flat.BasePlane.OriginZ);

                    var xform = Transform.Translation(vec);

                    var levelFlat = new Flat(flat);
                    levelFlat = levelFlat.Transform(xform) as Flat;

                    fl.Add(levelFlat);
                }
                result.Add(fl);
            }
            return result;
        }

        /// <summary>
        /// Удаляет строки, в которых есть кврартиры, отсутсвующие в flats (Катя)
        


        /// </summary>
        /// <returns></returns>
        private List<string> DeleteExtraRows()
        {
            var goodRows = new List<string>();


            //разбить текст на строки
            var textRows = combinations.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            //создать список всех кодов в наших квартирах
            var flatCodes = new List<string>();
            foreach (var flat in flats)
            {
                flatCodes.Add(flat.Code);
            }

            foreach (var row in textRows)
            {
                var codes = row.Split(',');

                bool ok = true;
                foreach (var code in codes)
                {
                    if (code == "llu")
                        continue;

                    if (!flatCodes.Contains(code))
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                    goodRows.Add(row);
            }
            

            return goodRows;
        }
    }
}