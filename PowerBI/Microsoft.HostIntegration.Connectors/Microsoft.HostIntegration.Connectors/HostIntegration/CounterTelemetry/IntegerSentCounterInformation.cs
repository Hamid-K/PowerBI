using System;

namespace Microsoft.HostIntegration.CounterTelemetry
{
	// Token: 0x02000610 RID: 1552
	internal class IntegerSentCounterInformation : SentCounterInformation
	{
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000AED2C File Offset: 0x000ACF2C
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x000AED34 File Offset: 0x000ACF34
		internal uint Counter { get; private set; }

		// Token: 0x06003479 RID: 13433 RVA: 0x000AED3D File Offset: 0x000ACF3D
		internal IntegerSentCounterInformation(uint feature, uint subFeature, uint collection, uint counter, uint value)
			: base(feature, subFeature, collection, value)
		{
			this.Counter = counter;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000AED54 File Offset: 0x000ACF54
		internal unsafe override void GetBytes(ref byte* bytePointer)
		{
			uint* ptr = bytePointer;
			*(ptr++) = base.Feature;
			*(ptr++) = base.SubFeature;
			*(ptr++) = base.Collection;
			*(ptr++) = this.Counter;
			*(ptr++) = base.Value;
			bytePointer = ptr;
		}
	}
}
