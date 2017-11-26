namespace DatabaseTesting
{
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    /// <summary>
    /// The users database context.
    /// </summary>
    public class UserContext : DbContext
    {
        /// <inheritdoc />
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        /// <summary>
        /// Users.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
