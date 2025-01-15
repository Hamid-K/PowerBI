using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F8 RID: 248
	internal class DbSetDiscoveryService
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0002FE42 File Offset: 0x0002E042
		public DbSetDiscoveryService(DbContext context)
		{
			this._context = context;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0002FE54 File Offset: 0x0002E054
		private Dictionary<Type, List<string>> GetSets()
		{
			DbContextTypesInitializersPair dbContextTypesInitializersPair;
			if (!DbSetDiscoveryService._objectSetInitializers.TryGetValue(this._context.GetType(), out dbContextTypesInitializersPair))
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(DbContext), "dbContext");
				List<Action<DbContext>> initDelegates = new List<Action<DbContext>>();
				Dictionary<Type, List<string>> dictionary = new Dictionary<Type, List<string>>();
				foreach (PropertyInfo propertyInfo in from p in this._context.GetType().GetInstanceProperties()
					where p.GetIndexParameters().Length == 0 && p.DeclaringType != typeof(DbContext)
					select p)
				{
					Type setType = DbSetDiscoveryService.GetSetType(propertyInfo.PropertyType);
					if (setType != null)
					{
						if (!setType.IsValidStructuralType())
						{
							throw Error.InvalidEntityType(setType);
						}
						List<string> list;
						if (!dictionary.TryGetValue(setType, out list))
						{
							list = new List<string>();
							dictionary[setType] = list;
						}
						list.Add(propertyInfo.Name);
						if (DbSetDiscoveryService.DbSetPropertyShouldBeInitialized(propertyInfo))
						{
							MethodInfo methodInfo = propertyInfo.Setter();
							if (methodInfo != null && methodInfo.IsPublic)
							{
								MethodInfo methodInfo2 = DbSetDiscoveryService.SetMethod.MakeGenericMethod(new Type[] { setType });
								MethodCallExpression methodCallExpression = Expression.Call(parameterExpression, methodInfo2);
								MethodCallExpression methodCallExpression2 = Expression.Call(Expression.Convert(parameterExpression, this._context.GetType()), methodInfo, new Expression[] { methodCallExpression });
								initDelegates.Add(Expression.Lambda<Action<DbContext>>(methodCallExpression2, new ParameterExpression[] { parameterExpression }).Compile());
							}
						}
					}
				}
				Action<DbContext> action = delegate(DbContext dbContext)
				{
					foreach (Action<DbContext> action2 in initDelegates)
					{
						action2(dbContext);
					}
				};
				dbContextTypesInitializersPair = new DbContextTypesInitializersPair(dictionary, action);
				DbSetDiscoveryService._objectSetInitializers.TryAdd(this._context.GetType(), dbContextTypesInitializersPair);
			}
			return dbContextTypesInitializersPair.EntityTypeToPropertyNameMap;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00030044 File Offset: 0x0002E244
		public void InitializeSets()
		{
			this.GetSets();
			DbSetDiscoveryService._objectSetInitializers[this._context.GetType()].SetsInitializer(this._context);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00030074 File Offset: 0x0002E274
		public void RegisterSets(DbModelBuilder modelBuilder)
		{
			IEnumerable<KeyValuePair<Type, List<string>>> enumerable = this.GetSets();
			if (modelBuilder.Version.IsEF6OrHigher())
			{
				enumerable = enumerable.OrderBy((KeyValuePair<Type, List<string>> s) => s.Value[0]);
			}
			foreach (KeyValuePair<Type, List<string>> keyValuePair in enumerable)
			{
				if (keyValuePair.Value.Count > 1)
				{
					throw Error.Mapping_MESTNotSupported(keyValuePair.Value[0], keyValuePair.Value[1], keyValuePair.Key);
				}
				modelBuilder.Entity(keyValuePair.Key).EntitySetName = keyValuePair.Value[0];
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00030144 File Offset: 0x0002E344
		private static bool DbSetPropertyShouldBeInitialized(PropertyInfo propertyInfo)
		{
			return !propertyInfo.GetCustomAttributes(false).Any<SuppressDbSetInitializationAttribute>() && !propertyInfo.DeclaringType.GetCustomAttributes(false).Any<SuppressDbSetInitializationAttribute>();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0003016C File Offset: 0x0002E36C
		private static Type GetSetType(Type declaredType)
		{
			if (!declaredType.IsArray)
			{
				Type setElementType = DbSetDiscoveryService.GetSetElementType(declaredType);
				if (setElementType != null)
				{
					Type type = typeof(DbSet<>).MakeGenericType(new Type[] { setElementType });
					if (declaredType.IsAssignableFrom(type))
					{
						return setElementType;
					}
				}
			}
			return null;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000301B8 File Offset: 0x0002E3B8
		private static Type GetSetElementType(Type setType)
		{
			try
			{
				Type type = ((setType.IsGenericType() && typeof(IDbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition())) ? setType : setType.GetInterface(typeof(IDbSet<>).FullName));
				if (type != null && !type.ContainsGenericParameters())
				{
					return type.GetGenericArguments()[0];
				}
			}
			catch (AmbiguousMatchException)
			{
			}
			return null;
		}

		// Token: 0x04000912 RID: 2322
		private static readonly ConcurrentDictionary<Type, DbContextTypesInitializersPair> _objectSetInitializers = new ConcurrentDictionary<Type, DbContextTypesInitializersPair>();

		// Token: 0x04000913 RID: 2323
		public static readonly MethodInfo SetMethod = typeof(DbContext).GetDeclaredMethod("Set", new Type[0]);

		// Token: 0x04000914 RID: 2324
		private readonly DbContext _context;
	}
}
