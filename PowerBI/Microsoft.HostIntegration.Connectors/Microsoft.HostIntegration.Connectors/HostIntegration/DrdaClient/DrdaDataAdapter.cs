using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009F1 RID: 2545
	[DefaultEvent("RowUpdated")]
	public sealed class DrdaDataAdapter : DbDataAdapter, ICloneable
	{
		// Token: 0x06004F4E RID: 20302 RVA: 0x0013EB61 File Offset: 0x0013CD61
		public DrdaDataAdapter()
		{
			Trace.ApiEnterTrace("DrdaDataAdapter()");
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0013EB80 File Offset: 0x0013CD80
		public DrdaDataAdapter(DrdaCommand selectCommand)
			: this()
		{
			Trace.ApiEnterTrace("DrdaDataAdapter(DrdaCommand)");
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0013EB9C File Offset: 0x0013CD9C
		public DrdaDataAdapter(string selectCommandText, string selectConnectionString)
			: this()
		{
			Trace.ApiEnterTrace("DrdaDataAdapter(string, string)");
			DrdaConnection drdaConnection = new DrdaConnection(selectConnectionString);
			this.SelectCommand = new DrdaCommand();
			this.SelectCommand.Connection = drdaConnection;
			this.SelectCommand.CommandText = selectCommandText;
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x0013EBE3 File Offset: 0x0013CDE3
		public DrdaDataAdapter(string selectCommandText, DrdaConnection selectConnection)
			: this()
		{
			Trace.ApiEnterTrace("DrdaDataAdapter(string, DrdaConnection)");
			this.SelectCommand = new DrdaCommand();
			this.SelectCommand.Connection = selectConnection;
			this.SelectCommand.CommandText = selectCommandText;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0013EC18 File Offset: 0x0013CE18
		public DrdaDataAdapter(DrdaDataAdapter from)
			: base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x0013EC2E File Offset: 0x0013CE2E
		// (set) Token: 0x06004F54 RID: 20308 RVA: 0x0013EC3B File Offset: 0x0013CE3B
		[DefaultValue(null)]
		public new DrdaCommand DeleteCommand
		{
			get
			{
				return base.DeleteCommand as DrdaCommand;
			}
			set
			{
				base.DeleteCommand = value;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x0013EC44 File Offset: 0x0013CE44
		// (set) Token: 0x06004F56 RID: 20310 RVA: 0x0013EC51 File Offset: 0x0013CE51
		[DefaultValue(null)]
		public new DrdaCommand InsertCommand
		{
			get
			{
				return base.InsertCommand as DrdaCommand;
			}
			set
			{
				base.InsertCommand = value;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x0013EC5A File Offset: 0x0013CE5A
		// (set) Token: 0x06004F58 RID: 20312 RVA: 0x0013EC67 File Offset: 0x0013CE67
		[DefaultValue(null)]
		public new DrdaCommand SelectCommand
		{
			get
			{
				return base.SelectCommand as DrdaCommand;
			}
			set
			{
				base.SelectCommand = value;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x0013EC70 File Offset: 0x0013CE70
		// (set) Token: 0x06004F5A RID: 20314 RVA: 0x0013EC67 File Offset: 0x0013CE67
		internal DrdaSchemaCommand SchemaSelectCommand
		{
			get
			{
				return base.SelectCommand as DrdaSchemaCommand;
			}
			set
			{
				base.SelectCommand = value;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x0013EC7D File Offset: 0x0013CE7D
		// (set) Token: 0x06004F5C RID: 20316 RVA: 0x0013EC8A File Offset: 0x0013CE8A
		[DefaultValue(null)]
		public new DrdaCommand UpdateCommand
		{
			get
			{
				return base.UpdateCommand as DrdaCommand;
			}
			set
			{
				base.UpdateCommand = value;
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004F5D RID: 20317 RVA: 0x0013EC93 File Offset: 0x0013CE93
		// (set) Token: 0x06004F5E RID: 20318 RVA: 0x0013EC9B File Offset: 0x0013CE9B
		public override int UpdateBatchSize
		{
			get
			{
				return this.updateBatchSize;
			}
			set
			{
				this.updateBatchSize = value;
			}
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x0013ECA4 File Offset: 0x0013CEA4
		protected override void InitializeBatching()
		{
			if (this.batchProcessor != null)
			{
				this.ClearBatch();
				return;
			}
			this.batchProcessor = new DrdaBatchProcessor();
			this.batchProcessor.InitializeBatch();
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0013ECCB File Offset: 0x0013CECB
		protected override void TerminateBatching()
		{
			this.ClearBatch();
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0013ECD3 File Offset: 0x0013CED3
		protected override int AddToBatch(IDbCommand command)
		{
			return this.batchProcessor.AddToBatch(command as DrdaCommand);
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0013ECE6 File Offset: 0x0013CEE6
		protected override void ClearBatch()
		{
			this.batchProcessor.ClearBatch();
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x000189CC File Offset: 0x00016BCC
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return null;
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x0013ECF3 File Offset: 0x0013CEF3
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this.batchProcessor.GetBatchedRecordsAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0013ED03 File Offset: 0x0013CF03
		protected override int ExecuteBatch()
		{
			return this.batchProcessor.ExecuteBatch();
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0013ED10 File Offset: 0x0013CF10
		object ICloneable.Clone()
		{
			return new DrdaDataAdapter(this);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x0013ED18 File Offset: 0x0013CF18
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new DrdaRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0013ED24 File Offset: 0x0013CF24
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new DrdaRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0013ED30 File Offset: 0x0013CF30
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			DrdaRowUpdatedEventHandler drdaRowUpdatedEventHandler = (DrdaRowUpdatedEventHandler)base.Events[DrdaDataAdapter.EventRowUpdated];
			if (drdaRowUpdatedEventHandler != null && value is DrdaRowUpdatedEventArgs)
			{
				drdaRowUpdatedEventHandler(this, (DrdaRowUpdatedEventArgs)value);
			}
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0013ED6C File Offset: 0x0013CF6C
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			DrdaRowUpdatingEventHandler drdaRowUpdatingEventHandler = (DrdaRowUpdatingEventHandler)base.Events[DrdaDataAdapter.EventRowUpdating];
			if (drdaRowUpdatingEventHandler != null && value is DrdaRowUpdatingEventArgs)
			{
				drdaRowUpdatingEventHandler(this, (DrdaRowUpdatingEventArgs)value);
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06004F6B RID: 20331 RVA: 0x0013EDA7 File Offset: 0x0013CFA7
		// (remove) Token: 0x06004F6C RID: 20332 RVA: 0x0013EDBA File Offset: 0x0013CFBA
		public event DrdaRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(DrdaDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(DrdaDataAdapter.EventRowUpdated, value);
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06004F6D RID: 20333 RVA: 0x0013EDCD File Offset: 0x0013CFCD
		// (remove) Token: 0x06004F6E RID: 20334 RVA: 0x0013EDF6 File Offset: 0x0013CFF6
		public event DrdaRowUpdatingEventHandler RowUpdating
		{
			add
			{
				DrdaRowUpdatingEventHandler drdaRowUpdatingEventHandler = (DrdaRowUpdatingEventHandler)base.Events[DrdaDataAdapter.EventRowUpdating];
				base.Events.AddHandler(DrdaDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(DrdaDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x04003F02 RID: 16130
		internal static readonly object EventRowUpdated = new object();

		// Token: 0x04003F03 RID: 16131
		internal static readonly object EventRowUpdating = new object();

		// Token: 0x04003F04 RID: 16132
		private DrdaBatchProcessor batchProcessor;

		// Token: 0x04003F05 RID: 16133
		private int updateBatchSize = 1;
	}
}
