using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryData
{
    public class BackupHelper
    {
        public void ExportBooks(List<Book> book,string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(book, options);

                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Backup successfully created at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write backup file: {ex.Message}");
            }
        }

        public List<Book> ImportBooks(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Backup file not found at: {filePath}");
                    return new List<Book>();
                }

                string jsonString = File.ReadAllText(filePath);
                List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString);

                return books ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read backup file: {ex.Message}");
                return new List<Book>();
            }
        }
    }
}
