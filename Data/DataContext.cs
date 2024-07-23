using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>(); //Initial edelim 

        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();

        public DbSet<KursKayit> KursKayitlari =>Set<KursKayit>();

        //Codefirst => Koddan veritabanına
        // Model first
        // Database first => veritabına önce sonra veritabanından entity'e


        public DbSet<Ogretmen> Ogretmenler =>Set<Ogretmen>();

    }
}