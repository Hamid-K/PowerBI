using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Internal.MockingProxies;
using System.Data.Entity.Internal.Validation;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200011D RID: 285
	internal abstract class InternalContext : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060013A8 RID: 5032 RVA: 0x00033258 File Offset: 0x00031458
		// (remove) Token: 0x060013A9 RID: 5033 RVA: 0x00033290 File Offset: 0x00031490
		public event EventHandler<EventArgs> OnDisposing;

		// Token: 0x060013AA RID: 5034 RVA: 0x000332C8 File Offset: 0x000314C8
		protected InternalContext(DbContext owner, Lazy<DbDispatchers> dispatchers = null)
		{
			this._owner = owner;
			Lazy<DbDispatchers> lazy = dispatchers;
			if (dispatchers == null)
			{
				lazy = new Lazy<DbDispatchers>(() => DbInterception.Dispatch);
			}
			this._dispatchers = lazy;
			this.AutoDetectChangesEnabled = true;
			this.ValidateOnSaveEnabled = true;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00033356 File Offset: 0x00031556
		protected InternalContext()
		{
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00033395 File Offset: 0x00031595
		public DbContext Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060013AD RID: 5037
		public abstract ObjectContext ObjectContext { get; }

		// Token: 0x060013AE RID: 5038
		public abstract ObjectContext GetObjectContextWithoutDatabaseInitialization();

		// Token: 0x060013AF RID: 5039 RVA: 0x0003339D File Offset: 0x0003159D
		public virtual ClonedObjectContext CreateObjectContextForDdlOps()
		{
			this.InitializeContext();
			return new ClonedObjectContext(new ObjectContextProxy(this.GetObjectContextWithoutDatabaseInitialization()), this.Connection, this.OriginalConnectionString, false);
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000333C2 File Offset: 0x000315C2
		protected ObjectContext TempObjectContext
		{
			get
			{
				return (this._tempObjectContext == null) ? null : this._tempObjectContext.ObjectContext;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual void UseTempObjectContext()
		{
			this._tempObjectContextCount++;
			if (this._tempObjectContext == null)
			{
				this._tempObjectContext = new ClonedObjectContext(new ObjectContextProxy(this.GetObjectContextWithoutDatabaseInitialization()), this.Connection, this.OriginalConnectionString, true);
				this.ResetDbSets();
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0003342C File Offset: 0x0003162C
		public virtual void DisposeTempObjectContext()
		{
			if (this._tempObjectContextCount > 0)
			{
				int num = this._tempObjectContextCount - 1;
				this._tempObjectContextCount = num;
				if (num == 0 && this._tempObjectContext != null)
				{
					this._tempObjectContext.Dispose();
					this._tempObjectContext = null;
					this.ResetDbSets();
				}
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00033475 File Offset: 0x00031675
		public virtual DbCompiledModel CodeFirstModel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00033478 File Offset: 0x00031678
		public virtual DbModel ModelBeingInitialized
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0003347C File Offset: 0x0003167C
		public virtual void CreateDatabase(ObjectContext objectContext, DatabaseExistenceState existenceState)
		{
			new DatabaseCreator().CreateDatabase(this, (DbMigrationsConfiguration config, DbContext context) => new DbMigrator(config, context, existenceState, true), objectContext);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000334AE File Offset: 0x000316AE
		public virtual bool CompatibleWithModel(bool throwIfNoMetadata, DatabaseExistenceState existenceState)
		{
			return new ModelCompatibilityChecker().CompatibleWithModel(this, new ModelHashCalculator(), throwIfNoMetadata, existenceState);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000334C2 File Offset: 0x000316C2
		public virtual bool ModelMatches(VersionedModel model)
		{
			return !new EdmModelDiffer().Diff(model.Model, this.Owner.GetModel(), null, null, model.Version, null).Any<MigrationOperation>();
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x000334F0 File Offset: 0x000316F0
		public virtual string QueryForModelHash()
		{
			return new EdmMetadataRepository(this, this.OriginalConnectionString, this.ProviderFactory).QueryForModelHash((DbConnection c) => new EdmMetadataContext(c));
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00033528 File Offset: 0x00031728
		public virtual VersionedModel QueryForModel(DatabaseExistenceState existenceState)
		{
			string text;
			string text2;
			XDocument lastModel = this.CreateHistoryRepository(existenceState).GetLastModel(out text, out text2, null);
			if (lastModel == null)
			{
				return null;
			}
			return new VersionedModel(lastModel, text2);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00033553 File Offset: 0x00031753
		public virtual void SaveMetadataToDatabase()
		{
			if (this.CodeFirstModel != null)
			{
				this.PerformInitializationAction(delegate
				{
					this.CreateHistoryRepository(DatabaseExistenceState.Unknown).BootstrapUsingEFProviderDdl(new VersionedModel(this.Owner.GetModel(), null));
				});
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0003356F File Offset: 0x0003176F
		public virtual bool HasHistoryTableEntry()
		{
			return this.CreateHistoryRepository(DatabaseExistenceState.Unknown).HasMigrations();
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00033580 File Offset: 0x00031780
		private HistoryRepository CreateHistoryRepository(DatabaseExistenceState existenceState = DatabaseExistenceState.Unknown)
		{
			this.DiscoverMigrationsConfiguration();
			string originalConnectionString = this.OriginalConnectionString;
			DbProviderFactory providerFactory = this.ProviderFactory;
			string contextKey = this._migrationsConfiguration().ContextKey;
			int? commandTimeout = this.CommandTimeout;
			Func<DbConnection, string, HistoryContext> historyContextFactory = this.HistoryContextFactory;
			IEnumerable<string> enumerable;
			if (this.DefaultSchema == null)
			{
				enumerable = Enumerable.Empty<string>();
			}
			else
			{
				IEnumerable<string> enumerable2 = new string[] { this.DefaultSchema };
				enumerable = enumerable2;
			}
			return new HistoryRepository(this, originalConnectionString, providerFactory, contextKey, commandTimeout, historyContextFactory, enumerable, this.Owner, existenceState, null);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000335EC File Offset: 0x000317EC
		public virtual DbTransaction TryGetCurrentStoreTransaction()
		{
			EntityTransaction currentTransaction = ((EntityConnection)this.GetObjectContextWithoutDatabaseInitialization().Connection).CurrentTransaction;
			if (currentTransaction == null)
			{
				return null;
			}
			return currentTransaction.StoreTransaction;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003361A File Offset: 0x0003181A
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x00033622 File Offset: 0x00031822
		protected bool InInitializationAction { get; set; }

		// Token: 0x060013C0 RID: 5056 RVA: 0x0003362C File Offset: 0x0003182C
		public void PerformInitializationAction(Action action)
		{
			if (this.InInitializationAction)
			{
				action();
				return;
			}
			try
			{
				this.InInitializationAction = true;
				action();
			}
			catch (DataException ex)
			{
				throw new DataException(Strings.Database_InitializationException, ex);
			}
			finally
			{
				this.InInitializationAction = false;
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0003368C File Offset: 0x0003188C
		public virtual void RegisterObjectStateManagerChangedEvent(CollectionChangeEventHandler handler)
		{
			this.ObjectContext.ObjectStateManager.ObjectStateManagerChanged += handler;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000336A0 File Offset: 0x000318A0
		public virtual bool EntityInContextAndNotDeleted(object entity)
		{
			ObjectStateEntry objectStateEntry;
			return this.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry) && objectStateEntry.State != EntityState.Deleted;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000336D0 File Offset: 0x000318D0
		public virtual int SaveChanges()
		{
			int num;
			try
			{
				if (this.ValidateOnSaveEnabled)
				{
					IEnumerable<DbEntityValidationResult> validationErrors = this.Owner.GetValidationErrors();
					if (validationErrors.Any<DbEntityValidationResult>())
					{
						throw new DbEntityValidationException(Strings.DbEntityValidationException_ValidationFailed, validationErrors);
					}
				}
				bool flag = this.AutoDetectChangesEnabled && !this.ValidateOnSaveEnabled;
				global::System.Data.Entity.Core.Objects.SaveOptions saveOptions = global::System.Data.Entity.Core.Objects.SaveOptions.AcceptAllChangesAfterSave | (flag ? global::System.Data.Entity.Core.Objects.SaveOptions.DetectChangesBeforeSave : global::System.Data.Entity.Core.Objects.SaveOptions.None);
				num = this.ObjectContext.SaveChanges(saveOptions);
			}
			catch (UpdateException ex)
			{
				throw this.WrapUpdateException(ex);
			}
			return num;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00033750 File Offset: 0x00031950
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.ValidateOnSaveEnabled)
			{
				IEnumerable<DbEntityValidationResult> validationErrors = this.Owner.GetValidationErrors();
				if (validationErrors.Any<DbEntityValidationResult>())
				{
					throw new DbEntityValidationException(Strings.DbEntityValidationException_ValidationFailed, validationErrors);
				}
			}
			TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
			bool flag = this.AutoDetectChangesEnabled && !this.ValidateOnSaveEnabled;
			global::System.Data.Entity.Core.Objects.SaveOptions saveOptions = global::System.Data.Entity.Core.Objects.SaveOptions.AcceptAllChangesAfterSave | (flag ? global::System.Data.Entity.Core.Objects.SaveOptions.DetectChangesBeforeSave : global::System.Data.Entity.Core.Objects.SaveOptions.None);
			Func<Exception, Exception> <>9__1;
			this.ObjectContext.SaveChangesAsync(saveOptions, cancellationToken).ContinueWith(delegate(Task<int> t)
			{
				if (t.IsFaulted)
				{
					IEnumerable<Exception> innerExceptions = t.Exception.InnerExceptions;
					Func<Exception, Exception> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(Exception ex)
						{
							UpdateException ex2 = ex as UpdateException;
							if (ex2 != null)
							{
								return this.WrapUpdateException(ex2);
							}
							return ex;
						});
					}
					IEnumerable<Exception> enumerable = innerExceptions.Select(func);
					tcs.TrySetException(enumerable);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				tcs.TrySetResult(t.Result);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000337F2 File Offset: 0x000319F2
		public void Initialize()
		{
			Debugger.NotifyOfCrossThreadDependency();
			this.InitializeContext();
			this.InitializeDatabase();
		}

		// Token: 0x060013C6 RID: 5062
		protected abstract void InitializeContext();

		// Token: 0x060013C7 RID: 5063
		public abstract void MarkDatabaseNotInitialized();

		// Token: 0x060013C8 RID: 5064
		protected abstract void InitializeDatabase();

		// Token: 0x060013C9 RID: 5065
		public abstract void MarkDatabaseInitialized();

		// Token: 0x060013CA RID: 5066 RVA: 0x00033808 File Offset: 0x00031A08
		public void PerformDatabaseInitialization()
		{
			object obj;
			if ((obj = DbConfiguration.DependencyResolver.GetService(typeof(IDatabaseInitializer<>).MakeGenericType(new Type[] { this.Owner.GetType() }))) == null)
			{
				obj = this.DefaultInitializer ?? new NullDatabaseInitializer<DbContext>();
			}
			object obj2 = obj;
			Action action = (Action)InternalContext.CreateInitializationActionMethod.MakeGenericMethod(new Type[] { this.Owner.GetType() }).Invoke(this, new object[] { obj2 });
			bool autoDetectChangesEnabled = this.AutoDetectChangesEnabled;
			bool validateOnSaveEnabled = this.ValidateOnSaveEnabled;
			try
			{
				if (!(this.Owner is TransactionContext))
				{
					this.UseTempObjectContext();
				}
				this.PerformInitializationAction(action);
			}
			finally
			{
				if (!(this.Owner is TransactionContext))
				{
					this.DisposeTempObjectContext();
				}
				this.AutoDetectChangesEnabled = autoDetectChangesEnabled;
				this.ValidateOnSaveEnabled = validateOnSaveEnabled;
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000338E8 File Offset: 0x00031AE8
		private Action CreateInitializationAction<TContext>(IDatabaseInitializer<TContext> initializer) where TContext : DbContext
		{
			return delegate
			{
				initializer.InitializeDatabase((TContext)((object)this.Owner));
			};
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060013CC RID: 5068
		public abstract IDatabaseInitializer<DbContext> DefaultInitializer { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060013CD RID: 5069
		// (set) Token: 0x060013CE RID: 5070
		public abstract bool EnsureTransactionsForFunctionsAndCommands { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060013CF RID: 5071
		// (set) Token: 0x060013D0 RID: 5072
		public abstract bool LazyLoadingEnabled { get; set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060013D1 RID: 5073
		// (set) Token: 0x060013D2 RID: 5074
		public abstract bool ProxyCreationEnabled { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060013D3 RID: 5075
		// (set) Token: 0x060013D4 RID: 5076
		public abstract bool UseDatabaseNullSemantics { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060013D5 RID: 5077
		// (set) Token: 0x060013D6 RID: 5078
		public abstract bool DisableFilterOverProjectionSimplificationForCustomFunctions { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060013D7 RID: 5079
		// (set) Token: 0x060013D8 RID: 5080
		public abstract int? CommandTimeout { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00033908 File Offset: 0x00031B08
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00033910 File Offset: 0x00031B10
		public bool AutoDetectChangesEnabled { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00033919 File Offset: 0x00031B19
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00033921 File Offset: 0x00031B21
		public bool ValidateOnSaveEnabled { get; set; }

		// Token: 0x060013DD RID: 5085 RVA: 0x0003392C File Offset: 0x00031B2C
		protected void LoadContextConfigs()
		{
			int? num = this.AppConfig.ContextConfigs.TryGetCommandTimeout(this.Owner.GetType());
			if (num != null)
			{
				this.CommandTimeout = new int?(num.Value);
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00033970 File Offset: 0x00031B70
		~InternalContext()
		{
			this.DisposeContext(false);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000339A0 File Offset: 0x00031BA0
		public void Dispose()
		{
			this.DisposeContext(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000339B0 File Offset: 0x00031BB0
		public virtual void DisposeContext(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing && this.OnDisposing != null)
				{
					this.OnDisposing(this, new EventArgs());
					this.OnDisposing = null;
				}
				if (this._tempObjectContext != null)
				{
					this._tempObjectContext.Dispose();
				}
				this.Log = null;
				this.IsDisposed = true;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00033A09 File Offset: 0x00031C09
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00033A11 File Offset: 0x00031C11
		public bool IsDisposed { get; private set; }

		// Token: 0x060013E3 RID: 5091 RVA: 0x00033A1A File Offset: 0x00031C1A
		public virtual void DetectChanges(bool force = false)
		{
			if (this.AutoDetectChangesEnabled || force)
			{
				this.ObjectContext.DetectChanges();
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00033A34 File Offset: 0x00031C34
		public virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			if (typeof(TEntity) != ObjectContextTypeCache.GetObjectType(typeof(TEntity)))
			{
				throw Error.CannotCallGenericSetWithProxyType();
			}
			IInternalSetAdapter internalSetAdapter;
			if (!this._genericSets.TryGetValue(typeof(TEntity), out internalSetAdapter))
			{
				IInternalSet internalSet2;
				if (!this._nonGenericSets.TryGetValue(typeof(TEntity), out internalSetAdapter))
				{
					IInternalSet internalSet = new InternalSet<TEntity>(this);
					internalSet2 = internalSet;
				}
				else
				{
					internalSet2 = internalSetAdapter.InternalSet;
				}
				internalSetAdapter = new DbSet<TEntity>((InternalSet<TEntity>)internalSet2);
				this._genericSets.Add(typeof(TEntity), internalSetAdapter);
			}
			return (IDbSet<TEntity>)internalSetAdapter;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00033AD0 File Offset: 0x00031CD0
		public virtual IInternalSetAdapter Set(Type entityType)
		{
			entityType = ObjectContextTypeCache.GetObjectType(entityType);
			IInternalSetAdapter internalSetAdapter;
			if (!this._nonGenericSets.TryGetValue(entityType, out internalSetAdapter))
			{
				internalSetAdapter = this.CreateInternalSet(entityType, this._genericSets.TryGetValue(entityType, out internalSetAdapter) ? internalSetAdapter.InternalSet : null);
				this._nonGenericSets.Add(entityType, internalSetAdapter);
			}
			return internalSetAdapter;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00033B24 File Offset: 0x00031D24
		private IInternalSetAdapter CreateInternalSet(Type entityType, IInternalSet internalSet)
		{
			Func<InternalContext, IInternalSet, IInternalSetAdapter> func;
			if (!InternalContext._setFactories.TryGetValue(entityType, out func))
			{
				if (entityType.IsValueType())
				{
					throw Error.DbSet_EntityTypeNotInModel(entityType.Name);
				}
				MethodInfo declaredMethod = typeof(InternalDbSet<>).MakeGenericType(new Type[] { entityType }).GetDeclaredMethod("Create", new Type[]
				{
					typeof(InternalContext),
					typeof(IInternalSet)
				});
				func = (Func<InternalContext, IInternalSet, IInternalSetAdapter>)Delegate.CreateDelegate(typeof(Func<InternalContext, IInternalSet, IInternalSetAdapter>), declaredMethod);
				InternalContext._setFactories.TryAdd(entityType, func);
			}
			return func(this, internalSet);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00033BC3 File Offset: 0x00031DC3
		public virtual EntitySetTypePair GetEntitySetAndBaseTypeForType(Type entityType)
		{
			this.Initialize();
			this.UpdateEntitySetMappingsForType(entityType);
			return this.GetEntitySetMappingForType(entityType);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00033BD9 File Offset: 0x00031DD9
		public virtual EntitySetTypePair TryGetEntitySetAndBaseTypeForType(Type entityType)
		{
			this.Initialize();
			if (!this.TryUpdateEntitySetMappingsForType(entityType))
			{
				return null;
			}
			return this.GetEntitySetMappingForType(entityType);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00033BF3 File Offset: 0x00031DF3
		public virtual bool IsEntityTypeMapped(Type entityType)
		{
			this.Initialize();
			return this.TryUpdateEntitySetMappingsForType(entityType);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00033C04 File Offset: 0x00031E04
		public virtual IEnumerable<TEntity> GetLocalEntities<TEntity>()
		{
			return from e in this.ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged | EntityState.Added | EntityState.Modified)
				where e.Entity is TEntity
				select (TEntity)((object)e.Entity);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00033C6C File Offset: 0x00031E6C
		public virtual IEnumerator<TElement> ExecuteSqlQuery<TElement>(string sql, bool? streaming, object[] parameters)
		{
			this.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyEnumerator<TElement>(delegate
			{
				this.Initialize();
				return this.ObjectContext.ExecuteStoreQuery<TElement>(sql, new ExecutionOptions(MergeOption.AppendOnly, streaming), parameters);
			});
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00033CBC File Offset: 0x00031EBC
		public virtual IDbAsyncEnumerator<TElement> ExecuteSqlQueryAsync<TElement>(string sql, bool? streaming, object[] parameters)
		{
			this.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyAsyncEnumerator<TElement>(delegate(CancellationToken cancellationToken)
			{
				this.Initialize();
				return this.ObjectContext.ExecuteStoreQueryAsync<TElement>(sql, new ExecutionOptions(MergeOption.AppendOnly, streaming), cancellationToken, parameters);
			});
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00033D0C File Offset: 0x00031F0C
		public virtual IEnumerator ExecuteSqlQuery(Type elementType, string sql, bool? streaming, object[] parameters)
		{
			Func<InternalContext, string, bool?, object[], IEnumerator> func;
			if (!InternalContext._queryExecutors.TryGetValue(elementType, out func))
			{
				MethodInfo methodInfo = InternalContext.ExecuteSqlQueryAsIEnumeratorMethod.MakeGenericMethod(new Type[] { elementType });
				func = (Func<InternalContext, string, bool?, object[], IEnumerator>)Delegate.CreateDelegate(typeof(Func<InternalContext, string, bool?, object[], IEnumerator>), methodInfo);
				InternalContext._queryExecutors.TryAdd(elementType, func);
			}
			return func(this, sql, streaming, parameters);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00033D6B File Offset: 0x00031F6B
		private IEnumerator ExecuteSqlQueryAsIEnumerator<TElement>(string sql, bool? streaming, object[] parameters)
		{
			return this.ExecuteSqlQuery<TElement>(sql, streaming, parameters);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00033D78 File Offset: 0x00031F78
		public virtual IDbAsyncEnumerator ExecuteSqlQueryAsync(Type elementType, string sql, bool? streaming, object[] parameters)
		{
			Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator> func;
			if (!InternalContext._asyncQueryExecutors.TryGetValue(elementType, out func))
			{
				MethodInfo methodInfo = InternalContext.ExecuteSqlQueryAsIDbAsyncEnumeratorMethod.MakeGenericMethod(new Type[] { elementType });
				func = (Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>)Delegate.CreateDelegate(typeof(Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>), methodInfo);
				InternalContext._asyncQueryExecutors.TryAdd(elementType, func);
			}
			return func(this, sql, streaming, parameters);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00033DD7 File Offset: 0x00031FD7
		private IDbAsyncEnumerator ExecuteSqlQueryAsIDbAsyncEnumerator<TElement>(string sql, bool? streaming, object[] parameters)
		{
			return this.ExecuteSqlQueryAsync<TElement>(sql, streaming, parameters);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00033DE2 File Offset: 0x00031FE2
		public virtual int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, object[] parameters)
		{
			this.Initialize();
			return this.ObjectContext.ExecuteStoreCommand(transactionalBehavior, sql, parameters);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00033DF8 File Offset: 0x00031FF8
		public virtual Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, object[] parameters)
		{
			this.Initialize();
			return this.ObjectContext.ExecuteStoreCommandAsync(transactionalBehavior, sql, cancellationToken, parameters);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00033E10 File Offset: 0x00032010
		public virtual IEntityStateEntry GetStateEntry(object entity)
		{
			this.DetectChanges(false);
			ObjectStateEntry objectStateEntry;
			if (!this.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry))
			{
				return null;
			}
			return new StateEntryAdapter(objectStateEntry);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00033E41 File Offset: 0x00032041
		public virtual IEnumerable<IEntityStateEntry> GetStateEntries()
		{
			return this.GetStateEntries((ObjectStateEntry e) => e.Entity != null);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00033E68 File Offset: 0x00032068
		public virtual IEnumerable<IEntityStateEntry> GetStateEntries<TEntity>() where TEntity : class
		{
			return this.GetStateEntries((ObjectStateEntry e) => e.Entity is TEntity);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00033E90 File Offset: 0x00032090
		private IEnumerable<IEntityStateEntry> GetStateEntries(Func<ObjectStateEntry, bool> predicate)
		{
			this.DetectChanges(false);
			return from e in this.ObjectContext.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached).Where(predicate)
				select new StateEntryAdapter(e);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00033EE0 File Offset: 0x000320E0
		public virtual DbUpdateException WrapUpdateException(UpdateException updateException)
		{
			if (updateException.StateEntries != null)
			{
				if (updateException.StateEntries.Any((ObjectStateEntry e) => e.Entity == null))
				{
					return new DbUpdateException(this, updateException, true);
				}
			}
			OptimisticConcurrencyException ex = updateException as OptimisticConcurrencyException;
			if (ex == null)
			{
				return new DbUpdateException(this, updateException, false);
			}
			return new DbUpdateConcurrencyException(this, ex);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00033F44 File Offset: 0x00032144
		public virtual TEntity CreateObject<TEntity>() where TEntity : class
		{
			return this.ObjectContext.CreateObject<TEntity>();
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00033F54 File Offset: 0x00032154
		public virtual object CreateObject(Type type)
		{
			Func<InternalContext, object> func;
			if (!InternalContext._entityFactories.TryGetValue(type, out func))
			{
				MethodInfo methodInfo = InternalContext.CreateObjectAsObjectMethod.MakeGenericMethod(new Type[] { type });
				func = (Func<InternalContext, object>)Delegate.CreateDelegate(typeof(Func<InternalContext, object>), methodInfo);
				InternalContext._entityFactories.TryAdd(type, func);
			}
			return func(this);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00033FAF File Offset: 0x000321AF
		private object CreateObjectAsObject<TEntity>() where TEntity : class
		{
			return this.CreateObject<TEntity>();
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060013FB RID: 5115
		public abstract DbConnection Connection { get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060013FC RID: 5116
		public abstract string OriginalConnectionString { get; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060013FD RID: 5117
		public abstract DbConnectionStringOrigin ConnectionStringOrigin { get; }

		// Token: 0x060013FE RID: 5118
		public abstract void OverrideConnection(IInternalConnection connection);

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00033FBC File Offset: 0x000321BC
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x00033FCA File Offset: 0x000321CA
		public virtual AppConfig AppConfig
		{
			get
			{
				this.CheckContextNotDisposed();
				return this._appConfig;
			}
			set
			{
				this.CheckContextNotDisposed();
				this._appConfig = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00033FD9 File Offset: 0x000321D9
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x00033FDC File Offset: 0x000321DC
		public virtual DbProviderInfo ModelProviderInfo
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00033FDE File Offset: 0x000321DE
		public virtual string ConnectionStringName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00033FE1 File Offset: 0x000321E1
		public virtual string ProviderName
		{
			get
			{
				return this.Connection.GetProviderInvariantName();
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00033FF0 File Offset: 0x000321F0
		public DbProviderFactory ProviderFactory
		{
			get
			{
				DbProviderFactory dbProviderFactory;
				if ((dbProviderFactory = this._providerFactory) == null)
				{
					dbProviderFactory = (this._providerFactory = DbProviderServices.GetProviderFactory(this.Connection));
				}
				return dbProviderFactory;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0003401B File Offset: 0x0003221B
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0003401E File Offset: 0x0003221E
		public virtual Action<DbModelBuilder> OnModelCreating
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00034020 File Offset: 0x00032220
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x00034028 File Offset: 0x00032228
		public bool InitializerDisabled { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00034031 File Offset: 0x00032231
		public virtual DatabaseOperations DatabaseOperations
		{
			get
			{
				return new DatabaseOperations();
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00034038 File Offset: 0x00032238
		protected void CheckContextNotDisposed()
		{
			if (this.IsDisposed)
			{
				throw Error.DbContext_Disposed();
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00034048 File Offset: 0x00032248
		protected void ResetDbSets()
		{
			foreach (IInternalSetAdapter internalSetAdapter in this._genericSets.Values.Union(this._nonGenericSets.Values))
			{
				internalSetAdapter.InternalSet.ResetQuery();
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000340AC File Offset: 0x000322AC
		public void ForceOSpaceLoadingForKnownEntityTypes()
		{
			if (!this._oSpaceLoadingForced)
			{
				this._oSpaceLoadingForced = true;
				this.Initialize();
				foreach (IInternalSetAdapter internalSetAdapter in this._genericSets.Values.Union(this._nonGenericSets.Values))
				{
					internalSetAdapter.InternalSet.TryInitialize();
				}
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00034128 File Offset: 0x00032328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool TryUpdateEntitySetMappingsForType(Type entityType)
		{
			return this.GetObjectContextWithoutDatabaseInitialization().MetadataWorkspace.MetadataOptimization.TryUpdateEntitySetMappingsForType(entityType);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00034140 File Offset: 0x00032340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private EntitySetTypePair GetEntitySetMappingForType(Type entityType)
		{
			return this.GetObjectContextWithoutDatabaseInitialization().MetadataWorkspace.MetadataOptimization.EntitySetMappingCache[entityType];
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0003415D File Offset: 0x0003235D
		private void UpdateEntitySetMappingsForType(Type entityType)
		{
			if (this.TryUpdateEntitySetMappingsForType(entityType))
			{
				return;
			}
			if (this.IsComplexType(entityType))
			{
				throw Error.DbSet_DbSetUsedWithComplexType(entityType.Name);
			}
			if (InternalContext.IsPocoTypeInNonPocoAssembly(entityType))
			{
				throw Error.DbSet_PocoAndNonPocoMixedInSameAssembly(entityType.Name);
			}
			throw Error.DbSet_EntityTypeNotInModel(entityType.Name);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0003419D File Offset: 0x0003239D
		private static bool IsPocoTypeInNonPocoAssembly(Type entityType)
		{
			return entityType.Assembly().GetCustomAttributes<EdmSchemaAttribute>().Any<EdmSchemaAttribute>() && !entityType.GetCustomAttributes(true).Any<EdmEntityTypeAttribute>();
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000341C4 File Offset: 0x000323C4
		private bool IsComplexType(Type clrType)
		{
			MetadataWorkspace metadataWorkspace = this.GetObjectContextWithoutDatabaseInitialization().MetadataWorkspace;
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);
			return metadataWorkspace.GetItems<ComplexType>(DataSpace.OSpace).Any((ComplexType t) => objectItemCollection.GetClrType(t) == clrType);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00034214 File Offset: 0x00032414
		public void ApplyContextInfo(DbContextInfo info)
		{
			if (this._contextInfo != null)
			{
				return;
			}
			this.InitializerDisabled = true;
			this._contextInfo = info;
			this._contextInfo.ConfigureContext(this.Owner);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0003423E File Offset: 0x0003243E
		public virtual ValidationProvider ValidationProvider
		{
			get
			{
				return this._validationProvider;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00034246 File Offset: 0x00032446
		public virtual string DefaultSchema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00034249 File Offset: 0x00032449
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0003425B File Offset: 0x0003245B
		public string DefaultContextKey
		{
			get
			{
				return this._defaultContextKey ?? this.OwnerShortTypeName;
			}
			set
			{
				this._defaultContextKey = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00034264 File Offset: 0x00032464
		public DbMigrationsConfiguration MigrationsConfiguration
		{
			get
			{
				this.DiscoverMigrationsConfiguration();
				return this._migrationsConfiguration();
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00034277 File Offset: 0x00032477
		public Func<DbConnection, string, HistoryContext> HistoryContextFactory
		{
			get
			{
				this.DiscoverMigrationsConfiguration();
				return this._migrationsConfiguration().GetHistoryContextFactory(this.ProviderName);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00034295 File Offset: 0x00032495
		public virtual bool MigrationsConfigurationDiscovered
		{
			get
			{
				this.DiscoverMigrationsConfiguration();
				return this._migrationsConfigurationDiscovered.Value;
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x000342A8 File Offset: 0x000324A8
		private void DiscoverMigrationsConfiguration()
		{
			if (this._migrationsConfigurationDiscovered == null)
			{
				Type contextType = this.Owner.GetType();
				DbMigrationsConfiguration discoveredConfig = new MigrationsConfigurationFinder(new TypeFinder(contextType.Assembly)).FindMigrationsConfiguration(contextType, null, null, null, null, null);
				if (discoveredConfig != null)
				{
					this._migrationsConfiguration = () => discoveredConfig;
					this._migrationsConfigurationDiscovered = new bool?(true);
					return;
				}
				Func<DbMigrationsConfiguration> <>9__2;
				this._migrationsConfiguration = delegate
				{
					Func<DbMigrationsConfiguration> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = () => new DbMigrationsConfiguration
						{
							ContextType = contextType,
							AutomaticMigrationsEnabled = true,
							MigrationsAssembly = contextType.Assembly,
							MigrationsNamespace = contextType.Namespace,
							ContextKey = this.DefaultContextKey,
							TargetDatabase = new DbConnectionInfo(this.OriginalConnectionString, this.ProviderName),
							CommandTimeout = this.CommandTimeout
						});
					}
					return new Lazy<DbMigrationsConfiguration>(func).Value;
				};
				this._migrationsConfigurationDiscovered = new bool?(false);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00034353 File Offset: 0x00032553
		internal virtual string OwnerShortTypeName
		{
			get
			{
				return this.Owner.GetType().ToString();
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00034365 File Offset: 0x00032565
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0003437C File Offset: 0x0003257C
		public virtual Action<string> Log
		{
			get
			{
				if (this._logFormatter == null)
				{
					return null;
				}
				return this._logFormatter.WriteAction;
			}
			set
			{
				if (this._logFormatter == null || this._logFormatter.WriteAction != value)
				{
					if (this._logFormatter != null)
					{
						this._dispatchers.Value.RemoveInterceptor(this._logFormatter);
						this._logFormatter = null;
					}
					if (value != null)
					{
						this._logFormatter = DbConfiguration.DependencyResolver.GetService<Func<DbContext, Action<string>, DatabaseLogFormatter>>()(this.Owner, value);
						this._dispatchers.Value.AddInterceptor(this._logFormatter);
					}
				}
			}
		}

		// Token: 0x04000960 RID: 2400
		public static readonly MethodInfo CreateObjectAsObjectMethod = typeof(InternalContext).GetOnlyDeclaredMethod("CreateObjectAsObject");

		// Token: 0x04000961 RID: 2401
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, object>> _entityFactories = new ConcurrentDictionary<Type, Func<InternalContext, object>>();

		// Token: 0x04000962 RID: 2402
		public static readonly MethodInfo ExecuteSqlQueryAsIEnumeratorMethod = typeof(InternalContext).GetOnlyDeclaredMethod("ExecuteSqlQueryAsIEnumerator");

		// Token: 0x04000963 RID: 2403
		public static readonly MethodInfo ExecuteSqlQueryAsIDbAsyncEnumeratorMethod = typeof(InternalContext).GetOnlyDeclaredMethod("ExecuteSqlQueryAsIDbAsyncEnumerator");

		// Token: 0x04000964 RID: 2404
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IEnumerator>> _queryExecutors = new ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IEnumerator>>();

		// Token: 0x04000965 RID: 2405
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>> _asyncQueryExecutors = new ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>>();

		// Token: 0x04000966 RID: 2406
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, IInternalSet, IInternalSetAdapter>> _setFactories = new ConcurrentDictionary<Type, Func<InternalContext, IInternalSet, IInternalSetAdapter>>();

		// Token: 0x04000967 RID: 2407
		public static readonly MethodInfo CreateInitializationActionMethod = typeof(InternalContext).GetOnlyDeclaredMethod("CreateInitializationAction");

		// Token: 0x04000968 RID: 2408
		private AppConfig _appConfig = AppConfig.DefaultInstance;

		// Token: 0x04000969 RID: 2409
		private readonly DbContext _owner;

		// Token: 0x0400096A RID: 2410
		private ClonedObjectContext _tempObjectContext;

		// Token: 0x0400096B RID: 2411
		private int _tempObjectContextCount;

		// Token: 0x0400096C RID: 2412
		private readonly Dictionary<Type, IInternalSetAdapter> _genericSets = new Dictionary<Type, IInternalSetAdapter>();

		// Token: 0x0400096D RID: 2413
		private readonly Dictionary<Type, IInternalSetAdapter> _nonGenericSets = new Dictionary<Type, IInternalSetAdapter>();

		// Token: 0x0400096E RID: 2414
		private readonly ValidationProvider _validationProvider = new ValidationProvider(null, DbConfiguration.DependencyResolver.GetService<AttributeProvider>());

		// Token: 0x0400096F RID: 2415
		private bool _oSpaceLoadingForced;

		// Token: 0x04000970 RID: 2416
		private DbProviderFactory _providerFactory;

		// Token: 0x04000971 RID: 2417
		private readonly Lazy<DbDispatchers> _dispatchers;

		// Token: 0x04000973 RID: 2419
		private DatabaseLogFormatter _logFormatter;

		// Token: 0x04000974 RID: 2420
		private Func<DbMigrationsConfiguration> _migrationsConfiguration;

		// Token: 0x04000975 RID: 2421
		private bool? _migrationsConfigurationDiscovered;

		// Token: 0x04000976 RID: 2422
		private DbContextInfo _contextInfo;

		// Token: 0x04000977 RID: 2423
		private string _defaultContextKey;
	}
}
