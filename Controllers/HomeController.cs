using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyEShopContext _context;
        

        public HomeController(ILogger<HomeController> logger, MyEShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var product = _context.Products.ToList();
            return View(product);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList();

            var vm = new DetalViewModel()
            {
                Product = product,
                Categories = categories
            };

            return View(vm);
        }

        
        public IActionResult AddToCart(int itemId)
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == itemId);
            if (product != null)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
                if (order != null)
                {
                    var orderDetail =
                        _context.OrderDetails.FirstOrDefault(d =>
                            d.OrderId == order.OrderId && d.ProductId == product.Id);
                    if (orderDetail != null)
                    {
                        orderDetail.Count += 1;
                    }
                    else
                    {
                        _context.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.Id,
                            Price = product.Item.Price,
                            Count = 1
                        });
                    }
                }
                else
                {
                    order = new Orders()
                    {
                        IsFinaly = false,
                        CreateDate = DateTime.Now,
                        UserId = userId
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    _context.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.Id,
                        Price = product.Item.Price,
                        Count = 1
                    });
                }

                _context.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Orders.Where(o => o.UserId == userId && !o.IsFinaly)
                .Include(o => o.OrderDetails)
                .ThenInclude(c => c.Product).FirstOrDefault();
            return View(order);
        }

        public IActionResult RemoveCart(int detailId)
        {
            var orderDetail = _context.OrderDetails.Find(detailId);
            _context.Remove(orderDetail);
            _context.SaveChanges();

            return RedirectToAction("ShowCart");
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
