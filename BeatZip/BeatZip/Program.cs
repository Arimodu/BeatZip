﻿using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

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
                string currentFileName = Path.GetFileName(subdirs[i]) + ".zip";
                Console.WriteLine(currentFileName);
                if (File.Exists(destFolder + @"\" + currentFileName))
                {
                    Console.WriteLine("Already exists! Skipping!");
                    Thread.Sleep(5);
                    Console.WriteLine();
                    continue;
                }
                else
                {
                    Console.WriteLine("Zipping!");
                    ZipFile.CreateFromDirectory(subdirs[i], destFolder + @"\" + currentFileName);
                    Console.WriteLine("Zipped succesfully!");
                    Console.WriteLine();
                }
            }
            Thread.Sleep(500);
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("All files zipped succesfully");
            Thread.Sleep(10000);
        }
    }
}
