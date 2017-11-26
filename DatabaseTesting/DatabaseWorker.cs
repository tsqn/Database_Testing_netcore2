namespace DatabaseTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The database worker.
    /// </summary>
    public class DatabaseWorker
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly UserContext _dbContext;

        public DatabaseWorker(UserContext userContext)
        {
            _dbContext = userContext;
            _dbContext.Database.Migrate();
        }

        /// <summary>
        /// Add user to database.
        /// </summary>
        /// <param name="user">User.</param>
        public  void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Add users to database.
        /// </summary>
        /// <param name="users">Collection of user.</param>
        public void AddUsers(List<User> users)
        {
            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Get users by name.
        /// </summary>
        /// <param name="userName">User name.</param>
        public List<User> GetUsers(string userName)
        {
            return _dbContext.Users.Where(user => user.Name == userName).ToList();
        }
    }
}