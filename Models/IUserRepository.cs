using OnlineShop.Data;

namespace OnlineShop.Models
{
    public interface IUserRepository
    {
        void AddUser(Users user);

        public bool IsExistUserByEmail(string email);

        Users GetUserForLogin(string email , string password);
    }

    public class UserRepository : IUserRepository
    {
        private MyEShopContext _context;

        public UserRepository(MyEShopContext context)
        {
            _context = context;
        }
        public void AddUser(Users user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool IsExistUserByEmail(string email)
        {
            if (_context.Users.Any(i=> i.Email == email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(i => i.Email == email && i.Password == password);
        }
    }
}
