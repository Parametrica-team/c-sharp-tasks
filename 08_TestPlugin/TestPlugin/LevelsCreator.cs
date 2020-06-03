using Rhino.Geometry;
using Robot;
using System;
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

        public List<Level> Levels { get; }

        public LevelsCreator(List<Flat> flats, string combinationsTxt)
        {
            this.flats = flats;
            this.combinations = combinationsTxt;

            //удалить лишние строки
            var goodCombinations = DeleteExtraRows();

            //создать словать соответсвий квартир и кодов
            dict = flats.GroupBy(f => f.Code).ToDictionary(g => g.Key, g => g.ToList());

            //создать уровни из комбинаций
            List<Level> levels = CreateLevelFromCombinations(goodCombinations);

            //сортировать по
            List<List<Level>> sortedLevels = SortLevels(levels);

            int hStep = 100000;
            int vStep = 50000;
            Levels = PlaceLevels(sortedLevels, hStep, vStep);

        }


        private List<Level> PlaceLevels(List<List<Level>> sortedLevels, int horisontalStep, int verticalStep)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сгрупировать уровни по квартирографии
        /// </summary>
        /// <param name="levels"></param>
        /// <returns></returns>
        private List<List<Level>> SortLevels(List<Level> levels)
        {
            var result = new List<List<Level>>();

            //levels[0].AptTypology


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
                int height = 15000;

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

                //Переместить квартиры на место
                List<List<Flat>> levelFlats = MoveFlats(codes, points);

                foreach (var lf in levelFlats)
                {
                    levels.Add(new Level(lf));
                }
            }

            return levels;
        }

        /// <summary>
        /// Передвигает нужные квартиры на место
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<List<Flat>> MoveFlats(string[] codes, Point3d[] points)
        {
            throw new NotImplementedException();
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