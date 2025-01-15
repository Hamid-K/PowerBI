using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000073 RID: 115
	[DefaultEvent("RowUpdated")]
	[DesignerCategory("")]
	public sealed class SqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001B7FE File Offset: 0x000199FE
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001B806 File Offset: 0x00019A06
		public SqlDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001B82B File Offset: 0x00019A2B
		public SqlDataAdapter(SqlCommand selectCommand)
			: this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001B83C File Offset: 0x00019A3C
		public SqlDataAdapter(string selectCommandText, string selectConnectionString)
			: this()
		{
			SqlConnection sqlConnection = new SqlConnection(selectConnectionString);
			this.SelectCommand = new SqlCommand(selectCommandText, sqlConnection);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001B863 File Offset: 0x00019A63
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection)
			: this()
		{
			this.SelectCommand = new SqlCommand(selectCommandText, selectConnection);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001B878 File Offset: 0x00019A78
		private SqlDataAdapter(SqlDataAdapter from)
			: base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001B89E File Offset: 0x00019A9E
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0001B8A6 File Offset: 0x00019AA6
		[DefaultValue(null)]
		[ResCategory("Update")]
		[ResDescription("Used during Update for deleted rows in DataSet.")]
		public new SqlCommand DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001B89E File Offset: 0x00019A9E
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0001B8AF File Offset: 0x00019AAF
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0001B8C5 File Offset: 0x00019AC5
		[DefaultValue(null)]
		[ResCategory("Update")]
		[ResDescription("Used during Update for new rows in DataSet.")]
		public new SqlCommand InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0001B8CE File Offset: 0x00019ACE
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001B8DC File Offset: 0x00019ADC
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		[DefaultValue(null)]
		[ResCategory("Fill")]
		[ResDescription("Used during Fill/FillSchema.")]
		public new SqlCommand SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001B8DC File Offset: 0x00019ADC
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0001B8ED File Offset: 0x00019AED
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001B8FB File Offset: 0x00019AFB
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0001B903 File Offset: 0x00019B03
		[DefaultValue(null)]
		[ResCategory("Update")]
		[ResDescription("Used during Update for modified rows in DataSet.")]
		public new SqlCommand UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001B8FB File Offset: 0x00019AFB
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0001B90C File Offset: 0x00019B0C
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (SqlCommand)value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001B91A File Offset: 0x00019B1A
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0001B922 File Offset: 0x00019B22
		public override int UpdateBatchSize
		{
			get
			{
				return this._updateBatchSize;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange("UpdateBatchSize");
				}
				this._updateBatchSize = value;
				SqlClientEventSource.Log.TryTraceEvent<int, int>("SqlDataAdapter.Set_UpdateBatchSize | API | Object Id {0}, Update Batch Size value {1}", this.ObjectID, value);
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001B950 File Offset: 0x00019B50
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((SqlCommand)command);
			return commandCount;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001B97B File Offset: 0x00019B7B
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001B988 File Offset: 0x00019B88
		protected override int ExecuteBatch()
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId, int>("SqlDataAdapter.ExecuteBatch | Info | Correlation | Object Id {0}, Activity Id {1}, Command Count {2}", this.ObjectID, ActivityCorrelator.Current, this._commandSet.CommandCount);
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001B9BC File Offset: 0x00019BBC
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this._commandSet.GetBatchedAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		protected override void InitializeBatching()
		{
			SqlClientEventSource.Log.TryTraceEvent<int>("SqlDataAdapter.InitializeBatching | API | Object Id {0}", this.ObjectID);
			this._commandSet = new SqlCommandSet();
			SqlCommand sqlCommand = this.SelectCommand;
			if (sqlCommand == null)
			{
				sqlCommand = this.InsertCommand;
				if (sqlCommand == null)
				{
					sqlCommand = this.UpdateCommand;
					if (sqlCommand == null)
					{
						sqlCommand = this.DeleteCommand;
					}
				}
			}
			if (sqlCommand != null)
			{
				this._commandSet.Connection = sqlCommand.Connection;
				this._commandSet.Transaction = sqlCommand.Transaction;
				this._commandSet.CommandTimeout = sqlCommand.CommandTimeout;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001BA70 File Offset: 0x00019C70
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001BA8C File Offset: 0x00019C8C
		object ICloneable.Clone()
		{
			return new SqlDataAdapter(this);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001BA94 File Offset: 0x00019C94
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001BAA0 File Offset: 0x00019CA0
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000A5F RID: 2655 RVA: 0x0001BAAC File Offset: 0x00019CAC
		// (remove) Token: 0x06000A60 RID: 2656 RVA: 0x0001BABF File Offset: 0x00019CBF
		[ResCategory("Update")]
		[ResDescription("Event triggered before every DataRow during Update.")]
		public event SqlRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(SqlDataAdapter.s_eventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.s_eventRowUpdated, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000A61 RID: 2657 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		// (remove) Token: 0x06000A62 RID: 2658 RVA: 0x0001BB38 File Offset: 0x00019D38
		[ResCategory("Update")]
		[ResDescription("Event triggered after every DataRow during Update.")]
		public event SqlRowUpdatingEventHandler RowUpdating
		{
			add
			{
				SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.s_eventRowUpdating];
				if (sqlRowUpdatingEventHandler != null && value.Target is DbCommandBuilder)
				{
					SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler2 = (SqlRowUpdatingEventHandler)ADP.FindBuilder(sqlRowUpdatingEventHandler);
					if (sqlRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(SqlDataAdapter.s_eventRowUpdating, sqlRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(SqlDataAdapter.s_eventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.s_eventRowUpdating, value);
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001BB4C File Offset: 0x00019D4C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			SqlRowUpdatedEventHandler sqlRowUpdatedEventHandler = (SqlRowUpdatedEventHandler)base.Events[SqlDataAdapter.s_eventRowUpdated];
			if (sqlRowUpdatedEventHandler != null)
			{
				SqlRowUpdatedEventArgs sqlRowUpdatedEventArgs = value as SqlRowUpdatedEventArgs;
				if (sqlRowUpdatedEventArgs != null)
				{
					sqlRowUpdatedEventHandler(this, sqlRowUpdatedEventArgs);
				}
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001BB8C File Offset: 0x00019D8C
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.s_eventRowUpdating];
			if (sqlRowUpdatingEventHandler != null)
			{
				SqlRowUpdatingEventArgs sqlRowUpdatingEventArgs = value as SqlRowUpdatingEventArgs;
				if (sqlRowUpdatingEventArgs != null)
				{
					sqlRowUpdatingEventHandler(this, sqlRowUpdatingEventArgs);
				}
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x04000215 RID: 533
		private static readonly object s_eventRowUpdated = new object();

		// Token: 0x04000216 RID: 534
		private static readonly object s_eventRowUpdating = new object();

		// Token: 0x04000217 RID: 535
		private SqlCommand _deleteCommand;

		// Token: 0x04000218 RID: 536
		private SqlCommand _insertCommand;

		// Token: 0x04000219 RID: 537
		private SqlCommand _selectCommand;

		// Token: 0x0400021A RID: 538
		private SqlCommand _updateCommand;

		// Token: 0x0400021B RID: 539
		private SqlCommandSet _commandSet;

		// Token: 0x0400021C RID: 540
		private int _updateBatchSize = 1;

		// Token: 0x0400021D RID: 541
		private static int s_objectTypeCount;

		// Token: 0x0400021E RID: 542
		internal readonly int _objectID = Interlocked.Increment(ref SqlDataAdapter.s_objectTypeCount);
	}
}
