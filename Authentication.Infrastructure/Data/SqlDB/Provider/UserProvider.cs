using Authentication.Domain.Model;
using Authentication.Infrastructure.Data.SqlDB.Entities;
using Authentication.Infrastructure.Data.SqlDB.UnitOfWork;
using Authentication.Service.Interface;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Infrastructure.Data.SqlDB.Provider
{
    public class UserProvider : IUserProvider
    {
        public readonly IUnitOfWork _uowAuthentication;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public UserProvider(IUnitOfWork uowAuthentication, IMapper mapper, IMemoryCache memoryCache)
        {
            _uowAuthentication = uowAuthentication;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public const string Key = "AutheticationSystem1234567890";
        public static byte[] keySecret = Encoding.UTF8.GetBytes(Key);

        /// <summary>
        /// Encripta la cadena del aparámetro
        /// </summary>
        /// <param name="key">
        /// parametro que se va a encriptar
        /// </param>
        /// <returns>
        /// Cadena encriptada
        /// </returns>
        public static string EncryptPassword(string key)
        {
            byte[] secretPassword = Encoding.UTF8.GetBytes("AutheticationSystem");
            byte[] stringBytes = Encoding.UTF8.GetBytes(key);

            HMACMD5 _md5 = new HMACMD5(secretPassword);
            byte[] encrypt = _md5.ComputeHash(stringBytes);
            string encryptedPassword = ByteToString(encrypt);

            return encryptedPassword;
        }

        /// <summary>
        /// Convierte un arreglo de bytes a una cadena 
        /// </summary>
        /// <param name="vecByte">
        /// arreglo de bytes a convertir
        /// </param>
        /// <returns>
        /// Arreglo convertido en string
        /// </returns>
        private static string ByteToString(byte[] vecByte)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < vecByte.Length; i++)
            {
                sb.AppendFormat("{0:x}", vecByte[i]);
            }
            return sb.ToString();

        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                IEnumerable<UserEntity> ListuserEntity = await _uowAuthentication.UserRepository.GetAsync(c => c.EstadoUsuario == true && c.NombreUsuario == user.UserName && c.CorreoUsuario == user.UserEmail);
                UserEntity userEntity = ListuserEntity.FirstOrDefault();

                if (userEntity == null)
                {
                    var claveUsuario = EncryptPassword(user.UserPassword);
                    user.UserPassword = claveUsuario;
                    UserEntity add;
                    add = _mapper.Map<UserEntity>(user);
                    _uowAuthentication.UserRepository.Insert(add);
                    await _uowAuthentication.UserRepository.CommitAsync();
                }
                else
                {
                    throw new Exception("User already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User already exists", ex);
            }
            return user;
        }
    }
}
