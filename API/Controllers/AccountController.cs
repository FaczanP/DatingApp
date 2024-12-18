using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("register")] //POST: /api/account/register
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        var user = _mapper.Map<AppUser>(registerDto);

        // using var hmac = new HMACSHA512(); section 16

            user.UserName = registerDto.Username.ToLower();
            // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            // user.PasswordSalt = hmac.Key; section 16

        // _context.Users.Add(user);
        // await _context.SaveChangesAsync(); section 16
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if(!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        if(!roleResult.Succeeded) return BadRequest(result.Errors);

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender,

        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.Include(x => x.Photos)
        .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        
        if (user == null) return Unauthorized("Invalid username");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if(!result) return Unauthorized("Invalid password"); 

        // using var hmac = new HMACSHA512(user.PasswordSalt);

        // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        // for (int i = 0; i < computedHash.Length; i++)   
        // {
        //     if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password !");
        // } section 16

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender,

        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }


}
