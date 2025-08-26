using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PokemonDbContext>
    {
        public PokemonDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PokemonDbContext>();

            var conn = "User Id=rm558782;Password=fiap25;Data Source=oracle.fiap.com.br:1521/orcl";

            optionsBuilder.UseOracle(conn);

            return new PokemonDbContext(optionsBuilder.Options);
        }
    }
}
