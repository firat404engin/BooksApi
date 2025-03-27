using BooksApi.Context;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedBooksController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public RentedBooksController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getAllRentedBooks")]
        public IActionResult GetAllRentedBooks()
        {
            var rentedBooks=_dbContext.RentedBooks.ToList();
            foreach(var book in rentedBooks)
            {
                book.User=_dbContext.Users.Find(book.UserId);
                book.Book=_dbContext.Books.Find(book.BookId);
            }
            return Ok(rentedBooks);
        }

        [HttpGet("getByIdRentedBook")]
        public IActionResult GetByIdRentedBooks(int id)
        {
            var rentedBook = _dbContext.RentedBooks.FirstOrDefault(x => x.Id == id);
            rentedBook.User = _dbContext.Users.Find(rentedBook.UserId);
            rentedBook.Book= _dbContext.Books.Find(rentedBook.BookId);
            return Ok(rentedBook);
        }

        [HttpPost("createRenteBook")]
        public IActionResult CreateRentedBook(RentedBook model)
        {
            _dbContext.RentedBooks.Add(model);
            _dbContext.SaveChanges();
            return Ok("Kitap Kiralandı.");
        }
    }
}
