using System.Reflection.Metadata;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers
{
    public class OgretmenController:Controller
    {

        private readonly DataContext _context;

        public OgretmenController(DataContext context)
        {
            _context = context;
        }


        //Öğretmenleri listeleme
        public async Task<IActionResult> Index()
        {
            //var ogretmenler =await _context.Ogretmenler.ToListAsync();
            //return View(ogretmenler);
            //Tek satırda
            return View(await _context.Ogretmenler.ToListAsync());
        }


            //Öğretmen oluşturup veritabanına ekleme
        public IActionResult Create()
        {
            return View();
        }
    

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            _context.Ogretmenler.Add(model);
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

            var ogr = await _context.Ogretmenler.Include(o=>o.Kurslar).FirstOrDefaultAsync(o=>o.OgretmenId==id);//FinAsync ile sadece Id ile arama yapılır
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
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if(id != model.OgretmenId)
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
                    if(!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId)) // Düzeltilmek istenilen kayıt veritabanında yoksa
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

            var ogretmen = await _context.Ogretmenler.FindAsync(id);

            if(ogretmen == null)
            {
                return NotFound();
            }

            return View(ogretmen);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id) //Model Binding nereden geliyor. Burada formdan
        {

            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if(ogretmen == null)
            {
                return NotFound();
            }
            _context.Ogretmenler.Remove(ogretmen);
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

            var ogr = await _context.Ogretmenler.Include(o=>o.Kurslar).FirstOrDefaultAsync(o=>o.OgretmenId==id); //FinAsync ile sadece Id ile arama yapılır
            // var orgr = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.Eposta == eposta ) // Sadece Id ile değil diğer alanlarla da arama yapılıp getirilir
           
            if(ogr==null)
            {
                return NotFound();
            }

            return View(ogr);
        }







    }
}