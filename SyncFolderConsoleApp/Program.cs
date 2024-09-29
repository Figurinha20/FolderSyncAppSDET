using System.Runtime.CompilerServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string ORIGINAL_PATH_KEY = "originalPath";
            const string COPY_PATH_KEY = "copyPath";
            const string INTERVAL_KEY = "interval";
            const string LOG_PATH_KEY = "logPath";

            Dictionary<string, string> argsDic = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-"))
                {
                    string argName = arg.TrimStart('-');
                    argsDic[argName] = args[i + 1];
                }
            }

            string originalFolder = argsDic[ORIGINAL_PATH_KEY];
            string copyFolder = argsDic[COPY_PATH_KEY];
            int interval = GetIntFromText(argsDic[INTERVAL_KEY]);
            string logFile = argsDic[LOG_PATH_KEY];

            foreach (KeyValuePair<string, string> entry in argsDic)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }

            while (true)
            {
                SyncFolders();
                Thread.Sleep(interval * 1000); //seconds to miliseconds
            }

            int GetIntFromText(string newNumber)
            {
                return int.Parse(newNumber);
            }

            void SyncFolders()
            {
                Console.WriteLine($"Synq!");
                CopyDirectory(originalFolder, copyFolder);
            }

            static void CopyDirectory(string sourceDir, string destinationDir)
            {
                // Get information about the source directory
                var dir = new DirectoryInfo(sourceDir);

                // Get information about the copy directory
                var newDir = new DirectoryInfo(destinationDir);

                // Check if the source directory exists
                if (!dir.Exists)
                    throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

                // Create the destination directory
                if (!newDir.Exists)
                {
                    Console.WriteLine($"{destinationDir} doesn't exist! Let's create it!");
                    Directory.CreateDirectory(destinationDir);
                }

                // Cache directories before we start copying
                DirectoryInfo[] dirs = dir.GetDirectories();
                DirectoryInfo[] newDirs = newDir.GetDirectories();

                // Get the files in the source directory and copy to the destination directory
                foreach (FileInfo file in dir.GetFiles())
                {
                    string targetFilePath = Path.Combine(destinationDir, file.Name);
                    if (File.Exists(targetFilePath))
                    {
                        FileInfo newFile = new FileInfo(targetFilePath);
                        if (file.LastWriteTime > newFile.LastWriteTime)
                        {
                            FileInfo updatedFile = new FileInfo(file.FullName);
                            newFile.Delete();
                            updatedFile.CopyTo(targetFilePath);
                            Console.WriteLine($"File {file.Name} updated!");
                        }
                    }
                    else
                    {

                        file.CopyTo(targetFilePath);
                        Console.WriteLine($"File {file.Name} added!");
                    }
                }

                foreach (FileInfo file in newDir.GetFiles())
                {
                    string targetFilePath = Path.Combine(sourceDir, file.Name);
                    if (!File.Exists(targetFilePath))
                    {
                        file.Delete();
                        Console.WriteLine($"File {file.Name} deleted!");
                    }
                }

                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir);
                }

                foreach (DirectoryInfo subDir in newDirs)
                {
                    string newDestinationDir = Path.Combine(sourceDir, subDir.Name);
                    if (!Directory.Exists(newDestinationDir))
                    {
                        subDir.Delete();
                        Console.WriteLine($"Directory {subDir.Name} deleted!");
                    }
                }
            }
        }
    }
}

