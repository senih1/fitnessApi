using fitnessApp.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fitnessApp.Model
{
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Class>? Classes { get; set; } = new List<Class>();
    }

    public class GetTrainerRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GetTrainersRequest
    {
        public List<GetTrainerRequest> Trainers { get; set; } = new List<GetTrainerRequest>();
    }

    public class AddTrainerRequest
    {
        public string Name { get; set; }
        public int? ClassId { get; set; }
    }

    public class UpdateTrainerRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClassId { get; set; }
    }
}
