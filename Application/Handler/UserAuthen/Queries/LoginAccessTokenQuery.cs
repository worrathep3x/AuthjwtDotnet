using Application.DTO.Response;
using Application.Handler.WeatherForecasts.Queries.GetWeather;
using Application.Interfaces.Repositories;
using Application.Model;
using AutoMapper;
using MediatR;

namespace Application.Handler.UserAuthen.Queries;

public class LoginAccessTokenQuery : IRequest<Response<GetAdminAuthenResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class GetUserAuthenQueryHandler : IRequestHandler<LoginAccessTokenQuery, Response<GetAdminAuthenResponse>>
{
    private readonly IUserAuthenRespository _repo;
    public GetUserAuthenQueryHandler(IUserAuthenRespository repo)
    {
        _repo = repo;
    }
    public async Task<Response<GetAdminAuthenResponse>> Handle(LoginAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.LoginAccessTokenAsync(request.Email, request.Password);
        if(result is null)
        {
            return Response<GetAdminAuthenResponse>.FailedResult("ไม่พบผู้ใช้งานนี้");
        }
        return Response<GetAdminAuthenResponse>.SuccessResult(result);
    }
}
