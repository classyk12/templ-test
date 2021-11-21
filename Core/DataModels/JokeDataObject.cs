using System;
namespace tmplltest.Core.DataModels
{
    public class JokeDataObject
    {
        public string Joke { get; set; }
        public string CreatedBy { get; set; } = "Admin";
    }

    public class BaseObject
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }

    public class JokeDto : BaseObject
    {
        public string Joke { get; set; }
        public string CreatedBy { get; set; }
    }
}


