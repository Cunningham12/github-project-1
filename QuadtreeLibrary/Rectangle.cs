namespace Quadtree
{
    /// <summary>
    /// Represents a rectangle defined by its position and dimensions.
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// Gets or sets the x-coordinate of the rectangle's top-left corner.
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of the rectangle's top-left corner.
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// Gets or sets the length (height) of the rectangle.
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class with specified coordinates and dimensions.
        /// </summary>
        /// <param name="x">The x-coordinate of the rectangle's top-left corner.</param>
        /// <param name="y">The y-coordinate of the rectangle's top-left corner.</param>
        /// <param name="length">The length (height) of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        public Rectangle(int x, int y, int length, int width)
        {
            this.x = x;
            this.y = y;
            this.length = length;
            this.width = width;
        }

        /// <summary>
        /// Gets the area of the rectangle.
        /// </summary>
        /// <returns>The area of the rectangle (length * width).</returns>
        public int GetArea()
        {
            return length * width;
        }

        /// <summary>
        /// Gets the perimeter of the rectangle.
        /// </summary>
        /// <returns>The perimeter of the rectangle (2 * (length + width)).</returns>
        public int GetPerimeter()
        {
            return 2 * (length + width);
        }

        /// <summary>
        /// Determines whether a given point is inside the rectangle.
        /// </summary>
        /// <param name="px">The x-coordinate of the point to check.</param>
        /// <param name="py">The y-coordinate of the point to check.</param>
        /// <returns><c>true</c> if the point is inside the rectangle; otherwise, <c>false</c>.</returns>
        public bool IsPointInside(int px, int py)
        {
            return px >= x && px <= x + width && py >= y && py <= y + length;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Rectangle other)
            {
                return this.x == other.x && this.y == other.y &&
                       this.length == other.length && this.width == other.width;
            }
            return false;
        }
        /// <summary> Generates a hash code for the rectangle. </summary>
        public override int GetHashCode() => HashCode.Combine(x, y, length, width);


        /// <summary> Provides a string representation of the rectangle. </summary>
        public override string ToString() => $"Rectangle({x}, {y}, {width}x{length})";
    }
}
