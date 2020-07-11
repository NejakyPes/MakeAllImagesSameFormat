using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MakeAllImagesSameFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Log("Application started");
            IEnumerable<string> files = Directory.EnumerateFiles(Variables.FileLocation);
            if (files.Count() > 0)
            {
                Log($"Founded {files.Count()} files to process");
                foreach (string file in files)
                {
                    try
                    {
                        string[] fileParse = file.Split('\\');
                        string name = fileParse[fileParse.Length - 1];

                        Regex regex = new Regex(@"\d{8}_\d{6}(\.jpg|\.png)");
                        Match match = regex.Match(name);
                        if (match.Success)
                        {
                            string folder = $"{ Variables.FileLocation}\\{ match.Value.Split('_')[0]}";
                            Directory.CreateDirectory(folder);
                            if (Directory.EnumerateFiles(folder).Any(s => s.Contains(match.Value)))
                            {
                                Delete("Duplicate", name);
                                continue;
                            }
                            //Console.WriteLine($"{name} => {Variables.FileLocation}\\{match.Value}");
                            Log($"Move[{name}] | {Variables.FileLocation}\\{folder}\\{match.Value}");
                            File.Move($"{Variables.FileLocation}\\{name}", $"{folder}\\{match.Value}");
                        }
                        else
                            Delete("Regex.NotMatch", name);
                    }
                    catch (Exception e)
                    {
                        Log($"Error | {e.Message}");
                    }
                }
            }
            else
                Log("None file found");
            Log("Application ended");
        }

        private static void Delete(string reason, string name)
        {
            File.Delete($"{Variables.FileLocation}//{name}");
            Log($"Delete[{reason}] | {name}");
        }

        private static void Log(string text)
        {
            using (StreamWriter sw = File.AppendText(Variables.LogLocation+"Log.txt"))
            {
                sw.WriteLine($"[{DateTime.Now}] {text}");
            }
        }
    }
}
