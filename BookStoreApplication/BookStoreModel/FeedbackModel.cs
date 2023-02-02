using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreModel
{
    public class FeedbackModel
    {
        public int FeedbackID { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int UserID { get; set; }
        public int BookID  { get; set; }
    }
}
