using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoppingStore.API.DTO.UserDtos;
using ShoppingStore.Repository.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> usermanager,
            IConfiguration configuration,IMapper mapper)
        {
            this.usermanager = usermanager;
            this.configuration = configuration;
            this._mapper = mapper;
        }
        //Create Account new User "Registration" "Post"
        [HttpPost("register")]//api/Account/register
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var existuser = await usermanager.FindByNameAsync(userDto.UserName);
                if (existuser != null)
                {
                    return BadRequest("User Already exist with same UserName");
                }
                //save
                var user = _mapper.Map<ApplicationUser>(userDto);
                user.CreatedOn = DateTime.Now;
                IdentityResult result = await usermanager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Add Success");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
    

    [HttpPost("login")]//api/account/login
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        if (ModelState.IsValid == true)
        {
            ApplicationUser user = await usermanager.FindByNameAsync(userDto.UserName);
            if (user != null)
            {
                bool found = await usermanager.CheckPasswordAsync(user, userDto.Password);
                if (found == true)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    //get role
                    var roles = await usermanager.GetRolesAsync(user);
                    foreach (var itemRole in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, itemRole));
                    }
                    SecurityKey securityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                    SigningCredentials signincred =
                        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    //Design JWT
                    //create token  
                    JwtSecurityToken mytoken = new JwtSecurityToken(
                        issuer: configuration["JWT:ValidIssuer"],//url web api
                        audience: configuration["JWT:ValidAudiance"],//url consumer angular
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signincred
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    });
                }
            }
            return Unauthorized();
        }
        return Unauthorized();
    }
}
}
