using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogretmen
    {   [Key]
        public int OgretmenId {get; set;}

        public string? Ad {get; set;}

        public string? Soyad { get; set; }

        public string? Eposta { get; set; }

        public string? Telefon { get; set; }

        [DataType(DataType.Date)] //Sadece Tarih. Saat olmayacak
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        public DateTime BaslamaTarihi { get; set; }

        public ICollection<Kurs> Kurslar {get; set;} = new List<Kurs>();

    }
}