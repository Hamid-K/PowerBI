using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002DB RID: 731
	[DataContract]
	internal class BroadcastResult
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x00051CEB File Offset: 0x0004FEEB
		internal BroadcastResult(List<IHostConfiguration> hosts, List<IHostConfiguration> failedHosts)
		{
			this._hosts = hosts;
			this._failedHosts = failedHosts;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00051D01 File Offset: 0x0004FF01
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00051D09 File Offset: 0x0004FF09
		[DataMember]
		internal IEnumerable<IHostConfiguration> Hosts
		{
			get
			{
				return this._hosts;
			}
			set
			{
				this._hosts = new List<IHostConfiguration>();
				this._hosts.AddRange(value);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00051D22 File Offset: 0x0004FF22
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x00051D2A File Offset: 0x0004FF2A
		[DataMember]
		internal IEnumerable<IHostConfiguration> FailedHosts
		{
			get
			{
				return this._failedHosts;
			}
			set
			{
				this._failedHosts = new List<IHostConfiguration>();
				this._failedHosts.AddRange(value);
			}
		}

		// Token: 0x04000E54 RID: 3668
		private List<IHostConfiguration> _hosts;

		// Token: 0x04000E55 RID: 3669
		private List<IHostConfiguration> _failedHosts;
	}
}
