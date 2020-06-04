using System;
using System.IO;
using System.IO.Compression;

namespace BeatZip
{
    class Program
    {

        static void Main(string[] args)
        {
            string PCVRInstallDir;
            string destFolder;
            Console.Write("Enter your PCVR Beat Saber installation directory: ");
            PCVRInstallDir = Console.ReadLine();
            Console.Write("Enter your destination folder (the folder where to put all the zips): ");
            destFolder = Console.ReadLine();

            string PCVRSongDir = PCVRInstallDir + @"\Beat Saber_Data\CustomLevels";
            string[] subdirs = Directory.GetDirectories(PCVRSongDir);

            for (int i = 0; i < subdirs.Length; i++)
            {
                Console.WriteLine(subdirs[i]);
                Console.WriteLine("Zipping!");
                ZipFile.CreateFromDirectory(subdirs[i], destFolder + @"\" + subdirs[i].Substring(73) + ".zip");
                Console.WriteLine("Zipped succesfully!");
            }
            Console.WriteLine("All files zipped succesfully");
            Console.ReadKey();
        }
    }
}
