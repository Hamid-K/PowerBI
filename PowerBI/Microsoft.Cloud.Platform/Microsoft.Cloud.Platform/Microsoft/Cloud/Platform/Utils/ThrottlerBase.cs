using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B3 RID: 691
	public abstract class ThrottlerBase : IThrottler, IIdentifiable, IShuttable
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004089D File Offset: 0x0003EA9D
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x000408A5 File Offset: 0x0003EAA5
		private protected object Locker { protected get; private set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x000408AE File Offset: 0x0003EAAE
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x000408B6 File Offset: 0x0003EAB6
		private protected WorkTicketManager WorkTicketManager { protected get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x000408BF File Offset: 0x0003EABF
		public int MaxPendingOperations
		{
			get
			{
				return this.PendingOperations.MaxCapacity;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x000408CC File Offset: 0x0003EACC
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x000408D4 File Offset: 0x0003EAD4
		internal ThrottlerPendingOperationsContainer PendingOperations { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000408DD File Offset: 0x0003EADD
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x000408E5 File Offset: 0x0003EAE5
		private protected IThrottlerNotifications Notifications { protected get; private set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x000408EE File Offset: 0x0003EAEE
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x000408F6 File Offset: 0x0003EAF6
		private protected bool Active { protected get; private set; }

		// Token: 0x06001299 RID: 4761 RVA: 0x00040900 File Offset: 0x0003EB00
		protected ThrottlerBase([NotNull] string name, int maxPendingOperations, [NotNull] IThrottlerNotifications notifications, QueueFullPolicyType policyType)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxPendingOperations, "maxPendingOperations");
			ExtendedDiagnostics.EnsureArgumentNotNull<IThrottlerNotifications>(notifications, "notifications");
			this.Locker = new object();
			this.Name = name;
			this.WorkTicketManager = new WorkTicketManager(name + ".WorkTicketManager");
			this.PendingOperations = ThrottlerBase.ThrottlerAsyncResultQueueFactory.CreateQueueFullPolicy(policyType, maxPendingOperations);
			this.Notifications = notifications;
			this.Active = true;
		}

		// Token: 0x0600129A RID: 4762
		protected abstract void OnCurrentlyRunningOperationEnded(ThrottlerBase.ThrottlerWorkTicket ticket);

		// Token: 0x0600129B RID: 4763
		public abstract IAsyncResult BeginTryAcquireLock(string id, AsyncCallback userCallback, object userContext);

		// Token: 0x0600129C RID: 4764
		public abstract IDisposable EndTryAcquireLock(IAsyncResult result);

		// Token: 0x0600129D RID: 4765 RVA: 0x00040978 File Offset: 0x0003EB78
		public virtual async Task<IDisposable> TryAcquireLockAsync(string id)
		{
			return await Task.Factory.FromAsync<string, IDisposable>(new Func<string, AsyncCallback, object, IAsyncResult>(this.BeginTryAcquireLock), new Func<IAsyncResult, IDisposable>(this.EndTryAcquireLock), id, null);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x000409C5 File Offset: 0x0003EBC5
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x000409CD File Offset: 0x0003EBCD
		public virtual string Name { get; private set; }

		// Token: 0x060012A0 RID: 4768 RVA: 0x000409D8 File Offset: 0x0003EBD8
		public virtual void Stop()
		{
			object obj = this.Locker;
			lock (obj)
			{
				this.Active = false;
			}
			this.WorkTicketManager.Stop();
			obj = this.Locker;
			lock (obj)
			{
				while (this.PendingOperations.Count > 0)
				{
					ThrottlerAsyncResult asyncResult = this.PendingOperations.Dequeue();
					AsyncInvoker.InvokeMethodAsynchronously(delegate
					{
						asyncResult.SignalCompletion(false, new ShutdownSequenceStartedException(string.Concat(new string[] { "Throttler '", this.Name, "'.Stop invoked, operation '", asyncResult.Name, "' cannot run" })));
					}, WaitOrNot.DontWait, "Throttler.Stop");
				}
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00040A90 File Offset: 0x0003EC90
		public virtual void WaitForStopToComplete()
		{
			this.WorkTicketManager.WaitForStopToComplete();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00040A9D File Offset: 0x0003EC9D
		public virtual void Shutdown()
		{
			this.WorkTicketManager.Shutdown();
		}

		// Token: 0x02000779 RID: 1913
		private static class ThrottlerAsyncResultQueueFactory
		{
			// Token: 0x06003079 RID: 12409 RVA: 0x000A66C3 File Offset: 0x000A48C3
			public static ThrottlerPendingOperationsContainer CreateQueueFullPolicy(QueueFullPolicyType policyType, int maxCapacity)
			{
				if (policyType == QueueFullPolicyType.DropNewOperation)
				{
					return new DropNewOperationPendingOperationsContainer(maxCapacity);
				}
				if (policyType != QueueFullPolicyType.DropOldestOperation)
				{
					ExtendedDiagnostics.EnsureInvalidSwitchValue<QueueFullPolicyType>(policyType);
					return null;
				}
				return new DropOldestOperationIPendingOperationsContainer(maxCapacity);
			}
		}

		// Token: 0x0200077A RID: 1914
		protected class ThrottlerWorkTicket : WorkTicket
		{
			// Token: 0x1700075A RID: 1882
			// (get) Token: 0x0600307A RID: 12410 RVA: 0x000A66E3 File Offset: 0x000A48E3
			// (set) Token: 0x0600307B RID: 12411 RVA: 0x000A66EB File Offset: 0x000A48EB
			public bool IsWatched { get; set; }

			// Token: 0x0600307C RID: 12412 RVA: 0x000A66F4 File Offset: 0x000A48F4
			public ThrottlerWorkTicket(ThrottlerBase throttler, IWorkTicketFactory ticketFactory)
			{
				this.m_throttler = throttler;
				ticketFactory.CreateWorkTicket(throttler, this);
			}

			// Token: 0x0600307D RID: 12413 RVA: 0x000A670C File Offset: 0x000A490C
			public void Activate()
			{
				this.m_currentlyRunning = true;
			}

			// Token: 0x0600307E RID: 12414 RVA: 0x000A6715 File Offset: 0x000A4915
			protected override void Dispose(bool disposing)
			{
				if (!this.m_disposed)
				{
					this.m_disposed = true;
					if (disposing)
					{
						if (this.m_currentlyRunning)
						{
							this.m_throttler.OnCurrentlyRunningOperationEnded(this);
						}
						this.m_throttler = null;
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x04001624 RID: 5668
			private bool m_disposed;

			// Token: 0x04001625 RID: 5669
			private ThrottlerBase m_throttler;

			// Token: 0x04001626 RID: 5670
			private bool m_currentlyRunning;
		}
	}
}
