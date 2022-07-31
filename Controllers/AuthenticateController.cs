using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagementAPI.AccountsService;
using ProductManagementAPI.DTOs;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;

        public AuthenticateController(ILogger<AuthenticateController> logger,
                IAccountService accountService,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModelDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Model cannot be null");

            try
            {
                var user = await _accountService.CreateAsync(model);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("Account creation failed");
            }
        }

        //[HttpPost("[Action]")]
        //public async Task<IActionResult> Login(LoginModelDto model)
        //{
        //    if (!ModelState.IsValid) return BadRequest("Invalid login model");

        //    try
        //    {
        //        var token = await _accountService.LoginAsync(model);
        //        return Ok(token);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Error occurred");
        //    }
        //}

        [HttpGet("[Action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
