using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_DAL.DataBaseMecanism
{
    public static class StoreProcedureService
    {

        public static IServiceCollection AddStoredProcedure(this IServiceCollection services)
        {
            services.AddTransient<ISPConnection, SPConnection>();
            services.AddTransient<ISPCommand, SPCommand>();
            services.AddTransient<IStoredProcedure, StoredProcedure>();

            return services;
        }

    }
}
