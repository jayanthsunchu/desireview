using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desireview.Data
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, Column(Order = 0)]
        public string CommentBody { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        public DateTime CommentDate { get; set; }
        public int ReviewId { get; set; }
    }
}