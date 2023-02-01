using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        public BookModel AddBook(BookModel bookModel);

        public BookModel UpdateBook(BookModel bookModel);

        public bool DeleteBook(int BookID);

        public List<BookModel> GetAllBook();

        public BookModel GetBookByID(int BookID);
    }
}
