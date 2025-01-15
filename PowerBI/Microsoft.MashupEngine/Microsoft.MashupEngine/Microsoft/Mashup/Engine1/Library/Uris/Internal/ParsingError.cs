using System;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002CA RID: 714
	internal enum ParsingError
	{
		// Token: 0x04000937 RID: 2359
		None,
		// Token: 0x04000938 RID: 2360
		BadFormat,
		// Token: 0x04000939 RID: 2361
		BadScheme,
		// Token: 0x0400093A RID: 2362
		BadAuthority,
		// Token: 0x0400093B RID: 2363
		EmptyUriString,
		// Token: 0x0400093C RID: 2364
		LastRelativeUriOkErrIndex = 4,
		// Token: 0x0400093D RID: 2365
		SchemeLimit,
		// Token: 0x0400093E RID: 2366
		SizeLimit,
		// Token: 0x0400093F RID: 2367
		MustRootedPath,
		// Token: 0x04000940 RID: 2368
		BadHostName,
		// Token: 0x04000941 RID: 2369
		NonEmptyHost,
		// Token: 0x04000942 RID: 2370
		BadPort,
		// Token: 0x04000943 RID: 2371
		BadAuthorityTerminator,
		// Token: 0x04000944 RID: 2372
		CannotCreateRelative
	}
}
