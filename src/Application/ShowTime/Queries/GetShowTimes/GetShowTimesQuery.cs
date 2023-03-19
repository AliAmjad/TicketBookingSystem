using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.ShowTime.Queries.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.ShowTime.Queries.GetShowTimes;

public record GetShowTimesQuery : IRequest<List<ShowTimeDto>>;

public class GetShowTimesQueryHandler : IRequestHandler<GetShowTimesQuery, List<ShowTimeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShowTimesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ShowTimeDto>> Handle(GetShowTimesQuery request, CancellationToken cancellationToken)
    {
        var showTimes = await _context.ShowTimes
             .AsNoTracking()
             .ProjectTo<ShowTimeDto>(_mapper.ConfigurationProvider)
             .OrderBy(st => st.DateTime)
             .ToListAsync(cancellationToken);

        foreach (var item in showTimes)
        {
            item.MovieTitle = _context.Movies.FirstOrDefault(m => m.Id == item.MovieId)?.Title;
            item.TheatreName = _context.Theatres.FirstOrDefault(t => t.Id == item.TheatreId)?.Name;
        }

        return showTimes;
    }
}