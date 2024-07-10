using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        //id 0 => primary key
        [Key]
        public int OgrenciId { get; set; }

        public string? OgrenciAd { get; set; }

        public string? Ogrencisoyad { get; set; }

        public string? Eposta { get; set; }

        public string? Telefon { get; set; }
    }
}