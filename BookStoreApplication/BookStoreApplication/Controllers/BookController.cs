using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager bookManager;
        public BookController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }
        [HttpPost]
        [Route("BookStore/AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                BookModel bookData = this.bookManager.AddBook(bookModel);
                if (bookData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully", result = bookData });
                }
                return this.Ok(new { success = true, message = "Book Name Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("BookStore/UpdateBook")]
        public IActionResult UpdateBook(BookModel bookModel)
        {
            try
            {
                BookModel bookData = this.bookManager.UpdateBook(bookModel);
                if (bookData != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated Successfully", result = bookData });
                }
                return this.Ok(new { success = true, message = "Book Not Updated" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("BookStore/DeleteBook")]
        public IActionResult DeleteBook(int BookID)
        {
            try
            {
                bool deleteBook = this.bookManager.DeleteBook(BookID);
                if (deleteBook)
                {
                    return this.Ok(new { success = true, message = "Book Deleted Successfully", result = deleteBook });
                }
                return this.Ok(new { success = true, message = "Book Not Deleted" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/GetAllBook")]
        public IActionResult GetAllBook()
        {
            try
            {
                List<BookModel> allBooks = this.bookManager.GetAllBook();
                if (allBooks != null)
                {
                    return this.Ok(new { success = true, message = "All Books Get Successfully", result = allBooks });
                }
                return this.Ok(new { success = true, message = "No Books Present" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/GetBookByID")]
        public IActionResult GetBookByID(int BookID)
        {
            try
            {
                BookModel Book = this.bookManager.GetBookByID(BookID);
                if (Book != null)
                {
                    return this.Ok(new { success = true, message = "Book Get Successfully", result = Book });
                }
                return this.Ok(new { success = true, message = "Enter Valid Book ID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
