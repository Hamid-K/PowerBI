using System;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F8 RID: 504
	internal class PKeyAuthConstants
	{
		// Token: 0x040008DB RID: 2267
		public const string DeviceAuthHeaderName = "x-ms-PKeyAuth";

		// Token: 0x040008DC RID: 2268
		public const string DeviceAuthHeaderValue = "1.0";

		// Token: 0x040008DD RID: 2269
		public const string WwwAuthenticateHeader = "WWW-Authenticate";

		// Token: 0x040008DE RID: 2270
		public const string PKeyAuthName = "PKeyAuth";

		// Token: 0x040008DF RID: 2271
		public const string ChallengeResponseContext = "Context";

		// Token: 0x040008E0 RID: 2272
		public const string ChallengeResponseVersion = "Version";

		// Token: 0x040008E1 RID: 2273
		public const string PKeyAuthBypassReponseFormat = "PKeyAuth Context=\"{0}\",Version=\"{1}\"";
	}
}
