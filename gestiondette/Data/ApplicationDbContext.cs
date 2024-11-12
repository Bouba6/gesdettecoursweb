using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gestiondette.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }


    public DbSet<gestiondette.Models.Article> article { get; set; } = default!;

    public DbSet<gestiondette.Models.Client> client { get; set; } = default!;

    public DbSet<gestiondette.Models.User> users { get; set; } = default!;


}
