using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000041 RID: 65
	public abstract class DatabaseClientBase : IDatabaseClient, IIdentifiable
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005C18 File Offset: 0x00003E18
		protected DatabaseClientBase(DatabaseClientCreationContext context)
		{
			this.Context = context;
			this.Name = context.Identity;
			this.m_eventsKit = context.EventsKitFactory.CreateEventsKit<IDatabaseEventsKit>();
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005C44 File Offset: 0x00003E44
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005C4C File Offset: 0x00003E4C
		public string Name { get; private set; }

		// Token: 0x0600018A RID: 394 RVA: 0x00005C55 File Offset: 0x00003E55
		protected internal IAsyncResult BeginExecuteNonQueryText(string text, QueryExecutionOptions options, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.BeginExecute(new DatabaseClientBase.DatabaseSequencerWithTimeout(new DatabaseClientBase.ExecuteNonQueryFlow(this, new DatabaseClientBase.FlowParameters(CommandType.Text, options, text, new SqlParameter[0], exceptionTranslator)), this.Context), this.CreateTicket(), asyncCallback, asyncState);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005C86 File Offset: 0x00003E86
		protected internal IAsyncResult BeginExecuteNonQuery(string procedure, SqlParameter[] parameters, QueryExecutionOptions options, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.BeginExecute(new DatabaseClientBase.DatabaseSequencerWithTimeout(new DatabaseClientBase.ExecuteNonQueryFlow(this, new DatabaseClientBase.FlowParameters(CommandType.StoredProcedure, options, procedure, parameters, exceptionTranslator)), this.Context), this.CreateTicket(), asyncCallback, asyncState);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005CB3 File Offset: 0x00003EB3
		protected internal IAsyncResult BeginExecuteReaderSingleRowText<T>(string text, RowProcessor<T> processor, QueryExecutionOptions executionOptions, QueryResultOptions resultOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.BeginExecute(new DatabaseClientBase.DatabaseSequencerWithTimeout(new DatabaseClientBase.ExecuteReaderSingleRowFlow<T>(this, new DatabaseClientBase.FlowParameters(CommandType.Text, executionOptions, text, new SqlParameter[0], exceptionTranslator), processor, resultOptions), this.Context), this.CreateTicket(), asyncCallback, asyncState);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005CE8 File Offset: 0x00003EE8
		protected internal IAsyncResult BeginExecuteReaderText<T>(string text, RowProcessor<T> processor, QueryExecutionOptions executionOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginExecuteReader<T>(CommandType.Text, text, new SqlParameter[0], processor, executionOptions, exceptionTranslator, asyncCallback, asyncState);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005D0B File Offset: 0x00003F0B
		protected internal IAsyncResult BeginExecuteReaderSingleRow<T>(string procedure, SqlParameter[] parameters, RowProcessor<T> processor, QueryExecutionOptions executionOptions, QueryResultOptions queryOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.BeginExecute(new DatabaseClientBase.DatabaseSequencerWithTimeout(new DatabaseClientBase.ExecuteReaderSingleRowFlow<T>(this, new DatabaseClientBase.FlowParameters(CommandType.StoredProcedure, executionOptions, procedure, parameters, exceptionTranslator), processor, queryOptions), this.Context), this.CreateTicket(), asyncCallback, asyncState);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005D3C File Offset: 0x00003F3C
		protected internal IAsyncResult BeginExecuteReader<T>(string procedure, SqlParameter[] parameters, RowProcessor<T> processor, QueryExecutionOptions executionOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginExecuteReader<T>(CommandType.StoredProcedure, procedure, parameters, processor, executionOptions, exceptionTranslator, asyncCallback, asyncState);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005D5B File Offset: 0x00003F5B
		protected internal IAsyncResult BeginExecuteReader<T>(CommandType commandType, string command, SqlParameter[] parameters, RowProcessor<T> processor, QueryExecutionOptions executionOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.BeginExecute(new DatabaseClientBase.DatabaseSequencerWithTimeout(new DatabaseClientBase.ExecuteReaderFlow<T>(this, new DatabaseClientBase.FlowParameters(commandType, executionOptions, command, parameters, exceptionTranslator), processor), this.Context), this.CreateTicket(), asyncCallback, asyncState);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005D8B File Offset: 0x00003F8B
		protected internal int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			return ((DatabaseClientBase.ExecuteNonQueryFlow)SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.EndExecute(asyncResult).DatabaseFlow).Result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005DA2 File Offset: 0x00003FA2
		protected internal T EndExecuteReaderSingleRow<T>(IAsyncResult asyncResult)
		{
			return ((DatabaseClientBase.ExecuteReaderSingleRowFlow<T>)SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.EndExecute(asyncResult).DatabaseFlow).Result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005DB9 File Offset: 0x00003FB9
		protected internal IEnumerable<T> EndExecuteReader<T>(IAsyncResult asyncResult)
		{
			return ((DatabaseClientBase.ExecuteReaderFlow<T>)SequencerInvoker<DatabaseClientBase.DatabaseSequencerWithTimeout>.EndExecute(asyncResult).DatabaseFlow).Result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005DD0 File Offset: 0x00003FD0
		protected void BulkCopy(string tableName, DataTable data, SqlBulkCopyOptions options)
		{
			this.Context.ModelFactory.CreateSyncActivityAndInvokeWithNewModel(SingletonActivityType<DatabaseBulkInvocationActivity>.Instance, delegate
			{
				DatabaseMonitoringContext databaseMonitoringContext = new DatabaseMonitoringContext(this.m_eventsKit, "bulkcopy_{0}_{1}_{2}".FormatWithInvariantCulture(new object[]
				{
					tableName,
					options,
					data.Rows.Count
				}));
				try
				{
					IDatabaseSpecification enabledSpecification = this.Context.Proxy.GetEnabledSpecification();
					databaseMonitoringContext.NotifySpecification(enabledSpecification);
					using (this.CreateTicket())
					{
						databaseMonitoringContext.NotifyConnectionBegin();
						using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(enabledSpecification.ConnectionString, options))
						{
							databaseMonitoringContext.NotifyConnectionComplete();
							sqlBulkCopy.DestinationTableName = tableName;
							sqlBulkCopy.BulkCopyTimeout = enabledSpecification.CommandTimeout;
							sqlBulkCopy.BatchSize = enabledSpecification.BulkInsertBatchSize;
							foreach (object obj in data.Columns)
							{
								DataColumn dataColumn = (DataColumn)obj;
								sqlBulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
							}
							databaseMonitoringContext.NotifyRequestBegin();
							sqlBulkCopy.WriteToServer(data);
							databaseMonitoringContext.NotifyRequestComplete();
						}
					}
					databaseMonitoringContext.NotifyResponseComplete(0L);
				}
				catch (InvalidOperationException ex)
				{
					throw databaseMonitoringContext.NotifySqlError(ex);
				}
				catch (SqlException ex2)
				{
					throw databaseMonitoringContext.NotifySqlError(ex2);
				}
			});
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005E20 File Offset: 0x00004020
		protected async Task BulkCopyAsync(string tableName, DataTable data, SqlBulkCopyOptions options, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
		{
			DatabaseClientBase.<>c__DisplayClass19_0 CS$<>8__locals1 = new DatabaseClientBase.<>c__DisplayClass19_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.tableName = tableName;
			CS$<>8__locals1.options = options;
			CS$<>8__locals1.data = data;
			CS$<>8__locals1.sqlConnection = sqlConnection;
			CS$<>8__locals1.sqlTransaction = sqlTransaction;
			await this.ExecuteInMonitoredScope(SingletonActivityType<DatabaseBulkInvocationActivity>.Instance, delegate
			{
				DatabaseClientBase.<>c__DisplayClass19_0.<<BulkCopyAsync>b__0>d <<BulkCopyAsync>b__0>d;
				<<BulkCopyAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<BulkCopyAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<BulkCopyAsync>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<BulkCopyAsync>b__0>d.<>t__builder;
				<>t__builder.Start<DatabaseClientBase.<>c__DisplayClass19_0.<<BulkCopyAsync>b__0>d>(ref <<BulkCopyAsync>b__0>d);
				return <<BulkCopyAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005E8F File Offset: 0x0000408F
		protected Task ExecuteInMonitoredScope(ActivityType activityType, Func<Task> asyncMethod)
		{
			return AsyncUtils.ExecuteInMonitoredScope(activityType, this.Context.ActivityFactory, this.Context.ModelFactory, this.CreateTicket(), asyncMethod, null, null, null);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005EB7 File Offset: 0x000040B7
		protected static MonitoredException NoTranslator(SqlException exception)
		{
			return null;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005EBA File Offset: 0x000040BA
		protected static MonitoredException IgnoreNotifyingErrorDecorator([NotNull] MonitoredException mex)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<MonitoredException>(mex, "monitoredException");
			mex.Data["IGNORE_NOTIFYING_ERROR"] = true;
			return mex;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005EE0 File Offset: 0x000040E0
		protected static bool ShouldIgnoreNotifyingError([NotNull] MonitoredException mex)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<MonitoredException>(mex, "monitoredException");
			return mex.Data.Contains("IGNORE_NOTIFYING_ERROR") && mex.Data["IGNORE_NOTIFYING_ERROR"] is bool && (bool)mex.Data["IGNORE_NOTIFYING_ERROR"];
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005F38 File Offset: 0x00004138
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005F40 File Offset: 0x00004140
		private protected DatabaseClientCreationContext Context { protected get; private set; }

		// Token: 0x0600019C RID: 412 RVA: 0x00005F49 File Offset: 0x00004149
		private WorkTicket CreateTicket()
		{
			return this.Context.TicketManager.CreateWorkTicket(this);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005F5C File Offset: 0x0000415C
		protected static EntityRecord ParseEntityRecord(SqlDataReader reader, int startOrd)
		{
			return new EntityRecord(reader.GetInt64(startOrd), DatabaseParameters.GetClientDateTime(reader, startOrd + 1), reader.GetGuid(startOrd + 2));
		}

		// Token: 0x040000B2 RID: 178
		private readonly IDatabaseEventsKit m_eventsKit;

		// Token: 0x040000B3 RID: 179
		private const string c_ignoreNotifyingError = "IGNORE_NOTIFYING_ERROR";

		// Token: 0x040000B4 RID: 180
		protected const int c_replicationLagThresholdInSeconds = 900;

		// Token: 0x02000588 RID: 1416
		private sealed class FlowParameters
		{
			// Token: 0x06002A8E RID: 10894 RVA: 0x000987EA File Offset: 0x000969EA
			public FlowParameters(CommandType commandType, QueryExecutionOptions executionOptions, string text, SqlParameter[] parameters, ExceptionTranslator translator)
			{
				this.CommandType = commandType;
				this.ExecutionOptions = executionOptions;
				this.Text = text;
				this.Parameters = parameters;
				this.ExceptionTranslator = translator;
			}

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x06002A8F RID: 10895 RVA: 0x00098817 File Offset: 0x00096A17
			// (set) Token: 0x06002A90 RID: 10896 RVA: 0x0009881F File Offset: 0x00096A1F
			public CommandType CommandType { get; private set; }

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x06002A91 RID: 10897 RVA: 0x00098828 File Offset: 0x00096A28
			// (set) Token: 0x06002A92 RID: 10898 RVA: 0x00098830 File Offset: 0x00096A30
			public QueryExecutionOptions ExecutionOptions { get; private set; }

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x06002A93 RID: 10899 RVA: 0x00098839 File Offset: 0x00096A39
			// (set) Token: 0x06002A94 RID: 10900 RVA: 0x00098841 File Offset: 0x00096A41
			public string Text { get; private set; }

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x06002A95 RID: 10901 RVA: 0x0009884A File Offset: 0x00096A4A
			// (set) Token: 0x06002A96 RID: 10902 RVA: 0x00098852 File Offset: 0x00096A52
			public SqlParameter[] Parameters { get; private set; }

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x06002A97 RID: 10903 RVA: 0x0009885B File Offset: 0x00096A5B
			// (set) Token: 0x06002A98 RID: 10904 RVA: 0x00098863 File Offset: 0x00096A63
			public ExceptionTranslator ExceptionTranslator { get; private set; }

			// Token: 0x06002A99 RID: 10905 RVA: 0x0009886C File Offset: 0x00096A6C
			public override string ToString()
			{
				return "<Type={0}, Text={1}>".FormatWithInvariantCulture(new object[]
				{
					this.CommandType,
					(this.CommandType == CommandType.StoredProcedure) ? this.Text : "N/A"
				});
			}
		}

		// Token: 0x02000589 RID: 1417
		private class DatabaseSequencerWithTimeout : Sequencer
		{
			// Token: 0x06002A9A RID: 10906 RVA: 0x000988A5 File Offset: 0x00096AA5
			public DatabaseSequencerWithTimeout(DatabaseClientBase.FlowBase databaseFlow, DatabaseClientCreationContext context)
			{
				this.m_context = context;
				this.DatabaseFlow = databaseFlow;
			}

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000988BB File Offset: 0x00096ABB
			// (set) Token: 0x06002A9C RID: 10908 RVA: 0x000988C3 File Offset: 0x00096AC3
			public DatabaseClientBase.FlowBase DatabaseFlow { get; private set; }

			// Token: 0x06002A9D RID: 10909 RVA: 0x000988CC File Offset: 0x00096ACC
			protected override IEnumerable<IFlowStep> Run()
			{
				TimeSpan commandTimeout = TimeSpan.FromSeconds((double)this.m_context.Proxy.GetSpecification().CommandTimeout);
				SequencerWithTimeout sequencerWithTimeout = new SequencerWithTimeout(this.DatabaseFlow, commandTimeout);
				yield return base.RunAsyncStep("Running database flow with timeout of '{0}'".FormatWithInvariantCulture(new object[] { commandTimeout }), delegate(string step, Exception e)
				{
					if (e is SequencerTimeoutException)
					{
						DatabaseTimeoutException ex = new DatabaseTimeoutException(commandTimeout);
						this.DatabaseFlow.MonitoringContext.NotifyTimeout((long)commandTimeout.TotalMilliseconds, ex);
						return ex;
					}
					return e;
				}, new Sequencer.AsyncBeginFunction(sequencerWithTimeout.BeginExecute), new Sequencer.AsyncEndFunction(sequencerWithTimeout.EndExecute));
				yield break;
			}

			// Token: 0x04000F12 RID: 3858
			private DatabaseClientCreationContext m_context;
		}

		// Token: 0x0200058A RID: 1418
		private abstract class FlowBase : MonitoredActivitySequencer
		{
			// Token: 0x06002A9E RID: 10910 RVA: 0x000988DC File Offset: 0x00096ADC
			protected FlowBase(DatabaseClientBase owner, DatabaseClientBase.FlowParameters parameters, AsyncActivity activity)
				: base(activity, owner.Context.ModelFactory, true, null)
			{
				this.m_owner = owner;
				this.m_params = parameters;
				this.m_blh = new BottomLevelHandler<DatabaseClientBase.FlowBase, object>(DatabaseClientBase.FlowBase.s_mex, this);
				this.MonitoringContext = new DatabaseMonitoringContext(owner.m_eventsKit, (parameters.CommandType == CommandType.StoredProcedure) ? parameters.Text : "SQLText");
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00098943 File Offset: 0x00096B43
			// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x0009894B File Offset: 0x00096B4B
			public DatabaseMonitoringContext MonitoringContext { get; private set; }

			// Token: 0x06002AA1 RID: 10913
			protected abstract void CustomizeCommand(DatabaseCommand command, DatabaseClientBase.FlowParameters parameters);

			// Token: 0x06002AA2 RID: 10914
			protected abstract RetrySequence GetRetrySequence(DatabaseCommand command, IThrottler throttler);

			// Token: 0x06002AA3 RID: 10915
			protected abstract long Epilog(DatabaseCommand command, RetrySequence sequence);

			// Token: 0x06002AA4 RID: 10916 RVA: 0x00098954 File Offset: 0x00096B54
			protected sealed override IEnumerable<IFlowStep> Run()
			{
				RetrySequence retrySequence = null;
				DatabaseCommand command = null;
				this.m_blh.Run(null, delegate
				{
					IDatabaseSpecification enabledSpecification = this.m_owner.Context.Proxy.GetEnabledSpecification();
					this.MonitoringContext.NotifySpecification(enabledSpecification);
					command = this.CreateCommand();
					this.CustomizeCommand(command, this.m_params);
					if (enabledSpecification.OperationMode == StorageOperationMode.None || (enabledSpecification.OperationMode == StorageOperationMode.Read && this.m_params.ExecutionOptions.HasFlag(QueryExecutionOptions.Modify)))
					{
						DatabaseStorageModeViolationException ex2 = new DatabaseStorageModeViolationException(enabledSpecification.OperationMode, this.m_params.ExecutionOptions.HasFlag(QueryExecutionOptions.Modify) ? StorageOperationMode.Write : StorageOperationMode.Read);
						this.MonitoringContext.NotifyError(ex2);
						throw ex2;
					}
					IThrottler throttler = (this.m_params.ExecutionOptions.HasFlag(QueryExecutionOptions.UnThrottled) ? null : enabledSpecification.Throttler);
					retrySequence = this.GetRetrySequence(command, throttler);
				});
				yield return base.RunAsyncStep("Run {0}".FormatWithInvariantCulture(new object[] { this.m_params }), (string step, Exception ex) => this.TranslateException(ex) ?? ex, new Sequencer.AsyncBeginFunction(retrySequence.BeginExecute), new Sequencer.AsyncEndFunction(retrySequence.EndExecute));
				this.m_blh.Run(null, delegate
				{
					this.MonitoringContext.NotifyResponseBegin();
					long num = this.Epilog(command, retrySequence);
					this.MonitoringContext.NotifyResponseComplete(num);
				});
				yield break;
			}

			// Token: 0x06002AA5 RID: 10917 RVA: 0x00098964 File Offset: 0x00096B64
			private void HandleException(Exception ex)
			{
				MonitoredException ex2 = this.TranslateException(ex);
				if (ex2 != null)
				{
					throw ex2;
				}
			}

			// Token: 0x06002AA6 RID: 10918 RVA: 0x00098980 File Offset: 0x00096B80
			private MonitoredException TranslateException(Exception ex)
			{
				MonitoredException ex2 = null;
				SqlException ex3 = ex as SqlException;
				if (ex3 != null)
				{
					if (this.m_params.ExceptionTranslator != null && DatabaseErrors.GetErrorCode(ex3) == 50000)
					{
						ex2 = this.m_params.ExceptionTranslator(ex3);
						if (ex2 != null && !DatabaseClientBase.ShouldIgnoreNotifyingError(ex2))
						{
							this.MonitoringContext.NotifyError(ex2);
						}
					}
					if (ex2 == null)
					{
						ex2 = this.MonitoringContext.NotifySqlError(ex3);
					}
				}
				InvalidOperationException ex4 = ex as InvalidOperationException;
				if (ex4 != null)
				{
					ex2 = this.MonitoringContext.NotifySqlError(ex4);
				}
				return ex2;
			}

			// Token: 0x06002AA7 RID: 10919 RVA: 0x00098A04 File Offset: 0x00096C04
			private DatabaseCommand CreateCommand()
			{
				IDatabaseSpecification enabledSpecification = this.m_owner.Context.Proxy.GetEnabledSpecification();
				SqlConnection sqlConnection = new SqlConnection(enabledSpecification.ConnectionString);
				sqlConnection.InfoMessage += DatabaseClientBase.FlowBase.OnConnectionInfoMessage;
				DatabaseCommand databaseCommand2;
				using (DisposeController disposeController = new DisposeController(sqlConnection))
				{
					SqlCommand sqlCommand = sqlConnection.CreateCommand();
					sqlCommand.CommandText = this.m_params.Text;
					sqlCommand.CommandType = this.m_params.CommandType;
					CommandType commandType = this.m_params.CommandType;
					if (commandType == CommandType.Text || commandType == CommandType.StoredProcedure)
					{
						sqlCommand.Parameters.AddRange(this.m_params.Parameters);
					}
					DatabaseCommand databaseCommand = new DatabaseCommand(sqlCommand, enabledSpecification);
					disposeController.PreventDispose();
					databaseCommand2 = databaseCommand;
				}
				return databaseCommand2;
			}

			// Token: 0x06002AA8 RID: 10920 RVA: 0x00098AD0 File Offset: 0x00096CD0
			private static void OnConnectionInfoMessage(object sender, SqlInfoMessageEventArgs e)
			{
				if (TraceSourceBase<StorageTrace>.Tracer.ShouldTrace(TraceVerbosity.Fatal))
				{
					TraceVerbosity traceVerbosity = TraceVerbosity.Verbose;
					if (e.Message.Length > 1)
					{
						for (int i = 0; i < e.Message.Length - 1; i++)
						{
							if (e.Message[i] == '|')
							{
								TraceVerbosity traceVerbosity2;
								switch (e.Message[i + 1])
								{
								case '1':
									traceVerbosity2 = TraceVerbosity.Fatal;
									break;
								case '2':
									traceVerbosity2 = TraceVerbosity.Error;
									break;
								case '3':
									traceVerbosity2 = TraceVerbosity.Warning;
									break;
								case '4':
									traceVerbosity2 = TraceVerbosity.Info;
									break;
								default:
									traceVerbosity2 = TraceVerbosity.Verbose;
									break;
								}
								if (traceVerbosity2 < traceVerbosity)
								{
									traceVerbosity = traceVerbosity2;
								}
							}
						}
					}
					TraceSourceBase<StorageTrace>.Tracer.Trace(traceVerbosity, e.Message);
				}
			}

			// Token: 0x04000F14 RID: 3860
			private readonly DatabaseClientBase.FlowParameters m_params;

			// Token: 0x04000F15 RID: 3861
			private readonly DatabaseClientBase m_owner;

			// Token: 0x04000F16 RID: 3862
			private readonly BottomLevelHandler<DatabaseClientBase.FlowBase, object> m_blh;

			// Token: 0x04000F17 RID: 3863
			private static readonly Dictionary<Type, Action<Exception, DatabaseClientBase.FlowBase, object>> s_mex = new Dictionary<Type, Action<Exception, DatabaseClientBase.FlowBase, object>>
			{
				{
					typeof(SqlException),
					delegate(Exception ex, DatabaseClientBase.FlowBase seq, object obj)
					{
						seq.HandleException(ex);
					}
				},
				{
					typeof(InvalidOperationException),
					delegate(Exception ex, DatabaseClientBase.FlowBase seq, object obj)
					{
						seq.HandleException(ex);
					}
				}
			};
		}

		// Token: 0x0200058B RID: 1419
		private sealed class ExecuteNonQueryFlow : DatabaseClientBase.FlowBase
		{
			// Token: 0x06002AAA RID: 10922 RVA: 0x00098BCF File Offset: 0x00096DCF
			public ExecuteNonQueryFlow(DatabaseClientBase owner, DatabaseClientBase.FlowParameters parameters)
				: base(owner, parameters, owner.Context.ActivityFactory.CreateAsyncActivity(SingletonActivityType<DatabaseRequestActivity>.Instance))
			{
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x06002AAB RID: 10923 RVA: 0x00098BEE File Offset: 0x00096DEE
			// (set) Token: 0x06002AAC RID: 10924 RVA: 0x00098BF6 File Offset: 0x00096DF6
			public int Result { get; private set; }

			// Token: 0x06002AAD RID: 10925 RVA: 0x00098C00 File Offset: 0x00096E00
			protected override void CustomizeCommand(DatabaseCommand command, DatabaseClientBase.FlowParameters parameters)
			{
				if (parameters.ExecutionOptions.HasFlag(QueryExecutionOptions.ReturnValue))
				{
					SqlParameter sqlParameter = new SqlParameter("@__rc", DbType.Int32)
					{
						Direction = ParameterDirection.ReturnValue
					};
					command.Command.Parameters.Add(sqlParameter);
				}
			}

			// Token: 0x06002AAE RID: 10926 RVA: 0x00098C50 File Offset: 0x00096E50
			protected override RetrySequence GetRetrySequence(DatabaseCommand command, IThrottler throttler)
			{
				return new NonQueryRetrySequence(throttler, command, base.MonitoringContext);
			}

			// Token: 0x06002AAF RID: 10927 RVA: 0x00098C5F File Offset: 0x00096E5F
			protected override long Epilog(DatabaseCommand command, RetrySequence sequence)
			{
				if (command.Command.Parameters.Contains("@__rc"))
				{
					this.Result = (int)command.Command.Parameters["@__rc"].Value;
				}
				return 0L;
			}

			// Token: 0x04000F19 RID: 3865
			private const string c_retvalParam = "@__rc";
		}

		// Token: 0x0200058C RID: 1420
		private sealed class ExecuteReaderFlow<T> : DatabaseClientBase.FlowBase
		{
			// Token: 0x06002AB0 RID: 10928 RVA: 0x00098CA0 File Offset: 0x00096EA0
			public ExecuteReaderFlow(DatabaseClientBase owner, DatabaseClientBase.FlowParameters parameters, RowProcessor<T> processor)
				: base(owner, parameters, owner.Context.ActivityFactory.CreateAsyncActivity(SingletonActivityType<DatabaseRequestActivity>.Instance))
			{
				ExtendedDiagnostics.EnsureOperation(!parameters.ExecutionOptions.HasFlag(QueryExecutionOptions.ReturnValue), "rowset returning queries should not have return values");
				this.m_processor = processor;
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x00098CF4 File Offset: 0x00096EF4
			// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x00098CFC File Offset: 0x00096EFC
			public IEnumerable<T> Result { get; private set; }

			// Token: 0x06002AB3 RID: 10931 RVA: 0x00009B3B File Offset: 0x00007D3B
			protected override void CustomizeCommand(DatabaseCommand command, DatabaseClientBase.FlowParameters parameters)
			{
			}

			// Token: 0x06002AB4 RID: 10932 RVA: 0x00098D05 File Offset: 0x00096F05
			protected override RetrySequence GetRetrySequence(DatabaseCommand command, IThrottler throttler)
			{
				return new QueryRetrySequence(throttler, command, CommandBehavior.CloseConnection, base.MonitoringContext);
			}

			// Token: 0x06002AB5 RID: 10933 RVA: 0x00098D18 File Offset: 0x00096F18
			protected override long Epilog(DatabaseCommand command, RetrySequence sequencer)
			{
				QueryRetrySequence queryRetrySequence = (QueryRetrySequence)sequencer;
				List<T> list = new List<T>();
				using (queryRetrySequence.Reader)
				{
					while (queryRetrySequence.Reader.Read())
					{
						list.Add(this.m_processor(queryRetrySequence.Reader));
					}
					while (queryRetrySequence.Reader.NextResult())
					{
					}
				}
				this.Result = list;
				return (long)list.Count;
			}

			// Token: 0x04000F1B RID: 3867
			private readonly RowProcessor<T> m_processor;
		}

		// Token: 0x0200058D RID: 1421
		private sealed class ExecuteReaderSingleRowFlow<T> : DatabaseClientBase.FlowBase
		{
			// Token: 0x06002AB6 RID: 10934 RVA: 0x00098D98 File Offset: 0x00096F98
			public ExecuteReaderSingleRowFlow(DatabaseClientBase owner, DatabaseClientBase.FlowParameters parameters, RowProcessor<T> processor, QueryResultOptions options)
				: base(owner, parameters, owner.Context.ActivityFactory.CreateAsyncActivity(SingletonActivityType<DatabaseRequestActivity>.Instance))
			{
				ExtendedDiagnostics.EnsureOperation(!parameters.ExecutionOptions.HasFlag(QueryExecutionOptions.ReturnValue), "rowset returning queries should not have return values");
				this.m_processor = processor;
				this.m_options = options;
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00098DF4 File Offset: 0x00096FF4
			// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x00098DFC File Offset: 0x00096FFC
			public T Result { get; private set; }

			// Token: 0x06002AB9 RID: 10937 RVA: 0x00009B3B File Offset: 0x00007D3B
			protected override void CustomizeCommand(DatabaseCommand command, DatabaseClientBase.FlowParameters parameters)
			{
			}

			// Token: 0x06002ABA RID: 10938 RVA: 0x00098D05 File Offset: 0x00096F05
			protected override RetrySequence GetRetrySequence(DatabaseCommand command, IThrottler throttler)
			{
				return new QueryRetrySequence(throttler, command, CommandBehavior.CloseConnection, base.MonitoringContext);
			}

			// Token: 0x06002ABB RID: 10939 RVA: 0x00098E08 File Offset: 0x00097008
			protected override long Epilog(DatabaseCommand command, RetrySequence sequencer)
			{
				long num = 0L;
				QueryRetrySequence queryRetrySequence = (QueryRetrySequence)sequencer;
				using (queryRetrySequence.Reader)
				{
					if (queryRetrySequence.Reader.Read())
					{
						this.Result = this.m_processor(queryRetrySequence.Reader);
						num = 1L;
					}
					else
					{
						while (queryRetrySequence.Reader.NextResult())
						{
						}
						if (this.m_options.HasFlag(QueryResultOptions.SingleRowExpected))
						{
							DatabaseEmptyResultException ex = new DatabaseEmptyResultException(typeof(T).Name);
							base.MonitoringContext.NotifyError(ex);
							throw ex;
						}
					}
				}
				return num;
			}

			// Token: 0x04000F1D RID: 3869
			private readonly QueryResultOptions m_options;

			// Token: 0x04000F1E RID: 3870
			private readonly RowProcessor<T> m_processor;
		}
	}
}
