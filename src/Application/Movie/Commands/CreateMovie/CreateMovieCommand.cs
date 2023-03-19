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

namespace CleanArchitecture.Application.Movie.Commands.CreateMovie;

public class CreateMovieCommand : IRequest<int>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Image { get; set; }
}

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Movie
        {
            Title = request.Title,
            Description = request.Description,
            ReleaseDate = request.ReleaseDate,
            Image = request.Image
        };

        _context.Movies.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
