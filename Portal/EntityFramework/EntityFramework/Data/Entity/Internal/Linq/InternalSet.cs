using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000147 RID: 327
	internal class InternalSet<TEntity> : InternalQuery<TEntity>, IInternalSet<TEntity>, IInternalSet, IInternalQuery, IInternalQuery<TEntity> where TEntity : class
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x000376C5 File Offset: 0x000358C5
		public InternalSet(InternalContext internalContext)
			: base(internalContext)
		{
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000376CE File Offset: 0x000358CE
		public override void ResetQuery()
		{
			this._entitySet = null;
			this._localView = null;
			base.ResetQuery();
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000376E4 File Offset: 0x000358E4
		public TEntity Find(params object[] keyValues)
		{
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			this.InternalContext.DetectChanges(false);
			WrappedEntityKey wrappedEntityKey = new WrappedEntityKey(this.EntitySet, this.EntitySetName, keyValues, "keyValues");
			object obj = this.FindInStateManager(wrappedEntityKey) ?? this.FindInStore(wrappedEntityKey, "keyValues");
			if (obj != null && !(obj is TEntity))
			{
				throw Error.DbSet_WrongEntityTypeFound(obj.GetType().Name, typeof(TEntity).Name);
			}
			return (TEntity)((object)obj);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00037773 File Offset: 0x00035973
		public Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return this.FindInternalAsync(cancellationToken, keyValues);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0003779C File Offset: 0x0003599C
		private async Task<TEntity> FindInternalAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			this.InternalContext.DetectChanges(false);
			WrappedEntityKey wrappedEntityKey = new WrappedEntityKey(this.EntitySet, this.EntitySetName, keyValues, "keyValues");
			object obj = this.FindInStateManager(wrappedEntityKey);
			if (obj == null)
			{
				obj = await this.FindInStoreAsync(wrappedEntityKey, "keyValues", cancellationToken).WithCurrentCulture<object>();
			}
			object obj2 = obj;
			if (obj2 != null && !(obj2 is TEntity))
			{
				throw Error.DbSet_WrongEntityTypeFound(obj2.GetType().Name, typeof(TEntity).Name);
			}
			return (TEntity)((object)obj2);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000377F4 File Offset: 0x000359F4
		private object FindInStateManager(WrappedEntityKey key)
		{
			ObjectStateEntry objectStateEntry;
			if (!key.HasNullValues && this.InternalContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(key.EntityKey, out objectStateEntry))
			{
				return objectStateEntry.Entity;
			}
			object obj = null;
			foreach (ObjectStateEntry objectStateEntry2 in from e in this.InternalContext.ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added)
				where !e.IsRelationship && e.Entity != null && this.EntitySetBaseType.IsAssignableFrom(e.Entity.GetType())
				select e)
			{
				bool flag = true;
				foreach (KeyValuePair<string, object> keyValuePair in key.KeyValuePairs)
				{
					int ordinal = objectStateEntry2.CurrentValues.GetOrdinal(keyValuePair.Key);
					if (!DbHelpers.KeyValuesEqual(keyValuePair.Value, objectStateEntry2.CurrentValues.GetValue(ordinal)))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					if (obj != null)
					{
						throw Error.DbSet_MultipleAddedEntitiesFound();
					}
					obj = objectStateEntry2.Entity;
				}
			}
			return obj;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00037918 File Offset: 0x00035B18
		private object FindInStore(WrappedEntityKey key, string keyValuesParamName)
		{
			if (key.HasNullValues)
			{
				return null;
			}
			object obj;
			try
			{
				obj = this.BuildFindQuery(key).SingleOrDefault<TEntity>();
			}
			catch (EntitySqlException ex)
			{
				throw new ArgumentException(Strings.DbSet_WrongKeyValueType, keyValuesParamName, ex);
			}
			return obj;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00037964 File Offset: 0x00035B64
		private async Task<object> FindInStoreAsync(WrappedEntityKey key, string keyValuesParamName, CancellationToken cancellationToken)
		{
			object obj;
			if (key.HasNullValues)
			{
				obj = null;
			}
			else
			{
				try
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<TEntity> cultureAwaiter = this.BuildFindQuery(key).SingleOrDefaultAsync(cancellationToken).WithCurrentCulture<TEntity>()
						.GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<TEntity> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<TEntity>);
					}
					obj = cultureAwaiter.GetResult();
				}
				catch (EntitySqlException ex)
				{
					throw new ArgumentException(Strings.DbSet_WrongKeyValueType, keyValuesParamName, ex);
				}
			}
			return obj;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000379C4 File Offset: 0x00035BC4
		private ObjectQuery<TEntity> BuildFindQuery(WrappedEntityKey key)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", this.QuotedEntitySetName);
			EntityKeyMember[] entityKeyValues = key.EntityKey.EntityKeyValues;
			ObjectParameter[] array = new ObjectParameter[entityKeyValues.Length];
			for (int i = 0; i < entityKeyValues.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" AND ");
				}
				string text = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
				stringBuilder.AppendFormat("X.{0} = @{1}", DbHelpers.QuoteIdentifier(entityKeyValues[i].Key), text);
				array[i] = new ObjectParameter(text, entityKeyValues[i].Value);
			}
			return this.InternalContext.ObjectContext.CreateQuery<TEntity>(stringBuilder.ToString(), array);
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00037A88 File Offset: 0x00035C88
		public ObservableCollection<TEntity> Local
		{
			get
			{
				this.InternalContext.DetectChanges(false);
				DbLocalView<TEntity> dbLocalView;
				if ((dbLocalView = this._localView) == null)
				{
					dbLocalView = (this._localView = new DbLocalView<TEntity>(this.InternalContext));
				}
				return dbLocalView;
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00037AC0 File Offset: 0x00035CC0
		public virtual void Attach(object entity)
		{
			this.ActOnSet(delegate
			{
				this.InternalContext.ObjectContext.AttachTo(this.EntitySetName, entity);
			}, EntityState.Unchanged, entity, "Attach");
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00037B00 File Offset: 0x00035D00
		public virtual void Add(object entity)
		{
			this.ActOnSet(delegate
			{
				this.InternalContext.ObjectContext.AddObject(this.EntitySetName, entity);
			}, EntityState.Added, entity, "Add");
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00037B3F File Offset: 0x00035D3F
		public virtual void AddRange(IEnumerable entities)
		{
			this.InternalContext.DetectChanges(false);
			this.ActOnSet(delegate(object entity)
			{
				this.InternalContext.ObjectContext.AddObject(this.EntitySetName, entity);
			}, EntityState.Added, entities, "AddRange");
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00037B68 File Offset: 0x00035D68
		public virtual void Remove(object entity)
		{
			if (!(entity is TEntity))
			{
				throw Error.DbSet_BadTypeForAddAttachRemove("Remove", entity.GetType().Name, typeof(TEntity).Name);
			}
			this.InternalContext.DetectChanges(false);
			this.InternalContext.ObjectContext.DeleteObject(entity);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00037BC0 File Offset: 0x00035DC0
		public virtual void RemoveRange(IEnumerable entities)
		{
			List<object> list = entities.Cast<object>().ToList<object>();
			this.InternalContext.DetectChanges(false);
			foreach (object obj in list)
			{
				Check.NotNull<object>(obj, "entity");
				if (!(obj is TEntity))
				{
					throw Error.DbSet_BadTypeForAddAttachRemove("RemoveRange", obj.GetType().Name, typeof(TEntity).Name);
				}
				this.InternalContext.ObjectContext.DeleteObject(obj);
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00037C68 File Offset: 0x00035E68
		private void ActOnSet(Action action, EntityState newState, object entity, string methodName)
		{
			if (!(entity is TEntity))
			{
				throw Error.DbSet_BadTypeForAddAttachRemove(methodName, entity.GetType().Name, typeof(TEntity).Name);
			}
			this.InternalContext.DetectChanges(false);
			ObjectStateEntry objectStateEntry;
			if (this.InternalContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry))
			{
				objectStateEntry.ChangeState(newState);
				return;
			}
			action();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00037CD4 File Offset: 0x00035ED4
		private void ActOnSet(Action<object> action, EntityState newState, IEnumerable entities, string methodName)
		{
			foreach (object obj in entities)
			{
				Check.NotNull<object>(obj, "entity");
				if (!(obj is TEntity))
				{
					throw Error.DbSet_BadTypeForAddAttachRemove(methodName, obj.GetType().Name, typeof(TEntity).Name);
				}
				ObjectStateEntry objectStateEntry;
				if (this.InternalContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(obj, out objectStateEntry))
				{
					objectStateEntry.ChangeState(newState);
				}
				else
				{
					action(obj);
				}
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00037D7C File Offset: 0x00035F7C
		public TEntity Create()
		{
			return this.InternalContext.CreateObject<TEntity>();
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00037D8C File Offset: 0x00035F8C
		public TEntity Create(Type derivedEntityType)
		{
			if (!typeof(TEntity).IsAssignableFrom(derivedEntityType))
			{
				throw Error.DbSet_BadTypeForCreate(derivedEntityType.Name, typeof(TEntity).Name);
			}
			return (TEntity)((object)this.InternalContext.CreateObject(ObjectContextTypeCache.GetObjectType(derivedEntityType)));
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00037DDC File Offset: 0x00035FDC
		public override ObjectQuery<TEntity> ObjectQuery
		{
			get
			{
				this.Initialize();
				return base.ObjectQuery;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00037DEA File Offset: 0x00035FEA
		public string EntitySetName
		{
			get
			{
				this.Initialize();
				return this._entitySetName;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x00037DF8 File Offset: 0x00035FF8
		public string QuotedEntitySetName
		{
			get
			{
				this.Initialize();
				return this._quotedEntitySetName;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00037E06 File Offset: 0x00036006
		public EntitySet EntitySet
		{
			get
			{
				this.Initialize();
				return this._entitySet;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00037E14 File Offset: 0x00036014
		public Type EntitySetBaseType
		{
			get
			{
				this.Initialize();
				return this._baseType;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00037E24 File Offset: 0x00036024
		public virtual void Initialize()
		{
			if (this._entitySet == null)
			{
				EntitySetTypePair entitySetAndBaseTypeForType = base.InternalContext.GetEntitySetAndBaseTypeForType(typeof(TEntity));
				if (this._entitySet == null)
				{
					this.InitializeUnderlyingTypes(entitySetAndBaseTypeForType);
				}
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00037E60 File Offset: 0x00036060
		public virtual void TryInitialize()
		{
			if (this._entitySet == null)
			{
				EntitySetTypePair entitySetTypePair = base.InternalContext.TryGetEntitySetAndBaseTypeForType(typeof(TEntity));
				if (entitySetTypePair != null)
				{
					this.InitializeUnderlyingTypes(entitySetTypePair);
				}
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00037E98 File Offset: 0x00036098
		private void InitializeUnderlyingTypes(EntitySetTypePair pair)
		{
			this._entitySet = pair.EntitySet;
			this._baseType = pair.BaseType;
			this._entitySetName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				this._entitySet.EntityContainer.Name,
				this._entitySet.Name
			});
			this._quotedEntitySetName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				DbHelpers.QuoteIdentifier(this._entitySet.EntityContainer.Name),
				DbHelpers.QuoteIdentifier(this._entitySet.Name)
			});
			base.InitializeQuery(this.CreateObjectQuery(false, null, null));
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00037F58 File Offset: 0x00036158
		private ObjectQuery<TEntity> CreateObjectQuery(bool asNoTracking, bool? streaming = null, IDbExecutionStrategy executionStrategy = null)
		{
			ObjectQuery<TEntity> objectQuery = this.InternalContext.ObjectContext.CreateQuery<TEntity>(this._quotedEntitySetName, new ObjectParameter[0]);
			if (this._baseType != typeof(TEntity))
			{
				objectQuery = objectQuery.OfType<TEntity>();
			}
			if (asNoTracking)
			{
				objectQuery.MergeOption = MergeOption.NoTracking;
			}
			if (streaming != null)
			{
				objectQuery.Streaming = streaming.Value;
			}
			objectQuery.ExecutionStrategy = executionStrategy;
			return objectQuery;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00037FC8 File Offset: 0x000361C8
		public override string ToString()
		{
			this.Initialize();
			return base.ToString();
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00037FD6 File Offset: 0x000361D6
		public override string ToTraceString()
		{
			this.Initialize();
			return base.ToTraceString();
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00037FE4 File Offset: 0x000361E4
		public override InternalContext InternalContext
		{
			get
			{
				this.Initialize();
				return base.InternalContext;
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00037FF2 File Offset: 0x000361F2
		public override IInternalQuery<TEntity> Include(string path)
		{
			this.Initialize();
			return base.Include(path);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00038004 File Offset: 0x00036204
		public override IInternalQuery<TEntity> AsNoTracking()
		{
			this.Initialize();
			return new InternalQuery<TEntity>(this.InternalContext, this.CreateObjectQuery(true, null, null));
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00038033 File Offset: 0x00036233
		public override IInternalQuery<TEntity> AsStreaming()
		{
			this.Initialize();
			return new InternalQuery<TEntity>(this.InternalContext, this.CreateObjectQuery(false, new bool?(true), null));
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00038054 File Offset: 0x00036254
		public override IInternalQuery<TEntity> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			this.Initialize();
			return new InternalQuery<TEntity>(this.InternalContext, this.CreateObjectQuery(false, new bool?(false), executionStrategy));
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00038078 File Offset: 0x00036278
		public IEnumerator ExecuteSqlQuery(string sql, bool asNoTracking, bool? streaming, object[] parameters)
		{
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			this.Initialize();
			MergeOption mergeOption = (asNoTracking ? MergeOption.NoTracking : MergeOption.AppendOnly);
			return new LazyEnumerator<TEntity>(() => this.InternalContext.ObjectContext.ExecuteStoreQuery<TEntity>(sql, this.EntitySetName, new ExecutionOptions(mergeOption, streaming), parameters));
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000380E0 File Offset: 0x000362E0
		public IDbAsyncEnumerator ExecuteSqlQueryAsync(string sql, bool asNoTracking, bool? streaming, object[] parameters)
		{
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			this.Initialize();
			MergeOption mergeOption = (asNoTracking ? MergeOption.NoTracking : MergeOption.AppendOnly);
			return new LazyAsyncEnumerator<TEntity>((CancellationToken cancellationToken) => this.InternalContext.ObjectContext.ExecuteStoreQueryAsync<TEntity>(sql, this.EntitySetName, new ExecutionOptions(mergeOption, streaming), cancellationToken, parameters));
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00038147 File Offset: 0x00036347
		public override Expression Expression
		{
			get
			{
				this.Initialize();
				return base.Expression;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00038155 File Offset: 0x00036355
		public override ObjectQueryProvider ObjectQueryProvider
		{
			get
			{
				this.Initialize();
				return base.ObjectQueryProvider;
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00038163 File Offset: 0x00036363
		public override IEnumerator<TEntity> GetEnumerator()
		{
			this.Initialize();
			return base.GetEnumerator();
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00038171 File Offset: 0x00036371
		public override IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
		{
			this.Initialize();
			return base.GetAsyncEnumerator();
		}

		// Token: 0x040009D0 RID: 2512
		private DbLocalView<TEntity> _localView;

		// Token: 0x040009D1 RID: 2513
		private EntitySet _entitySet;

		// Token: 0x040009D2 RID: 2514
		private string _entitySetName;

		// Token: 0x040009D3 RID: 2515
		private string _quotedEntitySetName;

		// Token: 0x040009D4 RID: 2516
		private Type _baseType;
	}
}
