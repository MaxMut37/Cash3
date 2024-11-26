using Lab2.Models;

namespace Lab2.Repositories
{
    public class Repository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public List<Item> GetAll()
        {
            return _context.Items.ToList();
        }

        public Item? GetById(int id)
        {
            return _context.Items.FirstOrDefault(i => i.Id == id);
        }

        public void Add(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void Update(Item item)
        {
            _context.Items.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
