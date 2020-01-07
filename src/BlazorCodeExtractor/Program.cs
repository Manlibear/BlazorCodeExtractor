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

                if (File.Exists(markdownFile))
                {
                    File.Delete(markdownFile);
                }

                if (!codeBlock.Any())
                {
                    continue;
                }

                Console.WriteLine($"Creating file {markdownFile}");

                await using var streamWriter = File.CreateText(markdownFile);
                await streamWriter.WriteLineAsync("```");

                foreach (var line in codeBlock)
                {
                    await streamWriter.WriteLineAsync(line);
                }

                await streamWriter.WriteLineAsync("```");
            }
        }

        private static async Task<IList<string>> TryGetCode(string file)
        {
            using var reader = File.OpenText(file);

            var codeBlock = new List<string>();
            var isCode = false;
            var line = await reader.ReadLineAsync();

            while (!string.IsNullOrEmpty(line))
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
