using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class Movie : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Image { get; set; }
}
