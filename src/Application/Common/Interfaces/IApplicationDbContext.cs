using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Domain.Entities.Movie> Movies { get; }

    DbSet<Domain.Entities.Theatre> Theatres { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
