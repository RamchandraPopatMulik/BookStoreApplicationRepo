﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreModel
{
    public class WishlistModel
    {
        public int WishlistID { get; set; }

        public int BookID { get; set; }
        public int UserID { get; set; }
    }
}
