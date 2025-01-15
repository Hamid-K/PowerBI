using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000093 RID: 147
	public sealed class ThreadJobContext : IDisposable
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0000DF90 File Offset: 0x0000C190
		public ThreadJobContext(RunningJobContext parentJobCtx)
		{
			this.m_prevCtx = ProcessingContext.ThreadContext;
			ProcessingContext.ThreadContext = this;
			this.m_parentJobCtx = parentJobCtx;
			this.m_thread = Thread.CurrentThread;
			this.m_state = 0;
			this.m_threadId = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		public int ThreadId
		{
			get
			{
				return this.m_threadId;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000E000 File Offset: 0x0000C200
		public RunningJobContext RunningJobContext
		{
			get
			{
				return this.m_parentJobCtx;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E008 File Offset: 0x0000C208
		public IAbortHelper GetAbortHelper()
		{
			if (this.m_disposed)
			{
				return null;
			}
			return this.m_abortHelper;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E01C File Offset: 0x0000C21C
		public void AddAbortHelper(IAbortHelper abortHelper)
		{
			if (this.m_disposed)
			{
				return;
			}
			object abortHelperSync = this.m_abortHelperSync;
			lock (abortHelperSync)
			{
				if (this.m_abortHelper != null)
				{
					throw new InternalCatalogException("Abort helper added twice");
				}
				this.m_abortHelper = abortHelper;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000E07C File Offset: 0x0000C27C
		public void RemoveAbortHelper()
		{
			if (this.m_disposed)
			{
				return;
			}
			object abortHelperSync = this.m_abortHelperSync;
			lock (abortHelperSync)
			{
				if (this.m_abortHelper == null)
				{
					throw new InternalCatalogException("No abort helper to remove");
				}
				this.m_abortHelper = null;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		public void AddCommand(global::System.Data.IDbCommand cmd)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_commands.Add(cmd);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		public void AddCommand(Microsoft.ReportingServices.DataProcessing.IDbCommand cmd)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_commands.Add(cmd);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000E10C File Offset: 0x0000C30C
		public void RemoveCommand(global::System.Data.IDbCommand cmd)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_commands.Remove(cmd);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000E123 File Offset: 0x0000C323
		public void RemoveCommand(Microsoft.ReportingServices.DataProcessing.IDbCommand cmd)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_commands.Remove(cmd);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000E13A File Offset: 0x0000C33A
		public void Dispose()
		{
			if (this.m_disposed)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "ThreadJobContext.Dispose object is already disposed");
				return;
			}
			ProcessingContext.ThreadContext = this.m_prevCtx;
			this.m_disposed = true;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000E167 File Offset: 0x0000C367
		internal void BeginCancelableState()
		{
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.BeginCancelableState");
			this.m_state = 1;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000E180 File Offset: 0x0000C380
		internal void EndCancelableState()
		{
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.EndCancelableState");
			Interlocked.CompareExchange(ref this.m_state, 0, 1);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		internal void WaitForCancelException()
		{
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.WaitForCancelException entered");
			int num = 0;
			while (this.m_state == 2)
			{
				num++;
				Thread.Sleep(100);
				if (num > 100000)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "Wait for cancel lock still on, breaking...");
					break;
				}
			}
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.WaitForCancelException finished");
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E200 File Offset: 0x0000C400
		internal bool Cancel(ReportServerAbortInfo.AbortReason reason)
		{
			if (this.m_disposed)
			{
				if (ProcessingContext.JobsTracer.TraceVerbose)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel operation ignored because job was disposed");
				}
				return true;
			}
			bool flag;
			try
			{
				if (this.m_state == 1)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel acquiring lock on command list");
					object obj = this.m_commands.SyncRoot;
					lock (obj)
					{
						ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel lock acquired on command list");
						if (this.m_commands.Count > 0)
						{
							for (int i = 0; i < this.m_commands.Count; i++)
							{
								global::System.Data.IDbCommand dbCommand = this.m_commands[i] as global::System.Data.IDbCommand;
								if (dbCommand != null)
								{
									dbCommand.Cancel();
									ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel Sql: System.Data.IDBCommand was cancelled");
								}
								else
								{
									(this.m_commands[i] as Microsoft.ReportingServices.DataProcessing.IDbCommand).Cancel();
									ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel Sql: Microsoft.ReportingServices.DataProcessing.IDbCommand was cancelled");
								}
							}
						}
					}
					bool flag2 = false;
					obj = this.m_abortHelperSync;
					lock (obj)
					{
						if (this.m_abortHelper != null)
						{
							ProcessingStatus processingStatus = ProcessingStatus.AbnormalTermination;
							switch (reason)
							{
							case ReportServerAbortInfo.AbortReason.TimeoutExpired:
								processingStatus = ProcessingStatus.TimeoutExpired;
								break;
							case ReportServerAbortInfo.AbortReason.JobCanceled:
								processingStatus = ProcessingStatus.CanceledByUser;
								break;
							case ReportServerAbortInfo.AbortReason.JobOrphaned:
								processingStatus = ProcessingStatus.CanceledByUser;
								break;
							}
							ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel: Cancelling via abort helper");
							flag2 = this.m_abortHelper.Abort(processingStatus);
						}
					}
					if (!flag2)
					{
						ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel waiting");
						if (Interlocked.CompareExchange(ref this.m_state, 2, 1) == 1)
						{
							try
							{
								ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel: Calling Abort() on thread");
								this.m_thread.Abort(new ReportServerAbortInfo(reason));
							}
							catch (ThreadStateException)
							{
								ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "ThreadJobContext.Cancel failed - resetting state");
								if (Interlocked.CompareExchange(ref this.m_state, 1, 2) != 2)
								{
									throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "Unexpected cancel state: {0} - expected cancel in progress", this.m_state));
								}
								return false;
							}
							ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel succeeded - abort raised");
						}
						else
						{
							ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "ThreadJobContext.Cancel: Thread is no longer is cancelable state, Thread state = {0}", new object[] { this.m_state });
						}
					}
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel lock released on command list");
					flag = true;
				}
				else
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "ThreadJobContext.Cancel thread was not in cancelable period");
					flag = false;
				}
			}
			finally
			{
				this.m_parentJobCtx.RemoveWorkThread(this);
			}
			return flag;
		}

		// Token: 0x040002AB RID: 683
		private Thread m_thread;

		// Token: 0x040002AC RID: 684
		private int m_state;

		// Token: 0x040002AD RID: 685
		private ArrayList m_commands = ArrayList.Synchronized(new ArrayList());

		// Token: 0x040002AE RID: 686
		private ThreadJobContext m_prevCtx;

		// Token: 0x040002AF RID: 687
		private bool m_disposed;

		// Token: 0x040002B0 RID: 688
		private int m_threadId;

		// Token: 0x040002B1 RID: 689
		private IAbortHelper m_abortHelper;

		// Token: 0x040002B2 RID: 690
		private object m_abortHelperSync = new object();

		// Token: 0x040002B3 RID: 691
		private RunningJobContext m_parentJobCtx;

		// Token: 0x020000F9 RID: 249
		private enum ThreadState
		{
			// Token: 0x040004D6 RID: 1238
			NonCancelable,
			// Token: 0x040004D7 RID: 1239
			Cancelable,
			// Token: 0x040004D8 RID: 1240
			CancelInProgress
		}
	}
}
