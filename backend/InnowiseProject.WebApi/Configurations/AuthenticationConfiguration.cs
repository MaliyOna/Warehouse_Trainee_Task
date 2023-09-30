using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnowiseProject.WebApi.Configurations
{
    public class AuthenticationConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string EncryptionKey { get; set; }

        public SecurityKey GetSecurityKey() =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EncryptionKey));
    }
}
