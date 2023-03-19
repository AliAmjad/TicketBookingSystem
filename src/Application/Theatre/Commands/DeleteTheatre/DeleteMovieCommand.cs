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

namespace CleanArchitecture.Application.Theatre.Commands.DeleteTheatre;
public record DeleteTheatreCommand(int Id) : IRequest;

public class DeleteTheatreCommandHandler : IRequestHandler<DeleteTheatreCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTheatreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTheatreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Theatres
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Movie), request.Id);
        }

        _context.Theatres.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

