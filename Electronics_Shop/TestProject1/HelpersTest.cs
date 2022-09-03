using Electronics_Shop.Helpers;

namespace TestProject1
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TwoPieceDisount_Test()
        {
            // Arrange
            decimal unitPrice = new(10000);
            int qty = 7;
            int discountPercentage = 10;

            // Act
            var result = DiscountHandler.TwoPieceDisount(unitPrice, qty, discountPercentage);

            //Applied for 6 items with value 6000 subtracted from the total
            
            // Assert
            Assert.AreEqual((decimal)64000, result, "Discount is wrongly calculated");
        }
    }
}