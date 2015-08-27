﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desireview.Data
{
    public interface IDesiReviewRepository
    {
        IQueryable<Movie> GetMovies();

        bool IsUsernameAvailable(string userName);

        User RegisterNewUser(User newUser);

        User ValidateExistingUser(User existingUser);

        bool SendPasswordResetLink(User existinguser);

        bool UpdatePassword(PasswordUpdate newPassword);
    }
}