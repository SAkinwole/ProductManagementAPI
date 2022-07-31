using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagementAPI.DataContext;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleManagementController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleManagementController> _logger;

        public RoleManagementController(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleManagementController> logger
        )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        [Route("CreateNewRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (!roleExist) // checks if the role exist 
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));
                // We need to check if the role has been added successfully
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {name} has been added successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been added successfully"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role {name} has not been added");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been added"
                    });
                }

            }

            return BadRequest(new { error = "Role already exist" });
        }

        [HttpPost]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string name)
        {
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (roleExist) // checks on the role exist status
            {
                var roleResult = await _roleManager.DeleteAsync(new IdentityRole(name));
                // We need to check if the role has been deleted successfully
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {name} has been deleted successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been deleted successfully"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role {name} has not been deleted");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been deleted"
                    });
                }

            }

            return BadRequest(new { error = "Role does not exist" });
        }
    }
}
