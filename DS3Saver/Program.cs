using System;
using System.IO;

namespace DS3Saver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/DarkSoulsIII";
            string destDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/DarkSouls3Save";
            try
            {
                CopyDirectory(filePath, destDir, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Copy completed. Check your desktop for DarkSouls3Save\nPress any key to exit...");
            Console.ReadKey();
        }

       private static void CopyDirectory(string sourceDir, string destDir, bool copySubs)
       {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);    
            DirectoryInfo[] dirs = dir.GetDirectories();

            //Exception if source directory does not exists.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist " + sourceDir);
            }
            // Creates distination directory
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }
            //Get file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                //Create path to the new copy of file.
                string tmpPath = Path.Combine(destDir, file.Name);

                //Copy the file.
                file.CopyTo(tmpPath, false);
            }

            if (copySubs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    //Create subdirectory
                    string tempPath = Path.Combine(destDir, subdir.Name);

                    //Copy subs
                    CopyDirectory(subdir.FullName, tempPath, copySubs);
                }
            }
       }
    }
}
