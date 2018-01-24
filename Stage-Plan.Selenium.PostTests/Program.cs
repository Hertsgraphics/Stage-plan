using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Selenium.PostTests
{
    class Program
    {
        static bool _isFail;


        /// <summary>
        /// Requires tempLocation;errorLocation;[originalFile;newFile;locationForNewFile]
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {


            _isFail = false;
            var timeToWait = 2000;
            Console.WriteLine("Waiting ... for " + timeToWait + " milli-seconds... Preparing");

            System.Threading.Thread.Sleep(timeToWait);

            if (args == null)
            {
                Console.WriteLine("args is null");
                Console.ReadKey();
            }

            if (args.Count() <= 0)
            {
                Console.WriteLine("no args found");
                Console.ReadKey();
            }

            Console.WriteLine("args have " + args.Length + " items");

            var j = 0;
            foreach (var item in args)
            {
                Console.WriteLine(j + ": " + item);
                j++;
            }

            var allArgs = String.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Adding " + args[i]);
                allArgs += args[i];
            }

            Console.WriteLine("Arg as string: " + allArgs);

            var split = allArgs.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("Split by ; count: " + split.Length);

            if (split.Count() <= 0)
            {
                Console.WriteLine("0 (or less) number of args after split");
                Console.ReadKey();
            }

            if (split.Count() % 2 != 1)
            {
                Console.WriteLine("invalid number of split items, expected even numer");
                Console.ReadKey();
            }

            var list = new List<Tuple<string, string, string>>();
            for (int i = 2; i < split.Count(); i = i + 3)//+ by 3 because we have original, new, finalLocation
            {
                list.Add(new Tuple<string, string, string>(split.ElementAt(i), split.ElementAt(i + 1), split.ElementAt(i + 2)));

            }

            var tempFolder = split.ElementAt(0);
            var failureFolder = split.ElementAt(1);

            Console.WriteLine();
            Console.WriteLine();
        
            Console.WriteLine("Starting copy process");
            Start(list);

            if (_isFail)
            {
                System.Diagnostics.Process.Start(tempFolder);
                Console.WriteLine("Ended early");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Deleting: " + tempFolder);

                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);


            }

            //this app is only called when failed
            System.Diagnostics.Process.Start(failureFolder);



        }

        static void Start(IEnumerable<Tuple<string, string, string>> images)
        {
            try
            {
                for (int i = 0; i < images.Count(); i++)
                {
                    var original = images.ElementAt(i).Item1;
                    var newFile = images.ElementAt(i).Item2;
                    var newDestination = images.ElementAt(i).Item3;

                    var newDestinationForOriginal = Path.GetDirectoryName(newDestination) + "\\" + "expected.png";
                    
                    var destinationDirectory = Path.GetDirectoryName(newDestination);

                    if (!Directory.Exists(destinationDirectory))
                    {
                        Console.WriteLine("Creating: " + destinationDirectory);
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    if (!File.Exists(newDestination))
                    {
                        Console.WriteLine("Cut from: " + newFile);
                        Console.WriteLine("To: " + newDestination);
                        File.Move(newFile, newDestination);
                    }

                    Console.WriteLine("Copy from: " + original);
                    Console.WriteLine("To: " + newDestinationForOriginal);

                    File.Copy(original, newDestinationForOriginal);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("There was a failure.");
                Console.WriteLine(e.Message);
                _isFail = true;
            }
        }
    }
}
