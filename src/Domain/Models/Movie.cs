using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public string PosterUrl { get; set; }
        public string Type { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
