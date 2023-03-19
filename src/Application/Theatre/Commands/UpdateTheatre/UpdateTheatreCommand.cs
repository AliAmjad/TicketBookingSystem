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

namespace CleanArchitecture.Application.Theatre.Commands.UpdateTheatre;
public class UpdateTheatreCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int SeatingCapacity { get; set; }
}

public class UpdateMovieCommandHandler : IRequestHandler<UpdateTheatreCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTheatreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Theatres
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Theatre), request.Id);
        }

        entity.Name = request.Name;
        entity.Location = request.Location;
        entity.SeatingCapacity = request.SeatingCapacity;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
