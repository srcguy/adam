using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using MIVeditor;
using CommandList;
using System.IO;
using Cosmos.System.FileSystem.VFS;
using System.Reflection.PortableExecutable;


namespace cosmos
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        public static string file;

        static void Eve()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string currentDirectory = Directory.GetCurrentDirectory();

            Console.Write($"{currentDirectory}>>");
            string input = Console.ReadLine();
            string[] args = input.Split(' ');
            string command = args[0].ToLower();
            switch (command)
            {
                case "calc":
                    if (args.Length == 3 && double.TryParse(args[1], out double a) && double.TryParse(args[2], out double b))
                    {
                        Console.WriteLine(Commands.Calc(a, b));
                    }
                    else
                    {
                        Console.WriteLine("Invalid arguments");
                    }
                    break;
                
                //file manipulation
                case "ls":
                    Commands.Ls();
                    break;
                case "cat":
                    Commands.Cat(args);
                    break;
                case "touch":
                    Commands.Touch(args);
                    break;
                case "cd":
                    Commands.Cd(args);
                    break;
                case "rm":
                    Commands.Rm(args);
                    break;
                case "mkdir":
                    Commands.Mkdir(args);
                    break;
                case "rmdir":
                    Commands.Rmdir(args);
                    break;
                case "miv":
                    MIV.StartMIV();
                    break;

                //system
                case "exit":
                    Sys.Power.Shutdown();
                    break;
                case "cls":
                    Console.Clear();
                    break;
                case "reboot":
                    Sys.Power.Reboot();
                    break;
                case "help":
                    Commands.Help();
                    break;

                //other

                case "date":
                    Console.WriteLine(DateTime.Now);
                    break;

            }
            Eve();
        }

        protected override void BeforeRun()
        {
            VFSManager.RegisterVFS(fs);

            var available_space = fs.GetAvailableFreeSpace(@"0:\");
            var fs_type = fs.GetFileSystemType(@"0:\");
            Console.WriteLine("Available free space: " + available_space);
            Console.WriteLine("File system: " + fs_type);
            Console.WriteLine($"{Cosmos.Core.CPU.GetCPUBrandString()}");
            Console.WriteLine($"RAM: {Cosmos.Core.CPU.GetAmountOfRAM()} MB");

            Directory.SetCurrentDirectory(@"0:\");
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Current directory: " + currentDirectory);

        }

        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nAdam b1.0 ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("by src_guy");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("booted succesfully \n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("God's in his heaven. All's right with the world. \n");

            Eve();
        }
    }
}