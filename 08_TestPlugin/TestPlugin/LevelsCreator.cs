using Robot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms.VisualStyles;

namespace TestPlugin
{
    public class LevelsCreator
    {
        private List<Flat> flats;
        private string combinations;

        public List<Level> Levels { get; }

        public LevelsCreator(List<Flat> flats, string combinationsTxt)
        {
            this.flats = flats;
            this.combinations = combinationsTxt;

            //удалить лишние строки
            var goodCombinations = DeleteExtraRows();

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
            var result = new List<Level>();



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