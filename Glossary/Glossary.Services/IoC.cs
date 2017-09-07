using System;
using StructureMap;


namespace Glossary.Services
{
    public class IoC
    {
        private static IContainer _container;
        private static IoC _ioC;

        public static IoC GetInstance()
        {
            _ioC = _ioC ?? new IoC();
            return _ioC;
        }

        public IContainer Initialize(Registry registry)
        {
            _container = new Container(registry);
            return _container;
        }

        public T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        public T Resolve<T>(string name)
        {
            return _container.GetInstance<T>(name);
        }

        public void BuildUp(object target)
        {
            _container.BuildUp(target);
        }

        // helper method that shows what's in the container
        public string WhatDoIHave()
        {
            return _container.WhatDoIHave();
        }

        public IContainer GetContainer()
        {
            return _container;
        }
    }
}
