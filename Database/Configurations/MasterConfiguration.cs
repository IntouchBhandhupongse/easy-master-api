using easy_master_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace easy_master_api.Infra;

public class MasterConfiguration : IEntityTypeConfiguration<MasterModel>
{
    public void Configure(EntityTypeBuilder<MasterModel> builder)
    {
        builder
            .HasKey(a => a.id);
            
        builder
            .Property(m => m.first_name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .Property(m => m.last_name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(m => m.tel)
            .HasMaxLength(20);
        
        builder
            .Property(m => m.email)
            .HasMaxLength(50);

        builder
            .ToTable("master");
    }
}