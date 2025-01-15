using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200021F RID: 543
	public class DbCompiledModel
	{
		// Token: 0x06001C83 RID: 7299 RVA: 0x00051963 File Offset: 0x0004FB63
		internal DbCompiledModel()
		{
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0005196B File Offset: 0x0004FB6B
		internal DbCompiledModel(CodeFirstCachedMetadataWorkspace workspace, DbModelBuilder cachedModelBuilder)
		{
			this._workspace = workspace;
			this._cachedModelBuilder = cachedModelBuilder;
			this._defaultSchema = cachedModelBuilder.ModelConfiguration.DefaultSchema;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00051992 File Offset: 0x0004FB92
		internal DbCompiledModel(CodeFirstCachedMetadataWorkspace workspace, string defaultSchema)
		{
			this._workspace = workspace;
			this._defaultSchema = defaultSchema;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000519A8 File Offset: 0x0004FBA8
		internal virtual DbModelBuilder CachedModelBuilder
		{
			get
			{
				return this._cachedModelBuilder;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x000519B0 File Offset: 0x0004FBB0
		internal virtual DbProviderInfo ProviderInfo
		{
			get
			{
				return this._workspace.ProviderInfo;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x000519BD File Offset: 0x0004FBBD
		internal string DefaultSchema
		{
			get
			{
				return this._defaultSchema;
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000519C8 File Offset: 0x0004FBC8
		public TContext CreateObjectContext<TContext>(DbConnection existingConnection) where TContext : ObjectContext
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			EntityConnection entityConnection = new EntityConnection(this._workspace.GetMetadataWorkspace(existingConnection), existingConnection);
			TContext tcontext = (TContext)((object)DbCompiledModel.GetConstructorDelegate<TContext>()(entityConnection));
			tcontext.ContextOwnsConnection = true;
			if (string.IsNullOrEmpty(tcontext.DefaultContainerName))
			{
				tcontext.DefaultContainerName = this._workspace.DefaultContainerName;
			}
			foreach (Assembly assembly in this._workspace.Assemblies)
			{
				tcontext.MetadataWorkspace.LoadFromAssembly(assembly);
			}
			return tcontext;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00051A8C File Offset: 0x0004FC8C
		internal static Func<EntityConnection, ObjectContext> GetConstructorDelegate<TContext>() where TContext : ObjectContext
		{
			if (typeof(TContext) == typeof(ObjectContext))
			{
				return DbCompiledModel._objectContextConstructor;
			}
			Func<EntityConnection, ObjectContext> func;
			if (!DbCompiledModel._contextConstructors.TryGetValue(typeof(TContext), out func))
			{
				ConstructorInfo declaredConstructor = typeof(TContext).GetDeclaredConstructor((ConstructorInfo c) => c.IsPublic, new Type[][]
				{
					new Type[] { typeof(EntityConnection) },
					new Type[] { typeof(DbConnection) },
					new Type[] { typeof(IDbConnection) },
					new Type[] { typeof(IDisposable) },
					new Type[] { typeof(Component) },
					new Type[] { typeof(MarshalByRefObject) },
					new Type[] { typeof(object) }
				});
				if (declaredConstructor == null)
				{
					throw Error.DbModelBuilder_MissingRequiredCtor(typeof(TContext).Name);
				}
				ParameterExpression parameterExpression;
				func = Expression.Lambda<Func<EntityConnection, ObjectContext>>(Expression.New(declaredConstructor, new Expression[] { parameterExpression }), new ParameterExpression[] { parameterExpression }).Compile();
				DbCompiledModel._contextConstructors.TryAdd(typeof(TContext), func);
			}
			return func;
		}

		// Token: 0x04000AF5 RID: 2805
		private static readonly ConcurrentDictionary<Type, Func<EntityConnection, ObjectContext>> _contextConstructors = new ConcurrentDictionary<Type, Func<EntityConnection, ObjectContext>>();

		// Token: 0x04000AF6 RID: 2806
		private static readonly Func<EntityConnection, ObjectContext> _objectContextConstructor = (EntityConnection c) => new ObjectContext(c);

		// Token: 0x04000AF7 RID: 2807
		private readonly ICachedMetadataWorkspace _workspace;

		// Token: 0x04000AF8 RID: 2808
		private readonly DbModelBuilder _cachedModelBuilder;

		// Token: 0x04000AF9 RID: 2809
		private readonly string _defaultSchema;
	}
}
