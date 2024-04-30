using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Authentication.Infrastructure.Data.SqlDB.UnitOfWork;
using Authentication.Service.Interface;
using Authentication.Domain.AppSettings;
using Authentication.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Infrastructure.Exceptions;
using Authentication.Infrastructure.Data.SqlDB.Entities;
using Microsoft.Extensions.Configuration;

namespace Authentication.Infrastructure.Data.SqlDB.Provider
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public readonly IUnitOfWork _uowAuthentication;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public AuthenticationProvider(IUnitOfWork uowAuthentication, IMapper mapper, IMemoryCache memoryCache)
        {
            _uowAuthentication = uowAuthentication;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<LoginResponse> Login(AuthenticationRequest userRequest)
        {
            try
            {
                ValidateRequestAuthenticate(userRequest);
                
                var userKey = UserProvider.EncryptPassword(userRequest.UserPassword);
                IEnumerable<UserEntity> ListuserEntity = await _uowAuthentication.UserRepository.GetAsync(u => u.IdentificacionUsuario == userRequest.User && u.ContrasenaUsuario == userKey);
                UserEntity userEntity = ListuserEntity.FirstOrDefault();
                User user;
                user = _mapper.Map<User>(userEntity);

                if (userEntity == null)
                {
                    throw new InvalidUserException();
                }

                var response = await GenerateJWTToken(user, userRequest);

                return response;
            }

            catch (InvalidUserException ex)
            {
                throw;
            }
            catch (SecurityRequestException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ValidateRequestAuthenticate(AuthenticationRequest userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.User) || string.IsNullOrWhiteSpace(userRequest.UserPassword))
            {
                throw new InvalidUserException();
            }

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            string secret = configuration["SecretSettings:Secret"];

            if (userRequest.Secret != secret)
            {
                throw new SecurityRequestException();
            }
        }


        private async Task<LoginResponse> GenerateJWTToken(User userInfo, AuthenticationRequest? userRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userRequest.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserIdentification ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, userInfo.UserEmail ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserIdentification.ToString()),
                new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
            };
  
            var token = new JwtSecurityToken(
                issuer: "MyAplication",
                audience: "Users",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );
            LoginResponse loginResponse = new LoginResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = DateTime.Now.AddMinutes(5)
            };
            return loginResponse;
        }
    }
}
