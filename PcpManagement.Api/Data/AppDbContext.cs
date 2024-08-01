using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PcpManagement.Core.Models;

namespace PcpManagement.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<VirtualMachine> VirtualMachines { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}