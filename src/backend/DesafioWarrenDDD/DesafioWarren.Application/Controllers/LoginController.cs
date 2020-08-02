using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DesafioWarren.Data.Identity;
using DesafioWarren.Model.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DesafioWarren.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<LoginResponseDto> Authenticate(
            [FromBody] LoginRequestDto usuario,
            [FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            var userIdentity = await userManager.FindByNameAsync(usuario.UserName);
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.UserName))
            {
                if (userIdentity != null)
                {
                    var resultadoLogin = await signInManager.CheckPasswordSignInAsync(userIdentity, usuario.Password, false);
                    if (resultadoLogin.Succeeded)
                    {
                        credenciaisValidas = await userManager.IsInRoleAsync(userIdentity, Roles.CLIENT_ROLE);
                    }
                }
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.UserName, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new LoginResponseDto()
                {
                    Authenticated = true,
                    Created = dataCriacao,
                    Expiration = dataExpiracao,
                    AccessToken = token,
                    Message = "OK",
                    ClientId = userIdentity.ClientId
                };
            }
            else
            {
                return new LoginResponseDto()
                {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                };
            }
        }
    }
}
