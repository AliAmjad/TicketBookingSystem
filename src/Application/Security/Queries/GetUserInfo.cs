using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Movie.Queries.Dto;
using CleanArchitecture.Application.Movie.Queries.GetMovies;
using CleanArchitecture.Application.Security.Dto;
using MediatR;

namespace CleanArchitecture.Application.Security.Queries;
public class GetUserInfoQuery : IRequest<UserInfoDto>
{
    public string UserId { get; set; }
}

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetUserInfoQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var isAdmin = await _identityService.IsInRoleAsync(request.UserId, "Administrator");
        return new UserInfoDto
        {
            IsAdmin = isAdmin
        };
    }
}
