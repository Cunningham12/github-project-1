
namespace FileReaderParserLibrary;

using System;
using System.IO;

/// <summary>
/// A class for reading content from a file.
/// </summary>
public class FileReader
{
    /// <summary>
    /// Reads the entire content of the specified file.
    /// </summary>
    /// <param name="filePath">The path of the file to read.</param>
    /// <returns>A string containing the content of the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the specified file is not found.</exception>
    public string ReadFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The specified file was not found.", filePath);
        }

        return File.ReadAllText(filePath);
    }
}
