using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020000A1 RID: 161
	public static class DbSetMigrationsExtensions
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		public static void AddOrUpdate<TEntity>(this IDbSet<TEntity> set, params TEntity[] entities) where TEntity : class
		{
			Check.NotNull<IDbSet<TEntity>>(set, "set");
			Check.NotNull<TEntity[]>(entities, "entities");
			DbSet<TEntity> dbSet = set as DbSet<TEntity>;
			if (dbSet != null)
			{
				InternalSet<TEntity> internalSet = (InternalSet<TEntity>)((IInternalSetAdapter)dbSet).InternalSet;
				if (internalSet != null)
				{
					dbSet.AddOrUpdate(DbSetMigrationsExtensions.GetKeyProperties<TEntity>(typeof(TEntity), internalSet), internalSet, entities);
					return;
				}
			}
			Type type = set.GetType();
			MethodInfo declaredMethod = type.GetDeclaredMethod("AddOrUpdate", new Type[] { typeof(TEntity[]) });
			if (declaredMethod == null)
			{
				throw Error.UnableToDispatchAddOrUpdate(type);
			}
			object[] array = new TEntity[][] { entities };
			declaredMethod.Invoke(set, array);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0001EE94 File Offset: 0x0001D094
		public static void AddOrUpdate<TEntity>(this IDbSet<TEntity> set, Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities) where TEntity : class
		{
			Check.NotNull<IDbSet<TEntity>>(set, "set");
			Check.NotNull<Expression<Func<TEntity, object>>>(identifierExpression, "identifierExpression");
			Check.NotNull<TEntity[]>(entities, "entities");
			DbSet<TEntity> dbSet = set as DbSet<TEntity>;
			if (dbSet != null)
			{
				InternalSet<TEntity> internalSet = (InternalSet<TEntity>)((IInternalSetAdapter)dbSet).InternalSet;
				if (internalSet != null)
				{
					IEnumerable<PropertyPath> simplePropertyAccessList = identifierExpression.GetSimplePropertyAccessList();
					dbSet.AddOrUpdate(simplePropertyAccessList, internalSet, entities);
					return;
				}
			}
			Type type = set.GetType();
			MethodInfo declaredMethod = type.GetDeclaredMethod("AddOrUpdate", new Type[]
			{
				typeof(Expression<Func<TEntity, object>>),
				typeof(TEntity[])
			});
			if (declaredMethod == null)
			{
				throw Error.UnableToDispatchAddOrUpdate(type);
			}
			declaredMethod.Invoke(set, new object[] { identifierExpression, entities });
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0001EF48 File Offset: 0x0001D148
		private static void AddOrUpdate<TEntity>(this DbSet<TEntity> set, IEnumerable<PropertyPath> identifyingProperties, InternalSet<TEntity> internalSet, params TEntity[] entities) where TEntity : class
		{
			IEnumerable<PropertyPath> keyProperties = DbSetMigrationsExtensions.GetKeyProperties<TEntity>(typeof(TEntity), internalSet);
			ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
			for (int i = 0; i < entities.Length; i++)
			{
				TEntity entity = entities[i];
				Expression expression = identifyingProperties.Select((PropertyPath pi) => Expression.Equal(Expression.Property(parameter, pi.Single<PropertyInfo>()), Expression.Constant(pi.Last<PropertyInfo>().GetValue(entity, null), pi.Last<PropertyInfo>().PropertyType))).Aggregate(null, delegate(Expression current, BinaryExpression predicate)
				{
					if (current != null)
					{
						return Expression.AndAlso(current, predicate);
					}
					return predicate;
				});
				TEntity tentity = set.SingleOrDefault(Expression.Lambda<Func<TEntity, bool>>(expression, new ParameterExpression[] { parameter }));
				if (tentity != null)
				{
					foreach (PropertyPath propertyPath in keyProperties)
					{
						propertyPath.Single<PropertyInfo>().GetPropertyInfoForSet().SetValue(entity, propertyPath.Single<PropertyInfo>().GetValue(tentity, null), null);
					}
					internalSet.InternalContext.Owner.Entry<TEntity>(tentity).CurrentValues.SetValues(entity);
				}
				else
				{
					internalSet.Add(entity);
				}
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0001F0C8 File Offset: 0x0001D2C8
		private static IEnumerable<PropertyPath> GetKeyProperties<TEntity>(Type entityType, InternalSet<TEntity> internalSet) where TEntity : class
		{
			return internalSet.InternalContext.GetEntitySetAndBaseTypeForType(typeof(TEntity)).EntitySet.ElementType.KeyMembers.Select((EdmMember km) => new PropertyPath(entityType.GetAnyProperty(km.Name)));
		}
	}
}
