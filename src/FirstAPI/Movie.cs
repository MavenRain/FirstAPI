using System;
using System.ComponentModel.DataAnnotations;

namespace FirstAPI
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsDone { get; set; }

    }
}