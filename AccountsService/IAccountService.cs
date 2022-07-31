using ProductManagementAPI.DTOs;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.AccountsService
{
    public interface IAccountService
    {
        Task<ApplicationUser> CreateAsync(RegisterModelDto model, string role = "admin");
        //Task<string> LoginAsync(LoginModelDto model);
    }
}
