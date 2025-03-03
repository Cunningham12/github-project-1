
namespace Quadtree.Tests
{
    public class QuadtreeTest
    {
        // Test inserting a rectangle into the quadtree
        [Fact]
        public void TestInsertRectangle()
        {
            // Create a quadtree with bounds and a threshold for subdivisions
            var quadtree = new Quadtree(0, 0, 100, 100, 4);  // Bounds from (0,0) to (100,100) and threshold 4

            // Create a rectangle with position (10, 10) and size 5x5
            var rect = new Rectangle(10, 10, 5, 5);

            // Insert the rectangle into the quadtree
            quadtree.Insert(rect);

            // Try to find the rectangle in the quadtree by its position (10, 10)
            var foundRect = quadtree.Find(10, 10);

            // Assert that the found rectangle is equal to the inserted rectangle
            Assert.Equal(rect, foundRect);
        }

        // Test deleting a rectangle from the quadtree
        [Fact]
        public void TestDeleteRectangle()
        {
            // Create a quadtree with bounds and a threshold for subdivisions
            var quadtree = new Quadtree(0, 0, 100, 100, 4);  // Bounds from (0,0) to (100,100) and threshold 4

            // Create a rectangle with position (10, 10) and size 5x5
            var rect = new Rectangle(10, 10, 5, 5);

            // Insert the rectangle into the quadtree
            quadtree.Insert(rect);

            // Now delete the rectangle at position (10, 10)
            quadtree.Delete(10, 10);

            // Try to find the rectangle in the quadtree after deletion
            var foundRect = quadtree.Find(10, 10);

            // Assert that no rectangle was found after deletion
            Assert.Null(foundRect);
        }

        // Test inserting multiple rectangles
        [Fact]
        public void TestInsertMultipleRectangles()
        {
            var quadtree = new Quadtree(0, 0, 100, 100, 4);  // Bounds from (0,0) to (100,100) and threshold 4

            // Create multiple rectangles
            var rect1 = new Rectangle(10, 10, 5, 5);
            var rect2 = new Rectangle(20, 20, 10, 10);
            var rect3 = new Rectangle(30, 30, 5, 5);

            // Insert rectangles into the quadtree
            quadtree.Insert(rect1);
            quadtree.Insert(rect2);
            quadtree.Insert(rect3);

            // Try to find each rectangle by their position
            var foundRect1 = quadtree.Find(10, 10);
            var foundRect2 = quadtree.Find(20, 20);
            var foundRect3 = quadtree.Find(30, 30);

            // Assert that the correct rectangles are found
            Assert.Equal(rect1, foundRect1);
            Assert.Equal(rect2, foundRect2);
            Assert.Equal(rect3, foundRect3);
        }
    }
}
