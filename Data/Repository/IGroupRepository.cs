using OnlineShop.Models;

namespace OnlineShop.Data.Repository
{
    public interface IGroupRepository
    {
        IEnumerable<Category> GetAllCategories();

        IEnumerable<ShowGroupViewModel> GetGroupForShow();
    }

    public class GroupRepository : IGroupRepository
    {
        private MyEShopContext _context;

        public GroupRepository(MyEShopContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _context.Categories
                .Select(c => new ShowGroupViewModel()
                {
                    GroupId = c.Id,
                    Name = c.Name,
                    ProductCount = _context.CategoryToProducts.Count(g => g.CategoryId == c.Id)
                })
                .ToList();
        }
    }
}
