using Datos.UnitOfWork;
using Entidades.EntityModels;
using Entidades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servicios
{
    public class UserServiceJWT : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider serviceProvider;
        private UsuariosEModel userLogued = null;
        public UserServiceJWT(IUnitOfWork unitOfWork, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            this.serviceProvider = serviceProvider;

        }

        public AutenticarRespuestaVModel Autenticar(AutenticarVModel model)
        {
            UsuariosEModel UserEntity = _unitOfWork.Repositories.UsuariosRepository.GetByEmail(model.Email);

            if (UserEntity == null || UserEntity.Contraseina != model.Password)
                return null;

            return GenerarToken(UserEntity);

        }


        public UsuariosEModel UserLogued()
        {
            string userEmail = string.Empty;

            using (IServiceScope scope = serviceProvider.CreateScope())
                userEmail = scope.ServiceProvider
                    .GetRequiredService<IHttpContextAccessor>()
                    .HttpContext.User?.Identity?.Name;


            if (userLogued == null)
                userLogued = _unitOfWork.Repositories.UsuariosRepository.GetByEmail(userEmail);

            return userLogued;
        }

        AutenticarRespuestaVModel GenerarToken(UsuariosEModel user)
        {

            var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);
            DateTime TimeSesionOnMinutes = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["TimeSesionOnMinutes"]));


            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = TimeSesionOnMinutes,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);



            return new AutenticarRespuestaVModel
            {
                Email = user.Email,
                Token = tokenHandler.WriteToken(createdToken)
            };
        }


    }
}
