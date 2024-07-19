using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursKayitController:Controller
    {
        private readonly DataContext _context;

        public KursKayitController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kursKayitlari = await _context.KursKayitlari.Include(x=>x.Ogrenci).Include(x=>x.Kurs).ToListAsync();
            return View(kursKayitlari);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList( await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList( await _context.Kurslar.ToListAsync(), "KursId", "Baslik");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            model.KayitTarihi = DateTime.Now;
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            ViewBag.Ogrenciler = new SelectList( await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList( await _context.Kurslar.ToListAsync(), "KursId", "Baslik");


            var kurkayit = await _context.KursKayitlari.FindAsync(id); //FinAsync ile sadece Id ile arama yapılır
            // var orgr = await _context.KursKayitlari.FirstOrDefaultAsync(o => o.Eposta == eposta ) // Sadece Id ile değil diğer alanlarla da arama yapılıp getirilir
           
            if(kurkayit==null)
            {
                return NotFound();
            }

            return View(kurkayit);
        }





        [HttpPost]
        [ValidateAntiForgeryToken] // Sayfada form içinde hiddden olarak oluşturulan __RequestVerificationToken'nin oluşturulması istenilir.
                                   // Böylelikle formu gönderenin isteyen kişi ile aynı olduğu denetlenmiş olur
        public async Task<IActionResult> Edit(int id, KursKayit model)
        {
            if(id != model.KayitId)
            {
                return NotFound();
            }


            if(ModelState.IsValid)
            {

                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException) // Daha genel Db hataları için
                {
                    if(!_context.KursKayitlari.Any(o => o.KayitId == model.KayitId)) // Düzeltilmek istenilen kayıt veritabanında yoksa
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }









        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var kurskaydi = await _context.KursKayitlari.FindAsync(id);

            if(kurskaydi == null)
            {
                return NotFound();
            }

            return View(kurskaydi);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id) //Model Binding nereden geliyor. Burada formdan
        {

            var kurskaydi = await _context.KursKayitlari.FindAsync(id);
            if(kurskaydi == null)
            {
                return NotFound();
            }
            _context.KursKayitlari.Remove(kurskaydi);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }






    }
}