using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AopTest.Data;
using AopTest.Models;
using AspectCore.Injector;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AopTest.Services
{
    public class UserService : IUserService
    {

        private AppDbContext AppDbContext;

        private ILogger<UserService> _logger;

        public UserService(AppDbContext appDbContext,ILogger<UserService> logger)
        {
            AppDbContext = appDbContext;
            _logger = logger;
        }


        public async Task<User> GetUserAsync(int id)
        {
            _logger.LogWarning("Before Excute GetUserAsync");
            return await AppDbContext.Users.FindAsync(id);
          
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await AppDbContext.Users.ToListAsync();

        }
    }
}
