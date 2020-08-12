using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketProject.Models;

namespace TicketProject.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ticket>().Property(c => c.status).HasConversion<int>();
            base.OnModelCreating(builder);
            builder.Entity<Ticket_Management>().Property(c => c.Status).HasConversion<int>();
            base.OnModelCreating(builder);
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Ticket_Management>  Ticket_Managements {get; set;}
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<IdentityUser> IdentityUsers { get; set; }
    }
}
