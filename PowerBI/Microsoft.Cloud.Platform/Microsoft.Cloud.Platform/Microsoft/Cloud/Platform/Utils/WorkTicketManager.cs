using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F6 RID: 758
	public class WorkTicketManager : IWorkTicketFactory, IIdentifiable, IShuttable
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x00045750 File Offset: 0x00043950
		public WorkTicketManager(string name)
			: this(name, true)
		{
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0004575C File Offset: 0x0004395C
		public WorkTicketManager(string name, bool initiallyActive)
		{
			this.m_name = ((name != null) ? name : "");
			this.m_locker = new object();
			this.m_active = initiallyActive;
			this.m_canBeActivated = true;
			this.m_numPendingWorkTickets = 0L;
			this.m_nextId = 1L;
			this.m_trackTickets = false;
			this.m_captureTicketsCallStack = false;
			this.m_pendingWorkTickets = new Dictionary<long, WorkTicket>();
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000457C2 File Offset: 0x000439C2
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000457CC File Offset: 0x000439CC
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x00045820 File Offset: 0x00043A20
		public bool TrackTickets
		{
			get
			{
				object locker = this.m_locker;
				bool flag2;
				lock (locker)
				{
					flag2 = this.m_trackTickets || WorkTicketManager.sm_trackTicketsTweak.Value;
				}
				return flag2;
			}
			set
			{
				object locker = this.m_locker;
				lock (locker)
				{
					this.m_trackTickets = value;
				}
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00045864 File Offset: 0x00043A64
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x000458B8 File Offset: 0x00043AB8
		public bool CaptureTicketsCallStack
		{
			get
			{
				object locker = this.m_locker;
				bool flag2;
				lock (locker)
				{
					flag2 = this.m_captureTicketsCallStack || WorkTicketManager.sm_captureTicketsCallStackTweak.Value;
				}
				return flag2;
			}
			set
			{
				object locker = this.m_locker;
				lock (locker)
				{
					this.m_captureTicketsCallStack = value;
				}
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000458FC File Offset: 0x00043AFC
		public IEnumerable<WorkTicket> EnumeratePendingTickets(int maxOfTickets)
		{
			WorkTicket[] array = null;
			object locker = this.m_locker;
			lock (locker)
			{
				Ensure.IsTrue(this.TrackTickets, "Cannot invoke WorkTicketManager.EnumeratePendingTickets if TrackTickets is not enabled");
				int num = this.m_pendingWorkTickets.Count;
				if (maxOfTickets > 0 && maxOfTickets < num)
				{
					num = maxOfTickets;
				}
				array = new WorkTicket[num];
				int num2 = 0;
				foreach (KeyValuePair<long, WorkTicket> keyValuePair in this.m_pendingWorkTickets)
				{
					if (num2 >= array.Length)
					{
						break;
					}
					array[num2] = keyValuePair.Value;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000459C0 File Offset: 0x00043BC0
		public IEnumerable<WorkTicket> EnumeratePendingTickets(IIdentifiable owningEntity)
		{
			List<WorkTicket> list = new List<WorkTicket>();
			object locker = this.m_locker;
			WorkTicket[] array;
			lock (locker)
			{
				foreach (KeyValuePair<long, WorkTicket> keyValuePair in this.m_pendingWorkTickets)
				{
					if (keyValuePair.Value.OwningEntity == owningEntity)
					{
						list.Add(keyValuePair.Value);
					}
				}
				array = new WorkTicket[list.Count];
				int num = 0;
				foreach (WorkTicket workTicket in list)
				{
					array[num] = workTicket;
					num++;
				}
			}
			return array;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00045AAC File Offset: 0x00043CAC
		public void Start()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManager of {0} received Start()", new object[] { this.Name });
			object locker = this.m_locker;
			lock (locker)
			{
				Ensure.IsTrue(this.m_canBeActivated, "WorkTicketManager.Start cannot be called post Stop or Shutdown");
				this.m_active = true;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00045B1C File Offset: 0x00043D1C
		public void NonFinalStop()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManager of {0} received NonFinalStop()", new object[] { this.Name });
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_active = false;
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00045B7C File Offset: 0x00043D7C
		public void Stop()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManager of {0} received Stop()", new object[] { this.Name });
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_active = false;
				this.m_canBeActivated = false;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00045BE4 File Offset: 0x00043DE4
		public void WaitForStopToComplete()
		{
			this.WaitForStopToCompleteOrCrash(-1, null);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00045BF0 File Offset: 0x00043DF0
		public void WaitForStopToCompleteOrCrash(int timeout, Action<WorkTicketManager> invokeOnTimeout)
		{
			ExtendedDiagnostics.EnsureArgument(timeout, "timeout value must be greater than zero", timeout == -1 || timeout > 0);
			DebuggableMonitorWaiter debuggableMonitorWaiter = new DebuggableMonitorWaiter(this.m_locker, timeout);
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManager of {0} received WaitForStopToComplete(OrCrash)", new object[] { this.Name });
			object locker = this.m_locker;
			lock (locker)
			{
				Ensure.IsTrue(!this.m_active, "WorkTicketManager.WaitForStopToComplete(OrCrash) can only be called when the state is inactive (e.g., after Stop)");
				while (this.m_numPendingWorkTickets != 0L)
				{
					if (this.TrackTickets)
					{
						TraceDump traceDump = new TraceDump();
						traceDump.Add("Following is a list of pending work tickets in WTM " + this.Name);
						int num = 0;
						foreach (WorkTicket workTicket in this.EnumeratePendingTickets(10))
						{
							num++;
							traceDump.Add("-- Work ticket " + workTicket.Id + " ---");
							workTicket.Dump(traceDump);
						}
						traceDump.Add("End of list of pending work tickets (" + this.m_numPendingWorkTickets + " tickets)");
						foreach (string text in traceDump.Lines)
						{
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "{0}", new object[] { text });
						}
					}
					if (!debuggableMonitorWaiter.TryWait())
					{
						if (invokeOnTimeout != null)
						{
							invokeOnTimeout(this);
						}
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "WorkTicketManager.WaitForStopToCompleteOrCrash of {0} has reached a timeout (after {1} milliseconds) with {2} undisposed tickets", new object[] { this.Name, timeout, this.m_numPendingWorkTickets });
						ExtendedEnvironment.FailSlow(this, string.Format(CultureInfo.CurrentCulture, "WorkTicketManager.WaitForStopToCompleteOrCrash of {0} has reached a timeout but {1} worktickets were not disposed", new object[] { this.Name, this.m_numPendingWorkTickets }));
					}
				}
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "WorkTicketManager of {0} completed WaitForStopToCompleteOrCrash", new object[] { this.Name });
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00045E60 File Offset: 0x00044060
		public void Shutdown()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_canBeActivated = false;
				if (this.m_active)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "WorkTicketManager.Shutdown of {0} may only be invoked when it is inactive. Did you forget to call Stop?", new object[] { this.Name });
					Ensure.IsTrue(!this.m_active, text);
				}
				if (this.m_numPendingWorkTickets != 0L)
				{
					string text2 = string.Format(CultureInfo.InvariantCulture, "WorkTicketManager.Shutdown of {0} may only be invoked when there are no pending work tickets. Currently there are {1}. Did you forget to call WaitForStopToComplete?", new object[] { this.Name, this.m_numPendingWorkTickets });
					Ensure.IsTrue(this.m_numPendingWorkTickets == 0L, text2);
				}
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00045F1C File Offset: 0x0004411C
		public WorkTicket TryCreateWorkTicket(IIdentifiable owningEntity)
		{
			WorkTicket workTicket = new WorkTicket();
			if (!this.InitWorkTicket(owningEntity, workTicket))
			{
				return null;
			}
			return workTicket;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00045F3C File Offset: 0x0004413C
		public WorkTicket CreateWorkTicket(IIdentifiable owningEntity)
		{
			WorkTicket workTicket = new WorkTicket();
			return this.CreateWorkTicket(owningEntity, workTicket);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00045F57 File Offset: 0x00044157
		public WorkTicket CreateWorkTicket(IIdentifiable owningEntity, WorkTicket ticket)
		{
			if (this.InitWorkTicket(owningEntity, ticket))
			{
				return ticket;
			}
			throw new ShutdownSequenceStartedException("Cannot create work ticket after Stop was called");
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00045F6F File Offset: 0x0004416F
		public bool TryCreateWorkTicket(IIdentifiable owningEntity, WorkTicket ticket)
		{
			return this.InitWorkTicket(owningEntity, ticket);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00045F7C File Offset: 0x0004417C
		private bool InitWorkTicket(IIdentifiable owningEntity, [NotNull] WorkTicket ticket)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(ticket, "ticket");
			if (ticket.IsValid())
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Ticket ID {0} is already initialized", new object[] { ticket.Id });
				throw new InvalidOperationException("Error: ticket id " + ticket.Id + " is already initialized");
			}
			object locker = this.m_locker;
			bool flag2;
			lock (locker)
			{
				string text = ((owningEntity == null || string.IsNullOrEmpty(owningEntity.Name)) ? "unidetifiedOwner" : owningEntity.Name);
				if (!this.m_active)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "WorkTicketManger of {0} unable to create WorkTicket requested by {1}", new object[] { this.Name, text });
					flag2 = false;
				}
				else
				{
					this.m_numPendingWorkTickets += 1L;
					long nextId = this.m_nextId;
					this.m_nextId = nextId + 1L;
					long num = nextId;
					ticket.Init(this, num, owningEntity, this.TrackTickets, WorkTicketManager.sm_leakDetectionEnabledTweak.Value, this.CaptureTicketsCallStack);
					if (ticket.Tracked)
					{
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Work ticket '{0}.{1}' owned by '{2}' is being initialized and tracked. Pending tickets (before init): '{3}'", new object[] { this.Name, ticket.Id, text, this.m_numPendingWorkTickets });
						this.m_pendingWorkTickets.Add(num, ticket);
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000460FC File Offset: 0x000442FC
		internal void OnWorkTicketDisposed(WorkTicket ticket)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				string text = ((ticket.OwningEntity == null) ? "unidetifiedOwner" : ticket.OwningEntity.Name);
				if (ticket.Tracked)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Work ticket '{0}.{1}' owned by '{2}' is being disposed. Pending tickets (before dispose): '{3}'", new object[] { this.Name, ticket.Id, text, this.m_numPendingWorkTickets });
					this.m_pendingWorkTickets.Remove(ticket.Id);
				}
				this.m_numPendingWorkTickets -= 1L;
				if (this.m_numPendingWorkTickets == 0L)
				{
					Monitor.PulseAll(this.m_locker);
				}
			}
		}

		// Token: 0x0400079F RID: 1951
		public const string LeakDetectionEnabledTweakName = "Microsoft.Cloud.Platform.Utils.WorkTicket.LeakDetectionEnabled";

		// Token: 0x040007A0 RID: 1952
		public const string TrackTicketsTweakName = "Microsoft.Cloud.Platform.Utils.WorkTicket.TrackTickets";

		// Token: 0x040007A1 RID: 1953
		public const string CaptureTicketsCallStackTweakName = "Microsoft.Cloud.Platform.Utils.WorkTicket.CaptureTicketsCallStack";

		// Token: 0x040007A2 RID: 1954
		private readonly string m_name;

		// Token: 0x040007A3 RID: 1955
		private object m_locker;

		// Token: 0x040007A4 RID: 1956
		private bool m_active;

		// Token: 0x040007A5 RID: 1957
		private bool m_canBeActivated;

		// Token: 0x040007A6 RID: 1958
		private long m_numPendingWorkTickets;

		// Token: 0x040007A7 RID: 1959
		private long m_nextId;

		// Token: 0x040007A8 RID: 1960
		private bool m_trackTickets;

		// Token: 0x040007A9 RID: 1961
		private bool m_captureTicketsCallStack;

		// Token: 0x040007AA RID: 1962
		private Dictionary<long, WorkTicket> m_pendingWorkTickets;

		// Token: 0x040007AB RID: 1963
		private static Tweak<bool> sm_leakDetectionEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.WorkTicket.LeakDetectionEnabled", "When set, work ticket leaks are detected in debug builds", false);

		// Token: 0x040007AC RID: 1964
		private static Tweak<bool> sm_trackTicketsTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.WorkTicket.TrackTickets", "When set, all work tickets are tracked", false);

		// Token: 0x040007AD RID: 1965
		private static Tweak<bool> sm_captureTicketsCallStackTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.WorkTicket.CaptureTicketsCallStack", "When set, each work ticket maintains the call stack that created it", false);
	}
}
