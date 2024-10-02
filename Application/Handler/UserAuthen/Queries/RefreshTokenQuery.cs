using Application.DTO.Response;
using Application.Interfaces.Repositories;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Handler.UserAuthen.Queries;

public record RefreshTokenQuery(HttpContext Httptoken) : IRequest<Response<GetUserAuthenResponseDTO>>;
public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, Response<GetUserAuthenResponseDTO>>
{
    private readonly IUserAuthenRespository _repo;
    public RefreshTokenQueryHandler(IUserAuthenRespository repo)
    {
        _repo = repo;
    }
    public async Task<Response<GetUserAuthenResponseDTO>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var result = _repo.RefreshToken(request.Httptoken);
        return Response<GetUserAuthenResponseDTO>.SuccessResult(result);
    }
}
