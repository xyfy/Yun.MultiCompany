using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore.Design;
namespace Yun.MultiCompany.EntityFrameworkCore
{
    public static class DbContextModelBuilderExtensions
    {

        public static void ConfigureMultiCompany([NotNull] this ModelBuilder builder)
        {
            builder.Entity<UserCompany>(b =>
            {
                var tableName = b.Metadata.GetTableName();
                b.ToTable(MultiCompanyConsts.DbTablePrefix + tableName, MultiCompanyConsts.DbSchema);
                b.HasKey(new[] { nameof(UserCompany.UserId), nameof(UserCompany.CompanyId) });
                b.Property(x => x.UserId).IsRequired();
                b.Property(x => x.CompanyId).IsRequired();
                b.HasOne(x => x.Company).WithMany(x => x.UserCompanys).HasForeignKey(x => x.CompanyId);
                b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
                b.ConfigureByConvention();
            });

            builder.Entity<Company>(b =>
            {
                var tableName = b.Metadata.GetTableName();
                b.ToTable(MultiCompanyConsts.DbTablePrefix + tableName, MultiCompanyConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<CompanyRole>(b =>
            {
                var tableName = b.Metadata.GetTableName();
                b.ToTable(MultiCompanyConsts.DbTablePrefix + tableName, MultiCompanyConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<CompanyUserRole>(b =>
            {
                var tableName = b.Metadata.GetTableName();
                b.ToTable(MultiCompanyConsts.DbTablePrefix + tableName, MultiCompanyConsts.DbSchema);
                b.HasKey(new[] { nameof(CompanyUserRole.UserId), nameof(CompanyUserRole.CompanyId), nameof(CompanyUserRole.CompanyRoleId) });
                b.Property(x => x.UserId).IsRequired();
                b.Property(x => x.CompanyId).IsRequired();
                b.Property(x => x.CompanyRoleId).IsRequired();
                b.HasOne(b => b.IdentityUser).WithMany().HasForeignKey(x => x.UserId);
                b.HasOne(b => b.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(b => b.CompanyRole).WithMany().HasForeignKey(x => x.CompanyRoleId);
                b.ConfigureByConvention();
            });
        }
    }
}
