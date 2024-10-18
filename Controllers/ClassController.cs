using fitnessApp.Model;
using Microsoft.AspNetCore.Mvc;
using fitnessApp.Data;

namespace fitnessApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClassController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClassController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Class> GetClasses()
        {
            var classes = _context.Classes.ToList();
            return classes;
        }

        [HttpGet("{id}")]
        public Class GetClass(int id)
        {
            var inputClass = _context.Classes.Find(id);
            return inputClass;
        }

        [HttpPost]
        public AddClassRequest AddClass(AddClassRequest model)
        {
            if(model == null)
            {
                return new AddClassRequest();
            }

            var classModel = new Class
            {
                Name = model.Name,
            };

            _context.Classes.Add(classModel);
            _context.SaveChanges();

            return model;
        }
    }
}
