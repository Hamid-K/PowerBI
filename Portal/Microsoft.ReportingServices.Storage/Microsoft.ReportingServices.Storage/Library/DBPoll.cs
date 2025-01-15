using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000006 RID: 6
	internal sealed class DBPoll
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002AF4 File Offset: 0x00000CF4
		internal static IServiceInstanceContext ServiceInstanceContext
		{
			get
			{
				return DBPoll.m_serviceInstanceContext;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002AFB File Offset: 0x00000CFB
		internal static int MaxInactivity
		{
			get
			{
				return Globals.Configuration.DBQueryTimeout + 30;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B0C File Offset: 0x00000D0C
		public DBPoll()
		{
			this.m_pollTimeStamp = DateTime.Now;
			this.m_maxNoPollActivity = new TimeSpan(0, 0, DBPoll.MaxInactivity);
			if (DBPoll.m_serviceInstanceContext == null)
			{
				DBPoll.m_serviceInstanceContext = ProcessingContext.ServiceInstanceContext;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B9C File Offset: 0x00000D9C
		internal void StartPollWorkers()
		{
			object syncRoot = this.m_workers.SyncRoot;
			lock (syncRoot)
			{
				if (this.m_workers.Count != 0)
				{
					foreach (object obj in this.m_workers)
					{
						((PollWorker)obj).Start();
					}
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C30 File Offset: 0x00000E30
		public void StartPolling()
		{
			if (this.m_pollingThread != null)
			{
				return;
			}
			this.StartPollWorkers();
			this.m_pollingThread = new Thread(new ThreadStart(this.PollingMaintenance));
			this.m_continuePolling = true;
			this.m_pollingThread.Start();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C6A File Offset: 0x00000E6A
		public void StopPolling(Globals.ServiceStopMode mode)
		{
			new Thread(new ParameterizedThreadStart(this.StopPollingThread)).Start(mode);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C88 File Offset: 0x00000E88
		internal void StopPollWorkers(Globals.ServiceStopMode mode)
		{
			object syncRoot = this.m_workers.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_workers)
				{
					((PollWorker)obj).Stop(mode);
				}
			}
			this.ClearWorkers();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002D14 File Offset: 0x00000F14
		private void StopPollingThread(object param)
		{
			Globals.ServiceStopMode serviceStopMode = (Globals.ServiceStopMode)param;
			this.m_continuePolling = false;
			DBPoll.SetWaitEvent();
			if (this.m_pollingThread != null)
			{
				try
				{
					if (!this.m_pollingThread.Join(5000))
					{
						RSTrace.DbPollingTracer.Trace(TraceLevel.Info, "[ThreadAborted] Aborting the polling thread after a 5 second wait.");
						this.m_pollingThread.Abort();
					}
				}
				catch (ThreadStateException)
				{
				}
				this.m_pollingThread = null;
			}
			this.StopPollWorkers(serviceStopMode);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002D8C File Offset: 0x00000F8C
		internal static void PollItemStart()
		{
			ProcessingContext.InitializeRequestContext(new BackgroundRequestContext(DBPoll.m_serviceInstanceContext));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D9D File Offset: 0x00000F9D
		internal static void PollItemEnd()
		{
			ProcessingContext.EndRequestContext();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private void PollingMaintenance()
		{
			foreach (object obj in this.PollingMaintenanceLoop())
			{
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002DF4 File Offset: 0x00000FF4
		internal IEnumerable PollingMaintenanceLoop()
		{
			Exception lastException = null;
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace(TraceLevel.Info, "PollingMaintenance: Polling started");
			}
			int timesToTrace = 0;
			try
			{
				do
				{
					try
					{
						DBPoll.m_resetEvent.Reset();
						this.m_pollTimeStamp = DateTime.Now;
						if (ResourceUtilities.TooMuchMemory)
						{
							if (timesToTrace == 0)
							{
								RSTrace.DbPollingTracer.Trace(TraceLevel.Info, "PollingMaintenance: Skipping work item as memory consumption exceeded the limit");
							}
							int num = timesToTrace;
							timesToTrace = num + 1;
							if (timesToTrace > 30)
							{
								timesToTrace = 0;
							}
						}
						else if (this.PollingFunction(ref lastException))
						{
							if (this.m_tracer.BufferOutput && this.m_tracer.TraceInfo)
							{
								this.m_tracer.Trace(TraceLevel.Info, "PollingMaintenance: Polling cycle completed.");
							}
							this.m_tracer.BufferOutput = false;
							this.m_tracer.WriteBuffer();
							lastException = null;
						}
					}
					catch (Exception ex)
					{
						if (!this.HandleException("PollingMaintenance", ex, lastException))
						{
							yield break;
						}
						lastException = ex;
					}
					catch
					{
						if (this.m_tracer.TraceError)
						{
							this.m_tracer.Trace(TraceLevel.Error, "PollingMaintenance: Polling exiting from a severe error");
							this.m_tracer.Trace(TraceLevel.Error, "PollingMaintenance: Stopping Service because of severe error.");
						}
						this.ForceFullServiceStop();
						break;
					}
					yield return null;
					DBPoll.m_resetEvent.WaitOne(this.m_pollInterval, false);
				}
				while (this.m_continuePolling);
			}
			finally
			{
				this.m_tracer.BufferOutput = false;
				this.m_tracer.WriteBuffer();
			}
			yield break;
			yield break;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E04 File Offset: 0x00001004
		private void ForceFullServiceStop()
		{
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E10 File Offset: 0x00001010
		internal bool HandleException(string methodName, Exception e, Exception lastException)
		{
			if (ExceptionUtils.IsStoppingException(e))
			{
				if (e is ThreadAbortException)
				{
					if (this.m_tracer.TraceInfo)
					{
						this.m_tracer.Trace(TraceLevel.Info, "{0}: Exiting for the following exception: {1}", new object[] { methodName, e.Message });
					}
				}
				else if (this.m_tracer.TraceError && this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "{0}: Exiting for the following exception: {1}", new object[]
					{
						methodName,
						e.ToString()
					});
				}
				return false;
			}
			if (lastException == null || e.GetType() != lastException.GetType())
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "{0}: {1}.", new object[]
					{
						methodName,
						e.ToString()
					});
				}
				this.m_tracer.BufferOutput = true;
				this.m_tracer.WriteBuffer();
			}
			else
			{
				this.m_tracer.ClearBuffer();
			}
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F0C File Offset: 0x0000110C
		internal void ReleasePollingMaintenance()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002F0E File Offset: 0x0000110E
		public static void SetWaitEvent()
		{
			DBPoll.m_resetEvent.Set();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F1C File Offset: 0x0000111C
		private bool PollingFunction(ref Exception lastException)
		{
			bool flag = true;
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			bool flag2;
			try
			{
				object syncRoot = this.m_workers.SyncRoot;
				lock (syncRoot)
				{
					foreach (object obj in this.m_workers)
					{
						PollWorker pollWorker = (PollWorker)obj;
						if (!this.m_continuePolling)
						{
							break;
						}
						if (pollWorker.Poll)
						{
							connectionManager.BeginTransaction();
							try
							{
								using (InstrumentedSqlCommand pollCommand = pollWorker.PollCommand)
								{
									pollCommand.Connection = connectionManager.Connection;
									pollCommand.Transaction = connectionManager.Transaction;
									using (IDataReader dataReader = pollCommand.ExecuteReader())
									{
										pollWorker.ProcessData(dataReader);
									}
									connectionManager.CommitTransaction();
								}
							}
							catch (Exception ex)
							{
								connectionManager.AbortTransaction();
								if (!this.HandleException("PollingFunction", ex, lastException))
								{
									throw;
								}
								lastException = ex;
								flag = false;
							}
							catch
							{
								connectionManager.AbortTransaction();
								throw;
							}
						}
					}
				}
				flag2 = flag;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return flag2;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000030FC File Offset: 0x000012FC
		public void AddWorker(PollWorker worker)
		{
			object syncRoot = this.m_workers.SyncRoot;
			lock (syncRoot)
			{
				this.m_workers.Add(worker);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003148 File Offset: 0x00001348
		private void ClearWorkers()
		{
			object syncRoot = this.m_workers.SyncRoot;
			lock (syncRoot)
			{
				this.m_workers.Clear();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003194 File Offset: 0x00001394
		public bool IsPollingWorking
		{
			get
			{
				DateTime lastCheckTimeStamp = this.m_lastCheckTimeStamp;
				this.m_lastCheckTimeStamp = DateTime.Now;
				if (this.m_workers.Count == 0)
				{
					return true;
				}
				TimeSpan timeSpan = DateTime.Now - this.m_pollTimeStamp;
				if (!(timeSpan > this.m_maxNoPollActivity))
				{
					return true;
				}
				if (Globals.Configuration.ProcessTimeoutGcExtension != 0 && timeSpan <= this.m_maxNoPollActivity + new TimeSpan(0, 0, Globals.Configuration.ProcessTimeoutGcExtension))
				{
					return true;
				}
				if (lastCheckTimeStamp != DateTime.MinValue && DateTime.Now - lastCheckTimeStamp > this.m_maxNoPollActivity)
				{
					if (RSTrace.DbPollingTracer.TraceInfo)
					{
						RSTrace.DbPollingTracer.Trace(TraceLevel.Info, "Skipping unreliable IsPollingWorking check.");
					}
					this.m_pollTimeStamp = DateTime.Now;
					return true;
				}
				return false;
			}
		}

		// Token: 0x04000059 RID: 89
		private Thread m_pollingThread;

		// Token: 0x0400005A RID: 90
		private bool m_continuePolling = true;

		// Token: 0x0400005B RID: 91
		private RSTrace m_tracer = RSTrace.DbPollingTracer;

		// Token: 0x0400005C RID: 92
		private ArrayList m_workers = new ArrayList();

		// Token: 0x0400005D RID: 93
		private static AutoResetEvent m_resetEvent = new AutoResetEvent(false);

		// Token: 0x0400005E RID: 94
		private readonly TimeSpan m_pollInterval = new TimeSpan(0, 0, 1);

		// Token: 0x0400005F RID: 95
		private readonly TimeSpan m_maxNoPollActivity = TimeSpan.Zero;

		// Token: 0x04000060 RID: 96
		private DateTime m_pollTimeStamp = DateTime.MinValue;

		// Token: 0x04000061 RID: 97
		private DateTime m_lastCheckTimeStamp = DateTime.MinValue;

		// Token: 0x04000062 RID: 98
		private static IServiceInstanceContext m_serviceInstanceContext = null;
	}
}
