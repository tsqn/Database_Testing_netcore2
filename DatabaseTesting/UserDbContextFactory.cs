namespace DatabaseTesting
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// Database context factory.
    /// </summary>
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        /// <summary>
        /// Default database source.
        /// </summary>
        private const string DEFAULT_DATA_SOURCE = @"(localdb)\MSSQLLocalDB";

        /// <summary>
        /// Default database name.
        /// </summary>
        private const string DEFAULT_DATABASE_NAME = "DefaultDatabaseName";

        /// <summary>
        /// Returns the database context.
        /// </summary>
        /// <remarks>
        /// Creates new database if does not exits. 
        /// </remarks>
        public UserContext CreateDbContext(string[] args)
        {
            return CreateDBContext(DEFAULT_DATA_SOURCE, DEFAULT_DATABASE_NAME);
        }

        /// <summary>
        /// Returns the database context.
        /// </summary>
        /// <param name="dataSource">Data Source.</param>
        /// <param name="databaseName">Database Name</param>
        /// <remarks>
        /// Creates new database if does not exits. 
        /// </remarks>
        public UserContext CreateDBContext(string dataSource, string databaseName)
        {
            var builder = new DbContextOptionsBuilder<UserContext>();

            var connectionString = $@"Data Source={dataSource};Database={databaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            builder.UseSqlServer(connectionString);
            return new UserContext(builder.Options);
        }
    }
}