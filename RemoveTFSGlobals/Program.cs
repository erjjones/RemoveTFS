using System;
using System.Text;
using System.IO;

namespace RemoveTFSGlobals
{
    public class Program
    {
        static void Main(string[] args)
        {
            var sourceDirectory = "d:/platformapps";
            var start = "GlobalSection(TeamFoundationVersionControl) = preSolution";
            var end = "EndGlobalSection";
            var solutions = Directory.GetFiles(sourceDirectory, "*.sln", SearchOption.AllDirectories);
            var inTheGlobalSection = false;
            var count = 0;

            foreach (var x in solutions)
            {
                bool fixit = false;
                var tempFile = Path.GetTempFileName();
                var result = new StringBuilder();
                if (File.Exists(x))
                {
                    using (var sr = new StreamReader(x))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if(line.Contains(start))
                            {
                                count = count + 1;
                                Console.Write(x + Environment.NewLine);
                                fixit = true;
                            }
                        }
                    }

                    if (fixit)
                    {
                        using (var sr = new StreamReader(x))
                        {
                            using (var sw = new StreamWriter(tempFile))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (!inTheGlobalSection)
                                    {
                                        if (!line.Contains(start))
                                        {
                                            sw.WriteLine(line);
                                        }
                                        else
                                        {
                                            inTheGlobalSection = true;
                                        }
                                    }
                                    else
                                    {
                                        {
                                            if (line.Contains(end))
                                            {
                                                inTheGlobalSection = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        File.Delete(x);
                        File.Move(tempFile, x);
                    }
                }
            }
            Console.WriteLine(count);
            Console.ReadLine();
        }

        //public class Functions
        //{
        //    public static long Factorial(int n)
        //    {
        //        // Test for invalid input
        //        if ((n < 0) || (n > 2))
        //        {
        //            return -1;
        //        }

        //        // Calculate the factorial iteratively rather than recursively:
        //        long tempResult = 1;
        //        for (int i = 1; i <= n; i++)
        //        {
        //            tempResult *= i;
        //        }
        //        return tempResult;
        //    }
        //}
    }
}
