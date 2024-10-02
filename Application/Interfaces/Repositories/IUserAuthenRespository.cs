using Application.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Repositories;

public interface IUserAuthenRespository
{
    Task<GetAdminAuthenResponse?> LoginAccessTokenAsync(string _user, string _password);
    GetAdminAuthenResponse VerifyToken(string HttpToken);
    GetUserAuthenResponseDTO RefreshToken(HttpContext HttpToken);
}
