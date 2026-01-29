using Microsoft.EntityFrameworkCore;
using ClientesAppMvc.Models;

namespace ClientesAppMvc.Data;

public class ApplicationDbContext : DbContext 
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

    // DbSet for Clientes

	public DbSet<Cliente> Clientes { get; set; }
}
