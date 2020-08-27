using FlexiCart.Model.Interfaces;

namespace FlexiCart.Model.Entities
{
    public class CartItem
    {
        public CartItem()
        {
            ProcessedQty = 0;
            ToBeProcessedQty = OrderedQty;
            GrossAmount = 0;
        }

        /// <summary>
        /// Represent the actual Product from db and exposed as Item
        /// </summary>
        public IProduct Item { get; set; }

        /// <summary>
        /// Ordered quantity from UI 
        /// </summary>
        public int OrderedQty { get; set; }

        /// <summary>
        /// when we have different rules to applied we need to determined which are processed [Mutually Exclusive]
        /// </summary>
        public int ProcessedQty { get; set; }

        /// <summary>
        /// these are remaining qty which need to processed or fallback to default calculation without any promotion benfit
        /// </summary>
        public int ToBeProcessedQty { get; set; }

        /// <summary>
        /// Each item price by default product unitprice * qty
        /// </summary>
        public decimal GrossAmount { get; set; }


    }
}
