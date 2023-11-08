﻿using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class AccountRepos : IAccountRepos
    {
        private readonly EMS_ITCContext _context;

        public AccountRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<LoginViewModel> AddUser(LoginViewModel model)
        {
            try
            {
                // Create a new User entity
                var newUser = new User
                {
                    UserLoginId = model.UserLoginId,
                    UserName = model.UserName,
                    UserEmail = model.UserEmail
                };

                if (model.UserPassword == model.ConfirmPassword)
                {
                    string password = model.UserPassword; // Get the user's input password
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    newUser.UserPassword = hashedPassword; // Store the hashed password in the user entity
                    newUser.CreatedBy = "Admin";
                    newUser.Salt = salt;
                    newUser.IsActive = true;

                    // Add the new user entity to the context and save changes
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<LoginViewModel>> GetUsers()
        {
            var result = await _context.Users.Where(x => x.IsActive == true).Select(x => new LoginViewModel
                {
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    UserLoginId = x.UserLoginId,
                    UserPassword = x.UserPassword,
                    UserContact = x.UserContact
                })
                .ToListAsync();

            if (result != null && result.Any())
            {
                return result;
            }
            else
            {
                // Handle the case where no active branches are found
                return new List<LoginViewModel>(); // Returning an empty list
            }

        }

        public async Task<LoginViewModel> GetUser(string UserLoginId)
        {
            var result = await _context.Users.FirstOrDefaultAsync(a => a.UserLoginId == UserLoginId && a.IsActive == true);

            if (result != null)
            {
                var loginviewmodel = new LoginViewModel
                {
                    UserName = result.UserName,
                    UserEmail = result.UserEmail,
                    UserLoginId = result.UserLoginId,
                    UserPassword = result.UserPassword,
                    UserContact = result.UserContact
                };
                return loginviewmodel;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetUserEmail(string UserLoginId)
        {
            var result = await _context.Users.FirstOrDefaultAsync(a => a.UserLoginId == UserLoginId);
            if (result == null)
            {
                return null;
            }

            return result.UserEmail;
        }
        public async Task<string> GetUserPhoneNumber(string UserLoginId)
        {
            var result = await _context.Users.FirstOrDefaultAsync(a => a.UserLoginId == UserLoginId);
            if (result == null)
            {
                return null;
            }

            return result.UserContact;
        }

        public async Task<bool> LoginUser(PasswordUpdateViewModel model)
        {

            var result = await _context.Users.SingleOrDefaultAsync(u => u.UserLoginId == model.UserLoginId);

            if (result == null)
            {
                return false; // User not found
            }



            // Retrieve the stored hashed password and salt from the database
            string storedHashedPassword = result.UserPassword;
            string storedSalt = result.Salt;



            // Verify the input password against the stored hash and salt
            if (BCrypt.Net.BCrypt.Verify(model.UserPassword, storedHashedPassword))
            {
                // Passwords match, login successful
                return true;
            }
            else
            {
                // Passwords don't match, login failed
                return false;
            }
        }

        public async Task<PasswordUpdateViewModel> UpdateUserPassword(PasswordUpdateViewModel model)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(a => a.UserLoginId == model.UserLoginId);
                if (result != null)
                {
                    string password = model.UserPassword; // Get the user's new input password
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    result.UserPassword = hashedPassword; // Store the new hashed password in the user entity

                    // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                    result.UpdatedBy = result.UserName; // Set the appropriate value
                    result.UpdatedOn = DateTime.Now; // Set the appropriate value

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return model; // Return a string indicating success
                }
                else
                {
                    // User not found
                    return null;
                }
            }
            catch (Exception)
            {
                // Handle any exceptions, such as database errors
                return null; // Return an error message
            }
        }

        public async Task<bool> DeleteUser(string UserLoginId)
        {
            var result = await _context.Users.Where(a => a.UserLoginId == UserLoginId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

