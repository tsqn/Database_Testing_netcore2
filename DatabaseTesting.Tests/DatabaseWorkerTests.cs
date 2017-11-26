namespace DatabaseTesting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DatabaseWorkerTests
    {
        private const string DATA_SOURCE = @"(localdb)\MSSQLLocalDB";

        [TestMethod]
        public void AddUser()
        {
            using (var context = new UserDbContextFactory().CreateDBContext(DATA_SOURCE, Guid.NewGuid().ToString()))
            {
                var databaseWorker = new DatabaseWorker(context);

                const string username = "UserName";
                var user = new User
                {
                    Name = username
                };

                databaseWorker.AddUser(user);

                Assert.IsTrue(context.Users.Any(u => u.Name == username));

                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void AddUsers()
        {
            using (var context = new UserDbContextFactory().CreateDBContext(DATA_SOURCE, Guid.NewGuid().ToString()))
            {
                var databaseWorker = new DatabaseWorker(context);

                const int usersCount = 100;

                var users = new List<User>();

                for (var i = 0; i < usersCount; i++)
                    users.Add(new User
                    {
                        Name = Guid.NewGuid().ToString()
                    });

                databaseWorker.AddUsers(users);

                Assert.AreEqual(context.Users.Count(), usersCount);

                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void GetUsers()
        {
            using (var context = new UserDbContextFactory().CreateDBContext(DATA_SOURCE, Guid.NewGuid().ToString()))
            {
                var databaseWorker = new DatabaseWorker(context);

                const string username = "UserName";
                var user = new User
                {
                    Name = username
                };
                databaseWorker.AddUser(user);


                var users = new List<User>();
                for (var i = 0; i < 5; i++)
                    users.Add(new User {Name = $"{username}{i}"});

                databaseWorker.AddUsers(users);

                Assert.AreEqual(user.Name, databaseWorker.GetUsers(username).First().Name);

                context.Database.EnsureDeleted();
            }
        }
    }
}