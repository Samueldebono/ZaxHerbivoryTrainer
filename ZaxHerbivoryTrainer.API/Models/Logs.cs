using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace ZaxHerbivoryTrainer.API.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Message { get; set; }
    }
}
