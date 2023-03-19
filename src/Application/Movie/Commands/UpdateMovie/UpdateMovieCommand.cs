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

namespace CleanArchitecture.Application.Movie.Commands.UpdateMovie;
public class UpdateMovieCommand : IRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Image { get; set; }
}

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Movie), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.ReleaseDate = request.ReleaseDate;
        entity.Image = request.Image;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
