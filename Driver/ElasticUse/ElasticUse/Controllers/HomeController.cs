using ElasticUse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticUse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly IElasticClient _elasticClient;
        private readonly IPersonRepositoryE personRepository;
        public HomeController(IElasticClient elasticClient, IPersonRepositoryE personRepository)
        {
            _elasticClient = elasticClient;
            this.personRepository = personRepository;
        }


       

        [HttpGet]
        public IActionResult Get(/*int pageSize=10,int pageIndex=0*/)
        {
            var p = new Person
            {
                FullName = "alinouri",
                Id = 2
            };
            // personRepository.Save(p);
            var analyzeResponse = _elasticClient.Indices.Analyze(a => a
  .Analyzer("html_strip")
  .Text("<p>F#</p> is THE SUPERIOR language :)")
);

            var searchResponse = _elasticClient.Search<Person>(s => s
            .Query(q => q
                .MatchAll()
            )
        );


            return Ok();

           //// var p = personRepository.GetAll(pageSize, pageIndex);
           ////return Ok(p);
        }
        [HttpPost]
        public IActionResult Post(/*string fullName*/)
        {
            var p = new Person
            {
                FullName = "alinouri",
                Id = 1
            };
            //_elasticClient.Update<Person>(p, c => c.Doc(p));
            return Ok();
            //var p = new Person { FullName = fullName ,Id=1};
            //personRepository.Save(p);
        
            //return RedirectToAction("Get");
        }
    }
}
