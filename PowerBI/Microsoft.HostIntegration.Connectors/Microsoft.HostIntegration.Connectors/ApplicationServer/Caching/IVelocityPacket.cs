using System;
using System.Collections.Generic;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200015F RID: 351
	internal interface IVelocityPacket
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AC9 RID: 2761
		// (set) Token: 0x06000ACA RID: 2762
		VelocityPacketType MessageType { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000ACB RID: 2763
		// (set) Token: 0x06000ACC RID: 2764
		ErrStatus ResponseCode { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000ACD RID: 2765
		// (set) Token: 0x06000ACE RID: 2766
		int MessageID { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ACF RID: 2767
		// (set) Token: 0x06000AD0 RID: 2768
		VelocityPacketHeaderFlags HeaderFlags { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000AD1 RID: 2769
		// (set) Token: 0x06000AD2 RID: 2770
		DataCacheItemVersion Version { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AD3 RID: 2771
		// (set) Token: 0x06000AD4 RID: 2772
		uint? ExpiryTTL { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AD5 RID: 2773
		// (set) Token: 0x06000AD6 RID: 2774
		DataCacheLockHandle LockHandle { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AD7 RID: 2775
		// (set) Token: 0x06000AD8 RID: 2776
		PartitionId PartitionKey { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AD9 RID: 2777
		// (set) Token: 0x06000ADA RID: 2778
		string CacheName { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000ADB RID: 2779
		// (set) Token: 0x06000ADC RID: 2780
		string Region { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000ADD RID: 2781
		// (set) Token: 0x06000ADE RID: 2782
		string Key { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000ADF RID: 2783
		// (set) Token: 0x06000AE0 RID: 2784
		byte[][] Value { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AE1 RID: 2785
		// (set) Token: 0x06000AE2 RID: 2786
		int CacheItemMask { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AE3 RID: 2787
		IVelocityPacketPropertyBag PropertyBag { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AE4 RID: 2788
		// (set) Token: 0x06000AE5 RID: 2789
		object Opaque { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AE6 RID: 2790
		// (set) Token: 0x06000AE7 RID: 2791
		IList<string> Keys { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AE8 RID: 2792
		TcpPacketSendTypes SendType { get; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000AE9 RID: 2793
		// (set) Token: 0x06000AEA RID: 2794
		bool IsMemcacheProtocol { get; set; }
	}
}
