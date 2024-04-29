using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private MyEShopContext _context;

        public ProductController(MyEShopContext context)
        {
            _context = context;
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id , string name)
        {
            ViewData["GroupName"] = name;
            var products = _context.CategoryToProducts
                .Where(i=>i.CategoryId == id)
                .Include(c=> c.Product)
                .Select(a=>a.Product)
                .ToList();

            return View(products);
        }
    }
}
