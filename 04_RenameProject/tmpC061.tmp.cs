using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv",
                RestoreDirectory = true
            };
            result = openFileDialog.ShowDialog(); // открывает окно выбора файла

            string filePath = "";
            string[] lines = System.IO.File.ReadAllLines(filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
                filePath = openFileDialog.FileName;

            if (string.IsNullOrEmpty(filePath))
                return;


            //Найти все файлы JSON
            string[] jsonPaths = GetFileNames(folderPath, ".json");
            if (jsonPaths == null)
            {
                Console.WriteLine("нет файлов .json");
                Console.ReadKey();
                return;
            }
                
            //Переименовать
            foreach (var jsonPath in jsonPaths)
            {
                bool success = RenameProperties(jsonPath, filePath);
                

                //Вывести на коснсоль результат в виде: С:/.../вася.json переименован (или "не найдено подходящих свойств")
            }
        }

        private static bool RenameProperties(string jsonPath, string filePath)
        {
            throw new NotImplementedException();
            {
                foreach (string line in lines)
                {
                    var a = line.Split(';');
                    var x = a[0];
                }
                return "";

                var key = "project_name"|| "code_name" ||"part_name";

                if (x == key)

                {
                    return a[1].Trim();
                }
            }
            return true;
        }

        private static string[] GetFileNames(string folderPath, string extension)
        {
            var allFiles = System.IO.Directory.GetFiles(folderPath, "*", System.IO.SearchOption.AllDirectories);
            // доделать

            return null;

        
        }
    }
}
