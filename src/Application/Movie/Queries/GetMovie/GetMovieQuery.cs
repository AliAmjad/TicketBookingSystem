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

public record GetMovieQuery : IRequest<MovieDto>
{
    public int Id { get; set; }
}

public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMovieQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovieDto> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {
        return await _context.Movies
             .AsNoTracking()
             .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
    }
}