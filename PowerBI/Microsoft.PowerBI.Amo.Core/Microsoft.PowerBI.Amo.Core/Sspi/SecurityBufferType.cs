using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x0200010B RID: 267
	internal enum SecurityBufferType
	{
		// Token: 0x04000921 RID: 2337
		Empty,
		// Token: 0x04000922 RID: 2338
		Data,
		// Token: 0x04000923 RID: 2339
		Token,
		// Token: 0x04000924 RID: 2340
		PkgParams,
		// Token: 0x04000925 RID: 2341
		Missing,
		// Token: 0x04000926 RID: 2342
		Extra,
		// Token: 0x04000927 RID: 2343
		StreamTrailer,
		// Token: 0x04000928 RID: 2344
		StreamHeader,
		// Token: 0x04000929 RID: 2345
		NegotiationInfo,
		// Token: 0x0400092A RID: 2346
		Padding,
		// Token: 0x0400092B RID: 2347
		Stream,
		// Token: 0x0400092C RID: 2348
		Mechlist,
		// Token: 0x0400092D RID: 2349
		MechlistSignature,
		// Token: 0x0400092E RID: 2350
		Target,
		// Token: 0x0400092F RID: 2351
		ChannelBindind,
		// Token: 0x04000930 RID: 2352
		ChangePassResponse,
		// Token: 0x04000931 RID: 2353
		TargetHost,
		// Token: 0x04000932 RID: 2354
		Alert,
		// Token: 0x04000933 RID: 2355
		ApplicationProtocols,
		// Token: 0x04000934 RID: 2356
		SrtpProtectionProfiles,
		// Token: 0x04000935 RID: 2357
		SrtpMasterKeyIdentifier,
		// Token: 0x04000936 RID: 2358
		TokenBinding,
		// Token: 0x04000937 RID: 2359
		PresharedKey,
		// Token: 0x04000938 RID: 2360
		PresharedKeyIdentity,
		// Token: 0x04000939 RID: 2361
		DtlsMtu
	}
}
