using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Movie.Commands.DeleteMovie;
public record DeleteMovieCommand(int Id) : IRequest;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Movie), request.Id);
        }

        _context.Movies.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

