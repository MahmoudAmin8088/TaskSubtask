using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskSubtask.core.Helper;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.Models;

namespace TaskSubtask.Ef.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Jwt _jwt;
        private readonly IMapper _mapper;

        public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<Jwt> jwt, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mapper = mapper;
        }

        public async Task<string> AddAdminAsync(AddRoleModel model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Ivalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User is already assigned to this role ";

            await _userManager.RemoveFromRoleAsync(user, "Employee");

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<string> DeleteAdminAsync(DeleteRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Ivalid user ID or Role";
            if (!await _userManager.IsInRoleAsync(user, model.Role))
                return "User is Not assigned to this role ";

            var result = await _userManager.RemoveFromRoleAsync(user, model.Role);

             await _userManager.AddToRoleAsync(user, "Employee");

            return result.Succeeded ? string.Empty : "Something went wrong";
        }


        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is in Correct!";
                return authModel;
            }
            var jwtSecurityToken = await CreateToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthentication = true;
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Roles = roleList.ToList();
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email Is Already Exists!"};
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName Is Already Exists!" };

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded) 
            {
                var errors = string.Empty;
                foreach(var error in result.Errors)
                {
                    errors += $"{error.Description} ,";
                }
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "Employee");
           

            var roleList= await _userManager.GetRolesAsync(user);

            //var jwtSecurityToken = await CreateToken(user);

            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                IsAuthentication=true,
                Roles= roleList.ToList(),
                // ExpiresOn = jwtSecurityToken.ValidTo,
                //Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }
        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles  = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim("userId",user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                
                issuer:_jwt.Issuer,
                audience:_jwt.Audience,
                expires:DateTime.Now.AddDays(_jwt.DurationInDays),
                claims:claims,
                signingCredentials:signingCredential
                );

            return jwtSecurityToken;
        }

    }
}
