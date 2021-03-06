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
            const string defaultSteamDir = @"C:\Program Files (x86)\Steam\steamapps\common\Beat Saber";
            string PCVRInstallDir;
            string destFolder;
            bool historyEnabled = true;
            bool beatSageMode = false;
            string historyFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"BeatZip\History.txt");
            string beatZipData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"BeatZip");
            string pathHistoryFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"BeatZip\PathHistory.txt");
            string PCVRSongDir;


            if (args.Length != 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToLower())
                    {
                        case "-disablehistory":
                            historyEnabled = false;
                            Console.WriteLine("History disabled!");
                            Thread.Sleep(1000);
                            break;

                        case "disablehistory":
                            historyEnabled = false;
                            Console.WriteLine("History disabled!");
                            Thread.Sleep(1000);
                            break;

                        case "-BeatSageMode":
                            beatSageMode = true;
                            Console.WriteLine("Beat Sage Mode enabled!");
                            Thread.Sleep(1000);
                            break;

                        case "BeatSageMode":
                            beatSageMode = true;
                            Console.WriteLine("Beat Sage Mode enabled!");
                            Thread.Sleep(1000);
                            break;

                        default:
                            Console.WriteLine(args[i] + " is an unknown argument. Ignoring...");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }


            if (historyEnabled == true)
            {
                if (!Directory.Exists(beatZipData))
                {
                    Directory.CreateDirectory(beatZipData);
                    using Stream ST = File.Create(historyFile);
                }
                else if (!File.Exists(historyFile))
                {
                    using Stream ST = File.Create(historyFile);
                }
                else
                {
                    Console.WriteLine("History file detected and enabled!");
                    Console.WriteLine("To disable history start the application with the -disablehistory argument");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                if (!File.Exists(pathHistoryFile))
                {
                    using Stream ST = File.Create(pathHistoryFile);
                }
            }
            string[] history = File.ReadAllLines(historyFile);
            string[] pathHistory = File.ReadAllLines(pathHistoryFile);

            if (!Directory.Exists(defaultSteamDir))
            {
                if (pathHistory.Length == 0)
                {
                    Console.Write("Enter your PCVR Beat Saber installation directory: ");
                    PCVRInstallDir = Console.ReadLine();
                    using StreamWriter sw = File.AppendText(pathHistoryFile);
                    sw.WriteLine(PCVRInstallDir);
                }
                else
                {
                    PCVRInstallDir = pathHistory[0];
                }
            }
            else
            {
                Console.WriteLine("Default Steam installation detected!");
                Thread.Sleep(2000);
                PCVRInstallDir = defaultSteamDir;
            }

            if (pathHistory.Length !<= 1)
            {
                Console.Write("Enter your destination folder (the folder where to put all the zips): ");
                destFolder = Console.ReadLine();
                using StreamWriter sw = File.AppendText(pathHistoryFile);
                sw.WriteLine(destFolder);
            }
            else
            {
                destFolder = pathHistory[1];
            }

            string destFolderOld = destFolder + @"\old";
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
                Directory.CreateDirectory(destFolderOld);
            }
            else if (!Directory.Exists(destFolderOld))
            {
                Directory.CreateDirectory(destFolderOld);
            }

            if (!beatSageMode)
            {
                PCVRSongDir = PCVRInstallDir + @"\Beat Saber_Data\CustomLevels";
            }
            else
            {
                PCVRSongDir = PCVRInstallDir;
            }
            
            string[] subdirs = Directory.GetDirectories(PCVRSongDir);


            for (int i = 0; i < subdirs.Length; i++)
            {
                string currentFileName = Path.GetFileName(Path.TrimEndingDirectorySeparator(subdirs[i])) + ".zip";
                Console.WriteLine(currentFileName);
                string currentFilePath = destFolder + @"\" + currentFileName;

                if (File.Exists(currentFilePath))
                {
                    Console.WriteLine("Already exists! Moving to old {0} out of {1}", i + 1, subdirs.Length);
                    File.Move(currentFilePath, destFolderOld + @"\" + currentFileName, true);
                    Console.WriteLine();
                    Thread.Sleep(5);
                    continue;
                }
                else if (File.Exists(destFolderOld + @"\" + currentFileName))
                {
                    Console.WriteLine("Already exists in old! Skipping {0} out of {1}", i + 1, subdirs.Length);
                    Console.WriteLine();
                    Thread.Sleep(5);
                    continue;
                }
                else if (historyEnabled == true)
                {
                    bool foundInHistory = false;
                    foreach (string song in history)
                    {
                        if (song == currentFileName)
                        {
                            Console.WriteLine("Found in history! Skipping {0} out of {1}", i + 1, subdirs.Length);
                            Console.WriteLine();
                            foundInHistory = true;
                            break;
                        }
                    }
                    if (foundInHistory)
                    {
                        continue;
                    }
                }
                Console.WriteLine("Zipping {0} out of {1}", i + 1, subdirs.Length);
                ZipFile.CreateFromDirectory(subdirs[i], destFolder + @"\" + currentFileName);
                Console.WriteLine("Zipped succesfully!");
                if (historyEnabled == true)
                {
                    using StreamWriter sw = File.AppendText(historyFile);
                    sw.WriteLine(currentFileName);
                }

                Console.WriteLine();
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
