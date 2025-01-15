using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200039B RID: 923
	internal interface IRequestTracker
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060020B7 RID: 8375
		TimeSpan ClientEndToEnd { get; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060020B8 RID: 8376
		TimeSpan GatewayEndToEnd { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060020B9 RID: 8377
		TimeSpan GatewayLatency { get; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060020BA RID: 8378
		TimeSpan GatewayLatencyAfterSend { get; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060020BB RID: 8379
		TimeSpan GatewayReturnLatency { get; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060020BC RID: 8380
		TimeSpan PrimaryEndToEnd { get; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060020BD RID: 8381
		string GatewayId { get; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060020BE RID: 8382
		string GatewayDisplayFriendlyId { get; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060020BF RID: 8383
		string PrimaryId { get; }
	}
}
