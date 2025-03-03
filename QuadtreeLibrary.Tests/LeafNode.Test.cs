using Xunit;

namespace Quadtree.Tests
{
    public class LeafNodeTests
    {
        [Fact]
        public void Insert_Rectangle_ShouldBeFound()
        {
            // Arrange
            var space = new Rectangle(-50, -50, 200, 200);
            var leafNode = new LeafNode(5, space);
            var rect = new Rectangle(10, 10, 30, 30);

            // Act
            leafNode.Insert(rect);
            var found = leafNode.Find(10, 10);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(10, found.x);
            Assert.Equal(10, found.y);
        }

        [Fact]
        public void Find_NonExistentRectangle_ShouldReturnNull()
        {
            // Arrange
            var space = new Rectangle(-50, -50, 200, 200);
            var leafNode = new LeafNode(5, space);

            // Act
            var found = leafNode.Find(30, 30);

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void Delete_Rectangle_ShouldNoLongerBeFound()
        {
            // Arrange
            var space = new Rectangle(-50, -50, 200, 200);
            var leafNode = new LeafNode(5, space);
            var rect = new Rectangle(10, 10, 30, 30);
            leafNode.Insert(rect);

            // Act
            leafNode.Delete(10, 10);
            var found = leafNode.Find(10, 10);

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void Update_Rectangle_ShouldHaveNewDimensions()
        {
            // Arrange
            var space = new Rectangle(-50, -50, 100, 100);
            var leafNode = new LeafNode(5, space);
            var rect = new Rectangle(10, 10, 30, 30);
            leafNode.Insert(rect);

            // Act
            leafNode.Update(10, 10, 40, 40);
            var updatedRect = leafNode.Find(10, 10);

            // Assert
            Assert.NotNull(updatedRect);
            Assert.Equal(40, updatedRect.length);
            Assert.Equal(40, updatedRect.width);
        }

        [Fact]
        public void Insert_WhenThresholdExceeded_ShouldTriggerSplit()
        {
            // Arrange
            var space = new Rectangle(-50, -50, 200, 200);
            var leafNode = new LeafNode(2, space); // Small threshold for testing

            // Insert multiple rectangles to exceed threshold
            leafNode.Insert(new Rectangle(5, 5, 20, 20));
            leafNode.Insert(new Rectangle(-5, -5, 20, 20));


             Assert.Throws<InvalidOperationException>(() => leafNode.Insert(new Rectangle(15, 15, 10, 10)));

   
        }
    }
}
