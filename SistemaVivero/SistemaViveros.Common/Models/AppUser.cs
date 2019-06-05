using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveros.Common.Models
{
    public class AppUser : ApplicationUser
    {
        public List<Wall> Walls { get; set; }
    }
}
