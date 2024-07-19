using System.Reflection.Metadata;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers
{
    public class OgrenciController:Controller
    {


        //Constructor
        private readonly DataContext _context;

        public OgrenciController(DataContext context)
        {
            _context = context;
        }



        //Ogrencileri listeleme
        public async Task<IActionResult> Index()
        {
            //var ogrenciler =await _context.Ogrenciler.ToListAsync();
            //return View(ogrenciler);
            //Tek satırda
            return View(await _context.Ogrenciler.ToListAsync());
        }

        


        //Ogrenci oluşturup veritabanına ekleme
        public IActionResult Create()
        {
            return View();
        }
    

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
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

            var ogr = await _context.Ogrenciler.Include(o=>o.KursKayitlari).ThenInclude(o=>o.Kurs).FirstOrDefaultAsync(o=>o.OgrenciId==id); //FinAsync ile sadece Id ile arama yapılır
            // var orgr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.Eposta == eposta ) // Sadece Id ile değil diğer alanlarla da arama yapılıp getirilir
           
            if(ogr==null)
            {
                return NotFound();
            }

            return View(ogr);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] // Sayfada form içinde hiddden olarak oluşturulan __RequestVerificationToken'nin oluşturulması istenilir.
                                   // Böylelikle formu gönderenin isteyen kişi ile aynı olduğu denetlenmiş olur
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if(id != model.OgrenciId)
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
                catch(DbUpdateConcurrencyException)
                {
                    if(!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId)) // Düzeltilmek istenilen kayıt veritabanında yoksa
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

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if(ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id) //Model Binding nereden geliyor. Burada formdan
        {

            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if(ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var ogr = await _context.Ogrenciler.Include(o=>o.KursKayitlari).ThenInclude(o=>o.Kurs).FirstOrDefaultAsync(o=>o.OgrenciId==id); //FinAsync ile sadece Id ile arama yapılır
            // var orgr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.Eposta == eposta ) // Sadece Id ile değil diğer alanlarla da arama yapılıp getirilir
           
            if(ogr==null)
            {
                return NotFound();
            }

            return View(ogr);
        }

    }
}