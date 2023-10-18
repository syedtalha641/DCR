using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.IRepos;

namespace Repository.Repos
{
    public class AccountRepos : IAccountRepos
    {
        private readonly EMS_ITCContext _context;
     



        private readonly IConfiguration _configuration;


        public AccountRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }


        public async Task<User> AddUser(string UserLoginId, string UserName, string UserEmail, string UserPassword)
        {
            try
            {
                // Create a new User entity
                var newUser = new User
                {
                    UserLoginId = UserLoginId,
                    UserName = UserName,
                    UserEmail = UserEmail
                };

                string password = UserPassword; // Get the user's input password
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                newUser.UserPassword = hashedPassword; // Store the hashed password in the user entity
                newUser.CreatedBy = "Admin";
                newUser.Salt = salt;
                newUser.IsActive = true;

                // Add the new user entity to the context and save changes
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();



                return newUser;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Where( x => x.IsActive == true).ToListAsync();
        }

        public async Task<User> GetUser(string UserLoginId)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.UserLoginId == UserLoginId);
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

        public async Task<User> LoginUser(string UserLoginId, string UserPassword)
        {

            var result = await _context.Users.SingleOrDefaultAsync(u => u.UserLoginId == UserLoginId);

            if (result == null)
            {
                return null; // User not found
            }



            // Retrieve the stored hashed password and salt from the database
            string storedHashedPassword = result.UserPassword;
            string storedSalt = result.Salt;



            // Verify the input password against the stored hash and salt
            if (BCrypt.Net.BCrypt.Verify(UserPassword, storedHashedPassword))
            {
                // Passwords match, login successful
                return result;
            }
            else
            {
                // Passwords don't match, login failed
                return null;
            }
        }

        public async Task<User> UpdateUserPassword(string UserLoginId, string UserPassword)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(a => a.UserLoginId == UserLoginId);
                if (result != null)
                {
                    string password = UserPassword; // Get the user's new input password
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    result.UserPassword = hashedPassword; // Store the new hashed password in the user entity

                    // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                    result.UpdatedBy = result.UserName; // Set the appropriate value
                    result.UpdatedOn = DateTime.Now; // Set the appropriate value

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return result; // Return a string indicating success
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

        public async Task<User> DeleteUser(string UserLoginId)
        {
            var result = await _context.Users.Where(a => a.UserLoginId == UserLoginId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}

