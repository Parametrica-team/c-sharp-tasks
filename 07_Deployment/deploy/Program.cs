using System;
using System.IO;
using System.Linq;

namespace deploy
{
    class Program
    {
        static void Main(string[] args)
        {
           
            if (args.Length> 0)
            {
                var currentFolder = Environment.CurrentDirectory;

                if (args[0] == "build")
                {
                    BuildRelease(currentFolder, args);
                }
                else if (args[0] == "update")
                {
                    UpdateSettings(currentFolder, args);
                }
            }

            Console.WriteLine();
                
        }

        private static void UpdateSettings(string currentFolder, string[] args)
        {
            string deployFolder;
            if (args.Length > 1)
                deployFolder = args[1];
            else
                deployFolder = "00_RELEASE";

            if (!Directory.Exists(deployFolder))
            {
                Console.WriteLine($"Путь не найден {deployFolder}");
                return;
            }

            //get all content
            var allPaths = Directory.GetFiles(deployFolder, "*", SearchOption.AllDirectories);
            var relativePaths = allPaths.Select(p => Path.GetRelativePath(deployFolder, p)).ToList();

            var writer = File.CreateText(".deploysettings");
            Console.WriteLine("Запись файла .deploysettings...");
            writer.WriteLine(deployFolder);
            Console.WriteLine(deployFolder);
            foreach (var line in relativePaths)
            {
                writer.WriteLine(line);
                Console.WriteLine(line);
            }
            writer.Close();
        }

        private static void BuildRelease(string currentFolder, string[] args)
        {
            var settingsPath = Path.Combine(currentFolder, ".deploysettings");

            if (!File.Exists(settingsPath))
            {
                Console.WriteLine("Не найден файл .settings");
                return;
            }

            string[] settingsLines;
            try
            {
                settingsLines = File.ReadAllLines(settingsPath, System.Text.Encoding.Default);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            if (settingsLines.Length < 1)
            {
                Console.WriteLine(".deploysettings не содержит строк");
                return;
            }

            var deployPath = Path.GetFullPath(settingsLines[0]);
            
            Directory.CreateDirectory(deployPath);

            //delete old stuff
            var oldDirs = Directory.GetDirectories(deployPath);
            foreach (var dir in oldDirs)
            {
                Directory.Delete(dir, true);
            }
            var oldFiles = Directory.GetFiles(deployPath);
            foreach (var path in oldFiles)
            {
                File.Delete(path);
            }           

            //Copy new staff
            foreach (var path in settingsLines.Skip(1))
            {
                if (File.Exists(path))
                {
                    var targetPath = Path.Combine(deployPath, path);
                    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                    File.Copy(path, targetPath);
                    Console.WriteLine($"Copied to {targetPath}");
                }                
            }
        }
    }
}
