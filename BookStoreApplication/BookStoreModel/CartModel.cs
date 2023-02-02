using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreModel
{
    public class CartModel
    {
        public int CartID { get; set; }

        public int CartQuantity { get; set; }

        public int BookID  { get; set; }

        public int UserID { get; set; }
    }
}
