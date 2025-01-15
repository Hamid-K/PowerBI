using System;
using System.Threading;
using Microsoft.HostIntegration.Automaton;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AFF RID: 2815
	public class WrappedPooledQueueManager : IWrappedPooledQueueManager
	{
		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x0600596B RID: 22891 RVA: 0x00171502 File Offset: 0x0016F702
		// (set) Token: 0x0600596C RID: 22892 RVA: 0x0017150A File Offset: 0x0016F70A
		public int Instance { get; private set; }

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x0600596D RID: 22893 RVA: 0x00171513 File Offset: 0x0016F713
		// (set) Token: 0x0600596E RID: 22894 RVA: 0x0017151B File Offset: 0x0016F71B
		internal PooledQueueManager QueueManager { get; set; }

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x0600596F RID: 22895 RVA: 0x00171524 File Offset: 0x0016F724
		// (set) Token: 0x06005970 RID: 22896 RVA: 0x0017152C File Offset: 0x0016F72C
		internal bool TimingOut { get; set; }

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06005971 RID: 22897 RVA: 0x00171535 File Offset: 0x0016F735
		// (set) Token: 0x06005972 RID: 22898 RVA: 0x0017153D File Offset: 0x0016F73D
		internal bool BeingDeleted { get; set; }

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x00171546 File Offset: 0x0016F746
		// (set) Token: 0x06005974 RID: 22900 RVA: 0x0017154E File Offset: 0x0016F74E
		internal DateTime TimeLastUsed { get; set; }

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06005975 RID: 22901 RVA: 0x00171557 File Offset: 0x0016F757
		// (set) Token: 0x06005976 RID: 22902 RVA: 0x0017155F File Offset: 0x0016F75F
		internal NameQueueManager NameQueueManager { get; set; }

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x00171568 File Offset: 0x0016F768
		public int ReferenceCount
		{
			get
			{
				object obj = this.lockObject;
				int num;
				lock (obj)
				{
					num = this.shareCount;
				}
				return num;
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06005978 RID: 22904 RVA: 0x001715AC File Offset: 0x0016F7AC
		// (set) Token: 0x06005979 RID: 22905 RVA: 0x001715B4 File Offset: 0x0016F7B4
		internal bool AlreadyRemoved { get; set; }

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x001715BD File Offset: 0x0016F7BD
		// (set) Token: 0x0600597B RID: 22907 RVA: 0x001715C5 File Offset: 0x0016F7C5
		internal bool CreatedPooled { get; set; }

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x001715D0 File Offset: 0x0016F7D0
		public bool Failed
		{
			get
			{
				AutomatonDefinition automaton = this.QueueManager.Automaton.Automaton;
				bool inFailedState;
				lock (automaton)
				{
					inFailedState = (this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).InFailedState;
				}
				return inFailedState;
			}
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x00171638 File Offset: 0x0016F838
		internal WrappedPooledQueueManager(PooledQueueManager queueManager, NameQueueManager nameQueueManager)
		{
			this.QueueManager = queueManager;
			queueManager.WrappedPooledQueueManager = this;
			this.NameQueueManager = nameQueueManager;
			this.Instance = Interlocked.Increment(ref WrappedPooledQueueManager.instance);
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x00171670 File Offset: 0x0016F870
		public int AddReference()
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.shareCount + 1;
				this.shareCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x001716C0 File Offset: 0x0016F8C0
		public int Release()
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.shareCount - 1;
				this.shareCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x00171710 File Offset: 0x0016F910
		public ReturnCode UpdateAsyncCounters()
		{
			return this.QueueManager.UpdateAsyncCounters();
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06005981 RID: 22913 RVA: 0x0017171D File Offset: 0x0016F91D
		// (set) Token: 0x06005982 RID: 22914 RVA: 0x0017173E File Offset: 0x0016F93E
		public int FailedCount
		{
			get
			{
				return (this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).FailedPutCount;
			}
			set
			{
				(this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).FailedPutCount = value;
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06005983 RID: 22915 RVA: 0x00171760 File Offset: 0x0016F960
		// (set) Token: 0x06005984 RID: 22916 RVA: 0x00171781 File Offset: 0x0016F981
		public int SucceededCount
		{
			get
			{
				return (this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).SuccessfulPutCount;
			}
			set
			{
				(this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).SuccessfulPutCount = value;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x001717A3 File Offset: 0x0016F9A3
		// (set) Token: 0x06005986 RID: 22918 RVA: 0x001717C4 File Offset: 0x0016F9C4
		public int WarningCount
		{
			get
			{
				return (this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).WarningPutCount;
			}
			set
			{
				(this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).WarningPutCount = value;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x001717E6 File Offset: 0x0016F9E6
		public int MaximumMessageSize
		{
			get
			{
				return (this.QueueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext).MaximumMessageSize;
			}
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00171808 File Offset: 0x0016FA08
		public override string ToString()
		{
			if (this.NameQueueManager != null)
			{
				return string.Format("Name: '{0}', Id: {1}, QMCIs: {2}", this.NameQueueManager.Name, this.Instance, this.ReferenceCount);
			}
			return string.Format("Id: {0}", this.Instance);
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x0017185E File Offset: 0x0016FA5E
		public override int GetHashCode()
		{
			return this.Instance;
		}

		// Token: 0x0400460F RID: 17935
		private static int instance;

		// Token: 0x04004616 RID: 17942
		private object lockObject = new object();

		// Token: 0x04004617 RID: 17943
		private int shareCount;
	}
}
