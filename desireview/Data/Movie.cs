using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace desireview.Data
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Title  { get; set; }
        public string Cast { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public decimal DesiReviewRating { get; set; }
        public decimal AverageRating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public string MovieLanguage { get; set; }

        public ICollection<Review> Reviews { get; set; }

        [NotMapped]
        public decimal UserRating { get; set; }
    }
}
