
using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        { 
            await  _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnologia" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Licores" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Shared.Entities.Country { Name = "Colombia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Perú" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Argentina" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "USA" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Italia" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
