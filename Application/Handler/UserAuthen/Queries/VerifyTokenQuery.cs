using Application.DTO.Response;
using Application.Interfaces.Repositories;
using Application.Model;
using AutoMapper;
using MediatR;

namespace Application.Handler.UserAuthen.Queries;

public record VerifyTokenQuery(string Httptoken) : IRequest<Response<GetAdminAuthenResponse>>;
public class VerifyTokenQueryHandler : IRequestHandler<VerifyTokenQuery, Response<GetAdminAuthenResponse>>
{
    private readonly IUserAuthenRespository _repo;
    public VerifyTokenQueryHandler(IUserAuthenRespository repo)
    {
        _repo = repo;
    }
    public async Task<Response<GetAdminAuthenResponse>> Handle(VerifyTokenQuery request, CancellationToken cancellationToken)
    {
        var result = _repo.VerifyToken(request.Httptoken);
        return Response<GetAdminAuthenResponse>.SuccessResult(result);
    }
}
