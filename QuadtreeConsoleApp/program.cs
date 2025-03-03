
using System;
using System.Collections.Generic;
using FileReaderParserLibrary;
using Quadtree;

namespace QuadtreeCLI
{
    /// <summary>
    /// The main entry point for the Quadtree command-line interface (CLI).
    /// Reads commands from a file and processes them using a Quadtree structure.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main method that starts the QuadtreeCLI program.
        /// </summary>
        /// <param name="args">Command-line arguments. Expects a single argument: the path to a command file.</param>
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: QuadtreeCLI <path_to_cmmd_file>");
                return;
            }

            string filePath = args[0];

            // Parse commands from the file using the static method
            var commands = CommandParser.ParseCommands(filePath);

            if (commands.Count == 0)
            {
                Console.WriteLine("No commands found in the file.");
                return;
            }

            // Initialize the quadtree
            var initialSpace = new Rectangle(-50, -50, 100, 100);
            var quadtree = new LeafNode(5, initialSpace);

            // Process each command
            foreach (var command in commands)
            {
                try
                {
                    var commandParts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (commandParts.Length == 0) continue;

                    string action = commandParts[0].ToLower();

                    switch (action)
                    {
                        case "insert":
                            ProcessInsertCommand(commandParts, quadtree);
                            break;

                        case "delete":
                            ProcessDeleteCommand(commandParts, quadtree);
                            break;

                        case "find":
                            ProcessFindCommand(commandParts, quadtree);
                            break;

                        case "update":
                            ProcessUpdateCommand(commandParts, quadtree);
                            break;

                        case "dump":
                            quadtree.Dump(0);
                            break;

                        default:
                            Console.WriteLine($"Unknown command: {action}");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Error: Invalid number format in command: {command}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error processing command '{command}': {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Processes an "insert" command by adding a new rectangle to the Quadtree.
        /// </summary>
        /// <param name="commandParts">An array of command parameters.</param>
        /// <param name="quadtree">The Quadtree instance.</param>
        private static void ProcessInsertCommand(string[] commandParts, LeafNode quadtree)
        {
            if (commandParts.Length == 5 &&
                int.TryParse(commandParts[1], out int x) &&
                int.TryParse(commandParts[2], out int y) &&
                int.TryParse(commandParts[3], out int width) &&
                int.TryParse(commandParts[4], out int height))
            {
                var rect = new Rectangle(x, y, width, height);
                quadtree.Insert(rect);
            }
            else
            {
                Console.WriteLine("Invalid Insert command format.");
            }
        }

        /// <summary>
        /// Processes a "delete" command by removing a rectangle from the Quadtree.
        /// </summary>
        /// <param name="commandParts">An array of command parameters.</param>
        /// <param name="quadtree">The Quadtree instance.</param>
        private static void ProcessDeleteCommand(string[] commandParts, LeafNode quadtree)
        {
            if (commandParts.Length == 3 &&
                int.TryParse(commandParts[1], out int x) &&
                int.TryParse(commandParts[2], out int y))
            {
                var rect = quadtree.Find(x, y);
                if (rect != null)
                {
                    quadtree.Delete(rect);
                }
                else
                {
                    Console.WriteLine($"Nothing to delete at {x}, {y}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Delete command format.");
            }
        }

        /// <summary>
        /// Processes a "find" command by searching for a rectangle at the given coordinates.
        /// </summary>
        /// <param name="commandParts">An array of command parameters.</param>
        /// <param name="quadtree">The Quadtree instance.</param>
        private static void ProcessFindCommand(string[] commandParts, LeafNode quadtree)
        {
            if (commandParts.Length == 3 &&
                int.TryParse(commandParts[1], out int x) &&
                int.TryParse(commandParts[2], out int y))
            {
                var rect = quadtree.Find(x, y);
                if (rect != null)
                {
                    Console.WriteLine($"Rectangle at {x}, {y}: {rect.width}x{rect.length}");
                }
                else
                {
                    Console.WriteLine($"Nothing is at {x}, {y}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Find command format.");
            }
        }

        /// <summary>
        /// Processes an "update" command by modifying a rectangle's dimensions in the Quadtree.
        /// </summary>
        /// <param name="commandParts">An array of command parameters.</param>
        /// <param name="quadtree">The Quadtree instance.</param>
        private static void ProcessUpdateCommand(string[] commandParts, LeafNode quadtree)
        {
            if (commandParts.Length == 5 &&
                int.TryParse(commandParts[1], out int x) &&
                int.TryParse(commandParts[2], out int y) &&
                int.TryParse(commandParts[3], out int newWidth) &&
                int.TryParse(commandParts[4], out int newHeight))
            {
                var rect = quadtree.Find(x, y);
                if (rect != null)
                {
                    quadtree.Update(x, y, newWidth, newHeight);
                }
                else
                {
                    Console.WriteLine($"Nothing to update at {x}, {y}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Update command format.");
            }
        }
    }
}
