using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_DAL.IRepository;
using Web_DAL.Repository;

namespace Web_DAL
{
    public static class RepoServiceCollection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services) {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
