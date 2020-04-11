﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkerCRM.Data;
using WorkerCRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkerCRM.Common;
using WorkerCRM.Services.Dto;
using WorkerCRM.Services.Contract;
using WorkerCRM.Services.Contract.Dto;
using WorkerCRM.Data.Contract.Repositories;
using WorkerCRM.Data.Repositories;

namespace WorkerCRM.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        
        IAuthorizationRepository _repo;
       
        public AuthorizationService(IAuthorizationRepository repo)
        {
            _repo = repo;
      
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = _repo.GetUserByIndentity(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                    {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                    };
                return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            }
            return null;

        }
        public bool IsCorrectUser(ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity != null;
        }
    
        public ResponceToken SetToken(ClaimsIdentity claimsIdentity)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
           return new ResponceToken { AccessToken = encodedJwt, UserName = claimsIdentity.Name };
        }

        public ResponceToken GetToken(string username, string password)
        {
            ClaimsIdentity claimsIdentity= GetIdentity(username, password);
            if (IsCorrectUser(claimsIdentity))
                return SetToken(claimsIdentity);
            return null;
        }

       
    }
}
