using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options) 
{
    public DbSet<OnlineShopUser> Users { get; set; }
}
