﻿using System.Threading.Tasks;
using AutoMapper;
using EConsult_T.Api.Models;
using EConsult_T.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EConsult_T.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        /// <summary>User Log In</summary>
        /// <returns>Return JWT token</returns>
        /// <remarks>Login user and generate JWT token</remarks>
        /// <param name="loginCredentials">Login model</param> 
        /// <response code="200">Log In successful</response>
        /// <response code="400">Invalid username or password</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] LoginCredentials loginCredentials)
        {
            string token = await accountService.CreateJwtTokenAsync(loginCredentials.Email, loginCredentials.Password);
            if (token == null)
                return BadRequest("Invalid username or password.");

            return Ok(new { access_token = token });
        }

        /// <summary>Register new user</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Registration new user</remarks>
        /// <param name="registrationUserCredentials">User registration model</param> 
        /// <response code="200">Registration successful</response>
        /// <response code="400">User with thes same email exist</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationUserCredentials registrationUserCredentials)
        {
            await accountService.RegisterUserAsync(mapper.Map<RegistrationUserCredentials, UserRegistrationDto>(registrationUserCredentials));

            return Ok();
        }
    }
}