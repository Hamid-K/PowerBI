using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000257 RID: 599
	internal class RoutingClientDrmUtility : IDRMUtility, IDisposable
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x0003F7E5 File Offset: 0x0003D9E5
		internal RoutingClientDrmUtility(string id)
		{
			this._logSource = "Velocity.DrmUtility" + id;
			this._clientDRMs = new Dictionary<string, DRM>();
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003F809 File Offset: 0x0003DA09
		internal void AddCacheDRM(string cacheName, DRM drm)
		{
			this._clientDRMs.Add(cacheName, drm);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003F818 File Offset: 0x0003DA18
		internal void RemoveCacheDRM(string cacheName)
		{
			this._clientDRMs.Remove(cacheName);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003F828 File Offset: 0x0003DA28
		public void Dispose()
		{
			Dictionary<string, DRM>.ValueCollection values = this._clientDRMs.Values;
			foreach (DRM drm in values)
			{
				drm.Dispose();
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003F884 File Offset: 0x0003DA84
		private DRM GetDRMFromCacheName(string cacheName)
		{
			DRM drm2;
			try
			{
				DRM drm = this._clientDRMs[cacheName];
				drm2 = drm;
			}
			catch (KeyNotFoundException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this._logSource, "Cache Name not found for [{0}] - {1}", new object[] { cacheName, ex });
				}
				drm2 = null;
			}
			return drm2;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0003F8E0 File Offset: 0x0003DAE0
		public void PrepareNotificationRequest(RequestBody requestBody, PartitionId partitionId, NamedCacheConfiguration cacheConfiguration)
		{
			if (requestBody == null)
			{
				throw new ArgumentNullException("requestBody");
			}
			requestBody.Fwd = ForwardingType.Routed;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0003F8F8 File Offset: 0x0003DAF8
		internal EndpointID GetPrimaryEndPoint(string cacheName, string regionName, int systemRegionCount)
		{
			DRM drmfromCacheName = this.GetDRMFromCacheName(cacheName);
			if (drmfromCacheName == null)
			{
				throw new DataCacheException("Could not get the Partition Id. Make sure that the cache exists");
			}
			EndpointID primaryEndPoint = drmfromCacheName.GetPrimaryEndPoint(cacheName, regionName, systemRegionCount);
			if (primaryEndPoint == null)
			{
				throw new DataCacheException("Could not get the Partition Id. Make sure that the cache exists");
			}
			return primaryEndPoint;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0003F934 File Offset: 0x0003DB34
		public ReadOnlyCollection<PartitionId> GetPartitionIdList(string cacheName)
		{
			DRM drmfromCacheName = this.GetDRMFromCacheName(cacheName);
			if (drmfromCacheName == null)
			{
				return null;
			}
			return drmfromCacheName.GetPartitionIdList(cacheName);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0003F958 File Offset: 0x0003DB58
		public PartitionId GetPartitionId(string cacheName, string regionName, int systemRegionCount)
		{
			DRM drmfromCacheName = this.GetDRMFromCacheName(cacheName);
			if (drmfromCacheName == null)
			{
				return null;
			}
			int regionId = Utility.GetRegionId(regionName, systemRegionCount);
			return drmfromCacheName.GetPartitionId(cacheName, regionId);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003F984 File Offset: 0x0003DB84
		public EndpointID GetPrimaryEndPoint(PartitionId partitionId)
		{
			DRM drmfromCacheName = this.GetDRMFromCacheName(partitionId.ServiceNamespace);
			if (drmfromCacheName == null)
			{
				return null;
			}
			return drmfromCacheName.GetPrimaryEndPoint(partitionId);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0003F9AC File Offset: 0x0003DBAC
		public void RaiseComplaint(PartitionId partitionId, EndpointID endpoint)
		{
			DRM drmfromCacheName = this.GetDRMFromCacheName(partitionId.ServiceNamespace);
			if (drmfromCacheName != null)
			{
				drmfromCacheName.RaiseComplaint(partitionId, endpoint);
			}
		}

		// Token: 0x04000C12 RID: 3090
		private Dictionary<string, DRM> _clientDRMs;

		// Token: 0x04000C13 RID: 3091
		private string _logSource;
	}
}
