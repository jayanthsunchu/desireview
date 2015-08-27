using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desireview.Data
{
    public class DesiReviewContext: DbContext
    {

        public DesiReviewContext() : base("DefaultConnection") {
             
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserPasswordRequest> UserPasswordRequests { get; set; }
    }
}
