using System;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002D2 RID: 722
	[Flags]
	internal enum UriSyntaxFlags
	{
		// Token: 0x0400098C RID: 2444
		None = 0,
		// Token: 0x0400098D RID: 2445
		MustHaveAuthority = 1,
		// Token: 0x0400098E RID: 2446
		OptionalAuthority = 2,
		// Token: 0x0400098F RID: 2447
		MayHaveUserInfo = 4,
		// Token: 0x04000990 RID: 2448
		MayHavePort = 8,
		// Token: 0x04000991 RID: 2449
		MayHavePath = 16,
		// Token: 0x04000992 RID: 2450
		MayHaveQuery = 32,
		// Token: 0x04000993 RID: 2451
		MayHaveFragment = 64,
		// Token: 0x04000994 RID: 2452
		AllowEmptyHost = 128,
		// Token: 0x04000995 RID: 2453
		AllowUncHost = 256,
		// Token: 0x04000996 RID: 2454
		AllowDnsHost = 512,
		// Token: 0x04000997 RID: 2455
		AllowIPv4Host = 1024,
		// Token: 0x04000998 RID: 2456
		AllowIPv6Host = 2048,
		// Token: 0x04000999 RID: 2457
		AllowAnInternetHost = 3584,
		// Token: 0x0400099A RID: 2458
		AllowAnyOtherHost = 4096,
		// Token: 0x0400099B RID: 2459
		FileLikeUri = 8192,
		// Token: 0x0400099C RID: 2460
		MailToLikeUri = 16384,
		// Token: 0x0400099D RID: 2461
		V1_UnknownUri = 65536,
		// Token: 0x0400099E RID: 2462
		SimpleUserSyntax = 131072,
		// Token: 0x0400099F RID: 2463
		BuiltInSyntax = 262144,
		// Token: 0x040009A0 RID: 2464
		ParserSchemeOnly = 524288,
		// Token: 0x040009A1 RID: 2465
		AllowDOSPath = 1048576,
		// Token: 0x040009A2 RID: 2466
		PathIsRooted = 2097152,
		// Token: 0x040009A3 RID: 2467
		ConvertPathSlashes = 4194304,
		// Token: 0x040009A4 RID: 2468
		CompressPath = 8388608,
		// Token: 0x040009A5 RID: 2469
		CanonicalizeAsFilePath = 16777216,
		// Token: 0x040009A6 RID: 2470
		UnEscapeDotsAndSlashes = 33554432,
		// Token: 0x040009A7 RID: 2471
		AllowIdn = 67108864,
		// Token: 0x040009A8 RID: 2472
		AllowIriParsing = 268435456
	}
}
