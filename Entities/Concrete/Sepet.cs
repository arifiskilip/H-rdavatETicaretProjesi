namespace Entities.Concrete
{
    public class Sepet
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public int UrunId { get; set; }
        public int? Adet { get; set; }


        public Musteri Musteri { get; set; }
        public Urun Urun { get; set; }
    }
}
