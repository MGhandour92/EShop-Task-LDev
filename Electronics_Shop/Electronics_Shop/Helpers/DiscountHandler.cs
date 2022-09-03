namespace Electronics_Shop.Helpers
{
    public static class DiscountHandler
    {
        /// <summary>
        /// Calculating Discount to be applied if two or even number of pieces placed
        /// </summary>
        /// <param name="unitPrice">Price of a single product</param>
        /// <param name="qty">Qty ordered</param>
        /// <param name="discountP">Discount percentage to be applied </param>
        /// <returns></returns>
        public static decimal TwoPieceDisount(decimal unitPrice, int qty, int discountP)
        {
            //Total Result without discount
            decimal result = unitPrice * qty;

            //if eligible
            if (qty > 1 && discountP > 0)
            {
                //get the nearest even qty if it is not already even
                decimal evenQty = (decimal)((qty % 2 == 0) ? qty : (qty - 1));

                //caluclate result with discount
                decimal dDisc = ((decimal)discountP) / 100;

                //Subtract the discount from the total
                result -= (evenQty * unitPrice * dDisc);
            }

            return result;
        }
    }
}
