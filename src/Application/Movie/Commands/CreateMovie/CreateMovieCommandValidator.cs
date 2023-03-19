using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Movie.Commands.CreateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMovieCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Movies
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
