using System;
using System.Collections.Generic;
using Microsoft.Fabric.Common;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C6 RID: 966
	internal abstract class ServiceResolver : ServiceResolverBase
	{
		// Token: 0x06002206 RID: 8710 RVA: 0x00068BD8 File Offset: 0x00066DD8
		protected ServiceResolver(IEnumerable<string> serviceNamespaces, IEnumerable<string> nodeAddresses)
		{
			this.m_nodeAddresses = new List<string>(nodeAddresses);
			if (serviceNamespaces != null)
			{
				this.m_serviceNamespaces = new HashSet<string>(serviceNamespaces);
			}
			else
			{
				this.m_serviceNamespaces = null;
			}
			this.m_failedAddresses = new HashSet<string>();
			this.m_random = new Random((int)DateTime.Now.Ticks);
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00068C33 File Offset: 0x00066E33
		internal HashSet<string> ServiceNamespaces
		{
			get
			{
				return this.m_serviceNamespaces;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00068C3C File Offset: 0x00066E3C
		internal List<string> GetEndpointAddresses()
		{
			return new List<string>(this.m_nodeAddresses);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00068C58 File Offset: 0x00066E58
		private void UpdateEndpointAddresses(LookupTableTransfer transfer)
		{
			HashSet<string> hashSet = new HashSet<string>(this.m_nodeAddresses);
			foreach (LookupTableEntry lookupTableEntry in transfer.Entries)
			{
				foreach (string text in lookupTableEntry.Config.AllReplicas)
				{
					hashSet.Add(text);
					this.m_failedAddresses.Remove(text);
				}
			}
			List<string> list = new List<string>(hashSet);
			Utility.GenerateShuffledArray<string>(list);
			this.m_nodeAddresses = list;
		}

		// Token: 0x0600220A RID: 8714
		protected abstract IAsyncResult BeginRequestLookupTable(string nodeAddress, LookupTableRequest request, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600220B RID: 8715
		protected abstract LookupTableTransfer EndRequestLookupTable(IAsyncResult result);

		// Token: 0x0600220C RID: 8716 RVA: 0x00068D1C File Offset: 0x00066F1C
		private bool RetrieveLookupTable(string addr)
		{
			LookupTableTransfer lookupTableTransfer;
			try
			{
				LookupTableRequest lookupTableRequest = base.PartitionTable.CreateRequest(this.m_serviceNamespaces);
				lookupTableTransfer = this.EndRequestLookupTable(this.BeginRequestLookupTable(addr, lookupTableRequest, ServiceResolver.LookupTableRequestTimeout, null, null));
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteWarning("CASClient", "Failed to retrieve lookup table from {0}: {1}", new object[] { addr, ex });
				if (Utility.IsCommunicationException(ex))
				{
					this.m_failedAddresses.Add(addr);
					base.LastException = ex;
					return false;
				}
				throw;
			}
			base.LastException = null;
			if (lookupTableTransfer == null || lookupTableTransfer.Entries == null)
			{
				EventLogWriter.WriteInfo("CASClient", "Retrieve lookup table from {0} returns no update", new object[] { addr });
				return false;
			}
			if (lookupTableTransfer.Count > 0)
			{
				base.UpdateLookupTable(lookupTableTransfer);
				this.UpdateEndpointAddresses(lookupTableTransfer);
			}
			return true;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00068DF8 File Offset: 0x00066FF8
		internal override bool RetrieveLookupTable()
		{
			List<string> list = new List<string>();
			int num = this.m_random.Next(0, this.m_nodeAddresses.Count);
			for (int i = 0; i < this.m_nodeAddresses.Count; i++)
			{
				int num2 = (num + i) % this.m_nodeAddresses.Count;
				string text = this.m_nodeAddresses[num2];
				if (this.m_failedAddresses.Contains(text))
				{
					list.Add(text);
				}
				else if (this.RetrieveLookupTable(text))
				{
					return true;
				}
			}
			foreach (string text2 in list)
			{
				this.m_failedAddresses.Remove(text2);
				if (this.RetrieveLookupTable(text2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001594 RID: 5524
		private HashSet<string> m_serviceNamespaces;

		// Token: 0x04001595 RID: 5525
		private List<string> m_nodeAddresses;

		// Token: 0x04001596 RID: 5526
		private HashSet<string> m_failedAddresses;

		// Token: 0x04001597 RID: 5527
		private Random m_random;

		// Token: 0x04001598 RID: 5528
		private static readonly TimeSpan LookupTableRequestTimeout = TimeSpan.FromSeconds(30.0);
	}
}
