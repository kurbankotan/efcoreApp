using System.Reflection.Metadata;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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





    }
}