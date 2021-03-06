﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DIContainer;
using DIContainer.Attributes;
using ExpenseAPI.DataAccess;
using ExpenseAPI.Models;
using Validator.Helpers;

namespace ExpenseAPI.BusinessLogic
{
    internal class UserService : BaseBusinessLogicService, IUserService, IDisposable
    {
        #region Fields

        private User _user;
        private readonly object _syncRoot = new object();

        #endregion


        #region Constructor

        [Inject]
        public UserService(IReadOnlyContainer container) 
            : base(container)
        {
        }
        
        #endregion


        #region Properties

        public bool IsLoggedIn
        {
            get { return _user != null; }
        }

        public int UserId
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_user == null)
                        throw new UserServiceException("User is not logged in.");

                    return _user.UserId;
                }
            }
        }

        public string UserName
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_user == null)
                        throw new UserServiceException("User is not logged in.");

                    return _user.Name;
                }
            }
        }
        
        #endregion


        #region Methods

        private static void ValidateUserName(string name)
        {
            if (!StringValidator.ValidateString(name, 1, 50, false, false))
                throw new ArgumentException("User name must have 1-50 characters.", "name");
        }

        public IDisposable LogIn(string name, bool createUserNotExist)
        {
            ValidateUserName(name);

            lock (_syncRoot)
            {
                if (_user != null)
                    throw new UserServiceException("User is already logged in.");

                using (var persistence = Container.Get<IPersistenceService>())
                {
                    var user = persistence.GetEntitySet<User>().SingleOrDefault(u => u.Name == name);
                    if (user == null)
                    {
                        if (!createUserNotExist)
                            throw new UserServiceException("User '{0}' does not exist.", name);

                        user =
                            new User
                            {
                                Name = name,
                                CreateDate = Time.UtcNow
                            };

                        persistence.GetEntitySet<User>().Add(user);
                        persistence.SaveChanges();
                    }
                    
                    _user = user;
                }

                return this;
            }
        }

        public bool DoesUserExist(string name)
        {
            ValidateUserName(name);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                return persistence.GetEntitySet<User>().Any(u => u.Name == name);
            }
        }

        public async Task CreateUserAsync(string name)
        {
            ValidateUserName(name);

            var utcNow = Time.UtcNow;

            await RethrowUniqueKeyExceptionAsync(
                "UK_User",
                () => new UserServiceException("User '{0}' already exists.", name),
                async () =>
                {
                    using (var persistence = Container.Get<IPersistenceService>())
                    {
                        var user = 
                            new User
                            {
                                Name = name,
                                CreateDate = utcNow
                            };

                        persistence.GetEntitySet<User>().Add(user);

                        await persistence.SaveChangesAsync();
                    }
                });
        }
        
        public void Dispose()
        {
            lock (_syncRoot)
            {
                _user = null;
            }
        }
        
        public async Task<UserGet[]> GetUsersAsync()
        {
            using (var persistence = Container.Get<IPersistenceService>())
            {
                return await 
                    persistence.GetEntitySet<User>()
                        .OrderBy(u => u.Name)
                        .Select(u => new UserGet { Name = u.Name })
                        .ToArrayAsync();
            }
        }

        #endregion
    }
}
