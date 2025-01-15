using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003AC RID: 940
	internal class VasHybridChannelConfig
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x00066F50 File Offset: 0x00065150
		public VasHybridChannelConfig(EndpointID vipEndpoint, int optimalConnectionCount)
		{
			this.VipEndpoint = vipEndpoint;
			this.OptimalConnectionCount = optimalConnectionCount;
			this.IsVipPoolCreated = false;
			this.TotalActiveClientConnections = 0;
		}

		// Token: 0x04001542 RID: 5442
		internal int OptimalConnectionCount = ConfigManager.DefaultOptimalConnectionCount;

		// Token: 0x04001543 RID: 5443
		internal EndpointID VipEndpoint;

		// Token: 0x04001544 RID: 5444
		internal int TotalActiveClientConnections;

		// Token: 0x04001545 RID: 5445
		internal bool IsVipPoolCreated;

		// Token: 0x04001546 RID: 5446
		internal List<ChannelContainer> VipConnectionPool;

		// Token: 0x04001547 RID: 5447
		public int InProgressChannelCount;
	}
}
