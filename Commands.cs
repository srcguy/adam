using System;
using System.IO;
using System.Linq;
using Cosmos.System.FileSystem;

namespace CommandList
{
    public class Commands
    {
        public static void Help()
        {
            Console.WriteLine("\nNavigation");
            Console.WriteLine("  cd <directory>   - Change current directory");
            Console.WriteLine("  ls               - List files and directories in the current directory");

            Console.WriteLine("\nFile operations etc.");
            Console.WriteLine("  mkdir <directory>- Create a new directory");
            Console.WriteLine("  rm <file>        - Delete a file");
            Console.WriteLine("  rmdir <directory>- Delete a directory");
            Console.WriteLine("  cat <file>       - Display a file");
            Console.WriteLine("  touch <file>     - Create an empty file");
            Console.WriteLine("  miv              - Open MIV");
            Console.WriteLine("  pwd              - Print working directory");

            Console.WriteLine("\nSystem commands");
            Console.WriteLine("  reboot           - Reboot the system");
            Console.WriteLine("  help             - Show available commands");
            Console.WriteLine("  cls              - Clear the console screen");
            Console.WriteLine("  shutdown (exit)  - Shutdown the system\n");
        }

        public static string Calc(double a, double b)
        {
            return $"{a} + {b} = {a + b}\n{a} - {b} = {a - b}\n{a} / {b} = {a / b}\n{a} * {b} = {a * b}";
        }

        public static void Ls()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] directories = Directory.GetDirectories(currentDirectory);
            string[] files = Directory.GetFiles(currentDirectory);

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var directory in directories)
            {
                Console.Write(@$"\{Path.GetFileName(directory)} ");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var file in files)
            {
                Console.Write($"{Path.GetFileName(file)} ");
            }
            Console.Write("\n");
        }

        public static void Cat(string[] args)
        {
            if (args.Length > 1)
            {
                string file = string.Join(" ", args.Skip(1));

                try
                {
                    string content = File.ReadAllText(Directory.GetCurrentDirectory() + @"\"+ file);
                    Console.WriteLine(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: cat <file>");
            }
        }

        public static void Touch(string[] args)
        {
            if (args.Length > 1)
            {
                string file = string.Join(" ", args.Skip(1));

                try
                {
                    File.Create(Directory.GetCurrentDirectory() + @"\" + file).Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: touch <file>");
            }
        }

        public static void Cd(string[] args)
        {
            if (args.Length > 1)
            {
                string newDirectory = string.Join(" ", args.Skip(1));

                if (newDirectory == "..")
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string parentDirectory = Path.GetDirectoryName(currentDirectory);

                    if (!string.IsNullOrEmpty(parentDirectory))
                    {
                        try
                        {
                            Directory.SetCurrentDirectory(parentDirectory);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Already at the root directory.");
                    }
                }
                else
                {
                    string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), newDirectory);

                    try
                    {
                        if (Directory.Exists(targetDirectory))
                        {
                            Directory.SetCurrentDirectory(targetDirectory);
                        }
                        else
                        {
                            Console.WriteLine($"Error: Directory '{newDirectory}' not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Usage: cd <directory>");
            }

        }
        public static void Mkdir(string[] args)
        {
            if (args.Length > 1)
            {
                string directory = string.Join(" ", args.Skip(1));

                try
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\" + directory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error y: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: mkdir <directory>");
            }
        }
        public static void Rm(string[] args)
        {
            if (args.Length > 1)
            {
                string file = string.Join(" ", args.Skip(1));

                try
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\" + file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: rm <file>");
            }
        }

        public static void Rmdir(string[] args)
        {
            if (args.Length > 1)
            {
                string directory = string.Join(" ", args.Skip(1));

                try
                {
                    Directory.Delete(Directory.GetCurrentDirectory() + @"\" + directory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: rmdir <directory>");
            }
        }
    }
}
