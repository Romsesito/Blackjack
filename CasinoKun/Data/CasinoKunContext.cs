using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CasinoKun.Models;

namespace CasinoKun.Data
{
    public class CasinoKunContext : DbContext
    {
        public CasinoKunContext (DbContextOptions<CasinoKunContext> options)
            : base(options)
        {
        }

        public DbSet<CasinoKun.Models.User> User { get; set; } = default!;
    }
}
