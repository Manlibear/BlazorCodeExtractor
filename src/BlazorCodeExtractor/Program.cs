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
                var lines = new List<string>();
                var isCode = false;

                using (var sr = File.OpenText(file))
                {
                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s.StartsWith("@code") || isCode)
                        {
                            isCode = true;

                            lines.Add(s);
                        }
                    }
                }

                if (isCode)
                {
                    lines.Insert(0, "<code>");
                    lines.Add("</code>");

                    await File.AppendAllLinesAsync(file, lines);
                }

                Console.WriteLine(file);
            }
        }
    }
}
