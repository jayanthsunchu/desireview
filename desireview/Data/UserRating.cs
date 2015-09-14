using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desireview.Data
{
    public class UserRating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }

        public decimal Rating { get; set; }

        public string VideoReviewUrl { get; set; }

        public string VideoReviewLikeCount { get; set; }

        public string VideoReviewDislikeCount { get; set; }

        public string VideoReviewThumb { get; set; }



    }
}
