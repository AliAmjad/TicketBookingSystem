using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.ShowTime.Commands.CreateShowTime;

public class CreateShowTimeCommand : IRequest<int>
{
    public DateTime DateTime { get; set; }
    public double Price { get; set; }
    public int MovieId { get; set; }
    public int TheatreId { get; set; }
}

public class CreateShowTimeCommandHandler : IRequestHandler<CreateShowTimeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateShowTimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateShowTimeCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == request.MovieId, cancellationToken: cancellationToken);
        var theatre = await _context.Theatres.FirstOrDefaultAsync(t => t.Id == request.TheatreId, cancellationToken: cancellationToken);

        if (movie == null || theatre == null)
        {
            return 0;
        }

        var entity = new Domain.Entities.ShowTime
        {
            DateTime = request.DateTime,
            Price = request.Price,
            Movie = movie,
            Theatre = theatre
        };

        _context.ShowTimes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
