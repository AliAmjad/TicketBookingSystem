using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Movie.Queries.Dto;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Movie.Queries.GetMovies;
public record GetMoviesQuery : IRequest<List<MovieDto>>;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Movies
             .AsNoTracking()
             .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
             .OrderBy(m => m.Title)
             .ToListAsync(cancellationToken);
    }
}