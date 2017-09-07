using StructureMap.Web.Pipeline;

namespace Glossary.Data
{
    public class DataRegistry : StructureMap.Registry
    {
        public DataRegistry()
        {
            For<IDataContext>(new HybridLifecycle()).Use<DataContext>();
        }
    }
}
