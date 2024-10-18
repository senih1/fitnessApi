using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitnessApp.Model
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int? TrainerId { get; set; }
        public Trainer? Trainer { get; set; }

        public ICollection<User>? Users { get; set; } = new List<User>();
    }

    public class AddClassRequest
    {
        public string Name { get; set; }
    }
}
