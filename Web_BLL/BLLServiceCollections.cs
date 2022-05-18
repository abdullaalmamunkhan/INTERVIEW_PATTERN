using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_BLL
{
    public static class BLLServiceCollections
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            return services;
        }
    }
}
