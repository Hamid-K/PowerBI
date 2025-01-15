using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000116 RID: 278
	internal enum SecurityBufferType
	{
		// Token: 0x0400095B RID: 2395
		Empty,
		// Token: 0x0400095C RID: 2396
		Data,
		// Token: 0x0400095D RID: 2397
		Token,
		// Token: 0x0400095E RID: 2398
		PkgParams,
		// Token: 0x0400095F RID: 2399
		Missing,
		// Token: 0x04000960 RID: 2400
		Extra,
		// Token: 0x04000961 RID: 2401
		StreamTrailer,
		// Token: 0x04000962 RID: 2402
		StreamHeader,
		// Token: 0x04000963 RID: 2403
		NegotiationInfo,
		// Token: 0x04000964 RID: 2404
		Padding,
		// Token: 0x04000965 RID: 2405
		Stream,
		// Token: 0x04000966 RID: 2406
		Mechlist,
		// Token: 0x04000967 RID: 2407
		MechlistSignature,
		// Token: 0x04000968 RID: 2408
		Target,
		// Token: 0x04000969 RID: 2409
		ChannelBindind,
		// Token: 0x0400096A RID: 2410
		ChangePassResponse,
		// Token: 0x0400096B RID: 2411
		TargetHost,
		// Token: 0x0400096C RID: 2412
		Alert,
		// Token: 0x0400096D RID: 2413
		ApplicationProtocols,
		// Token: 0x0400096E RID: 2414
		SrtpProtectionProfiles,
		// Token: 0x0400096F RID: 2415
		SrtpMasterKeyIdentifier,
		// Token: 0x04000970 RID: 2416
		TokenBinding,
		// Token: 0x04000971 RID: 2417
		PresharedKey,
		// Token: 0x04000972 RID: 2418
		PresharedKeyIdentity,
		// Token: 0x04000973 RID: 2419
		DtlsMtu
	}
}
