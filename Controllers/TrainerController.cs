using fitnessApp.Model;
using Microsoft.AspNetCore.Mvc;
using fitnessApp.Data;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace fitnessApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TrainerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TrainerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public GetTrainerRequest GetTrainer(int id)
        {
            var inputTrainer = _context.Trainers.Find(id);

            if (inputTrainer == null)
            {
                return new GetTrainerRequest(); 
            }

            var trainer = new GetTrainerRequest
            {
                Id = inputTrainer.Id,
                Name = inputTrainer.Name
            };

            return trainer;
        }

        [HttpGet]
        public GetTrainersRequest GetTrainers()
        {
            var trainers = new GetTrainersRequest
            {
                Trainers = _context.Trainers.Select(t => new GetTrainerRequest
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };
            return trainers;
        }

        [HttpPost]
        public AddTrainerRequest AddTrainer(AddTrainerRequest model)
        {
            if (ModelState.IsValid)
            {
                var trainer = new Trainer();
                trainer.Name = model.Name;

                if (model.ClassId != 0)
                {
                    var classEntity = _context.Classes.Find(model.ClassId);

                    if (classEntity != null)
                    {
                        trainer.Classes.Add(classEntity);
                    }
                }

                _context.Trainers.Add(trainer);
                _context.SaveChanges();

                return model;
            }

            return model;
        }

        [HttpPatch]
        public UpdateTrainerRequest UpdateTrainer(UpdateTrainerRequest model)
        {
            var trainer = _context.Trainers.Find(model.Id);

            if (trainer != null && ModelState.IsValid)
            {
                trainer.Name = model.Name;

                if (model.ClassId != null)
                {
                    var classEntity = _context.Classes.Find(model.ClassId);

                    if (classEntity != null && !trainer.Classes.Contains(classEntity))
                    {
                        trainer.Classes.Add(classEntity);
                    }
                }
                _context.SaveChanges();
            }
            return model;
        }

        [HttpDelete("{id}")]
        public string DeleteTrainer(int id)
        {
            var trainer = _context.Trainers.Find(id);

            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                _context.SaveChanges();

                return $"Trainer {id} Deleted!";
            }

            return $"There is no trainer found by id = {id}!";
        }
    }
}
