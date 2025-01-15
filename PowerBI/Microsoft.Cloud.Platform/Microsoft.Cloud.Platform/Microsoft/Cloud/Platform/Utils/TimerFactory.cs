using System;
using System.Linq;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D1 RID: 721
	public class TimerFactory : IIdentifiable, IShuttable
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x00042C58 File Offset: 0x00040E58
		public TimerFactory(string identity, TimerCreationFlags timerCreationFlags)
		{
			this.m_workTicketFactory = new WorkTicketManager(identity);
			this.m_workTicketFactory.TrackTickets = true;
			this.m_stopLock = new WorkTicketManagerSlim(identity);
			this.m_timerCreationFlags = timerCreationFlags;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00042C8C File Offset: 0x00040E8C
		public OneShotTimer ScheduleOneShotTimer(string identity, int dueTime, TimerCallback timerCallback, object state)
		{
			OneShotTimer oneShotTimer2;
			using (this.m_stopLock.CreateWorkTicket(this))
			{
				OneShotTimer oneShotTimer = new OneShotTimer(identity, this.m_workTicketFactory, timerCallback, state, this.m_timerCreationFlags);
				oneShotTimer.ScheduleTimer(dueTime);
				oneShotTimer2 = oneShotTimer;
			}
			return oneShotTimer2;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00042CE4 File Offset: 0x00040EE4
		public OneShotTimer ScheduleOneShotTimer(string identity, TimeSpan dueTime, TimerCallback timerCallback, object state)
		{
			int num = TimerBase.ConvertTimeSpanToInt(dueTime);
			return this.ScheduleOneShotTimer(identity, num, timerCallback, state);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00042D04 File Offset: 0x00040F04
		public PeriodicTimer SchedulePeriodicTimer(string identity, int period, TimerCallback timerCallback, object state)
		{
			NonOverlappingStrictPeriodicTimerPolicy nonOverlappingStrictPeriodicTimerPolicy = new NonOverlappingStrictPeriodicTimerPolicy(period);
			return this.SchedulePeriodicTimer(identity, nonOverlappingStrictPeriodicTimerPolicy, timerCallback, state);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00042D24 File Offset: 0x00040F24
		public PeriodicTimer SchedulePeriodicTimer(string identity, TimeSpan period, TimerCallback timerCallback, object state)
		{
			int num = TimerBase.ConvertTimeSpanToInt(period);
			return this.SchedulePeriodicTimer(identity, num, timerCallback, state);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00042D44 File Offset: 0x00040F44
		public PeriodicTimer SchedulePeriodicTimer(string identity, PeriodicTimerPolicy policy, TimerCallback timerCallback, object state)
		{
			PeriodicTimer periodicTimer;
			using (this.m_stopLock.CreateWorkTicket(this))
			{
				periodicTimer = new PeriodicTimer(identity, this.m_workTicketFactory, policy, timerCallback, state, this.m_timerCreationFlags);
			}
			return periodicTimer;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00042D94 File Offset: 0x00040F94
		public string Name
		{
			get
			{
				return this.m_workTicketFactory.Name;
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00042DA4 File Offset: 0x00040FA4
		public void Stop()
		{
			this.m_workTicketFactory.Stop();
			this.m_stopLock.Stop();
			this.m_stopLock.WaitForStopToComplete();
			this.m_stopLock.Shutdown();
			foreach (WorkTicket workTicket in this.m_workTicketFactory.EnumeratePendingTickets(0))
			{
				PendingCallback pendingCallback = workTicket as PendingCallback;
				if (pendingCallback != null)
				{
					pendingCallback.InvokeCallback(PendingCallbackReason.Shutdown);
				}
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00042E2C File Offset: 0x0004102C
		public void WaitForStopToComplete()
		{
			this.m_workTicketFactory.WaitForStopToComplete();
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00042E39 File Offset: 0x00041039
		public void Shutdown()
		{
			this.m_workTicketFactory.Shutdown();
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00042E46 File Offset: 0x00041046
		internal int PendingWorkTickets
		{
			get
			{
				return this.m_workTicketFactory.EnumeratePendingTickets(0).Count<WorkTicket>();
			}
		}

		// Token: 0x0400073B RID: 1851
		protected WorkTicketManager m_workTicketFactory;

		// Token: 0x0400073C RID: 1852
		private WorkTicketManagerSlim m_stopLock;

		// Token: 0x0400073D RID: 1853
		private TimerCreationFlags m_timerCreationFlags;
	}
}
