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
            foreach (var item in container.dicContainer)
            {
                Console.WriteLine($"{ item.Key}--{item.Value} ");

            }

            var typeinfo = typeof(PersonController);
            var constructor = typeinfo.GetConstructors().Single();
            var paratype= constructor.GetParameters().Select(a => a.ParameterType).Single();
            Console.WriteLine(paratype);
            var dicTypeObject = container.dicContainer.FirstOrDefault(a => a.Key.Equals(paratype));
            Console.WriteLine(dicTypeObject.Value.UnderlyingSystemType);

           var resolvertype= dicTypeObject.Value.UnderlyingSystemType;

            Console.WriteLine(resolvertype);
           //var resolverobject=container.dicContainer.FirstOrDefault(a =>a.Key== paratype.Name);

           var personRepo = (PersonRepository)Activator.CreateInstance(resolvertype);
            var personcontroller = (PersonController)Activator.CreateInstance(typeof(PersonController),personRepo);
            personcontroller.DisaplyPerson();

            Console.ReadLine();


        }
    }

    public class DependencyContainer
    {
        public Dictionary<Type , Type> dicContainer;
        public DependencyContainer()
        {
            dicContainer = new Dictionary<Type, Type>();

        }
        public void Add(Type tkey, Type tvalue)
        {
            dicContainer.Add(tkey, tvalue);

        }
        public Type GetTypeInfo(Type tkey)
        {

            return dicContainer[tkey].GetType();
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
