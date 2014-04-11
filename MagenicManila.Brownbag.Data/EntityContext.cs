using MagenicManila.Brownbag.Core.Extensions;
using MagenicManila.Brownbag.Data.Contracts;
using MagenicManila.Brownbag.Data.Entities;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MagenicManila.Brownbag.Data
{
    internal sealed class EntityContext : DbContext, IEntityContext
    {
        private const string Key = "__MagenicManila.Brownbag.Entity-Context__";
        private static object contextLock = new object();
        private int referenceCount;

        public void MarkPropertyAsModified<T>(T entity, Expression<Func<T, object>> propertyExpression) where T : class
        {
            var entry = this.Entry(entity);
            entry.Property(entity.GetPropertyName(propertyExpression)).IsModified = true; ;
        }

        public void SetState(object entity, EntityState state)
        {
            this.Entry(entity).State = state;
        }

        public static EntityContext GetContext()
        {
            lock (EntityContext.contextLock)
            {
                EntityContext context;
                var dictionary = Csla.ApplicationContext.LocalContext;

                if (dictionary.Contains(EntityContext.Key))
                {
                    context = (EntityContext)dictionary[EntityContext.Key];
                }
                else
                {
                    context = new EntityContext();
                    dictionary.Add(EntityContext.Key, context);
                }

                context.referenceCount++;
                return context;
            }
        }

        void IDisposable.Dispose()
        {
            lock (EntityContext.contextLock)
            {
                this.referenceCount--;

                if (this.referenceCount == 0)
                {
                    Csla.ApplicationContext.LocalContext.Remove(EntityContext.Key);
                    base.Dispose();
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Map(entity => { entity.ToTable("Product"); });
            modelBuilder.Entity<Person>().Map(entity => { entity.ToTable("Person"); });
            modelBuilder.Entity<Order>().Map(entity => { entity.ToTable("Order"); });
            base.OnModelCreating(modelBuilder);
        }

        public EntityContext()
            : base("name=EntityContext")
        {
        }

        public IDbSet<Person> People { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
    }
}