using EConsult_T.DAL.EF;
using EConsult_T.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EConsult_T.Api.Extensions
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection AddEFUnitOfWork(this IServiceCollection services, string connection)
        {
            var builder = new DbContextOptionsBuilder<EConsultDbContext>()
                .UseSqlServer(connection);
            services.AddTransient<IUnitOfWork>(provider => new EFUnitOfWork(builder.Options));

            return services;
        }
    }
}
