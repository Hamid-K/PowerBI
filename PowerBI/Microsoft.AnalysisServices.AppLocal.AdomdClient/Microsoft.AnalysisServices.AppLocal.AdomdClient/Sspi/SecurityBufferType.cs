using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000116 RID: 278
	internal enum SecurityBufferType
	{
		// Token: 0x04000968 RID: 2408
		Empty,
		// Token: 0x04000969 RID: 2409
		Data,
		// Token: 0x0400096A RID: 2410
		Token,
		// Token: 0x0400096B RID: 2411
		PkgParams,
		// Token: 0x0400096C RID: 2412
		Missing,
		// Token: 0x0400096D RID: 2413
		Extra,
		// Token: 0x0400096E RID: 2414
		StreamTrailer,
		// Token: 0x0400096F RID: 2415
		StreamHeader,
		// Token: 0x04000970 RID: 2416
		NegotiationInfo,
		// Token: 0x04000971 RID: 2417
		Padding,
		// Token: 0x04000972 RID: 2418
		Stream,
		// Token: 0x04000973 RID: 2419
		Mechlist,
		// Token: 0x04000974 RID: 2420
		MechlistSignature,
		// Token: 0x04000975 RID: 2421
		Target,
		// Token: 0x04000976 RID: 2422
		ChannelBindind,
		// Token: 0x04000977 RID: 2423
		ChangePassResponse,
		// Token: 0x04000978 RID: 2424
		TargetHost,
		// Token: 0x04000979 RID: 2425
		Alert,
		// Token: 0x0400097A RID: 2426
		ApplicationProtocols,
		// Token: 0x0400097B RID: 2427
		SrtpProtectionProfiles,
		// Token: 0x0400097C RID: 2428
		SrtpMasterKeyIdentifier,
		// Token: 0x0400097D RID: 2429
		TokenBinding,
		// Token: 0x0400097E RID: 2430
		PresharedKey,
		// Token: 0x0400097F RID: 2431
		PresharedKeyIdentity,
		// Token: 0x04000980 RID: 2432
		DtlsMtu
	}
}
