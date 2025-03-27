using BooksApi.Context;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public BooksController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getAllBooks")]
        public IActionResult GetAllBooks()
        {
            var books = _dbContext.Books.ToList();
            return Ok(books);
        }

        [HttpPost("createBook")]
        public IActionResult CreateBook(Book model)
        {
            if(model.Title ==null || model.Title=="" || model.Title.IsNullOrEmpty())
            {
                return BadRequest("Başlık bilgisi boş geçilemez.");
            }
            if (model.Author == null || model.Author == "" || model.Author.IsNullOrEmpty())
            {
                return BadRequest("Yazar bilgisi boş geçilemez.");
            }
            _dbContext.Books.Add(model);
            _dbContext.SaveChanges();
            return Ok("Kitap kaydı başarıyla oluşturdu.");
        }

        [HttpGet("getByIBook")]
        public IActionResult GetBook(int id)
        {
            var book = _dbContext.Books.Where(x=> x.Id== id).FirstOrDefault();
            if (book == null)
            {
                return NotFound("Kitap Bulunamadı");
            }
            return Ok(book);
        }

        [HttpPut("updateBook")]
        public IActionResult UpdateBook(Book model)
        {
            var book = _dbContext.Books.Find(model.Id);
            if(book == null)
            {
                return NotFound("Kitap bulunamadı.");
            }
            book.Title= model.Title;    
            book.Author= model.Author;
            book.Description= model.Description;    
            book.Stock= model.Stock;
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
            return Ok("Kitap başarıyla güncellendi.");
        }

        [HttpDelete("deleteBook")]
        public IActionResult DeleteBook(int id)
        {
            var book = _dbContext.Books.Where(x=>x.Id== id).FirstOrDefault();
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok("Kayıt başarıyla güncellendi");
        }
    }
}
