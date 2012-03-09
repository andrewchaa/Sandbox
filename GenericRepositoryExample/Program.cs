using System;
using StructureMap;


namespace GenericRepositoryExample
{
    class Program
    {
        static void Main(string[] args)
        {
           ObjectFactory.Initialize(x =>
                                        {
                                            x.For<IRepository<Company>>().Use<CompanyRepository>();
                                            x.For<IRepository<User>>().Use<UserRepository>();
                                        });

            var repository = ObjectFactory.GetInstance<GenericRepository>();
            var company = repository.Find<Company>(1);
            var user = repository.Find<User>(10);

            Console.WriteLine("Object: " + company.GetType() + ", " + company.Id);
            Console.WriteLine("Object: " + user.GetType() + ", " + user.Id);
        }
    }

    public class GenericRepository
    {
        public T Find<T>(int id)
        {
            var actualRepository = ObjectFactory.GetInstance<IRepository<T>>();
            return actualRepository.Find(id);
        }

    }

    public class UserRepository : IRepository<User>
    {
        public User Find(int id)
        {
            return new User { Id = id };
        }
    }

    public class CompanyRepository : IRepository<Company>
    {
        public Company Find(int id)
        {
            return new Company { Id = id};
        }
    }

    public interface IRepository<T>
    {
        T Find(int id);
    }

    public class User
    {
        public int Id { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
    }
}
