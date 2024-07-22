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
                if (id == null)
                {
                    return NotFound();
                }

                var kurskayit = await _context.KursKayitlari
                    .Include(x => x.Ogrenci)
                    .Include(x => x.Kurs)
                    .FirstOrDefaultAsync(m => m.KayitId == id);

                if (kurskayit == null)
                {
                    return NotFound();
                }

                ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad", kurskayit.OgrenciId);
                ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik", kurskayit.KursId);

                return View(kurskayit);
        }





            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, KursKayitEditModel model)
            {
                if (id != model.KayitId)
                {
                    return NotFound();
                }
                

                if (!ModelState.IsValid)
                {
                    // ModelState geçerli değilse, kullanıcıya hangi alanların hatalı olduğunu göstermek için aynı formu tekrar göster
                    ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad", model.OgrenciId);
                    ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik", model.KursId);
                    return View(model);
                }
                try
                {
                    var kurskayit = await _context.KursKayitlari
                    .FirstOrDefaultAsync(m => m.KayitId == id);

                    kurskayit.KursId = model.KursId;
                    kurskayit.OgrenciId = model.OgrenciId;

                    _context.Update(kurskayit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception details here to diagnose the issue
                    ModelState.AddModelError("", "Güncelleme işlemi sırasında bir hata oluştu.");
                    return View(model);

                }
                
            }









        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            //var kurskaydi = await _context.KursKayitlari.FindAsync(id);
            var kurskaydi = await _context.KursKayitlari.Include(o=>o.Ogrenci).Include(o=>o.Kurs).FirstOrDefaultAsync(o=>o.KayitId==id);

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