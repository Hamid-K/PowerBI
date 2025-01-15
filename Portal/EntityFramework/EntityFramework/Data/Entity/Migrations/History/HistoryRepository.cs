using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020000DC RID: 220
	internal class HistoryRepository : RepositoryBase
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x000265D8 File Offset: 0x000247D8
		public HistoryRepository(InternalContext usersContext, string connectionString, DbProviderFactory providerFactory, string contextKey, int? commandTimeout, Func<DbConnection, string, HistoryContext> historyContextFactory, IEnumerable<string> schemas = null, DbContext contextForInterception = null, DatabaseExistenceState initialExistence = DatabaseExistenceState.Unknown, Func<Exception, bool> permissionDeniedDetector = null)
			: base(usersContext, connectionString, providerFactory)
		{
			this._initialExistence = initialExistence;
			this._permissionDeniedDetector = permissionDeniedDetector;
			this._commandTimeout = commandTimeout;
			this._existingTransaction = usersContext.TryGetCurrentStoreTransaction();
			this._schemas = new string[] { "dbo" }.Concat(schemas ?? Enumerable.Empty<string>()).Distinct<string>();
			this._contextForInterception = contextForInterception;
			this._historyContextFactory = historyContextFactory;
			DbConnection dbConnection = null;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					EntityType entityType = ((IObjectContextAdapter)historyContext).ObjectContext.MetadataWorkspace.GetItems<EntityType>(DataSpace.CSpace).Single((EntityType et) => et.GetClrType() == typeof(HistoryRow));
					int? num = entityType.Properties.Single((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(HistoryRepository.MigrationIdProperty)).MaxLength;
					this._migrationIdMaxLength = ((num != null) ? num.Value : 150);
					num = entityType.Properties.Single((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(HistoryRepository.ContextKeyProperty)).MaxLength;
					this._contextKeyMaxLength = ((num != null) ? num.Value : 300);
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			this._contextKey = contextKey.RestrictTo(this._contextKeyMaxLength);
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00026770 File Offset: 0x00024970
		public int ContextKeyMaxLength
		{
			get
			{
				return this._contextKeyMaxLength;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00026778 File Offset: 0x00024978
		public int MigrationIdMaxLength
		{
			get
			{
				return this._migrationIdMaxLength;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00026780 File Offset: 0x00024980
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x00026788 File Offset: 0x00024988
		public string CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
			set
			{
				this._currentSchema = value;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00026794 File Offset: 0x00024994
		public virtual XDocument GetLastModel(out string migrationId, out string productVersion, string contextKey = null)
		{
			migrationId = null;
			productVersion = null;
			if (!this.Exists(contextKey))
			{
				return null;
			}
			DbConnection dbConnection = null;
			XDocument xdocument;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						var <>f__AnonymousType = (from h in this.CreateHistoryQuery(historyContext, contextKey)
							orderby h.MigrationId descending
							select h into s
							select new { s.MigrationId, s.Model, s.ProductVersion }).FirstOrDefault();
						if (<>f__AnonymousType == null)
						{
							xdocument = null;
						}
						else
						{
							migrationId = <>f__AnonymousType.MigrationId;
							productVersion = <>f__AnonymousType.ProductVersion;
							xdocument = new ModelCompressor().Decompress(<>f__AnonymousType.Model);
						}
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return xdocument;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00026990 File Offset: 0x00024B90
		public virtual XDocument GetModel(string migrationId, out string productVersion)
		{
			productVersion = null;
			if (!this.Exists(null))
			{
				return null;
			}
			migrationId = migrationId.RestrictTo(this._migrationIdMaxLength);
			DbConnection dbConnection = null;
			XDocument xdocument;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					var <>f__AnonymousType = (from h in this.CreateHistoryQuery(historyContext, null)
						where h.MigrationId == migrationId
						select new { h.Model, h.ProductVersion }).SingleOrDefault();
					if (<>f__AnonymousType == null)
					{
						xdocument = null;
					}
					else
					{
						productVersion = <>f__AnonymousType.ProductVersion;
						xdocument = new ModelCompressor().Decompress(<>f__AnonymousType.Model);
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return xdocument;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00026B70 File Offset: 0x00024D70
		public virtual IEnumerable<string> GetPendingMigrations(IEnumerable<string> localMigrations)
		{
			if (!this.Exists(null))
			{
				return localMigrations;
			}
			DbConnection dbConnection = null;
			IEnumerable<string> enumerable2;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					List<string> list;
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						list = (from h in this.CreateHistoryQuery(historyContext, null)
							select h.MigrationId).ToList<string>();
					}
					localMigrations = localMigrations.Select((string m) => m.RestrictTo(this._migrationIdMaxLength)).ToArray<string>();
					IEnumerable<string> enumerable = localMigrations.Except(list);
					string text = list.FirstOrDefault<string>();
					string text2 = localMigrations.FirstOrDefault<string>();
					if (text != text2 && text != null && text.MigrationName() == Strings.InitialCreate && text2 != null && text2.MigrationName() == Strings.InitialCreate)
					{
						enumerable = enumerable.Skip(1);
					}
					enumerable2 = enumerable.ToList<string>();
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return enumerable2;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00026CB8 File Offset: 0x00024EB8
		public virtual IEnumerable<string> GetMigrationsSince(string migrationId)
		{
			bool flag = this.Exists(null);
			DbConnection dbConnection = null;
			IEnumerable<string> enumerable;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					IQueryable<HistoryRow> queryable = this.CreateHistoryQuery(historyContext, null);
					migrationId = migrationId.RestrictTo(this._migrationIdMaxLength);
					if (migrationId != "0")
					{
						if (!flag || !queryable.Any((HistoryRow h) => h.MigrationId == migrationId))
						{
							throw Error.MigrationNotFound(migrationId);
						}
						queryable = queryable.Where((HistoryRow h) => string.Compare(h.MigrationId, migrationId, StringComparison.Ordinal) > 0);
					}
					else if (!flag)
					{
						return Enumerable.Empty<string>();
					}
					enumerable = (from h in queryable
						orderby h.MigrationId descending
						select h.MigrationId).ToList<string>();
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return enumerable;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00026F38 File Offset: 0x00025138
		public virtual string GetMigrationId(string migrationName)
		{
			if (!this.Exists(null))
			{
				return null;
			}
			DbConnection dbConnection = null;
			string text;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					List<string> list = (from h in this.CreateHistoryQuery(historyContext, null)
						select h.MigrationId into m
						where m.Substring(16) == migrationName
						select m).ToList<string>();
					if (!list.Any<string>())
					{
						text = null;
					}
					else
					{
						if (list.Count<string>() != 1)
						{
							throw Error.AmbiguousMigrationName(migrationName);
						}
						text = list.Single<string>();
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return text;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000270B8 File Offset: 0x000252B8
		private IQueryable<HistoryRow> CreateHistoryQuery(HistoryContext context, string contextKey = null)
		{
			IQueryable<HistoryRow> queryable = context.History;
			contextKey = ((!string.IsNullOrWhiteSpace(contextKey)) ? contextKey.RestrictTo(this._contextKeyMaxLength) : this._contextKey);
			if (this._contextKeyColumnExists)
			{
				queryable = queryable.Where((HistoryRow h) => h.ContextKey == contextKey);
			}
			return queryable;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00027174 File Offset: 0x00025374
		public virtual bool IsShared()
		{
			if (!this.Exists(null) || !this._contextKeyColumnExists)
			{
				return false;
			}
			DbConnection dbConnection = null;
			bool flag;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					flag = historyContext.History.Any((HistoryRow hr) => hr.ContextKey != this._contextKey);
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return flag;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00027240 File Offset: 0x00025440
		public virtual bool HasMigrations()
		{
			if (!this.Exists(null))
			{
				return false;
			}
			if (!this._contextKeyColumnExists)
			{
				return true;
			}
			DbConnection dbConnection = null;
			bool flag;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					flag = historyContext.History.Count((HistoryRow hr) => hr.ContextKey == this._contextKey) > 0;
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return flag;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00027314 File Offset: 0x00025514
		public virtual bool Exists(string contextKey = null)
		{
			if (this._exists == null)
			{
				this._exists = new bool?(this.QueryExists(contextKey ?? this._contextKey));
			}
			return this._exists.Value;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0002734C File Offset: 0x0002554C
		private bool QueryExists(string contextKey)
		{
			if (this._initialExistence == DatabaseExistenceState.DoesNotExist)
			{
				return false;
			}
			DbConnection dbConnection = null;
			try
			{
				dbConnection = base.CreateConnection();
				if (this._initialExistence == DatabaseExistenceState.Unknown)
				{
					using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
					{
						if (!historyContext.Database.Exists())
						{
							return false;
						}
					}
				}
				foreach (string text in this._schemas.Reverse<string>())
				{
					using (HistoryContext historyContext2 = this.CreateContext(dbConnection, text))
					{
						this._currentSchema = text;
						this._contextKeyColumnExists = true;
						try
						{
							using (new TransactionScope(TransactionScopeOption.Suppress))
							{
								contextKey = contextKey.RestrictTo(this._contextKeyMaxLength);
								if (historyContext2.History.Count((HistoryRow hr) => hr.ContextKey == contextKey) > 0)
								{
									return true;
								}
							}
						}
						catch (EntityException ex)
						{
							if (this._permissionDeniedDetector != null && this._permissionDeniedDetector(ex.InnerException))
							{
								throw;
							}
							this._contextKeyColumnExists = false;
						}
						if (!this._contextKeyColumnExists)
						{
							try
							{
								using (new TransactionScope(TransactionScopeOption.Suppress))
								{
									historyContext2.History.Count<HistoryRow>();
								}
							}
							catch (EntityException ex2)
							{
								if (this._permissionDeniedDetector != null && this._permissionDeniedDetector(ex2.InnerException))
								{
									throw;
								}
								this._currentSchema = null;
							}
						}
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return !string.IsNullOrWhiteSpace(this._currentSchema);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00027610 File Offset: 0x00025810
		public virtual void ResetExists()
		{
			this._exists = null;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0002761E File Offset: 0x0002581E
		public virtual IEnumerable<MigrationOperation> GetUpgradeOperations()
		{
			if (!this.Exists(null))
			{
				yield break;
			}
			DbConnection connection = null;
			try
			{
				connection = base.CreateConnection();
				string tableName = "dbo.__MigrationHistory";
				DbProviderManifest dbProviderManifest;
				if (connection.GetProviderInfo(out dbProviderManifest).IsSqlCe())
				{
					tableName = "__MigrationHistory";
				}
				using (LegacyHistoryContext context = new LegacyHistoryContext(connection))
				{
					bool flag = false;
					try
					{
						this.InjectInterceptionContext(context);
						using (new TransactionScope(TransactionScopeOption.Suppress))
						{
							context.History.Select((LegacyHistoryRow h) => h.CreatedOn).FirstOrDefault<DateTime>();
						}
						flag = true;
					}
					catch (EntityException)
					{
					}
					if (flag)
					{
						yield return new DropColumnOperation(tableName, "CreatedOn", null);
					}
				}
				LegacyHistoryContext context = null;
				using (HistoryContext context2 = this.CreateContext(connection, null))
				{
					if (!this._contextKeyColumnExists)
					{
						if (this._historyContextFactory != HistoryContext.DefaultFactory)
						{
							throw Error.UnableToUpgradeHistoryWhenCustomFactory();
						}
						yield return new AddColumnOperation(tableName, new ColumnModel(PrimitiveTypeKind.String)
						{
							MaxLength = new int?(this._contextKeyMaxLength),
							Name = "ContextKey",
							IsNullable = new bool?(false),
							DefaultValue = this._contextKey
						}, null);
						XDocument model = new DbModelBuilder().Build(connection).GetModel();
						CreateTableOperation createTableOperation = (CreateTableOperation)new EdmModelDiffer().Diff(model, context2.GetModel(), null, null, null, null).Single<MigrationOperation>();
						DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(null)
						{
							Table = tableName,
							CreateTableOperation = createTableOperation
						};
						dropPrimaryKeyOperation.Columns.Add("MigrationId");
						yield return dropPrimaryKeyOperation;
						yield return new AlterColumnOperation(tableName, new ColumnModel(PrimitiveTypeKind.String)
						{
							MaxLength = new int?(this._migrationIdMaxLength),
							Name = "MigrationId",
							IsNullable = new bool?(false)
						}, false, null);
						AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null)
						{
							Table = tableName
						};
						addPrimaryKeyOperation.Columns.Add("MigrationId");
						addPrimaryKeyOperation.Columns.Add("ContextKey");
						yield return addPrimaryKeyOperation;
					}
				}
				HistoryContext context2 = null;
				tableName = null;
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			yield break;
			yield break;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00027630 File Offset: 0x00025830
		public virtual MigrationOperation CreateInsertOperation(string migrationId, VersionedModel versionedModel)
		{
			DbConnection dbConnection = null;
			MigrationOperation migrationOperation;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					historyContext.History.Add(new HistoryRow
					{
						MigrationId = migrationId.RestrictTo(this._migrationIdMaxLength),
						ContextKey = this._contextKey,
						Model = new ModelCompressor().Compress(versionedModel.Model),
						ProductVersion = (versionedModel.Version ?? HistoryRepository._productVersion)
					});
					using (CommandTracer commandTracer = new CommandTracer(historyContext))
					{
						historyContext.SaveChanges();
						migrationOperation = new HistoryOperation(commandTracer.CommandTrees.OfType<DbModificationCommandTree>().ToList<DbModificationCommandTree>(), null);
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return migrationOperation;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00027718 File Offset: 0x00025918
		public virtual MigrationOperation CreateDeleteOperation(string migrationId)
		{
			DbConnection dbConnection = null;
			MigrationOperation migrationOperation;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					HistoryRow historyRow = new HistoryRow
					{
						MigrationId = migrationId.RestrictTo(this._migrationIdMaxLength),
						ContextKey = this._contextKey
					};
					historyContext.History.Attach(historyRow);
					historyContext.History.Remove(historyRow);
					using (CommandTracer commandTracer = new CommandTracer(historyContext))
					{
						historyContext.SaveChanges();
						migrationOperation = new HistoryOperation(commandTracer.CommandTrees.OfType<DbModificationCommandTree>().ToList<DbModificationCommandTree>(), null);
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return migrationOperation;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000277E4 File Offset: 0x000259E4
		public virtual IEnumerable<DbQueryCommandTree> CreateDiscoveryQueryTrees()
		{
			DbConnection connection = null;
			try
			{
				connection = base.CreateConnection();
				foreach (string text in this._schemas)
				{
					using (HistoryContext context = this.CreateContext(connection, text))
					{
						IOrderedQueryable<string> orderedQueryable = from h in context.History
							where h.ContextKey == this._contextKey
							select h into s
							select s.MigrationId into s
							orderby s descending
							select s;
						DbQuery<string> dbQuery = orderedQueryable as DbQuery<string>;
						if (dbQuery != null)
						{
							dbQuery.InternalQuery.ObjectQuery.EnablePlanCaching = false;
						}
						using (CommandTracer commandTracer = new CommandTracer(context))
						{
							orderedQueryable.First<string>();
							DbQueryCommandTree dbQueryCommandTree = commandTracer.CommandTrees.OfType<DbQueryCommandTree>().Single((DbQueryCommandTree t) => t.DataSpace == DataSpace.SSpace);
							yield return new DbQueryCommandTree(dbQueryCommandTree.MetadataWorkspace, dbQueryCommandTree.DataSpace, dbQueryCommandTree.Query.Accept<DbExpression>(new HistoryRepository.ParameterInliner(commandTracer.DbCommands.Single<DbCommand>().Parameters)));
						}
						CommandTracer commandTracer = null;
					}
					HistoryContext context = null;
				}
				IEnumerator<string> enumerator = null;
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			yield break;
			yield break;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000277F4 File Offset: 0x000259F4
		public virtual void BootstrapUsingEFProviderDdl(VersionedModel versionedModel)
		{
			DbConnection dbConnection = null;
			try
			{
				dbConnection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(dbConnection, null))
				{
					historyContext.Database.ExecuteSqlCommand(((IObjectContextAdapter)historyContext).ObjectContext.CreateDatabaseScript(), new object[0]);
					historyContext.History.Add(new HistoryRow
					{
						MigrationId = MigrationAssembly.CreateMigrationId(Strings.InitialCreate).RestrictTo(this._migrationIdMaxLength),
						ContextKey = this._contextKey,
						Model = new ModelCompressor().Compress(versionedModel.Model),
						ProductVersion = (versionedModel.Version ?? HistoryRepository._productVersion)
					});
					historyContext.SaveChanges();
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000278CC File Offset: 0x00025ACC
		public HistoryContext CreateContext(DbConnection connection, string schema = null)
		{
			HistoryContext historyContext = this._historyContextFactory(connection, schema ?? this.CurrentSchema);
			historyContext.Database.CommandTimeout = this._commandTimeout;
			if (this._existingTransaction != null && this._existingTransaction.Connection == connection)
			{
				historyContext.Database.UseTransaction(this._existingTransaction);
			}
			this.InjectInterceptionContext(historyContext);
			return historyContext;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00027931 File Offset: 0x00025B31
		private void InjectInterceptionContext(DbContext context)
		{
			if (this._contextForInterception != null)
			{
				ObjectContext objectContext = context.InternalContext.ObjectContext;
				objectContext.InterceptionContext = objectContext.InterceptionContext.WithDbContext(this._contextForInterception);
			}
		}

		// Token: 0x040008B6 RID: 2230
		private static readonly string _productVersion = typeof(HistoryRepository).Assembly().GetInformationalVersion();

		// Token: 0x040008B7 RID: 2231
		public static readonly PropertyInfo MigrationIdProperty = typeof(HistoryRow).GetDeclaredProperty("MigrationId");

		// Token: 0x040008B8 RID: 2232
		public static readonly PropertyInfo ContextKeyProperty = typeof(HistoryRow).GetDeclaredProperty("ContextKey");

		// Token: 0x040008B9 RID: 2233
		private readonly string _contextKey;

		// Token: 0x040008BA RID: 2234
		private readonly int? _commandTimeout;

		// Token: 0x040008BB RID: 2235
		private readonly IEnumerable<string> _schemas;

		// Token: 0x040008BC RID: 2236
		private readonly Func<DbConnection, string, HistoryContext> _historyContextFactory;

		// Token: 0x040008BD RID: 2237
		private readonly DbContext _contextForInterception;

		// Token: 0x040008BE RID: 2238
		private readonly int _contextKeyMaxLength;

		// Token: 0x040008BF RID: 2239
		private readonly int _migrationIdMaxLength;

		// Token: 0x040008C0 RID: 2240
		private readonly DatabaseExistenceState _initialExistence;

		// Token: 0x040008C1 RID: 2241
		private readonly Func<Exception, bool> _permissionDeniedDetector;

		// Token: 0x040008C2 RID: 2242
		private readonly DbTransaction _existingTransaction;

		// Token: 0x040008C3 RID: 2243
		private string _currentSchema;

		// Token: 0x040008C4 RID: 2244
		private bool? _exists;

		// Token: 0x040008C5 RID: 2245
		private bool _contextKeyColumnExists;

		// Token: 0x020007B4 RID: 1972
		private class ParameterInliner : DefaultExpressionVisitor
		{
			// Token: 0x060057FF RID: 22527 RVA: 0x00137BDC File Offset: 0x00135DDC
			public ParameterInliner(DbParameterCollection parameters)
			{
				this._parameters = parameters;
			}

			// Token: 0x06005800 RID: 22528 RVA: 0x00137BEB File Offset: 0x00135DEB
			public override DbExpression Visit(DbParameterReferenceExpression expression)
			{
				return DbExpressionBuilder.Constant(this._parameters[expression.ParameterName].Value);
			}

			// Token: 0x06005801 RID: 22529 RVA: 0x00137C08 File Offset: 0x00135E08
			public override DbExpression Visit(DbOrExpression expression)
			{
				return expression.Left.Accept<DbExpression>(this);
			}

			// Token: 0x06005802 RID: 22530 RVA: 0x00137C16 File Offset: 0x00135E16
			public override DbExpression Visit(DbAndExpression expression)
			{
				if (expression.Right is DbNotExpression)
				{
					return expression.Left.Accept<DbExpression>(this);
				}
				return base.Visit(expression);
			}

			// Token: 0x04002100 RID: 8448
			private readonly DbParameterCollection _parameters;
		}
	}
}
