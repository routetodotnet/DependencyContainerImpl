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
            DependencyContainer container = new DependencyContainer();
            container.Add(typeof(IPersonRep), typeof(PersonRepository));

            var typeinfo = typeof(PersonController);
            var constructor = typeinfo.GetConstructors().Single();
            var paratype= constructor.GetParameters().Select(a => a.ParameterType).Single();
            Console.WriteLine(paratype.Name);
            var resolvertype=container.GetTypeInfo(paratype.Name.GetType());
            var resolverobject=container.dicContainer.FirstOrDefault(a => a.Key == resolvertype);

          //  var personRepo = (PersonRepository)Activator.CreateInstance(typeof(PersonRepository));
            var personcontroller = (PersonController)Activator.CreateInstance(typeof(PersonController),(resolverobject));
            personcontroller.DisaplyPerson();

            Console.ReadLine();


        }
    }

    public class DependencyContainer
    {
        public Dictionary<Type, Type> dicContainer;
        public DependencyContainer()
        {
            dicContainer = new Dictionary<Type, Type>();

        }
        public void Add(Type tkey,Type tvalue)
        {
            dicContainer.Add(tkey, tvalue);

        }
        public Type GetTypeInfo(Type tkey)
        {

            return dicContainer[tkey];
        }

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
