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
public record GetTheatresQuery : IRequest<List<TheatreDto>>;

public class GetTheatresQueryHandler : IRequestHandler<GetTheatresQuery, List<TheatreDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTheatresQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TheatreDto>> Handle(GetTheatresQuery request, CancellationToken cancellationToken)
    {
        return await _context.Theatres
             .AsNoTracking()
             .ProjectTo<TheatreDto>(_mapper.ConfigurationProvider)
             .OrderBy(t => t.Name)
             .ToListAsync(cancellationToken);
    }
}