namespace Quadtree
{
    /// <summary>
    /// Represents a node in the quadtree. A node can be either an internal node (with children)
    /// or a leaf node (storing rectangles).
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// The minimum x-coordinate of the node's boundary.
        /// </summary>
        public int xMin { get; set; }

        /// <summary>
        /// The minimum y-coordinate of the node's boundary.
        /// </summary>
        public int yMin { get; set; }

        /// <summary>
        /// The maximum x-coordinate of the node's boundary.
        /// </summary>
        public int xMax { get; set; }

        /// <summary>
        /// The maximum y-coordinate of the node's boundary.
        /// </summary>
        public int yMax { get; set; }

        /// <summary>
        /// A list of rectangles contained within this node. 
        /// This is primarily used by leaf nodes, while internal nodes delegate storage to their child nodes.
        /// </summary>
        public List<Rectangle> Rectangles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="xMin">The minimum x-coordinate of the node's boundary.</param>
        /// <param name="yMin">The minimum y-coordinate of the node's boundary.</param>
        /// <param name="xMax">The maximum x-coordinate of the node's boundary.</param>
        /// <param name="yMax">The maximum y-coordinate of the node's boundary.</param>
        public Node(int xMin, int yMin, int xMax, int yMax)
        {
            Rectangles = new List<Rectangle>();
            this.xMin = xMin;
            this.yMin = yMin;
            this.xMax = xMax;
            this.yMax = yMax;
        }

        /// <summary>
        /// Inserts a rectangle into the node. If the node is a leaf, it stores the rectangle directly.
        /// If the node is an internal node, it delegates the insertion to the appropriate child.
        /// </summary>
        /// <param name="rect">The rectangle to insert.</param>
        public abstract void Insert(Rectangle rect);

        /// <summary>
        /// Deletes a rectangle from the node based on the given coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the rectangle to delete.</param>
        /// <param name="y">The y-coordinate of the rectangle to delete.</param>
        public abstract void Delete(int x, int y);

        /// <summary>
        /// Finds a rectangle in the quadtree based on the given coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the rectangle to find.</param>
        /// <param name="y">The y-coordinate of the rectangle to find.</param>
        /// <returns>The found rectangle, or <c>null</c> if no rectangle exists at the given coordinates.</returns>
        public abstract Rectangle Find(int x, int y);

        /// <summary>
        /// Updates a rectangle's dimensions in the node.
        /// </summary>
        /// <param name="x">The x-coordinate of the rectangle to update.</param>
        /// <param name="y">The y-coordinate of the rectangle to update.</param>
        /// <param name="length">The new length of the rectangle.</param>
        /// <param name="width">The new width of the rectangle.</param>
        public abstract void Update(int x, int y, int length, int width);

        /// <summary>
        /// Dumps the structure of the node for debugging purposes.
        /// </summary>
        /// <param name="level">The depth level of the current node, used for indentation.</param>
        public abstract void Dump(int level);
    }
}
