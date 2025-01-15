using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C6 RID: 454
	internal enum MessageFormatStatus
	{
		// Token: 0x04000A2E RID: 2606
		WellFormed,
		// Token: 0x04000A2F RID: 2607
		KeyNull,
		// Token: 0x04000A30 RID: 2608
		CacheNameNullOrEmpty,
		// Token: 0x04000A31 RID: 2609
		InvalidCacheName,
		// Token: 0x04000A32 RID: 2610
		RegionNameNullOrEmpty,
		// Token: 0x04000A33 RID: 2611
		InvalidRegionName,
		// Token: 0x04000A34 RID: 2612
		ValueNull,
		// Token: 0x04000A35 RID: 2613
		LockHandleNull,
		// Token: 0x04000A36 RID: 2614
		TimeToLiveNegative,
		// Token: 0x04000A37 RID: 2615
		TimeToLiveNotGreaterThanZero,
		// Token: 0x04000A38 RID: 2616
		InvalidReqType,
		// Token: 0x04000A39 RID: 2617
		EnumStateNull,
		// Token: 0x04000A3A RID: 2618
		NullTags,
		// Token: 0x04000A3B RID: 2619
		TagsLengthZero,
		// Token: 0x04000A3C RID: 2620
		MoreThanOneTag,
		// Token: 0x04000A3D RID: 2621
		NullTag,
		// Token: 0x04000A3E RID: 2622
		InvalidNotificationRequest,
		// Token: 0x04000A3F RID: 2623
		CorruptStream,
		// Token: 0x04000A40 RID: 2624
		InvalidRegionCount,
		// Token: 0x04000A41 RID: 2625
		InValidForwardingType,
		// Token: 0x04000A42 RID: 2626
		InvalidPartitionId,
		// Token: 0x04000A43 RID: 2627
		InvalidDOMRequestType,
		// Token: 0x04000A44 RID: 2628
		InitialValueNull,
		// Token: 0x04000A45 RID: 2629
		InvalidOperationOnSystemRegion
	}
}
