using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fitnessApp.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Joined { get; set; }
        public int PassCount { get; set; } = 0;

        public ICollection<Class>? Classes { get; set; } = new List<Class>();
    }

    public class GetUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Joined { get; set; }
        public int PassCount { get; set; }
    }

    public class GetUsersRequest
    {
        public List<GetUserRequest> Users { get; set; } = new List<GetUserRequest>();
    }

    public class AddUserRequest
    {
        [Required] public string Name { get; set; }
    }

    public class UpdateUserRequest
    {
        public string? Name { get; set; }
        public int? PassCount { get; set; } = 0;
        public int? ClassId { get; set; }
    }
}