using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Movie.Queries.Dto;
public class MovieDto : IMapFrom<Domain.Entities.Movie>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Image { get; set; }
}
