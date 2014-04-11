using MagenicManila.Brownbag.Data.Entities;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace MagenicManila.Brownbag.Data.Contracts
{
    public interface IEntityContext : IObjectContextAdapter, IDisposable
    {
        IDbSet<Person> People { get; }
        IDbSet<Order> Orders { get; }
        IDbSet<Product> Products { get; }
        DbEntityEntry Entry(object entity);
        void MarkPropertyAsModified<T>(T entity, Expression<Func<T, object>> propertyExpression) where T : class;
        void SetState(object entity, EntityState state);
        int SaveChanges();
    }
}