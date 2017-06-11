using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PANExam.DAL.Models;

namespace PANExam.DAL.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : EntityBase
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        void Save(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
    }
}
