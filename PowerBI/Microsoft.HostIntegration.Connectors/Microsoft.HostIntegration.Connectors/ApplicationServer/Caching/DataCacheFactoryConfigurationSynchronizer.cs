using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200009A RID: 154
	internal sealed class DataCacheFactoryConfigurationSynchronizer : IDisposable
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000387 RID: 903 RVA: 0x000126F8 File Offset: 0x000108F8
		// (remove) Token: 0x06000388 RID: 904 RVA: 0x00012730 File Offset: 0x00010930
		internal event Action<long> LocalCacheItemCountChanged;

		// Token: 0x06000389 RID: 905 RVA: 0x00012768 File Offset: 0x00010968
		internal void RegisterFactory(DataCacheFactory factory)
		{
			lock (this.factoryInstancesLock)
			{
				if (!this.factoryInstances.Contains(factory))
				{
					this.factoryInstances.Add(factory);
				}
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000127BC File Offset: 0x000109BC
		internal void UnregisterFactory(DataCacheFactory factory)
		{
			lock (this.factoryInstancesLock)
			{
				if (this.factoryInstances.Contains(factory))
				{
					this.factoryInstances.Remove(factory);
				}
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012814 File Offset: 0x00010A14
		internal void UpdateLocalCacheInformation()
		{
			lock (this.factoryInstancesLock)
			{
				long num = 0L;
				foreach (DataCacheFactory dataCacheFactory in this.factoryInstances)
				{
					if (dataCacheFactory.Configuration.LocalCacheProperties.IsEnabled)
					{
						num += dataCacheFactory.LocalCacheInstance.CountLocalCacheItem;
					}
				}
				if (this.LocalCacheItemCountChanged != null)
				{
					this.LocalCacheItemCountChanged(num);
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000128C4 File Offset: 0x00010AC4
		private DataCacheFactoryConfigurationSynchronizer()
		{
			if (ClientPerformanceCounters.IsPerfCounterCategoryExists())
			{
				this.LocalCacheItemCountChanged += ClientPerfCounterUpdate.OnLocalCacheItemCountChanged;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000128FC File Offset: 0x00010AFC
		private void Cleanup()
		{
			if (this.updateTimer != null)
			{
				this.updateTimer.Dispose();
				this.updateTimer = null;
			}
			lock (DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.factoryInstancesLock)
			{
				if (DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.factoryInstances != null)
				{
					DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.factoryInstances.Clear();
					DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.factoryInstances = null;
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001297C File Offset: 0x00010B7C
		public void Dispose()
		{
			this.Cleanup();
		}

		// Token: 0x040002C1 RID: 705
		internal static DataCacheFactoryConfigurationSynchronizer SynchronizerInstance = new DataCacheFactoryConfigurationSynchronizer();

		// Token: 0x040002C2 RID: 706
		private Timer updateTimer;

		// Token: 0x040002C3 RID: 707
		private List<DataCacheFactory> factoryInstances = new List<DataCacheFactory>();

		// Token: 0x040002C4 RID: 708
		private object factoryInstancesLock = new object();
	}
}
