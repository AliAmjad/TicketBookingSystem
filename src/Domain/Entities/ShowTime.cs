using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class ShowTime : BaseAuditableEntity
{
    public DateTime DateTime { get; set; }
    public double Price { get; set; }
    public Movie Movie { get; set; } = null!;
    public Theatre Theatre { get; set; } = null!;
}
