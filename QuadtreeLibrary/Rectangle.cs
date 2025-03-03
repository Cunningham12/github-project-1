namespace FileReaderParserLibrary.Tests
{
    using System;
    using System.IO;
    using Xunit;

    /// <summary>
    /// Unit tests for verifying the functionality of the <see cref="FileReader"/> class.
    /// </summary>
    public class FileReaderTests
    {
        // Constant to manage the file path for testing.
        private static readonly string FilePath = "testfile.txt";

        /// <summary>
        /// Verifies that the <see cref="FileReader.ReadFile"/> method reads the file content correctly.
        /// </summary>
        [Fact]
        public void Should_ReadFileContentCorrectly_When_FileExists()
        {
            // Setup: Create a file with expected content.
            var expectedContent = "Hello, world!";
            File.WriteAllText(FilePath, expectedContent);

            var reader = new FileReader();

            // Act: Read content from the file.
            var actualContent = reader.ReadFile(FilePath);

            // Assert: Check if the content read from the file matches the expected.
            Assert.Equal(expectedContent, actualContent);

            // Clean up: Delete the file after test execution.
            File.Delete(FilePath);
        }

        /// <summary>
        /// Verifies that the <see cref="FileReader.ReadFile"/> method throws an exception when
        /// trying to read a non-existing file.
        /// </summary>
        [Fact]
        public void Should_ThrowFileNotFoundException_When_FileDoesNotExist()
        {
            // Setup: Path to a non-existent file.
            var nonExistentFilePath = "missingfile.txt";
            var reader = new FileReader();

            // Act & Assert: Check that reading a non-existent file throws an exception.
            Assert.Throws<FileNotFoundException>(() => reader.ReadFile(nonExistentFilePath));
        }
    }
}
