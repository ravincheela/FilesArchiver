using System;
using System.Configuration;
using System.IO;

namespace FileExporter2
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = string.Format(@"{0}{1}\", ConfigurationManager.AppSettings["BasePath"], ConfigurationManager.AppSettings["SourcePath"]);
            string archivePath = string.Format(@"{0}{1}\", ConfigurationManager.AppSettings["BasePath"], ConfigurationManager.AppSettings["ArchivePath"]);
            string fileNamePattern = ConfigurationManager.AppSettings["FileNamePattern"];

            ArchiveFiles(sourcePath, archivePath , new[] { fileNamePattern });

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        internal static void ArchiveFiles(string fromPath, string toPath, string[] fileMasks)
        {
            var fromDirectoryInfo = new DirectoryInfo(fromPath);
            var toDirectoryInfo = new DirectoryInfo(toPath);

            if (!fromDirectoryInfo.Exists) return;

            if (!toDirectoryInfo.Exists)
            {
                Directory.CreateDirectory(toPath);
            }

            foreach (var fileMask in fileMasks)
            {
                var files = fromDirectoryInfo.GetFiles(fileMask);

                foreach (var fileInfo in files)
                {
                    fileInfo.CopyTo(toPath + fileInfo.Name, true);
                    fileInfo.Delete();
                }
            }
        }
    }


}
