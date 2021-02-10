using ElasticUse.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticUse
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                
                .DefaultIndex(defaultIndex)
                
                .DefaultMappingFor<Person>(m => m.PropertyName(p => p.Id, "id"))
                .DefaultMappingFor<Teacher>(p=>p.PropertyName(x=>x.Id,"id"))
                .DefaultMappingFor<Teacher>(s=>s.Ignore(e=>e.InvalidName))
                .DefaultMappingFor<Htmls>(h=>h.PropertyName(p=>p.Id,"id" )
                );
            var client = new ElasticClient(settings);
          
            services.AddSingleton<IElasticClient>(client);
        }
    }
}
