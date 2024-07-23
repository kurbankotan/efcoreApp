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







    }
}