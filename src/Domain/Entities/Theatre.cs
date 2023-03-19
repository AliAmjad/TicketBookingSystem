using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class Theatre : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int SeatingCapacity { get; set; }
}
