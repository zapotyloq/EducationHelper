using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationHelper.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        public const string KEY = "shsshsshsshsshsshsshsshsshsshsshsshsshsshsshsshsshs";   // ключ для шифрации
        public const int LIFETIME = 120; // время жизни токена - 120 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
