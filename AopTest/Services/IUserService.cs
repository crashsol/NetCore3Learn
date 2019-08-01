using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AopTest.Models;
using AopTest.Interceptors;
using System.ComponentModel;

namespace AopTest.Services
{
    public interface IUserService
    {
        [Description("获取用户信息")]
        [CustomInterceptor]
        Task<User> GetUserAsync(int id);


        Task<List<User>> GetUsersAsync();
    }
}
