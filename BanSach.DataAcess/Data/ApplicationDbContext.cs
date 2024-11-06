using BanSach.Model;
using Microsoft.EntityFrameworkCore;

namespace BanSach.DataAcess.Data
{
    public class ApplicationDbContext:DbContext
    {
        // hàm tạo
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //khai báo list Category
        public DbSet<Category> Categories{ get; set; }
        public DbSet<CoverType> CoverTypes{ get; set; }
        public DbSet<Product> Products{ get; set; }
    }
}
