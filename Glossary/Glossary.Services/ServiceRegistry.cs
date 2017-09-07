using System;
using Glossary.Data;
using Glossary.Services.Implementations;
using Glossary.Services.Interfaces;
using StructureMap;

namespace Glossary.Services
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry(IoC container)
        {

            For<IGlossaryRepository>().Use<GlossaryRepository>();

            IncludeRegistry(new DataRegistry());
            For<IoC>().Use(container);
        }
    }
}
