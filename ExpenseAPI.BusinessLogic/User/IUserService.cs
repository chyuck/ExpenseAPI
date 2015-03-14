using System;
using System.Threading.Tasks;
using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    /// <summary>User service</summary>
    public interface IUserService
    {
        /// <summary>Logs in user</summary>
        /// <returns>Object that must be disposed on log out</returns>
        IDisposable LogIn(string name);

        /// <summary>Returns whether user is logged in</summary>
        bool IsLoggedIn { get; }

        /// <summary>Returns user ID</summary>
        int UserId { get; }

        /// <summary>Returns user name</summary>
        string UserName { get; }

        /// <summary>Returns whether user exists</summary>
        bool DoesUserExist(string name);

        /// <summary>Creates user</summary>
        Task CreateUserAsync(string name);

        /// <summary>Returns all users</summary>
        Task<UserGet[]> GetUsersAsync();
    }
}
