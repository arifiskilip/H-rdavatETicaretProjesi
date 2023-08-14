using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MusteriRol
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public int RoleId { get; set; }
    }
}
