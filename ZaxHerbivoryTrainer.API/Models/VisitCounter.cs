using System.ComponentModel.DataAnnotations;

namespace API.ZaxHerbivoryTrainer.Models
{
    public class VisitCounter
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
