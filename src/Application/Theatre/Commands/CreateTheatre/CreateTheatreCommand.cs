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

namespace CleanArchitecture.Application.Theatre.Commands.CreateTheatre;

public class CreateTheatreCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int SeatingCapacity { get; set; }
}

public class CreateMovieCommandHandler : IRequestHandler<CreateTheatreCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTheatreCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Theatre
        {
            Name = request.Name,
            Location = request.Location,
            SeatingCapacity = request.SeatingCapacity
        };

        _context.Theatres.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
