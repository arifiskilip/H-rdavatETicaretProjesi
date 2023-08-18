using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Marka
    {
        public int Id { get; set; }
        public string? Ad { get; set; }

        public List<Urun> Urun { get; set; }
    }
}
