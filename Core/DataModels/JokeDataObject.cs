using System;
using System.ComponentModel.DataAnnotations;

namespace tmplltest.Core.DataModels
{
    public class JokeDataObject : BaseObject
    {
        public string Joke { get; set; }
        public string CreatedBy { get; set; } = "Admin";
    }

    public class BaseObject
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }

    public class JokeDto
    {
        public string Joke { get; set; }
        public string CreatedBy { get; set; }
    }
}


