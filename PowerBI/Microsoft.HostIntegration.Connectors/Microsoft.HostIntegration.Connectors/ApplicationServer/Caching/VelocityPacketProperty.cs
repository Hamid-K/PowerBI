using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200017F RID: 383
	internal enum VelocityPacketProperty : short
	{
		// Token: 0x040008A9 RID: 2217
		Tags = 1,
		// Token: 0x040008AA RID: 2218
		EnumState,
		// Token: 0x040008AB RID: 2219
		NotificationRequest,
		// Token: 0x040008AC RID: 2220
		NotificationLsnRequest,
		// Token: 0x040008AD RID: 2221
		NotificationReply,
		// Token: 0x040008AE RID: 2222
		InitialValue,
		// Token: 0x040008AF RID: 2223
		LookupTable = 32,
		// Token: 0x040008B0 RID: 2224
		ExternalLookupTableWithIdentifiers,
		// Token: 0x040008B1 RID: 2225
		MessageTrackingId = 128,
		// Token: 0x040008B2 RID: 2226
		MessageGatewayTracker,
		// Token: 0x040008B3 RID: 2227
		MessagePrimaryTracker,
		// Token: 0x040008B4 RID: 2228
		AuthorizationToken = 240,
		// Token: 0x040008B5 RID: 2229
		DeploymentMode = 257,
		// Token: 0x040008B6 RID: 2230
		PartitionCount,
		// Token: 0x040008B7 RID: 2231
		RegionCount,
		// Token: 0x040008B8 RID: 2232
		DefaultTTL,
		// Token: 0x040008B9 RID: 2233
		EvictionType,
		// Token: 0x040008BA RID: 2234
		ExpirationType,
		// Token: 0x040008BB RID: 2235
		NotificationProperties,
		// Token: 0x040008BC RID: 2236
		ServerVersion = 513,
		// Token: 0x040008BD RID: 2237
		HostSize = 769,
		// Token: 0x040008BE RID: 2238
		HostMaxBufferSize,
		// Token: 0x040008BF RID: 2239
		HostDefaultBufferSize,
		// Token: 0x040008C0 RID: 2240
		GetRequestCount = 1025,
		// Token: 0x040008C1 RID: 2241
		GetMissRequestCount,
		// Token: 0x040008C2 RID: 2242
		UpsertRequestCount,
		// Token: 0x040008C3 RID: 2243
		AddRequestCount,
		// Token: 0x040008C4 RID: 2244
		TotalObjectCount = 1041,
		// Token: 0x040008C5 RID: 2245
		TotalObjectSize,
		// Token: 0x040008C6 RID: 2246
		IncomingBandwidth,
		// Token: 0x040008C7 RID: 2247
		OutgoingBandwidth
	}
}
