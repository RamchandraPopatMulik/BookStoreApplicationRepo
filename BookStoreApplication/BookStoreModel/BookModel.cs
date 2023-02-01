using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreModel
{
    public class BookModel
    {
        public int BookID { get; set; }

        public string? BookName { get; set; }

        public string? AuthorName { get; set; }

        public int Ratings { get; set; }
        public long No_Of_People_Rated { get; set; }

        public int Discount_Price { get; set; }

        public int Original_Price { get; set; }

        public string? Book_Details { get; set; }

        public string? Book_Image { get; set; }

        public int Book_Quantity { get; set; }

    }
}
