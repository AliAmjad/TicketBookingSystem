using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Theatre.Queries.Dto;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Theatre.Queries.GetTheatres;

public record GetTheatreQuery : IRequest<TheatreDto>
{
    public int Id { get; set; }
}

public class GetTheatreHandler : IRequestHandler<GetTheatreQuery, TheatreDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTheatreHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TheatreDto> Handle(GetTheatreQuery request, CancellationToken cancellationToken)
    {
        return await _context.Theatres
             .AsNoTracking()
             .ProjectTo<TheatreDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
    }
}