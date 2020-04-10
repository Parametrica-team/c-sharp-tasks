using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _04_RenameProject
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            //Выбрать папку с JSON
            var openFolderDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Выберите папку с файлами JSON"
            };
            var result = openFolderDialog.ShowDialog();  // открывает окно выбора папки

            string folderPath = "";

            if (result == System.Windows.Forms.DialogResult.OK)
                folderPath = openFolderDialog.SelectedPath;

            if (string.IsNullOrEmpty(folderPath))
                return;


            //Выбрать файл csv для переименования
            var openFileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Title = "Выберите файл c таблицей переименований",
                Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt",
                RestoreDirectory = true
            };
            result = openFileDialog.ShowDialog(); // открывает окно выбора файла

            string filePath = "";

            if (result == System.Windows.Forms.DialogResult.OK)
                filePath = openFileDialog.FileName;

            if (string.IsNullOrEmpty(filePath))
                return;


            //Найти все файлы JSON
            var jsonPaths = GetFileNames(folderPath, ".json");
            if (jsonPaths == null)
            {
                Console.WriteLine("нет файлов .json");
                Console.ReadKey();
                return;
            }

            //Переименовать
            foreach (var jsonPath in jsonPaths)
            {
                string json = RenameProperties(jsonPath, filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    System.IO.File.WriteAllText(jsonPath, json);
                    Console.WriteLine($"Файл {jsonPath} перезаписан");
                }
                else
                {
                    Console.WriteLine($"В файле {jsonPath} не найдено подходящих свойств");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Перезапись завершена, нажмите любую клавишу");
            Console.ReadKey();
        }

        private static string RenameProperties(string jsonPath, string csvPath)
        {
            if (string.IsNullOrEmpty(jsonPath) || string.IsNullOrEmpty(jsonPath)) 
                return null;

            string json;
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(csvPath, Encoding.Default);
                json = System.IO.File.ReadAllText(jsonPath, Encoding.Default);
            }
            catch
            {
                return null;
            }

            bool result = false;

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                string pattern = @"(?<= """ + key + @""":\s"")[\w\s]+(?="")";

                if (Regex.IsMatch(json, pattern))
                {
                    json = Regex.Replace(json, pattern, value);
                    result = true;
                }
            }

            if (result)
                return json;
            else
                return null;
        }

        private static List<string> GetFileNames(string folderPath, string extension)
        {
            var allFiles = System.IO.Directory.GetFiles(folderPath, "*", System.IO.SearchOption.AllDirectories);
            if (allFiles == null)
                return null;

            var jsons = new List<string>();

            foreach (var path in allFiles)
            {
                if (System.IO.Path.GetExtension(path).ToLower() == extension.ToLower())
                    jsons.Add(path);
            }

            return jsons;
        }
    }
}
