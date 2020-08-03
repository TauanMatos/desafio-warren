using DesafioWarren.Data.Context;
using DesafioWarren.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioWarren.Data.Repository
{
    public class Repository<TEntity> where TEntity : EntityBase
    {
        protected readonly WarrenDbContext _warrenDbContext;

        public Repository(WarrenDbContext warrenDbContext)
        {
            _warrenDbContext = warrenDbContext;
        }

        protected virtual void Insert(TEntity obj)
        {
            _warrenDbContext.Set<TEntity>().Add(obj);
            _warrenDbContext.SaveChanges();
        }

        protected virtual void Update(TEntity obj)
        {
            _warrenDbContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _warrenDbContext.SaveChanges();
        }

        protected virtual void Delete(int id)
        {
            _warrenDbContext.Set<TEntity>().Remove(Select(id));
            _warrenDbContext.SaveChanges();
        }

        protected virtual IList<TEntity> Select() =>
            _warrenDbContext.Set<TEntity>().ToList();

        protected virtual TEntity Select(int id) =>
            _warrenDbContext.Set<TEntity>().Find(id);

        protected virtual void SaveChanges() => _warrenDbContext.SaveChanges();
    }
}
