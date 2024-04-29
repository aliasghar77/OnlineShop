using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyEShopContext _context;

        public AddModel(MyEShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }

        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();


                var item = new Item()
                {
                    Price = Product.Price,
                    QuntityInStock = Product.QuantityInStock
                };
                _context.Add(item);
                _context.SaveChanges();

                var pro = new Product()
                {
                    Name = Product.Name,
                    Item = item,
                    Description = Product.Description,


                };

              

                //string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                //        "wwwroot",
                //        "images",
                //         pro.Id + Path.GetExtension(Product.Picture.FileName));
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    Product.Picture.CopyTo(stream);
                //}

                


                _context.Add(pro);
                _context.SaveChanges();
                pro.ItemId = pro.Id;
                _context.SaveChanges();

                
                    
                



            }
            catch (Exception)
            {


            }

            return RedirectToPage("Index");

        }
    }
}

