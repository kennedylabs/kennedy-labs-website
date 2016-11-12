using Microsoft.EntityFrameworkCore;

namespace KennedyLabsWebsite.Models
{
    public class WebsiteContext : DbContext
    {
        public DbSet<PageModel> Pages { get; set; }

        public DbSet<SectionModel> Sections { get; set; }

        public DbSet<ItemModel> Items { get; set; }
    }
}
