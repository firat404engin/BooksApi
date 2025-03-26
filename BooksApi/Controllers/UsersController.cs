using BooksApi.Context;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public UsersController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetAllUser()
        {
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpGet("getByIdUser")]
        public IActionResult GetByIdUser(int id)
        {
            if(id == 0)
            {
                return BadRequest("Id zorunlu alandır.");
            }
            var user = _dbContext.Users.Where(x=> x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }
            return Ok(user);    
        }

        [HttpPost("createUser")]
        public IActionResult CreateUser(User model)
        {
            _dbContext.Users.Add(model);    
            _dbContext.SaveChanges();
            return Ok("Kullanıcı Oluşturuldu.");
        }
        [HttpPut("updateUser")]
        public IActionResult UpateUsers(User model)
        {
            var user = _dbContext.Users.Where(x=> x.Id == model.Id).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");

            }
            else
            {
                user.Name=model.Name;   
                user.Email=model.Email;
                user.Phone=model.Phone;
                user.Surname=model.Surname;
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();

                return Ok("Kullanıcı Güncellendi.");
            }
        }

        [HttpDelete("deleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if(user == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }
            else
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return Ok("Kullanıcı kaydı silindi!");
            }
        }
    }
}
