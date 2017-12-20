namespace DatabaseTesting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DatabaseWorkerUniteTests
    {
        private const string DATA_SOURCE = @"(localdb)\MSSQLLocalDB";

        private static UserContext _userContext;

        private static UserContext UserContext
        {
            get { if (_userContext != null)
                    return _userContext;

                _userContext = new UserDbContextFactory().CreateDBContext(DATA_SOURCE, Guid.NewGuid().ToString());
                return _userContext;
            }
        }

        [TestMethod]
        public void AddUserUnite()
        {
            var databaseWorker = new DatabaseWorker(UserContext);

            const string username = "UserName";

            var user = new User
            {
                Name = username
            };

            databaseWorker.AddUser(user);

            Assert.IsTrue(UserContext.Users.Any(u => u.Name == username));
        }

        [TestMethod]
        public void AddUsersUnite()
        {
            var databaseWorker = new DatabaseWorker(UserContext);

            const int usersCount = 100;

            var users = new List<User>();

            for (var i = 0; i < usersCount; i++)
                users.Add(new User
                {
                    Name = Guid.NewGuid().ToString()
                });

            databaseWorker.AddUsers(users);

            var databaseUsers = UserContext.Users.ToList();

            Assert.IsTrue(users.All(user => databaseUsers.Contains(user)));
        }

        [TestMethod]
        public void GetUsersUnite()
        {
            var databaseWorker = new DatabaseWorker(UserContext);

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
        }
    }
}