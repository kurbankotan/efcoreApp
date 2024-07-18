using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController:Controller
    {
        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }

        //Kurs oluşturup veritabanına ekleme
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



       //Kurs güncelleme
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id); //FinAsync ile sadece Id ile arama yapılır
            // var orgr = await _context.Kurslar.FirstOrDefaultAsync(o => o.Tarihler == Tarihler ) // Sadece Id ile değil diğer alanlarla da arama yapılıp getirilir
           
            if(kurs==null)
            {
                return NotFound();
            }

            return View(kurs);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Sayfada form içinde hiddden olarak oluşturulan __RequestVerificationToken'nin oluşturulması istenilir.
                                   // Böylelikle formu gönderenin isteyen kişi ile aynı olduğu denetlenmiş olur
        public async Task<IActionResult> Edit(int id, Kurs model)
        {
            if(id != model.KursId)
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
                    if(!_context.Kurslar.Any(o => o.KursId == model.KursId)) // Düzeltilmek istenilen kayıt veritabanında yoksa
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

            return View(model);
        }


    }
}