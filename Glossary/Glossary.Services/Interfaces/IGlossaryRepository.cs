using Glossary.Services.DTO;

namespace Glossary.Services.Interfaces
{
    public interface IGlossaryRepository : IDataRepository<GlossaryDto>
    {
        bool FindTerm(string term);
    }
}
