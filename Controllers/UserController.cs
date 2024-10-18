using fitnessApp.Model;
using Microsoft.AspNetCore.Mvc;
using fitnessApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace fitnessApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public GetUserRequest GetUser(int id)
        {
            var inputUser = _context.Users.Find(id);

            if (inputUser == null)
            {
                return new GetUserRequest();
            }

            var user = new GetUserRequest
            {
                Id = inputUser.Id,
                Name = inputUser.Name,
                Joined = inputUser.Joined,
                PassCount = inputUser.PassCount,
            };

            return user;
        }

        [HttpGet]
        public GetUsersRequest GetUsers()
        {
            var users = new GetUsersRequest
            {
                Users = _context.Users.Select(x => new GetUserRequest
                {
                    Id = x.Id,
                    Name = x.Name,
                    Joined = x.Joined,
                    PassCount = x.PassCount
                }).ToList()
            };
            
            return users;
        }

        [HttpPost]
        public IActionResult AddUser(AddUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Name = model.Name,
                Joined = DateTime.Now,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User created!"); 
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(UpdateUserRequest model, int id)
        {
            var inputUser = _context.Users.Find(id);

            if (inputUser == null)
            {
                return NotFound();
            }

            if (model.Name != null)
            {
                inputUser.Name = model.Name;
            }

            if(model.PassCount != null)
            {
                inputUser.PassCount = model.PassCount.Value;
            }

            if (model.ClassId.HasValue)
            {
                var classEntity = _context.Classes.Find(model.ClassId.Value);

                if (classEntity != null)
                {
                    if (!inputUser.Classes.Contains(classEntity))
                    {
                        inputUser.Classes.Add(classEntity);
                    }
                }
            }

            _context.SaveChanges(); 
            return Ok(inputUser); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("User got deleted.");
        }
    }
}
