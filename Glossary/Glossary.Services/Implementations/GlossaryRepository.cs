
using System;
using System.Collections.Generic;
using System.Linq;
using Glossary.Data;
using Glossary.Data.Models;
using Glossary.Services.DTO;
using Glossary.Services.Interfaces;

namespace Glossary.Services.Implementations
{
    public class GlossaryRepository : IGlossaryRepository
    {
        private readonly IDataContext _dataContext;

        public GlossaryRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool FindTerm(string term)
        {

            return _dataContext.Glossaries.Any(g => g.Term == term);

        }


        public GlossaryDto Add(GlossaryDto entity)
        {

            var glossaryEntity = new GlossaryEntity
            {
                GlossaryEntityId = entity.GlossaryId,
                Definition = entity.Definition,
                Term = entity.Term,
                DateCreated = entity.DateCreated,
                DateChanged = entity.DateChanged
            };

            _dataContext.Glossaries.Add(glossaryEntity);

            _dataContext.SaveChanges();

            entity.GlossaryId = glossaryEntity.GlossaryEntityId;

            return entity;

        }



        public void Remove(int id)
        {
            var glossaryEntity = _dataContext.Glossaries.SingleOrDefault(g => g.GlossaryEntityId == id);
            if (glossaryEntity == null)
                return;


            _dataContext.Glossaries.Remove(glossaryEntity);
            _dataContext.SaveChanges();


        }

        public GlossaryDto Update(GlossaryDto entity)
        {
            var glossaryEntity = _dataContext.Glossaries.SingleOrDefault(g => g.GlossaryEntityId == entity.GlossaryId);
            if (glossaryEntity == null)
                return null;

            glossaryEntity.Definition = entity.Definition;
            glossaryEntity.Term = entity.Term;

            _dataContext.SaveChanges();

            return new GlossaryDto
            {
                GlossaryId = glossaryEntity.GlossaryEntityId,
                Term = glossaryEntity.Term,
                Definition = glossaryEntity.Definition,
                DateChanged = glossaryEntity.DateChanged,
                DateCreated = glossaryEntity.DateCreated,
            };
            
        }

        public IEnumerable<GlossaryDto> Get()
        {
            return _dataContext.Glossaries.Select(g => new GlossaryDto
            {
                GlossaryId = g.GlossaryEntityId,
                Definition = g.Definition,
                Term = g.Term,
                DateCreated = g.DateCreated,
                DateChanged = g.DateChanged
            }).ToList();
        }

        public GlossaryDto Get(int id)
        {
            var glossaryEntity = _dataContext.Glossaries.SingleOrDefault(g => g.GlossaryEntityId == id);
            if (glossaryEntity == null)
                return null;

            return new GlossaryDto
            {
                GlossaryId = glossaryEntity.GlossaryEntityId,
                Definition = glossaryEntity.Definition,
                Term = glossaryEntity.Term,
                DateCreated = glossaryEntity.DateCreated,
                DateChanged = glossaryEntity.DateChanged
            };

        }

    }
}
