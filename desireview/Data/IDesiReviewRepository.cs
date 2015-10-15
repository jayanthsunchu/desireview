using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desireview.Data
{
    public interface IDesiReviewRepository
    {
        Review GetReviewById(int movieId);
        bool AddReview(Review review);
        IQueryable<Movie> GetMovies();

        IQueryable<Movie> GetMoviesByLanguage(string Language);

        bool IsUsernameAvailable(string userName);

        UserAccessToken RegisterNewUser(User newUser);

        UserAccessToken ValidateExistingUser(User existingUser);

        bool SendPasswordResetLink(User existinguser);

        bool UpdatePassword(PasswordUpdate newPassword);
        bool AddMovie(Movie movieToAdd);
    }
}
