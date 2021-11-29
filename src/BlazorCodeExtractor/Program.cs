using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                var codeBlock = await TryGetCode(file);
                var markdownFile = file.Replace(".razor", ".razor.md");
                var targetFile = $"wwwroot\\{markdownFile}".Replace("\\Pages\\", "\\Code\\");

                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                if (!codeBlock.Any())
                {
                    Console.WriteLine($"...No @code section found, skipping\n");
                    continue;
                }

                Console.WriteLine($"...Creating file {targetFile}\n");

                await using var streamWriter = File.CreateText(targetFile);

                foreach (var line in codeBlock)
                {
                    await streamWriter.WriteLineAsync(line);
                }
            }
        }

        private static async Task<IList<string>> TryGetCode(string file)
        {
            using var reader = File.OpenText(file);

            var codeBlock = new List<string>();
            var isCode = false;
            var line = await reader.ReadLineAsync();

            while (line != null)
            {
                if (line.StartsWith("@code") || isCode) // TODO: Needs smarter way to know when code block ends
                {
                    isCode = true;

                    codeBlock.Add(line);
                }

                line = await reader.ReadLineAsync();
            }

            return codeBlock;
        }
    }
}
