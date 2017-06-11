using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PANExam.DAL.Models;

namespace PANExam.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity>
        : IRepository<TEntity, Guid>
        where TEntity : EntityBase
    {
        public UniversityDbContext Context;

        public RepositoryBase()
        {
            Context = new UniversityDbContext();
        }

        public RepositoryBase(UniversityDbContext context)
        {
            Context = context;
        }

        public TEntity Get(Guid id)
        {
            return Context.Set<TEntity>().SingleOrDefault(_ => _.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().Select(_ => _);
        }

        public void Save(TEntity entity)
        {
            Context.Set<TEntity>().AddOrUpdate(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Delete(Guid id)
        {
            Context.Set<TEntity>().Remove(Get(id));
        }
    }
}
