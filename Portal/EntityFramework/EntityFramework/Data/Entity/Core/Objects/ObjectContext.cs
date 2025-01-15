using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000413 RID: 1043
	public class ObjectContext : IDisposable, IObjectContextAdapter
	{
		// Token: 0x06003153 RID: 12627 RVA: 0x0009D230 File Offset: 0x0009B430
		public ObjectContext(EntityConnection connection)
			: this(connection, true, null, null, null)
		{
			this._contextOwnsConnection = false;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x0009D244 File Offset: 0x0009B444
		public ObjectContext(EntityConnection connection, bool contextOwnsConnection)
			: this(connection, true, null, null, null)
		{
			this._contextOwnsConnection = contextOwnsConnection;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0009D258 File Offset: 0x0009B458
		public ObjectContext(string connectionString)
			: this(ObjectContext.CreateEntityConnection(connectionString), false, null, null, null)
		{
			this._contextOwnsConnection = true;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x0009D271 File Offset: 0x0009B471
		protected ObjectContext(string connectionString, string defaultContainerName)
			: this(connectionString)
		{
			this.DefaultContainerName = defaultContainerName;
			if (!string.IsNullOrEmpty(defaultContainerName))
			{
				this._disallowSettingDefaultContainerName = true;
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x0009D290 File Offset: 0x0009B490
		protected ObjectContext(EntityConnection connection, string defaultContainerName)
			: this(connection)
		{
			this.DefaultContainerName = defaultContainerName;
			if (!string.IsNullOrEmpty(defaultContainerName))
			{
				this._disallowSettingDefaultContainerName = true;
			}
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x0009D2B0 File Offset: 0x0009B4B0
		internal ObjectContext(EntityConnection connection, bool isConnectionConstructor, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory, Translator translator = null, ColumnMapFactory columnMapFactory = null)
		{
			this._options = new ObjectContextOptions();
			this._asyncMonitor = new ThrowingMonitor();
			base..ctor();
			Check.NotNull<EntityConnection>(connection, "connection");
			this._interceptionContext = new DbInterceptionContext().WithObjectContext(this);
			this._objectQueryExecutionPlanFactory = objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null);
			this._translator = translator ?? new Translator();
			this._columnMapFactory = columnMapFactory ?? new ColumnMapFactory();
			this._adapter = new EntityAdapter(this);
			this._connection = connection;
			this._connection.AssociateContext(this);
			this._connection.StateChange += this.ConnectionStateChange;
			this._entityWrapperFactory = new EntityWrapperFactory();
			string connectionString = connection.ConnectionString;
			if (connectionString == null || connectionString.Trim().Length == 0)
			{
				throw isConnectionConstructor ? new ArgumentException(Strings.ObjectContext_InvalidConnection, "connection", null) : new ArgumentException(Strings.ObjectContext_InvalidConnectionString, "connectionString", null);
			}
			try
			{
				this._workspace = this.RetrieveMetadataWorkspaceFromConnection();
			}
			catch (InvalidOperationException ex)
			{
				throw isConnectionConstructor ? new ArgumentException(Strings.ObjectContext_InvalidConnection, "connection", ex) : new ArgumentException(Strings.ObjectContext_InvalidConnectionString, "connectionString", ex);
			}
			string text = ConfigurationManager.AppSettings["EntityFramework_UseLegacyPreserveChangesBehavior"];
			bool flag = false;
			if (bool.TryParse(text, out flag))
			{
				this.ContextOptions.UseLegacyPreserveChangesBehavior = flag;
			}
			this.InitializeMappingViewCacheFactory(null);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0009D41C File Offset: 0x0009B61C
		internal ObjectContext(ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null, Translator translator = null, ColumnMapFactory columnMapFactory = null, IEntityAdapter adapter = null)
		{
			this._options = new ObjectContextOptions();
			this._asyncMonitor = new ThrowingMonitor();
			base..ctor();
			this._interceptionContext = new DbInterceptionContext().WithObjectContext(this);
			this._objectQueryExecutionPlanFactory = objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null);
			this._translator = translator ?? new Translator();
			this._columnMapFactory = columnMapFactory ?? new ColumnMapFactory();
			this._adapter = adapter ?? new EntityAdapter(this);
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x0009D499 File Offset: 0x0009B699
		public virtual DbConnection Connection
		{
			get
			{
				if (this._connection == null)
				{
					throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
				}
				return this._connection;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x0009D4B8 File Offset: 0x0009B6B8
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x0009D4E0 File Offset: 0x0009B6E0
		public virtual string DefaultContainerName
		{
			get
			{
				EntityContainer defaultContainer = this.Perspective.GetDefaultContainer();
				if (defaultContainer == null)
				{
					return string.Empty;
				}
				return defaultContainer.Name;
			}
			set
			{
				if (!this._disallowSettingDefaultContainerName)
				{
					this.Perspective.SetDefaultContainer(value);
					return;
				}
				throw new InvalidOperationException(Strings.ObjectContext_CannotSetDefaultContainerName);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x0009D501 File Offset: 0x0009B701
		public virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._workspace;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x0009D509 File Offset: 0x0009B709
		public virtual ObjectStateManager ObjectStateManager
		{
			get
			{
				if (this._objectStateManager == null)
				{
					this._objectStateManager = new ObjectStateManager(this._workspace);
				}
				return this._objectStateManager;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x0009D52A File Offset: 0x0009B72A
		internal bool ContextOwnsConnection
		{
			set
			{
				this._contextOwnsConnection = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x0009D533 File Offset: 0x0009B733
		internal ClrPerspective Perspective
		{
			get
			{
				if (this._perspective == null)
				{
					this._perspective = new ClrPerspective(this.MetadataWorkspace);
				}
				return this._perspective;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x0009D554 File Offset: 0x0009B754
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x0009D55C File Offset: 0x0009B75C
		public virtual int? CommandTimeout
		{
			get
			{
				return this._queryTimeout;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						throw new ArgumentException(Strings.ObjectContext_InvalidCommandTimeout, "value");
					}
				}
				this._queryTimeout = value;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x0009D5A1 File Offset: 0x0009B7A1
		protected internal virtual IQueryProvider QueryProvider
		{
			get
			{
				if (this._queryProvider == null)
				{
					this._queryProvider = new ObjectQueryProvider(this);
				}
				return this._queryProvider;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x0009D5BD File Offset: 0x0009B7BD
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x0009D5C5 File Offset: 0x0009B7C5
		internal bool InMaterialization { get; set; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x0009D5CE File Offset: 0x0009B7CE
		internal ThrowingMonitor AsyncMonitor
		{
			get
			{
				return this._asyncMonitor;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x0009D5D6 File Offset: 0x0009B7D6
		public virtual ObjectContextOptions ContextOptions
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x0009D5DE File Offset: 0x0009B7DE
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x0009D5E6 File Offset: 0x0009B7E6
		internal CollectionColumnMap ColumnMapBuilder { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x0009D5EF File Offset: 0x0009B7EF
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x0009D5F7 File Offset: 0x0009B7F7
		ObjectContext IObjectContextAdapter.ObjectContext
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x0009D5FA File Offset: 0x0009B7FA
		public TransactionHandler TransactionHandler
		{
			get
			{
				this.EnsureTransactionHandlerRegistered();
				return this._transactionHandler;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x0009D608 File Offset: 0x0009B808
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x0009D610 File Offset: 0x0009B810
		public DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
			internal set
			{
				this._interceptionContext = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600316F RID: 12655 RVA: 0x0009D619 File Offset: 0x0009B819
		// (remove) Token: 0x06003170 RID: 12656 RVA: 0x0009D632 File Offset: 0x0009B832
		public event EventHandler SavingChanges
		{
			add
			{
				this._onSavingChanges = (EventHandler)Delegate.Combine(this._onSavingChanges, value);
			}
			remove
			{
				this._onSavingChanges = (EventHandler)Delegate.Remove(this._onSavingChanges, value);
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x0009D64B File Offset: 0x0009B84B
		private void OnSavingChanges()
		{
			if (this._onSavingChanges != null)
			{
				this._onSavingChanges(this, new EventArgs());
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06003172 RID: 12658 RVA: 0x0009D666 File Offset: 0x0009B866
		// (remove) Token: 0x06003173 RID: 12659 RVA: 0x0009D67F File Offset: 0x0009B87F
		public event ObjectMaterializedEventHandler ObjectMaterialized
		{
			add
			{
				this._onObjectMaterialized = (ObjectMaterializedEventHandler)Delegate.Combine(this._onObjectMaterialized, value);
			}
			remove
			{
				this._onObjectMaterialized = (ObjectMaterializedEventHandler)Delegate.Remove(this._onObjectMaterialized, value);
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x0009D698 File Offset: 0x0009B898
		internal void OnObjectMaterialized(object entity)
		{
			if (this._onObjectMaterialized != null)
			{
				this._onObjectMaterialized(this, new ObjectMaterializedEventArgs(entity));
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x0009D6B4 File Offset: 0x0009B8B4
		internal bool OnMaterializedHasHandlers
		{
			get
			{
				return this._onObjectMaterialized != null && this._onObjectMaterialized.GetInvocationList().Length != 0;
			}
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x0009D6D0 File Offset: 0x0009B8D0
		public virtual void AcceptAllChanges()
		{
			if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
			foreach (ObjectStateEntry objectStateEntry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted))
			{
				objectStateEntry.AcceptChanges();
			}
			foreach (ObjectStateEntry objectStateEntry2 in this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
			{
				objectStateEntry2.AcceptChanges();
			}
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x0009D778 File Offset: 0x0009B978
		private void VerifyRootForAdd(bool doAttach, string entitySetName, IEntityWrapper wrappedEntity, EntityEntry existingEntry, out EntitySet entitySet, out bool isNoOperation)
		{
			isNoOperation = false;
			EntitySet entitySet2 = null;
			if (doAttach)
			{
				if (!string.IsNullOrEmpty(entitySetName))
				{
					entitySet2 = this.GetEntitySetFromName(entitySetName);
				}
			}
			else
			{
				entitySet2 = this.GetEntitySetFromName(entitySetName);
			}
			EntitySet entitySet3 = null;
			EntityKey entityKey = ((existingEntry != null) ? existingEntry.EntityKey : wrappedEntity.GetEntityKeyFromEntity());
			if (entityKey != null)
			{
				entitySet3 = entityKey.GetEntitySet(this.MetadataWorkspace);
				if (entitySet2 != null)
				{
					EntityUtil.ValidateEntitySetInKey(entityKey, entitySet2, "entitySetName");
				}
				entityKey.ValidateEntityKey(this._workspace, entitySet3);
			}
			entitySet = entitySet3 ?? entitySet2;
			if (entitySet == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntitySetNameOrEntityKeyRequired);
			}
			this.ValidateEntitySet(entitySet, wrappedEntity.IdentityType);
			if (doAttach && existingEntry == null)
			{
				if (entityKey == null)
				{
					entityKey = this.ObjectStateManager.CreateEntityKey(entitySet, wrappedEntity.Entity);
				}
				existingEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			}
			if (existingEntry == null || (doAttach && existingEntry.IsKeyEntry))
			{
				return;
			}
			if (existingEntry.Entity != wrappedEntity.Entity)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(wrappedEntity.IdentityType.FullName));
			}
			EntityState entityState = (doAttach ? EntityState.Unchanged : EntityState.Added);
			if (existingEntry.State != entityState)
			{
				throw doAttach ? new InvalidOperationException(Strings.ObjectContext_EntityAlreadyExistsInObjectStateManager) : new InvalidOperationException(Strings.ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(existingEntry.State));
			}
			isNoOperation = true;
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x0009D8B4 File Offset: 0x0009BAB4
		public virtual void AddObject(string entitySetName, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			EntityEntry entityEntry;
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out entityEntry);
			if (entityEntry == null)
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			}
			EntitySet entitySet;
			bool flag;
			this.VerifyRootForAdd(false, entitySetName, entityWrapper, entityEntry, out entitySet, out flag);
			if (flag)
			{
				return;
			}
			global::System.Data.Entity.Core.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
			transactionManager.BeginAddTracking();
			try
			{
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				bool flag2 = true;
				try
				{
					this.AddSingleObject(entitySet, entityWrapper, "entity");
					flag2 = false;
				}
				finally
				{
					if (flag2 && entityWrapper.Context == this)
					{
						EntityEntry entityEntry2 = this.ObjectStateManager.FindEntityEntry(entityWrapper.Entity);
						if (entityEntry2 != null && entityEntry2.EntityKey.IsTemporary)
						{
							relationshipManager.NodeVisited = true;
							RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(entityWrapper);
							RelatedEnd.RemoveEntityFromObjectStateManager(entityWrapper);
						}
					}
				}
				relationshipManager.AddRelatedEntitiesToObjectStateManager(false);
			}
			finally
			{
				transactionManager.EndAddTracking();
			}
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x0009D9AC File Offset: 0x0009BBAC
		internal void AddSingleObject(EntitySet entitySet, IEntityWrapper wrappedEntity, string argumentName)
		{
			EntityKey entityKeyFromEntity = wrappedEntity.GetEntityKeyFromEntity();
			if (entityKeyFromEntity != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKeyFromEntity, entitySet);
				entityKeyFromEntity.ValidateEntityKey(this._workspace, entitySet);
			}
			this.VerifyContextForAddOrAttach(wrappedEntity);
			wrappedEntity.Context = this;
			EntityEntry entityEntry = this.ObjectStateManager.AddEntry(wrappedEntity, null, entitySet, argumentName, true);
			this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
			wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
			entityEntry.FixupFKValuesFromNonAddedReferences();
			this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
			wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x0009DA30 File Offset: 0x0009BC30
		public virtual void LoadProperty(object entity, string navigationProperty)
		{
			this.WrapEntityAndCheckContext(entity, "property").RelationshipManager.GetRelatedEnd(navigationProperty, false).Load();
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x0009DA4F File Offset: 0x0009BC4F
		public virtual void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption)
		{
			this.WrapEntityAndCheckContext(entity, "property").RelationshipManager.GetRelatedEnd(navigationProperty, false).Load(mergeOption);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x0009DA70 File Offset: 0x0009BC70
		public virtual void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector)
		{
			bool flag;
			string text = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out flag);
			this.WrapEntityAndCheckContext(entity, "property").RelationshipManager.GetRelatedEnd(text, flag).Load();
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x0009DAA8 File Offset: 0x0009BCA8
		public virtual void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption)
		{
			bool flag;
			string text = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out flag);
			this.WrapEntityAndCheckContext(entity, "property").RelationshipManager.GetRelatedEnd(text, flag).Load(mergeOption);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x0009DAE1 File Offset: 0x0009BCE1
		private IEntityWrapper WrapEntityAndCheckContext(object entity, string refType)
		{
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(entity, this);
			if (entityWrapper.Context == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotExplicitlyLoadDetachedRelationships(refType));
			}
			if (entityWrapper.Context != this)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotLoadReferencesUsingDifferentContext(refType));
			}
			return entityWrapper;
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x0009DB1C File Offset: 0x0009BD1C
		internal static string ParsePropertySelectorExpression<TEntity>(Expression<Func<TEntity, object>> selector, out bool removedConvert)
		{
			Check.NotNull<Expression<Func<TEntity, object>>>(selector, "selector");
			removedConvert = false;
			Expression expression = selector.Body;
			while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
			{
				removedConvert = true;
				expression = ((UnaryExpression)expression).Operand;
			}
			MemberExpression memberExpression = expression as MemberExpression;
			if (memberExpression == null || !memberExpression.Member.DeclaringType.IsAssignableFrom(typeof(TEntity)) || memberExpression.Expression.NodeType != ExpressionType.Parameter)
			{
				throw new ArgumentException(Strings.ObjectContext_SelectorExpressionMustBeMemberAccess);
			}
			return memberExpression.Member.Name;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x0009DBAF File Offset: 0x0009BDAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete("Use ApplyCurrentValues instead")]
		public virtual void ApplyPropertyChanges(string entitySetName, object changed)
		{
			Check.NotNull<object>(changed, "changed");
			Check.NotEmpty(entitySetName, "entitySetName");
			this.ApplyCurrentValues<object>(entitySetName, changed);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x0009DBD4 File Offset: 0x0009BDD4
		public virtual TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class
		{
			Check.NotNull<TEntity>(currentEntity, "currentEntity");
			Check.NotEmpty(entitySetName, "entitySetName");
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(currentEntity, this);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EntityKey entityKey = entityWrapper.EntityKey;
			if (entityKey != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, entitySetFromName, "entitySetName");
				entityKey.ValidateEntityKey(this._workspace, entitySetFromName);
			}
			else
			{
				entityKey = this.ObjectStateManager.CreateEntityKey(entitySetFromName, currentEntity);
			}
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			if (entityEntry == null || entityEntry.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_EntityNotTracked);
			}
			entityEntry.ApplyCurrentValuesInternal(entityWrapper);
			return (TEntity)((object)entityEntry.Entity);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0009DC94 File Offset: 0x0009BE94
		public virtual TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class
		{
			Check.NotNull<TEntity>(originalEntity, "originalEntity");
			Check.NotEmpty(entitySetName, "entitySetName");
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(originalEntity, this);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EntityKey entityKey = entityWrapper.EntityKey;
			if (entityKey != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, entitySetFromName, "entitySetName");
				entityKey.ValidateEntityKey(this._workspace, entitySetFromName);
			}
			else
			{
				entityKey = this.ObjectStateManager.CreateEntityKey(entitySetFromName, originalEntity);
			}
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			if (entityEntry == null || entityEntry.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityNotTrackedOrHasTempKey);
			}
			if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged && entityEntry.State != EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(entityEntry.State.ToString()));
			}
			if (entityEntry.WrappedEntity.IdentityType != entityWrapper.IdentityType)
			{
				throw new ArgumentException(Strings.ObjectContext_EntitiesHaveDifferentType(entityEntry.Entity.GetType().FullName, originalEntity.GetType().FullName));
			}
			entityEntry.CompareKeyProperties(originalEntity);
			entityEntry.UpdateOriginalValues(entityWrapper.Entity);
			return (TEntity)((object)entityEntry.Entity);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x0009DDE4 File Offset: 0x0009BFE4
		public virtual void AttachTo(string entitySetName, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			EntityEntry entityEntry;
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out entityEntry);
			if (entityEntry == null)
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			}
			EntitySet entitySet;
			bool flag;
			this.VerifyRootForAdd(true, entitySetName, entityWrapper, entityEntry, out entitySet, out flag);
			if (flag)
			{
				return;
			}
			global::System.Data.Entity.Core.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
			transactionManager.BeginAttachTracking();
			try
			{
				this.ObjectStateManager.TransactionManager.OriginalMergeOption = new MergeOption?(entityWrapper.MergeOption);
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				bool flag2 = true;
				try
				{
					this.AttachSingleObject(entityWrapper, entitySet);
					flag2 = false;
				}
				finally
				{
					if (flag2 && entityWrapper.Context == this)
					{
						relationshipManager.NodeVisited = true;
						RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(entityWrapper);
						RelatedEnd.RemoveEntityFromObjectStateManager(entityWrapper);
					}
				}
				relationshipManager.AddRelatedEntitiesToObjectStateManager(true);
			}
			finally
			{
				transactionManager.EndAttachTracking();
			}
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x0009DECC File Offset: 0x0009C0CC
		public virtual void Attach(IEntityWithKey entity)
		{
			Check.NotNull<IEntityWithKey>(entity, "entity");
			if (entity.EntityKey == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotAttachEntityWithoutKey);
			}
			this.AttachTo(null, entity);
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x0009DEF8 File Offset: 0x0009C0F8
		internal void AttachSingleObject(IEntityWrapper wrappedEntity, EntitySet entitySet)
		{
			RelationshipManager relationshipManager = wrappedEntity.RelationshipManager;
			EntityKey entityKey = wrappedEntity.GetEntityKeyFromEntity();
			if (entityKey != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, entitySet);
				entityKey.ValidateEntityKey(this._workspace, entitySet);
			}
			else
			{
				entityKey = this.ObjectStateManager.CreateEntityKey(entitySet, wrappedEntity.Entity);
			}
			if (entityKey.IsTemporary)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotAttachEntityWithTemporaryKey);
			}
			if (wrappedEntity.EntityKey != entityKey)
			{
				wrappedEntity.EntityKey = entityKey;
			}
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			if (entityEntry == null)
			{
				this.VerifyContextForAddOrAttach(wrappedEntity);
				wrappedEntity.Context = this;
				entityEntry = this.ObjectStateManager.AttachEntry(entityKey, wrappedEntity, entitySet);
				this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
				wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
				this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
				wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
				relationshipManager.CheckReferentialConstraintProperties(entityEntry);
				return;
			}
			if (entityEntry.IsKeyEntry)
			{
				this.ObjectStateManager.PromoteKeyEntryInitialization(this, entityEntry, wrappedEntity, false);
				this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
				wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
				this.ObjectStateManager.PromoteKeyEntry(entityEntry, wrappedEntity, false, false, true);
				this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
				relationshipManager.CheckReferentialConstraintProperties(entityEntry);
				return;
			}
			throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(wrappedEntity.IdentityType.FullName));
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x0009E03F File Offset: 0x0009C23F
		private void VerifyContextForAddOrAttach(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.Context != this && !wrappedEntity.Context.ObjectStateManager.IsDisposed && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.Entity_EntityCantHaveMultipleChangeTrackers);
			}
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x0009E078 File Offset: 0x0009C278
		public virtual EntityKey CreateEntityKey(string entitySetName, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			Check.NotEmpty(entitySetName, "entitySetName");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(EntityUtil.GetEntityIdentityType(entity.GetType()), null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			return this.ObjectStateManager.CreateEntityKey(entitySetFromName, entity);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x0009E0CC File Offset: 0x0009C2CC
		internal EntitySet GetEntitySetFromName(string entitySetName)
		{
			string text;
			string text2;
			ObjectContext.GetEntitySetName(entitySetName, "entitySetName", this, out text, out text2);
			return this.GetEntitySet(text, text2);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x0009E0F4 File Offset: 0x0009C2F4
		private void AddRefreshKey(object entityLike, Dictionary<EntityKey, EntityEntry> entities, Dictionary<EntitySet, List<EntityKey>> currentKeys)
		{
			if (entityLike == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementIsNull(entities.Count));
			}
			EntityKey entityKey = this.EntityWrapperFactory.WrapEntityUsingContext(entityLike, this).EntityKey;
			this.RefreshCheck(entities, entityKey);
			EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
			List<EntityKey> list = null;
			if (!currentKeys.TryGetValue(entitySet, out list))
			{
				list = new List<EntityKey>();
				currentKeys.Add(entitySet, list);
			}
			list.Add(entityKey);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x0009E164 File Offset: 0x0009C364
		public virtual ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class
		{
			return new ObjectSet<TEntity>(this.GetEntitySetForType(typeof(TEntity), "TEntity"), this);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x0009E181 File Offset: 0x0009C381
		public virtual ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class
		{
			return new ObjectSet<TEntity>(this.GetEntitySetForNameAndType(entitySetName, typeof(TEntity), "TEntity"), this);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x0009E1A0 File Offset: 0x0009C3A0
		private EntitySet GetEntitySetForType(Type entityCLRType, string exceptionParameterName)
		{
			EntitySet entitySet = null;
			EntityContainer defaultContainer = this.Perspective.GetDefaultContainer();
			if (defaultContainer == null)
			{
				using (IEnumerator<EntityContainer> enumerator = this.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EntityContainer entityContainer = enumerator.Current;
						EntitySet entitySetFromContainer = this.GetEntitySetFromContainer(entityContainer, entityCLRType, exceptionParameterName);
						if (entitySetFromContainer != null)
						{
							if (entitySet != null)
							{
								throw new ArgumentException(Strings.ObjectContext_MultipleEntitySetsFoundInAllContainers(entityCLRType.FullName), exceptionParameterName);
							}
							entitySet = entitySetFromContainer;
						}
					}
					goto IL_0071;
				}
			}
			entitySet = this.GetEntitySetFromContainer(defaultContainer, entityCLRType, exceptionParameterName);
			IL_0071:
			if (entitySet == null)
			{
				throw new ArgumentException(Strings.ObjectContext_NoEntitySetFoundForType(entityCLRType.FullName), exceptionParameterName);
			}
			return entitySet;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x0009E244 File Offset: 0x0009C444
		private EntitySet GetEntitySetFromContainer(EntityContainer container, Type entityCLRType, string exceptionParameterName)
		{
			EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
			EntitySet entitySet = null;
			foreach (EntitySetBase entitySetBase in container.BaseEntitySets)
			{
				if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.EntitySet && entitySetBase.ElementType == edmType)
				{
					if (entitySet != null)
					{
						throw new ArgumentException(Strings.ObjectContext_MultipleEntitySetsFoundInSingleContainer(entityCLRType.FullName, container.Name), exceptionParameterName);
					}
					entitySet = (EntitySet)entitySetBase;
				}
			}
			return entitySet;
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x0009E2D8 File Offset: 0x0009C4D8
		private EntitySet GetEntitySetForNameAndType(string entitySetName, Type entityCLRType, string exceptionParameterName)
		{
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
			if (entitySetFromName.ElementType != edmType)
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidObjectSetTypeForEntitySet(entityCLRType.FullName, entitySetFromName.ElementType.FullName, entitySetName), exceptionParameterName);
			}
			return entitySetFromName;
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0009E324 File Offset: 0x0009C524
		internal virtual void EnsureConnection(bool shouldMonitorTransactions)
		{
			if (shouldMonitorTransactions)
			{
				this.EnsureTransactionHandlerRegistered();
			}
			if (this.Connection.State == ConnectionState.Broken)
			{
				this.Connection.Close();
			}
			if (this.Connection.State == ConnectionState.Closed)
			{
				this.Connection.Open();
				this._openedConnection = true;
			}
			if (this._openedConnection)
			{
				this._connectionRequestCount++;
			}
			try
			{
				Transaction transaction = Transaction.Current;
				this.EnsureContextIsEnlistedInCurrentTransaction<bool>(transaction, delegate
				{
					this.Connection.Open();
					return true;
				}, false);
				this._lastTransaction = transaction;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x0009E3C8 File Offset: 0x0009C5C8
		internal virtual async Task EnsureConnectionAsync(bool shouldMonitorTransactions, CancellationToken cancellationToken)
		{
			ObjectContext.<>c__DisplayClass107_0 CS$<>8__locals1 = new ObjectContext.<>c__DisplayClass107_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.cancellationToken.ThrowIfCancellationRequested();
			if (shouldMonitorTransactions)
			{
				this.EnsureTransactionHandlerRegistered();
			}
			if (this.Connection.State == ConnectionState.Broken)
			{
				this.Connection.Close();
			}
			if (this.Connection.State == ConnectionState.Closed)
			{
				await this.Connection.OpenAsync(CS$<>8__locals1.cancellationToken).WithCurrentCulture();
				this._openedConnection = true;
			}
			if (this._openedConnection)
			{
				this._connectionRequestCount++;
			}
			try
			{
				Transaction currentTransaction = Transaction.Current;
				await this.EnsureContextIsEnlistedInCurrentTransaction<Task<bool>>(currentTransaction, delegate
				{
					ObjectContext.<>c__DisplayClass107_0.<<EnsureConnectionAsync>b__0>d <<EnsureConnectionAsync>b__0>d;
					<<EnsureConnectionAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<EnsureConnectionAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
					<<EnsureConnectionAsync>b__0>d.<>1__state = -1;
					AsyncTaskMethodBuilder<bool> <>t__builder = <<EnsureConnectionAsync>b__0>d.<>t__builder;
					<>t__builder.Start<ObjectContext.<>c__DisplayClass107_0.<<EnsureConnectionAsync>b__0>d>(ref <<EnsureConnectionAsync>b__0>d);
					return <<EnsureConnectionAsync>b__0>d.<>t__builder.Task;
				}, Task.FromResult<bool>(false)).WithCurrentCulture<bool>();
				this._lastTransaction = currentTransaction;
				currentTransaction = null;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x0009E420 File Offset: 0x0009C620
		private void EnsureTransactionHandlerRegistered()
		{
			if (this._transactionHandler == null)
			{
				if (!this.InterceptionContext.DbContexts.Any((DbContext dbc) => dbc is TransactionContext))
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					string name = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderFactory).Name;
					Func<TransactionHandler> service = DbConfiguration.DependencyResolver.GetService(new ExecutionStrategyKey(name, this.Connection.DataSource));
					if (service != null)
					{
						this._transactionHandler = service();
						this._transactionHandler.Initialize(this);
					}
				}
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x0009E4C8 File Offset: 0x0009C6C8
		private T EnsureContextIsEnlistedInCurrentTransaction<T>(Transaction currentTransaction, Func<T> openConnection, T defaultValue)
		{
			if (this.Connection.State != ConnectionState.Open)
			{
				throw new InvalidOperationException(Strings.BadConnectionWrapping);
			}
			if ((null != currentTransaction && !currentTransaction.Equals(this._lastTransaction)) || (null != this._lastTransaction && !this._lastTransaction.Equals(currentTransaction)))
			{
				if (!this._openedConnection)
				{
					if (currentTransaction != null)
					{
						this.Connection.EnlistTransaction(currentTransaction);
					}
				}
				else if (this._connectionRequestCount > 1)
				{
					if (!(null == this._lastTransaction))
					{
						this.Connection.Close();
						return openConnection();
					}
					this.Connection.EnlistTransaction(currentTransaction);
				}
			}
			return defaultValue;
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x0009E581 File Offset: 0x0009C781
		private void ConnectionStateChange(object sender, StateChangeEventArgs e)
		{
			if (e.CurrentState == ConnectionState.Closed)
			{
				this._connectionRequestCount = 0;
				this._openedConnection = false;
			}
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x0009E59C File Offset: 0x0009C79C
		internal virtual void ReleaseConnection()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
			}
			if (this._openedConnection)
			{
				if (this._connectionRequestCount > 0)
				{
					this._connectionRequestCount--;
				}
				if (this._connectionRequestCount == 0)
				{
					this.Connection.Close();
					this._openedConnection = false;
				}
			}
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x0009E5F8 File Offset: 0x0009C7F8
		public virtual ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters)
		{
			Check.NotNull<string>(queryString, "queryString");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
			ObjectQuery<T> objectQuery = new ObjectQuery<T>(queryString, this, MergeOption.AppendOnly);
			foreach (ObjectParameter objectParameter in parameters)
			{
				objectQuery.Parameters.Add(objectParameter);
			}
			return objectQuery;
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x0009E661 File Offset: 0x0009C861
		private static EntityConnection CreateEntityConnection(string connectionString)
		{
			Check.NotEmpty(connectionString, "connectionString");
			return new EntityConnection(connectionString);
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x0009E675 File Offset: 0x0009C875
		private MetadataWorkspace RetrieveMetadataWorkspaceFromConnection()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
			}
			return this._connection.GetMetadataWorkspace();
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x0009E696 File Offset: 0x0009C896
		public virtual void DeleteObject(object entity)
		{
			this.DeleteObject(entity, null);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x0009E6A0 File Offset: 0x0009C8A0
		internal void DeleteObject(object entity, EntitySet expectedEntitySet)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
			if (entityEntry == null || entityEntry.Entity != entity)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotDeleteEntityNotInObjectStateManager);
			}
			if (expectedEntitySet != null)
			{
				EntitySetBase entitySet = entityEntry.EntitySet;
				if (entitySet != expectedEntitySet)
				{
					throw new InvalidOperationException(Strings.ObjectContext_EntityNotInObjectSet_Delete(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name));
				}
			}
			entityEntry.Delete();
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x0009E712 File Offset: 0x0009C912
		public virtual void Detach(object entity)
		{
			this.Detach(entity, null);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x0009E71C File Offset: 0x0009C91C
		internal void Detach(object entity, EntitySet expectedEntitySet)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
			if (entityEntry == null || entityEntry.Entity != entity || entityEntry.Entity == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotDetachEntityNotInObjectStateManager);
			}
			if (expectedEntitySet != null)
			{
				EntitySetBase entitySet = entityEntry.EntitySet;
				if (entitySet != expectedEntitySet)
				{
					throw new InvalidOperationException(Strings.ObjectContext_EntityNotInObjectSet_Detach(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name));
				}
			}
			entityEntry.Detach();
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x0009E798 File Offset: 0x0009C998
		~ObjectContext()
		{
			this.Dispose(false);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (this._transactionHandler != null)
				{
					this._transactionHandler.Dispose();
				}
				if (disposing)
				{
					if (this._connection != null)
					{
						this._connection.StateChange -= this.ConnectionStateChange;
						if (this._contextOwnsConnection)
						{
							this._connection.Dispose();
						}
					}
					this._connection = null;
					if (this._objectStateManager != null)
					{
						this._objectStateManager.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x0009E856 File Offset: 0x0009CA56
		internal bool IsDisposed
		{
			get
			{
				return this._disposed;
			}
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x0009E860 File Offset: 0x0009CA60
		internal EntitySet GetEntitySet(string entitySetName, string entityContainerName)
		{
			EntityContainer entityContainer = null;
			if (string.IsNullOrEmpty(entityContainerName))
			{
				entityContainer = this.Perspective.GetDefaultContainer();
			}
			else if (!this.MetadataWorkspace.TryGetEntityContainer(entityContainerName, DataSpace.CSpace, out entityContainer))
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityContainerNotFoundForName(entityContainerName));
			}
			EntitySet entitySet = null;
			if (!entityContainer.TryGetEntitySetByName(entitySetName, false, out entitySet))
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntitySetNotFoundForName(TypeHelpers.GetFullName(entityContainer.Name, entitySetName)));
			}
			return entitySet;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x0009E8C8 File Offset: 0x0009CAC8
		private static void GetEntitySetName(string qualifiedName, string parameterName, ObjectContext context, out string entityset, out string container)
		{
			entityset = null;
			container = null;
			Check.NotEmpty(qualifiedName, parameterName);
			string[] array = qualifiedName.Split(new char[] { '.' });
			if (array.Length > 2)
			{
				throw new ArgumentException(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
			}
			if (array.Length == 1)
			{
				entityset = array[0];
			}
			else
			{
				container = array[0];
				entityset = array[1];
				if (container == null || container.Length == 0)
				{
					throw new ArgumentException(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
				}
			}
			if (entityset == null || entityset.Length == 0)
			{
				throw new ArgumentException(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
			}
			if (context != null && string.IsNullOrEmpty(container) && context.Perspective.GetDefaultContainer() == null)
			{
				throw new ArgumentException(Strings.ObjectContext_ContainerQualifiedEntitySetNameRequired, parameterName);
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x0009E97C File Offset: 0x0009CB7C
		private void ValidateEntitySet(EntitySet entitySet, Type entityType)
		{
			TypeUsage typeUsage = this.GetTypeUsage(entityType);
			if (!entitySet.ElementType.IsAssignableFrom(typeUsage.EdmType))
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidEntitySetOnEntity(entitySet.Name, entityType), "entity");
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0009E9BC File Offset: 0x0009CBBC
		internal TypeUsage GetTypeUsage(Type entityCLRType)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityCLRType, Assembly.GetCallingAssembly());
			TypeUsage typeUsage = null;
			if (!this.Perspective.TryGetType(entityCLRType, out typeUsage) || !TypeSemantics.IsEntityType(typeUsage))
			{
				throw new InvalidOperationException(Strings.ObjectContext_NoMappingForEntityType(entityCLRType.FullName));
			}
			return typeUsage;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0009EA08 File Offset: 0x0009CC08
		public virtual object GetObjectByKey(EntityKey key)
		{
			Check.NotNull<EntityKey>(key, "key");
			EntitySet entitySet = key.GetEntitySet(this.MetadataWorkspace);
			this.MetadataWorkspace.ImplicitLoadFromEntityType(entitySet.ElementType, Assembly.GetCallingAssembly());
			object obj;
			if (!this.TryGetObjectByKey(key, out obj))
			{
				throw new ObjectNotFoundException(Strings.ObjectContext_ObjectNotFound);
			}
			return obj;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x0009EA5B File Offset: 0x0009CC5B
		public virtual void Refresh(RefreshMode refreshMode, IEnumerable collection)
		{
			Check.NotNull<IEnumerable>(collection, "collection");
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			this.RefreshEntities(refreshMode, collection);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x0009EA77 File Offset: 0x0009CC77
		public virtual void Refresh(RefreshMode refreshMode, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			this.RefreshEntities(refreshMode, new object[] { entity });
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x0009EA9C File Offset: 0x0009CC9C
		public Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection)
		{
			return this.RefreshAsync(refreshMode, collection, CancellationToken.None);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x0009EAAB File Offset: 0x0009CCAB
		public virtual Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection, CancellationToken cancellationToken)
		{
			Check.NotNull<IEnumerable>(collection, "collection");
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			return this.RefreshEntitiesAsync(refreshMode, collection, cancellationToken);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x0009EADA File Offset: 0x0009CCDA
		public Task RefreshAsync(RefreshMode refreshMode, object entity)
		{
			return this.RefreshAsync(refreshMode, entity, CancellationToken.None);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x0009EAE9 File Offset: 0x0009CCE9
		public virtual Task RefreshAsync(RefreshMode refreshMode, object entity, CancellationToken cancellationToken)
		{
			Check.NotNull<object>(entity, "entity");
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			return this.RefreshEntitiesAsync(refreshMode, new object[] { entity }, cancellationToken);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x0009EB24 File Offset: 0x0009CD24
		private void RefreshCheck(Dictionary<EntityKey, EntityEntry> entities, EntityKey key)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(key);
			if (entityEntry == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementNotInObjectStateManager(entities.Count));
			}
			if (EntityState.Added == entityEntry.State)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementInAddedState(entities.Count));
			}
			try
			{
				entities.Add(key, entityEntry);
			}
			catch (ArgumentException)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementIsDuplicate(entities.Count));
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x0009EBA8 File Offset: 0x0009CDA8
		private void RefreshEntities(RefreshMode refreshMode, IEnumerable collection)
		{
			this.AsyncMonitor.EnsureNotEntered();
			bool flag = false;
			try
			{
				Dictionary<EntityKey, EntityEntry> dictionary = new Dictionary<EntityKey, EntityEntry>(ObjectContext.RefreshEntitiesSize(collection));
				Dictionary<EntitySet, List<EntityKey>> dictionary2 = new Dictionary<EntitySet, List<EntityKey>>();
				foreach (object obj in collection)
				{
					this.AddRefreshKey(obj, dictionary, dictionary2);
				}
				if (dictionary2.Count > 0)
				{
					this.EnsureConnection(false);
					flag = true;
					foreach (EntitySet entitySet in dictionary2.Keys)
					{
						List<EntityKey> list = dictionary2[entitySet];
						for (int i = 0; i < list.Count; i = this.BatchRefreshEntitiesByKey(refreshMode, dictionary, entitySet, list, i))
						{
						}
					}
				}
				if (RefreshMode.StoreWins == refreshMode)
				{
					using (Dictionary<EntityKey, EntityEntry>.Enumerator enumerator3 = dictionary.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<EntityKey, EntityEntry> keyValuePair = enumerator3.Current;
							if (EntityState.Detached != keyValuePair.Value.State)
							{
								this.ObjectStateManager.TransactionManager.BeginDetaching();
								try
								{
									keyValuePair.Value.Delete();
								}
								finally
								{
									this.ObjectStateManager.TransactionManager.EndDetaching();
								}
								keyValuePair.Value.AcceptChanges();
							}
						}
						return;
					}
				}
				if (RefreshMode.ClientWins == refreshMode && 0 < dictionary.Count)
				{
					string text = string.Empty;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair2 in dictionary)
					{
						if (keyValuePair2.Value.State == EntityState.Deleted)
						{
							keyValuePair2.Value.AcceptChanges();
						}
						else
						{
							stringBuilder.Append(text).Append(Environment.NewLine);
							stringBuilder.Append('\'').Append(keyValuePair2.Value.WrappedEntity.IdentityType.FullName).Append('\'');
							text = ",";
						}
					}
					if (stringBuilder.Length > 0)
					{
						throw new InvalidOperationException(Strings.ObjectContext_ClientEntityRemovedFromStore(stringBuilder.ToString()));
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.ReleaseConnection();
				}
			}
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x0009EE64 File Offset: 0x0009D064
		private int BatchRefreshEntitiesByKey(RefreshMode refreshMode, Dictionary<EntityKey, EntityEntry> trackedEntities, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom)
		{
			Tuple<ObjectQueryExecutionPlan, int> queryPlanAndNextPosition = this.PrepareRefreshQuery(refreshMode, targetSet, targetKeys, startFrom);
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			Func<ObjectResult<object>> <>9__1;
			ObjectResult<object> objectResult = executionStrategy.Execute<ObjectResult<object>>(delegate
			{
				ObjectContext <>4__this = this;
				Func<ObjectResult<object>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => queryPlanAndNextPosition.Item1.Execute<object>(this, null));
				}
				return <>4__this.ExecuteInTransaction<ObjectResult<object>>(func, executionStrategy, false, true);
			});
			this.ProcessRefreshedEntities(trackedEntities, objectResult);
			return queryPlanAndNextPosition.Item2;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x0009EED4 File Offset: 0x0009D0D4
		private async Task RefreshEntitiesAsync(RefreshMode refreshMode, IEnumerable collection, CancellationToken cancellationToken)
		{
			this.AsyncMonitor.Enter();
			bool openedConnection = false;
			try
			{
				Dictionary<EntityKey, EntityEntry> entities = new Dictionary<EntityKey, EntityEntry>(ObjectContext.RefreshEntitiesSize(collection));
				Dictionary<EntitySet, List<EntityKey>> refreshKeys = new Dictionary<EntitySet, List<EntityKey>>();
				foreach (object obj in collection)
				{
					this.AddRefreshKey(obj, entities, refreshKeys);
				}
				if (refreshKeys.Count > 0)
				{
					await this.EnsureConnectionAsync(false, cancellationToken).WithCurrentCulture();
					openedConnection = true;
					foreach (EntitySet targetSet in refreshKeys.Keys)
					{
						List<EntityKey> setKeys = refreshKeys[targetSet];
						for (int i = 0; i < setKeys.Count; i = await this.BatchRefreshEntitiesByKeyAsync(refreshMode, entities, targetSet, setKeys, i, cancellationToken).WithCurrentCulture<int>())
						{
						}
						setKeys = null;
						targetSet = null;
					}
					Dictionary<EntitySet, List<EntityKey>>.KeyCollection.Enumerator enumerator2 = default(Dictionary<EntitySet, List<EntityKey>>.KeyCollection.Enumerator);
				}
				if (RefreshMode.StoreWins == refreshMode)
				{
					using (Dictionary<EntityKey, EntityEntry>.Enumerator enumerator3 = entities.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<EntityKey, EntityEntry> keyValuePair = enumerator3.Current;
							if (EntityState.Detached != keyValuePair.Value.State)
							{
								this.ObjectStateManager.TransactionManager.BeginDetaching();
								try
								{
									keyValuePair.Value.Delete();
								}
								finally
								{
									this.ObjectStateManager.TransactionManager.EndDetaching();
								}
								keyValuePair.Value.AcceptChanges();
							}
						}
						goto IL_03C5;
					}
				}
				if (RefreshMode.ClientWins == refreshMode && 0 < entities.Count)
				{
					string text = string.Empty;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair2 in entities)
					{
						if (keyValuePair2.Value.State == EntityState.Deleted)
						{
							keyValuePair2.Value.AcceptChanges();
						}
						else
						{
							stringBuilder.Append(text).Append(Environment.NewLine);
							stringBuilder.Append('\'').Append(keyValuePair2.Value.WrappedEntity.IdentityType.FullName).Append('\'');
							text = ",";
						}
					}
					if (stringBuilder.Length > 0)
					{
						throw new InvalidOperationException(Strings.ObjectContext_ClientEntityRemovedFromStore(stringBuilder.ToString()));
					}
				}
				IL_03C5:
				entities = null;
				refreshKeys = null;
			}
			finally
			{
				if (openedConnection)
				{
					this.ReleaseConnection();
				}
				this.AsyncMonitor.Exit();
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x0009EF34 File Offset: 0x0009D134
		private async Task<int> BatchRefreshEntitiesByKeyAsync(RefreshMode refreshMode, Dictionary<EntityKey, EntityEntry> trackedEntities, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom, CancellationToken cancellationToken)
		{
			Tuple<ObjectQueryExecutionPlan, int> queryPlanAndNextPosition = this.PrepareRefreshQuery(refreshMode, targetSet, targetKeys, startFrom);
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			Func<Task<ObjectResult<object>>> <>9__1;
			ObjectResult<object> objectResult = await executionStrategy.ExecuteAsync<ObjectResult<object>>(delegate
			{
				ObjectContext <>4__this = this;
				Func<Task<ObjectResult<object>>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => queryPlanAndNextPosition.Item1.ExecuteAsync<object>(this, null, cancellationToken));
				}
				return <>4__this.ExecuteInTransactionAsync<ObjectResult<object>>(func, executionStrategy, false, true, cancellationToken);
			}, cancellationToken).WithCurrentCulture<ObjectResult<object>>();
			this.ProcessRefreshedEntities(trackedEntities, objectResult);
			return queryPlanAndNextPosition.Item2;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x0009EFAC File Offset: 0x0009D1AC
		internal virtual Tuple<ObjectQueryExecutionPlan, int> PrepareRefreshQuery(RefreshMode refreshMode, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom)
		{
			DbExpressionBinding dbExpressionBinding = targetSet.Scan().BindAs("EntitySet");
			DbExpression refKey = dbExpressionBinding.Variable.GetEntityRef().GetRefKey();
			int num = Math.Min(250, targetKeys.Count - startFrom);
			DbExpression[] array = new DbExpression[num];
			for (int i = 0; i < num; i++)
			{
				DbExpression dbExpression = DbExpressionBuilder.NewRow(targetKeys[startFrom++].GetKeyValueExpressions(targetSet));
				array[i] = refKey.Equal(dbExpression);
			}
			DbExpression dbExpression2 = Helpers.BuildBalancedTreeInPlace<DbExpression>(array, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
			DbExpression dbExpression3 = dbExpressionBinding.Filter(dbExpression2);
			DbQueryCommandTree dbQueryCommandTree = DbQueryCommandTree.FromValidExpression(this.MetadataWorkspace, DataSpace.CSpace, dbExpression3, true, false);
			MergeOption mergeOption = ((RefreshMode.StoreWins == refreshMode) ? MergeOption.OverwriteChanges : MergeOption.PreserveChanges);
			return new Tuple<ObjectQueryExecutionPlan, int>(this._objectQueryExecutionPlanFactory.Prepare(this, dbQueryCommandTree, typeof(object), mergeOption, false, null, null, DbExpressionBuilder.AliasGenerator), startFrom);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x0009F090 File Offset: 0x0009D290
		private void ProcessRefreshedEntities(Dictionary<EntityKey, EntityEntry> trackedEntities, ObjectResult<object> results)
		{
			foreach (object obj in results)
			{
				EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(obj);
				if (entityEntry != null && entityEntry.State == EntityState.Modified)
				{
					entityEntry.SetModifiedAll();
				}
				EntityKey entityKey = this.EntityWrapperFactory.WrapEntityUsingContext(obj, this).EntityKey;
				if (entityKey == null)
				{
					throw Error.EntityKey_UnexpectedNull();
				}
				if (!trackedEntities.Remove(entityKey))
				{
					throw new InvalidOperationException(Strings.ObjectContext_StoreEntityNotPresentInClient);
				}
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0009F124 File Offset: 0x0009D324
		private static int RefreshEntitiesSize(IEnumerable collection)
		{
			ICollection collection2 = collection as ICollection;
			if (collection2 == null)
			{
				return 0;
			}
			return collection2.Count;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x0009F143 File Offset: 0x0009D343
		public virtual int SaveChanges()
		{
			return this.SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x0009F14C File Offset: 0x0009D34C
		public virtual Task<int> SaveChangesAsync()
		{
			return this.SaveChangesAsync(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave, CancellationToken.None);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x0009F15A File Offset: 0x0009D35A
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return this.SaveChangesAsync(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave, cancellationToken);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x0009F164 File Offset: 0x0009D364
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete("Use SaveChanges(SaveOptions options) instead.")]
		public virtual int SaveChanges(bool acceptChangesDuringSave)
		{
			return this.SaveChanges(acceptChangesDuringSave ? (SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave) : SaveOptions.DetectChangesBeforeSave);
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0009F173 File Offset: 0x0009D373
		public virtual int SaveChanges(SaveOptions options)
		{
			return this.SaveChangesInternal(options, false);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x0009F180 File Offset: 0x0009D380
		internal int SaveChangesInternal(SaveOptions options, bool executeInExistingTransaction)
		{
			this.AsyncMonitor.EnsureNotEntered();
			this.PrepareToSaveChanges(options);
			int num = 0;
			if (this.ObjectStateManager.HasChanges())
			{
				if (executeInExistingTransaction)
				{
					num = this.SaveChangesToStore(options, null, false);
				}
				else
				{
					IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
					num = executionStrategy.Execute<int>(() => this.SaveChangesToStore(options, executionStrategy, true));
				}
			}
			return num;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x0009F20B File Offset: 0x0009D40B
		public virtual Task<int> SaveChangesAsync(SaveOptions options)
		{
			return this.SaveChangesAsync(options, CancellationToken.None);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0009F219 File Offset: 0x0009D419
		public virtual Task<int> SaveChangesAsync(SaveOptions options, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			return this.SaveChangesInternalAsync(options, false, cancellationToken);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x0009F238 File Offset: 0x0009D438
		internal async Task<int> SaveChangesInternalAsync(SaveOptions options, bool executeInExistingTransaction, CancellationToken cancellationToken)
		{
			this.AsyncMonitor.Enter();
			int num2;
			try
			{
				this.PrepareToSaveChanges(options);
				int num = 0;
				if (this.ObjectStateManager.HasChanges())
				{
					if (executeInExistingTransaction)
					{
						num = await this.SaveChangesToStoreAsync(options, null, false, cancellationToken).WithCurrentCulture<int>();
					}
					else
					{
						IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
						num = await executionStrategy.ExecuteAsync<int>(() => this.SaveChangesToStoreAsync(options, executionStrategy, true, cancellationToken), cancellationToken).WithCurrentCulture<int>();
					}
				}
				num2 = num;
			}
			finally
			{
				this.AsyncMonitor.Exit();
			}
			return num2;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x0009F298 File Offset: 0x0009D498
		private void PrepareToSaveChanges(SaveOptions options)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
			}
			this.OnSavingChanges();
			if ((SaveOptions.DetectChangesBeforeSave & options) != SaveOptions.None)
			{
				this.ObjectStateManager.DetectChanges();
			}
			if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x0009F2E8 File Offset: 0x0009D4E8
		private int SaveChangesToStore(SaveOptions options, IDbExecutionStrategy executionStrategy, bool startLocalTransaction)
		{
			this._adapter.AcceptChangesDuringUpdate = false;
			this._adapter.Connection = this.Connection;
			this._adapter.CommandTimeout = this.CommandTimeout;
			int num = this.ExecuteInTransaction<int>(() => this._adapter.Update(), executionStrategy, startLocalTransaction, true);
			if ((SaveOptions.AcceptAllChangesAfterSave & options) != SaveOptions.None)
			{
				try
				{
					this.AcceptAllChanges();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Strings.ObjectContext_AcceptAllChangesFailure(ex.Message), ex);
				}
			}
			return num;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x0009F36C File Offset: 0x0009D56C
		private async Task<int> SaveChangesToStoreAsync(SaveOptions options, IDbExecutionStrategy executionStrategy, bool startLocalTransaction, CancellationToken cancellationToken)
		{
			this._adapter.AcceptChangesDuringUpdate = false;
			this._adapter.Connection = this.Connection;
			this._adapter.CommandTimeout = this.CommandTimeout;
			int num = await this.ExecuteInTransactionAsync<int>(() => this._adapter.UpdateAsync(cancellationToken), executionStrategy, startLocalTransaction, true, cancellationToken).WithCurrentCulture<int>();
			if ((SaveOptions.AcceptAllChangesAfterSave & options) != SaveOptions.None)
			{
				try
				{
					this.AcceptAllChanges();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Strings.ObjectContext_AcceptAllChangesFailure(ex.Message), ex);
				}
			}
			return num;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x0009F3D4 File Offset: 0x0009D5D4
		internal virtual T ExecuteInTransaction<T>(Func<T> func, IDbExecutionStrategy executionStrategy, bool startLocalTransaction, bool releaseConnectionOnSuccess)
		{
			this.EnsureConnection(startLocalTransaction);
			bool flag = false;
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			if (entityConnection.CurrentTransaction == null && !entityConnection.EnlistedInUserTransaction && this._lastTransaction == null)
			{
				flag = startLocalTransaction;
			}
			else if (executionStrategy != null && executionStrategy.RetriesOnFailure)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_ExistingTransaction(executionStrategy.GetType().Name));
			}
			DbTransaction dbTransaction = null;
			T t2;
			try
			{
				if (flag)
				{
					dbTransaction = entityConnection.BeginTransaction();
				}
				T t = func();
				if (dbTransaction != null)
				{
					dbTransaction.Commit();
				}
				if (releaseConnectionOnSuccess)
				{
					this.ReleaseConnection();
				}
				t2 = t;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
			finally
			{
				if (dbTransaction != null)
				{
					dbTransaction.Dispose();
				}
			}
			return t2;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x0009F494 File Offset: 0x0009D694
		internal virtual async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> func, IDbExecutionStrategy executionStrategy, bool startLocalTransaction, bool releaseConnectionOnSuccess, CancellationToken cancellationToken)
		{
			await this.EnsureConnectionAsync(startLocalTransaction, cancellationToken).WithCurrentCulture();
			bool flag = false;
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			if (entityConnection.CurrentTransaction == null && !entityConnection.EnlistedInUserTransaction && this._lastTransaction == null)
			{
				flag = startLocalTransaction;
			}
			else if (executionStrategy.RetriesOnFailure)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_ExistingTransaction(executionStrategy.GetType().Name));
			}
			DbTransaction localTransaction = null;
			T t2;
			try
			{
				if (flag)
				{
					localTransaction = entityConnection.BeginTransaction();
				}
				T t = await func().WithCurrentCulture<T>();
				if (localTransaction != null)
				{
					localTransaction.Commit();
				}
				if (releaseConnectionOnSuccess)
				{
					this.ReleaseConnection();
				}
				t2 = t;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
			finally
			{
				if (localTransaction != null)
				{
					localTransaction.Dispose();
				}
			}
			return t2;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x0009F503 File Offset: 0x0009D703
		public virtual void DetectChanges()
		{
			this.ObjectStateManager.DetectChanges();
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x0009F510 File Offset: 0x0009D710
		public virtual bool TryGetObjectByKey(EntityKey key, out object value)
		{
			EntityEntry entityEntry;
			this.ObjectStateManager.TryGetEntityEntry(key, out entityEntry);
			if (entityEntry != null && !entityEntry.IsKeyEntry)
			{
				value = entityEntry.Entity;
				return value != null;
			}
			if (key.IsTemporary)
			{
				value = null;
				return false;
			}
			EntitySet entitySet = key.GetEntitySet(this.MetadataWorkspace);
			key.ValidateEntityKey(this._workspace, entitySet, true, "key");
			this.MetadataWorkspace.ImplicitLoadFromEntityType(entitySet.ElementType, Assembly.GetCallingAssembly());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT VALUE X FROM {0}.{1} AS X WHERE ", EntityUtil.QuoteIdentifier(entitySet.EntityContainer.Name), EntityUtil.QuoteIdentifier(entitySet.Name));
			EntityKeyMember[] entityKeyValues = key.EntityKeyValues;
			ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
			ObjectParameter[] array = new ObjectParameter[entityKeyValues.Length];
			for (int i = 0; i < entityKeyValues.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" AND ");
				}
				string text = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
				stringBuilder.AppendFormat("X.{0} = @{1}", EntityUtil.QuoteIdentifier(entityKeyValues[i].Key), text);
				array[i] = new ObjectParameter(text, entityKeyValues[i].Value);
				EdmMember edmMember = null;
				if (keyMembers.TryGetValue(entityKeyValues[i].Key, true, out edmMember))
				{
					array[i].TypeUsage = edmMember.TypeUsage;
				}
			}
			object obj = null;
			using (IEnumerator<object> enumerator = this.CreateQuery<object>(stringBuilder.ToString(), array).Execute(MergeOption.AppendOnly).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
			}
			value = obj;
			return value != null;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x0009F6D8 File Offset: 0x0009D8D8
		public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return this.ExecuteFunction<TElement>(functionName, MergeOption.AppendOnly, parameters);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x0009F6EF File Offset: 0x0009D8EF
		public virtual ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			Check.NotEmpty(functionName, "functionName");
			return this.ExecuteFunction<TElement>(functionName, new ExecutionOptions(mergeOption), parameters);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x0009F718 File Offset: 0x0009D918
		public virtual ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, ExecutionOptions executionOptions, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			Check.NotEmpty(functionName, "functionName");
			this.AsyncMonitor.EnsureNotEntered();
			EdmFunction functionImport;
			EntityCommand entityCommand = this.CreateEntityCommandForFunctionImport(functionName, out functionImport, parameters);
			int num = Math.Max(1, functionImport.ReturnParameters.Count);
			EdmType[] expectedEdmTypes = new EdmType[num];
			expectedEdmTypes[0] = MetadataHelper.GetAndCheckFunctionImportReturnType<TElement>(functionImport, 0, this.MetadataWorkspace);
			for (int i = 1; i < num; i++)
			{
				if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, i, out expectedEdmTypes[i]))
				{
					throw EntityUtil.ExecuteFunctionCalledWithNonReaderFunction(functionImport);
				}
			}
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			if (executionOptions.UserSpecifiedStreaming == null)
			{
				executionOptions = new ExecutionOptions(executionOptions.MergeOption, !executionStrategy.RetriesOnFailure);
			}
			bool startLocalTransaction = !executionOptions.UserSpecifiedStreaming.Value && this._options.EnsureTransactionsForFunctionsAndCommands;
			Func<ObjectResult<TElement>> <>9__1;
			return executionStrategy.Execute<ObjectResult<TElement>>(delegate
			{
				ObjectContext <>4__this = this;
				Func<ObjectResult<TElement>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.CreateFunctionObjectResult<TElement>(entityCommand, functionImport.EntitySets, expectedEdmTypes, executionOptions));
				}
				return <>4__this.ExecuteInTransaction<ObjectResult<TElement>>(func, executionStrategy, startLocalTransaction, !executionOptions.UserSpecifiedStreaming.Value);
			});
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x0009F8C4 File Offset: 0x0009DAC4
		public virtual int ExecuteFunction(string functionName, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			Check.NotEmpty(functionName, "functionName");
			this.AsyncMonitor.EnsureNotEntered();
			EdmFunction edmFunction;
			EntityCommand entityCommand = this.CreateEntityCommandForFunctionImport(functionName, out edmFunction, parameters);
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			Func<int> <>9__1;
			return executionStrategy.Execute<int>(delegate
			{
				ObjectContext <>4__this = this;
				Func<int> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => ObjectContext.ExecuteFunctionCommand(entityCommand));
				}
				return <>4__this.ExecuteInTransaction<int>(func, executionStrategy, this._options.EnsureTransactionsForFunctionsAndCommands, true);
			});
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x0009F940 File Offset: 0x0009DB40
		private static int ExecuteFunctionCommand(EntityCommand entityCommand)
		{
			entityCommand.Prepare();
			int num;
			try
			{
				num = entityCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableEntityExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandExecutionFailed, ex);
				}
				throw;
			}
			return num;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x0009F984 File Offset: 0x0009DB84
		private EntityCommand CreateEntityCommandForFunctionImport(string functionName, out EdmFunction functionImport, params ObjectParameter[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i] == null)
				{
					throw new InvalidOperationException(Strings.ObjectContext_ExecuteFunctionCalledWithNullParameter(i));
				}
			}
			string text;
			string text2;
			functionImport = MetadataHelper.GetFunctionImport(functionName, this.DefaultContainerName, this.MetadataWorkspace, out text, out text2);
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			EntityCommand entityCommand = new EntityCommand(this.InterceptionContext);
			entityCommand.CommandType = CommandType.StoredProcedure;
			entityCommand.CommandText = text + "." + text2;
			entityCommand.Connection = entityConnection;
			if (this.CommandTimeout != null)
			{
				entityCommand.CommandTimeout = this.CommandTimeout.Value;
			}
			this.PopulateFunctionImportEntityCommandParameters(parameters, functionImport, entityCommand);
			return entityCommand;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x0009FA40 File Offset: 0x0009DC40
		private ObjectResult<TElement> CreateFunctionObjectResult<TElement>(EntityCommand entityCommand, ReadOnlyCollection<EntitySet> entitySets, EdmType[] edmTypes, ExecutionOptions executionOptions)
		{
			EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
			DbDataReader dbDataReader = null;
			try
			{
				dbDataReader = commandDefinition.ExecuteStoreCommands(entityCommand, executionOptions.UserSpecifiedStreaming.Value ? CommandBehavior.Default : CommandBehavior.SequentialAccess);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableEntityExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandExecutionFailed, ex);
				}
				throw;
			}
			ShaperFactory<TElement> shaperFactory = null;
			if (!executionOptions.UserSpecifiedStreaming.Value)
			{
				BufferedDataReader bufferedDataReader = null;
				try
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					shaperFactory = this._translator.TranslateColumnMap<TElement>(commandDefinition.CreateColumnMap(dbDataReader, 0), this.MetadataWorkspace, null, executionOptions.MergeOption, false, false);
					bufferedDataReader = new BufferedDataReader(dbDataReader);
					bufferedDataReader.Initialize(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns);
					dbDataReader = bufferedDataReader;
				}
				catch (Exception)
				{
					if (bufferedDataReader != null)
					{
						bufferedDataReader.Dispose();
					}
					throw;
				}
			}
			return this.MaterializedDataRecord<TElement>(entityCommand, dbDataReader, 0, entitySets, edmTypes, shaperFactory, executionOptions.MergeOption, executionOptions.UserSpecifiedStreaming.Value);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x0009FB68 File Offset: 0x0009DD68
		internal ObjectResult<TElement> MaterializedDataRecord<TElement>(EntityCommand entityCommand, DbDataReader storeReader, int resultSetIndex, ReadOnlyCollection<EntitySet> entitySets, EdmType[] edmTypes, ShaperFactory<TElement> shaperFactory, MergeOption mergeOption, bool streaming)
		{
			EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
			ObjectResult<TElement> objectResult;
			try
			{
				bool flag = edmTypes.Length <= resultSetIndex + 1;
				EntitySet entitySet = ((entitySets.Count > resultSetIndex) ? entitySets[resultSetIndex] : null);
				if (shaperFactory == null)
				{
					shaperFactory = this._translator.TranslateColumnMap<TElement>(commandDefinition.CreateColumnMap(storeReader, resultSetIndex), this.MetadataWorkspace, null, mergeOption, streaming, false);
				}
				Shaper<TElement> shaper = shaperFactory.Create(storeReader, this, this.MetadataWorkspace, mergeOption, flag, streaming);
				bool onReaderDisposeHasRun = false;
				Action<object, EventArgs> action = delegate(object sender, EventArgs e)
				{
					if (!onReaderDisposeHasRun)
					{
						onReaderDisposeHasRun = true;
						CommandHelper.ConsumeReader(storeReader);
						entityCommand.NotifyDataReaderClosing();
					}
				};
				NextResultGenerator nextResultGenerator;
				if (flag)
				{
					shaper.OnDone += action.Invoke;
					nextResultGenerator = null;
				}
				else
				{
					nextResultGenerator = new NextResultGenerator(this, entityCommand, edmTypes, entitySets, mergeOption, streaming, resultSetIndex + 1);
				}
				objectResult = new ObjectResult<TElement>(shaper, entitySet, TypeUsage.Create(edmTypes[resultSetIndex]), true, streaming, nextResultGenerator, action, null);
			}
			catch
			{
				this.ReleaseConnection();
				storeReader.Dispose();
				throw;
			}
			return objectResult;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x0009FC8C File Offset: 0x0009DE8C
		private void PopulateFunctionImportEntityCommandParameters(ObjectParameter[] parameters, EdmFunction functionImport, EntityCommand command)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				ObjectParameter objectParameter = parameters[i];
				EntityParameter entityParameter = new EntityParameter();
				FunctionParameter functionParameter = ObjectContext.FindParameterMetadata(functionImport, parameters, i);
				if (functionParameter != null)
				{
					entityParameter.Direction = MetadataHelper.ParameterModeToParameterDirection(functionParameter.Mode);
					entityParameter.ParameterName = functionParameter.Name;
				}
				else
				{
					entityParameter.ParameterName = objectParameter.Name;
				}
				entityParameter.Value = objectParameter.Value ?? DBNull.Value;
				if (DBNull.Value == entityParameter.Value || entityParameter.Direction != ParameterDirection.Input)
				{
					TypeUsage typeUsage;
					if (functionParameter != null)
					{
						typeUsage = functionParameter.TypeUsage;
					}
					else if (objectParameter.TypeUsage == null)
					{
						if (!this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullNameWithNesting(), false, out typeUsage))
						{
							this.MetadataWorkspace.ImplicitLoadAssemblyForType(objectParameter.MappableType, null);
							this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullNameWithNesting(), false, out typeUsage);
						}
					}
					else
					{
						typeUsage = objectParameter.TypeUsage;
					}
					EntityCommandDefinition.PopulateParameterFromTypeUsage(entityParameter, typeUsage, entityParameter.Direction != ParameterDirection.Input);
				}
				if (entityParameter.Direction != ParameterDirection.Input)
				{
					ObjectContext.ParameterBinder parameterBinder = new ObjectContext.ParameterBinder(entityParameter, objectParameter);
					command.OnDataReaderClosing += parameterBinder.OnDataReaderClosingHandler;
				}
				command.Parameters.Add(entityParameter);
			}
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x0009FDC8 File Offset: 0x0009DFC8
		private static FunctionParameter FindParameterMetadata(EdmFunction functionImport, ObjectParameter[] parameters, int ordinal)
		{
			string name = parameters[ordinal].Name;
			FunctionParameter functionParameter;
			if (!functionImport.Parameters.TryGetValue(name, false, out functionParameter))
			{
				int num = 0;
				int num2 = 0;
				while (num2 < parameters.Length && num < 2)
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(parameters[num2].Name, name))
					{
						num++;
					}
					num2++;
				}
				if (num == 1)
				{
					functionImport.Parameters.TryGetValue(name, true, out functionParameter);
				}
			}
			return functionParameter;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x0009FE34 File Offset: 0x0009E034
		public virtual void CreateProxyTypes(IEnumerable<Type> types)
		{
			ObjectItemCollection ospaceItems = (ObjectItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
			EntityProxyFactory.TryCreateProxyTypes(from entityType in types.Select(delegate(Type type)
				{
					this.MetadataWorkspace.ImplicitLoadAssemblyForType(type, null);
					EntityType entityType2;
					ospaceItems.TryGetItem<EntityType>(type.FullNameWithNesting(), out entityType2);
					return entityType2;
				})
				where entityType != null
				select entityType, this.MetadataWorkspace);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x0009FEA6 File Offset: 0x0009E0A6
		public static IEnumerable<Type> GetKnownProxyTypes()
		{
			return EntityProxyFactory.GetKnownProxyTypes();
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x0009FEAD File Offset: 0x0009E0AD
		public static Type GetObjectType(Type type)
		{
			Check.NotNull<Type>(type, "type");
			if (!EntityProxyFactory.IsProxyType(type))
			{
				return type;
			}
			return type.BaseType();
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x0009FECC File Offset: 0x0009E0CC
		public virtual T CreateObject<T>() where T : class
		{
			T t = default(T);
			Type typeFromHandle = typeof(T);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeFromHandle, null);
			ClrEntityType item = this.MetadataWorkspace.GetItem<ClrEntityType>(typeFromHandle.FullNameWithNesting(), DataSpace.OSpace);
			EntityProxyTypeInfo proxyType;
			if (this.ContextOptions.ProxyCreationEnabled && (proxyType = EntityProxyFactory.GetProxyType(item, this.MetadataWorkspace)) != null)
			{
				t = (T)((object)proxyType.CreateProxyObject());
				IEntityWrapper entityWrapper = EntityWrapperFactory.CreateNewWrapper(t, null);
				entityWrapper.InitializingProxyRelatedEnds = true;
				try
				{
					entityWrapper.AttachContext(this, null, MergeOption.NoTracking);
					proxyType.SetEntityWrapper(entityWrapper);
					if (proxyType.InitializeEntityCollections != null)
					{
						proxyType.InitializeEntityCollections.Invoke(null, new object[] { entityWrapper });
					}
					return t;
				}
				finally
				{
					entityWrapper.InitializingProxyRelatedEnds = false;
					entityWrapper.DetachContext();
				}
			}
			t = DelegateFactory.GetConstructorDelegateForType(item)() as T;
			return t;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0009FFC0 File Offset: 0x0009E1C0
		public virtual int ExecuteStoreCommand(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreCommand(this._options.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, commandText, parameters);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x0009FFDC File Offset: 0x0009E1DC
		public virtual int ExecuteStoreCommand(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters)
		{
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			this.AsyncMonitor.EnsureNotEntered();
			Func<int> <>9__1;
			return executionStrategy.Execute<int>(delegate
			{
				ObjectContext <>4__this = this;
				Func<int> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.ExecuteStoreCommandInternal(commandText, parameters));
				}
				return <>4__this.ExecuteInTransaction<int>(func, executionStrategy, transactionalBehavior != TransactionalBehavior.DoNotEnsureTransaction, true);
			});
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000A0044 File Offset: 0x0009E244
		private int ExecuteStoreCommandInternal(string commandText, object[] parameters)
		{
			DbCommand dbCommand = this.CreateStoreCommand(commandText, parameters);
			int num;
			try
			{
				num = dbCommand.ExecuteNonQuery();
			}
			finally
			{
				dbCommand.Parameters.Clear();
				dbCommand.Dispose();
			}
			return num;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000A0088 File Offset: 0x0009E288
		public Task<int> ExecuteStoreCommandAsync(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreCommandAsync(this._options.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, commandText, CancellationToken.None, parameters);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000A00A8 File Offset: 0x0009E2A8
		public Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters)
		{
			return this.ExecuteStoreCommandAsync(transactionalBehavior, commandText, CancellationToken.None, parameters);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000A00B8 File Offset: 0x0009E2B8
		public virtual Task<int> ExecuteStoreCommandAsync(string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			return this.ExecuteStoreCommandAsync(this._options.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, commandText, cancellationToken, parameters);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000A00D4 File Offset: 0x0009E2D4
		public virtual Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			return this.ExecuteStoreCommandInternalAsync(transactionalBehavior, commandText, cancellationToken, parameters);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000A00F4 File Offset: 0x0009E2F4
		private async Task<int> ExecuteStoreCommandInternalAsync(TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			this.AsyncMonitor.Enter();
			int num;
			try
			{
				Func<Task<int>> <>9__1;
				num = await executionStrategy.ExecuteAsync<int>(delegate
				{
					ObjectContext <>4__this = this;
					Func<Task<int>> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = () => this.ExecuteStoreCommandInternalAsync(commandText, cancellationToken, parameters));
					}
					return <>4__this.ExecuteInTransactionAsync<int>(func, executionStrategy, transactionalBehavior != TransactionalBehavior.DoNotEnsureTransaction, true, cancellationToken);
				}, cancellationToken).WithCurrentCulture<int>();
			}
			finally
			{
				this.AsyncMonitor.Exit();
			}
			return num;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000A015C File Offset: 0x0009E35C
		private async Task<int> ExecuteStoreCommandInternalAsync(string commandText, CancellationToken cancellationToken, object[] parameters)
		{
			DbCommand command = this.CreateStoreCommand(commandText, parameters);
			int num;
			try
			{
				num = await command.ExecuteNonQueryAsync(cancellationToken).WithCurrentCulture<int>();
			}
			finally
			{
				command.Parameters.Clear();
				command.Dispose();
			}
			return num;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000A01B9 File Offset: 0x0009E3B9
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreQueryReliably<TElement>(commandText, null, ExecutionOptions.Default, parameters);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000A01C9 File Offset: 0x0009E3C9
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters)
		{
			return this.ExecuteStoreQueryReliably<TElement>(commandText, null, executionOptions, parameters);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000A01D5 File Offset: 0x0009E3D5
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			return this.ExecuteStoreQueryReliably<TElement>(commandText, entitySetName, new ExecutionOptions(mergeOption), parameters);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000A01F3 File Offset: 0x0009E3F3
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			return this.ExecuteStoreQueryReliably<TElement>(commandText, entitySetName, executionOptions, parameters);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000A020C File Offset: 0x0009E40C
		private ObjectResult<TElement> ExecuteStoreQueryReliably<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			this.AsyncMonitor.EnsureNotEntered();
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			if (executionOptions.UserSpecifiedStreaming == null)
			{
				executionOptions = new ExecutionOptions(executionOptions.MergeOption, !executionStrategy.RetriesOnFailure);
			}
			Func<ObjectResult<TElement>> <>9__1;
			return executionStrategy.Execute<ObjectResult<TElement>>(delegate
			{
				ObjectContext <>4__this = this;
				Func<ObjectResult<TElement>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.ExecuteStoreQueryInternal<TElement>(commandText, entitySetName, executionOptions, parameters));
				}
				return <>4__this.ExecuteInTransaction<ObjectResult<TElement>>(func, executionStrategy, false, !executionOptions.UserSpecifiedStreaming.Value);
			});
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000A0324 File Offset: 0x0009E524
		private ObjectResult<TElement> ExecuteStoreQueryInternal<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			DbDataReader dbDataReader = null;
			DbCommand dbCommand = null;
			EntitySet entitySet;
			TypeUsage typeUsage;
			ShaperFactory<TElement> shaperFactory;
			try
			{
				dbCommand = this.CreateStoreCommand(commandText, parameters);
				dbDataReader = dbCommand.ExecuteReader(executionOptions.UserSpecifiedStreaming.Value ? CommandBehavior.Default : CommandBehavior.SequentialAccess);
				shaperFactory = this.InternalTranslate<TElement>(dbDataReader, entitySetName, executionOptions.MergeOption, executionOptions.UserSpecifiedStreaming.Value, out entitySet, out typeUsage);
			}
			catch
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
				}
				if (dbCommand != null)
				{
					dbCommand.Parameters.Clear();
					dbCommand.Dispose();
				}
				throw;
			}
			if (!executionOptions.UserSpecifiedStreaming.Value)
			{
				BufferedDataReader bufferedDataReader = null;
				try
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedDataReader = new BufferedDataReader(dbDataReader);
					bufferedDataReader.Initialize(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns);
					dbDataReader = bufferedDataReader;
				}
				catch
				{
					if (bufferedDataReader != null)
					{
						bufferedDataReader.Dispose();
					}
					throw;
				}
			}
			return this.ShapeResult<TElement>(dbDataReader, executionOptions.MergeOption, true, executionOptions.UserSpecifiedStreaming.Value, shaperFactory, entitySet, typeUsage, dbCommand);
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000A0454 File Offset: 0x0009E654
		public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreQueryAsync<TElement>(commandText, CancellationToken.None, parameters);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000A0464 File Offset: 0x0009E664
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			this.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			return this.ExecuteStoreQueryReliablyAsync<TElement>(commandText, null, ExecutionOptions.Default, cancellationToken, executionStrategy, parameters);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000A049E File Offset: 0x0009E69E
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters)
		{
			return this.ExecuteStoreQueryAsync<TElement>(commandText, executionOptions, CancellationToken.None, parameters);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, CancellationToken cancellationToken, params object[] parameters)
		{
			this.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			return this.ExecuteStoreQueryReliablyAsync<TElement>(commandText, null, executionOptions, cancellationToken, executionStrategy, parameters);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000A0525 File Offset: 0x0009E725
		public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			return this.ExecuteStoreQueryAsync<TElement>(commandText, entitySetName, executionOptions, CancellationToken.None, parameters);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000A0538 File Offset: 0x0009E738
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, params object[] parameters)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			return this.ExecuteStoreQueryReliablyAsync<TElement>(commandText, entitySetName, executionOptions, cancellationToken, executionStrategy, parameters);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000A05BC File Offset: 0x0009E7BC
		private async Task<ObjectResult<TElement>> ExecuteStoreQueryReliablyAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, IDbExecutionStrategy executionStrategy, params object[] parameters)
		{
			if (executionOptions.MergeOption != MergeOption.NoTracking)
			{
				this.AsyncMonitor.Enter();
			}
			ObjectResult<TElement> objectResult;
			try
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
				if (executionOptions.UserSpecifiedStreaming == null)
				{
					executionOptions = new ExecutionOptions(executionOptions.MergeOption, !executionStrategy.RetriesOnFailure);
				}
				Func<Task<ObjectResult<TElement>>> <>9__1;
				objectResult = await executionStrategy.ExecuteAsync<ObjectResult<TElement>>(delegate
				{
					ObjectContext <>4__this = this;
					Func<Task<ObjectResult<TElement>>> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = () => this.ExecuteStoreQueryInternalAsync<TElement>(commandText, entitySetName, executionOptions, cancellationToken, parameters));
					}
					return <>4__this.ExecuteInTransactionAsync<ObjectResult<TElement>>(func, executionStrategy, false, !executionOptions.UserSpecifiedStreaming.Value, cancellationToken);
				}, cancellationToken).WithCurrentCulture<ObjectResult<TElement>>();
			}
			finally
			{
				if (executionOptions.MergeOption != MergeOption.NoTracking)
				{
					this.AsyncMonitor.Exit();
				}
			}
			return objectResult;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000A0634 File Offset: 0x0009E834
		private async Task<ObjectResult<TElement>> ExecuteStoreQueryInternalAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, params object[] parameters)
		{
			DbDataReader reader = null;
			DbCommand command = null;
			EntitySet entitySet;
			TypeUsage edmType;
			ShaperFactory<TElement> shaperFactory;
			try
			{
				command = this.CreateStoreCommand(commandText, parameters);
				DbDataReader dbDataReader = await command.ExecuteReaderAsync(executionOptions.UserSpecifiedStreaming.Value ? CommandBehavior.Default : CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
				reader = dbDataReader;
				shaperFactory = this.InternalTranslate<TElement>(reader, entitySetName, executionOptions.MergeOption, executionOptions.UserSpecifiedStreaming.Value, out entitySet, out edmType);
			}
			catch
			{
				if (reader != null)
				{
					reader.Dispose();
				}
				if (command != null)
				{
					command.Parameters.Clear();
					command.Dispose();
				}
				throw;
			}
			if (!executionOptions.UserSpecifiedStreaming.Value)
			{
				BufferedDataReader bufferedReader = null;
				try
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedReader = new BufferedDataReader(reader);
					await bufferedReader.InitializeAsync(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns, cancellationToken).WithCurrentCulture();
					reader = bufferedReader;
				}
				catch
				{
					if (bufferedReader != null)
					{
						bufferedReader.Dispose();
					}
					throw;
				}
				bufferedReader = null;
			}
			return this.ShapeResult<TElement>(reader, executionOptions.MergeOption, true, executionOptions.UserSpecifiedStreaming.Value, shaperFactory, entitySet, edmType, command);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000A06A4 File Offset: 0x0009E8A4
		public virtual ObjectResult<TElement> Translate<TElement>(DbDataReader reader)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
			EntitySet entitySet;
			TypeUsage typeUsage;
			ShaperFactory<TElement> shaperFactory = this.InternalTranslate<TElement>(reader, null, MergeOption.AppendOnly, false, out entitySet, out typeUsage);
			return this.ShapeResult<TElement>(reader, MergeOption.AppendOnly, false, false, shaperFactory, entitySet, typeUsage, null);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000A06E8 File Offset: 0x0009E8E8
		public virtual ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TEntity), Assembly.GetCallingAssembly());
			EntitySet entitySet;
			TypeUsage typeUsage;
			ShaperFactory<TEntity> shaperFactory = this.InternalTranslate<TEntity>(reader, entitySetName, mergeOption, false, out entitySet, out typeUsage);
			return this.ShapeResult<TEntity>(reader, mergeOption, false, false, shaperFactory, entitySet, typeUsage, null);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000A0738 File Offset: 0x0009E938
		private ShaperFactory<TElement> InternalTranslate<TElement>(DbDataReader reader, string entitySetName, MergeOption mergeOption, bool streaming, out EntitySet entitySet, out TypeUsage edmType)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			entitySet = null;
			if (!string.IsNullOrEmpty(entitySetName))
			{
				entitySet = this.GetEntitySetFromName(entitySetName);
			}
			Type type = Nullable.GetUnderlyingType(typeof(TElement)) ?? typeof(TElement);
			EdmType edmType2;
			CollectionColumnMap collectionColumnMap;
			if (this.MetadataWorkspace.TryDetermineCSpaceModelType<TElement>(out edmType2) || (type.IsEnum() && this.MetadataWorkspace.TryDetermineCSpaceModelType(type.GetEnumUnderlyingType(), out edmType2)))
			{
				if (entitySet != null && !entitySet.ElementType.IsAssignableFrom(edmType2))
				{
					throw new InvalidOperationException(Strings.ObjectContext_InvalidEntitySetForStoreQuery(entitySet.EntityContainer.Name, entitySet.Name, typeof(TElement)));
				}
				collectionColumnMap = this._columnMapFactory.CreateColumnMapFromReaderAndType(reader, edmType2, entitySet, null);
			}
			else
			{
				collectionColumnMap = this._columnMapFactory.CreateColumnMapFromReaderAndClrType(reader, typeof(TElement), this.MetadataWorkspace);
			}
			edmType = collectionColumnMap.Type;
			return this._translator.TranslateColumnMap<TElement>(collectionColumnMap, this.MetadataWorkspace, null, mergeOption, streaming, false);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000A083B File Offset: 0x0009EA3B
		private ObjectResult<TElement> ShapeResult<TElement>(DbDataReader reader, MergeOption mergeOption, bool readerOwned, bool streaming, ShaperFactory<TElement> shaperFactory, EntitySet entitySet, TypeUsage edmType, DbCommand command = null)
		{
			return new ObjectResult<TElement>(shaperFactory.Create(reader, this, this.MetadataWorkspace, mergeOption, readerOwned, streaming), entitySet, MetadataHelper.GetElementType(edmType), readerOwned, streaming, command);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000A0864 File Offset: 0x0009EA64
		private DbCommand CreateStoreCommand(string commandText, params object[] parameters)
		{
			DbCommand dbCommand = ((EntityConnection)this.Connection).StoreConnection.CreateCommand();
			dbCommand.CommandText = commandText;
			if (this.CommandTimeout != null)
			{
				dbCommand.CommandTimeout = this.CommandTimeout.Value;
			}
			EntityTransaction currentTransaction = ((EntityConnection)this.Connection).CurrentTransaction;
			if (currentTransaction != null)
			{
				dbCommand.Transaction = currentTransaction.StoreTransaction;
			}
			if (parameters != null && parameters.Length != 0)
			{
				DbParameter[] array = new DbParameter[parameters.Length];
				if (parameters.All((object p) => p is DbParameter))
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						array[i] = (DbParameter)parameters[i];
					}
				}
				else
				{
					if (parameters.Any((object p) => p is DbParameter))
					{
						throw new InvalidOperationException(Strings.ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues);
					}
					string[] array2 = new string[parameters.Length];
					string[] array3 = new string[parameters.Length];
					for (int j = 0; j < parameters.Length; j++)
					{
						array2[j] = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[] { j });
						array[j] = dbCommand.CreateParameter();
						array[j].ParameterName = array2[j];
						array[j].Value = parameters[j] ?? DBNull.Value;
						array3[j] = "@" + array2[j];
					}
					DbCommand dbCommand2 = dbCommand;
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string commandText2 = dbCommand.CommandText;
					object[] array4 = array3;
					dbCommand2.CommandText = string.Format(invariantCulture, commandText2, array4);
				}
				dbCommand.Parameters.AddRange(array);
			}
			return new InterceptableDbCommand(dbCommand, this.InterceptionContext, null);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000A0A2C File Offset: 0x0009EC2C
		public virtual void CreateDatabase()
		{
			DbConnection storeConnection = ((EntityConnection)this.Connection).StoreConnection;
			this.GetStoreItemCollection().ProviderFactory.GetProviderServices().CreateDatabase(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000A0A6C File Offset: 0x0009EC6C
		public virtual void DeleteDatabase()
		{
			DbConnection storeConnection = ((EntityConnection)this.Connection).StoreConnection;
			this.GetStoreItemCollection().ProviderFactory.GetProviderServices().DeleteDatabase(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000A0AAC File Offset: 0x0009ECAC
		public virtual bool DatabaseExists()
		{
			DbConnection storeConnection = ((EntityConnection)this.Connection).StoreConnection;
			DbProviderServices providerServices = this.GetStoreItemCollection().ProviderFactory.GetProviderServices();
			bool flag;
			try
			{
				flag = providerServices.DatabaseExists(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
			}
			catch (Exception)
			{
				if (this.Connection.State == ConnectionState.Open)
				{
					flag = true;
				}
				else
				{
					try
					{
						this.Connection.Open();
						flag = true;
					}
					catch (EntityException)
					{
						flag = false;
					}
					finally
					{
						this.Connection.Close();
					}
				}
			}
			return flag;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000A0B50 File Offset: 0x0009ED50
		private StoreItemCollection GetStoreItemCollection()
		{
			return (StoreItemCollection)((EntityConnection)this.Connection).GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000A0B70 File Offset: 0x0009ED70
		public virtual string CreateDatabaseScript()
		{
			DbProviderServices providerServices = this.GetStoreItemCollection().ProviderFactory.GetProviderServices();
			string providerManifestToken = this.GetStoreItemCollection().ProviderManifestToken;
			return providerServices.CreateDatabaseScript(providerManifestToken, this.GetStoreItemCollection());
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000A0BA8 File Offset: 0x0009EDA8
		internal void InitializeMappingViewCacheFactory(DbContext owner = null)
		{
			StorageMappingItemCollection itemCollection = (StorageMappingItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
			if (itemCollection == null)
			{
				return;
			}
			Type type = ((owner != null) ? owner.GetType() : base.GetType());
			ObjectContext._contextTypesWithViewCacheInitialized.GetOrAdd(type, delegate(Type t)
			{
				IEnumerable<DbMappingViewCacheTypeAttribute> enumerable = from a in t.Assembly().GetCustomAttributes<DbMappingViewCacheTypeAttribute>()
					where a.ContextType == t
					select a;
				int num = enumerable.Count<DbMappingViewCacheTypeAttribute>();
				if (num > 1)
				{
					throw new InvalidOperationException(Strings.DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType(t));
				}
				if (num == 1)
				{
					itemCollection.MappingViewCacheFactory = new DefaultDbMappingViewCacheFactory(enumerable.First<DbMappingViewCacheTypeAttribute>().CacheType);
				}
				return true;
			});
		}

		// Token: 0x04001048 RID: 4168
		private bool _disposed;

		// Token: 0x04001049 RID: 4169
		private readonly IEntityAdapter _adapter;

		// Token: 0x0400104A RID: 4170
		private EntityConnection _connection;

		// Token: 0x0400104B RID: 4171
		private readonly MetadataWorkspace _workspace;

		// Token: 0x0400104C RID: 4172
		private ObjectStateManager _objectStateManager;

		// Token: 0x0400104D RID: 4173
		private ClrPerspective _perspective;

		// Token: 0x0400104E RID: 4174
		private bool _contextOwnsConnection;

		// Token: 0x0400104F RID: 4175
		private bool _openedConnection;

		// Token: 0x04001050 RID: 4176
		private int _connectionRequestCount;

		// Token: 0x04001051 RID: 4177
		private int? _queryTimeout;

		// Token: 0x04001052 RID: 4178
		private Transaction _lastTransaction;

		// Token: 0x04001053 RID: 4179
		private readonly bool _disallowSettingDefaultContainerName;

		// Token: 0x04001054 RID: 4180
		private EventHandler _onSavingChanges;

		// Token: 0x04001055 RID: 4181
		private ObjectMaterializedEventHandler _onObjectMaterialized;

		// Token: 0x04001056 RID: 4182
		private ObjectQueryProvider _queryProvider;

		// Token: 0x04001057 RID: 4183
		private readonly EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x04001058 RID: 4184
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;

		// Token: 0x04001059 RID: 4185
		private readonly Translator _translator;

		// Token: 0x0400105A RID: 4186
		private readonly ColumnMapFactory _columnMapFactory;

		// Token: 0x0400105B RID: 4187
		private readonly ObjectContextOptions _options;

		// Token: 0x0400105C RID: 4188
		private const string UseLegacyPreserveChangesBehavior = "EntityFramework_UseLegacyPreserveChangesBehavior";

		// Token: 0x0400105D RID: 4189
		private readonly ThrowingMonitor _asyncMonitor;

		// Token: 0x0400105E RID: 4190
		private DbInterceptionContext _interceptionContext;

		// Token: 0x0400105F RID: 4191
		private static readonly ConcurrentDictionary<Type, bool> _contextTypesWithViewCacheInitialized = new ConcurrentDictionary<Type, bool>();

		// Token: 0x04001060 RID: 4192
		private TransactionHandler _transactionHandler;

		// Token: 0x02000A16 RID: 2582
		private class ParameterBinder
		{
			// Token: 0x060060E0 RID: 24800 RVA: 0x0014CC44 File Offset: 0x0014AE44
			internal ParameterBinder(EntityParameter entityParameter, ObjectParameter objectParameter)
			{
				this._entityParameter = entityParameter;
				this._objectParameter = objectParameter;
			}

			// Token: 0x060060E1 RID: 24801 RVA: 0x0014CC5C File Offset: 0x0014AE5C
			internal void OnDataReaderClosingHandler(object sender, EventArgs args)
			{
				if (this._entityParameter.Value != DBNull.Value && this._objectParameter.MappableType.IsEnum())
				{
					this._objectParameter.Value = Enum.ToObject(this._objectParameter.MappableType, this._entityParameter.Value);
					return;
				}
				this._objectParameter.Value = this._entityParameter.Value;
			}

			// Token: 0x04002939 RID: 10553
			private readonly EntityParameter _entityParameter;

			// Token: 0x0400293A RID: 10554
			private readonly ObjectParameter _objectParameter;
		}
	}
}
