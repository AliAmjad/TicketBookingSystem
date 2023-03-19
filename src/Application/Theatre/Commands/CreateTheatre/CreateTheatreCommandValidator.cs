﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Theatre.Commands.CreateTheatre;

public class UpdateTheatreCommandValidator : AbstractValidator<CreateTheatreCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTheatreCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified Name already exists.");
    }

    public async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
    {
        return await _context.Theatres
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
