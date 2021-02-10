using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticUse.Models
{
    public interface IPersonRepositoryE
    {
        void Save(Person person);
        IEnumerable<Person> GetAll(int count, int skip = 0);
        IEnumerable<Person> Search(string searchTerm, int count, int skip = 0);
    }
    public interface IPersonRepository
    {
        void Save(Person person);

        List<Person> GetPeople(int pageSize,int pageIndex);


    }
    public class ElasticPersonRepository: IPersonRepositoryE
    {
        private readonly IElasticClient elasticClient;

        public ElasticPersonRepository(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public IEnumerable<Person> GetAll(int count, int skip = 0)
        {
            var searchResponse = elasticClient.Search<Person>(s => s
                    .AllIndices()
                    .From(0)
                    .Size(10)
                ).Documents;
            return searchResponse;
        }

        public void Save(Person person)
        {
            elasticClient.IndexDocument(person);
        }

        public IEnumerable<Person> Search(string searchTerm, int count, int skip = 0)
        {
            var searchResponse = elasticClient.Search<Person>(s => s
                .AllIndices()
                .From(0)
                .Size(10)
                .Query(q => q
                     .Match(m => m
                        .Field(f => f.FullName)
                        .Query(searchTerm)
                     )
                )
            ).Documents;
            return searchResponse.ToList();
        }
    }

    public class InMemonryPersonRepository : IPersonRepository
    {
        public static List<Person> People = new List<Person>();
        public List<Person> GetPeople(int pageSize, int pageIndex)
        {
           return People.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public void Save(Person person)
        {
            var id = People.Count() + 1;
            var p = new Person
            {
                Id=id,
                FullName = person.FullName,
            };
        }
    }
}
