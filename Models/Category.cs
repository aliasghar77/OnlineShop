namespace OnlineShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        ICollection<CategoryToProduct> CategoryToProduct { get; set; }
    }
}
