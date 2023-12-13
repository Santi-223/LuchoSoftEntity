using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutenticarconToken.Models;
using AutenticarconToken.Models.Customs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace AutenticarconToken.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly DbpruebaContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(DbpruebaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");

            var KeyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
            new SymmetricSecurityKey(KeyBytes),

            SecurityAlgorithms.HmacSha256
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,

                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);
            return tokenCreado;
        }

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {

            var EmailUsuario = "";
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
                x.email == autorizacion.email &&
                x.contraseña == autorizacion.contraseña
                );
            if(usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }
            string tokenCreado = GenerarToken(usuario_encontrado.id_usuario.ToString());

            EmailUsuario = autorizacion.email;

            return new AutorizacionResponse() { Token =  tokenCreado,Resultado=true, Mensaje="Ok", EmailUsuario=EmailUsuario};
        }
    }
}
