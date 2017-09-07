using System;
using System.Data.Entity;
using Glossary.Data.Models;


namespace Glossary.Data
{
    public interface IDataContext : IDisposable
    {
        IDbSet<GlossaryEntity> Glossaries { get; set; }
        int SaveChanges();
    }
}
