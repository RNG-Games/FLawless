using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveLibrary
{
    /// <summary>
    /// Small Program to complete Compilation
    /// because I havn't found a way to do this in VS
    /// </summary>
    class Program
    {
        /// <summary>
        /// Move the Libraries to good locations
        /// </summary>
        /// <param name="args">
        /// Command Line Arguments: 
        /// 0 - Path to application folder
        /// 1 - Architekture
        /// </param>
        static void Main(string[] args)
        {
            var applicationFolderPath = "";
            if (args.Length >= 1)
            {
                applicationFolderPath = args[0];
            }
            else
            {
                throw new ArgumentException("No path specified");
            }
            Console.WriteLine("Application Folder: " + applicationFolderPath);

            var libraryPath = "";
            if (args.Length >= 2)
            {
                switch (args[1])
                {
                    case "x64":
                        libraryPath = applicationFolderPath + "lib\\x64\\";
                        break;
                    case "x86":
                        libraryPath = applicationFolderPath + "lib\\x86\\";
                        break;
                    case "AnyCPU":
                        libraryPath = applicationFolderPath + "lib\\x64\\";
                        break;
                    default:
                        throw new ArgumentException("Cannot identify architecture \"" + args[1] + "\"");
                }
            }
            else
            {
                throw new ArgumentException("No architecture specified");
            }
            Console.WriteLine("Library Folder: " + libraryPath);

            Console.WriteLine("Try moving library");
            foreach (var file in Directory.GetFiles(libraryPath, "*.dll"))
            {
                if(File.Exists(applicationFolderPath + "\\" + Path.GetFileName(file)))
                    File.Delete(applicationFolderPath + "\\" + Path.GetFileName(file));
                File.Move(file, applicationFolderPath + "\\" + Path.GetFileName(file));
            }
            Console.WriteLine("Done moving files");
            Console.WriteLine("Delete Directory");
            Directory.Delete(applicationFolderPath + "\\lib", true);
            Console.WriteLine("Done");
        }
    }
}
