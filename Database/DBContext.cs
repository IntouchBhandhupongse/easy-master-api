using easy_master_api.Infra;
using easy_master_api.Models;
using Microsoft.EntityFrameworkCore;

namespace easy_master_api.Class
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        #region DbSet List
        public DbSet<MasterModel> MASTER { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MasterConfiguration());
        }
    }
}