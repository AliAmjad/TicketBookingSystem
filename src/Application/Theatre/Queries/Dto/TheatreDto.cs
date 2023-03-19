using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Theatre.Queries.Dto;
public class TheatreDto : IMapFrom<Domain.Entities.Theatre>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int SeatingCapacity { get; set; }
}
