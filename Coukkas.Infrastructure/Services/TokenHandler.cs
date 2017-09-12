using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Coukkas.Infrastructure.Repositories.DTOS;
using Coukkas.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Coukkas.Core;

namespace Coukkas.Infrastructure.Services
{
    public  class TokenHandler : ITokenHandler
{

      private readonly TokenParameters _tokenParameters;

      public TokenHandler (TokenParameters tokenParameters)
      {
          _tokenParameters = tokenParameters;
      }

     public TokenDto CreateToken (Guid UserId, string role)
      {
          var now = DateTime.UtcNow;
          var claims = new List<Claim>()
          {
              new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim (JwtRegisteredClaimNames.Sub, UserId.ToString()),
              new Claim (JwtRegisteredClaimNames.UniqueName, UserId.ToString()),
              new Claim (ClaimTypes.Role, role),
              new Claim (ClaimTypes.Name, UserId.ToString()),
              new Claim (JwtRegisteredClaimNames.Iat, now.ToUnixTime().ToString()) 
          };

          var expires = now.AddMinutes(_tokenParameters.ExpiryMinutes);
          var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8
          .GetBytes(_tokenParameters.SigningKey)), SecurityAlgorithms.HmacSha256);

          string EncodedToken = 
            new  JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
             issuer: _tokenParameters.Issuer,
             claims: claims,
             expires: expires,
             signingCredentials: signingCredentials));

         return new TokenDto()
         {
             Token = EncodedToken,
             Expire = expires, 
             Role = role,

         };
      }
        
    }
}