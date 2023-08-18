using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UrunResim
    {
        public int Id { get; set; }
        public int? UrunId { get; set; }
        public string? ResimYol { get; set; }

        public Urun Urun { get; set; }
    }
}
