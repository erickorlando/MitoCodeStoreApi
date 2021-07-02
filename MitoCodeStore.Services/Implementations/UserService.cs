using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MitoCodeStore.DataAccess;
using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace MitoCodeStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<MitoCodeUserIdentity> _userManager;
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<AppSettings> _logger;
        private readonly ICustomerRepository _customerRepository;

        public UserService(UserManager<MitoCodeUserIdentity> userManager,
            IOptions<AppSettings> options,
            ILogger<AppSettings> logger, 
            ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _options = options;
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<RegisterUserDtoResponse> RegisterAsync(RegisterUserDtoRequest request)
        {
            var response = new RegisterUserDtoResponse();

            try
            {
                var result = await _userManager.CreateAsync(new MitoCodeUserIdentity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = Convert.ToDateTime(request.BirthDate),
                    Email = request.Email,
                    UserName = request.UserCode
                }, request.Password);

                if (!result.Succeeded)
                {
                    response.ValidationErrors = result.Errors
                        .Select(p => p.Description)
                        .ToList();
                }
                else
                {
                    var userIdentity = await _userManager.FindByNameAsync(request.UserCode);
                    if (userIdentity != null)
                    {
                        response.UserId = userIdentity.Id;

                        // Creamos el cliente.
                        await _customerRepository.CreateAsync(new Customer
                        {
                            Name = $"{request.FirstName} {request.LastName}",
                            BirthDate = DateTime.Today.AddYears(-18),
                            NumberId = "88888888",
                            Email = userIdentity.Email
                        });
                    }
                }

                response.Success = result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();

            var identity = await _userManager.FindByNameAsync(request.Username);

            if (identity == null)
            {
                response.Success = false;
                response.ErrorMessage = "El usuario no existe!";
                return response;
            }

            if (!await _userManager.CheckPasswordAsync(identity, request.Password))
            {
                response.ErrorMessage = "Clave incorrecta";
                return response;
            }

            var expiredDate = DateTime.Now.AddDays(1);

            response.ExpiredDate = expiredDate;
            response.FullName = $"{identity.FirstName} {identity.LastName}";
            response.UserCode = identity.UserName;
            response.UserId = identity.Id;

            if (response.UserCode == "erickorlando")
            {
                response.Modules = new List<ModuleDtoResponse>
                {
                    new ModuleDtoResponse {Name = "Clientes", Url = "/customers"},
                    new ModuleDtoResponse {Name = "Productos", Url = "/products"},
                    new ModuleDtoResponse {Name = "Categorías", Url = "/categories"},
                    new ModuleDtoResponse {Name = "Ventas", Url = "/sales"},
                    new ModuleDtoResponse {Name = "Consultas", Url = "/queries"},
                    new ModuleDtoResponse {Name = "Reportes", Url = "/reports"},
                };
            }
            else
            {
                response.Modules = new List<ModuleDtoResponse>
                {
                    new ModuleDtoResponse {Name = "Ventas", Url = "/sales"}, 
                    new ModuleDtoResponse {Name = "Reportes", Url = "/reports"},
                };
            }

            // Ubicar el registro del customer y devolver el ID como parte de los claims.
            var sid = string.Empty;
            var customer = await _customerRepository.GetItemByEmailAsync(identity.Email);
            if (customer != null)
            {
                response.CustomerId = customer.Id;
                sid = customer.Id.ToString();
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity.UserName),
                new Claim(ClaimTypes.Email, identity.Email),
                new Claim(ClaimTypes.GivenName, response.FullName),
                new Claim(ClaimTypes.Sid, sid)
            };
            
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SigningKey));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);

            var payload = new JwtPayload(
                issuer: _options.Value.Jwt.Issuer,
                audience: _options.Value.Jwt.Audience,
                claims: authClaims,
                notBefore: DateTime.Now,
                expires: expiredDate);

            var token = new JwtSecurityToken(header, payload);

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Success = true;

            return response;
        }

    }
}