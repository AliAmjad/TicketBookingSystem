using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.ShowTime.Commands.UpdateShowTime;
public class UpdateShowTimeCommand : IRequest
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public double Price { get; set; }
    public int MovieId { get; set; }
    public int TheatreId { get; set; }

}

public class UpdateShowTimeCommandHandler : IRequestHandler<UpdateShowTimeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateShowTimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateShowTimeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ShowTimes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.ShowTime), request.Id);
        }

        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == request.MovieId, cancellationToken: cancellationToken);
        var theatre = await _context.Theatres.FirstOrDefaultAsync(t => t.Id == request.TheatreId, cancellationToken: cancellationToken);

        if (movie == null || theatre == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.ShowTime), request.Id);
        }

        entity.DateTime = request.DateTime;
        entity.Price = request.Price;
        entity.Movie = movie;
        entity.Theatre = theatre;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
