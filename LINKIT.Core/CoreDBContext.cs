using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINKIT.Core
{
    public class CoreDBContext : DbContext
    {
        private string _sqlConnectionString = "sqlConnectionString";

        /// <summary>
        /// Create a SQL Database Context with the default setting.
        /// </summary>
        public CoreDBContext() { }

        /// <summary>
        /// Create a SQL Database Context with a custom setting.
        /// </summary>
        /// <param name="sqlConnectionString">The name of the setting containing the SQL connection string.</param>
        public CoreDBContext(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string sqlConnectionString = CoreConfiguration.Current.GetSectionValue(_sqlConnectionString);

            optionsBuilder.UseSqlServer(sqlConnectionString);
        }

        // Add Database Sets
        //public DbSet<RadarBlip> RadarBlips { get; set; }
    }
}
