using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E2 RID: 226
	[DefaultEvent("RecordsAffected")]
	[ToolboxItem(true)]
	[DesignerCategory("")]
	public sealed class SqlCommand : DbCommand, ICloneable
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00034B1D File Offset: 0x00032D1D
		internal bool InPrepare
		{
			get
			{
				return this._inPrepare;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00034B25 File Offset: 0x00032D25
		internal bool IsColumnEncryptionEnabled
		{
			get
			{
				return (this._columnEncryptionSetting == SqlCommandColumnEncryptionSetting.Enabled || (this._columnEncryptionSetting == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._activeConnection.IsColumnEncryptionSettingEnabled)) && this._activeConnection.Parser != null && this._activeConnection.Parser.IsColumnEncryptionSupported;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00034B64 File Offset: 0x00032D64
		internal bool ShouldUseEnclaveBasedWorkflow
		{
			get
			{
				return (!string.IsNullOrWhiteSpace(this._activeConnection.EnclaveAttestationUrl) || this.Connection.AttestationProtocol == SqlConnectionAttestationProtocol.None) && this.IsColumnEncryptionEnabled;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00034B8E File Offset: 0x00032D8E
		private bool ShouldCacheEncryptionMetadata
		{
			get
			{
				return !this.requiresEnclaveComputations || this._activeConnection.Parser.AreEnclaveRetriesSupported;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00034BAA File Offset: 0x00032DAA
		internal bool HasColumnEncryptionKeyStoreProvidersRegistered
		{
			get
			{
				return this._customColumnEncryptionKeyStoreProviders != null && this._customColumnEncryptionKeyStoreProviders.Count > 0;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00034BC4 File Offset: 0x00032DC4
		private SqlCommand.CachedAsyncState cachedAsyncState
		{
			get
			{
				if (this._cachedAsyncState == null)
				{
					this._cachedAsyncState = new SqlCommand.CachedAsyncState();
				}
				return this._cachedAsyncState;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00034BDF File Offset: 0x00032DDF
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x00034BE7 File Offset: 0x00032DE7
		internal bool IsDescribeParameterEncryptionRPCCurrentlyInProgress { get; private set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00034BF0 File Offset: 0x00032DF0
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x00034BF8 File Offset: 0x00032DF8
		internal bool CachingQueryMetadataPostponed { get; set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00034C01 File Offset: 0x00032E01
		private SqlCommand.CommandEventSink EventSink
		{
			get
			{
				if (this._smiEventSink == null)
				{
					this._smiEventSink = new SqlCommand.CommandEventSink(this);
				}
				this._smiEventSink.Parent = this.InternalSmiConnection.CurrentEventSink;
				return this._smiEventSink;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00034C33 File Offset: 0x00032E33
		private SmiEventSink_DeferedProcessing OutParamEventSink
		{
			get
			{
				if (this._outParamEventSink == null)
				{
					this._outParamEventSink = new SmiEventSink_DeferedProcessing(this.EventSink);
				}
				else
				{
					this._outParamEventSink.Parent = this.EventSink;
				}
				return this._outParamEventSink;
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00034C68 File Offset: 0x00032E68
		public SqlCommand()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00034CC2 File Offset: 0x00032EC2
		public SqlCommand(string cmdText)
			: this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00034CD1 File Offset: 0x00032ED1
		public SqlCommand(string cmdText, SqlConnection connection)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00034CE7 File Offset: 0x00032EE7
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00034D04 File Offset: 0x00032F04
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
			this._columnEncryptionSetting = columnEncryptionSetting;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00034D2C File Offset: 0x00032F2C
		private SqlCommand(SqlCommand from)
			: this()
		{
			this.CommandText = from.CommandText;
			this.CommandTimeout = from.CommandTimeout;
			this.CommandType = from.CommandType;
			this.Connection = from.Connection;
			this.DesignTimeVisible = from.DesignTimeVisible;
			this.Transaction = from.Transaction;
			this.UpdatedRowSource = from.UpdatedRowSource;
			this._columnEncryptionSetting = from.ColumnEncryptionSetting;
			SqlParameterCollection parameters = this.Parameters;
			foreach (object obj in from.Parameters)
			{
				parameters.Add((obj is ICloneable) ? (obj as ICloneable).Clone() : obj);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00034E04 File Offset: 0x00033004
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x00034E0C File Offset: 0x0003300C
		[DefaultValue(null)]
		[ResCategory("Data")]
		[ResDescription("Connection used by the command.")]
		public new SqlConnection Connection
		{
			get
			{
				return this._activeConnection;
			}
			set
			{
				if (this._activeConnection != value && this._activeConnection != null && this._cachedAsyncState != null && this._cachedAsyncState.PendingAsyncOperation)
				{
					throw SQL.CannotModifyPropertyAsyncOperationInProgress("Connection");
				}
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				this._smiRequestContext = null;
				if (this.IsPrepared && this._activeConnection != value && this._activeConnection != null)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.Unprepare();
					}
					catch (OutOfMemoryException)
					{
						this._activeConnection.InnerConnection.DoomThisConnection();
						throw;
					}
					catch (StackOverflowException)
					{
						this._activeConnection.InnerConnection.DoomThisConnection();
						throw;
					}
					catch (ThreadAbortException)
					{
						this._activeConnection.InnerConnection.DoomThisConnection();
						throw;
					}
					catch (Exception)
					{
					}
					finally
					{
						this._prepareHandle = -1;
						this._execType = SqlCommand.EXECTYPE.UNPREPARED;
					}
				}
				this._activeConnection = value;
				SqlClientEventSource.Log.TryTraceEvent<int, int?>("<sc.SqlCommand.set_Connection|API> {0}, {1}", this.ObjectID, (value != null) ? new int?(value.ObjectID) : null);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00034F54 File Offset: 0x00033154
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00034F5C File Offset: 0x0003315C
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (SqlConnection)value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00034F6A File Offset: 0x0003316A
		private SqlInternalConnectionSmi InternalSmiConnection
		{
			get
			{
				return (SqlInternalConnectionSmi)this._activeConnection.InnerConnection;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00034F7C File Offset: 0x0003317C
		private SqlInternalConnectionTds InternalTdsConnection
		{
			get
			{
				return (SqlInternalConnectionTds)this._activeConnection.InnerConnection;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00034F8E File Offset: 0x0003318E
		private bool Is2000
		{
			get
			{
				return this._activeConnection != null && this._activeConnection.Is2000;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00034FA5 File Offset: 0x000331A5
		private bool IsProviderRetriable
		{
			get
			{
				return SqlConfigurableRetryFactory.IsRetriable(this.RetryLogicProvider);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00034FB2 File Offset: 0x000331B2
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00034FCD File Offset: 0x000331CD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlRetryLogicBaseProvider RetryLogicProvider
		{
			get
			{
				if (this._retryLogicProvider == null)
				{
					this._retryLogicProvider = SqlConfigurableRetryLogicManager.CommandProvider;
				}
				return this._retryLogicProvider;
			}
			set
			{
				this._retryLogicProvider = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00034FD6 File Offset: 0x000331D6
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x00034FDE File Offset: 0x000331DE
		[DefaultValue(true)]
		[ResCategory("Notification")]
		[ResDescription("Automatic enlistment in notifications used by Microsoft SQL Server.")]
		public bool NotificationAutoEnlist
		{
			get
			{
				return this._notificationAutoEnlist;
			}
			set
			{
				this._notificationAutoEnlist = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00034FE7 File Offset: 0x000331E7
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x00034FEF File Offset: 0x000331EF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("Notification")]
		[ResDescription("Notification values used by Microsoft SQL Server.")]
		public SqlNotificationRequest Notification
		{
			get
			{
				return this._notification;
			}
			set
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.set_Notification|API> {0}", this.ObjectID);
				this._sqlDep = null;
				this._notification = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00035014 File Offset: 0x00033214
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._activeConnection != null && this._activeConnection.StatisticsEnabled)
				{
					return this._activeConnection.Statistics;
				}
				return null;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00035038 File Offset: 0x00033238
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0003505C File Offset: 0x0003325C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("The transaction used by the command.")]
		public new SqlTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				return this._transaction;
			}
			set
			{
				if (this._transaction != value && this._activeConnection != null && this.cachedAsyncState.PendingAsyncOperation)
				{
					throw SQL.CannotModifyPropertyAsyncOperationInProgress("Transaction");
				}
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.set_Transaction|API> {0}", this.ObjectID);
				this._transaction = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000350AE File Offset: 0x000332AE
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x000350B6 File Offset: 0x000332B6
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (SqlTransaction)value;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x000350C4 File Offset: 0x000332C4
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x000350D5 File Offset: 0x000332D5
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("Data")]
		[ResDescription("Command text to execute.")]
		public override string CommandText
		{
			get
			{
				return this._commandText ?? "";
			}
			set
			{
				SqlClientEventSource.Log.TryTraceEvent<int, string>("<sc.SqlCommand.set_CommandText|API> {0}, String Value = '{1}'", this.ObjectID, value);
				if (this._commandText != value)
				{
					this.PropertyChanging();
					this._commandText = value;
				}
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00035108 File Offset: 0x00033308
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("Data")]
		[ResDescription("Column encryption setting for the command. Overrides the connection level default.")]
		public SqlCommandColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				return this._columnEncryptionSetting;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00035110 File Offset: 0x00033310
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0003513C File Offset: 0x0003333C
		[ResCategory("Data")]
		[ResDescription("Time to wait for command to execute.")]
		public override int CommandTimeout
		{
			get
			{
				int? commandTimeout = this._commandTimeout;
				if (commandTimeout == null)
				{
					return this.DefaultCommandTimeout;
				}
				return commandTimeout.GetValueOrDefault();
			}
			set
			{
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlCommand.set_CommandTimeout|API> {0}, {1}", this.ObjectID, value);
				if (value < 0)
				{
					throw ADP.InvalidCommandTimeout(value, "CommandTimeout");
				}
				int? commandTimeout = this._commandTimeout;
				if (!((value == commandTimeout.GetValueOrDefault()) & (commandTimeout != null)))
				{
					this.PropertyChanging();
					this._commandTimeout = new int?(value);
				}
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003519C File Offset: 0x0003339C
		public void ResetCommandTimeout()
		{
			if (30 != this.CommandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = new int?(this.DefaultCommandTimeout);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x000351BF File Offset: 0x000333BF
		private int DefaultCommandTimeout
		{
			get
			{
				SqlConnection activeConnection = this._activeConnection;
				if (activeConnection == null)
				{
					return 30;
				}
				return activeConnection.CommandTimeout;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000351D4 File Offset: 0x000333D4
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x000351F0 File Offset: 0x000333F0
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("Data")]
		[ResDescription("How to interpret the CommandText.")]
		public override CommandType CommandType
		{
			get
			{
				CommandType commandType = this._commandType;
				if (commandType == (CommandType)0)
				{
					return CommandType.Text;
				}
				return commandType;
			}
			set
			{
				SqlClientEventSource.Log.TryTraceEvent<int, int, CommandType>("<sc.SqlCommand.set_CommandType|API> {0}, {1}{2}", this.ObjectID, (int)value, this._commandType);
				if (this._commandType == value)
				{
					return;
				}
				if (value == CommandType.Text || value == CommandType.StoredProcedure)
				{
					this.PropertyChanging();
					this._commandType = value;
					return;
				}
				if (value != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(value);
				}
				throw SQL.NotSupportedCommandType(value);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00035250 File Offset: 0x00033450
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x0003525B File Offset: 0x0003345B
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool DesignTimeVisible
		{
			get
			{
				return !this._designTimeInvisible;
			}
			set
			{
				this._designTimeInvisible = !value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003526D File Offset: 0x0003346D
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00035275 File Offset: 0x00033475
		public bool EnableOptimizedParameterBinding { get; set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003527E File Offset: 0x0003347E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResCategory("Data")]
		[ResDescription("The parameters collection.")]
		public new SqlParameterCollection Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new SqlParameterCollection();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00035299 File Offset: 0x00033499
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000352A4 File Offset: 0x000334A4
		internal static void CancelIgnoreFailureCallback(object state)
		{
			SqlCommand sqlCommand = (SqlCommand)state;
			sqlCommand.CancelIgnoreFailure();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000352C0 File Offset: 0x000334C0
		internal void CancelIgnoreFailure()
		{
			try
			{
				this.Cancel();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000352E8 File Offset: 0x000334E8
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000352F0 File Offset: 0x000334F0
		[DefaultValue(UpdateRowSource.Both)]
		[ResCategory("Update")]
		[ResDescription("When used by a DataAdapter.Update, how command results are applied to the current DataRow.")]
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
				throw ADP.InvalidUpdateRowSource(value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000FE6 RID: 4070 RVA: 0x00035304 File Offset: 0x00033504
		// (remove) Token: 0x06000FE7 RID: 4071 RVA: 0x0003531D File Offset: 0x0003351D
		[ResCategory("StatementCompleted")]
		[ResDescription("When records are affected by a given statement by the execution of the command.")]
		public event StatementCompletedEventHandler StatementCompleted
		{
			add
			{
				this._statementCompletedEventHandler = (StatementCompletedEventHandler)Delegate.Combine(this._statementCompletedEventHandler, value);
			}
			remove
			{
				this._statementCompletedEventHandler = (StatementCompletedEventHandler)Delegate.Remove(this._statementCompletedEventHandler, value);
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00035338 File Offset: 0x00033538
		internal void OnStatementCompleted(int recordCount)
		{
			if (0 <= recordCount)
			{
				StatementCompletedEventHandler statementCompletedEventHandler = this._statementCompletedEventHandler;
				if (statementCompletedEventHandler != null)
				{
					try
					{
						SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlCommand.OnStatementCompleted|INFO> {0}, recordCount={1}", this.ObjectID, recordCount);
						statementCompletedEventHandler(this, new StatementCompletedEventArgs(recordCount));
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableOrSecurityExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
					}
				}
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003539C File Offset: 0x0003359C
		private void PropertyChanging()
		{
			this.IsDirty = true;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000353A8 File Offset: 0x000335A8
		public override void Prepare()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			if (this._activeConnection != null && this._activeConnection.IsContextConnection)
			{
				return;
			}
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int>("<sc.SqlCommand.Prepare|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.Prepare|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if ((this.IsPrepared && !this.IsDirty) || this.CommandType == CommandType.StoredProcedure || (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0))
				{
					if (this.Statistics != null)
					{
						this.Statistics.SafeIncrement(ref this.Statistics._prepares);
					}
					this._hiddenPrepare = false;
				}
				else
				{
					this.ValidateCommand("Prepare", false);
					bool flag = true;
					TdsParser tdsParser = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
						this.GetStateObject(null);
						if (this._parameters != null)
						{
							int count = this._parameters.Count;
							for (int i = 0; i < count; i++)
							{
								this._parameters[i].Prepare(this);
							}
						}
						this.InternalPrepare();
					}
					catch (OutOfMemoryException ex)
					{
						flag = false;
						this._activeConnection.Abort(ex);
						throw;
					}
					catch (StackOverflowException ex2)
					{
						flag = false;
						this._activeConnection.Abort(ex2);
						throw;
					}
					catch (ThreadAbortException ex3)
					{
						flag = false;
						this._activeConnection.Abort(ex3);
						SqlInternalConnection.BestEffortCleanup(tdsParser);
						throw;
					}
					catch (Exception ex4)
					{
						flag = ADP.IsCatchableExceptionType(ex4);
						throw;
					}
					finally
					{
						if (flag)
						{
							this._hiddenPrepare = false;
							this.ReliablePutStateObject();
						}
					}
				}
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000355DC File Offset: 0x000337DC
		private void InternalPrepare()
		{
			if (this.IsDirty)
			{
				this.Unprepare();
				this.IsDirty = false;
			}
			this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
			this._preparedConnectionCloseCount = this._activeConnection.CloseCount;
			this._preparedConnectionReconnectCount = this._activeConnection.ReconnectCount;
			if (this.Statistics != null)
			{
				this.Statistics.SafeIncrement(ref this.Statistics._prepares);
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00035648 File Offset: 0x00033848
		internal void Unprepare()
		{
			if (this._activeConnection.IsContextConnection)
			{
				return;
			}
			this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
			if (this._activeConnection.CloseCount != this._preparedConnectionCloseCount || this._activeConnection.ReconnectCount != this._preparedConnectionReconnectCount)
			{
				this._prepareHandle = -1;
			}
			this._cachedMetaData = null;
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.Prepare|INFO> {0}, Command unprepared.", this.ObjectID);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000356B4 File Offset: 0x000338B4
		public override void Cancel()
		{
			using (TryEventScope.Create<int>("<sc.SqlCommand.Cancel | API > {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.Cancel|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				SqlStatistics sqlStatistics = null;
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					TaskCompletionSource<object> reconnectionCompletionSource = this._reconnectionCompletionSource;
					if (reconnectionCompletionSource == null || !reconnectionCompletionSource.TrySetCanceled())
					{
						if (this._activeConnection != null)
						{
							SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
							if (sqlInternalConnectionTds != null)
							{
								SqlInternalConnectionTds sqlInternalConnectionTds2 = sqlInternalConnectionTds;
								lock (sqlInternalConnectionTds2)
								{
									if (sqlInternalConnectionTds == this._activeConnection.InnerConnection as SqlInternalConnectionTds)
									{
										if (sqlInternalConnectionTds.Parser != null)
										{
											TdsParser tdsParser = null;
											RuntimeHelpers.PrepareConstrainedRegions();
											try
											{
												tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
												if (!this._pendingCancel)
												{
													this._pendingCancel = true;
													TdsParserStateObject stateObj = this._stateObj;
													if (stateObj != null)
													{
														stateObj.Cancel(this.ObjectID);
													}
													else
													{
														SqlDataReader sqlDataReader = sqlInternalConnectionTds.FindLiveReader(this);
														if (sqlDataReader != null)
														{
															sqlDataReader.Cancel(this.ObjectID);
														}
													}
												}
											}
											catch (OutOfMemoryException ex)
											{
												this._activeConnection.Abort(ex);
												throw;
											}
											catch (StackOverflowException ex2)
											{
												this._activeConnection.Abort(ex2);
												throw;
											}
											catch (ThreadAbortException ex3)
											{
												this._activeConnection.Abort(ex3);
												SqlInternalConnection.BestEffortCleanup(tdsParser);
												throw;
											}
										}
									}
								}
							}
						}
					}
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003318F File Offset: 0x0003138F
		public new SqlParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000358B8 File Offset: 0x00033AB8
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000358C0 File Offset: 0x00033AC0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._cachedMetaData = null;
				SqlCommand.CachedAsyncState cachedAsyncState = this._cachedAsyncState;
				if (cachedAsyncState != null)
				{
					cachedAsyncState.ResetAsyncState();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000358E4 File Offset: 0x00033AE4
		private SqlDataReader RunExecuteReaderWithRetry(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, string method)
		{
			return this.RetryLogicProvider.Execute<SqlDataReader>(this, () => this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method));
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00035934 File Offset: 0x00033B34
		public override object ExecuteScalar()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			object obj2;
			using (TryEventScope.Create<int>("<sc.SqlCommand.ExecuteScalar|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteScalar|API|Correlation> ObjectID{0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				bool flag = false;
				int? num = null;
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
					SqlDataReader sqlDataReader = (this.IsProviderRetriable ? this.RunExecuteReaderWithRetry(CommandBehavior.Default, RunBehavior.ReturnImmediately, true, "ExecuteScalar") : this.RunExecuteReader(CommandBehavior.Default, RunBehavior.ReturnImmediately, true, "ExecuteScalar"));
					object obj = this.CompleteExecuteScalar(sqlDataReader, false);
					flag = true;
					obj2 = obj;
				}
				catch (SqlException ex)
				{
					num = new int?(ex.Number);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
					this.WriteEndExecuteEvent(flag, num, true);
				}
			}
			return obj2;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00035A2C File Offset: 0x00033C2C
		private object CompleteExecuteScalar(SqlDataReader ds, bool returnSqlValue)
		{
			object obj = null;
			try
			{
				if (ds.Read() && ds.FieldCount > 0)
				{
					if (returnSqlValue)
					{
						obj = ds.GetSqlValue(0);
					}
					else
					{
						obj = ds.GetValue(0);
					}
				}
			}
			finally
			{
				ds.Close();
			}
			return obj;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00035A7C File Offset: 0x00033C7C
		private Task InternalExecuteNonQueryWithRetry(string methodName, bool sendToPipe, int timeout, out bool usedCache, bool asyncWrite, bool inRetry)
		{
			bool innerUsedCache = false;
			Task task = this.RetryLogicProvider.Execute<Task>(this, () => this.InternalExecuteNonQuery(null, methodName, sendToPipe, timeout, out innerUsedCache, asyncWrite, inRetry));
			usedCache = innerUsedCache;
			return task;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00035AE8 File Offset: 0x00033CE8
		public override int ExecuteNonQuery()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			int rowsAffected;
			using (TryEventScope.Create<int>("<sc.SqlCommand.ExecuteNonQuery|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteNonQuery|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				bool flag = false;
				int? num = null;
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
					if (this.IsProviderRetriable)
					{
						bool flag2;
						this.InternalExecuteNonQueryWithRetry("ExecuteNonQuery", false, this.CommandTimeout, out flag2, false, false);
					}
					else
					{
						bool flag2;
						this.InternalExecuteNonQuery(null, "ExecuteNonQuery", false, this.CommandTimeout, out flag2, false, false);
					}
					flag = true;
					rowsAffected = this._rowsAffected;
				}
				catch (SqlException ex)
				{
					num = new int?(ex.Number);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
					this.WriteEndExecuteEvent(flag, num, true);
				}
			}
			return rowsAffected;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00035BEC File Offset: 0x00033DEC
		internal void ExecuteToPipe(SmiContext pipeContext)
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int>("<sc.SqlCommand.ExecuteToPipe|INFO> {0}", this.ObjectID))
			{
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					bool flag;
					this.InternalExecuteNonQuery(null, "ExecuteNonQuery", true, this.CommandTimeout, out flag, false, false);
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00035C70 File Offset: 0x00033E70
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteNonQuery()
		{
			return this.BeginExecuteNonQuery(null, null);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00035C7A File Offset: 0x00033E7A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.BeginExecuteNonQuery|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			SqlConnection.ExecutePermission.Demand();
			return this.BeginExecuteNonQueryInternal(CommandBehavior.Default, callback, stateObject, 0, false, false);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00035CAC File Offset: 0x00033EAC
		private IAsyncResult BeginExecuteNonQueryAsync(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteNonQueryInternal(CommandBehavior.Default, callback, stateObject, this.CommandTimeout, false, true);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00035CC0 File Offset: 0x00033EC0
		private IAsyncResult BeginExecuteNonQueryInternal(CommandBehavior behavior, AsyncCallback callback, object stateObject, int timeout, bool inRetry, bool asyncWrite = false)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(stateObject);
			TaskCompletionSource<object> localCompletion = new TaskCompletionSource<object>(stateObject);
			if (!inRetry)
			{
				this._pendingCancel = false;
				this.ValidateAsyncCommand();
			}
			SqlStatistics sqlStatistics = null;
			IAsyncResult task2;
			try
			{
				if (!inRetry)
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
				}
				bool flag;
				try
				{
					Task task = this.InternalExecuteNonQuery(localCompletion, "BeginExecuteNonQuery", false, timeout, out flag, asyncWrite, inRetry);
					if (task != null)
					{
						AsyncHelper.ContinueTaskWithState(task, localCompletion, this, delegate(object state)
						{
							((SqlCommand)state).BeginExecuteNonQueryInternalReadStage(localCompletion);
						}, null, null, null, null, null);
					}
					else
					{
						this.BeginExecuteNonQueryInternalReadStage(localCompletion);
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (!this.TriggerInternalEndAndRetryIfNecessary(behavior, stateObject, timeout, "EndExecuteNonQuery", flag, inRetry, asyncWrite, taskCompletionSource, localCompletion, new Func<IAsyncResult, string, bool, object>(this.InternalEndExecuteNonQuery), new Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult>(this.BeginExecuteNonQueryInternal)))
				{
					taskCompletionSource = localCompletion;
				}
				if (callback != null)
				{
					taskCompletionSource.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = taskCompletionSource.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task2;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00035E10 File Offset: 0x00034010
		private void BeginExecuteNonQueryInternalReadStage(TaskCompletionSource<object> completion)
		{
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteNonQuery", this._activeConnection);
				this._stateObj.ReadSni(completion);
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			catch (Exception)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00035ED8 File Offset: 0x000340D8
		private void VerifyEndExecuteState(Task completionTask, string endMethod, bool fullCheckForColumnEncryption = false)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlCommand.VerifyEndExecuteState | API | ObjectId {0}, Client Connection Id {1}, MARS={2}, AsyncCommandInProgress={3}";
			SqlConnection activeConnection = this._activeConnection;
			int? num = ((activeConnection != null) ? new int?(activeConnection.ObjectID) : null);
			SqlConnection activeConnection2 = this._activeConnection;
			Guid? guid = ((activeConnection2 != null) ? new Guid?(activeConnection2.ClientConnectionId) : null);
			SqlConnection activeConnection3 = this._activeConnection;
			bool? flag;
			if (activeConnection3 == null)
			{
				flag = null;
			}
			else
			{
				TdsParser parser = activeConnection3.Parser;
				flag = ((parser != null) ? new bool?(parser.MARSOn) : null);
			}
			SqlConnection activeConnection4 = this._activeConnection;
			log.TryTraceEvent<int?, Guid?, bool?, bool?>(text, num, guid, flag, (activeConnection4 != null) ? new bool?(activeConnection4.AsyncCommandInProgress) : null);
			if (completionTask.IsCanceled)
			{
				if (this._stateObj == null)
				{
					throw SQL.CR_ReconnectionCancelled();
				}
				this._stateObj.Parser.State = TdsParserState.Broken;
				this._stateObj.Parser.Connection.BreakConnection();
				this._stateObj.Parser.ThrowExceptionAndWarning(this._stateObj, false, false);
			}
			else if (completionTask.IsFaulted)
			{
				throw completionTask.Exception.InnerException;
			}
			if (this.IsColumnEncryptionEnabled && !fullCheckForColumnEncryption)
			{
				if (this._activeConnection.State != ConnectionState.Open)
				{
					throw ADP.ClosedConnectionError();
				}
				return;
			}
			else
			{
				if (this.cachedAsyncState.EndMethodName == null)
				{
					throw ADP.MethodCalledTwice(endMethod);
				}
				if (endMethod != this.cachedAsyncState.EndMethodName)
				{
					throw ADP.MismatchedAsyncResult(this.cachedAsyncState.EndMethodName, endMethod);
				}
				if (this._activeConnection.State != ConnectionState.Open || !this.cachedAsyncState.IsActiveConnectionValid(this._activeConnection))
				{
					throw ADP.ClosedConnectionError();
				}
				return;
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00036070 File Offset: 0x00034270
		private void WaitForAsyncResults(IAsyncResult asyncResult, bool isInternal)
		{
			Task task = (Task)asyncResult;
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			if (this._stateObj != null)
			{
				this._stateObj._networkPacketTaskSource = null;
			}
			if (!isInternal && (!this.IsColumnEncryptionEnabled || !task.IsFaulted))
			{
				this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000360D0 File Offset: 0x000342D0
		public int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			int num;
			try
			{
				num = this.EndExecuteNonQueryInternal(asyncResult);
			}
			finally
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.EndExecuteNonQuery|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			}
			return num;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00036114 File Offset: 0x00034314
		private void ThrowIfReconnectionHasBeenCanceled()
		{
			if (this._stateObj == null)
			{
				TaskCompletionSource<object> reconnectionCompletionSource = this._reconnectionCompletionSource;
				if (reconnectionCompletionSource != null && reconnectionCompletionSource.Task.IsCanceled)
				{
					throw SQL.CR_ReconnectionCancelled();
				}
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00036148 File Offset: 0x00034348
		private int EndExecuteNonQueryAsync(IAsyncResult asyncResult)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.EndExecuteNonQueryAsync|Info|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				SqlCommand.CachedAsyncState cachedAsyncState = this.cachedAsyncState;
				if (cachedAsyncState != null)
				{
					cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			if (!this._internalEndExecuteInitiated)
			{
				TdsParserStateObject stateObj = this._stateObj;
				lock (stateObj)
				{
					return this.EndExecuteNonQueryInternal(asyncResult);
				}
			}
			return this.EndExecuteNonQueryInternal(asyncResult);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000361EC File Offset: 0x000343EC
		private int EndExecuteNonQueryInternal(IAsyncResult asyncResult)
		{
			SqlStatistics sqlStatistics = null;
			bool flag = false;
			int? num = null;
			int num3;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				int num2 = (int)this.InternalEndExecuteNonQuery(asyncResult, "EndExecuteNonQuery", false);
				flag = true;
				num3 = num2;
			}
			catch (SqlException ex)
			{
				num = new int?(ex.Number);
				SqlCommand.CachedAsyncState cachedAsyncState = this._cachedAsyncState;
				if (cachedAsyncState != null)
				{
					cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
			catch (Exception ex2)
			{
				SqlCommand.CachedAsyncState cachedAsyncState2 = this._cachedAsyncState;
				if (cachedAsyncState2 != null)
				{
					cachedAsyncState2.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex2))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				this.WriteEndExecuteEvent(flag, num, false);
			}
			return num3;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000362B4 File Offset: 0x000344B4
		private object InternalEndExecuteNonQuery(IAsyncResult asyncResult, string endMethod, bool isInternal)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlCommand.InternalEndExecuteNonQuery | INFO | ObjectId {0}, Client Connection Id {1}, MARS={2}, AsyncCommandInProgress={3}";
			SqlConnection activeConnection = this._activeConnection;
			int? num = ((activeConnection != null) ? new int?(activeConnection.ObjectID) : null);
			SqlConnection activeConnection2 = this._activeConnection;
			Guid? guid = ((activeConnection2 != null) ? new Guid?(activeConnection2.ClientConnectionId) : null);
			SqlConnection activeConnection3 = this._activeConnection;
			bool? flag;
			if (activeConnection3 == null)
			{
				flag = null;
			}
			else
			{
				TdsParser parser = activeConnection3.Parser;
				flag = ((parser != null) ? new bool?(parser.MARSOn) : null);
			}
			SqlConnection activeConnection4 = this._activeConnection;
			log.TryTraceEvent<int?, Guid?, bool?, bool?>(text, num, guid, flag, (activeConnection4 != null) ? new bool?(activeConnection4.AsyncCommandInProgress) : null);
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			object obj;
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.VerifyEndExecuteState((Task)asyncResult, endMethod, false);
				this.WaitForAsyncResults(asyncResult, isInternal);
				if (this.IsColumnEncryptionEnabled)
				{
					this.VerifyEndExecuteState((Task)asyncResult, endMethod, true);
				}
				bool flag2 = true;
				try
				{
					if (!isInternal)
					{
						this.NotifyDependency();
						if (this._internalEndExecuteInitiated)
						{
							this.cachedAsyncState.ResetAsyncState();
							return this._rowsAffected;
						}
					}
					this.CheckThrowSNIException();
					if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
					{
						try
						{
							bool flag3;
							if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag3))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
							goto IL_017C;
						}
						finally
						{
							if (!isInternal)
							{
								this.cachedAsyncState.ResetAsyncState();
							}
						}
					}
					SqlDataReader sqlDataReader = this.CompleteAsyncExecuteReader(isInternal, false);
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}
					IL_017C:;
				}
				catch (Exception ex)
				{
					flag2 = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (flag2)
					{
						this.PutStateObject();
					}
				}
				obj = this._rowsAffected;
			}
			catch (OutOfMemoryException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				this._activeConnection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				this._activeConnection.Abort(ex4);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			return obj;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00036538 File Offset: 0x00034738
		private Task InternalExecuteNonQuery(TaskCompletionSource<object> completion, string methodName, bool sendToPipe, int timeout, out bool usedCache, bool asyncWrite = false, bool inRetry = false)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlCommand.InternalExecuteNonQuery | INFO | ObjectId {0}, Client Connection Id {1}, AsyncCommandInProgress={2}";
			SqlConnection activeConnection = this._activeConnection;
			int? num = ((activeConnection != null) ? new int?(activeConnection.ObjectID) : null);
			SqlConnection activeConnection2 = this._activeConnection;
			Guid? guid = ((activeConnection2 != null) ? new Guid?(activeConnection2.ClientConnectionId) : null);
			SqlConnection activeConnection3 = this._activeConnection;
			log.TryTraceEvent<int?, Guid?, bool?>(text, num, guid, (activeConnection3 != null) ? new bool?(activeConnection3.AsyncCommandInProgress) : null);
			bool flag = completion != null;
			usedCache = false;
			SqlStatistics statistics = this.Statistics;
			this._rowsAffected = -1;
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			Task task2;
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				if (!inRetry)
				{
					this.ValidateCommand(methodName, flag);
				}
				this.CheckNotificationStateAndAutoEnlist();
				Task task = null;
				if (this._activeConnection.IsContextConnection)
				{
					if (statistics != null)
					{
						statistics.SafeIncrement(ref statistics._unpreparedExecs);
					}
					this.RunExecuteNonQuerySmi(sendToPipe);
				}
				else if (!this.ShouldUseEnclaveBasedWorkflow && !this.BatchRPCMode && CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
				{
					if (statistics != null)
					{
						if (!this.IsDirty && this.IsPrepared)
						{
							statistics.SafeIncrement(ref statistics._preparedExecs);
						}
						else
						{
							statistics.SafeIncrement(ref statistics._unpreparedExecs);
						}
					}
					task = this.RunExecuteNonQueryTds(methodName, flag, timeout, asyncWrite);
				}
				else
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.ExecuteNonQuery|INFO> {0}, Command executed as RPC.", this.ObjectID);
					SqlDataReader reader = this.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, methodName, completion, timeout, out task, out usedCache, asyncWrite, inRetry);
					if (reader != null)
					{
						if (task != null)
						{
							task = AsyncHelper.CreateContinuationTask(task, delegate
							{
								reader.Close();
							}, null, null);
						}
						else
						{
							reader.Close();
						}
					}
				}
				task2 = task;
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			return task2;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003677C File Offset: 0x0003497C
		public XmlReader ExecuteXmlReader()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			XmlReader xmlReader2;
			using (TryEventScope.Create<int>("<sc.SqlCommand.ExecuteXmlReader|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteXmlReader|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				bool flag = false;
				int? num = null;
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
					SqlDataReader sqlDataReader = (this.IsProviderRetriable ? this.RunExecuteReaderWithRetry(CommandBehavior.SequentialAccess, RunBehavior.ReturnImmediately, true, "ExecuteXmlReader") : this.RunExecuteReader(CommandBehavior.SequentialAccess, RunBehavior.ReturnImmediately, true, "ExecuteXmlReader"));
					XmlReader xmlReader = this.CompleteXmlReader(sqlDataReader, false);
					flag = true;
					xmlReader2 = xmlReader;
				}
				catch (SqlException ex)
				{
					num = new int?(ex.Number);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
					this.WriteEndExecuteEvent(flag, num, true);
				}
			}
			return xmlReader2;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00036878 File Offset: 0x00034A78
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteXmlReader()
		{
			return this.BeginExecuteXmlReader(null, null);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00036882 File Offset: 0x00034A82
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteXmlReader(AsyncCallback callback, object stateObject)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.BeginExecuteXmlReader|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			SqlConnection.ExecutePermission.Demand();
			return this.BeginExecuteXmlReaderInternal(CommandBehavior.SequentialAccess, callback, stateObject, 0, false, false);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000368B5 File Offset: 0x00034AB5
		private IAsyncResult BeginExecuteXmlReaderAsync(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteXmlReaderInternal(CommandBehavior.SequentialAccess, callback, stateObject, this.CommandTimeout, false, true);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000368CC File Offset: 0x00034ACC
		private IAsyncResult BeginExecuteXmlReaderInternal(CommandBehavior behavior, AsyncCallback callback, object stateObject, int timeout, bool inRetry, bool asyncWrite = false)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(stateObject);
			TaskCompletionSource<object> localCompletion = new TaskCompletionSource<object>(stateObject);
			if (!inRetry)
			{
				this._pendingCancel = false;
				this.ValidateAsyncCommand();
			}
			SqlStatistics sqlStatistics = null;
			IAsyncResult task2;
			try
			{
				if (!inRetry)
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
				}
				Task task;
				bool flag;
				try
				{
					this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, "BeginExecuteXmlReader", localCompletion, timeout, out task, out flag, asyncWrite, inRetry);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (task != null)
				{
					AsyncHelper.ContinueTaskWithState(task, localCompletion, this, delegate(object state)
					{
						((SqlCommand)state).BeginExecuteXmlReaderInternalReadStage(localCompletion);
					}, null, null, null, null, null);
				}
				else
				{
					this.BeginExecuteXmlReaderInternalReadStage(localCompletion);
				}
				if (!this.TriggerInternalEndAndRetryIfNecessary(behavior, stateObject, timeout, "EndExecuteXmlReader", flag, inRetry, asyncWrite, taskCompletionSource, localCompletion, new Func<IAsyncResult, string, bool, object>(this.InternalEndExecuteReader), new Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult>(this.BeginExecuteXmlReaderInternal)))
				{
					taskCompletionSource = localCompletion;
				}
				if (callback != null)
				{
					taskCompletionSource.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = taskCompletionSource.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task2;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00036A1C File Offset: 0x00034C1C
		private void BeginExecuteXmlReaderInternalReadStage(TaskCompletionSource<object> completion)
		{
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteXmlReader", this._activeConnection);
				this._stateObj.ReadSni(completion);
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				completion.TrySetException(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				completion.TrySetException(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				completion.TrySetException(ex3);
				throw;
			}
			catch (Exception ex4)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				completion.TrySetException(ex4);
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00036B08 File Offset: 0x00034D08
		public XmlReader EndExecuteXmlReader(IAsyncResult asyncResult)
		{
			XmlReader xmlReader;
			try
			{
				xmlReader = this.EndExecuteXmlReaderInternal(asyncResult);
			}
			finally
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.EndExecuteXmlReader|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			}
			return xmlReader;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00036B4C File Offset: 0x00034D4C
		private XmlReader EndExecuteXmlReaderAsync(IAsyncResult asyncResult)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.EndExecuteXmlReaderAsync|Info|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				SqlCommand.CachedAsyncState cachedAsyncState = this.cachedAsyncState;
				if (cachedAsyncState != null)
				{
					cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			if (!this._internalEndExecuteInitiated)
			{
				TdsParserStateObject stateObj = this._stateObj;
				lock (stateObj)
				{
					return this.EndExecuteXmlReaderInternal(asyncResult);
				}
			}
			return this.EndExecuteXmlReaderInternal(asyncResult);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00036BF0 File Offset: 0x00034DF0
		private XmlReader EndExecuteXmlReaderInternal(IAsyncResult asyncResult)
		{
			bool flag = false;
			int? num = null;
			XmlReader xmlReader2;
			try
			{
				XmlReader xmlReader = this.CompleteXmlReader(this.InternalEndExecuteReader(asyncResult, "EndExecuteXmlReader", false), true);
				flag = true;
				xmlReader2 = xmlReader;
			}
			catch (SqlException ex)
			{
				num = new int?(ex.Number);
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
			catch (Exception ex2)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex2))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				this.WriteEndExecuteEvent(flag, num, false);
			}
			return xmlReader2;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00036CA8 File Offset: 0x00034EA8
		private XmlReader CompleteXmlReader(SqlDataReader ds, bool async = false)
		{
			XmlReader xmlReader = null;
			SmiExtendedMetaData[] internalSmiMetaData = ds.GetInternalSmiMetaData();
			bool flag = internalSmiMetaData != null && internalSmiMetaData.Length == 1 && (internalSmiMetaData[0].SqlDbType == SqlDbType.NText || internalSmiMetaData[0].SqlDbType == SqlDbType.NVarChar || internalSmiMetaData[0].SqlDbType == SqlDbType.Xml);
			if (flag)
			{
				try
				{
					SqlStream sqlStream = new SqlStream(ds, true, internalSmiMetaData[0].SqlDbType != SqlDbType.Xml);
					xmlReader = sqlStream.ToXmlReader(async);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						ds.Close();
					}
					throw;
				}
			}
			if (xmlReader == null)
			{
				ds.Close();
				throw SQL.NonXmlResult();
			}
			return xmlReader;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00036D4C File Offset: 0x00034F4C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader()
		{
			return this.BeginExecuteReader(null, null, CommandBehavior.Default);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00036D57 File Offset: 0x00034F57
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteReader(callback, stateObject, CommandBehavior.Default);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00036D62 File Offset: 0x00034F62
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteDbDataReader|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			return this.ExecuteReader(behavior, "ExecuteReader");
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00036D8C File Offset: 0x00034F8C
		private SqlDataReader ExecuteReaderWithRetry(CommandBehavior behavior, string method)
		{
			return this.RetryLogicProvider.Execute<SqlDataReader>(this, () => this.ExecuteReader(behavior, method));
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00036DCC File Offset: 0x00034FCC
		public new SqlDataReader ExecuteReader()
		{
			SqlStatistics sqlStatistics = null;
			SqlDataReader sqlDataReader;
			using (TryEventScope.Create<int>("<sc.SqlCommand.ExecuteReader|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteReader|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					sqlDataReader = (this.IsProviderRetriable ? this.ExecuteReaderWithRetry(CommandBehavior.Default, "ExecuteReader") : this.ExecuteReader(CommandBehavior.Default, "ExecuteReader"));
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
			return sqlDataReader;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00036E64 File Offset: 0x00035064
		public new SqlDataReader ExecuteReader(CommandBehavior behavior)
		{
			SqlDataReader sqlDataReader;
			using (TryEventScope.Create<int, int>("<sc.SqlCommand.ExecuteReader|API> {0}, behavior={1}", this.ObjectID, (int)behavior))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteReader|API|Correlation> ObjectID {0}, behavior={1}, ActivityID {2}", this.ObjectID, (int)behavior, ActivityCorrelator.Current);
				sqlDataReader = (this.IsProviderRetriable ? this.ExecuteReaderWithRetry(behavior, "ExecuteReader") : this.ExecuteReader(behavior, "ExecuteReader"));
			}
			return sqlDataReader;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00036EDC File Offset: 0x000350DC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
		{
			return this.BeginExecuteReader(null, null, behavior);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00036EE7 File Offset: 0x000350E7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.BeginExecuteReader|API|Correlation> ObjectID{0}, behavior={1}, ActivityID {2}", this.ObjectID, (int)behavior, ActivityCorrelator.Current);
			SqlConnection.ExecutePermission.Demand();
			return this.BeginExecuteReaderInternal(behavior, callback, stateObject, 0, false, false);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00036F1C File Offset: 0x0003511C
		internal SqlDataReader ExecuteReader(CommandBehavior behavior, string method)
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag = false;
			int? num = null;
			SqlDataReader sqlDataReader2;
			try
			{
				this.WriteBeginExecuteEvent();
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				SqlDataReader sqlDataReader = this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, method);
				flag = true;
				sqlDataReader2 = sqlDataReader;
			}
			catch (SqlException ex)
			{
				num = new int?(ex.Number);
				throw;
			}
			catch (OutOfMemoryException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				this._activeConnection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				this._activeConnection.Abort(ex4);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				this.WriteEndExecuteEvent(flag, num, true);
			}
			return sqlDataReader2;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00037020 File Offset: 0x00035220
		public SqlDataReader EndExecuteReader(IAsyncResult asyncResult)
		{
			SqlDataReader sqlDataReader;
			try
			{
				sqlDataReader = this.EndExecuteReaderInternal(asyncResult);
			}
			finally
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.EndExecuteReader|API|Correlation> ObjectID{0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			}
			return sqlDataReader;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00037064 File Offset: 0x00035264
		private SqlDataReader EndExecuteReaderAsync(IAsyncResult asyncResult)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.EndExecuteReaderAsync|Info|Correlation> ObjectID{0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				SqlCommand.CachedAsyncState cachedAsyncState = this.cachedAsyncState;
				if (cachedAsyncState != null)
				{
					cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			if (!this._internalEndExecuteInitiated)
			{
				TdsParserStateObject stateObj = this._stateObj;
				lock (stateObj)
				{
					return this.EndExecuteReaderInternal(asyncResult);
				}
			}
			return this.EndExecuteReaderInternal(asyncResult);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00037108 File Offset: 0x00035308
		private SqlDataReader EndExecuteReaderInternal(IAsyncResult asyncResult)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlCommand.EndExecuteReaderInternal | API | ObjectId {0}, Client Connection Id {1}, MARS={2}, AsyncCommandInProgress={3}";
			SqlConnection activeConnection = this._activeConnection;
			int? num = ((activeConnection != null) ? new int?(activeConnection.ObjectID) : null);
			SqlConnection activeConnection2 = this._activeConnection;
			Guid? guid = ((activeConnection2 != null) ? new Guid?(activeConnection2.ClientConnectionId) : null);
			SqlConnection activeConnection3 = this._activeConnection;
			bool? flag;
			if (activeConnection3 == null)
			{
				flag = null;
			}
			else
			{
				TdsParser parser = activeConnection3.Parser;
				flag = ((parser != null) ? new bool?(parser.MARSOn) : null);
			}
			SqlConnection activeConnection4 = this._activeConnection;
			log.TryTraceEvent<int?, Guid?, bool?, bool?>(text, num, guid, flag, (activeConnection4 != null) ? new bool?(activeConnection4.AsyncCommandInProgress) : null);
			SqlStatistics sqlStatistics = null;
			bool flag2 = false;
			int? num2 = null;
			SqlDataReader sqlDataReader2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				SqlDataReader sqlDataReader = this.InternalEndExecuteReader(asyncResult, "EndExecuteReader", false);
				flag2 = true;
				sqlDataReader2 = sqlDataReader;
			}
			catch (SqlException ex)
			{
				num2 = new int?(ex.Number);
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
			catch (Exception ex2)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex2))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				this.WriteEndExecuteEvent(flag2, num2, false);
			}
			return sqlDataReader2;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00037274 File Offset: 0x00035474
		private IAsyncResult BeginExecuteReaderAsync(CommandBehavior behavior, AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteReaderInternal(behavior, callback, stateObject, this.CommandTimeout, false, true);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00037288 File Offset: 0x00035488
		private IAsyncResult BeginExecuteReaderInternal(CommandBehavior behavior, AsyncCallback callback, object stateObject, int timeout, bool inRetry, bool asyncWrite = false)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(stateObject);
			TaskCompletionSource<object> localCompletion = new TaskCompletionSource<object>(stateObject);
			if (!inRetry)
			{
				this._pendingCancel = false;
			}
			SqlStatistics sqlStatistics = null;
			IAsyncResult task2;
			try
			{
				if (!inRetry)
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
					this.ValidateAsyncCommand();
				}
				bool flag = false;
				Task task = null;
				try
				{
					this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, "BeginExecuteReader", localCompletion, timeout, out task, out flag, asyncWrite, inRetry);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (task != null)
				{
					AsyncHelper.ContinueTaskWithState(task, localCompletion, this, delegate(object state)
					{
						((SqlCommand)state).BeginExecuteReaderInternalReadStage(localCompletion);
					}, null, null, null, null, null);
				}
				else
				{
					this.BeginExecuteReaderInternalReadStage(localCompletion);
				}
				if (!this.TriggerInternalEndAndRetryIfNecessary(behavior, stateObject, timeout, "EndExecuteReader", flag, inRetry, asyncWrite, taskCompletionSource, localCompletion, new Func<IAsyncResult, string, bool, object>(this.InternalEndExecuteReader), new Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult>(this.BeginExecuteReaderInternal)))
				{
					taskCompletionSource = localCompletion;
				}
				if (callback != null)
				{
					taskCompletionSource.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = taskCompletionSource.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task2;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000373DC File Offset: 0x000355DC
		private bool TriggerInternalEndAndRetryIfNecessary(CommandBehavior behavior, object stateObject, int timeout, string endMethod, bool usedCache, bool inRetry, bool asyncWrite, TaskCompletionSource<object> globalCompletion, TaskCompletionSource<object> localCompletion, Func<IAsyncResult, string, bool, object> endFunc, Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult> retryFunc)
		{
			if (this.IsColumnEncryptionEnabled && !inRetry && (usedCache || this.ShouldUseEnclaveBasedWorkflow))
			{
				long firstAttemptStart = ADP.TimerCurrent();
				Action<Task<object>> <>9__1;
				localCompletion.Task.ContinueWith(delegate(Task<object> tsk)
				{
					if (tsk.IsFaulted)
					{
						globalCompletion.TrySetException(tsk.Exception.InnerException);
						return;
					}
					if (tsk.IsCanceled)
					{
						globalCompletion.TrySetCanceled();
						return;
					}
					try
					{
						this._internalEndExecuteInitiated = true;
						TdsParserStateObject stateObj = this._stateObj;
						lock (stateObj)
						{
							endFunc(tsk, endMethod, true);
						}
						globalCompletion.TrySetResult(tsk.Result);
					}
					catch (Exception ex)
					{
						if (ADP.IsCatchableExceptionType(ex))
						{
							this.ReliablePutStateObject();
						}
						bool flag2 = ex is EnclaveDelegate.RetryableEnclaveQueryExecutionException;
						if (ex is SqlException)
						{
							SqlException ex2 = ex as SqlException;
							for (int i = 0; i < ex2.Errors.Count; i++)
							{
								if ((usedCache && ex2.Errors[i].Number == 33514) || (this.ShouldUseEnclaveBasedWorkflow && ex2.Errors[i].Number == 33195))
								{
									flag2 = true;
									break;
								}
							}
						}
						if (!flag2)
						{
							if (this._cachedAsyncState != null)
							{
								this._cachedAsyncState.ResetAsyncState();
							}
							this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
							globalCompletion.TrySetException(ex);
						}
						else
						{
							SqlQueryMetadataCache.GetInstance().InvalidateCacheEntry(this);
							this.InvalidateEnclaveSession();
							try
							{
								this._internalEndExecuteInitiated = false;
								Task<object> task = (Task<object>)retryFunc(behavior, null, stateObject, TdsParserStaticMethods.GetRemainingTimeout(timeout, firstAttemptStart), true, asyncWrite);
								Task<object> task2 = task;
								Action<Task<object>> action;
								if ((action = <>9__1) == null)
								{
									action = (<>9__1 = delegate(Task<object> retryTsk)
									{
										if (retryTsk.IsFaulted)
										{
											globalCompletion.TrySetException(retryTsk.Exception.InnerException);
											return;
										}
										if (retryTsk.IsCanceled)
										{
											globalCompletion.TrySetCanceled();
											return;
										}
										globalCompletion.TrySetResult(retryTsk.Result);
									});
								}
								task2.ContinueWith(action, TaskScheduler.Default);
							}
							catch (Exception ex3)
							{
								globalCompletion.TrySetException(ex3);
							}
						}
					}
				}, TaskScheduler.Default);
				return true;
			}
			return false;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00037490 File Offset: 0x00035690
		private void InvalidateEnclaveSession()
		{
			if (this.ShouldUseEnclaveBasedWorkflow && this.enclavePackage != null)
			{
				EnclaveDelegate.Instance.InvalidateEnclaveSession(this._activeConnection.AttestationProtocol, this._activeConnection.Parser.EnclaveType, this.GetEnclaveSessionParameters(), this.enclavePackage.EnclaveSession);
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000374E3 File Offset: 0x000356E3
		private EnclaveSessionParameters GetEnclaveSessionParameters()
		{
			return new EnclaveSessionParameters(this._activeConnection.DataSource, this._activeConnection.EnclaveAttestationUrl, this._activeConnection.Database);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003750C File Offset: 0x0003570C
		private void BeginExecuteReaderInternalReadStage(TaskCompletionSource<object> completion)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlCommand.BeginExecuteReaderInternalReadStage | INFO | Correlation | Object Id {0}, Activity Id {1}, Client Connection Id {2}, Command Text '{3}'";
			int objectID = this.ObjectID;
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Current;
			SqlConnection connection = this.Connection;
			log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId, Guid?, string>(text, objectID, activityId, (connection != null) ? new Guid?(connection.ClientConnectionId) : null, this.CommandText);
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteReader", this._activeConnection);
				this._stateObj.ReadSni(completion);
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				completion.TrySetException(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				completion.TrySetException(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				completion.TrySetException(ex3);
				throw;
			}
			catch (Exception ex4)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				completion.TrySetException(ex4);
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00037638 File Offset: 0x00035838
		private SqlDataReader InternalEndExecuteReader(IAsyncResult asyncResult, string endMethod, bool isInternal)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlCommand.InternalEndExecuteReader | INFO | ObjectId {0}, Client Connection Id {1}, MARS={2}, AsyncCommandInProgress={3}";
			SqlConnection activeConnection = this._activeConnection;
			int? num = ((activeConnection != null) ? new int?(activeConnection.ObjectID) : null);
			SqlConnection activeConnection2 = this._activeConnection;
			Guid? guid = ((activeConnection2 != null) ? new Guid?(activeConnection2.ClientConnectionId) : null);
			SqlConnection activeConnection3 = this._activeConnection;
			bool? flag;
			if (activeConnection3 == null)
			{
				flag = null;
			}
			else
			{
				TdsParser parser = activeConnection3.Parser;
				flag = ((parser != null) ? new bool?(parser.MARSOn) : null);
			}
			SqlConnection activeConnection4 = this._activeConnection;
			log.TryTraceEvent<int?, Guid?, bool?, bool?>(text, num, guid, flag, (activeConnection4 != null) ? new bool?(activeConnection4.AsyncCommandInProgress) : null);
			this.VerifyEndExecuteState((Task)asyncResult, endMethod, false);
			this.WaitForAsyncResults(asyncResult, isInternal);
			if (this.IsColumnEncryptionEnabled)
			{
				this.VerifyEndExecuteState((Task)asyncResult, endMethod, true);
			}
			this.CheckThrowSNIException();
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SqlDataReader sqlDataReader2;
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				SqlDataReader sqlDataReader = this.CompleteAsyncExecuteReader(isInternal, false);
				sqlDataReader2 = sqlDataReader;
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			return sqlDataReader2;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003779C File Offset: 0x0003599C
		private Task<int> InternalExecuteNonQueryWithRetryAsync(CancellationToken cancellationToken)
		{
			return this.RetryLogicProvider.ExecuteAsync<int>(this, () => this.InternalExecuteNonQueryAsync(cancellationToken), cancellationToken);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000377DB File Offset: 0x000359DB
		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalExecuteNonQueryAsync(cancellationToken);
			}
			return this.InternalExecuteNonQueryWithRetryAsync(cancellationToken);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000377F4 File Offset: 0x000359F4
		private Task<int> InternalExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteNonQueryAsync|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			SqlConnection.ExecutePermission.Demand();
			TaskCompletionSource<int> source = new TaskCompletionSource<int>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this);
			}
			Task<int> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<int>(ref task);
				Task<int>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecuteNonQueryAsync), new Func<IAsyncResult, int>(this.EndExecuteNonQueryAsync), null).ContinueWith(delegate(Task<int> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
						return;
					}
					source.SetResult(t.Result);
				}, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				source.SetException(ex);
			}
			return task;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000378F0 File Offset: 0x00035AF0
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(behavior, cancellationToken).ContinueWith<DbDataReader>(delegate(Task<SqlDataReader> result)
			{
				if (result.IsFaulted)
				{
					throw result.Exception.InnerException;
				}
				return result.Result;
			}, CancellationToken.None, TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00037930 File Offset: 0x00035B30
		private Task<SqlDataReader> InternalExecuteReaderWithRetryAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.RetryLogicProvider.ExecuteAsync<SqlDataReader>(this, () => this.InternalExecuteReaderAsync(behavior, cancellationToken), cancellationToken);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00037976 File Offset: 0x00035B76
		public new Task<SqlDataReader> ExecuteReaderAsync()
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
			}
			return this.InternalExecuteReaderWithRetryAsync(CommandBehavior.Default, CancellationToken.None);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00037999 File Offset: 0x00035B99
		public new Task<SqlDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalExecuteReaderAsync(behavior, CancellationToken.None);
			}
			return this.InternalExecuteReaderWithRetryAsync(behavior, CancellationToken.None);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000379BC File Offset: 0x00035BBC
		public new Task<SqlDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
			}
			return this.InternalExecuteReaderWithRetryAsync(CommandBehavior.Default, cancellationToken);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000379D7 File Offset: 0x00035BD7
		public new Task<SqlDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalExecuteReaderAsync(behavior, cancellationToken);
			}
			return this.InternalExecuteReaderWithRetryAsync(behavior, cancellationToken);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000379F4 File Offset: 0x00035BF4
		private Task<SqlDataReader> InternalExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteReaderAsync|API|Correlation> ObjectID {0}, behavior={1}, ActivityID {2}", this.ObjectID, (int)behavior, ActivityCorrelator.Current);
			SqlConnection.ExecutePermission.Demand();
			TaskCompletionSource<SqlDataReader> source = new TaskCompletionSource<SqlDataReader>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this);
			}
			Task<SqlDataReader> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<SqlDataReader>(ref task);
				Task<SqlDataReader>.Factory.FromAsync<CommandBehavior>(new Func<CommandBehavior, AsyncCallback, object, IAsyncResult>(this.BeginExecuteReaderAsync), new Func<IAsyncResult, SqlDataReader>(this.EndExecuteReaderAsync), behavior, null).ContinueWith(delegate(Task<SqlDataReader> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
						return;
					}
					source.SetResult(t.Result);
				}, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				source.SetException(ex);
			}
			return task;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00037AF0 File Offset: 0x00035CF0
		public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			return this.InternalExecuteScalarAsync(cancellationToken);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00037AFC File Offset: 0x00035CFC
		private Task<object> InternalExecuteScalarAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(cancellationToken).ContinueWith<Task<object>>(delegate(Task<SqlDataReader> executeTask)
			{
				TaskCompletionSource<object> source = new TaskCompletionSource<object>();
				if (executeTask.IsCanceled)
				{
					source.SetCanceled();
				}
				else if (executeTask.IsFaulted)
				{
					source.SetException(executeTask.Exception.InnerException);
				}
				else
				{
					SqlDataReader reader = executeTask.Result;
					reader.ReadAsync(cancellationToken).ContinueWith(delegate(Task<bool> readTask)
					{
						try
						{
							if (readTask.IsCanceled)
							{
								reader.Dispose();
								source.SetCanceled();
							}
							else if (readTask.IsFaulted)
							{
								reader.Dispose();
								source.SetException(readTask.Exception.InnerException);
							}
							else
							{
								Exception ex = null;
								object obj = null;
								try
								{
									bool result = readTask.Result;
									if (result && reader.FieldCount > 0)
									{
										try
										{
											obj = reader.GetValue(0);
										}
										catch (Exception ex2)
										{
											ex = ex2;
										}
									}
								}
								finally
								{
									reader.Dispose();
								}
								if (ex != null)
								{
									source.SetException(ex);
								}
								else
								{
									source.SetResult(obj);
								}
							}
						}
						catch (Exception ex3)
						{
							source.SetException(ex3);
						}
					}, TaskScheduler.Default);
				}
				return source.Task;
			}, TaskScheduler.Default).Unwrap<object>();
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00037B3D File Offset: 0x00035D3D
		public Task<XmlReader> ExecuteXmlReaderAsync()
		{
			return this.ExecuteXmlReaderAsync(CancellationToken.None);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00037B4C File Offset: 0x00035D4C
		private Task<XmlReader> InternalExecuteXmlReaderWithRetryAsync(CancellationToken cancellationToken)
		{
			return this.RetryLogicProvider.ExecuteAsync<XmlReader>(this, () => this.InternalExecuteXmlReaderAsync(cancellationToken), cancellationToken);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00037B8B File Offset: 0x00035D8B
		public Task<XmlReader> ExecuteXmlReaderAsync(CancellationToken cancellationToken)
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalExecuteXmlReaderAsync(cancellationToken);
			}
			return this.InternalExecuteXmlReaderWithRetryAsync(cancellationToken);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00037BA4 File Offset: 0x00035DA4
		private Task<XmlReader> InternalExecuteXmlReaderAsync(CancellationToken cancellationToken)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlCommand.ExecuteXmlReaderAsync|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			SqlConnection.ExecutePermission.Demand();
			TaskCompletionSource<XmlReader> source = new TaskCompletionSource<XmlReader>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this);
			}
			Task<XmlReader> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<XmlReader>(ref task);
				Task<XmlReader>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecuteXmlReaderAsync), new Func<IAsyncResult, XmlReader>(this.EndExecuteXmlReaderAsync), null).ContinueWith(delegate(Task<XmlReader> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
						return;
					}
					source.SetResult(t.Result);
				}, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				source.SetException(ex);
			}
			return task;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00037CA0 File Offset: 0x00035EA0
		public void RegisterColumnEncryptionKeyStoreProvidersOnCommand(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			this.ValidateCustomProviders(customProviders);
			Dictionary<string, SqlColumnEncryptionKeyStoreProvider> dictionary = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(customProviders, StringComparer.OrdinalIgnoreCase);
			this._customColumnEncryptionKeyStoreProviders = dictionary;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00037CC8 File Offset: 0x00035EC8
		private void ValidateCustomProviders(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			if (customProviders == null)
			{
				throw SQL.NullCustomKeyStoreProviderDictionary();
			}
			foreach (string text in customProviders.Keys)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw SQL.EmptyProviderName();
				}
				if (text.StartsWith("MSSQL_", StringComparison.InvariantCultureIgnoreCase))
				{
					throw SQL.InvalidCustomKeyStoreProviderName(text, "MSSQL_");
				}
				if (customProviders[text] == null)
				{
					throw SQL.NullProviderValue(text);
				}
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00037D50 File Offset: 0x00035F50
		internal bool TryGetColumnEncryptionKeyStoreProvider(string providerName, out SqlColumnEncryptionKeyStoreProvider columnKeyStoreProvider)
		{
			return this._customColumnEncryptionKeyStoreProviders.TryGetValue(providerName, out columnKeyStoreProvider);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00037D5F File Offset: 0x00035F5F
		internal List<string> GetColumnEncryptionCustomKeyStoreProvidersNames()
		{
			return this._customColumnEncryptionKeyStoreProviders.Keys.ToList<string>();
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00037D74 File Offset: 0x00035F74
		private static string UnquoteProcedurePart(string part)
		{
			if (part != null && 2 <= part.Length && '[' == part[0] && ']' == part[part.Length - 1])
			{
				part = part.Substring(1, part.Length - 2);
				part = part.Replace("]]", "]");
			}
			return part;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00037DD0 File Offset: 0x00035FD0
		private static string UnquoteProcedureName(string name, out object groupNumber)
		{
			groupNumber = null;
			string text = name;
			if (text != null)
			{
				if (char.IsDigit(text[text.Length - 1]))
				{
					int num = text.LastIndexOf(';');
					if (num != -1)
					{
						string text2 = text.Substring(num + 1);
						int num2 = 0;
						if (int.TryParse(text2, out num2))
						{
							groupNumber = num2;
							text = text.Substring(0, num);
						}
					}
				}
				text = SqlCommand.UnquoteProcedurePart(text);
			}
			return text;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00037E38 File Offset: 0x00036038
		internal void DeriveParameters()
		{
			CommandType commandType = this.CommandType;
			if (commandType == CommandType.Text)
			{
				throw ADP.DeriveParametersNotSupported(this);
			}
			if (commandType != CommandType.StoredProcedure)
			{
				if (commandType != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(this.CommandType);
				}
				throw ADP.DeriveParametersNotSupported(this);
			}
			else
			{
				this.ValidateCommand("DeriveParameters", false);
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this.CommandText, "[\"", "]\"", Strings.SQL_SqlCommandCommandText, false);
				if (array[3] == null || ADP.IsEmpty(array[3]))
				{
					throw ADP.NoStoredProcedureExists(this.CommandText);
				}
				SqlCommand sqlCommand = null;
				StringBuilder stringBuilder = new StringBuilder();
				if (!ADP.IsEmpty(array[0]))
				{
					SqlCommandSet.BuildStoredProcedureName(stringBuilder, array[0]);
					stringBuilder.Append(".");
				}
				if (ADP.IsEmpty(array[1]))
				{
					array[1] = this.Connection.Database;
				}
				SqlCommandSet.BuildStoredProcedureName(stringBuilder, array[1]);
				stringBuilder.Append(".");
				string[] array2;
				bool flag;
				if (this.Connection.Is2008OrNewer)
				{
					stringBuilder.Append("[sys].[").Append("sp_procedure_params_100_managed").Append("]");
					array2 = SqlCommand.Sql2008ProcParamsNames;
					flag = true;
				}
				else
				{
					if (this.Connection.Is2005OrNewer)
					{
						stringBuilder.Append("[sys].[").Append("sp_procedure_params_managed").Append("]");
					}
					else
					{
						stringBuilder.Append(".[").Append("sp_procedure_params_rowset").Append("]");
					}
					array2 = SqlCommand.PreSql2008ProcParamsNames;
					flag = false;
				}
				sqlCommand = new SqlCommand(stringBuilder.ToString(), this.Connection, this.Transaction)
				{
					CommandType = CommandType.StoredProcedure
				};
				sqlCommand.Parameters.Add(new SqlParameter("@procedure_name", SqlDbType.NVarChar, 255));
				object obj;
				sqlCommand.Parameters[0].Value = SqlCommand.UnquoteProcedureName(array[3], out obj);
				if (obj != null)
				{
					SqlParameter sqlParameter = sqlCommand.Parameters.Add(new SqlParameter("@group_number", SqlDbType.Int));
					sqlParameter.Value = obj;
				}
				if (!ADP.IsEmpty(array[2]))
				{
					SqlParameter sqlParameter2 = sqlCommand.Parameters.Add(new SqlParameter("@procedure_schema", SqlDbType.NVarChar, 255));
					sqlParameter2.Value = SqlCommand.UnquoteProcedurePart(array[2]);
				}
				SqlDataReader sqlDataReader = null;
				List<SqlParameter> list = new List<SqlParameter>();
				bool flag2 = true;
				try
				{
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						SqlParameter sqlParameter3 = new SqlParameter
						{
							ParameterName = (string)sqlDataReader[array2[0]]
						};
						if (flag)
						{
							sqlParameter3.SqlDbType = (SqlDbType)((short)sqlDataReader[array2[3]]);
							SqlDbType sqlDbType = sqlParameter3.SqlDbType;
							if (sqlDbType <= SqlDbType.NText)
							{
								if (sqlDbType != SqlDbType.Image)
								{
									if (sqlDbType != SqlDbType.NText)
									{
										goto IL_02F1;
									}
									sqlParameter3.SqlDbType = SqlDbType.NVarChar;
									goto IL_02F1;
								}
							}
							else
							{
								if (sqlDbType == SqlDbType.Text)
								{
									sqlParameter3.SqlDbType = SqlDbType.VarChar;
									goto IL_02F1;
								}
								if (sqlDbType != SqlDbType.Timestamp)
								{
									goto IL_02F1;
								}
							}
							sqlParameter3.SqlDbType = SqlDbType.VarBinary;
						}
						else
						{
							sqlParameter3.SqlDbType = MetaType.GetSqlDbTypeFromOleDbType((short)sqlDataReader[array2[2]], ADP.IsNull(sqlDataReader[array2[9]]) ? "" : ((string)sqlDataReader[array2[9]]));
						}
						IL_02F1:
						object obj2 = sqlDataReader[array2[4]];
						if (obj2 is int)
						{
							int num = (int)obj2;
							if (num == 0 && (sqlParameter3.SqlDbType == SqlDbType.NVarChar || sqlParameter3.SqlDbType == SqlDbType.VarBinary || sqlParameter3.SqlDbType == SqlDbType.VarChar))
							{
								num = -1;
							}
							sqlParameter3.Size = num;
						}
						sqlParameter3.Direction = this.ParameterDirectionFromOleDbDirection((short)sqlDataReader[array2[1]]);
						if (sqlParameter3.SqlDbType == SqlDbType.Decimal)
						{
							sqlParameter3.ScaleInternal = (byte)((short)sqlDataReader[array2[6]] & 255);
							sqlParameter3.PrecisionInternal = (byte)((short)sqlDataReader[array2[5]] & 255);
						}
						if (SqlDbType.Udt == sqlParameter3.SqlDbType)
						{
							string text;
							if (flag)
							{
								text = (string)sqlDataReader[array2[9]];
							}
							else
							{
								text = (string)sqlDataReader[array2[13]];
							}
							SqlParameter sqlParameter4 = sqlParameter3;
							string[] array3 = new string[5];
							int num2 = 0;
							object obj3 = sqlDataReader[array2[7]];
							array3[num2] = ((obj3 != null) ? obj3.ToString() : null);
							array3[1] = ".";
							int num3 = 2;
							object obj4 = sqlDataReader[array2[8]];
							array3[num3] = ((obj4 != null) ? obj4.ToString() : null);
							array3[3] = ".";
							array3[4] = text;
							sqlParameter4.UdtTypeName = string.Concat(array3);
						}
						if (SqlDbType.Structured == sqlParameter3.SqlDbType)
						{
							SqlParameter sqlParameter5 = sqlParameter3;
							string[] array4 = new string[5];
							int num4 = 0;
							object obj5 = sqlDataReader[array2[7]];
							array4[num4] = ((obj5 != null) ? obj5.ToString() : null);
							array4[1] = ".";
							int num5 = 2;
							object obj6 = sqlDataReader[array2[8]];
							array4[num5] = ((obj6 != null) ? obj6.ToString() : null);
							array4[3] = ".";
							int num6 = 4;
							object obj7 = sqlDataReader[array2[9]];
							array4[num6] = ((obj7 != null) ? obj7.ToString() : null);
							sqlParameter5.TypeName = string.Concat(array4);
							sqlParameter3.IsDerivedParameterTypeName = true;
						}
						if (SqlDbType.Xml == sqlParameter3.SqlDbType)
						{
							object obj8 = sqlDataReader[array2[10]];
							sqlParameter3.XmlSchemaCollectionDatabase = (ADP.IsNull(obj8) ? string.Empty : ((string)obj8));
							obj8 = sqlDataReader[array2[11]];
							sqlParameter3.XmlSchemaCollectionOwningSchema = (ADP.IsNull(obj8) ? string.Empty : ((string)obj8));
							obj8 = sqlDataReader[array2[12]];
							sqlParameter3.XmlSchemaCollectionName = (ADP.IsNull(obj8) ? string.Empty : ((string)obj8));
						}
						if (MetaType._IsVarTime(sqlParameter3.SqlDbType))
						{
							object obj9 = sqlDataReader[array2[14]];
							if (obj9 is int)
							{
								sqlParameter3.ScaleInternal = (byte)((int)obj9 & 255);
							}
						}
						list.Add(sqlParameter3);
					}
				}
				catch (Exception ex)
				{
					flag2 = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (flag2)
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						sqlCommand.Connection = null;
					}
				}
				if (list.Count == 0)
				{
					throw ADP.NoStoredProcedureExists(this.CommandText);
				}
				this.Parameters.Clear();
				foreach (SqlParameter sqlParameter6 in list)
				{
					this._parameters.Add(sqlParameter6);
				}
				return;
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000384A4 File Offset: 0x000366A4
		private ParameterDirection ParameterDirectionFromOleDbDirection(short oledbDirection)
		{
			switch (oledbDirection)
			{
			case 2:
				return ParameterDirection.InputOutput;
			case 3:
				return ParameterDirection.Output;
			case 4:
				return ParameterDirection.ReturnValue;
			default:
				return ParameterDirection.Input;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000384C3 File Offset: 0x000366C3
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._cachedMetaData;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000384CC File Offset: 0x000366CC
		private void CheckNotificationStateAndAutoEnlist()
		{
			if (this.NotificationAutoEnlist && this._activeConnection.Is2005OrNewer)
			{
				string text = SqlCommand.SqlNotificationContext();
				if (!ADP.IsEmpty(text))
				{
					SqlDependency sqlDependency = SqlDependencyPerAppDomainDispatcher.SingletonInstance.LookupDependencyEntry(text);
					if (sqlDependency != null)
					{
						sqlDependency.AddCommandDependency(this);
					}
				}
			}
			if (this.Notification != null && this._sqlDep != null)
			{
				if (this._sqlDep.Options == null)
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
					SqlDependency.IdentityUserNamePair identityUserNamePair;
					if (sqlInternalConnectionTds.Identity != null)
					{
						identityUserNamePair = new SqlDependency.IdentityUserNamePair(sqlInternalConnectionTds.Identity, null);
					}
					else
					{
						identityUserNamePair = new SqlDependency.IdentityUserNamePair(null, sqlInternalConnectionTds.ConnectionOptions.UserID);
					}
					this.Notification.Options = SqlDependency.GetDefaultComposedOptions(this._activeConnection.DataSource, this.InternalTdsConnection.ServerProvidedFailOverPartner, identityUserNamePair, this._activeConnection.Database);
				}
				this.Notification.UserData = this._sqlDep.ComputeHashAndAddToDispatcher(this);
				this._sqlDep.AddToServerList(this._activeConnection.DataSource);
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000385D0 File Offset: 0x000367D0
		[SecurityPermission(SecurityAction.Assert, Infrastructure = true)]
		internal static string SqlNotificationContext()
		{
			return CallContext.GetData("MS.SqlDependencyCookie") as string;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000385E4 File Offset: 0x000367E4
		private Task RunExecuteNonQueryTds(string methodName, bool async, int timeout, bool asyncWrite)
		{
			bool flag = true;
			try
			{
				Task task = this._activeConnection.ValidateAndReconnect(null, timeout);
				if (task != null)
				{
					long reconnectionStart = ADP.TimerCurrent();
					if (async)
					{
						TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
						this._activeConnection.RegisterWaitingForReconnect(completion.Task);
						this._reconnectionCompletionSource = completion;
						CancellationTokenSource timeoutCTS = new CancellationTokenSource();
						AsyncHelper.SetTimeoutException(completion, timeout, () => SQL.CR_ReconnectTimeout(), timeoutCTS.Token);
						AsyncHelper.ContinueTask(task, completion, delegate
						{
							if (completion.Task.IsCompleted)
							{
								return;
							}
							Interlocked.CompareExchange<TaskCompletionSource<object>>(ref this._reconnectionCompletionSource, null, completion);
							timeoutCTS.Cancel();
							Task task3 = this.RunExecuteNonQueryTds(methodName, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart), asyncWrite);
							if (task3 == null)
							{
								completion.SetResult(null);
								return;
							}
							AsyncHelper.ContinueTaskWithState(task3, completion, completion, delegate(object state)
							{
								((TaskCompletionSource<object>)state).SetResult(null);
							}, null, null, null, null, null);
						}, null, null, null, null, this._activeConnection);
						return completion.Task;
					}
					AsyncHelper.WaitForCompletion(task, timeout, delegate
					{
						throw SQL.CR_ReconnectTimeout();
					}, true);
					timeout = TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart);
				}
				if (asyncWrite)
				{
					this._activeConnection.AddWeakReference(this, 2);
				}
				this.GetStateObject(null);
				this.ResetEncryptionState();
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.ExecuteNonQuery|INFO> {0}, Command executed as SQLBATCH.", this.ObjectID);
				Task task2 = this._stateObj.Parser.TdsExecuteSQLBatch(this.CommandText, timeout, this.Notification, this._stateObj, true, false, null);
				this.NotifyDependency();
				bool flag2;
				if (async)
				{
					this._activeConnection.GetOpenTdsConnection(methodName).IncrementAsyncCount();
				}
				else if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag2))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag && !async)
				{
					this.PutStateObject();
				}
			}
			return null;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00038840 File Offset: 0x00036A40
		private void RunExecuteNonQuerySmi(bool sendToPipe)
		{
			SqlInternalConnectionSmi internalSmiConnection = this.InternalSmiConnection;
			SmiRequestExecutor smiRequestExecutor = null;
			try
			{
				smiRequestExecutor = this.SetUpSmiRequest(internalSmiConnection);
				SmiExecuteType smiExecuteType;
				if (sendToPipe)
				{
					smiExecuteType = SmiExecuteType.ToPipe;
				}
				else
				{
					smiExecuteType = SmiExecuteType.NonQuery;
				}
				SmiEventStream smiEventStream = null;
				bool flag = true;
				try
				{
					long num;
					Transaction transaction;
					internalSmiConnection.GetCurrentTransactionPair(out num, out transaction);
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, long, int>("<sc.SqlCommand.RunExecuteNonQuerySmi|ADV> {0}, innerConnection={1}, transactionId=0x{2}, cmdBehavior={3}.", this.ObjectID, internalSmiConnection.ObjectID, num, 0);
					if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
					{
						smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, transaction, CommandBehavior.Default, smiExecuteType);
					}
					else
					{
						smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, CommandBehavior.Default, smiExecuteType);
					}
					while (smiEventStream.HasEvents)
					{
						smiEventStream.ProcessEvent(this.EventSink);
					}
				}
				catch (Exception ex)
				{
					flag = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (smiEventStream != null && flag)
					{
						smiEventStream.Close(this.EventSink);
					}
				}
				this.EventSink.ProcessMessagesAndThrow();
			}
			finally
			{
				if (smiRequestExecutor != null)
				{
					smiRequestExecutor.Close(this.EventSink);
					this.EventSink.ProcessMessagesAndThrow(true);
				}
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00038958 File Offset: 0x00036B58
		private void ResetEncryptionState()
		{
			this.ClearDescribeParameterEncryptionRequests();
			this._internalEndExecuteInitiated = false;
			this.CachingQueryMetadataPostponed = false;
			if (this._parameters != null)
			{
				for (int i = 0; i < this._parameters.Count; i++)
				{
					this._parameters[i].CipherMetadata = null;
					this._parameters[i].HasReceivedMetadata = false;
				}
			}
			ConcurrentDictionary<int, SqlTceCipherInfoEntry> concurrentDictionary = this.keysToBeSentToEnclave;
			if (concurrentDictionary != null)
			{
				concurrentDictionary.Clear();
			}
			this.enclavePackage = null;
			this.requiresEnclaveComputations = false;
			this.enclaveAttestationParameters = null;
			this.customData = null;
			this.customDataLength = 0;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000389F4 File Offset: 0x00036BF4
		private void PrepareTransparentEncryptionFinallyBlock(bool closeDataReader, bool clearDataStructures, bool decrementAsyncCount, bool wasDescribeParameterEncryptionNeeded, ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap, SqlDataReader describeParameterEncryptionDataReader)
		{
			if (clearDataStructures)
			{
				this.ClearDescribeParameterEncryptionRequests();
				if (describeParameterEncryptionRpcOriginalRpcMap != null)
				{
					describeParameterEncryptionRpcOriginalRpcMap = null;
				}
			}
			if (decrementAsyncCount)
			{
				SqlInternalConnectionTds openTdsConnection = this._activeConnection.GetOpenTdsConnection();
				if (openTdsConnection != null)
				{
					openTdsConnection.DecrementAsyncCount();
				}
			}
			if (closeDataReader && describeParameterEncryptionDataReader != null)
			{
				describeParameterEncryptionDataReader.Close();
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00038A38 File Offset: 0x00036C38
		private void PrepareForTransparentEncryption(CommandBehavior cmdBehavior, bool returnStream, bool async, int timeout, TaskCompletionSource<object> completion, out Task returnTask, bool asyncWrite, out bool usedCache, bool inRetry)
		{
			Task task = null;
			bool describeParameterEncryptionNeeded = false;
			SqlDataReader describeParameterEncryptionDataReader = null;
			returnTask = null;
			usedCache = false;
			if (!this.BatchRPCMode && !inRetry && this._parameters != null && this._parameters.Count > 0 && SqlQueryMetadataCache.GetInstance().GetQueryMetadataIfExists(this))
			{
				usedCache = true;
				return;
			}
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap = null;
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				try
				{
					describeParameterEncryptionDataReader = this.TryFetchInputParameterEncryptionInfo(timeout, async, asyncWrite, out describeParameterEncryptionNeeded, out task, out describeParameterEncryptionRpcOriginalRpcMap, inRetry);
					if (describeParameterEncryptionNeeded)
					{
						flag2 = async;
						if (task != null)
						{
							flag = false;
							returnTask = AsyncHelper.CreateContinuationTask(task, delegate
							{
								bool flag6 = true;
								bool flag7 = true;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									this.CheckThrowSNIException();
									SqlInternalConnectionTds openTdsConnection = this._activeConnection.GetOpenTdsConnection();
									if (openTdsConnection != null)
									{
										openTdsConnection.DecrementAsyncCount();
										flag7 = false;
									}
									describeParameterEncryptionDataReader = this.CompleteAsyncExecuteReader(false, true);
									this.ReadDescribeEncryptionParameterResults(describeParameterEncryptionDataReader, describeParameterEncryptionRpcOriginalRpcMap, inRetry);
								}
								catch (Exception ex6)
								{
									flag6 = ADP.IsCatchableExceptionType(ex6);
									throw;
								}
								finally
								{
									SqlCommand <>4__this = this;
									bool flag8 = flag6;
									bool flag9 = flag7;
									<>4__this.PrepareTransparentEncryptionFinallyBlock(flag8, flag6, flag9, describeParameterEncryptionNeeded, describeParameterEncryptionRpcOriginalRpcMap, describeParameterEncryptionDataReader);
								}
							}, null, delegate(Exception exception)
							{
								if (this._cachedAsyncState != null)
								{
									this._cachedAsyncState.ResetAsyncState();
								}
								if (exception != null)
								{
									throw exception;
								}
							});
							flag2 = false;
						}
						else if (async)
						{
							flag = false;
							returnTask = Task.Run(delegate
							{
								bool flag10 = true;
								bool flag11 = true;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									this.CheckThrowSNIException();
									SqlInternalConnectionTds openTdsConnection2 = this._activeConnection.GetOpenTdsConnection();
									if (openTdsConnection2 != null)
									{
										openTdsConnection2.DecrementAsyncCount();
										flag11 = false;
									}
									describeParameterEncryptionDataReader = this.CompleteAsyncExecuteReader(false, true);
									this.ReadDescribeEncryptionParameterResults(describeParameterEncryptionDataReader, describeParameterEncryptionRpcOriginalRpcMap, inRetry);
								}
								catch (Exception ex7)
								{
									flag10 = ADP.IsCatchableExceptionType(ex7);
									throw;
								}
								finally
								{
									SqlCommand <>4__this2 = this;
									bool flag12 = flag10;
									bool flag13 = flag11;
									<>4__this2.PrepareTransparentEncryptionFinallyBlock(flag12, flag10, flag13, describeParameterEncryptionNeeded, describeParameterEncryptionRpcOriginalRpcMap, describeParameterEncryptionDataReader);
								}
							});
							flag2 = false;
						}
						else
						{
							this.ReadDescribeEncryptionParameterResults(describeParameterEncryptionDataReader, describeParameterEncryptionRpcOriginalRpcMap, inRetry);
						}
					}
				}
				catch (Exception ex)
				{
					flag = ADP.IsCatchableExceptionType(ex);
					flag3 = true;
					throw;
				}
				finally
				{
					bool flag4 = (flag && !async) || flag3;
					bool flag5 = flag2 && flag3;
					this.PrepareTransparentEncryptionFinallyBlock(flag4, (flag && !async) || flag3, flag5, describeParameterEncryptionNeeded, describeParameterEncryptionRpcOriginalRpcMap, describeParameterEncryptionDataReader);
				}
			}
			catch (OutOfMemoryException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				this._activeConnection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				this._activeConnection.Abort(ex4);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			catch (Exception ex5)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex5))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00038C60 File Offset: 0x00036E60
		private SqlDataReader TryFetchInputParameterEncryptionInfo(int timeout, bool async, bool asyncWrite, out bool inputParameterEncryptionNeeded, out Task task, out ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap, bool inRetry)
		{
			inputParameterEncryptionNeeded = false;
			task = null;
			describeParameterEncryptionRpcOriginalRpcMap = null;
			byte[] array = null;
			if (this.ShouldUseEnclaveBasedWorkflow)
			{
				SqlConnectionAttestationProtocol attestationProtocol = this._activeConnection.AttestationProtocol;
				string enclaveType = this._activeConnection.Parser.EnclaveType;
				EnclaveSessionParameters enclaveSessionParameters = this.GetEnclaveSessionParameters();
				SqlEnclaveSession sqlEnclaveSession = null;
				EnclaveDelegate.Instance.GetEnclaveSession(attestationProtocol, enclaveType, enclaveSessionParameters, true, inRetry, out sqlEnclaveSession, out this.customData, out this.customDataLength);
				if (sqlEnclaveSession == null)
				{
					this.enclaveAttestationParameters = EnclaveDelegate.Instance.GetAttestationParameters(attestationProtocol, enclaveType, enclaveSessionParameters.AttestationUrl, this.customData, this.customDataLength);
					array = EnclaveDelegate.Instance.GetSerializedAttestationParameters(this.enclaveAttestationParameters, enclaveType);
				}
			}
			if (this.BatchRPCMode)
			{
				Dictionary<_SqlRPC, _SqlRPC> dictionary = new Dictionary<_SqlRPC, _SqlRPC>();
				for (int i = 0; i < this._SqlRPCBatchArray.Length; i++)
				{
					if (this._SqlRPCBatchArray[i].systemParams.Length > 1)
					{
						this._SqlRPCBatchArray[i].needsFetchParameterEncryptionMetadata = true;
						_SqlRPC sqlRPC = new _SqlRPC();
						this.PrepareDescribeParameterEncryptionRequest(this._SqlRPCBatchArray[i], ref sqlRPC, (i == 0) ? array : null);
						dictionary.Add(sqlRPC, this._SqlRPCBatchArray[i]);
					}
				}
				describeParameterEncryptionRpcOriginalRpcMap = new ReadOnlyDictionary<_SqlRPC, _SqlRPC>(dictionary);
				if (describeParameterEncryptionRpcOriginalRpcMap.Count == 0)
				{
					return null;
				}
				inputParameterEncryptionNeeded = true;
				this._sqlRPCParameterEncryptionReqArray = describeParameterEncryptionRpcOriginalRpcMap.Keys.ToArray<_SqlRPC>();
			}
			else if (this.ShouldUseEnclaveBasedWorkflow || this.GetParameterCount(this._parameters) != 0)
			{
				inputParameterEncryptionNeeded = true;
				this._sqlRPCParameterEncryptionReqArray = new _SqlRPC[1];
				_SqlRPC sqlRPC2 = null;
				this.GetRPCObject(0, this.GetParameterCount(this._parameters), ref sqlRPC2, false);
				sqlRPC2.rpcName = this.CommandText;
				sqlRPC2.userParams = this._parameters;
				this.PrepareDescribeParameterEncryptionRequest(sqlRPC2, ref this._sqlRPCParameterEncryptionReqArray[0], array);
			}
			if (inputParameterEncryptionNeeded)
			{
				this.IsDescribeParameterEncryptionRPCCurrentlyInProgress = true;
				return this.RunExecuteReaderTds(CommandBehavior.Default, RunBehavior.ReturnImmediately, true, async, timeout, out task, asyncWrite, false, null, true);
			}
			return null;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00038E3C File Offset: 0x0003703C
		private SqlParameter GetSqlParameterWithQueryText(string queryText)
		{
			return new SqlParameter(null, (queryText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, queryText.Length)
			{
				Value = queryText
			};
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00038E74 File Offset: 0x00037074
		private void PrepareDescribeParameterEncryptionRequest(_SqlRPC originalRpcRequest, ref _SqlRPC describeParameterEncryptionRequest, byte[] attestationParameters = null)
		{
			this.GetRPCObject((attestationParameters == null) ? 2 : 3, 0, ref describeParameterEncryptionRequest, true);
			describeParameterEncryptionRequest.rpcName = "sp_describe_parameter_encryption";
			if (this.BatchRPCMode)
			{
				string text = (string)originalRpcRequest.systemParams[0].Value;
				SqlParameter sqlParameter = describeParameterEncryptionRequest.systemParams[0];
				sqlParameter.SqlDbType = ((text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
				sqlParameter.Value = text;
				sqlParameter.Size = text.Length;
				sqlParameter.Direction = ParameterDirection.Input;
			}
			else
			{
				string text = originalRpcRequest.rpcName;
				if (this.CommandType == CommandType.StoredProcedure)
				{
					describeParameterEncryptionRequest.systemParams[0] = this.BuildStoredProcedureStatementForColumnEncryption(text, originalRpcRequest.userParams);
				}
				else
				{
					SqlParameter sqlParameter2 = describeParameterEncryptionRequest.systemParams[0];
					sqlParameter2.SqlDbType = ((text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
					sqlParameter2.Value = text;
					sqlParameter2.Size = text.Length;
					sqlParameter2.Direction = ParameterDirection.Input;
				}
			}
			string text2 = null;
			if (this.BatchRPCMode)
			{
				if (originalRpcRequest.systemParamCount > 1)
				{
					text2 = (string)originalRpcRequest.systemParams[1].Value;
				}
			}
			else
			{
				SqlParameterCollection sqlParameterCollection = new SqlParameterCollection();
				if (originalRpcRequest.userParams != null)
				{
					for (int i = 0; i < originalRpcRequest.userParams.Count; i++)
					{
						SqlParameter sqlParameter3 = originalRpcRequest.userParams[i];
						sqlParameterCollection.Add(new SqlParameter(sqlParameter3.ParameterName, sqlParameter3.SqlDbType, sqlParameter3.Size, sqlParameter3.Direction, sqlParameter3.Precision, sqlParameter3.Scale, sqlParameter3.SourceColumn, sqlParameter3.SourceVersion, sqlParameter3.SourceColumnNullMapping, sqlParameter3.Value, sqlParameter3.XmlSchemaCollectionDatabase, sqlParameter3.XmlSchemaCollectionOwningSchema, sqlParameter3.XmlSchemaCollectionName)
						{
							CompareInfo = sqlParameter3.CompareInfo,
							TypeName = sqlParameter3.TypeName,
							UdtTypeName = sqlParameter3.UdtTypeName,
							IsNullable = sqlParameter3.IsNullable,
							LocaleId = sqlParameter3.LocaleId,
							Offset = sqlParameter3.Offset
						});
					}
				}
				TdsParser tdsParser = null;
				if (this._activeConnection.Parser != null)
				{
					tdsParser = this._activeConnection.Parser;
					if (tdsParser == null || tdsParser.State == TdsParserState.Broken || tdsParser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
				}
				text2 = this.BuildParamList(tdsParser, sqlParameterCollection, true);
			}
			SqlParameter sqlParameter4 = describeParameterEncryptionRequest.systemParams[1];
			sqlParameter4.SqlDbType = ((text2.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
			sqlParameter4.Size = text2.Length;
			sqlParameter4.Value = text2;
			sqlParameter4.Direction = ParameterDirection.Input;
			if (attestationParameters != null)
			{
				SqlParameter sqlParameter5 = describeParameterEncryptionRequest.systemParams[2];
				sqlParameter5.SqlDbType = SqlDbType.VarBinary;
				sqlParameter5.Size = attestationParameters.Length;
				sqlParameter5.Value = attestationParameters;
				sqlParameter5.Direction = ParameterDirection.Input;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0003914C File Offset: 0x0003734C
		private void ReadDescribeEncryptionParameterResults(SqlDataReader ds, ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap, bool inRetry)
		{
			_SqlRPC sqlRPC = null;
			Dictionary<int, SqlTceCipherInfoEntry> dictionary = new Dictionary<int, SqlTceCipherInfoEntry>();
			int num = 0;
			while (!this.BatchRPCMode || num < this._sqlRPCParameterEncryptionReqArray.Length)
			{
				bool flag = true;
				while (ds.Read())
				{
					int @int = ds.GetInt32(0);
					SqlTceCipherInfoEntry sqlTceCipherInfoEntry;
					if (!dictionary.TryGetValue(@int, out sqlTceCipherInfoEntry))
					{
						sqlTceCipherInfoEntry = new SqlTceCipherInfoEntry(@int);
						dictionary.Add(@int, sqlTceCipherInfoEntry);
					}
					byte[] array = null;
					int num2 = (int)ds.GetBytes(5, 0L, array, 0, 0);
					array = new byte[num2];
					ds.GetBytes(5, 0L, array, 0, num2);
					byte[] array2 = new byte[8];
					ds.GetBytes(4, 0L, array2, 0, array2.Length);
					string @string = ds.GetString(6);
					string string2 = ds.GetString(7);
					sqlTceCipherInfoEntry.Add(array, ds.GetInt32(1), ds.GetInt32(2), ds.GetInt32(3), array2, string2, @string, ds.GetString(8));
					bool flag2 = false;
					if (this._activeConnection.Parser.TceVersionSupported >= 2)
					{
						flag2 = ds.GetBoolean(9);
					}
					else
					{
						flag = false;
					}
					if (flag2)
					{
						if (string.IsNullOrWhiteSpace(this.Connection.EnclaveAttestationUrl) && this.Connection.AttestationProtocol != SqlConnectionAttestationProtocol.None)
						{
							throw SQL.NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe(this._activeConnection.Parser.EnclaveType);
						}
						byte[] array3 = null;
						if (!ds.IsDBNull(10))
						{
							int num3 = (int)ds.GetBytes(10, 0L, array3, 0, 0);
							array3 = new byte[num3];
							ds.GetBytes(10, 0L, array3, 0, num3);
						}
						SqlSecurityUtility.VerifyColumnMasterKeySignature(@string, string2, flag2, array3, this._activeConnection, this);
						int num4 = @int;
						SqlTceCipherInfoEntry sqlTceCipherInfoEntry2;
						if (!dictionary.TryGetValue(num4, out sqlTceCipherInfoEntry2))
						{
							throw SQL.InvalidEncryptionKeyOrdinalEnclaveMetadata(num4, dictionary.Count);
						}
						if (this.keysToBeSentToEnclave == null)
						{
							this.keysToBeSentToEnclave = new ConcurrentDictionary<int, SqlTceCipherInfoEntry>();
							this.keysToBeSentToEnclave.TryAdd(@int, sqlTceCipherInfoEntry2);
						}
						else if (!this.keysToBeSentToEnclave.ContainsKey(@int))
						{
							this.keysToBeSentToEnclave.TryAdd(@int, sqlTceCipherInfoEntry2);
						}
						this.requiresEnclaveComputations = true;
					}
				}
				if (!flag && !ds.NextResult())
				{
					throw SQL.UnexpectedDescribeParamFormatParameterMetadata();
				}
				if (this.BatchRPCMode)
				{
					sqlRPC = null;
					bool flag3 = describeParameterEncryptionRpcOriginalRpcMap.TryGetValue(this._sqlRPCParameterEncryptionReqArray[num++], out sqlRPC);
				}
				else
				{
					sqlRPC = this._rpcArrayOf1[0];
				}
				SqlParameterCollection userParams = sqlRPC.userParams;
				int num5 = ((userParams != null) ? userParams.Count : 0);
				int num6 = 0;
				if (flag)
				{
					if (!ds.NextResult())
					{
						goto IL_034F;
					}
				}
				while (ds.Read())
				{
					string string3 = ds.GetString(1);
					int i = 0;
					while (i < num5)
					{
						SqlParameter sqlParameter = sqlRPC.userParams[i];
						if (sqlParameter.ParameterNameFixed.Equals(string3, StringComparison.Ordinal))
						{
							sqlParameter.HasReceivedMetadata = true;
							num6++;
							byte @byte = ds.GetByte(3);
							if (@byte == 0)
							{
								break;
							}
							byte byte2 = ds.GetByte(2);
							int int2 = ds.GetInt32(4);
							byte byte3 = ds.GetByte(5);
							SqlTceCipherInfoEntry sqlTceCipherInfoEntry;
							if (!dictionary.TryGetValue(int2, out sqlTceCipherInfoEntry))
							{
								throw SQL.InvalidEncryptionKeyOrdinalParameterMetadata(int2, dictionary.Count);
							}
							sqlParameter.CipherMetadata = new SqlCipherMetadata(sqlTceCipherInfoEntry, ushort.MaxValue, byte2, null, @byte, byte3);
							SqlSecurityUtility.DecryptSymmetricKey(sqlParameter.CipherMetadata, this._activeConnection, this);
							int num7 = (int)(sqlRPC.userParamMap[i] >> 32);
							num7 |= 8;
							sqlRPC.userParamMap[i] = ((long)num7 << 32) | (long)i;
							break;
						}
						else
						{
							i++;
						}
					}
				}
				IL_034F:
				if (num6 != num5)
				{
					for (int j = 0; j < num5; j++)
					{
						SqlParameter sqlParameter2 = sqlRPC.userParams[j];
						if (!sqlParameter2.HasReceivedMetadata && sqlParameter2.Direction != ParameterDirection.ReturnValue)
						{
							throw SQL.ParamEncryptionMetadataMissing(sqlParameter2.ParameterName, sqlRPC.GetCommandTextOrRpcName());
						}
					}
				}
				if (this.ShouldUseEnclaveBasedWorkflow && this.enclaveAttestationParameters != null && this.requiresEnclaveComputations)
				{
					if (!ds.NextResult())
					{
						throw SQL.UnexpectedDescribeParamFormatAttestationInfo(this._activeConnection.Parser.EnclaveType);
					}
					bool flag4 = false;
					while (ds.Read())
					{
						if (flag4)
						{
							throw SQL.MultipleRowsReturnedForAttestationInfo();
						}
						int num8 = (int)ds.GetBytes(0, 0L, null, 0, 0);
						byte[] array4 = new byte[num8];
						ds.GetBytes(0, 0L, array4, 0, num8);
						SqlConnectionAttestationProtocol attestationProtocol = this._activeConnection.AttestationProtocol;
						string enclaveType = this._activeConnection.Parser.EnclaveType;
						EnclaveDelegate.Instance.CreateEnclaveSession(attestationProtocol, enclaveType, this.GetEnclaveSessionParameters(), array4, this.enclaveAttestationParameters, this.customData, this.customDataLength, inRetry);
						this.enclaveAttestationParameters = null;
						flag4 = true;
					}
					if (!flag4)
					{
						throw SQL.AttestationInfoNotReturnedFromSqlServer(this._activeConnection.Parser.EnclaveType, this._activeConnection.EnclaveAttestationUrl);
					}
				}
				sqlRPC.needsFetchParameterEncryptionMetadata = false;
				if (!ds.NextResult())
				{
					break;
				}
			}
			if (this.BatchRPCMode)
			{
				for (int k = 0; k < this._SqlRPCBatchArray.Length; k++)
				{
					if (this._SqlRPCBatchArray[k].needsFetchParameterEncryptionMetadata)
					{
						throw SQL.ProcEncryptionMetadataMissing(this._SqlRPCBatchArray[k].rpcName);
					}
				}
			}
			if (!this.BatchRPCMode && this.ShouldCacheEncryptionMetadata && this._parameters != null && this._parameters.Count > 0)
			{
				SqlQueryMetadataCache.GetInstance().AddQueryMetadata(this, true);
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00039678 File Offset: 0x00037878
		internal SqlDataReader RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, string method)
		{
			Task task;
			bool flag;
			return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method, null, this.CommandTimeout, out task, out flag, false, false);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000396A0 File Offset: 0x000378A0
		internal SqlDataReader RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, string method, TaskCompletionSource<object> completion, int timeout, out Task task, out bool usedCache, bool asyncWrite = false, bool inRetry = false)
		{
			bool flag = completion != null;
			usedCache = false;
			task = null;
			this._rowsAffected = -1;
			this._rowsAffectedBySpDescribeParameterEncryption = -1;
			if ((CommandBehavior.SingleRow & cmdBehavior) != CommandBehavior.Default)
			{
				cmdBehavior |= CommandBehavior.SingleResult;
			}
			if (!inRetry)
			{
				this.ValidateCommand(method, flag);
			}
			this.CheckNotificationStateAndAutoEnlist();
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SqlDataReader sqlDataReader;
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				SqlStatistics statistics = this.Statistics;
				if (statistics != null)
				{
					if ((!this.IsDirty && this.IsPrepared && !this._hiddenPrepare) || (this.IsPrepared && this._execType == SqlCommand.EXECTYPE.PREPAREPENDING))
					{
						statistics.SafeIncrement(ref statistics._preparedExecs);
					}
					else
					{
						statistics.SafeIncrement(ref statistics._unpreparedExecs);
					}
				}
				this.ResetEncryptionState();
				if (this._activeConnection.IsContextConnection)
				{
					sqlDataReader = this.RunExecuteReaderSmi(cmdBehavior, runBehavior, returnStream);
				}
				else
				{
					if (this.IsColumnEncryptionEnabled)
					{
						Task task2 = null;
						this.PrepareForTransparentEncryption(cmdBehavior, returnStream, flag, timeout, completion, out task2, asyncWrite && flag, out usedCache, inRetry);
						long num = ADP.TimerCurrent();
						try
						{
							return this.RunExecuteReaderTdsWithTransparentParameterEncryption(cmdBehavior, runBehavior, returnStream, flag, timeout, out task, asyncWrite && flag, inRetry, null, false, task2);
						}
						catch (EnclaveDelegate.RetryableEnclaveQueryExecutionException)
						{
							if (inRetry)
							{
								throw;
							}
							SqlQueryMetadataCache.GetInstance().InvalidateCacheEntry(this);
							this.InvalidateEnclaveSession();
							return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method, completion, TdsParserStaticMethods.GetRemainingTimeout(timeout, num), out task, out usedCache, flag, true);
						}
						catch (SqlException ex)
						{
							if (inRetry || (!usedCache && !this.ShouldUseEnclaveBasedWorkflow))
							{
								throw;
							}
							bool flag2 = false;
							for (int i = 0; i < ex.Errors.Count; i++)
							{
								if ((usedCache && ex.Errors[i].Number == 33514) || (this.ShouldUseEnclaveBasedWorkflow && ex.Errors[i].Number == 33195))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								throw;
							}
							SqlQueryMetadataCache.GetInstance().InvalidateCacheEntry(this);
							this.InvalidateEnclaveSession();
							return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method, completion, TdsParserStaticMethods.GetRemainingTimeout(timeout, num), out task, out usedCache, flag, true);
						}
					}
					sqlDataReader = this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, flag, timeout, out task, asyncWrite && flag, inRetry, null, false);
				}
			}
			catch (OutOfMemoryException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				this._activeConnection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				this._activeConnection.Abort(ex4);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			return sqlDataReader;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003996C File Offset: 0x00037B6C
		private SqlDataReader RunExecuteReaderTdsWithTransparentParameterEncryption(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, bool async, int timeout, out Task task, bool asyncWrite, bool inRetry, SqlDataReader ds = null, bool describeParameterEncryptionRequest = false, Task describeParameterEncryptionTask = null)
		{
			if ((ds == null) & returnStream)
			{
				ds = new SqlDataReader(this, cmdBehavior);
			}
			if (describeParameterEncryptionTask != null)
			{
				long parameterEncryptionStart = ADP.TimerCurrent();
				TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
				AsyncHelper.ContinueTaskWithState(describeParameterEncryptionTask, completion, this, delegate(object state)
				{
					SqlCommand sqlCommand = (SqlCommand)state;
					Task task2 = null;
					sqlCommand.GenerateEnclavePackage();
					sqlCommand.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, parameterEncryptionStart), out task2, asyncWrite, inRetry, ds, false);
					if (task2 == null)
					{
						completion.SetResult(null);
						return;
					}
					AsyncHelper.ContinueTaskWithState(task2, completion, completion, delegate(object state2)
					{
						((TaskCompletionSource<object>)state2).SetResult(null);
					}, null, null, null, null, null);
				}, delegate(Exception exception, object state)
				{
					SqlCommand.CachedAsyncState cachedAsyncState = ((SqlCommand)state)._cachedAsyncState;
					if (cachedAsyncState != null)
					{
						cachedAsyncState.ResetAsyncState();
					}
					if (exception != null)
					{
						throw exception;
					}
				}, delegate(object state)
				{
					SqlCommand.CachedAsyncState cachedAsyncState2 = ((SqlCommand)state)._cachedAsyncState;
					if (cachedAsyncState2 == null)
					{
						return;
					}
					cachedAsyncState2.ResetAsyncState();
				}, null, null, this._activeConnection);
				task = completion.Task;
				return ds;
			}
			this.GenerateEnclavePackage();
			return this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, timeout, out task, asyncWrite, inRetry, ds, false);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00039AB4 File Offset: 0x00037CB4
		private void GenerateEnclavePackage()
		{
			if (this.keysToBeSentToEnclave == null || this.keysToBeSentToEnclave.Count <= 0)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(this._activeConnection.EnclaveAttestationUrl) && this.Connection.AttestationProtocol != SqlConnectionAttestationProtocol.None)
			{
				throw SQL.NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage(this._activeConnection.Parser.EnclaveType);
			}
			string enclaveType = this._activeConnection.Parser.EnclaveType;
			if (string.IsNullOrWhiteSpace(enclaveType))
			{
				throw SQL.EnclaveTypeNullForEnclaveBasedQuery();
			}
			SqlConnectionAttestationProtocol attestationProtocol = this._activeConnection.AttestationProtocol;
			if (attestationProtocol == SqlConnectionAttestationProtocol.NotSpecified)
			{
				throw SQL.AttestationProtocolNotSpecifiedForGeneratingEnclavePackage();
			}
			try
			{
				this.enclavePackage = EnclaveDelegate.Instance.GenerateEnclavePackage(attestationProtocol, this.keysToBeSentToEnclave, this.CommandText, enclaveType, this.GetEnclaveSessionParameters(), this._activeConnection, this);
			}
			catch (EnclaveDelegate.RetryableEnclaveQueryExecutionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw SQL.ExceptionWhenGeneratingEnclavePackage(ex);
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00039B98 File Offset: 0x00037D98
		private SqlDataReader RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, bool async, int timeout, out Task task, bool asyncWrite, bool inRetry, SqlDataReader ds = null, bool describeParameterEncryptionRequest = false)
		{
			if ((ds == null) & returnStream)
			{
				ds = new SqlDataReader(this, cmdBehavior);
			}
			Task task2 = this._activeConnection.ValidateAndReconnect(null, timeout);
			if (task2 != null)
			{
				long reconnectionStart = ADP.TimerCurrent();
				if (async)
				{
					TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
					this._activeConnection.RegisterWaitingForReconnect(completion.Task);
					this._reconnectionCompletionSource = completion;
					CancellationTokenSource timeoutCTS = new CancellationTokenSource();
					AsyncHelper.SetTimeoutException(completion, timeout, () => SQL.CR_ReconnectTimeout(), timeoutCTS.Token);
					AsyncHelper.ContinueTask(task2, completion, delegate
					{
						if (completion.Task.IsCompleted)
						{
							return;
						}
						Interlocked.CompareExchange<TaskCompletionSource<object>>(ref this._reconnectionCompletionSource, null, completion);
						timeoutCTS.Cancel();
						Task task5;
						this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart), out task5, asyncWrite, inRetry, ds, false);
						if (task5 == null)
						{
							completion.SetResult(null);
							return;
						}
						AsyncHelper.ContinueTaskWithState(task5, completion, completion, delegate(object state)
						{
							((TaskCompletionSource<object>)state).SetResult(null);
						}, null, null, null, null, null);
					}, null, null, null, null, this._activeConnection);
					task = completion.Task;
					return ds;
				}
				AsyncHelper.WaitForCompletion(task2, timeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, true);
				timeout = TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart);
			}
			bool flag = (cmdBehavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
			_SqlRPC sqlRPC = null;
			task = null;
			string optionSettings = null;
			bool flag2 = true;
			bool flag3 = false;
			if (async)
			{
				this._activeConnection.GetOpenTdsConnection().IncrementAsyncCount();
				flag3 = true;
			}
			try
			{
				if (asyncWrite)
				{
					this._activeConnection.AddWeakReference(this, 2);
				}
				this.GetStateObject(null);
				Task task3;
				if (describeParameterEncryptionRequest)
				{
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._sqlRPCParameterEncryptionReqArray, timeout, flag, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else if (this.BatchRPCMode)
				{
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._SqlRPCBatchArray, timeout, flag, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
				{
					if (returnStream)
					{
						SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.ExecuteReader|INFO> {0}, Command executed as SQLBATCH.", this.ObjectID);
					}
					string text = this.GetCommandText(cmdBehavior) + this.GetResetOptionsString(cmdBehavior);
					if (this.requiresEnclaveComputations)
					{
						if (this.enclavePackage == null)
						{
							throw SQL.NullEnclavePackageForEnclaveBasedQuery(this._activeConnection.Parser.EnclaveType, this._activeConnection.EnclaveAttestationUrl);
						}
						task3 = this._stateObj.Parser.TdsExecuteSQLBatch(text, timeout, this.Notification, this._stateObj, !asyncWrite, false, this.enclavePackage.EnclavePackageBytes);
					}
					else
					{
						task3 = this._stateObj.Parser.TdsExecuteSQLBatch(text, timeout, this.Notification, this._stateObj, !asyncWrite, false, null);
					}
				}
				else if (CommandType.Text == this.CommandType)
				{
					if (this.IsDirty)
					{
						if (this._execType == SqlCommand.EXECTYPE.PREPARED)
						{
							this._hiddenPrepare = true;
						}
						this.Unprepare();
						this.IsDirty = false;
					}
					if (this._execType == SqlCommand.EXECTYPE.PREPARED)
					{
						sqlRPC = this.BuildExecute(flag);
					}
					else if (this._execType == SqlCommand.EXECTYPE.PREPAREPENDING)
					{
						sqlRPC = this.BuildPrepExec(cmdBehavior);
						this._execType = SqlCommand.EXECTYPE.PREPARED;
						this._preparedConnectionCloseCount = this._activeConnection.CloseCount;
						this._preparedConnectionReconnectCount = this._activeConnection.ReconnectCount;
						this._inPrepare = true;
					}
					else
					{
						this.BuildExecuteSql(cmdBehavior, null, this._parameters, ref sqlRPC);
					}
					if (this._activeConnection.Is2000)
					{
						sqlRPC.options = 2;
					}
					if (returnStream)
					{
						SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.ExecuteReader|INFO> {0}, Command executed as RPC.", this.ObjectID);
					}
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._rpcArrayOf1, timeout, flag, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else
				{
					this.BuildRPC(flag, this._parameters, ref sqlRPC);
					optionSettings = this.GetSetOptionsString(cmdBehavior);
					if (returnStream)
					{
						SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlCommand.ExecuteReader|INFO> {0}, Command executed as RPC.", this.ObjectID);
					}
					if (optionSettings != null)
					{
						Task task4 = this._stateObj.Parser.TdsExecuteSQLBatch(optionSettings, timeout, this.Notification, this._stateObj, true, false, null);
						bool flag4;
						if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag4))
						{
							throw SQL.SynchronousCallMayNotPend();
						}
						optionSettings = this.GetResetOptionsString(cmdBehavior);
					}
					this._activeConnection.CheckSQLDebug();
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._rpcArrayOf1, timeout, flag, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				if (async)
				{
					flag3 = false;
					if (task3 != null)
					{
						task = AsyncHelper.CreateContinuationTask(task3, delegate
						{
							this._activeConnection.GetOpenTdsConnection();
							this.cachedAsyncState.SetAsyncReaderState(ds, runBehavior, optionSettings);
						}, null, delegate(Exception exc)
						{
							this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
						});
					}
					else
					{
						this.cachedAsyncState.SetAsyncReaderState(ds, runBehavior, optionSettings);
					}
				}
				else
				{
					this.FinishExecuteReader(ds, runBehavior, optionSettings, false, false, !describeParameterEncryptionRequest);
				}
			}
			catch (Exception ex)
			{
				flag2 = ADP.IsCatchableExceptionType(ex);
				if (flag3)
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
					if (sqlInternalConnectionTds != null)
					{
						sqlInternalConnectionTds.DecrementAsyncCount();
					}
				}
				throw;
			}
			finally
			{
				if (flag2 && !async)
				{
					this.PutStateObject();
				}
			}
			return ds;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003A260 File Offset: 0x00038460
		private SqlDataReader RunExecuteReaderSmi(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream)
		{
			SqlInternalConnectionSmi internalSmiConnection = this.InternalSmiConnection;
			SmiEventStream smiEventStream = null;
			SqlDataReader sqlDataReader = null;
			SmiRequestExecutor smiRequestExecutor = null;
			try
			{
				smiRequestExecutor = this.SetUpSmiRequest(internalSmiConnection);
				long num;
				Transaction transaction;
				internalSmiConnection.GetCurrentTransactionPair(out num, out transaction);
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, long>("<sc.SqlCommand.RunExecuteReaderSmi|ADV> {0}, innerConnection={1}, transactionId=0x{2}, commandBehavior={(int)cmdBehavior}.", this.ObjectID, internalSmiConnection.ObjectID, num);
				if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
				{
					smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, transaction, cmdBehavior, SmiExecuteType.Reader);
				}
				else
				{
					smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, cmdBehavior, SmiExecuteType.Reader);
				}
				if ((runBehavior & RunBehavior.UntilDone) != (RunBehavior)0)
				{
					while (smiEventStream.HasEvents)
					{
						smiEventStream.ProcessEvent(this.EventSink);
					}
					smiEventStream.Close(this.EventSink);
				}
				if (returnStream)
				{
					sqlDataReader = new SqlDataReaderSmi(smiEventStream, this, cmdBehavior, internalSmiConnection, this.EventSink, smiRequestExecutor);
					sqlDataReader.NextResult();
					this._activeConnection.AddWeakReference(sqlDataReader, 1);
				}
				this.EventSink.ProcessMessagesAndThrow();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableOrSecurityExceptionType(ex))
				{
					throw;
				}
				if (smiEventStream != null)
				{
					smiEventStream.Close(this.EventSink);
				}
				if (smiRequestExecutor != null)
				{
					smiRequestExecutor.Close(this.EventSink);
					this.EventSink.ProcessMessagesAndThrow(true);
				}
				throw;
			}
			return sqlDataReader;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0003A388 File Offset: 0x00038588
		private SqlDataReader CompleteAsyncExecuteReader(bool isInternal = false, bool forDescribeParameterEncryption = false)
		{
			SqlDataReader cachedAsyncReader = this.cachedAsyncState.CachedAsyncReader;
			bool flag = true;
			try
			{
				this.FinishExecuteReader(cachedAsyncReader, this.cachedAsyncState.CachedRunBehavior, this.cachedAsyncState.CachedSetOptions, isInternal, forDescribeParameterEncryption, !forDescribeParameterEncryption);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag)
				{
					if (!isInternal)
					{
						this.cachedAsyncState.ResetAsyncState();
					}
					this.PutStateObject();
				}
			}
			return cachedAsyncReader;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003A408 File Offset: 0x00038608
		private void FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, string resetOptionsString, bool isInternal, bool forDescribeParameterEncryption, bool shouldCacheForAlwaysEncrypted = true)
		{
			if (!isInternal && !forDescribeParameterEncryption)
			{
				this.NotifyDependency();
				if (this._internalEndExecuteInitiated)
				{
					return;
				}
			}
			if (runBehavior == RunBehavior.UntilDone)
			{
				try
				{
					bool flag;
					if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, ds, null, this._stateObj, out flag))
					{
						throw SQL.SynchronousCallMayNotPend();
					}
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						if (this._inPrepare)
						{
							this._inPrepare = false;
							this.IsDirty = true;
							this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
						}
						if (ds != null)
						{
							ds.Close();
						}
					}
					throw;
				}
			}
			if (ds != null)
			{
				ds.Bind(this._stateObj);
				this._stateObj = null;
				ds.ResetOptionsString = resetOptionsString;
				this._activeConnection.AddWeakReference(ds, 1);
				try
				{
					if (shouldCacheForAlwaysEncrypted)
					{
						this._cachedMetaData = ds.MetaData;
					}
					else
					{
						_SqlMetaDataSet metaData = ds.MetaData;
					}
					ds.IsInitialized = true;
				}
				catch (Exception ex2)
				{
					if (ADP.IsCatchableExceptionType(ex2))
					{
						if (this._inPrepare)
						{
							this._inPrepare = false;
							this.IsDirty = true;
							this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
						}
						ds.Close();
					}
					throw;
				}
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003A524 File Offset: 0x00038724
		private void NotifyDependency()
		{
			if (this._sqlDep != null)
			{
				this._sqlDep.StartTimer(this.Notification);
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003A540 File Offset: 0x00038740
		public SqlCommand Clone()
		{
			SqlCommand sqlCommand = new SqlCommand(this);
			SqlClientEventSource.Log.TryTraceEvent<int, int?>("<sc.SqlCommand.Clone|API> {0}, clone={1}", this.ObjectID, (sqlCommand != null) ? new int?(sqlCommand.ObjectID) : null);
			return sqlCommand;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0003A583 File Offset: 0x00038783
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003A58C File Offset: 0x0003878C
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outterTask)
		{
			SqlConnection activeConnection = this._activeConnection;
			if (activeConnection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			activeConnection.RegisterForConnectionCloseNotification<T>(ref outterTask, this, 2);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003A5B4 File Offset: 0x000387B4
		private void ValidateCommand(string method, bool async)
		{
			if (this._activeConnection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
			if (((this.ColumnEncryptionSetting == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._activeConnection.IsColumnEncryptionSettingEnabled) || this.ColumnEncryptionSetting == SqlCommandColumnEncryptionSetting.Enabled || this.ColumnEncryptionSetting == SqlCommandColumnEncryptionSetting.ResultSetOnly) && sqlInternalConnectionTds != null && sqlInternalConnectionTds.Parser != null && !sqlInternalConnectionTds.Parser.IsColumnEncryptionSupported)
			{
				throw SQL.TceNotSupported();
			}
			if (sqlInternalConnectionTds != null)
			{
				TdsParser parser = sqlInternalConnectionTds.Parser;
				if (parser == null || parser.State == TdsParserState.Closed)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Closed);
				}
				if (parser.State != TdsParserState.OpenLoggedIn)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Broken);
				}
			}
			else
			{
				if (this._activeConnection.State == ConnectionState.Closed)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Closed);
				}
				if (this._activeConnection.State == ConnectionState.Broken)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Broken);
				}
			}
			this.ValidateAsyncCommand();
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this._activeConnection.ValidateConnectionForExecute(method, this);
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			if (this._transaction != null && this._transaction.Connection == null)
			{
				this._transaction = null;
			}
			if (this._activeConnection.HasLocalTransactionFromAPI && this._transaction == null)
			{
				throw ADP.TransactionRequired(method);
			}
			if (this._transaction != null && this._activeConnection != this._transaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
			if (ADP.IsEmpty(this.CommandText))
			{
				throw ADP.CommandTextRequired(method);
			}
			if (this.Notification != null && !this._activeConnection.Is2005OrNewer)
			{
				throw SQL.NotificationsRequire2005();
			}
			if (async && this._activeConnection.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003A7A8 File Offset: 0x000389A8
		private void ValidateAsyncCommand()
		{
			if (this.cachedAsyncState.PendingAsyncOperation)
			{
				if (this.cachedAsyncState.IsActiveConnectionValid(this._activeConnection))
				{
					throw SQL.PendingBeginXXXExists();
				}
				this._stateObj = null;
				this.cachedAsyncState.ResetAsyncState();
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003A7E4 File Offset: 0x000389E4
		private void GetStateObject(TdsParser parser = null)
		{
			if (this._pendingCancel)
			{
				this._pendingCancel = false;
				throw SQL.OperationCancelled();
			}
			if (parser == null)
			{
				parser = this._activeConnection.Parser;
				if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			TdsParserStateObject session = parser.GetSession(this);
			session.StartSession(this.ObjectID);
			this._stateObj = session;
			if (this._pendingCancel)
			{
				this._pendingCancel = false;
				throw SQL.OperationCancelled();
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003A868 File Offset: 0x00038A68
		private void ReliablePutStateObject()
		{
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.PutStateObject();
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0003A8EC File Offset: 0x00038AEC
		private void PutStateObject()
		{
			TdsParserStateObject stateObj = this._stateObj;
			this._stateObj = null;
			if (stateObj != null)
			{
				stateObj.CloseSession();
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003A910 File Offset: 0x00038B10
		internal void OnDoneDescribeParameterEncryptionProc(TdsParserStateObject stateObj)
		{
			if (this.BatchRPCMode)
			{
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].cumulativeRecordsAffected = this._rowsAffected;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].recordsAffected = new int?((0 < this._currentlyExecutingDescribeParameterEncryptionRPC && 0 <= this._rowsAffected) ? (this._rowsAffected - Math.Max(this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC - 1].cumulativeRecordsAffected, 0)) : this._rowsAffected);
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].errorsIndexStart = ((0 < this._currentlyExecutingDescribeParameterEncryptionRPC) ? this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC - 1].errorsIndexEnd : 0);
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].errorsIndexEnd = stateObj.ErrorCount;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].errors = stateObj._errors;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].warningsIndexStart = ((0 < this._currentlyExecutingDescribeParameterEncryptionRPC) ? this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC - 1].warningsIndexEnd : 0);
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].warningsIndexEnd = stateObj.WarningCount;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].warnings = stateObj._warnings;
				this._currentlyExecutingDescribeParameterEncryptionRPC++;
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003AA64 File Offset: 0x00038C64
		internal void OnDoneProc()
		{
			if (this.BatchRPCMode)
			{
				SqlCommand.OnDone(this._stateObj, this._currentlyExecutingBatch, this._SqlRPCBatchArray, this._rowsAffected);
				this._currentlyExecutingBatch++;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003AA9C File Offset: 0x00038C9C
		private static void OnDone(TdsParserStateObject stateObj, int index, _SqlRPC[] array, int rowsAffected)
		{
			_SqlRPC sqlRPC = array[index];
			_SqlRPC sqlRPC2 = ((index > 0) ? array[index - 1] : null);
			sqlRPC.cumulativeRecordsAffected = rowsAffected;
			sqlRPC.recordsAffected = new int?((sqlRPC2 != null && 0 <= rowsAffected) ? (rowsAffected - Math.Max(sqlRPC2.cumulativeRecordsAffected, 0)) : rowsAffected);
			sqlRPC.errorsIndexStart = ((sqlRPC2 != null) ? sqlRPC2.errorsIndexEnd : 0);
			sqlRPC.errorsIndexEnd = stateObj.ErrorCount;
			sqlRPC.errors = stateObj._errors;
			sqlRPC.warningsIndexStart = ((sqlRPC2 != null) ? sqlRPC2.warningsIndexEnd : 0);
			sqlRPC.warningsIndexEnd = stateObj.WarningCount;
			sqlRPC.warnings = stateObj._warnings;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003AB38 File Offset: 0x00038D38
		internal void OnReturnStatus(int status)
		{
			if (this._inPrepare)
			{
				return;
			}
			if (this.IsDescribeParameterEncryptionRPCCurrentlyInProgress)
			{
				return;
			}
			SqlParameterCollection sqlParameterCollection = this._parameters;
			if (this.BatchRPCMode)
			{
				if (this._parameterCollectionList.Count > this._currentlyExecutingBatch)
				{
					sqlParameterCollection = this._parameterCollectionList[this._currentlyExecutingBatch];
				}
				else
				{
					sqlParameterCollection = null;
				}
			}
			int parameterCount = this.GetParameterCount(sqlParameterCollection);
			int i = 0;
			while (i < parameterCount)
			{
				SqlParameter sqlParameter = sqlParameterCollection[i];
				if (sqlParameter.Direction == ParameterDirection.ReturnValue)
				{
					object value = sqlParameter.Value;
					if (value != null && value.GetType() == typeof(SqlInt32))
					{
						sqlParameter.Value = new SqlInt32(status);
					}
					else
					{
						sqlParameter.Value = status;
					}
					if (!this.BatchRPCMode && this.CachingQueryMetadataPostponed && this.ShouldCacheEncryptionMetadata && this._parameters != null && this._parameters.Count > 0)
					{
						SqlQueryMetadataCache.GetInstance().AddQueryMetadata(this, false);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003AC38 File Offset: 0x00038E38
		internal void OnReturnValue(SqlReturnValue rec, TdsParserStateObject stateObj)
		{
			if (this._inPrepare)
			{
				if (!rec.value.IsNull)
				{
					this._prepareHandle = rec.value.Int32;
				}
				this._inPrepare = false;
				return;
			}
			SqlParameterCollection currentParameterCollection = this.GetCurrentParameterCollection();
			int parameterCount = this.GetParameterCount(currentParameterCollection);
			SqlParameter parameterForOutputValueExtraction = this.GetParameterForOutputValueExtraction(currentParameterCollection, rec.parameter, parameterCount);
			if (parameterForOutputValueExtraction != null)
			{
				if (rec.cipherMD != null && parameterForOutputValueExtraction.CipherMetadata != null && (parameterForOutputValueExtraction.Direction == ParameterDirection.Output || parameterForOutputValueExtraction.Direction == ParameterDirection.InputOutput || parameterForOutputValueExtraction.Direction == ParameterDirection.ReturnValue))
				{
					if (rec.tdsType != 165)
					{
						throw SQL.InvalidDataTypeForEncryptedParameter(parameterForOutputValueExtraction.ParameterNameFixed, (int)rec.tdsType, 165);
					}
					TdsParser parser = this._activeConnection.Parser;
					if (parser == null || parser.State == TdsParserState.Closed || parser.State == TdsParserState.Broken)
					{
						throw ADP.ClosedConnectionError();
					}
					if (!rec.value.IsNull)
					{
						try
						{
							rec.cipherMD.EncryptionInfo = parameterForOutputValueExtraction.CipherMetadata.EncryptionInfo;
							byte[] array = SqlSecurityUtility.DecryptWithKey(rec.value.ByteArray, rec.cipherMD, this._activeConnection, this);
							if (array != null)
							{
								SqlBuffer sqlBuffer = new SqlBuffer();
								parser.DeserializeUnencryptedValue(sqlBuffer, array, rec, stateObj, rec.NormalizationRuleVersion);
								parameterForOutputValueExtraction.SetSqlBuffer(sqlBuffer);
							}
							return;
						}
						catch (Exception ex)
						{
							throw SQL.ParamDecryptionFailed(parameterForOutputValueExtraction.ParameterNameFixed, null, ex);
						}
					}
					SqlBuffer sqlBuffer2 = new SqlBuffer();
					TdsParser.GetNullSqlValue(sqlBuffer2, rec, SqlCommandColumnEncryptionSetting.Enabled, parser.Connection);
					parameterForOutputValueExtraction.SetSqlBuffer(sqlBuffer2);
					return;
				}
				else
				{
					object value = parameterForOutputValueExtraction.Value;
					if (SqlDbType.Udt == parameterForOutputValueExtraction.SqlDbType)
					{
						try
						{
							this.Connection.CheckGetExtendedUDTInfo(rec, true);
							object obj;
							if (rec.value.IsNull)
							{
								obj = DBNull.Value;
							}
							else
							{
								obj = rec.value.ByteArray;
							}
							parameterForOutputValueExtraction.Value = this.Connection.GetUdtValue(obj, rec, false);
						}
						catch (FileNotFoundException ex2)
						{
							parameterForOutputValueExtraction.SetUdtLoadError(ex2);
						}
						catch (FileLoadException ex3)
						{
							parameterForOutputValueExtraction.SetUdtLoadError(ex3);
						}
						return;
					}
					parameterForOutputValueExtraction.SetSqlBuffer(rec.value);
					MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(rec.type, rec.IsMultiValued);
					if (rec.type == SqlDbType.Decimal)
					{
						parameterForOutputValueExtraction.ScaleInternal = rec.scale;
						parameterForOutputValueExtraction.PrecisionInternal = rec.precision;
					}
					else if (metaTypeFromSqlDbType.IsVarTime)
					{
						parameterForOutputValueExtraction.ScaleInternal = rec.scale;
					}
					else if (rec.type == SqlDbType.Xml)
					{
						SqlCachedBuffer sqlCachedBuffer = parameterForOutputValueExtraction.Value as SqlCachedBuffer;
						if (sqlCachedBuffer != null)
						{
							parameterForOutputValueExtraction.Value = sqlCachedBuffer.ToString();
						}
					}
					if (rec.collation != null)
					{
						parameterForOutputValueExtraction.Collation = rec.collation;
					}
				}
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003AEE8 File Offset: 0x000390E8
		internal void OnParametersAvailableSmi(SmiParameterMetaData[] paramMetaData, ITypedGettersV3 parameterValues)
		{
			for (int i = 0; i < paramMetaData.Length; i++)
			{
				this.OnParameterAvailableSmi(paramMetaData[i], parameterValues, i);
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003AF10 File Offset: 0x00039110
		internal void OnParameterAvailableSmi(SmiParameterMetaData metaData, ITypedGettersV3 parameterValues, int ordinal)
		{
			if (ParameterDirection.Input != metaData.Direction)
			{
				string text = null;
				if (ParameterDirection.ReturnValue != metaData.Direction)
				{
					text = metaData.Name;
				}
				SqlParameterCollection currentParameterCollection = this.GetCurrentParameterCollection();
				int parameterCount = this.GetParameterCount(currentParameterCollection);
				SqlParameter parameterForOutputValueExtraction = this.GetParameterForOutputValueExtraction(currentParameterCollection, text, parameterCount);
				if (parameterForOutputValueExtraction != null)
				{
					parameterForOutputValueExtraction.LocaleId = (int)metaData.LocaleId;
					parameterForOutputValueExtraction.CompareInfo = metaData.CompareOptions;
					SqlBuffer sqlBuffer = new SqlBuffer();
					object obj;
					if (this._activeConnection.Is2008OrNewer)
					{
						obj = ValueUtilsSmi.GetOutputParameterV200Smi(this.OutParamEventSink, (SmiTypedGetterSetter)parameterValues, ordinal, metaData, this._smiRequestContext, sqlBuffer);
					}
					else
					{
						obj = ValueUtilsSmi.GetOutputParameterV3Smi(this.OutParamEventSink, parameterValues, ordinal, metaData, this._smiRequestContext, sqlBuffer);
					}
					if (obj != null)
					{
						parameterForOutputValueExtraction.Value = obj;
						return;
					}
					parameterForOutputValueExtraction.SetSqlBuffer(sqlBuffer);
				}
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003AFD0 File Offset: 0x000391D0
		private SqlParameterCollection GetCurrentParameterCollection()
		{
			if (!this.BatchRPCMode)
			{
				return this._parameters;
			}
			if (this._parameterCollectionList.Count > this._currentlyExecutingBatch)
			{
				return this._parameterCollectionList[this._currentlyExecutingBatch];
			}
			return null;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003B008 File Offset: 0x00039208
		private SqlParameter GetParameterForOutputValueExtraction(SqlParameterCollection parameters, string paramName, int paramCount)
		{
			SqlParameter sqlParameter = null;
			bool flag = false;
			if (paramName == null)
			{
				for (int i = 0; i < paramCount; i++)
				{
					sqlParameter = parameters[i];
					if (sqlParameter.Direction == ParameterDirection.ReturnValue)
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < paramCount; j++)
				{
					sqlParameter = parameters[j];
					if (sqlParameter.Direction != ParameterDirection.Input && sqlParameter.Direction != ParameterDirection.ReturnValue && paramName == sqlParameter.ParameterNameFixed)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				return sqlParameter;
			}
			return null;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003B080 File Offset: 0x00039280
		private void GetRPCObject(int systemParamCount, int userParamCount, ref _SqlRPC rpc, bool forSpDescribeParameterEncryption = false)
		{
			if (rpc == null)
			{
				if (!forSpDescribeParameterEncryption)
				{
					if (this._rpcArrayOf1 == null)
					{
						this._rpcArrayOf1 = new _SqlRPC[1];
						this._rpcArrayOf1[0] = new _SqlRPC();
					}
					rpc = this._rpcArrayOf1[0];
				}
				else
				{
					if (this._rpcForEncryption == null)
					{
						this._rpcForEncryption = new _SqlRPC();
					}
					rpc = this._rpcForEncryption;
				}
			}
			rpc.ProcID = 0;
			rpc.rpcName = null;
			rpc.options = 0;
			rpc.systemParamCount = systemParamCount;
			rpc.recordsAffected = null;
			rpc.cumulativeRecordsAffected = -1;
			rpc.errorsIndexStart = 0;
			rpc.errorsIndexEnd = 0;
			rpc.errors = null;
			rpc.warningsIndexStart = 0;
			rpc.warningsIndexEnd = 0;
			rpc.warnings = null;
			rpc.needsFetchParameterEncryptionMetadata = false;
			SqlParameter[] systemParams = rpc.systemParams;
			int num = ((systemParams != null) ? systemParams.Length : 0);
			if (num < systemParamCount)
			{
				Array.Resize<SqlParameter>(ref rpc.systemParams, systemParamCount);
				Array.Resize<byte>(ref rpc.systemParamOptions, systemParamCount);
				for (int i = num; i < systemParamCount; i++)
				{
					rpc.systemParams[i] = new SqlParameter();
				}
			}
			for (int j = 0; j < systemParamCount; j++)
			{
				rpc.systemParamOptions[j] = 0;
			}
			long[] userParamMap = rpc.userParamMap;
			if (((userParamMap != null) ? userParamMap.Length : 0) < userParamCount)
			{
				Array.Resize<long>(ref rpc.userParamMap, userParamCount);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003B1CC File Offset: 0x000393CC
		private void SetUpRPCParameters(_SqlRPC rpc, bool inSchema, SqlParameterCollection parameters)
		{
			int parameterCount = this.GetParameterCount(parameters);
			int num = 0;
			for (int i = 0; i < parameterCount; i++)
			{
				SqlParameter sqlParameter = parameters[i];
				sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
				if (!sqlParameter.ValidateTypeLengths().IsPlp && sqlParameter.Direction != ParameterDirection.Output)
				{
					sqlParameter.FixStreamDataForNonPLP();
				}
				if (SqlCommand.ShouldSendParameter(sqlParameter, false))
				{
					byte b = 0;
					if (sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Output)
					{
						b = 1;
					}
					if (sqlParameter.CipherMetadata != null)
					{
						b |= 8;
					}
					if (sqlParameter.Direction != ParameterDirection.Output)
					{
						if (sqlParameter.Value == null && (!inSchema || SqlDbType.Structured == sqlParameter.SqlDbType))
						{
							b |= 2;
						}
						if (sqlParameter.IsDerivedParameterTypeName)
						{
							string[] array = MultipartIdentifier.ParseMultipartIdentifier(sqlParameter.TypeName, "[\"", "]\"", Strings.SQL_TDSParserTableName, false);
							if (array != null && array.Length == 4 && array[3] != null && array[2] != null && array[1] != null)
							{
								sqlParameter.TypeName = SqlCommand.QuoteIdentifier(array, 2, 2);
							}
						}
					}
					rpc.userParamMap[num] = (long)(((ulong)b << 32) | (ulong)((long)i));
					num++;
				}
			}
			rpc.userParamCount = num;
			rpc.userParams = parameters;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003B2F8 File Offset: 0x000394F8
		private _SqlRPC BuildPrepExec(CommandBehavior behavior)
		{
			int num = this.CountSendableParameters(this._parameters);
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(3, num, ref sqlRPC, false);
			sqlRPC.ProcID = 13;
			sqlRPC.rpcName = "sp_prepexec";
			SqlParameter sqlParameter = sqlRPC.systemParams[0];
			sqlParameter.SqlDbType = SqlDbType.Int;
			sqlParameter.Value = this._prepareHandle;
			sqlParameter.Size = 4;
			sqlParameter.Direction = ParameterDirection.InputOutput;
			sqlRPC.systemParamOptions[0] = 1;
			string text = this.BuildParamList(this._stateObj.Parser, this._parameters, false);
			sqlParameter = sqlRPC.systemParams[1];
			sqlParameter.SqlDbType = ((text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
			sqlParameter.Value = text;
			sqlParameter.Size = text.Length;
			sqlParameter.Direction = ParameterDirection.Input;
			string commandText = this.GetCommandText(behavior);
			sqlParameter = sqlRPC.systemParams[2];
			sqlParameter.SqlDbType = ((commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
			sqlParameter.Size = commandText.Length;
			sqlParameter.Value = commandText;
			sqlParameter.Direction = ParameterDirection.Input;
			this.SetUpRPCParameters(sqlRPC, false, this._parameters);
			return sqlRPC;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003B41C File Offset: 0x0003961C
		private static bool ShouldSendParameter(SqlParameter p, bool includeReturnValue = false)
		{
			ParameterDirection direction = p.Direction;
			return direction - ParameterDirection.Input <= 2 || (direction == ParameterDirection.ReturnValue && includeReturnValue);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003B440 File Offset: 0x00039640
		private int CountSendableParameters(SqlParameterCollection parameters)
		{
			int num = 0;
			if (parameters != null)
			{
				int count = parameters.Count;
				for (int i = 0; i < count; i++)
				{
					if (SqlCommand.ShouldSendParameter(parameters[i], false))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003B479 File Offset: 0x00039679
		private int GetParameterCount(SqlParameterCollection parameters)
		{
			if (parameters == null)
			{
				return 0;
			}
			return parameters.Count;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003B488 File Offset: 0x00039688
		private void BuildRPC(bool inSchema, SqlParameterCollection parameters, ref _SqlRPC rpc)
		{
			int num = this.CountSendableParameters(parameters);
			this.GetRPCObject(0, num, ref rpc, false);
			int num2 = 2 * this.CommandText.Length;
			rpc.ProcID = 0;
			if (num2 <= 1046)
			{
				rpc.rpcName = this.CommandText;
				this.SetUpRPCParameters(rpc, inSchema, parameters);
				return;
			}
			throw ADP.InvalidArgumentLength("CommandText", 1046);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003B4F0 File Offset: 0x000396F0
		private _SqlRPC BuildExecute(bool inSchema)
		{
			int num = this.CountSendableParameters(this._parameters);
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(1, num, ref sqlRPC, false);
			sqlRPC.ProcID = 12;
			sqlRPC.rpcName = "sp_execute";
			SqlParameter sqlParameter = sqlRPC.systemParams[0];
			sqlParameter.SqlDbType = SqlDbType.Int;
			sqlParameter.Value = this._prepareHandle;
			sqlParameter.Size = 4;
			sqlParameter.Direction = ParameterDirection.Input;
			this.SetUpRPCParameters(sqlRPC, inSchema, this._parameters);
			return sqlRPC;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003B568 File Offset: 0x00039768
		private void BuildExecuteSql(CommandBehavior behavior, string commandText, SqlParameterCollection parameters, ref _SqlRPC rpc)
		{
			int num = this.CountSendableParameters(parameters);
			int num2;
			if (num > 0)
			{
				num2 = 2;
			}
			else
			{
				num2 = 1;
			}
			this.GetRPCObject(num2, num, ref rpc, false);
			rpc.ProcID = 10;
			rpc.rpcName = "sp_executesql";
			if (commandText == null)
			{
				commandText = this.GetCommandText(behavior);
			}
			SqlParameter sqlParameter = rpc.systemParams[0];
			sqlParameter.SqlDbType = ((commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
			sqlParameter.Size = commandText.Length;
			sqlParameter.Value = commandText;
			sqlParameter.Direction = ParameterDirection.Input;
			if (num > 0)
			{
				string text = this.BuildParamList(this._stateObj.Parser, this.BatchRPCMode ? parameters : this._parameters, false);
				sqlParameter = rpc.systemParams[1];
				sqlParameter.SqlDbType = ((text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText);
				sqlParameter.Size = text.Length;
				sqlParameter.Value = text;
				sqlParameter.Direction = ParameterDirection.Input;
				bool flag = (behavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
				this.SetUpRPCParameters(rpc, flag, parameters);
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003B674 File Offset: 0x00039874
		private SqlParameter BuildStoredProcedureStatementForColumnEncryption(string storedProcedureName, SqlParameterCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EXEC ");
			if (parameters == null)
			{
				stringBuilder.Append(SqlCommand.ParseAndQuoteIdentifier(storedProcedureName, false));
				return new SqlParameter(null, (stringBuilder.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, stringBuilder.Length)
				{
					Value = stringBuilder.ToString()
				};
			}
			SqlParameter sqlParameter = null;
			foreach (object obj in parameters)
			{
				SqlParameter sqlParameter2 = (SqlParameter)obj;
				if (sqlParameter2.Direction == ParameterDirection.ReturnValue)
				{
					sqlParameter = sqlParameter2;
					break;
				}
			}
			if (sqlParameter != null)
			{
				stringBuilder.AppendFormat("{0}=", sqlParameter.ParameterNameFixed);
			}
			stringBuilder.Append(SqlCommand.ParseAndQuoteIdentifier(storedProcedureName, false));
			int i = 0;
			int count = parameters.Count;
			if (count > 0)
			{
				while (i < parameters.Count && parameters[i].Direction == ParameterDirection.ReturnValue)
				{
					i++;
				}
				if (i < count)
				{
					stringBuilder.AppendFormat(" {0}={0}", parameters[i].ParameterNameFixed);
					if (parameters[i].Direction == ParameterDirection.Output || parameters[i].Direction == ParameterDirection.InputOutput)
					{
						stringBuilder.AppendFormat(" OUTPUT", Array.Empty<object>());
					}
				}
			}
			for (i++; i < count; i++)
			{
				if (parameters[i].Direction != ParameterDirection.ReturnValue)
				{
					stringBuilder.AppendFormat(", {0}={0}", parameters[i].ParameterNameFixed);
					if (parameters[i].Direction == ParameterDirection.Output || parameters[i].Direction == ParameterDirection.InputOutput)
					{
						stringBuilder.AppendFormat(" OUTPUT", Array.Empty<object>());
					}
				}
			}
			return new SqlParameter(null, (stringBuilder.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, stringBuilder.Length)
			{
				Value = stringBuilder.ToString()
			};
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003B85C File Offset: 0x00039A5C
		internal string BuildParamList(TdsParser parser, SqlParameterCollection parameters, bool includeReturnValue = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			int count = parameters.Count;
			for (int i = 0; i < count; i++)
			{
				SqlParameter sqlParameter = parameters[i];
				sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
				if (SqlCommand.ShouldSendParameter(sqlParameter, includeReturnValue))
				{
					if (flag)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(sqlParameter.ParameterNameFixed);
					MetaType metaType = sqlParameter.InternalMetaType;
					stringBuilder.Append(" ");
					if (metaType.SqlDbType == SqlDbType.Udt)
					{
						string udtTypeName = sqlParameter.UdtTypeName;
						if (ADP.IsEmpty(udtTypeName))
						{
							throw SQL.MustSetUdtTypeNameForUdtParams();
						}
						stringBuilder.Append(SqlCommand.ParseAndQuoteIdentifier(udtTypeName, true));
					}
					else if (metaType.SqlDbType == SqlDbType.Structured)
					{
						string typeName = sqlParameter.TypeName;
						if (ADP.IsEmpty(typeName))
						{
							throw SQL.MustSetTypeNameForParam(metaType.TypeName, sqlParameter.ParameterNameFixed);
						}
						stringBuilder.Append(SqlCommand.ParseAndQuoteIdentifier(typeName, false));
						stringBuilder.Append(" READONLY");
					}
					else
					{
						metaType = sqlParameter.ValidateTypeLengths();
						if (!metaType.IsPlp && sqlParameter.Direction != ParameterDirection.Output)
						{
							sqlParameter.FixStreamDataForNonPLP();
						}
						stringBuilder.Append(metaType.TypeName);
					}
					flag = true;
					if (metaType.SqlDbType == SqlDbType.Decimal)
					{
						byte b = sqlParameter.GetActualPrecision();
						byte actualScale = sqlParameter.GetActualScale();
						stringBuilder.Append('(');
						if (b == 0)
						{
							if (this.Is2000)
							{
								b = 29;
							}
							else
							{
								b = 28;
							}
						}
						stringBuilder.Append(b);
						stringBuilder.Append(',');
						stringBuilder.Append(actualScale);
						stringBuilder.Append(')');
					}
					else if (metaType.IsVarTime)
					{
						byte actualScale2 = sqlParameter.GetActualScale();
						stringBuilder.Append('(');
						stringBuilder.Append(actualScale2);
						stringBuilder.Append(')');
					}
					else if (!metaType.IsFixed && !metaType.IsLong && metaType.SqlDbType != SqlDbType.Timestamp && metaType.SqlDbType != SqlDbType.Udt && SqlDbType.Structured != metaType.SqlDbType)
					{
						int num = sqlParameter.Size;
						stringBuilder.Append('(');
						if (metaType.IsAnsiType)
						{
							object coercedValue = sqlParameter.GetCoercedValue();
							string text = null;
							if (coercedValue != null && DBNull.Value != coercedValue)
							{
								text = coercedValue as string;
								if (text == null)
								{
									SqlString sqlString = ((coercedValue is SqlString) ? ((SqlString)coercedValue) : SqlString.Null);
									if (!sqlString.IsNull)
									{
										text = sqlString.Value;
									}
								}
							}
							if (text != null)
							{
								int encodingCharLength = parser.GetEncodingCharLength(text, sqlParameter.GetActualSize(), sqlParameter.Offset, null);
								if (encodingCharLength > num)
								{
									num = encodingCharLength;
								}
							}
						}
						if (num == 0)
						{
							num = (metaType.IsSizeInCharacters ? 4000 : 8000);
						}
						stringBuilder.Append(num);
						stringBuilder.Append(')');
					}
					else if (metaType.IsPlp && metaType.SqlDbType != SqlDbType.Xml && metaType.SqlDbType != SqlDbType.Udt)
					{
						stringBuilder.Append("(max) ");
					}
					if (sqlParameter.Direction != ParameterDirection.Input)
					{
						stringBuilder.Append(" output");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003BB78 File Offset: 0x00039D78
		private static string ParseAndQuoteIdentifier(string identifier, bool isUdtTypeName)
		{
			string[] array = SqlParameter.ParseTypeName(identifier, isUdtTypeName);
			return ADP.BuildMultiPartName(array);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003BB94 File Offset: 0x00039D94
		private static string QuoteIdentifier(string[] strings, int offset, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = offset; i < offset + length; i++)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append('.');
				}
				if (strings[i] != null && strings[i].Length != 0)
				{
					ADP.AppendQuotedString(stringBuilder, "[", "]", strings[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003BBF0 File Offset: 0x00039DF0
		private string GetSetOptionsString(CommandBehavior behavior)
		{
			string text = null;
			if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly) || CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
			{
				text = " SET FMTONLY OFF;";
				if (CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
				{
					text += " SET NO_BROWSETABLE ON;";
				}
				if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly))
				{
					text += " SET FMTONLY ON;";
				}
			}
			return text;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003BC38 File Offset: 0x00039E38
		private string GetResetOptionsString(CommandBehavior behavior)
		{
			string text = null;
			if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly))
			{
				text += " SET FMTONLY OFF;";
			}
			if (CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
			{
				text += " SET NO_BROWSETABLE OFF;";
			}
			return text;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003BC6C File Offset: 0x00039E6C
		private string GetCommandText(CommandBehavior behavior)
		{
			return this.GetSetOptionsString(behavior) + this.CommandText;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003BC80 File Offset: 0x00039E80
		internal void CheckThrowSNIException()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.CheckThrowSNIException();
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0003BCA0 File Offset: 0x00039EA0
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0003BCBD File Offset: 0x00039EBD
		internal TdsParserStateObject StateObject
		{
			get
			{
				return this._stateObj;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0003BCC5 File Offset: 0x00039EC5
		private bool IsPrepared
		{
			get
			{
				return this._execType > SqlCommand.EXECTYPE.UNPREPARED;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0003BCD0 File Offset: 0x00039ED0
		private bool IsUserPrepared
		{
			get
			{
				return this.IsPrepared && !this._hiddenPrepare && !this.IsDirty;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0003BCF0 File Offset: 0x00039EF0
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0003BD53 File Offset: 0x00039F53
		internal bool IsDirty
		{
			get
			{
				SqlConnection activeConnection = this._activeConnection;
				return this.IsPrepared && (this._dirty || (this._parameters != null && this._parameters.IsDirty) || (activeConnection != null && (activeConnection.CloseCount != this._preparedConnectionCloseCount || activeConnection.ReconnectCount != this._preparedConnectionReconnectCount)));
			}
			set
			{
				this._dirty = value && this.IsPrepared;
				if (this._parameters != null)
				{
					this._parameters.IsDirty = this._dirty;
				}
				this._cachedMetaData = null;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0003BD87 File Offset: 0x00039F87
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0003BD8F File Offset: 0x00039F8F
		internal int RowsAffectedByDescribeParameterEncryption
		{
			get
			{
				return this._rowsAffectedBySpDescribeParameterEncryption;
			}
			set
			{
				if (-1 == this._rowsAffectedBySpDescribeParameterEncryption)
				{
					this._rowsAffectedBySpDescribeParameterEncryption = value;
					return;
				}
				if (0 < value)
				{
					this._rowsAffectedBySpDescribeParameterEncryption += value;
				}
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0003BDBC File Offset: 0x00039FBC
		internal int InternalRecordsAffected
		{
			get
			{
				return this._rowsAffected;
			}
			set
			{
				if (-1 == this._rowsAffected)
				{
					this._rowsAffected = value;
					return;
				}
				if (0 < value)
				{
					this._rowsAffected += value;
				}
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0003BDE1 File Offset: 0x00039FE1
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0003BDE9 File Offset: 0x00039FE9
		internal bool BatchRPCMode
		{
			get
			{
				return this._batchRPCMode;
			}
			set
			{
				this._batchRPCMode = value;
				if (!this._batchRPCMode)
				{
					this.ClearBatchCommand();
					return;
				}
				if (this._RPCList == null)
				{
					this._RPCList = new List<_SqlRPC>();
				}
				if (this._parameterCollectionList == null)
				{
					this._parameterCollectionList = new List<SqlParameterCollection>();
				}
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003BE27 File Offset: 0x0003A027
		private void ClearDescribeParameterEncryptionRequests()
		{
			this._sqlRPCParameterEncryptionReqArray = null;
			this._currentlyExecutingDescribeParameterEncryptionRPC = 0;
			this.IsDescribeParameterEncryptionRPCCurrentlyInProgress = false;
			this._rowsAffectedBySpDescribeParameterEncryption = -1;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003BE48 File Offset: 0x0003A048
		internal void ClearBatchCommand()
		{
			List<_SqlRPC> rpclist = this._RPCList;
			if (rpclist != null)
			{
				rpclist.Clear();
			}
			if (this._parameterCollectionList != null)
			{
				this._parameterCollectionList.Clear();
			}
			this._SqlRPCBatchArray = null;
			this._currentlyExecutingBatch = 0;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003BE86 File Offset: 0x0003A086
		private void SetColumnEncryptionSetting(SqlCommandColumnEncryptionSetting newColumnEncryptionSetting)
		{
			if (!this._wasBatchModeColumnEncryptionSettingSetOnce)
			{
				this._columnEncryptionSetting = newColumnEncryptionSetting;
				this._wasBatchModeColumnEncryptionSettingSetOnce = true;
				return;
			}
			if (this._columnEncryptionSetting != newColumnEncryptionSetting)
			{
				throw SQL.BatchedUpdateColumnEncryptionSettingMismatch();
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003BEB0 File Offset: 0x0003A0B0
		internal void AddBatchCommand(string commandText, SqlParameterCollection parameters, CommandType cmdType, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			_SqlRPC sqlRPC = new _SqlRPC();
			this.CommandText = commandText;
			this.CommandType = cmdType;
			this.SetColumnEncryptionSetting(columnEncryptionSetting);
			this.GetStateObject(null);
			if (cmdType == CommandType.StoredProcedure)
			{
				this.BuildRPC(false, parameters, ref sqlRPC);
			}
			else
			{
				this.BuildExecuteSql(CommandBehavior.Default, commandText, parameters, ref sqlRPC);
			}
			this._RPCList.Add(sqlRPC);
			this._parameterCollectionList.Add(parameters);
			this.ReliablePutStateObject();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003BF19 File Offset: 0x0003A119
		internal int ExecuteBatchRPCCommand()
		{
			this._SqlRPCBatchArray = this._RPCList.ToArray();
			this._currentlyExecutingBatch = 0;
			return this.ExecuteNonQuery();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003BF39 File Offset: 0x0003A139
		internal int? GetRecordsAffected(int commandIndex)
		{
			return this._SqlRPCBatchArray[commandIndex].recordsAffected;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003BF48 File Offset: 0x0003A148
		internal SqlException GetErrors(int commandIndex)
		{
			SqlException ex = null;
			int num = this._SqlRPCBatchArray[commandIndex].errorsIndexEnd - this._SqlRPCBatchArray[commandIndex].errorsIndexStart;
			if (0 < num)
			{
				SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
				for (int i = this._SqlRPCBatchArray[commandIndex].errorsIndexStart; i < this._SqlRPCBatchArray[commandIndex].errorsIndexEnd; i++)
				{
					sqlErrorCollection.Add(this._SqlRPCBatchArray[commandIndex].errors[i]);
				}
				for (int j = this._SqlRPCBatchArray[commandIndex].warningsIndexStart; j < this._SqlRPCBatchArray[commandIndex].warningsIndexEnd; j++)
				{
					sqlErrorCollection.Add(this._SqlRPCBatchArray[commandIndex].warnings[j]);
				}
				ex = SqlException.CreateException(sqlErrorCollection, this.Connection.ServerVersion, this.Connection.ClientConnectionId, null);
			}
			return ex;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0003C020 File Offset: 0x0003A220
		private SmiRequestExecutor SetUpSmiRequest(SqlInternalConnectionSmi innerConnection)
		{
			if (this.Notification != null)
			{
				throw SQL.NotificationsNotAvailableOnContextConnection();
			}
			SmiParameterMetaData[] array = null;
			ParameterPeekAheadValue[] array2 = null;
			int parameterCount = this.GetParameterCount(this.Parameters);
			if (0 < parameterCount)
			{
				array = new SmiParameterMetaData[parameterCount];
				array2 = new ParameterPeekAheadValue[parameterCount];
				for (int i = 0; i < parameterCount; i++)
				{
					SqlParameter sqlParameter = this.Parameters[i];
					sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
					array[i] = sqlParameter.MetaDataForSmi(out array2[i]);
					if (!innerConnection.Is2008OrNewer)
					{
						MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(array[i].SqlDbType, array[i].IsMultiValued);
						if (!metaTypeFromSqlDbType.Is90Supported)
						{
							throw ADP.VersionDoesNotSupportDataType(metaTypeFromSqlDbType.TypeName);
						}
					}
				}
			}
			CommandType commandType = this.CommandType;
			this._smiRequestContext = innerConnection.InternalContext;
			SmiRequestExecutor smiRequestExecutor = this._smiRequestContext.CreateRequestExecutor(this.CommandText, commandType, array, this.EventSink);
			this.EventSink.ProcessMessagesAndThrow();
			for (int j = 0; j < parameterCount; j++)
			{
				if (ParameterDirection.Output != array[j].Direction && ParameterDirection.ReturnValue != array[j].Direction)
				{
					SqlParameter sqlParameter2 = this.Parameters[j];
					object obj = sqlParameter2.GetCoercedValue();
					if (obj is XmlDataFeed && array[j].SqlDbType != SqlDbType.Xml)
					{
						obj = MetaType.GetStringFromXml(((XmlDataFeed)obj)._source);
					}
					ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(array[j].SqlDbType, array[j].IsMultiValued, obj, null, SmiContextFactory.Instance.NegotiatedSmiVersion);
					if (CommandType.StoredProcedure == commandType && ExtendedClrTypeCode.Empty == extendedClrTypeCode)
					{
						smiRequestExecutor.SetDefault(j);
					}
					else
					{
						int size = sqlParameter2.Size;
						if (size != 0 && (long)size != -1L && !sqlParameter2.SizeInferred)
						{
							SqlDbType sqlDbType = array[j].SqlDbType;
							if (sqlDbType != SqlDbType.Image)
							{
								switch (sqlDbType)
								{
								case SqlDbType.NText:
									if (size != 1073741823)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_0308;
								case SqlDbType.NVarChar:
									if (size > 0 && size != 1073741823 && array[j].MaxLength == -1L)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_0308;
								case SqlDbType.Real:
								case SqlDbType.UniqueIdentifier:
								case SqlDbType.SmallDateTime:
								case SqlDbType.SmallInt:
								case SqlDbType.SmallMoney:
								case SqlDbType.TinyInt:
								case (SqlDbType)24:
									goto IL_0308;
								case SqlDbType.Text:
									break;
								case SqlDbType.Timestamp:
									if ((long)size < SmiMetaData.DefaultTimestamp.MaxLength)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_0308;
								case SqlDbType.VarBinary:
								case SqlDbType.VarChar:
									if (size > 0 && size != 2147483647 && array[j].MaxLength == -1L)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_0308;
								case SqlDbType.Variant:
								{
									if (obj == null)
									{
										goto IL_0308;
									}
									MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(obj, true);
									if ((metaTypeFromValue.IsNCharType && (long)size < 4000L) || (metaTypeFromValue.IsBinType && (long)size < 8000L) || (metaTypeFromValue.IsAnsiType && (long)size < 8000L))
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_0308;
								}
								case SqlDbType.Xml:
									if (obj != null && ExtendedClrTypeCode.SqlXml != extendedClrTypeCode)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_0308;
								default:
									goto IL_0308;
								}
							}
							if (size != 2147483647)
							{
								throw SQL.ParameterSizeRestrictionFailure(j);
							}
						}
						IL_0308:
						if (innerConnection.Is2008OrNewer)
						{
							ValueUtilsSmi.SetCompatibleValueV200(this.EventSink, smiRequestExecutor, j, array[j], obj, extendedClrTypeCode, sqlParameter2.Offset, array2[j]);
						}
						else
						{
							ValueUtilsSmi.SetCompatibleValue(this.EventSink, smiRequestExecutor, j, array[j], obj, extendedClrTypeCode, sqlParameter2.Offset);
						}
					}
				}
			}
			return smiRequestExecutor;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003C390 File Offset: 0x0003A590
		private void WriteBeginExecuteEvent()
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			int objectID = this.ObjectID;
			SqlConnection connection = this.Connection;
			string text = ((connection != null) ? connection.DataSource : null);
			SqlConnection connection2 = this.Connection;
			string text2 = ((connection2 != null) ? connection2.Database : null);
			string commandText = this.CommandText;
			SqlConnection connection3 = this.Connection;
			log.TryBeginExecuteEvent(objectID, text, text2, commandText, (connection3 != null) ? new Guid?(connection3.ClientConnectionId) : null, "WriteBeginExecuteEvent");
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003C3FC File Offset: 0x0003A5FC
		private void WriteEndExecuteEvent(bool success, int? sqlExceptionNumber, bool synchronous)
		{
			if (SqlClientEventSource.Log.IsExecutionTraceEnabled())
			{
				int num = ((success > false) ? 1 : 0);
				int num2 = ((sqlExceptionNumber != null) ? 2 : 0);
				int num3 = (synchronous ? 4 : 0);
				int num4 = num | num2 | num3;
				SqlClientEventSource log = SqlClientEventSource.Log;
				int objectID = this.ObjectID;
				int num5 = num4;
				int valueOrDefault = sqlExceptionNumber.GetValueOrDefault();
				SqlConnection connection = this.Connection;
				log.TryEndExecuteEvent(objectID, num5, valueOrDefault, (connection != null) ? new Guid?(connection.ClientConnectionId) : null, "WriteEndExecuteEvent");
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003C474 File Offset: 0x0003A674
		// Note: this type is marked as 'beforefieldinit'.
		static SqlCommand()
		{
			string[] array = new string[15];
			array[0] = "PARAMETER_NAME";
			array[1] = "PARAMETER_TYPE";
			array[2] = "DATA_TYPE";
			array[4] = "CHARACTER_MAXIMUM_LENGTH";
			array[5] = "NUMERIC_PRECISION";
			array[6] = "NUMERIC_SCALE";
			array[7] = "UDT_CATALOG";
			array[8] = "UDT_SCHEMA";
			array[9] = "TYPE_NAME";
			array[10] = "XML_CATALOGNAME";
			array[11] = "XML_SCHEMANAME";
			array[12] = "XML_SCHEMACOLLECTIONNAME";
			array[13] = "UDT_NAME";
			SqlCommand.PreSql2008ProcParamsNames = array;
			SqlCommand.Sql2008ProcParamsNames = new string[]
			{
				"PARAMETER_NAME", "PARAMETER_TYPE", null, "MANAGED_DATA_TYPE", "CHARACTER_MAXIMUM_LENGTH", "NUMERIC_PRECISION", "NUMERIC_SCALE", "TYPE_CATALOG_NAME", "TYPE_SCHEMA_NAME", "TYPE_NAME",
				"XML_CATALOGNAME", "XML_SCHEMANAME", "XML_SCHEMACOLLECTIONNAME", null, "SS_DATETIME_PRECISION"
			};
		}

		// Token: 0x040006AB RID: 1707
		private static int _objectTypeCount;

		// Token: 0x040006AC RID: 1708
		private const int MaxRPCNameLength = 1046;

		// Token: 0x040006AD RID: 1709
		internal readonly int ObjectID = Interlocked.Increment(ref SqlCommand._objectTypeCount);

		// Token: 0x040006AE RID: 1710
		private string _commandText;

		// Token: 0x040006AF RID: 1711
		private CommandType _commandType;

		// Token: 0x040006B0 RID: 1712
		private int? _commandTimeout;

		// Token: 0x040006B1 RID: 1713
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x040006B2 RID: 1714
		private bool _designTimeInvisible;

		// Token: 0x040006B3 RID: 1715
		private bool _wasBatchModeColumnEncryptionSettingSetOnce;

		// Token: 0x040006B4 RID: 1716
		private SqlCommandColumnEncryptionSetting _columnEncryptionSetting;

		// Token: 0x040006B5 RID: 1717
		internal SqlDependency _sqlDep;

		// Token: 0x040006B6 RID: 1718
		internal static readonly Action<object> s_cancelIgnoreFailure = new Action<object>(SqlCommand.CancelIgnoreFailureCallback);

		// Token: 0x040006B7 RID: 1719
		private bool _inPrepare;

		// Token: 0x040006B8 RID: 1720
		private int _prepareHandle = -1;

		// Token: 0x040006B9 RID: 1721
		private bool _hiddenPrepare;

		// Token: 0x040006BA RID: 1722
		private int _preparedConnectionCloseCount = -1;

		// Token: 0x040006BB RID: 1723
		private int _preparedConnectionReconnectCount = -1;

		// Token: 0x040006BC RID: 1724
		private SqlParameterCollection _parameters;

		// Token: 0x040006BD RID: 1725
		private SqlConnection _activeConnection;

		// Token: 0x040006BE RID: 1726
		private bool _dirty;

		// Token: 0x040006BF RID: 1727
		private SqlCommand.EXECTYPE _execType;

		// Token: 0x040006C0 RID: 1728
		private _SqlRPC[] _rpcArrayOf1;

		// Token: 0x040006C1 RID: 1729
		private _SqlRPC _rpcForEncryption;

		// Token: 0x040006C2 RID: 1730
		private _SqlMetaDataSet _cachedMetaData;

		// Token: 0x040006C3 RID: 1731
		internal EnclavePackage enclavePackage;

		// Token: 0x040006C4 RID: 1732
		private SqlEnclaveAttestationParameters enclaveAttestationParameters;

		// Token: 0x040006C5 RID: 1733
		private byte[] customData;

		// Token: 0x040006C6 RID: 1734
		private int customDataLength;

		// Token: 0x040006C7 RID: 1735
		private TaskCompletionSource<object> _reconnectionCompletionSource;

		// Token: 0x040006C8 RID: 1736
		internal ConcurrentDictionary<int, SqlTceCipherInfoEntry> keysToBeSentToEnclave;

		// Token: 0x040006C9 RID: 1737
		internal bool requiresEnclaveComputations;

		// Token: 0x040006CA RID: 1738
		private IReadOnlyDictionary<string, SqlColumnEncryptionKeyStoreProvider> _customColumnEncryptionKeyStoreProviders;

		// Token: 0x040006CB RID: 1739
		private SqlCommand.CachedAsyncState _cachedAsyncState;

		// Token: 0x040006CC RID: 1740
		internal int _rowsAffected = -1;

		// Token: 0x040006CD RID: 1741
		private int _rowsAffectedBySpDescribeParameterEncryption = -1;

		// Token: 0x040006CE RID: 1742
		private SqlNotificationRequest _notification;

		// Token: 0x040006CF RID: 1743
		private bool _notificationAutoEnlist = true;

		// Token: 0x040006D0 RID: 1744
		private SqlTransaction _transaction;

		// Token: 0x040006D1 RID: 1745
		private StatementCompletedEventHandler _statementCompletedEventHandler;

		// Token: 0x040006D2 RID: 1746
		private TdsParserStateObject _stateObj;

		// Token: 0x040006D3 RID: 1747
		private volatile bool _pendingCancel;

		// Token: 0x040006D4 RID: 1748
		private bool _batchRPCMode;

		// Token: 0x040006D5 RID: 1749
		private List<_SqlRPC> _RPCList;

		// Token: 0x040006D6 RID: 1750
		private _SqlRPC[] _SqlRPCBatchArray;

		// Token: 0x040006D7 RID: 1751
		private _SqlRPC[] _sqlRPCParameterEncryptionReqArray;

		// Token: 0x040006D8 RID: 1752
		private List<SqlParameterCollection> _parameterCollectionList;

		// Token: 0x040006D9 RID: 1753
		private int _currentlyExecutingBatch;

		// Token: 0x040006DA RID: 1754
		private SqlRetryLogicBaseProvider _retryLogicProvider;

		// Token: 0x040006DB RID: 1755
		private int _currentlyExecutingDescribeParameterEncryptionRPC;

		// Token: 0x040006DD RID: 1757
		private volatile bool _internalEndExecuteInitiated;

		// Token: 0x040006DF RID: 1759
		private SmiContext _smiRequestContext;

		// Token: 0x040006E0 RID: 1760
		private SqlCommand.CommandEventSink _smiEventSink;

		// Token: 0x040006E1 RID: 1761
		private SmiEventSink_DeferedProcessing _outParamEventSink;

		// Token: 0x040006E3 RID: 1763
		internal static readonly string[] PreSql2008ProcParamsNames;

		// Token: 0x040006E4 RID: 1764
		internal static readonly string[] Sql2008ProcParamsNames;

		// Token: 0x02000215 RID: 533
		private enum EXECTYPE
		{
			// Token: 0x040015A4 RID: 5540
			UNPREPARED,
			// Token: 0x040015A5 RID: 5541
			PREPAREPENDING,
			// Token: 0x040015A6 RID: 5542
			PREPARED
		}

		// Token: 0x02000216 RID: 534
		private sealed class CachedAsyncState
		{
			// Token: 0x06001E31 RID: 7729 RVA: 0x0007C09B File Offset: 0x0007A29B
			internal CachedAsyncState()
			{
			}

			// Token: 0x17000A33 RID: 2611
			// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0007C0B1 File Offset: 0x0007A2B1
			internal SqlDataReader CachedAsyncReader
			{
				get
				{
					return this._cachedAsyncReader;
				}
			}

			// Token: 0x17000A34 RID: 2612
			// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0007C0B9 File Offset: 0x0007A2B9
			internal RunBehavior CachedRunBehavior
			{
				get
				{
					return this._cachedRunBehavior;
				}
			}

			// Token: 0x17000A35 RID: 2613
			// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0007C0C1 File Offset: 0x0007A2C1
			internal string CachedSetOptions
			{
				get
				{
					return this._cachedSetOptions;
				}
			}

			// Token: 0x17000A36 RID: 2614
			// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0007C0C9 File Offset: 0x0007A2C9
			internal bool PendingAsyncOperation
			{
				get
				{
					return this._cachedAsyncResult != null;
				}
			}

			// Token: 0x17000A37 RID: 2615
			// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0007C0D4 File Offset: 0x0007A2D4
			internal string EndMethodName
			{
				get
				{
					return this._cachedEndMethod;
				}
			}

			// Token: 0x06001E37 RID: 7735 RVA: 0x0007C0DC File Offset: 0x0007A2DC
			internal bool IsActiveConnectionValid(SqlConnection activeConnection)
			{
				return this._cachedAsyncConnection == activeConnection && this._cachedAsyncCloseCount == activeConnection.CloseCount;
			}

			// Token: 0x06001E38 RID: 7736 RVA: 0x0007C0F8 File Offset: 0x0007A2F8
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void ResetAsyncState()
			{
				SqlClientEventSource log = SqlClientEventSource.Log;
				string text = "CachedAsyncState.ResetAsyncState | API | ObjectId {0}, Client Connection Id {1}, AsyncCommandInProgress={2}";
				SqlConnection cachedAsyncConnection = this._cachedAsyncConnection;
				int? num = ((cachedAsyncConnection != null) ? new int?(cachedAsyncConnection.ObjectID) : null);
				SqlConnection cachedAsyncConnection2 = this._cachedAsyncConnection;
				Guid? guid = ((cachedAsyncConnection2 != null) ? new Guid?(cachedAsyncConnection2.ClientConnectionId) : null);
				SqlConnection cachedAsyncConnection3 = this._cachedAsyncConnection;
				log.TryTraceEvent<int?, Guid?, bool?>(text, num, guid, (cachedAsyncConnection3 != null) ? new bool?(cachedAsyncConnection3.AsyncCommandInProgress) : null);
				this._cachedAsyncCloseCount = -1;
				this._cachedAsyncResult = null;
				if (this._cachedAsyncConnection != null)
				{
					this._cachedAsyncConnection.AsyncCommandInProgress = false;
					this._cachedAsyncConnection = null;
				}
				this._cachedAsyncReader = null;
				this._cachedRunBehavior = RunBehavior.ReturnImmediately;
				this._cachedSetOptions = null;
				this._cachedEndMethod = null;
			}

			// Token: 0x06001E39 RID: 7737 RVA: 0x0007C1B8 File Offset: 0x0007A3B8
			internal void SetActiveConnectionAndResult(TaskCompletionSource<object> completion, string endMethod, SqlConnection activeConnection)
			{
				TdsParser parser = activeConnection.Parser;
				SqlClientEventSource.Log.TryTraceEvent<int?, Guid?, bool?>("SqlCommand.SetActiveConnectionAndResult | API | ObjectId {0}, Client Connection Id {1}, MARS={2}", (activeConnection != null) ? new int?(activeConnection.ObjectID) : null, (activeConnection != null) ? new Guid?(activeConnection.ClientConnectionId) : null, (parser != null) ? new bool?(parser.MARSOn) : null);
				if (parser == null || parser.State == TdsParserState.Closed || parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
				this._cachedAsyncCloseCount = activeConnection.CloseCount;
				this._cachedAsyncResult = completion;
				if (activeConnection != null && !parser.MARSOn && activeConnection.AsyncCommandInProgress)
				{
					throw SQL.MARSUnsupportedOnConnection();
				}
				this._cachedAsyncConnection = activeConnection;
				this._cachedAsyncConnection.AsyncCommandInProgress = true;
				this._cachedEndMethod = endMethod;
			}

			// Token: 0x06001E3A RID: 7738 RVA: 0x0007C286 File Offset: 0x0007A486
			internal void SetAsyncReaderState(SqlDataReader ds, RunBehavior runBehavior, string optionSettings)
			{
				this._cachedAsyncReader = ds;
				this._cachedRunBehavior = runBehavior;
				this._cachedSetOptions = optionSettings;
			}

			// Token: 0x040015A7 RID: 5543
			private int _cachedAsyncCloseCount = -1;

			// Token: 0x040015A8 RID: 5544
			private TaskCompletionSource<object> _cachedAsyncResult;

			// Token: 0x040015A9 RID: 5545
			private SqlConnection _cachedAsyncConnection;

			// Token: 0x040015AA RID: 5546
			private SqlDataReader _cachedAsyncReader;

			// Token: 0x040015AB RID: 5547
			private RunBehavior _cachedRunBehavior = RunBehavior.ReturnImmediately;

			// Token: 0x040015AC RID: 5548
			private string _cachedSetOptions;

			// Token: 0x040015AD RID: 5549
			private string _cachedEndMethod;
		}

		// Token: 0x02000217 RID: 535
		private sealed class CommandEventSink : SmiEventSink_Default
		{
			// Token: 0x06001E3B RID: 7739 RVA: 0x0007C29D File Offset: 0x0007A49D
			internal CommandEventSink(SqlCommand command)
			{
				this._command = command;
			}

			// Token: 0x06001E3C RID: 7740 RVA: 0x0007C2AC File Offset: 0x0007A4AC
			internal override void StatementCompleted(int rowsAffected)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.SqlCommand.CommandEventSink.StatementCompleted|ADV> {0}, rowsAffected={1}.", this._command.ObjectID, rowsAffected);
				this._command.InternalRecordsAffected = rowsAffected;
			}

			// Token: 0x06001E3D RID: 7741 RVA: 0x0007C2D5 File Offset: 0x0007A4D5
			internal override void BatchCompleted()
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlCommand.CommandEventSink.BatchCompleted|ADV> {0}.", this._command.ObjectID);
			}

			// Token: 0x06001E3E RID: 7742 RVA: 0x0007C2F4 File Offset: 0x0007A4F4
			internal override void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 parameterValues)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int?>("<sc.SqlCommand.CommandEventSink.ParametersAvailable|ADV> {0} metaData.Length={1}.", this._command.ObjectID, (metaData != null) ? new int?(metaData.Length) : null);
				if (SqlClientEventSource.Log.IsAdvancedTraceOn() && metaData != null)
				{
					for (int i = 0; i < metaData.Length; i++)
					{
						SqlClientEventSource.Log.AdvancedTraceEvent<int, int, Type, string>("<sc.SqlCommand.CommandEventSink.ParametersAvailable|ADV> {0}, metaData[{1}] is {2}{3}", this._command.ObjectID, i, metaData[i].GetType(), metaData[i].TraceString());
					}
				}
				this._command.OnParametersAvailableSmi(metaData, parameterValues);
			}

			// Token: 0x06001E3F RID: 7743 RVA: 0x0007C388 File Offset: 0x0007A588
			internal override void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter parameterValues, int ordinal)
			{
				if (SqlClientEventSource.Log.IsAdvancedTraceOn())
				{
					SqlClientEventSource.Log.AdvancedTraceEvent<int, int, Type, string>("<sc.SqlCommand.CommandEventSink.ParameterAvailable|ADV> {0}, metaData[{1}] is {2}{ 3}", this._command.ObjectID, ordinal, (metaData != null) ? metaData.GetType() : null, (metaData != null) ? metaData.TraceString() : null);
				}
				this._command.OnParameterAvailableSmi(metaData, parameterValues, ordinal);
			}

			// Token: 0x040015AE RID: 5550
			private SqlCommand _command;
		}

		// Token: 0x02000218 RID: 536
		private enum ProcParamsColIndex
		{
			// Token: 0x040015B0 RID: 5552
			ParameterName,
			// Token: 0x040015B1 RID: 5553
			ParameterType,
			// Token: 0x040015B2 RID: 5554
			DataType,
			// Token: 0x040015B3 RID: 5555
			ManagedDataType,
			// Token: 0x040015B4 RID: 5556
			CharacterMaximumLength,
			// Token: 0x040015B5 RID: 5557
			NumericPrecision,
			// Token: 0x040015B6 RID: 5558
			NumericScale,
			// Token: 0x040015B7 RID: 5559
			TypeCatalogName,
			// Token: 0x040015B8 RID: 5560
			TypeSchemaName,
			// Token: 0x040015B9 RID: 5561
			TypeName,
			// Token: 0x040015BA RID: 5562
			XmlSchemaCollectionCatalogName,
			// Token: 0x040015BB RID: 5563
			XmlSchemaCollectionSchemaName,
			// Token: 0x040015BC RID: 5564
			XmlSchemaCollectionName,
			// Token: 0x040015BD RID: 5565
			UdtTypeName,
			// Token: 0x040015BE RID: 5566
			DateTimeScale
		}
	}
}
