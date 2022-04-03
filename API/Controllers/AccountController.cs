using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;

        public AccountController(UserManager<AppUser> userManager,
            ITokenServices tokenServices,
            SignInManager<AppUser> signinManager,
            IMapper mapper)
        {
            _tokenServices = tokenServices;
            _signinManager = signinManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.GenerateToken(user)
            });
        }

        [HttpGet("EmailExist")]
        public async Task<bool> EmailExist([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            return Ok(_mapper.Map<Address,AddressDto>(user.Address));
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            user.Address = _mapper.Map<AddressDto,Address>(address);
            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded) return Ok(_mapper.Map<Address,AddressDto>(user.Address));

            return BadRequest("خطا در ویرایش ");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
            {
                return Ok(new UserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = _tokenServices.GenerateToken(user)
                });
            };

            return Unauthorized(new ApiResponse(401));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.Displayname,
                UserName = registerDto.Username,
                Email = registerDto.Username
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.GenerateToken(user)
            });
        }
    }
}