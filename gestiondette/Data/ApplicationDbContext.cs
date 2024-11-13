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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(u => u.CreateAt).HasColumnName("createat");
        modelBuilder.Entity<User>().Property(u => u.UpdateAt).HasColumnName("updateat");

        modelBuilder.Entity<Client>().ToTable("client");
        modelBuilder.Entity<Client>().Property(c => c.CreateAt).HasColumnName("createat");
        modelBuilder.Entity<Client>().Property(c => c.UpdateAt).HasColumnName("updateat");

        modelBuilder.Entity<Dette>().ToTable("dette");
        modelBuilder.Entity<Dette>().Property(c => c.CreateAt).HasColumnName("createat");
        modelBuilder.Entity<Dette>().Property(c => c.UpdateAt).HasColumnName("updateat");
        modelBuilder.Entity<Article>().ToTable("article");
        modelBuilder.Entity<Article>().Property(c => c.CreateAt).HasColumnName("createat");
        modelBuilder.Entity<Article>().Property(c => c.UpdateAt).HasColumnName("updateat");

        modelBuilder.Entity<DetailDette>().ToTable("detaildette");
        modelBuilder.Entity<DetailDette>().Property(c => c.CreateAt).HasColumnName("createat");
        modelBuilder.Entity<DetailDette>().Property(c => c.UpdateAt).HasColumnName("updateat");
        modelBuilder.Entity<Paiement>().ToTable("paiement");
        modelBuilder.Entity<Paiement>().Property(c => c.CreateAt).HasColumnName("createat");
        modelBuilder.Entity<Paiement>().Property(c => c.UpdateAt).HasColumnName("updateat");

        base.OnModelCreating(modelBuilder);
    }


    public DbSet<gestiondette.Models.Article> article { get; set; } = default!;

    public DbSet<gestiondette.Models.Client> client { get; set; } = default!;

    public DbSet<gestiondette.Models.User> users { get; set; } = default!;

    public DbSet<gestiondette.Models.Dette> dette { get; set; } = default!;

    public DbSet<gestiondette.Models.DetailDette> detaildette { get; set; } = default!;

    public DbSet<gestiondette.Models.Paiement> paiement { get; set; } = default!;




}
