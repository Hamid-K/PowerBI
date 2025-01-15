using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005DB RID: 1499
	public class EntityCommand : DbCommand
	{
		// Token: 0x06004895 RID: 18581 RVA: 0x0010246D File Offset: 0x0010066D
		public EntityCommand()
			: this(new DbInterceptionContext())
		{
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x0010247A File Offset: 0x0010067A
		internal EntityCommand(DbInterceptionContext interceptionContext)
			: this(interceptionContext, new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x00102488 File Offset: 0x00100688
		internal EntityCommand(DbInterceptionContext interceptionContext, EntityCommand.EntityDataReaderFactory factory)
		{
			this._designTimeVisible = true;
			this._commandType = CommandType.Text;
			this._updatedRowSource = UpdateRowSource.Both;
			this._parameters = new EntityParameterCollection();
			this._interceptionContext = interceptionContext;
			this._enableQueryPlanCaching = true;
			this._entityDataReaderFactory = factory ?? new EntityCommand.EntityDataReaderFactory();
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x001024D9 File Offset: 0x001006D9
		public EntityCommand(string statement)
			: this(statement, new DbInterceptionContext(), new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x001024EC File Offset: 0x001006EC
		internal EntityCommand(string statement, DbInterceptionContext context, EntityCommand.EntityDataReaderFactory factory)
			: this(context, factory)
		{
			this._esqlCommandText = statement;
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x001024FD File Offset: 0x001006FD
		public EntityCommand(string statement, EntityConnection connection, IDbDependencyResolver resolver)
			: this(statement, connection)
		{
			this._dependencyResolver = resolver;
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x0010250E File Offset: 0x0010070E
		public EntityCommand(string statement, EntityConnection connection)
			: this(statement, connection, new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0010251D File Offset: 0x0010071D
		internal EntityCommand(string statement, EntityConnection connection, EntityCommand.EntityDataReaderFactory factory)
			: this(statement, new DbInterceptionContext(), factory)
		{
			this._connection = connection;
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x00102533 File Offset: 0x00100733
		public EntityCommand(string statement, EntityConnection connection, EntityTransaction transaction)
			: this(statement, connection, transaction, new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x00102543 File Offset: 0x00100743
		internal EntityCommand(string statement, EntityConnection connection, EntityTransaction transaction, EntityCommand.EntityDataReaderFactory factory)
			: this(statement, connection, factory)
		{
			this._transaction = transaction;
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x00102558 File Offset: 0x00100758
		internal EntityCommand(EntityCommandDefinition commandDefinition, DbInterceptionContext context, EntityCommand.EntityDataReaderFactory factory = null)
			: this(context, factory)
		{
			this._commandDefinition = commandDefinition;
			this._parameters = new EntityParameterCollection();
			foreach (EntityParameter entityParameter in commandDefinition.Parameters)
			{
				this._parameters.Add(entityParameter.Clone());
			}
			this._parameters.ResetIsDirty();
			this._isCommandDefinitionBased = true;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x001025DC File Offset: 0x001007DC
		internal EntityCommand(EntityConnection connection, EntityCommandDefinition entityCommandDefinition, DbInterceptionContext context, EntityCommand.EntityDataReaderFactory factory = null)
			: this(entityCommandDefinition, context, factory)
		{
			this._connection = connection;
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x001025EF File Offset: 0x001007EF
		internal virtual DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060048A2 RID: 18594 RVA: 0x001025F7 File Offset: 0x001007F7
		// (set) Token: 0x060048A3 RID: 18595 RVA: 0x001025FF File Offset: 0x001007FF
		public new virtual EntityConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (this._connection != value)
				{
					if (this._connection != null)
					{
						this.Unprepare();
					}
					this._connection = value;
					this._transaction = null;
				}
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060048A4 RID: 18596 RVA: 0x0010262C File Offset: 0x0010082C
		// (set) Token: 0x060048A5 RID: 18597 RVA: 0x00102634 File Offset: 0x00100834
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (EntityConnection)value;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060048A6 RID: 18598 RVA: 0x00102642 File Offset: 0x00100842
		// (set) Token: 0x060048A7 RID: 18599 RVA: 0x00102666 File Offset: 0x00100866
		public override string CommandText
		{
			get
			{
				if (this._commandTreeSetByUser != null)
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotGetCommandText);
				}
				return this._esqlCommandText ?? "";
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (this._commandTreeSetByUser != null)
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotSetCommandText);
				}
				if (this._esqlCommandText != value)
				{
					this._esqlCommandText = value;
					this.Unprepare();
					this._isCommandDefinitionBased = false;
				}
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060048A8 RID: 18600 RVA: 0x001026A3 File Offset: 0x001008A3
		// (set) Token: 0x060048A9 RID: 18601 RVA: 0x001026C4 File Offset: 0x001008C4
		public virtual DbCommandTree CommandTree
		{
			get
			{
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotGetCommandTree);
				}
				return this._commandTreeSetByUser;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotSetCommandTree);
				}
				if (CommandType.Text != this.CommandType)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1026));
				}
				if (this._commandTreeSetByUser != value)
				{
					this._commandTreeSetByUser = value;
					this.Unprepare();
					this._isCommandDefinitionBased = false;
				}
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x0010272C File Offset: 0x0010092C
		// (set) Token: 0x060048AB RID: 18603 RVA: 0x00102783 File Offset: 0x00100983
		public override int CommandTimeout
		{
			get
			{
				if (this._commandTimeout != null)
				{
					return this._commandTimeout.Value;
				}
				if (this._connection != null && this._connection.StoreProviderFactory != null)
				{
					DbCommand dbCommand = this._connection.StoreProviderFactory.CreateCommand();
					if (dbCommand != null)
					{
						return dbCommand.CommandTimeout;
					}
				}
				return 0;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._commandTimeout = new int?(value);
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x00102797 File Offset: 0x00100997
		// (set) Token: 0x060048AD RID: 18605 RVA: 0x0010279F File Offset: 0x0010099F
		public override CommandType CommandType
		{
			get
			{
				return this._commandType;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (value != CommandType.Text && value != CommandType.StoredProcedure)
				{
					throw new NotSupportedException(Strings.EntityClient_UnsupportedCommandType);
				}
				this._commandType = value;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x001027C1 File Offset: 0x001009C1
		public new virtual EntityParameterCollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060048AF RID: 18607 RVA: 0x001027C9 File Offset: 0x001009C9
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060048B0 RID: 18608 RVA: 0x001027D1 File Offset: 0x001009D1
		// (set) Token: 0x060048B1 RID: 18609 RVA: 0x001027D9 File Offset: 0x001009D9
		public new virtual EntityTransaction Transaction
		{
			get
			{
				return this._transaction;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._transaction = value;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060048B2 RID: 18610 RVA: 0x001027E8 File Offset: 0x001009E8
		// (set) Token: 0x060048B3 RID: 18611 RVA: 0x001027F0 File Offset: 0x001009F0
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (EntityTransaction)value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x001027FE File Offset: 0x001009FE
		// (set) Token: 0x060048B5 RID: 18613 RVA: 0x00102806 File Offset: 0x00100A06
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._updatedRowSource = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060048B6 RID: 18614 RVA: 0x00102815 File Offset: 0x00100A15
		// (set) Token: 0x060048B7 RID: 18615 RVA: 0x0010281D File Offset: 0x00100A1D
		public override bool DesignTimeVisible
		{
			get
			{
				return this._designTimeVisible;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x00102832 File Offset: 0x00100A32
		// (set) Token: 0x060048B9 RID: 18617 RVA: 0x0010283A File Offset: 0x00100A3A
		public virtual bool EnablePlanCaching
		{
			get
			{
				return this._enableQueryPlanCaching;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._enableQueryPlanCaching = value;
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x00102849 File Offset: 0x00100A49
		public override void Cancel()
		{
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0010284B File Offset: 0x00100A4B
		public new virtual EntityParameter CreateParameter()
		{
			return new EntityParameter();
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x00102852 File Offset: 0x00100A52
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0010285A File Offset: 0x00100A5A
		public new virtual EntityDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x00102864 File Offset: 0x00100A64
		public new virtual EntityDataReader ExecuteReader(CommandBehavior behavior)
		{
			this.Prepare();
			EntityDataReader entityDataReader = this._entityDataReaderFactory.CreateEntityDataReader(this, this._commandDefinition.Execute(this, behavior), behavior);
			this._dataReader = entityDataReader;
			return entityDataReader;
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0010289A File Offset: 0x00100A9A
		public new virtual Task<EntityDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x001028A8 File Offset: 0x00100AA8
		public new virtual Task<EntityDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x001028B2 File Offset: 0x00100AB2
		public new virtual Task<EntityDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x001028C0 File Offset: 0x00100AC0
		public new virtual async Task<EntityDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.Prepare();
			DbDataReader dbDataReader = await this._commandDefinition.ExecuteAsync(this, behavior, cancellationToken).WithCurrentCulture<DbDataReader>();
			EntityDataReader entityDataReader = this._entityDataReaderFactory.CreateEntityDataReader(this, dbDataReader, behavior);
			this._dataReader = entityDataReader;
			return entityDataReader;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00102915 File Offset: 0x00100B15
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x00102920 File Offset: 0x00100B20
		protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return await this.ExecuteReaderAsync(behavior, cancellationToken).WithCurrentCulture<EntityDataReader>();
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x00102978 File Offset: 0x00100B78
		public override int ExecuteNonQuery()
		{
			int recordsAffected;
			using (EntityDataReader entityDataReader = this.ExecuteReader(CommandBehavior.SequentialAccess))
			{
				CommandHelper.ConsumeReader(entityDataReader);
				recordsAffected = entityDataReader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x001029B8 File Offset: 0x00100BB8
		public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			EntityDataReader entityDataReader = await this.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<EntityDataReader>();
			int recordsAffected;
			using (EntityDataReader reader = entityDataReader)
			{
				await CommandHelper.ConsumeReaderAsync(reader, cancellationToken).WithCurrentCulture();
				recordsAffected = reader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x00102A08 File Offset: 0x00100C08
		public override object ExecuteScalar()
		{
			object obj2;
			using (EntityDataReader entityDataReader = this.ExecuteReader(CommandBehavior.SequentialAccess))
			{
				object obj = (entityDataReader.Read() ? entityDataReader.GetValue(0) : null);
				CommandHelper.ConsumeReader(entityDataReader);
				obj2 = obj;
			}
			return obj2;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x00102A54 File Offset: 0x00100C54
		internal virtual void Unprepare()
		{
			this._commandDefinition = null;
			this._preparedCommandTree = null;
			this._parameters.ResetIsDirty();
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x00102A6F File Offset: 0x00100C6F
		public override void Prepare()
		{
			this.ThrowIfDataReaderIsOpen();
			this.CheckIfReadyToPrepare();
			this.InnerPrepare();
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x00102A83 File Offset: 0x00100C83
		private void InnerPrepare()
		{
			if (this._parameters.IsDirty)
			{
				this.Unprepare();
			}
			this._commandDefinition = this.GetCommandDefinition();
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x00102AA4 File Offset: 0x00100CA4
		private DbCommandTree MakeCommandTree()
		{
			DbCommandTree dbCommandTree = null;
			if (this._commandTreeSetByUser != null)
			{
				dbCommandTree = this._commandTreeSetByUser;
			}
			else if (CommandType.Text == this.CommandType)
			{
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					Perspective perspective = new ModelPerspective(this._connection.GetMetadataWorkspace());
					Dictionary<string, TypeUsage> parameterTypeUsage = this.GetParameterTypeUsage();
					dbCommandTree = CqlQuery.Compile(this._esqlCommandText, perspective, null, parameterTypeUsage.Select((KeyValuePair<string, TypeUsage> paramInfo) => paramInfo.Value.Parameter(paramInfo.Key))).CommandTree;
				}
				else
				{
					if (this._isCommandDefinitionBased)
					{
						throw new InvalidOperationException(Strings.EntityClient_CannotReprepareCommandDefinitionBasedCommand);
					}
					throw new InvalidOperationException(Strings.EntityClient_NoCommandText);
				}
			}
			else if (CommandType.StoredProcedure == this.CommandType)
			{
				IEnumerable<KeyValuePair<string, TypeUsage>> parameterTypeUsage2 = this.GetParameterTypeUsage();
				EdmFunction edmFunction = this.DetermineFunctionImport();
				dbCommandTree = new DbFunctionCommandTree(this.Connection.GetMetadataWorkspace(), DataSpace.CSpace, edmFunction, null, parameterTypeUsage2);
			}
			return dbCommandTree;
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x00102B7C File Offset: 0x00100D7C
		private EdmFunction DetermineFunctionImport()
		{
			if (string.IsNullOrEmpty(this.CommandText) || string.IsNullOrEmpty(this.CommandText.Trim()))
			{
				throw new InvalidOperationException(Strings.EntityClient_FunctionImportEmptyCommandText);
			}
			string text = null;
			string text2;
			string text3;
			CommandHelper.ParseFunctionImportCommandText(this.CommandText, text, out text2, out text3);
			return CommandHelper.FindFunctionImport(this._connection.GetMetadataWorkspace(), text2, text3);
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x00102BD8 File Offset: 0x00100DD8
		internal virtual EntityCommandDefinition GetCommandDefinition()
		{
			EntityCommandDefinition entityCommandDefinition = this._commandDefinition;
			if (entityCommandDefinition == null)
			{
				if (!this.TryGetEntityCommandDefinitionFromQueryCache(out entityCommandDefinition))
				{
					entityCommandDefinition = this.CreateCommandDefinition();
				}
				this._commandDefinition = entityCommandDefinition;
			}
			return entityCommandDefinition;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x00102C08 File Offset: 0x00100E08
		internal virtual EntityTransaction ValidateAndGetEntityTransaction()
		{
			if (this.Transaction != null && this.Transaction != this.Connection.CurrentTransaction)
			{
				throw new InvalidOperationException(Strings.EntityClient_InvalidTransactionForCommand);
			}
			return this.Connection.CurrentTransaction;
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x00102C3C File Offset: 0x00100E3C
		[Browsable(false)]
		public virtual string ToTraceString()
		{
			this.CheckConnectionPresent();
			this.InnerPrepare();
			EntityCommandDefinition commandDefinition = this._commandDefinition;
			if (commandDefinition != null)
			{
				return commandDefinition.ToTraceString();
			}
			return string.Empty;
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x00102C6C File Offset: 0x00100E6C
		private bool TryGetEntityCommandDefinitionFromQueryCache(out EntityCommandDefinition entityCommandDefinition)
		{
			entityCommandDefinition = null;
			if (!this._enableQueryPlanCaching || string.IsNullOrEmpty(this._esqlCommandText))
			{
				return false;
			}
			EntityClientCacheKey entityClientCacheKey = new EntityClientCacheKey(this);
			QueryCacheManager queryCacheManager = this._connection.GetMetadataWorkspace().GetQueryCacheManager();
			if (!queryCacheManager.TryCacheLookup<EntityClientCacheKey, EntityCommandDefinition>(entityClientCacheKey, out entityCommandDefinition))
			{
				entityCommandDefinition = this.CreateCommandDefinition();
				QueryCacheEntry queryCacheEntry = null;
				if (queryCacheManager.TryLookupAndAdd(new QueryCacheEntry(entityClientCacheKey, entityCommandDefinition), out queryCacheEntry))
				{
					entityCommandDefinition = (EntityCommandDefinition)queryCacheEntry.GetTarget();
				}
			}
			return true;
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x00102CE0 File Offset: 0x00100EE0
		private EntityCommandDefinition CreateCommandDefinition()
		{
			if (this._preparedCommandTree == null)
			{
				this._preparedCommandTree = this.MakeCommandTree();
			}
			if (!this._preparedCommandTree.MetadataWorkspace.IsMetadataWorkspaceCSCompatible(this.Connection.GetMetadataWorkspace()))
			{
				throw new InvalidOperationException(Strings.EntityClient_CommandTreeMetadataIncompatible);
			}
			return EntityProviderServices.CreateCommandDefinition(this._connection.StoreProviderFactory, this._preparedCommandTree, this._interceptionContext, this._dependencyResolver);
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00102D4B File Offset: 0x00100F4B
		private void CheckConnectionPresent()
		{
			if (this._connection == null)
			{
				throw new InvalidOperationException(Strings.EntityClient_NoConnectionForCommand);
			}
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x00102D60 File Offset: 0x00100F60
		private void CheckIfReadyToPrepare()
		{
			this.CheckConnectionPresent();
			if (this._connection.StoreProviderFactory == null || this._connection.StoreConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this._connection.State == ConnectionState.Closed || this._connection.State == ConnectionState.Broken)
			{
				throw new InvalidOperationException(Strings.EntityClient_ExecutingOnClosedConnection((this._connection.State == ConnectionState.Closed) ? Strings.EntityClient_ConnectionStateClosed : Strings.EntityClient_ConnectionStateBroken));
			}
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x00102DD3 File Offset: 0x00100FD3
		private void ThrowIfDataReaderIsOpen()
		{
			if (this._dataReader != null)
			{
				throw new InvalidOperationException(Strings.EntityClient_DataReaderIsStillOpen);
			}
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x00102DE8 File Offset: 0x00100FE8
		internal virtual Dictionary<string, TypeUsage> GetParameterTypeUsage()
		{
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>(this._parameters.Count);
			foreach (object obj in this._parameters)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				string parameterName = entityParameter.ParameterName;
				if (string.IsNullOrEmpty(parameterName))
				{
					throw new InvalidOperationException(Strings.EntityClient_EmptyParameterName);
				}
				if (this.CommandType == CommandType.Text && entityParameter.Direction != ParameterDirection.Input)
				{
					throw new InvalidOperationException(Strings.EntityClient_InvalidParameterDirection(entityParameter.ParameterName));
				}
				if (entityParameter.EdmType == null && entityParameter.DbType == DbType.Object && (entityParameter.Value == null || entityParameter.Value is DBNull))
				{
					throw new InvalidOperationException(Strings.EntityClient_UnknownParameterType(parameterName));
				}
				TypeUsage typeUsage = entityParameter.GetTypeUsage();
				try
				{
					dictionary.Add(parameterName, typeUsage);
				}
				catch (ArgumentException ex)
				{
					throw new InvalidOperationException(Strings.EntityClient_DuplicateParameterNames(entityParameter.ParameterName), ex);
				}
			}
			return dictionary;
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00102EFC File Offset: 0x001010FC
		internal virtual void NotifyDataReaderClosing()
		{
			this._dataReader = null;
			if (this._storeProviderCommand != null)
			{
				CommandHelper.SetEntityParameterValues(this, this._storeProviderCommand, this._connection);
				this._storeProviderCommand = null;
			}
			if (this.IsNotNullOnDataReaderClosingEvent())
			{
				this.InvokeOnDataReaderClosingEvent(this, new EventArgs());
			}
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x00102F3A File Offset: 0x0010113A
		internal virtual void SetStoreProviderCommand(DbCommand storeProviderCommand)
		{
			this._storeProviderCommand = storeProviderCommand;
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x00102F43 File Offset: 0x00101143
		internal virtual bool IsNotNullOnDataReaderClosingEvent()
		{
			return this.OnDataReaderClosing != null;
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x00102F4E File Offset: 0x0010114E
		internal virtual void InvokeOnDataReaderClosingEvent(EntityCommand sender, EventArgs e)
		{
			this.OnDataReaderClosing(sender, e);
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060048DA RID: 18650 RVA: 0x00102F60 File Offset: 0x00101160
		// (remove) Token: 0x060048DB RID: 18651 RVA: 0x00102F98 File Offset: 0x00101198
		internal event EventHandler OnDataReaderClosing;

		// Token: 0x040019B6 RID: 6582
		private bool _designTimeVisible;

		// Token: 0x040019B7 RID: 6583
		private string _esqlCommandText;

		// Token: 0x040019B8 RID: 6584
		private EntityConnection _connection;

		// Token: 0x040019B9 RID: 6585
		private DbCommandTree _preparedCommandTree;

		// Token: 0x040019BA RID: 6586
		private readonly EntityParameterCollection _parameters;

		// Token: 0x040019BB RID: 6587
		private int? _commandTimeout;

		// Token: 0x040019BC RID: 6588
		private CommandType _commandType;

		// Token: 0x040019BD RID: 6589
		private EntityTransaction _transaction;

		// Token: 0x040019BE RID: 6590
		private UpdateRowSource _updatedRowSource;

		// Token: 0x040019BF RID: 6591
		private EntityCommandDefinition _commandDefinition;

		// Token: 0x040019C0 RID: 6592
		private bool _isCommandDefinitionBased;

		// Token: 0x040019C1 RID: 6593
		private DbCommandTree _commandTreeSetByUser;

		// Token: 0x040019C2 RID: 6594
		private DbDataReader _dataReader;

		// Token: 0x040019C3 RID: 6595
		private bool _enableQueryPlanCaching;

		// Token: 0x040019C4 RID: 6596
		private DbCommand _storeProviderCommand;

		// Token: 0x040019C5 RID: 6597
		private readonly EntityCommand.EntityDataReaderFactory _entityDataReaderFactory;

		// Token: 0x040019C6 RID: 6598
		private readonly IDbDependencyResolver _dependencyResolver;

		// Token: 0x040019C7 RID: 6599
		private readonly DbInterceptionContext _interceptionContext;

		// Token: 0x02000C1C RID: 3100
		internal class EntityDataReaderFactory
		{
			// Token: 0x060069A7 RID: 27047 RVA: 0x00169AEB File Offset: 0x00167CEB
			internal virtual EntityDataReader CreateEntityDataReader(EntityCommand entityCommand, DbDataReader storeDataReader, CommandBehavior behavior)
			{
				return new EntityDataReader(entityCommand, storeDataReader, behavior);
			}
		}
	}
}
