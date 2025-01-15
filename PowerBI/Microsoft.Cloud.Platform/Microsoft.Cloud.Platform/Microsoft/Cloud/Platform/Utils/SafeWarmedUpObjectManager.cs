using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000173 RID: 371
	public class SafeWarmedUpObjectManager : IDisposable
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x00021654 File Offset: 0x0001F854
		public static bool IsValidConfig(int liveDomains, int warmedUpDomains, int domainMaxSafeCalls)
		{
			return liveDomains >= 1 && liveDomains <= 20 && domainMaxSafeCalls >= 3 && domainMaxSafeCalls <= 10000 && warmedUpDomains >= 0 && warmedUpDomains <= 20;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002167C File Offset: 0x0001F87C
		public SafeWarmedUpObjectManager(ITraceSource tracer, Type marshallerType, int liveDomains, int warmedUpDomains, int domainMaxSafeCalls)
		{
			this.m_lastAllocatedDomainId = 0;
			this.m_tracer = tracer ?? TraceSourceBase<CommonTrace>.Tracer;
			this.m_tracer.TraceInformation("SafeWarmedUpObjectManager initialization. marshallerType={0}, liveDomains={1}, warmedUpDomains={2}, domainMaxSafeCalls={3}", new object[]
			{
				(marshallerType != null) ? marshallerType.ToString() : "N/A",
				liveDomains,
				warmedUpDomains,
				domainMaxSafeCalls
			});
			if (marshallerType == null)
			{
				this.m_tracer.TraceError("SafeWarmedUpObjectManager: invalid marshallerType");
				throw new ArgumentException("Invalid marshallerType");
			}
			if (!SafeWarmedUpObjectManager.IsValidConfig(liveDomains, warmedUpDomains, domainMaxSafeCalls))
			{
				this.m_tracer.TraceError("SafeWarmedUpObjectManager: invalid configuration");
				throw new ArgumentException("Invalid Domains and/or MaxSafeCalls configuration");
			}
			this.m_marshallerType = marshallerType;
			this.m_domainMaxSafeCalls = domainMaxSafeCalls;
			this.m_warmedUpDomainsLimit = warmedUpDomains;
			try
			{
				this.InitDomainsList("Live MultiUsageAppDomains", liveDomains, out this.m_liveAppDomains);
				this.InitDomainsList("Warned-Up MultiUsageAppDomains", this.m_warmedUpDomainsLimit, out this.m_warmedUpAppDomains);
				this.m_replenishManager = new SafeWarmedUpObjectManager.ReplenishManager();
			}
			catch (Exception ex) when (!ex.IsFatal())
			{
				this.m_tracer.TraceError("SafeWarmedUpObjectManager initialization failed: {0}", new object[] { ex.Message });
				this.DisposeDomainsList(this.m_liveAppDomains);
				this.DisposeDomainsList(this.m_warmedUpAppDomains);
				throw;
			}
			this.m_usageLoadBalancer = new SafeWarmedUpObjectManager.UsageLoadBalancer(liveDomains);
			this.m_managementLock = new object();
			this.m_allocatedDomainsCounter = liveDomains + this.m_warmedUpDomainsLimit;
			this.m_tracer.TraceInformation("SafeWarmedUpObjectManager initialization done. {0}", new object[] { this.InventoryReportUnderLock() });
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00021834 File Offset: 0x0001FA34
		public SafeWarmedUpObject TryCreateSafeObject(object creationData)
		{
			SafeWarmedUpMultiUsageAppDomain availableDomainAndReplenishIfNeeded = this.GetAvailableDomainAndReplenishIfNeeded();
			SafeWarmedUpObject safeWarmedUpObject = new SafeWarmedUpObject(this.m_tracer, availableDomainAndReplenishIfNeeded, null);
			using (DisposeController disposeController = new DisposeController(safeWarmedUpObject))
			{
				if (safeWarmedUpObject.InitializeMarshaler(creationData))
				{
					disposeController.PreventDispose();
					this.m_tracer.TraceInformation("TryCreateSafeObject created marshaller in domain {0}", new object[] { availableDomainAndReplenishIfNeeded.Identification });
					return safeWarmedUpObject;
				}
			}
			return null;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public void OnAppDomainUnload()
		{
			Interlocked.Increment(ref this.m_unloadedDomainsCounter);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000218C0 File Offset: 0x0001FAC0
		public void Dispose()
		{
			object managementLock = this.m_managementLock;
			List<SafeWarmedUpMultiUsageAppDomain> liveAppDomains;
			List<SafeWarmedUpMultiUsageAppDomain> warmedUpAppDomains;
			lock (managementLock)
			{
				this.m_replenishManager.Dispose();
				liveAppDomains = this.m_liveAppDomains;
				this.m_liveAppDomains = null;
				warmedUpAppDomains = this.m_warmedUpAppDomains;
				this.m_warmedUpAppDomains = null;
			}
			this.DisposeDomainsList(liveAppDomains);
			this.DisposeDomainsList(warmedUpAppDomains);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00021930 File Offset: 0x0001FB30
		public void TraceError(string message)
		{
			this.m_tracer.TraceError(message);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00021940 File Offset: 0x0001FB40
		private SafeWarmedUpMultiUsageAppDomain GetAvailableDomainAndReplenishIfNeeded()
		{
			SafeWarmedUpMultiUsageAppDomain curAppDomain = null;
			string text = null;
			object managementLock = this.m_managementLock;
			lock (managementLock)
			{
				curAppDomain = this.m_liveAppDomains[this.m_usageLoadBalancer.GetNextInstanceToUse()];
				curAppDomain.Reserve();
				bool flag2;
				curAppDomain.AccountForUsage(out flag2);
				if (!flag2)
				{
					return curAppDomain;
				}
				this.TryReplaceLiveDomainWithWarmedUpUnderLock(curAppDomain);
				text = this.InventoryReportUnderLock();
			}
			this.m_tracer.TraceInformation("Use of domain {0}: starting background task to replenish domains. Current inventory is: {1}", new object[] { curAppDomain.Identification, text });
			this.m_replenishManager.Add(delegate
			{
				this.ExecuteAppDomainReplenishment(curAppDomain.Identification);
			});
			return curAppDomain;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00021A30 File Offset: 0x0001FC30
		private void ReplaceLiveDomainAndRetireInLock(SafeWarmedUpMultiUsageAppDomain domainToReplace, SafeWarmedUpMultiUsageAppDomain domainToActivate)
		{
			this.m_liveAppDomains.Add(domainToActivate);
			this.m_liveAppDomains.Remove(domainToReplace);
			Interlocked.Increment(ref this.m_retiredDomainsCounter);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00021A58 File Offset: 0x0001FC58
		private void TryReplaceLiveDomainWithWarmedUpUnderLock(SafeWarmedUpMultiUsageAppDomain curAppDomain)
		{
			SafeWarmedUpMultiUsageAppDomain safeWarmedUpMultiUsageAppDomain = this.m_warmedUpAppDomains.FirstOrDefault<SafeWarmedUpMultiUsageAppDomain>();
			if (safeWarmedUpMultiUsageAppDomain != null)
			{
				this.m_warmedUpAppDomains.Remove(safeWarmedUpMultiUsageAppDomain);
				this.ReplaceLiveDomainAndRetireInLock(curAppDomain, safeWarmedUpMultiUsageAppDomain);
				this.m_tracer.TraceInformation("Live domain {0}, replaced by warmedUp domain {1} and retired ({2})", new object[]
				{
					curAppDomain.Identification,
					safeWarmedUpMultiUsageAppDomain.Identification,
					this.InventoryReportUnderLock()
				});
				curAppDomain.Release();
				return;
			}
			if (this.m_warmedUpDomainsLimit > 0)
			{
				this.m_tracer.TraceWarning("No retierment of live domain {0} yet, as WarmedUp list is empty ({1}).", new object[]
				{
					curAppDomain.Identification,
					this.InventoryReportUnderLock()
				});
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		private void ExecuteAppDomainReplenishment(string spawningDomainIdentification)
		{
			string text = string.Empty;
			SafeWarmedUpMultiUsageAppDomain safeWarmedUpMultiUsageAppDomain = null;
			SafeWarmedUpMultiUsageAppDomain safeWarmedUpMultiUsageAppDomain2 = null;
			SafeWarmedUpMultiUsageAppDomain safeWarmedUpMultiUsageAppDomain3 = this.CreateAppDomain();
			object managementLock = this.m_managementLock;
			lock (managementLock)
			{
				safeWarmedUpMultiUsageAppDomain2 = this.m_liveAppDomains.FirstOrDefault((SafeWarmedUpMultiUsageAppDomain d) => d.ShouldRetire);
				if (safeWarmedUpMultiUsageAppDomain2 != null)
				{
					this.ReplaceLiveDomainAndRetireInLock(safeWarmedUpMultiUsageAppDomain2, safeWarmedUpMultiUsageAppDomain3);
					this.m_tracer.TraceInformation("ExecuteAppDomainReplenishment for domain {0}: Live domain ({1}) replaced by newly created domain ({2}) and retired ({3})", new object[]
					{
						spawningDomainIdentification,
						safeWarmedUpMultiUsageAppDomain2.Identification,
						safeWarmedUpMultiUsageAppDomain3.Identification,
						this.InventoryReportUnderLock()
					});
				}
				else if (this.m_warmedUpAppDomains.Count < this.m_warmedUpDomainsLimit)
				{
					this.m_warmedUpAppDomains.Add(safeWarmedUpMultiUsageAppDomain3);
					this.m_tracer.TraceInformation("ExecuteAppDomainReplenishment for domain {0}: Newly created domain ({1}) added to warmedUpAppDomains ({2})", new object[]
					{
						spawningDomainIdentification,
						safeWarmedUpMultiUsageAppDomain3.Identification,
						this.InventoryReportUnderLock()
					});
				}
				else
				{
					safeWarmedUpMultiUsageAppDomain = safeWarmedUpMultiUsageAppDomain3;
				}
				text = this.InventoryReportUnderLock();
			}
			if (safeWarmedUpMultiUsageAppDomain != null)
			{
				this.m_tracer.TraceWarning("ExecuteAppDomainReplenishment for domain {0}: Newly created domain ({1}) disposed, as not required ({2})", new object[] { spawningDomainIdentification, safeWarmedUpMultiUsageAppDomain.Identification, text });
				Interlocked.Increment(ref this.m_retiredDomainsCounter);
				safeWarmedUpMultiUsageAppDomain.Release();
			}
			if (safeWarmedUpMultiUsageAppDomain2 != null)
			{
				safeWarmedUpMultiUsageAppDomain2.Release();
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00021C4C File Offset: 0x0001FE4C
		private void InitDomainsList(string listName, int domains, out List<SafeWarmedUpMultiUsageAppDomain> domainsList)
		{
			domainsList = new List<SafeWarmedUpMultiUsageAppDomain>();
			this.m_tracer.TraceInformation("SafeWarmedUpObjectManager initialization: Creating {0} domains for {1} ", new object[] { domains, listName });
			for (int i = 0; i < domains; i++)
			{
				SafeWarmedUpMultiUsageAppDomain safeWarmedUpMultiUsageAppDomain = this.CreateAppDomain();
				this.m_tracer.TraceInformation("SafeWarmedUpObjectManager initialization {0}: domain {1} (in {2}) created", new object[] { i, safeWarmedUpMultiUsageAppDomain.Identification, listName });
				domainsList.Add(safeWarmedUpMultiUsageAppDomain);
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00021CCA File Offset: 0x0001FECA
		private void DisposeDomainsList(List<SafeWarmedUpMultiUsageAppDomain> domainsList)
		{
			if (domainsList != null)
			{
				domainsList.ForEach(delegate(SafeWarmedUpMultiUsageAppDomain d)
				{
					d.Release();
				});
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00021CF4 File Offset: 0x0001FEF4
		private SafeWarmedUpMultiUsageAppDomain CreateAppDomain()
		{
			int num = Interlocked.Increment(ref this.m_lastAllocatedDomainId);
			SafeWarmedUpObjectManager.DisposableDomainWrapper disposableDomainWrapper = new SafeWarmedUpObjectManager.DisposableDomainWrapper(new SafeWarmedUpMultiUsageAppDomain(this, this.m_tracer, this.m_marshallerType, num, this.m_domainMaxSafeCalls));
			SafeWarmedUpMultiUsageAppDomain safeDomain;
			using (DisposeController disposeController = new DisposeController(disposableDomainWrapper))
			{
				disposableDomainWrapper.SafeDomain.InitAndWarmup();
				disposeController.PreventDispose();
				Interlocked.Increment(ref this.m_allocatedDomainsCounter);
				safeDomain = disposableDomainWrapper.SafeDomain;
			}
			return safeDomain;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00021D74 File Offset: 0x0001FF74
		private string InventoryReportUnderLock()
		{
			return string.Format("WarmUp:{0}, Live:{1}. #Allocated:{2},#retired:{3}, #disposed:{4}", new object[]
			{
				this.m_warmedUpAppDomains.Count,
				this.m_liveAppDomains.Count,
				this.m_allocatedDomainsCounter,
				this.m_retiredDomainsCounter,
				this.m_unloadedDomainsCounter
			});
		}

		// Token: 0x040003AF RID: 943
		private const int MaxLiveOrWarmedUpDomains = 20;

		// Token: 0x040003B0 RID: 944
		private const int CallsLimitPerDomainMinimum = 3;

		// Token: 0x040003B1 RID: 945
		private const int CallsLimitPerDomainMaximum = 10000;

		// Token: 0x040003B2 RID: 946
		private const int LiveDomainsMinimum = 1;

		// Token: 0x040003B3 RID: 947
		private readonly SafeWarmedUpObjectManager.UsageLoadBalancer m_usageLoadBalancer;

		// Token: 0x040003B4 RID: 948
		private object m_managementLock;

		// Token: 0x040003B5 RID: 949
		private int m_allocatedDomainsCounter;

		// Token: 0x040003B6 RID: 950
		private int m_retiredDomainsCounter;

		// Token: 0x040003B7 RID: 951
		private int m_unloadedDomainsCounter;

		// Token: 0x040003B8 RID: 952
		private readonly int m_warmedUpDomainsLimit;

		// Token: 0x040003B9 RID: 953
		private readonly int m_domainMaxSafeCalls;

		// Token: 0x040003BA RID: 954
		private readonly Type m_marshallerType;

		// Token: 0x040003BB RID: 955
		private readonly ITraceSource m_tracer;

		// Token: 0x040003BC RID: 956
		private List<SafeWarmedUpMultiUsageAppDomain> m_liveAppDomains;

		// Token: 0x040003BD RID: 957
		private List<SafeWarmedUpMultiUsageAppDomain> m_warmedUpAppDomains;

		// Token: 0x040003BE RID: 958
		private SafeWarmedUpObjectManager.ReplenishManager m_replenishManager;

		// Token: 0x040003BF RID: 959
		private int m_lastAllocatedDomainId;

		// Token: 0x02000642 RID: 1602
		public enum ExecuteStatus
		{
			// Token: 0x0400119A RID: 4506
			OK,
			// Token: 0x0400119B RID: 4507
			ParamError,
			// Token: 0x0400119C RID: 4508
			Failed
		}

		// Token: 0x02000643 RID: 1603
		private class UsageLoadBalancer
		{
			// Token: 0x06002CEA RID: 11498 RVA: 0x0009FA77 File Offset: 0x0009DC77
			public UsageLoadBalancer(int targetInstances)
			{
				this.m_targets = targetInstances;
			}

			// Token: 0x06002CEB RID: 11499 RVA: 0x0009FA86 File Offset: 0x0009DC86
			public int GetNextInstanceToUse()
			{
				return (int)(Interlocked.Increment(ref this.m_lastValue) % (long)this.m_targets);
			}

			// Token: 0x0400119D RID: 4509
			private int m_targets;

			// Token: 0x0400119E RID: 4510
			private long m_lastValue;
		}

		// Token: 0x02000644 RID: 1604
		private class ReplenishManager : IDisposable
		{
			// Token: 0x06002CEC RID: 11500 RVA: 0x0009FA9C File Offset: 0x0009DC9C
			public ReplenishManager()
			{
				this.m_cancellationTokenSource = new CancellationTokenSource();
			}

			// Token: 0x06002CED RID: 11501 RVA: 0x0009FAAF File Offset: 0x0009DCAF
			public void Add(Action action)
			{
				Task.Run(action, this.m_cancellationTokenSource.Token).DoNotWait();
			}

			// Token: 0x06002CEE RID: 11502 RVA: 0x0009FAC7 File Offset: 0x0009DCC7
			public void Dispose()
			{
				this.m_cancellationTokenSource.Cancel();
				this.m_cancellationTokenSource.Dispose();
			}

			// Token: 0x0400119F RID: 4511
			private CancellationTokenSource m_cancellationTokenSource;
		}

		// Token: 0x02000645 RID: 1605
		private class DisposableDomainWrapper : IDisposable
		{
			// Token: 0x06002CEF RID: 11503 RVA: 0x0009FADF File Offset: 0x0009DCDF
			public DisposableDomainWrapper(SafeWarmedUpMultiUsageAppDomain safeWarmedUpMultiUsageAppDomain)
			{
				this.SafeDomain = safeWarmedUpMultiUsageAppDomain;
			}

			// Token: 0x17000714 RID: 1812
			// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x0009FAEE File Offset: 0x0009DCEE
			// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x0009FAF6 File Offset: 0x0009DCF6
			public SafeWarmedUpMultiUsageAppDomain SafeDomain { get; private set; }

			// Token: 0x06002CF2 RID: 11506 RVA: 0x0009FAFF File Offset: 0x0009DCFF
			public void Dispose()
			{
				if (this.SafeDomain != null)
				{
					SafeWarmedUpMultiUsageAppDomain safeDomain = this.SafeDomain;
					this.SafeDomain = null;
					safeDomain.Release();
				}
			}
		}
	}
}
