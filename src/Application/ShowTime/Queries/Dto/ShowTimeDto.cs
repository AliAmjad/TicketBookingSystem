using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;

namespace CleanArchitecture.Application.ShowTime.Queries.Dto;
public class ShowTimeDto : IMapFrom<Domain.Entities.ShowTime>
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public double Price { get; set; }
    public string? MovieTitle { get; set; }
    public string? TheatreName { get; set; }
    public int MovieId { get; set; }
    public int TheatreId { get; set; }
}
