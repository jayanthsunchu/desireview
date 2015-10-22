using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace desireview.Data
{
    public interface IDesiReviewRepository
    {
        void SubmitContact(Contact contact);
        IQueryable<Contact> GetContacts();
        UserRating AddUserRating(UserRating rating);
        Review GetReviewById(int movieId);
        IEnumerable<SelectListItem> GetMovieDropdown();
        bool AddReview(Review review);
        IQueryable<Movie> GetMovies(UserAccessToken userName);

        IQueryable<Movie> GetMoviesByLanguage(string Language);

        bool IsUsernameAvailable(string userName);

        UserAccessToken RegisterNewUser(User newUser);

        UserAccessToken ValidateExistingUser(User existingUser);

        bool SendPasswordResetLink(User existinguser);

        bool UpdatePassword(PasswordUpdate newPassword);
        bool AddMovie(Movie movieToAdd);
    }
}
