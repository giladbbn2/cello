using Cello.Api.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cello.Api.Services.Extensions
{
    public static class FamilyTreeServiceCollectionExtensions
    {
        public static void AddFamilyTreeService(this IServiceCollection services)
        {
            services.AddSingleton<IFamilyTreeService, FamilyTreeService>();
        }
    }
}
