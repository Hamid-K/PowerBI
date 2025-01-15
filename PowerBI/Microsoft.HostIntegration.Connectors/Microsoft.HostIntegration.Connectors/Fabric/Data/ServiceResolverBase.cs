using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003B2 RID: 946
	internal abstract class ServiceResolverBase
	{
		// Token: 0x0600216F RID: 8559 RVA: 0x00067190 File Offset: 0x00065390
		internal ServiceResolverBase()
		{
			this.m_hardComplaints = new List<ServiceResolverBase.HardComplaintContext>();
			this.m_resolvedComplaints = new Queue<ServiceResolverBase.HardComplaintContext>();
			this.m_isRefreshActive = false;
			this.m_refreshInterval = ServiceResolverBase.InitialRefreshInterval;
			this.m_refreshCallback = new WaitCallback(this.Refresh);
			this.m_resolveCallback = new WaitCallback(this.CompleteResolvedContexts);
			this.m_partitionTable = new ClientPartitionTable();
			this.m_lookupTable = new LookupTable();
			this.m_lockObject = new object();
		}

		// Token: 0x06002170 RID: 8560
		internal abstract bool RetrieveLookupTable();

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x00067210 File Offset: 0x00065410
		internal ClientPartitionTable PartitionTable
		{
			get
			{
				return this.m_partitionTable;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x00067218 File Offset: 0x00065418
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x00067220 File Offset: 0x00065420
		internal Exception LastException
		{
			get
			{
				return this.m_lastException;
			}
			set
			{
				this.m_lastException = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		public ReplicaSetMap this[string serviceNamespace]
		{
			get
			{
				return this.m_lookupTable.GetApplicationLookupTable(serviceNamespace);
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00067238 File Offset: 0x00065438
		public void TriggerRefreshAll()
		{
			lock (this.m_lockObject)
			{
				this.StartRefreshIfNeeded();
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00067274 File Offset: 0x00065474
		public ServiceReplicaSet TriggerRefresh(string serviceNamespace, int key, ServiceReplicaSet serviceReplicaSet)
		{
			ServiceReplicaSet updatedConfig;
			lock (this.m_lockObject)
			{
				updatedConfig = this.GetUpdatedConfig(serviceNamespace, key, serviceReplicaSet);
				if (updatedConfig == null)
				{
					this.StartRefreshIfNeeded();
				}
			}
			return updatedConfig;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000672BC File Offset: 0x000654BC
		public bool RefreshAll(TimeSpan timeout)
		{
			return this.EndRefreshAll(this.BeginRefreshAll(timeout, null, null));
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000672CD File Offset: 0x000654CD
		public IAsyncResult BeginRefreshAll(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginRefresh(null, 0, null, timeout, callback, state);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000672DC File Offset: 0x000654DC
		public bool EndRefreshAll(IAsyncResult ar)
		{
			ServiceResolverBase.HardComplaintContext hardComplaintContext = (ServiceResolverBase.HardComplaintContext)ar;
			try
			{
				hardComplaintContext.End();
			}
			catch (Exception ex)
			{
				if (Utility.IsException<TimeoutException>(ex))
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0006731C File Offset: 0x0006551C
		public ServiceReplicaSet Refresh(string serviceNamespace, int key, ServiceReplicaSet replicaSet, TimeSpan timeout)
		{
			return this.EndRefresh(this.BeginRefresh(serviceNamespace, key, replicaSet, timeout, null, null));
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00067334 File Offset: 0x00065534
		public IAsyncResult BeginRefresh(string serviceNamespace, int key, ServiceReplicaSet replicaSet, TimeSpan timeout, AsyncCallback callback, object state)
		{
			ServiceResolverBase.HardComplaintContext hardComplaintContext = new ServiceResolverBase.HardComplaintContext(this, serviceNamespace, key, replicaSet, timeout, callback, state);
			lock (this.m_lockObject)
			{
				ServiceReplicaSet serviceReplicaSet;
				if (serviceNamespace != null)
				{
					serviceReplicaSet = this.GetUpdatedConfig(serviceNamespace, key, replicaSet);
				}
				else
				{
					serviceReplicaSet = null;
				}
				if (serviceReplicaSet != null)
				{
					hardComplaintContext.Update(serviceReplicaSet, true);
				}
				else
				{
					this.m_hardComplaints.Add(hardComplaintContext);
					EventLogWriter.WriteInfo("Complaint", "Add hard complaint {0}", new object[] { hardComplaintContext });
					hardComplaintContext.StartTimer();
					this.StartRefreshIfNeeded();
				}
			}
			return hardComplaintContext;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000673CC File Offset: 0x000655CC
		public ServiceReplicaSet EndRefresh(IAsyncResult ar)
		{
			ServiceResolverBase.HardComplaintContext hardComplaintContext = (ServiceResolverBase.HardComplaintContext)ar;
			try
			{
				hardComplaintContext.End();
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteInfo("Complaint", "Hard complaint {0} failed to be resolved within specified timeout", new object[] { hardComplaintContext });
				if (Utility.IsCommunicationException(ex))
				{
					return null;
				}
				throw;
			}
			EventLogWriter.WriteInfo("Complaint", "Hard complaint {0} resolved with {1}", new object[] { hardComplaintContext, hardComplaintContext.UpdatedConfig });
			return hardComplaintContext.UpdatedConfig;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00067454 File Offset: 0x00065654
		private ServiceReplicaSet GetUpdatedConfig(string serviceNamespace, int key, ServiceReplicaSet replicaSet)
		{
			ServiceReplicaSet serviceReplicaSet;
			try
			{
				serviceReplicaSet = this[serviceNamespace].Lookup(key);
			}
			catch (LookupException)
			{
				serviceReplicaSet = null;
			}
			if (serviceReplicaSet != null && serviceReplicaSet.IsUsable && (replicaSet == null || serviceReplicaSet.Version > replicaSet.Version))
			{
				return serviceReplicaSet;
			}
			return null;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000674A8 File Offset: 0x000656A8
		private void StartRefreshIfNeeded()
		{
			if (!this.m_isRefreshActive)
			{
				ThreadPool.QueueUserWorkItem(this.m_refreshCallback, null);
				this.m_isRefreshActive = true;
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000674C8 File Offset: 0x000656C8
		private bool CheckHardComplaints()
		{
			bool flag = false;
			for (int i = this.m_hardComplaints.Count - 1; i >= 0; i--)
			{
				ServiceResolverBase.HardComplaintContext hardComplaintContext = this.m_hardComplaints[i];
				ServiceReplicaSet serviceReplicaSet;
				bool flag2;
				if (hardComplaintContext.IsRefreshComplaint)
				{
					serviceReplicaSet = null;
					flag2 = true;
				}
				else
				{
					serviceReplicaSet = this.GetUpdatedConfig(hardComplaintContext.ServiceNamespace, hardComplaintContext.Key, hardComplaintContext.ComplaintConfig);
					flag2 = serviceReplicaSet != null;
				}
				if (flag2)
				{
					if (hardComplaintContext.Callback == null)
					{
						hardComplaintContext.Update(serviceReplicaSet, false);
					}
					else
					{
						this.m_resolvedComplaints.Enqueue(hardComplaintContext);
						if (this.m_resolvedComplaints.Count == 1)
						{
							ThreadPool.QueueUserWorkItem(this.m_resolveCallback, null);
						}
					}
					this.m_hardComplaints.RemoveAt(i);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00067580 File Offset: 0x00065780
		internal void UpdateLookupTable(LookupTableTransfer transfer)
		{
			lock (this.m_partitionTable.LockObject)
			{
				IEnumerable<LookupTableEntry> enumerable = this.m_partitionTable.UpdateFromTransfer(transfer);
				if (enumerable != null)
				{
					this.m_lookupTable.ReplaceLookupLists(enumerable);
				}
			}
			EventLogWriter.WriteInfo("CASClient", "Updated partition table to {0} with transfer {1}", new object[]
			{
				this.m_partitionTable.ToShortString(),
				transfer.ToShortString()
			});
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00067604 File Offset: 0x00065804
		private void Refresh(object state)
		{
			bool flag = this.RetrieveLookupTable();
			TimeSpan refreshInterval;
			lock (this.m_lockObject)
			{
				if (flag)
				{
					flag = this.CheckHardComplaints();
				}
				if (this.m_hardComplaints.Count == 0)
				{
					this.m_isRefreshActive = false;
					return;
				}
				refreshInterval = this.m_refreshInterval;
				if (flag)
				{
					this.m_refreshInterval = ServiceResolverBase.InitialRefreshInterval;
				}
				else
				{
					this.m_refreshInterval = new TimeSpan(this.m_refreshInterval.Ticks * 2L);
					if (this.m_refreshInterval > ServiceResolverBase.MaxRefreshInterval)
					{
						this.m_refreshInterval = ServiceResolverBase.MaxRefreshInterval;
					}
				}
			}
			Microsoft.Fabric.Common.Timer timer = new Microsoft.Fabric.Common.Timer(this.m_refreshCallback, null);
			timer.Enqueue(refreshInterval);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000676C0 File Offset: 0x000658C0
		private void CompleteResolvedContexts(object state)
		{
			for (;;)
			{
				ServiceResolverBase.HardComplaintContext hardComplaintContext;
				ServiceReplicaSet serviceReplicaSet;
				lock (this.m_lockObject)
				{
					if (this.m_resolvedComplaints.Count == 0)
					{
						break;
					}
					hardComplaintContext = this.m_resolvedComplaints.Dequeue();
					if (hardComplaintContext.IsRefreshComplaint)
					{
						serviceReplicaSet = null;
					}
					else
					{
						serviceReplicaSet = this[hardComplaintContext.ServiceNamespace].Lookup(hardComplaintContext.Key);
					}
				}
				hardComplaintContext.Update(serviceReplicaSet, false);
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0006773C File Offset: 0x0006593C
		private void RemoveExpiredContext(ServiceResolverBase.HardComplaintContext context)
		{
			lock (this.m_lockObject)
			{
				this.m_hardComplaints.Remove(context);
			}
		}

		// Token: 0x04001553 RID: 5459
		internal const string LogSource = "CASClient";

		// Token: 0x04001554 RID: 5460
		private const string ComplaintLogSource = "Complaint";

		// Token: 0x04001555 RID: 5461
		private List<ServiceResolverBase.HardComplaintContext> m_hardComplaints;

		// Token: 0x04001556 RID: 5462
		private Queue<ServiceResolverBase.HardComplaintContext> m_resolvedComplaints;

		// Token: 0x04001557 RID: 5463
		private bool m_isRefreshActive;

		// Token: 0x04001558 RID: 5464
		private TimeSpan m_refreshInterval;

		// Token: 0x04001559 RID: 5465
		private WaitCallback m_refreshCallback;

		// Token: 0x0400155A RID: 5466
		private WaitCallback m_resolveCallback;

		// Token: 0x0400155B RID: 5467
		private ClientPartitionTable m_partitionTable;

		// Token: 0x0400155C RID: 5468
		private LookupTable m_lookupTable;

		// Token: 0x0400155D RID: 5469
		private Exception m_lastException;

		// Token: 0x0400155E RID: 5470
		private object m_lockObject;

		// Token: 0x0400155F RID: 5471
		internal static TimeSpan InitialRefreshInterval = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x04001560 RID: 5472
		internal static TimeSpan MaxRefreshInterval = TimeSpan.FromMilliseconds(3000.0);

		// Token: 0x020003B3 RID: 947
		private class HardComplaintContext : OperationContext
		{
			// Token: 0x06002185 RID: 8581 RVA: 0x000677A4 File Offset: 0x000659A4
			public HardComplaintContext(ServiceResolverBase client, string serviceNamespace, int key, ServiceReplicaSet complaintConfig, TimeSpan timeout, AsyncCallback callback, object state)
				: base(callback, state, timeout)
			{
				this.m_client = client;
				this.m_serviceNamespace = serviceNamespace;
				this.m_key = key;
				this.m_complaintConfig = complaintConfig;
				this.m_updatedConfig = null;
			}

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06002186 RID: 8582 RVA: 0x000677D6 File Offset: 0x000659D6
			public string ServiceNamespace
			{
				get
				{
					return this.m_serviceNamespace;
				}
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x06002187 RID: 8583 RVA: 0x000677DE File Offset: 0x000659DE
			public int Key
			{
				get
				{
					return this.m_key;
				}
			}

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x06002188 RID: 8584 RVA: 0x000677E6 File Offset: 0x000659E6
			public bool IsRefreshComplaint
			{
				get
				{
					return this.m_serviceNamespace == null;
				}
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x06002189 RID: 8585 RVA: 0x000677F1 File Offset: 0x000659F1
			public ServiceReplicaSet ComplaintConfig
			{
				get
				{
					return this.m_complaintConfig;
				}
			}

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x0600218A RID: 8586 RVA: 0x000677F9 File Offset: 0x000659F9
			public ServiceReplicaSet UpdatedConfig
			{
				get
				{
					return this.m_updatedConfig;
				}
			}

			// Token: 0x0600218B RID: 8587 RVA: 0x00067801 File Offset: 0x00065A01
			public void Update(ServiceReplicaSet updatedConfig, bool completedSynchronously)
			{
				this.m_updatedConfig = updatedConfig;
				base.CompleteOperation(completedSynchronously, null);
			}

			// Token: 0x0600218C RID: 8588 RVA: 0x00067812 File Offset: 0x00065A12
			protected override void OnTimerExpired()
			{
				base.OnTimerExpired();
				this.m_client.RemoveExpiredContext(this);
			}

			// Token: 0x0600218D RID: 8589 RVA: 0x00067828 File Offset: 0x00065A28
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1} {2}", new object[] { this.m_serviceNamespace, this.m_key, this.m_complaintConfig });
			}

			// Token: 0x04001561 RID: 5473
			private ServiceResolverBase m_client;

			// Token: 0x04001562 RID: 5474
			private string m_serviceNamespace;

			// Token: 0x04001563 RID: 5475
			private int m_key;

			// Token: 0x04001564 RID: 5476
			private ServiceReplicaSet m_complaintConfig;

			// Token: 0x04001565 RID: 5477
			private ServiceReplicaSet m_updatedConfig;
		}
	}
}
