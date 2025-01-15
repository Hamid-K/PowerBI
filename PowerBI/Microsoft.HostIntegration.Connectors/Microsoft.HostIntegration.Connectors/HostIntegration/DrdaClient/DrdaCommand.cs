using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009C0 RID: 2496
	[ToolboxItem(true)]
	public sealed class DrdaCommand : DbCommand, ICloneable
	{
		// Token: 0x06004D1C RID: 19740 RVA: 0x0013500C File Offset: 0x0013320C
		public DrdaCommand()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection), "DrdaCommand()");
			this._connection = null;
			this._transaction = null;
			this._dataReader = null;
			this._isPrepared = false;
			this._sqlCommandText = string.Empty;
			this._commandSourceId = RequesterFactory.Instance.GetCommandSourceId();
			this._timeout = new Timeout((double)(this._commandTimeout * 1000), new TimeoutCallback(this.SendTimeout), null);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x001350E0 File Offset: 0x001332E0
		public DrdaCommand(string commandText)
			: this()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection), "DrdaCommand(string)");
			this.CommandText = commandText;
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00135104 File Offset: 0x00133304
		public DrdaCommand(string commandText, DrdaConnection connection)
			: this()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection), "DrdaCommand(string, DrdaConnection)");
			this.CommandText = commandText;
			this.Connection = connection;
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x0013512F File Offset: 0x0013332F
		public DrdaCommand(string commandText, DrdaConnection connection, DrdaTransaction tx)
			: this()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection), "DrdaCommand(string, DrdaConnection, DrdaTransaction)");
			this.CommandText = commandText;
			this.Connection = connection;
			this.Transaction = tx;
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x00135164 File Offset: 0x00133364
		public DrdaCommand(DrdaCommand command)
			: this()
		{
			this.CommandText = command.CommandText;
			this.CommandType = command.CommandType;
			this.Connection = command.Connection;
			this.DesignTimeVisible = command.DesignTimeVisible;
			this.UpdatedRowSource = command.UpdatedRowSource;
			this.Transaction = command.Transaction;
			this._executed = command.Executed;
			if (command._parameterCollection != null && 0 < command._parameterCollection.Count)
			{
				DrdaParameterCollection parameters = this.Parameters;
				foreach (object obj in command.Parameters)
				{
					ICloneable cloneable = (ICloneable)obj;
					parameters.Add(cloneable.Clone());
				}
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x0013523C File Offset: 0x0013343C
		internal bool Executed
		{
			get
			{
				return this._executed;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x00135244 File Offset: 0x00133444
		internal ISqlStatement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x0013524C File Offset: 0x0013344C
		internal IRequester Requester
		{
			get
			{
				return this.Connection.Requester;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x00135259 File Offset: 0x00133459
		// (set) Token: 0x06004D25 RID: 19749 RVA: 0x00135261 File Offset: 0x00133461
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool DesignTimeVisible
		{
			get
			{
				return this._designTimeVisible;
			}
			set
			{
				this._designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x00135270 File Offset: 0x00133470
		// (set) Token: 0x06004D27 RID: 19751 RVA: 0x00135278 File Offset: 0x00133478
		[DefaultValue(UpdateRowSource.Both)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				if (value <= UpdateRowSource.Both)
				{
					this._updatedRowSource = value;
					return;
				}
				throw DrdaException.InvalidUpdateRowSource((int)value);
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x0013528C File Offset: 0x0013348C
		// (set) Token: 0x06004D29 RID: 19753 RVA: 0x00135294 File Offset: 0x00133494
		public long CommandSourceId
		{
			get
			{
				return this._commandSourceId;
			}
			set
			{
				if (this._commandSourceId != value)
				{
					this._commandSourceId = value;
					if (this._statement != null)
					{
						this._statement.CommandSourceId = this._commandSourceId;
					}
				}
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x001352BF File Offset: 0x001334BF
		internal bool Cancelled
		{
			get
			{
				return this._cancelled;
			}
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x001352C8 File Offset: 0x001334C8
		public override void Cancel()
		{
			try
			{
				this._cancelled = true;
				Task.Factory.StartNew<Task>(() => this._connection.Requester.InterruptAsync(true, CancellationToken.None));
			}
			catch
			{
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x00135308 File Offset: 0x00133508
		// (set) Token: 0x06004D2D RID: 19757 RVA: 0x00135310 File Offset: 0x00133510
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public override string CommandText
		{
			get
			{
				return this._commandText;
			}
			set
			{
				this.HandleCommandTextChange(value);
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06004D2E RID: 19758 RVA: 0x00135319 File Offset: 0x00133519
		// (set) Token: 0x06004D2F RID: 19759 RVA: 0x00135321 File Offset: 0x00133521
		public override int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw DrdaException.InvalidCommandTimeout(value);
				}
				if (value != this._commandTimeout)
				{
					this.PropertyChanging();
					this._commandTimeout = value;
					this._timeout.Interval = (double)(value * 1000);
				}
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06004D30 RID: 19760 RVA: 0x00135357 File Offset: 0x00133557
		// (set) Token: 0x06004D31 RID: 19761 RVA: 0x0013535F File Offset: 0x0013355F
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public override CommandType CommandType
		{
			get
			{
				return this._commandType;
			}
			set
			{
				if (this._commandType == value)
				{
					return;
				}
				if (value == CommandType.Text || value == CommandType.StoredProcedure || value == CommandType.TableDirect)
				{
					this._commandType = value;
					this.HandleCommandTextChange(this._commandText);
					return;
				}
				throw DrdaException.InvalidCommandType(value);
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06004D32 RID: 19762 RVA: 0x00135395 File Offset: 0x00133595
		// (set) Token: 0x06004D33 RID: 19763 RVA: 0x001353A0 File Offset: 0x001335A0
		[DefaultValue(null)]
		public new DrdaConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				if (this._connection != value)
				{
					if (this._connection != null)
					{
						this.ResetResourcesAsync(true, CancellationToken.None).GetAwaiter().GetResult();
					}
					this.PropertyChanging();
					this._connection = value;
				}
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06004D34 RID: 19764 RVA: 0x001353E4 File Offset: 0x001335E4
		// (set) Token: 0x06004D35 RID: 19765 RVA: 0x00135411 File Offset: 0x00133611
		public bool LiteralReplacement
		{
			get
			{
				if (this._literalReplacement == -1 && this._connection != null)
				{
					return this._connection.DrdaConnectionString.LiteralReplacement;
				}
				return this._literalReplacement == 1;
			}
			set
			{
				this._literalReplacement = (value ? 1 : 0);
			}
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x00135420 File Offset: 0x00133620
		private void HandleCommandTextChange(string commandText)
		{
			if (string.IsNullOrEmpty(this._commandText) || !this._commandText.Equals(commandText))
			{
				this._executed = false;
			}
			this._commandText = commandText;
			this._sqlCommandText = null;
			this.PropertyChanging();
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00135458 File Offset: 0x00133658
		internal static void SetSqlAttribute(DrdaConnection connection, ISqlStatement statement, SqlTypeEnum enumSqlType)
		{
			SqlAttribute sqlAttribute = SqlAttribute.Null;
			HostType hostType = connection.Requester.HostType;
			switch (enumSqlType)
			{
			case SqlTypeEnum.NonQuery:
				if (hostType == HostType.DB2 || hostType == HostType.MVS)
				{
					int hostVersion = connection.GetHostVersion(HOST_VERSION_ENUM.VERSION);
					int hostVersion2 = connection.GetHostVersion(HOST_VERSION_ENUM.MODIFICATION);
					if (hostVersion >= 10 && hostVersion2 >= 5)
					{
						sqlAttribute = SqlAttribute.ExternalIndicators;
					}
				}
				break;
			case SqlTypeEnum.NonQueryBatch:
				if (hostType == HostType.DB2 || hostType == HostType.MVS)
				{
					int hostVersion3 = connection.GetHostVersion(HOST_VERSION_ENUM.VERSION);
					int hostVersion4 = connection.GetHostVersion(HOST_VERSION_ENUM.MODIFICATION);
					if (hostVersion3 < 10 && hostVersion4 >= 5)
					{
						sqlAttribute = SqlAttribute.NotAtomic;
					}
					else
					{
						sqlAttribute = SqlAttribute.ExternalIndicatorsMultiRow;
					}
				}
				break;
			}
			statement.SqlAttribute = sqlAttribute;
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004D38 RID: 19768 RVA: 0x001354D8 File Offset: 0x001336D8
		private bool ConnectionIsClosed
		{
			get
			{
				DrdaConnection connection = this.Connection;
				return connection == null || connection.State == ConnectionState.Closed;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x001354FA File Offset: 0x001336FA
		// (set) Token: 0x06004D3A RID: 19770 RVA: 0x00135502 File Offset: 0x00133702
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (DrdaConnection)value;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004D3B RID: 19771 RVA: 0x00135510 File Offset: 0x00133710
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x00135518 File Offset: 0x00133718
		// (set) Token: 0x06004D3D RID: 19773 RVA: 0x00135520 File Offset: 0x00133720
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (DrdaTransaction)value;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x0013552E File Offset: 0x0013372E
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x00135536 File Offset: 0x00133736
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new DrdaParameterCollection Parameters
		{
			get
			{
				if (this._parameterCollection == null)
				{
					this._parameterCollection = new DrdaParameterCollection();
				}
				return this._parameterCollection;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x00135551 File Offset: 0x00133751
		// (set) Token: 0x06004D41 RID: 19777 RVA: 0x00135575 File Offset: 0x00133775
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DrdaTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.IsComplete)
				{
					this._transaction = null;
				}
				return this._transaction;
			}
			set
			{
				this._transaction = value;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x0013557E File Offset: 0x0013377E
		public string CursorName
		{
			get
			{
				if (this._statement != null)
				{
					return this._statement.CursorName;
				}
				return null;
			}
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00135595 File Offset: 0x00133795
		public object Clone()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return new DrdaCommand(this);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x001355AD File Offset: 0x001337AD
		public new DrdaParameter CreateParameter()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return new DrdaParameter();
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x001355C4 File Offset: 0x001337C4
		protected override DbParameter CreateDbParameter()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.CreateParameter();
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x001355DC File Offset: 0x001337DC
		private string UpdateNonQueryCommandText(string cmdtext)
		{
			if (cmdtext == null)
			{
				return null;
			}
			string databaseName = this.Connection.DatabaseName;
			if (string.IsNullOrEmpty(databaseName))
			{
				return cmdtext;
			}
			cmdtext = cmdtext.Trim();
			if (cmdtext.Length == 0)
			{
				return string.Empty;
			}
			HostType hostType = this.Connection.Requester.HostType;
			if (hostType == HostType.DB2 || hostType == HostType.MVS)
			{
				bool flag = false;
				if (cmdtext.EndsWith(";"))
				{
					flag = true;
					cmdtext = cmdtext.Substring(0, cmdtext.Length - 1);
				}
				string[] array = cmdtext.ToUpperInvariant().Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length > 1 && array[0].Equals("CREATE") && array[1].Equals("TABLE"))
				{
					if (databaseName.Contains("."))
					{
						cmdtext += " IN ";
					}
					else
					{
						cmdtext += " IN DATABASE ";
					}
					cmdtext += databaseName;
					if (flag)
					{
						cmdtext += " ; ";
					}
				}
			}
			return cmdtext;
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x001356E0 File Offset: 0x001338E0
		private async Task<int> InternalExecuteNonQueryAsync(bool isAsync, CancellationToken cancellationToken)
		{
			string originalCommandText = this.CommandText;
			int num;
			try
			{
				this._cancelled = false;
				this._timeout.Start();
				this.ValidateCommandText();
				string text = this.UpdateNonQueryCommandText(originalCommandText);
				if (string.Compare(originalCommandText, text) != 0)
				{
					this.CommandText = text;
				}
				int rowCount = 0;
				this.CheckExecutionConditions();
				await this.CheckStatementAsync(isAsync, cancellationToken);
				this._statement.SqlAttribute = SqlAttribute.Null;
				if (this.IsNeedPrepareParameters())
				{
					if (!this._isPrepared && this.CommandType != CommandType.TableDirect)
					{
						this._sqlCommandText = this.GenerateSQLCommandText();
						this.CommandText = this._sqlCommandText;
						if (string.IsNullOrEmpty(this._sqlCommandText))
						{
							this._sqlCommandText = this.CommandText;
						}
						await this._statement.PrepareAsync(this._sqlCommandText, isAsync, cancellationToken);
						this._isPrepared = true;
					}
					await this.PrepareParametersAsync(isAsync, cancellationToken);
				}
				bool flag = this.Parameters.WriteParameters(this.Statement, originalCommandText);
				if (!this._isPrepared)
				{
					this._sqlCommandText = this.GenerateSQLCommandText();
					if (flag)
					{
						DrdaCommand.SetSqlAttribute(this._connection, this.Statement, SqlTypeEnum.NonQuery);
					}
				}
				await this._statement.ExecuteAsync(this._sqlCommandText, this.Parameters.ToSqlParameters(), false, false, isAsync, cancellationToken);
				this.Parameters.ReadParameters(this._statement, this._sqlCommandText);
				rowCount = this._statement.AffectedRowCount;
				await this._statement.CloseAsync(isAsync, cancellationToken);
				await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
				num = rowCount;
			}
			finally
			{
				this._timeout.Stop();
				this._executed = true;
				this.CommandText = originalCommandText;
				this.ReleaseStatementHandleAsync(false, CancellationToken.None).GetAwaiter().GetResult();
			}
			return num;
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x00135738 File Offset: 0x00133938
		public DrdaDataReader ExecuteNonQueryIdentity()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			DrdaDataReader drdaDataReader;
			try
			{
				this._identityInsert = true;
				drdaDataReader = (DrdaDataReader)base.ExecuteReader();
			}
			finally
			{
				this._identityInsert = false;
			}
			return drdaDataReader;
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0013578C File Offset: 0x0013398C
		public async Task<DrdaDataReader> ExecuteNonQueryIdentityAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			DrdaDataReader drdaDataReader;
			try
			{
				this._identityInsert = true;
				TaskAwaiter<DbDataReader> taskAwaiter = base.ExecuteReaderAsync(cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<DbDataReader> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<DbDataReader>);
				}
				drdaDataReader = (DrdaDataReader)taskAwaiter.GetResult();
			}
			finally
			{
				this._identityInsert = false;
			}
			return drdaDataReader;
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x001357D9 File Offset: 0x001339D9
		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteNonQueryAsync(true, cancellationToken);
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x001357F4 File Offset: 0x001339F4
		public override int ExecuteNonQuery()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteNonQueryAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0013582C File Offset: 0x00133A2C
		internal void SendTimeout(object state)
		{
			try
			{
				Task.Factory.StartNew<Task>(() => this._connection.Requester.InterruptAsync(true, CancellationToken.None));
			}
			catch
			{
			}
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00135868 File Offset: 0x00133A68
		private async Task<DbDataReader> InternalExecuteDbDataReaderAsync(CommandBehavior behavior, bool isAsync, CancellationToken cancellationToken)
		{
			DbDataReader dbDataReader;
			try
			{
				this._cancelled = false;
				this._timeout.Start();
				this.CheckExecutionConditions();
				await this.CheckStatementAsync(isAsync, cancellationToken);
				if ((behavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default)
				{
					this._sqlCommandText = this.GenerateSQLCommandText();
					await this._statement.PrepareAsync(this._sqlCommandText, isAsync, cancellationToken);
				}
				else
				{
					this.Parameters.WriteParameters(this.Statement, this.CommandText);
					if (!this._isPrepared)
					{
						this._sqlCommandText = this.GenerateSQLCommandText();
						DrdaCommand.SetSqlAttribute(this._connection, this.Statement, SqlTypeEnum.Query);
					}
					await this._statement.ExecuteAsync(this._sqlCommandText, this.Parameters.ToSqlParameters(), true, this._identityInsert, isAsync, cancellationToken);
					this.Parameters.ReadParameters(this.Statement, this._sqlCommandText);
				}
				int count = this._statement.ResultSets.Count;
				int affectedRowCount = this._statement.AffectedRowCount;
				Trace.MessageVerboseTrace(Trace.GetTracePoint(this._connection), "ExecuteDbDataReader(): resultSets = {0}, rowsAffected = {1}", count, affectedRowCount);
				DrdaResultSet[] array;
				if (count > 0)
				{
					array = new DrdaResultSet[count];
					ushort num = 0;
					while ((int)num < count)
					{
						array[(int)num] = new DrdaClientResultSet(this._statement.ResultSets[(int)num], this.Connection, this._statement, (behavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly);
						((DrdaClientResultSet)array[(int)num]).Initialize();
						num += 1;
					}
				}
				else
				{
					Trace.MessageVerboseTrace(Trace.GetTracePoint(this._connection), "ExecuteDbDataReader(): creating empty result set.");
					array = new DrdaResultSet[]
					{
						new DrdaEmptyResultSet()
					};
				}
				DrdaDataReader drdaDataReader = new DrdaDataReader(this.Connection, this, this.Statement, this._sqlCommandText, behavior, affectedRowCount, array);
				this.SetDataReader(drdaDataReader);
				dbDataReader = drdaDataReader;
			}
			finally
			{
				this._executed = true;
				this._timeout.Stop();
			}
			return dbDataReader;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x001358C5 File Offset: 0x00133AC5
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteDbDataReaderAsync(behavior, true, cancellationToken);
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x001358E0 File Offset: 0x00133AE0
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteDbDataReaderAsync(behavior, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x00135917 File Offset: 0x00133B17
		public new DrdaDataReader ExecuteReader()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			return (DrdaDataReader)base.ExecuteReader();
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0013593A File Offset: 0x00133B3A
		public new DrdaDataReader ExecuteReader(CommandBehavior behavior)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			return (DrdaDataReader)base.ExecuteReader(behavior);
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x00135960 File Offset: 0x00133B60
		public new async Task<DrdaDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			TaskAwaiter<DbDataReader> taskAwaiter = base.ExecuteReaderAsync(cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DbDataReader> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DbDataReader>);
			}
			return (DrdaDataReader)taskAwaiter.GetResult();
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x001359B0 File Offset: 0x00133BB0
		public new async Task<DrdaDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			TaskAwaiter<DbDataReader> taskAwaiter = base.ExecuteReaderAsync(behavior, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DbDataReader> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DbDataReader>);
			}
			return (DrdaDataReader)taskAwaiter.GetResult();
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00135A08 File Offset: 0x00133C08
		public new async Task<DrdaDataReader> ExecuteReaderAsync()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			TaskAwaiter<DbDataReader> taskAwaiter = base.ExecuteReaderAsync().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DbDataReader> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DbDataReader>);
			}
			return (DrdaDataReader)taskAwaiter.GetResult();
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x00135A50 File Offset: 0x00133C50
		public new async Task<DrdaDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateCommandText();
			TaskAwaiter<DbDataReader> taskAwaiter = base.ExecuteReaderAsync(behavior).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DbDataReader> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DbDataReader>);
			}
			return (DrdaDataReader)taskAwaiter.GetResult();
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x00135AA0 File Offset: 0x00133CA0
		public override object ExecuteScalar()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			object obj = null;
			using (DrdaDataReader drdaDataReader = this.ExecuteReader())
			{
				if (drdaDataReader.Read() && 0 < drdaDataReader.FieldCount)
				{
					obj = drdaDataReader.GetValue(0);
				}
			}
			return obj;
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x00135AFC File Offset: 0x00133CFC
		public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			object value = null;
			DrdaDataReader drdaDataReader = await this.ExecuteReaderAsync(cancellationToken);
			using (DrdaDataReader reader = drdaDataReader)
			{
				TaskAwaiter<bool> taskAwaiter = reader.ReadAsync(cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult() && 0 < reader.FieldCount)
				{
					value = reader.GetValue(0);
				}
			}
			DrdaDataReader reader = null;
			return value;
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00135B4C File Offset: 0x00133D4C
		internal async Task InternalPrepareAsync(bool isAsync, CancellationToken cancellationToken)
		{
			try
			{
				this._cancelled = false;
				this._timeout.Start();
				this.ValidateCommandText();
				if (!this._isPrepared && this.CommandType != CommandType.TableDirect)
				{
					this.CheckExecutionConditions();
					await this.CheckStatementAsync(isAsync, cancellationToken);
					this._sqlCommandText = this.GenerateSQLCommandText();
					await this._statement.PrepareAsync(this._sqlCommandText, isAsync, cancellationToken);
					this._isPrepared = true;
				}
			}
			finally
			{
				this._timeout.Stop();
			}
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x00135BA1 File Offset: 0x00133DA1
		public Task PrepareAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalPrepareAsync(true, cancellationToken);
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x00135BBC File Offset: 0x00133DBC
		public override void Prepare()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalPrepareAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x00135BF4 File Offset: 0x00133DF4
		internal bool IsNeedPrepareParameters()
		{
			if (this._connection == null || this.Parameters.Count == 0)
			{
				return false;
			}
			bool flag = false;
			using (IEnumerator enumerator = this.Parameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((DrdaParameter)enumerator.Current).IsDataTypeSet)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x00135C6C File Offset: 0x00133E6C
		internal async Task PrepareParametersAsync(bool isAsync, CancellationToken cancellationToken)
		{
			IList<ISqlParameter> list = await this._statement.GetParametersAsync(this.CommandText, isAsync, cancellationToken);
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				ISqlParameter sqlParameter = list[i];
				this.Parameters[i].Direction = sqlParameter.Direction;
				this.Parameters[i].Scale = sqlParameter.Scale;
				this.Parameters[i].Precision = sqlParameter.Precision;
				this.Parameters[i].Size = sqlParameter.Size;
				this.Parameters[i].IsNullable = sqlParameter.IsNullable;
				this.Parameters[i].DrdaType = DataTypeConverter.ToDrdaType(sqlParameter.DrdaType);
			}
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x00135CC4 File Offset: 0x00133EC4
		internal async Task CheckStatementAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._statement != null && this._statement.Requester != this.Requester)
			{
				this._isPrepared = false;
				await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
			}
			if (this._statement == null)
			{
				this._statement = this.Connection.CheckOutStatement();
				this._statement.LiteralReplacementInvestigator = new Func<bool, bool>(this.NeedLiteralReplacement);
				this._statement.CommandSourceId = this.CommandSourceId;
			}
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x00135D19 File Offset: 0x00133F19
		private bool NeedLiteralReplacement(bool defaultValue)
		{
			if (this._literalReplacement == -1)
			{
				return defaultValue;
			}
			return this._literalReplacement == 1;
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x00135D30 File Offset: 0x00133F30
		internal void CheckExecutionConditions()
		{
			if (this.Connection == null || this.Connection.State != ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			if (this.Connection == null || this.Connection.State != ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			DrdaTransaction transaction = this.Transaction;
			DrdaTransaction transaction2 = this.Connection.Transaction;
			if (transaction != transaction2)
			{
				if (transaction2 != null)
				{
					throw DrdaException.NotPartOfTransaction();
				}
				if (!transaction.IsComplete)
				{
					throw DrdaException.NoActiveTransaction();
				}
				this.Transaction = null;
			}
			this.Connection.CheckExecuteTransactionIsolationLevel();
			if (this._dataReader != null)
			{
				throw DrdaException.OpenReaderExists();
			}
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x00135DC8 File Offset: 0x00133FC8
		private string GenerateSQLCommandText()
		{
			CommandType commandType = this.CommandType;
			if (commandType == CommandType.Text)
			{
				return this.ReplaceNamedParameters();
			}
			if (commandType == CommandType.StoredProcedure)
			{
				string text = "CALL " + this.CommandText;
				if (this.Parameters.Count > 0)
				{
					text += " (";
					for (int i = 0; i < this.Parameters.Count; i++)
					{
						text += "?";
						if (i != this.Parameters.Count - 1)
						{
							text += ", ";
						}
					}
					text += ")";
				}
				return text;
			}
			if (commandType != CommandType.TableDirect)
			{
				return this.CommandText;
			}
			if (this.CommandText.IndexOf(',') != -1)
			{
				throw DrdaException.TableListNotSupported();
			}
			return "SELECT * FROM " + this.CommandText;
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x00135EA0 File Offset: 0x001340A0
		private string ReplaceNamedParameters()
		{
			string text = this.CommandText;
			text = text.Replace("\r\n", " ") + " ";
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				DrdaParameter drdaParameter = this.Parameters[i];
				if (drdaParameter.ParameterName != null && drdaParameter.ParameterName.Length > 0 && drdaParameter.ParameterName[0] == '@')
				{
					text = text.Replace(this.Parameters[i].ParameterName + ",", "?,");
					text = text.Replace(this.Parameters[i].ParameterName + " ", "? ");
					text = text.Replace(this.Parameters[i].ParameterName + ")", "?)");
				}
			}
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x00135FA8 File Offset: 0x001341A8
		internal async Task ReleaseStatementHandleAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (!this._isPrepared && this._statement != null)
			{
				this.Parameters.PropertyChanging();
				await this.Connection.CheckInStatementAsync(this._statement, isAsync, cancellationToken);
				this._statement = null;
			}
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x00135FFD File Offset: 0x001341FD
		private void SetDataReader(DrdaDataReader reader)
		{
			this._dataReader = reader;
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x00136008 File Offset: 0x00134208
		internal async Task RemoveDataReaderAsync(DrdaDataReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this._dataReader = null;
			await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x00136060 File Offset: 0x00134260
		private async Task DisconnectDataReader(DrdaDataReader dataReader, bool isAsync, CancellationToken cancellationToken)
		{
			if (dataReader != null && this.Statement != null)
			{
				Trace.MessageTrace(Trace.GetTracePoint(this._connection), "DisconnectDataReaders(): using a data reader with Statement = {0}", this.Statement);
				if (this.Connection.IsClosing)
				{
					Trace.MessageTrace(Trace.GetTracePoint(this._connection), "DisconnectDataReaders(): closing data reader because connection is closing.");
					await dataReader.InternalCloseAsync(isAsync, cancellationToken);
				}
				else
				{
					Trace.MessageTrace(Trace.GetTracePoint(this._connection), "DisconnectDataReaders(): data reader will own the statement.");
					this._statement = null;
				}
			}
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x001360C0 File Offset: 0x001342C0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._connection != null)
				{
					this.CloseInternalAsync(true, CancellationToken.None).GetAwaiter().GetResult();
				}
				this._transaction = null;
				this._cancelled = false;
				this._parameterCollection = null;
				this._connection = null;
				this._sqlCommandText = string.Empty;
				this._commandText = string.Empty;
				this._commandType = CommandType.Text;
				this._timeout.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x0013613C File Offset: 0x0013433C
		internal async Task CloseInternalAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.ResetResourcesAsync(isAsync, cancellationToken);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00136194 File Offset: 0x00134394
		private async Task ResetResourcesAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.DisconnectDataReader(this._dataReader, isAsync, cancellationToken);
			this.ReleaseBuffer();
			this._dataReader = null;
			this._isPrepared = false;
			this._isBatchStarted = false;
			if (this.Statement != null)
			{
				await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
				this._statement = null;
			}
			this._transaction = null;
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x001361E9 File Offset: 0x001343E9
		private void PropertyChanging()
		{
			this._isPrepared = false;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x001361F2 File Offset: 0x001343F2
		private void ValidateCommandText()
		{
			if (this.CommandText == null || this.CommandText.Trim().Length == 0)
			{
				throw DrdaException.CommandTextNotSet();
			}
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x00136214 File Offset: 0x00134414
		internal void StartTimeout()
		{
			this._timeout.Start();
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x00136221 File Offset: 0x00134421
		internal void StopTimeout()
		{
			this._timeout.Stop();
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00136230 File Offset: 0x00134430
		public int AddToBatch()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			object[] array = new object[this.Parameters.Count];
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				array[i] = this.Parameters[i].Value;
			}
			return this.AddToBatch(array);
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x00136290 File Offset: 0x00134490
		public int AddToBatch(object[] values)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.ValidateParameters();
			if (values.Length != this.Parameters.Count)
			{
				throw DrdaException.CommandBatchValuesToParameters(this.Parameters.Count, values.Length);
			}
			this._dataList.Add(values);
			return this._dataList.Count;
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x001362F0 File Offset: 0x001344F0
		public int ExecuteBatch()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteBatchAsync(false, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x00136328 File Offset: 0x00134528
		public int ExecuteBatch(bool done)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteBatchAsync(done, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0013635F File Offset: 0x0013455F
		public Task<int> ExecuteBatchAsync(bool done, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteBatchAsync(done, true, cancellationToken);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0013637A File Offset: 0x0013457A
		public Task<int> ExecuteBatchAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteBatchAsync(false, true, cancellationToken);
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00136398 File Offset: 0x00134598
		private async Task<int> InternalExecuteBatchAsync(bool done, bool isAsync, CancellationToken cancellationToken)
		{
			string originalCommandText = this.CommandText;
			Exception exception = null;
			int rows = 0;
			try
			{
				this._cancelled = false;
				this._timeout.Start();
				if ((!this._isPrepared || !this._isBatchStarted) && this.CommandType != CommandType.TableDirect)
				{
					this.ValidateCommandText();
					this.CommandText = this.UpdateNonQueryCommandText(originalCommandText);
					this.CheckExecutionConditions();
					await this.CheckStatementAsync(isAsync, cancellationToken);
					DrdaCommand.SetSqlAttribute(this._connection, this._statement, SqlTypeEnum.NonQueryBatch);
					await this._statement.PrepareAsync(this.CommandText, isAsync, cancellationToken);
					this._isPrepared = true;
					if (this.IsNeedPrepareParameters())
					{
						await this.PrepareParametersAsync(isAsync, cancellationToken);
					}
					this._isBatchStarted = true;
				}
				rows = await this.CommitAsync(isAsync, cancellationToken);
			}
			catch (Exception ex)
			{
				done = true;
				exception = ex;
			}
			finally
			{
				this._timeout.Stop();
			}
			this._executed = true;
			this.CommandText = originalCommandText;
			this.ReleaseBuffer();
			if (done)
			{
				await this._statement.CloseAsync(isAsync, cancellationToken);
				await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
				this._isPrepared = false;
				this._isBatchStarted = false;
			}
			else
			{
				this._isPrepared = true;
			}
			if (exception != null)
			{
				throw exception;
			}
			return rows;
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x001363F5 File Offset: 0x001345F5
		private bool ValidateParameters()
		{
			if (this.Parameters.Count <= 0)
			{
				throw DrdaException.CommandBatchNoParameters();
			}
			return true;
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0013640C File Offset: 0x0013460C
		private void ReleaseBuffer()
		{
			this._dataList.Clear();
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x0013641C File Offset: 0x0013461C
		private async Task<int> CommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			int numRows = this._dataList.Count;
			int num;
			if (numRows == 0)
			{
				num = numRows;
			}
			else
			{
				this._timeout.Start();
				try
				{
					await this._statement.InsertRowsAsync(this._dataList, isAsync, cancellationToken);
				}
				finally
				{
					this._timeout.Stop();
					this.ReleaseBuffer();
				}
				num = numRows;
			}
			return num;
		}

		// Token: 0x04003D1C RID: 15644
		private DrdaConnection _connection;

		// Token: 0x04003D1D RID: 15645
		private DrdaTransaction _transaction;

		// Token: 0x04003D1E RID: 15646
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04003D1F RID: 15647
		private bool _designTimeVisible = true;

		// Token: 0x04003D20 RID: 15648
		private static int _objectTypeCount;

		// Token: 0x04003D21 RID: 15649
		internal readonly int _objectID = Interlocked.Increment(ref DrdaCommand._objectTypeCount);

		// Token: 0x04003D22 RID: 15650
		private int _commandTimeout = 30;

		// Token: 0x04003D23 RID: 15651
		private bool _cancelled;

		// Token: 0x04003D24 RID: 15652
		private bool _executed;

		// Token: 0x04003D25 RID: 15653
		private long _commandSourceId;

		// Token: 0x04003D26 RID: 15654
		private bool _identityInsert;

		// Token: 0x04003D27 RID: 15655
		private int _literalReplacement = -1;

		// Token: 0x04003D28 RID: 15656
		private DrdaParameterCollection _parameterCollection;

		// Token: 0x04003D29 RID: 15657
		private DrdaDataReader _dataReader;

		// Token: 0x04003D2A RID: 15658
		private string _sqlCommandText;

		// Token: 0x04003D2B RID: 15659
		private bool _isPrepared;

		// Token: 0x04003D2C RID: 15660
		private ISqlStatement _statement;

		// Token: 0x04003D2D RID: 15661
		private string _commandText = string.Empty;

		// Token: 0x04003D2E RID: 15662
		private CommandType _commandType = CommandType.Text;

		// Token: 0x04003D2F RID: 15663
		private Timeout _timeout;

		// Token: 0x04003D30 RID: 15664
		private List<object[]> _dataList = new List<object[]>();

		// Token: 0x04003D31 RID: 15665
		private bool _isBatchStarted;
	}
}
