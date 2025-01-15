using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000025 RID: 37
	internal class PollerStructure
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000063D9 File Offset: 0x000045D9
		public PollerStructure(IDRMUtility drmDrmUtility)
		{
			this._partitionToPoll = new Dictionary<PartitionId, PartitionNodeInfo>();
			this._drmUtility = drmDrmUtility;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000063F4 File Offset: 0x000045F4
		public DataCacheItemVersion GetPollVersion(PartitionId pid)
		{
			DataCacheItemVersion dataCacheItemVersion = null;
			PartitionNodeInfo partitionNodeInfo;
			lock (this._partitionToPoll)
			{
				this._partitionToPoll.TryGetValue(pid, out partitionNodeInfo);
			}
			if (partitionNodeInfo != null)
			{
				NotificationLsn currentLsn = partitionNodeInfo.CurrentLsn;
				dataCacheItemVersion = new DataCacheItemVersion(new InternalCacheItemVersion(currentLsn.Epoch, currentLsn.Lsn));
			}
			return dataCacheItemVersion;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006464 File Offset: 0x00004664
		public int PreparePerNodePartitionMsg(int totalNotificationsToPoll, out Dictionary<EndpointID, Dictionary<string, List<PartitionNotificationRequest>>> requestTable)
		{
			requestTable = new Dictionary<EndpointID, Dictionary<string, List<PartitionNotificationRequest>>>();
			int num = 0;
			lock (this._partitionToPoll)
			{
				if (this._partitionToPoll.Count > 0)
				{
					int num2 = Math.Max(1, totalNotificationsToPoll / this._partitionToPoll.Count);
					foreach (KeyValuePair<PartitionId, PartitionNodeInfo> keyValuePair in this._partitionToPoll)
					{
						EndpointID primaryEndPoint = this._drmUtility.GetPrimaryEndPoint(keyValuePair.Key);
						if (primaryEndPoint == null)
						{
							if (Provider.IsEnabled(TraceLevel.Info))
							{
								EventLogWriter.WriteInfo("PollerStructure", "No Endpoint Found from DRM for partition {0}", new object[] { keyValuePair.Key });
							}
						}
						else
						{
							PartitionNodeInfo value = keyValuePair.Value;
							if (value.IsCacheLevel || value.RegionList != null)
							{
								bool isCacheLevel = value.IsCacheLevel;
								PartitionNotificationRequest partitionNotificationRequest = new PartitionNotificationRequest(keyValuePair.Key, isCacheLevel, isCacheLevel ? null : new List<string>(value.RegionList), value.CurrentLsn, num2);
								Dictionary<string, List<PartitionNotificationRequest>> dictionary = null;
								if (!requestTable.TryGetValue(primaryEndPoint, out dictionary))
								{
									dictionary = new Dictionary<string, List<PartitionNotificationRequest>>(32);
									requestTable.Add(primaryEndPoint, dictionary);
								}
								List<PartitionNotificationRequest> list = null;
								if (!dictionary.TryGetValue(partitionNotificationRequest.PartitionId.ServiceNamespace, out list))
								{
									list = new List<PartitionNotificationRequest>(32);
									dictionary[partitionNotificationRequest.PartitionId.ServiceNamespace] = list;
									num++;
								}
								list.Add(partitionNotificationRequest);
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000662C File Offset: 0x0000482C
		public bool UpdateLsnOnNotificationReceive(PartitionId pId, NotificationLsn newLsn)
		{
			bool flag2;
			lock (this._partitionToPoll)
			{
				PartitionNodeInfo partitionNodeInfo;
				this._partitionToPoll.TryGetValue(pId, out partitionNodeInfo);
				if (partitionNodeInfo != null && partitionNodeInfo.CurrentLsn < newLsn)
				{
					partitionNodeInfo.CurrentLsn = newLsn;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006694 File Offset: 0x00004894
		public bool PutRegistrationPartitionNode(PartitionId partitionId, string regionName)
		{
			PartitionNodeInfo partitionNodeInfo = null;
			bool flag2;
			lock (this._partitionToPoll)
			{
				this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
				if (partitionNodeInfo == null)
				{
					flag2 = false;
				}
				else
				{
					partitionNodeInfo.AddRegion(regionName);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000066F0 File Offset: 0x000048F0
		public void PutRegistrationPartitionNode(PartitionId partitionId, string regionName, NotificationLsn lsn)
		{
			PartitionNodeInfo partitionNodeInfo = null;
			lock (this._partitionToPoll)
			{
				this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
				if (partitionNodeInfo == null)
				{
					partitionNodeInfo = new PartitionNodeInfo(partitionId);
					partitionNodeInfo.CurrentLsn = lsn;
					this._partitionToPoll.Add(partitionId, partitionNodeInfo);
				}
				partitionNodeInfo.AddRegion(regionName);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006760 File Offset: 0x00004960
		public void RemoveRegistrationPartitionNode(PartitionId partitionId, string regionName)
		{
			PartitionNodeInfo partitionNodeInfo = null;
			lock (this._partitionToPoll)
			{
				this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
				if (partitionNodeInfo != null)
				{
					partitionNodeInfo.RemoveRegion(regionName);
					if (partitionNodeInfo.RefCount == 0)
					{
						this._partitionToPoll.Remove(partitionId);
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<PartitionId>("PollerStructure", "RemoveRegistrationPartitionNode: Partition removed from Poll List {0}.", partitionId);
						}
					}
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000067E4 File Offset: 0x000049E4
		public bool PutRegistrationPartitionNodeList(PartitionId[] partitionIdList)
		{
			PartitionNodeInfo partitionNodeInfo = null;
			lock (this._partitionToPoll)
			{
				for (int i = 0; i < partitionIdList.Length; i++)
				{
					partitionNodeInfo = null;
					PartitionId partitionId = partitionIdList[i];
					this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
					if (partitionNodeInfo == null)
					{
						for (int j = 0; j < i; j++)
						{
							partitionNodeInfo = null;
							partitionId = partitionIdList[j];
							this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
							partitionNodeInfo.RemoveForCache();
						}
						return false;
					}
					partitionNodeInfo.AddForCache();
				}
			}
			return true;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006884 File Offset: 0x00004A84
		public void PutRegistrationPartitionNodeList(List<PartitionNotificationLsn> pnList)
		{
			PartitionNodeInfo partitionNodeInfo = null;
			lock (this._partitionToPoll)
			{
				for (int i = 0; i < pnList.Count; i++)
				{
					partitionNodeInfo = null;
					PartitionId partitionId = pnList[i].PartitionId;
					this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
					if (partitionNodeInfo == null)
					{
						partitionNodeInfo = new PartitionNodeInfo(partitionId);
						partitionNodeInfo.CurrentLsn = pnList[i].LastLsn;
						this._partitionToPoll.Add(partitionId, partitionNodeInfo);
					}
					partitionNodeInfo.AddForCache();
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006924 File Offset: 0x00004B24
		public void RemoveRegistrationPartitionNodeList(PartitionId[] partitionIdList)
		{
			PartitionNodeInfo partitionNodeInfo = null;
			lock (this._partitionToPoll)
			{
				for (int i = 0; i < partitionIdList.Length; i++)
				{
					partitionNodeInfo = null;
					PartitionId partitionId = partitionIdList[i];
					this._partitionToPoll.TryGetValue(partitionId, out partitionNodeInfo);
					if (partitionNodeInfo != null)
					{
						partitionNodeInfo.RemoveForCache();
						if (partitionNodeInfo.RefCount == 0)
						{
							this._partitionToPoll.Remove(partitionId);
							if (Provider.IsEnabled(TraceLevel.Verbose))
							{
								EventLogWriter.WriteVerbose<PartitionId>("PollerStructure", "RemoveRegistrationPartitionNodeList: Partition removed from Poll List {0}.", partitionId);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400009A RID: 154
		private const string _myComponentName = "PollerStructure";

		// Token: 0x0400009B RID: 155
		private Dictionary<PartitionId, PartitionNodeInfo> _partitionToPoll;

		// Token: 0x0400009C RID: 156
		private IDRMUtility _drmUtility;
	}
}
