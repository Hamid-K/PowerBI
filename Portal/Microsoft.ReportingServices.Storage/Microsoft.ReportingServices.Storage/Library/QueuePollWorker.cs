using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000008 RID: 8
	internal abstract class QueuePollWorker : PollWorker, IDisposable
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003450 File Offset: 0x00001650
		~QueuePollWorker()
		{
			this.Dispose(false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003480 File Offset: 0x00001680
		public QueuePollWorker()
		{
			int pollingInterval = Globals.Configuration.PollingInterval;
			this.m_pollInterval = new TimeSpan(0, 0, pollingInterval);
			this.m_maxItems = Globals.Configuration.MaxQueueThreads;
			if (this.m_maxItems == 0)
			{
				this.m_maxItems = 2 * ProcessorUtilities.ProcessorsInUse;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003541 File Offset: 0x00001741
		protected TimeSpan HeartbeatInterval
		{
			get
			{
				return this.m_heartbeatInterval;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003549 File Offset: 0x00001749
		public void ItemPlacedInQueue()
		{
			this.m_knownQueueItemInDatabase = true;
			DBPoll.SetWaitEvent();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003558 File Offset: 0x00001758
		public override void ProcessData(IDataReader reader)
		{
			int num = 0;
			while (reader.Read())
			{
				num++;
				QueueItem nextQueueItem = this.GetNextQueueItem(reader);
				nextQueueItem.TimeEntered = DateTime.Now;
				this.AddCurrentItem(nextQueueItem);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkItemStart), nextQueueItem);
			}
			if (num > 0)
			{
				this.m_workLastPoll = true;
				if (this.m_tracer.TraceVerbose)
				{
					this.m_tracer.Trace("{0} processing {1} more items. {2} Total items in internal queue.", new object[]
					{
						this.PollingTraceName,
						num,
						this.m_currentItems.Count
					});
				}
			}
			else
			{
				this.m_lastEmptyPoll = DateTime.Now;
				this.m_workLastPoll = false;
			}
			this.m_lastPoll = DateTime.Now;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003614 File Offset: 0x00001814
		internal void AddCurrentItem(QueueItem item)
		{
			Hashtable currentItems = this.m_currentItems;
			lock (currentItems)
			{
				this.m_currentItems.Add(item.ID, item);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003668 File Offset: 0x00001868
		internal void RemoveItem(QueueItem item)
		{
			Hashtable currentItems = this.m_currentItems;
			lock (currentItems)
			{
				this.m_currentItems.Remove(item.ID);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73
		protected abstract string QueueTableName { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004A RID: 74
		protected abstract string QueueIDColumnName { get; }

		// Token: 0x0600004B RID: 75
		protected abstract void CleanInactiveRows(int minutes);

		// Token: 0x0600004C RID: 76
		public abstract QueueItem GetNextQueueItem(IDataRecord record);

		// Token: 0x0600004D RID: 77
		public abstract bool QueueWorker(QueueItem item);

		// Token: 0x0600004E RID: 78
		public abstract void DeleteQueueItem(QueueItem item);

		// Token: 0x0600004F RID: 79 RVA: 0x000036B8 File Offset: 0x000018B8
		protected void SetHeartbeat()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DateTime now = DateTime.Now;
			Hashtable currentItems = this.m_currentItems;
			lock (currentItems)
			{
				foreach (object obj in this.m_currentItems.Values)
				{
					QueueItem queueItem = (QueueItem)obj;
					if (now - queueItem.TimeEntered > this.HeartbeatInterval)
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append('\'');
						stringBuilder.Append(queueItem.ID.ToString());
						stringBuilder.Append('\'');
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "\r\n                                                    Update \r\n                                                        [{0}] \r\n                                                    set \r\n                                                        [ProcessHeartbeat] = GETUTCDATE()\r\n                                                    where \r\n                                                        [{1}] in ({2})", this.QueueTableName, this.QueueIDColumnName, stringBuilder.ToString());
				this.IssueTextQuey(text);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000037DC File Offset: 0x000019DC
		private void ResetItemRows()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DateTime now = DateTime.Now;
			Hashtable currentItems = this.m_currentItems;
			lock (currentItems)
			{
				foreach (object obj in this.m_currentItems.Values)
				{
					QueueItem queueItem = (QueueItem)obj;
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append('\'');
					stringBuilder.Append(queueItem.ID.ToString());
					stringBuilder.Append('\'');
				}
			}
			if (stringBuilder.Length > 0)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "\r\n                                                    Update \r\n                                                        [{0}] \r\n                                                    set \r\n                                                        [ProcessStart] = NULL,\r\n                                                        [ProcessHeartbeat] = NULL\r\n                                                    where \r\n                                                        [{1}] in ({2})", this.QueueTableName, this.QueueIDColumnName, stringBuilder.ToString());
				this.IssueTextQuey(text);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038E4 File Offset: 0x00001AE4
		private void ResetSingleRow(QueueItem item)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "\r\n                                                    Update \r\n                                                        [{0}] \r\n                                                    set \r\n                                                        [ProcessStart] = NULL,\r\n                                                        [ProcessHeartbeat] = NULL\r\n                                                    where \r\n                                                        [{1}] in ({2})", this.QueueTableName, this.QueueIDColumnName, "'" + item.ID.ToString() + "'");
			this.IssueTextQuey(text);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003938 File Offset: 0x00001B38
		private void IssueTextQuey(string query)
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			if (!string.IsNullOrEmpty(this.PollCatalog))
			{
				connectionManager.ChangeDatabase(this.PollCatalog);
			}
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(query, connectionManager.Connection)))
				{
					instrumentedSqlCommand.CommandType = CommandType.Text;
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039CC File Offset: 0x00001BCC
		private void WorkItemStart(object state)
		{
			try
			{
				DBPoll.PollItemStart();
				bool flag = false;
				QueueItem queueItem = (QueueItem)state;
				try
				{
					if (this.m_tracer.TraceVerbose)
					{
						this.m_tracer.Trace("{0} processing item {1}", new object[] { this.PollingTraceName, queueItem.ID });
					}
					if (base.ContinueWorking)
					{
						flag = this.QueueWorker(queueItem);
					}
				}
				catch (Exception ex)
				{
					flag = true;
					if (RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Queue item processing had unhandled exception: {0}", new object[] { ex.ToString() });
					}
					if (!(ex is RSException))
					{
						new InternalCatalogException(ex, "Queue item processing had unhandled non-RS exception");
					}
				}
				finally
				{
					this.RemoveItem(queueItem);
					if (flag)
					{
						this.DeleteQueueItem(queueItem);
						if (this.m_tracer.TraceVerbose)
						{
							this.m_tracer.Trace("{0} finished processing item {1}", new object[] { this.PollingTraceName, queueItem.ID });
						}
					}
					else
					{
						this.ResetSingleRow(queueItem);
						if (this.m_tracer.TraceVerbose)
						{
							this.m_tracer.Trace("{0} no longer processing item {1}, will be requeued", new object[] { this.PollingTraceName, queueItem.ID });
						}
					}
					if (this.ThreadPressureRelaxed())
					{
						DBPoll.SetWaitEvent();
					}
				}
			}
			catch (Exception ex2)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Queue worker thread caught unhandled exception: {0}", new object[] { ex2.ToString() });
				}
			}
			finally
			{
				DBPoll.PollItemEnd();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003BBC File Offset: 0x00001DBC
		protected int NumberOfItemsToRetrieve
		{
			get
			{
				int num;
				if (this.m_currentItems.Count == 0)
				{
					num = ProcessorUtilities.ProcessorsInUse;
				}
				else
				{
					double processCPUPercentage = ProcessorUtilities.ProcessCPUPercentage;
					if (processCPUPercentage > 0.75)
					{
						num = (int)((double)ProcessorUtilities.ProcessorsInUse * 0.25);
					}
					else if (processCPUPercentage > 0.5)
					{
						num = (int)((double)ProcessorUtilities.ProcessorsInUse * 0.5);
					}
					else
					{
						num = ProcessorUtilities.ProcessorsInUse;
					}
				}
				if (num + this.m_currentItems.Count > this.m_maxItems)
				{
					num = this.m_maxItems - this.m_currentItems.Count;
				}
				if (num <= 0)
				{
					num = 1;
				}
				return num;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003C5C File Offset: 0x00001E5C
		public override bool Poll
		{
			get
			{
				if (!this.m_workLastPoll && !this.m_knownQueueItemInDatabase)
				{
					return this.m_lastEmptyPoll == DateTime.MinValue || this.m_lastEmptyPoll > DateTime.Now || DateTime.Now - this.m_lastEmptyPoll >= this.m_pollInterval;
				}
				bool flag = true;
				if (this.m_currentItems.Count != 0)
				{
					if (this.m_currentItems.Count >= this.m_maxItems)
					{
						flag = false;
						if (DateTime.Now - this.m_lastPoll >= this.m_fullQueueStateInterval && DateTime.Now - QueuePollWorker.m_lastFullQueueWarning > this.m_fullQueueTraceInterval)
						{
							StringBuilder stringBuilder = new StringBuilder();
							int num = 0;
							foreach (object obj in this.m_currentItems.Values)
							{
								QueueItem queueItem = (QueueItem)obj;
								stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\r\n\t#{0}, {1}", num++, queueItem.ToString());
							}
							if (RSTrace.DbPollingTracer.TraceWarning)
							{
								RSTrace.DbPollingTracer.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "Schedules, events, and subscriptions processing may be delayed because processing queue reached the maximum item count = {0}. Items currently being processed: {1}.", this.m_maxItems, stringBuilder));
							}
							RSEventLog.Current.WriteWarning(Event.PollQueueFull, new object[] { this.m_maxItems, stringBuilder });
							QueuePollWorker.m_lastFullQueueWarning = DateTime.Now;
						}
					}
					else
					{
						flag = (!(DateTime.Now > this.m_lastPoll) || !(DateTime.Now - this.m_lastPoll < this.m_successInterval)) && !this.ProcessPressureReached();
					}
				}
				this.m_knownQueueItemInDatabase = false;
				return flag;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003E50 File Offset: 0x00002050
		public override bool IsStillWorking
		{
			get
			{
				int count = this.m_currentItems.Count;
				if (count > 0 && RSTrace.DbPollingTracer.TraceVerbose)
				{
					RSTrace.DbPollingTracer.Trace(TraceLevel.Verbose, "{0} still working with {1} items", new object[] { this.PollingTraceName, count });
				}
				return count > 0;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003EA8 File Offset: 0x000020A8
		private bool ProcessPressureReached()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			ThreadPool.GetAvailableThreads(out num, out num2);
			ThreadPool.GetMaxThreads(out num3, out num4);
			int num5 = num3 - num;
			if ((double)num5 > (double)num3 * 0.75)
			{
				if (RSTrace.DbPollingTracer.TraceVerbose)
				{
					RSTrace.DbPollingTracer.Trace(TraceLevel.Verbose, "Paused polling, thread pressure reached: " + num5.ToString(CultureInfo.InvariantCulture));
				}
				return true;
			}
			double processCPUPercentage = ProcessorUtilities.ProcessCPUPercentage;
			if (RSTrace.DbPollingTracer.TraceVerbose)
			{
				RSTrace.DbPollingTracer.Trace(TraceLevel.Verbose, "CPU percentage = {0}", new object[] { processCPUPercentage });
			}
			if (processCPUPercentage > 0.5)
			{
				if (RSTrace.DbPollingTracer.TraceVerbose)
				{
					RSTrace.DbPollingTracer.Trace(TraceLevel.Verbose, "Paused polling, CPU pressure reached: " + processCPUPercentage.ToString(CultureInfo.InvariantCulture));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003F84 File Offset: 0x00002184
		private bool ThreadPressureRelaxed()
		{
			return this.m_currentItems.Count == 0;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F98 File Offset: 0x00002198
		protected void ExecuteStoredProcedure(string spName, string spParameterName, object spParameterValue)
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = PollWorker.NewStandardSqlCommand(spName, connectionManager))
				{
					instrumentedSqlCommand.Parameters.AddWithValue(spParameterName, spParameterValue);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004004 File Offset: 0x00002204
		public override void Start()
		{
			base.Start();
			if (this.m_heartbeatTimer != null)
			{
				return;
			}
			Hashtable currentItems = this.m_currentItems;
			lock (currentItems)
			{
				this.m_currentItems.Clear();
			}
			this.m_heartbeatTimer = this.CreateHeartbeatTimer();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004064 File Offset: 0x00002264
		private void HeartbeatTimerAction()
		{
			if (!base.ContinueWorking)
			{
				return;
			}
			try
			{
				this.SetHeartbeat();
				if (this.m_heartbeatIterations % 3 == 0)
				{
					this.CleanInactiveRows(this.HeartbeatInterval.Minutes * 3);
					this.m_heartbeatIterations = 0;
				}
				this.m_heartbeatIterations++;
			}
			catch (Exception ex)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Exception in HeartbeatTimerAction({0}): {1}.", new object[]
					{
						this.PollingTraceName,
						ex.ToString()
					});
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004104 File Offset: 0x00002304
		public override void Stop(Globals.ServiceStopMode mode)
		{
			base.Stop(mode);
			try
			{
				if (mode == Globals.ServiceStopMode.FullStop)
				{
					this.ResetItemRows();
					this.Dispose(true);
				}
			}
			catch (Exception ex)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Error stopping Queue worker {0}: {1}", new object[] { this.PollingTraceName, ex.Message });
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004174 File Offset: 0x00002374
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000417D File Offset: 0x0000237D
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this.m_heartbeatTimer != null)
				{
					this.m_heartbeatTimer.Stop();
					this.m_heartbeatTimer = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000041B4 File Offset: 0x000023B4
		internal TimerActionBase CreateHeartbeatTimer()
		{
			TimerActionBase timerActionBase = null;
			try
			{
				timerActionBase = new QueuePollWorker.ActionTimer(delegate
				{
					this.HeartbeatTimerAction();
				});
				timerActionBase.Start(0, Convert.ToInt32(this.HeartbeatInterval.TotalSeconds), this.PollingTraceName + " HeartbeatTimer");
			}
			catch (Exception ex)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Error starting {0} heartbeat timer: {1}", new object[]
					{
						this.PollingTraceName,
						ex.ToString()
					});
				}
			}
			return timerActionBase;
		}

		// Token: 0x04000065 RID: 101
		private bool m_workLastPoll;

		// Token: 0x04000066 RID: 102
		private Hashtable m_currentItems = new Hashtable();

		// Token: 0x04000067 RID: 103
		private DateTime m_lastEmptyPoll = DateTime.MinValue;

		// Token: 0x04000068 RID: 104
		private DateTime m_lastPoll = DateTime.MinValue;

		// Token: 0x04000069 RID: 105
		private readonly TimeSpan m_pollInterval = TimeSpan.MinValue;

		// Token: 0x0400006A RID: 106
		private RSTrace m_tracer = RSTrace.DbPollingTracer;

		// Token: 0x0400006B RID: 107
		private readonly TimeSpan m_heartbeatInterval = new TimeSpan(0, 5, 0);

		// Token: 0x0400006C RID: 108
		private readonly TimeSpan m_successInterval = new TimeSpan(0, 0, 3);

		// Token: 0x0400006D RID: 109
		private bool m_disposed;

		// Token: 0x0400006E RID: 110
		private bool m_knownQueueItemInDatabase;

		// Token: 0x0400006F RID: 111
		private readonly TimeSpan m_fullQueueStateInterval = new TimeSpan(0, 30, 0);

		// Token: 0x04000070 RID: 112
		private readonly TimeSpan m_fullQueueTraceInterval = new TimeSpan(0, 1, 0);

		// Token: 0x04000071 RID: 113
		private static DateTime m_lastFullQueueWarning = DateTime.MinValue;

		// Token: 0x04000072 RID: 114
		private readonly int m_maxItems;

		// Token: 0x04000073 RID: 115
		private int m_heartbeatIterations;

		// Token: 0x04000074 RID: 116
		private TimerActionBase m_heartbeatTimer;

		// Token: 0x04000075 RID: 117
		private const string _SetHeartbeatFormat = "\r\n                                                    Update \r\n                                                        [{0}] \r\n                                                    set \r\n                                                        [ProcessHeartbeat] = GETUTCDATE()\r\n                                                    where \r\n                                                        [{1}] in ({2})";

		// Token: 0x04000076 RID: 118
		private const string _ResetItemRowsFormat = "\r\n                                                    Update \r\n                                                        [{0}] \r\n                                                    set \r\n                                                        [ProcessStart] = NULL,\r\n                                                        [ProcessHeartbeat] = NULL\r\n                                                    where \r\n                                                        [{1}] in ({2})";

		// Token: 0x02000048 RID: 72
		internal class ActionTimer : TimerActionBase
		{
			// Token: 0x0600027D RID: 637 RVA: 0x0000AE7F File Offset: 0x0000907F
			public ActionTimer(Action action)
			{
				this.m_action = action;
			}

			// Token: 0x0600027E RID: 638 RVA: 0x0000AE9C File Offset: 0x0000909C
			public override void DoTimerAction()
			{
				object syncObject = this.m_syncObject;
				lock (syncObject)
				{
					this.m_action();
				}
			}

			// Token: 0x040001DC RID: 476
			private Action m_action;

			// Token: 0x040001DD RID: 477
			private object m_syncObject = new object();
		}
	}
}
