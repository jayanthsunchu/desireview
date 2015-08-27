using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desireview.Data
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; }
        public int UserId { get; set; }
        public DateTime ReviewedDate { get; set; }
        
        public decimal ReviewRating { get; set; }
        public int MovieId { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}