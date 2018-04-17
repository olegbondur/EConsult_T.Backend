﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EConsult_T.Api.Models;
using EConsult_T.DAL.Entities;
using EConsult_T.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EConsult_T.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AuthOptions authOptions;
        private readonly IMapper mapper;

        public AccountService(
            IUnitOfWork unitOfWork,
            IOptions<AuthOptions> globalOptions,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.authOptions = globalOptions.Value;
            this.mapper = mapper;
        }

        public async Task<string> CreateJwtTokenAsync(string email, string password)
        {
            var claimsIdentity = await GetIdentity(email, password);
            if (claimsIdentity == null)
                return null;

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            using (unitOfWork)
            {
                var userRepository = unitOfWork.UserRepository;
                var user = mapper.Map<UserRegistrationDto, User>(userRegistrationDto);
                try
                {
                    userRepository.Add(user);
                    await unitOfWork.SaveAsync();
                }
                catch (DbUpdateException)
                {

                }
            }
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            using (unitOfWork)
            {
                var userRepository = unitOfWork.UserRepository;

                var user = await userRepository.FindAsync(x => x.Email == email && x.Password == password);
                if (user == null)
                    return null;

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType
                );

                return claimsIdentity;
            }
        }
    }
}
