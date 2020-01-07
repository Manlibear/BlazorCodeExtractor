using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlazorCodeExtractor
{
    internal class Program
    {
        private static async Task Main()
        {
            var files = Directory.GetFiles("Pages", "*.razor", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                Console.WriteLine($"Processing file {file}");

                var lines = new List<string>
                {
                    Environment.NewLine,
                    "<code>",
                    "</code>"
                };

                var isCode = false;

                using (var sr = File.OpenText(file))
                {
                    var line = sr.ReadLine();

                    while (!string.IsNullOrEmpty(line))
                    {
                        if (line.StartsWith("@code") || isCode)
                        {
                            isCode = true;

                            lines.Insert(lines.Count - 1, line.Replace("@", "@@"));
                        }

                        line = sr.ReadLine();
                    }
                }

                if (isCode)
                {
                    await File.AppendAllLinesAsync(file, lines);
                }
            }
        }
    }
}
