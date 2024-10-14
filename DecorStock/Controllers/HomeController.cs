using DecorStock.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DecorStock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly ItemsDbContext _itemsDbContext;

        public HomeController(ILogger<HomeController> logger, ItemsDbContext itemsDbContext)
        {
            _logger = logger;
            _itemsDbContext = itemsDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Items()
        {
            var allItems = _itemsDbContext.Items.ToList();

            var totalItemsPrice = allItems.Sum(x => x.Price);

            ViewBag.Items = totalItemsPrice;

            return View(allItems);
        }

        public IActionResult CreateEditItems(int? id)
        {
            if (id != null) 
            {
                var itemInDb = _itemsDbContext.Items.SingleOrDefault(item => item.Id == id);
                return View(itemInDb);
            }
            return View();
        }

        public IActionResult DeleteItem(int id) 
        {
            var itemInDb = _itemsDbContext.Items.SingleOrDefault(item => item.Id == id);
            _itemsDbContext.Items.Remove(itemInDb);
            _itemsDbContext.SaveChanges();
            return RedirectToAction("Items");
        }

        public IActionResult CreateEditItemForm(Item model)
        {
            if (model.Id == 0 )
            {
                // Creating an Item
                _itemsDbContext.Items.Add(model);
            } else
            {
                _itemsDbContext.Items.Update(model);
            }

           
            _itemsDbContext.SaveChanges();

            return RedirectToAction("Items");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
