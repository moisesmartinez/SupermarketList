using Microsoft.AspNetCore.Mvc;
using SupermarketListUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketListUI.Data;

namespace SupermarketListUI.Controllers
{
    public class SupermarketListController : Controller
    {
        private SupermarketListDbContext _dbContext;

        public SupermarketListController(SupermarketListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.SupermarketLists.ToList());
        }

        //GET method
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create([Bind("Descripcion")] SupermarketList supermarketList)
        {
            //Validate if Description is empty
            if(!string.IsNullOrEmpty(supermarketList.Descripcion))
            {
                //Validate if entry exists in Database
                var dbSupermarketListItem = _dbContext.SupermarketLists.FirstOrDefault(x => x.Descripcion == supermarketList.Descripcion);
                if(dbSupermarketListItem == null)
                {
                    _dbContext.SupermarketLists.Add(supermarketList);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["error"] = $"Item '{supermarketList.Descripcion}' already exists in database";
                }
            }
            return View(supermarketList);
        }
    }
}
