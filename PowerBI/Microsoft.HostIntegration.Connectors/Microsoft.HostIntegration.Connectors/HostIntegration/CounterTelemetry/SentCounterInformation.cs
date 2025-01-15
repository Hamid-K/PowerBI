using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x0200060F RID: 1551
	internal abstract class SentCounterInformation
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000AECC3 File Offset: 0x000ACEC3
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000AECCB File Offset: 0x000ACECB
		internal uint Feature { get; private set; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000AECD4 File Offset: 0x000ACED4
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000AECDC File Offset: 0x000ACEDC
		internal uint SubFeature { get; private set; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000AECE5 File Offset: 0x000ACEE5
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x000AECED File Offset: 0x000ACEED
		internal uint Collection { get; private set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000AECF6 File Offset: 0x000ACEF6
		// (set) Token: 0x06003474 RID: 13428 RVA: 0x000AECFE File Offset: 0x000ACEFE
		internal uint Value { get; private set; }

		// Token: 0x06003475 RID: 13429 RVA: 0x000AED07 File Offset: 0x000ACF07
		internal SentCounterInformation(uint feature, uint subFeature, uint collection, uint value)
		{
			this.Feature = feature;
			this.SubFeature = subFeature;
			this.Collection = collection;
			this.Value = value;
		}

		// Token: 0x06003476 RID: 13430
		internal unsafe abstract void GetBytes(ref byte* bytePointer);
	}
}
