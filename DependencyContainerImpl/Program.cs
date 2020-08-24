using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyContainerImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var typeinfo = typeof(PersonController);
            var constructor = typeinfo.GetConstructors().Single();
            var paratype= constructor.GetParameters().Select(a => a.ParameterType).Single();


            Console.WriteLine(paratype);

            var personRepo = (PersonRepository)Activator.CreateInstance(typeof(PersonRepository));
            var personcontroller = (PersonController)Activator.CreateInstance(typeof(PersonController), personRepo);
            personcontroller.DisaplyPerson();

            Console.ReadLine();


        }
    }

    public class DependencyContainer
    {

        
    }
    public interface IPersonRep
    {
        void Display();

    }

    public class PersonRepository : IPersonRep
    {
        public void Display()
        {
            Console.WriteLine("Display Person data");
        }
    }
    public class PersonController
    {
        private IPersonRep _repo;
        public PersonController(IPersonRep repo)
        {
            _repo = repo;

        }

        public void DisaplyPerson()
        {
            _repo.Display();

        
        }
    
    }
}
