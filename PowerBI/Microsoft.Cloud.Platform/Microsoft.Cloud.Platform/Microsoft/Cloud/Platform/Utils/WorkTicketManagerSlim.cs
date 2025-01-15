using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F5 RID: 757
	internal class WorkTicketManagerSlim : IWorkTicketFactory, IIdentifiable, IShuttable
	{
		// Token: 0x06001403 RID: 5123 RVA: 0x000455C4 File Offset: 0x000437C4
		public WorkTicketManagerSlim(string name)
		{
			this.m_name = ((name != null) ? name : string.Empty);
			this.m_stopped = 0;
			this.m_numPendingWorkTickets = 0L;
			this.m_workTicket = new WorkTicketManagerSlim.WorkTicketSlim(this);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x000455F8 File Offset: 0x000437F8
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00045600 File Offset: 0x00043800
		[CanBeNull]
		public WorkTicket TryCreateWorkTicket(IIdentifiable owningEntity)
		{
			if (Interlocked.Increment(ref this.m_numPendingWorkTickets) < 0L)
			{
				Interlocked.Decrement(ref this.m_numPendingWorkTickets);
				return null;
			}
			return this.m_workTicket;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00045625 File Offset: 0x00043825
		public WorkTicket CreateWorkTicket(IIdentifiable owningEntity)
		{
			WorkTicket workTicket = this.TryCreateWorkTicket(owningEntity);
			if (workTicket == null)
			{
				throw new ShutdownSequenceStartedException();
			}
			return workTicket;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00045637 File Offset: 0x00043837
		public WorkTicket CreateWorkTicket(IIdentifiable owningEntity, WorkTicket ticket)
		{
			throw new NotSupportedException("WorkTicketManagerSlim of '" + this.m_name + "' does not support creating a work ticket with an existing item");
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00045637 File Offset: 0x00043837
		public bool TryCreateWorkTicket(IIdentifiable owningEntity, WorkTicket ticket)
		{
			throw new NotSupportedException("WorkTicketManagerSlim of '" + this.m_name + "' does not support creating a work ticket with an existing item");
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00045654 File Offset: 0x00043854
		public void Stop()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManagerSlim.Stop called for {0}", new object[] { this.Name });
			if (Interlocked.Exchange(ref this.m_stopped, 1) == 0)
			{
				Interlocked.Add(ref this.m_numPendingWorkTickets, -2147483648L);
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000456A0 File Offset: 0x000438A0
		public void WaitForStopToComplete()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManagerSlim.WaitForStopToComplete called for {0}", new object[] { this.Name });
			bool flag = false;
			while (Interlocked.Read(ref this.m_numPendingWorkTickets) != -2147483648L)
			{
				if (flag)
				{
					Thread.Sleep(5);
				}
				else
				{
					flag = true;
					Thread.SpinWait(50);
				}
			}
			Interlocked.Add(ref this.m_numPendingWorkTickets, -19327352832L);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0004570C File Offset: 0x0004390C
		public void Shutdown()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManagerSlim.Shutdown called for {0}", new object[] { this.Name });
			Interlocked.Add(ref this.m_numPendingWorkTickets, -193273528320L);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00045742 File Offset: 0x00043942
		private void OnWorkTicketDisposed()
		{
			Interlocked.Decrement(ref this.m_numPendingWorkTickets);
		}

		// Token: 0x04000795 RID: 1941
		private string m_name;

		// Token: 0x04000796 RID: 1942
		private int m_stopped;

		// Token: 0x04000797 RID: 1943
		private const int NotStopped = 0;

		// Token: 0x04000798 RID: 1944
		private const int YesStopped = 1;

		// Token: 0x04000799 RID: 1945
		private long m_numPendingWorkTickets;

		// Token: 0x0400079A RID: 1946
		private const long Stopping = -2147483648L;

		// Token: 0x0400079B RID: 1947
		private const long Stopped = -21474836480L;

		// Token: 0x0400079C RID: 1948
		private const long ShuttedDown = -214748364800L;

		// Token: 0x0400079D RID: 1949
		private const long MaxConcurrentTickets = 1073741823L;

		// Token: 0x0400079E RID: 1950
		private WorkTicketManagerSlim.WorkTicketSlim m_workTicket;

		// Token: 0x0200078D RID: 1933
		private class WorkTicketSlim : WorkTicket
		{
			// Token: 0x060030B8 RID: 12472 RVA: 0x000A6ADE File Offset: 0x000A4CDE
			public WorkTicketSlim(WorkTicketManagerSlim manager)
			{
				this.m_manager = manager;
			}

			// Token: 0x060030B9 RID: 12473 RVA: 0x000A6AED File Offset: 0x000A4CED
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.m_manager.OnWorkTicketDisposed();
				}
			}

			// Token: 0x04001647 RID: 5703
			private WorkTicketManagerSlim m_manager;
		}
	}
}
