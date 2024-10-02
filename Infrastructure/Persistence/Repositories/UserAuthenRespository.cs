using Application.Interfaces.Repositories;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTO.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence.Repositories;

public class UserAuthenRespository : IUserAuthenRespository
{
    private readonly IEmployeeDbContext _context;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserAuthenRespository(IEmployeeDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _config = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetAdminAuthenResponse?> LoginAccessTokenAsync(string _user, string _password)
    {
        var data = await (from a in _context.Users
                          where a.AdminEmail == _user
                          && a.AdminPass == _password
                          select new GetAdminAuthenResponse
                          {
                              Username = a.Fname,
                              Lastname = a.Lname,
                              admin_email = a.AdminEmail
                          }).FirstOrDefaultAsync();
        if (data != null)
        {
            data.Token = GenerateToken(data.Username, data.Lastname, data.admin_email);
        }
        return data;
    }

    public GetAdminAuthenResponse VerifyToken(string HttpToken)
    {

        if (!string.IsNullOrEmpty(HttpToken))
        {
            var token = new JwtSecurityTokenHandler().ReadToken(HttpToken) as JwtSecurityToken;
            DateTime Expired = token.ValidTo;
            if (Expired > DateTime.Now)
            {
                string? Name = token.Claims.FirstOrDefault(claim => claim.Type == "Username")?.Value;
                string? Lastname = token.Claims.FirstOrDefault(claim => claim.Type == "Lastname")?.Value;
                string? admin_email = token.Claims.FirstOrDefault(claim => claim.Type == "AdminEmail")?.Value;

                GetAdminAuthenResponse response = new GetAdminAuthenResponse();
                response.Username = Name?.Trim();
                response.Lastname = Lastname?.Trim();
                response.admin_email = admin_email?.Trim();
                response.Token = HttpToken?.Trim();
                return response;
            }
            throw new UnauthorizedAccessException("Token Expire Please Login Again");
        }
        throw new UnauthorizedAccessException("401 Unauthorize");
    }

    public GetUserAuthenResponseDTO RefreshToken(HttpContext HttpToken)
    {

        //HttpContext httpContext = _httpContextAccessor.HttpContext;

        GetUserAuthenResponseDTO response = new GetUserAuthenResponseDTO();
        if (HttpToken.User != null && HttpToken.User.HasClaim(claim => claim.Type == "UserID"))
        {
            string Token = BuildToken(HttpToken.User.Claims.ToArray());
            response.UserId = HttpToken.User.Claims.FirstOrDefault(claim => claim.Type == "UserID")?.Value;
            response.UserName = HttpToken.User.Claims.FirstOrDefault(claim => claim.Type == "Username")?.Value;
            response.Actor = HttpToken.User.Claims.FirstOrDefault(claim => claim.Type == "Actor")?.Value;
            response.Group = HttpToken.User.Claims.FirstOrDefault(claim => claim.Type == "Group")?.Value;
            response.Token = Token;
        }
        return response;
    }

    private string GenerateToken(string _userName, string _lastname, string _email)
    {
        var claims = new[]{
            new Claim("Username", _userName),
            new Claim("Lastname", _lastname),
            new Claim("Email", _email)
        };
        return BuildToken(claims);
    }
    private string BuildToken(Claim[] claims)
    {
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["JwtConfig:Expire"]));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:ApiSecret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtConfig:Issuer"],
            audience: _config["JwtConfig:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}