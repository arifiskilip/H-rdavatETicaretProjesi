namespace WebUI.Models
{
    public class ProductAddModel
    {

        public string Kod { get; set; }
        public string Ad { get; set; }
        public bool? StokDurum { get; set; }
        public double? ListeFiyat { get; set; }
        public int? Iskonto1 { get; set; }
        public int? Iskonto2 { get; set; }
        public int? Iskonto3 { get; set; }
        public int? KutuAdet { get; set; }
    }
}
