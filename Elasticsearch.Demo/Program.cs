using Nest;
using System;
using System.Collections.Generic;

namespace Elasticsearch.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                                            .DefaultIndex("test_index");

            var client = new ElasticClient(settings);

            var person = new Person
            {
                Id = 2,
                Name = "Emrah",
                Surname = "Oner"
            };

            var indexResponse = client.IndexDocument(person);

            var searchResponse = client.Search<Person>(s => s
                                            .AllIndices()
                                            .From(0)
                                            .Size(10)
                                            .Query(q => q
                                                    .Match(m => m
                                                        .Field(f => f.Name)
                                                        .Query("Belma")))
                                            );

            var people = searchResponse.Documents;


        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public abstract class Document
    {
        public JoinField Join { get; set; }
    }

    public class Company : Document
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }

    }

    public class Employee : Document
    {
        public string Lastname { get; set; }
        public int Salary { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsManager { get; set; }
        public List<Employee> Employees { get; set; }
        public TimeSpan Hours { get; set; }

    }
}
