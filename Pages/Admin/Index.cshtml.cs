using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MyEShopContext _context;

        public IndexModel(MyEShopContext context)
        {
            _context = context; 
        }
        public IEnumerable<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _context.Products.Include(i=>i.Item);
        }

        public void OnPost() 
        {
        
        
        }    
    }
}