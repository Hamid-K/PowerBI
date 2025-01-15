using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000524 RID: 1316
	public static class HttpHeaderValueConstants
	{
		// Token: 0x04000E3C RID: 3644
		public const string Public = "public";

		// Token: 0x04000E3D RID: 3645
		public const string MaxAge = "max-age";

		// Token: 0x04000E3E RID: 3646
		public const string NoSniff = "nosniff";

		// Token: 0x04000E3F RID: 3647
		public const string Deny = "deny";

		// Token: 0x04000E40 RID: 3648
		public const string SameOrigin = "SAMEORIGIN";

		// Token: 0x04000E41 RID: 3649
		public const string AllowFromUriTemplate = "ALLOW-FROM {0}";

		// Token: 0x04000E42 RID: 3650
		public const string NoCache = "no-cache";

		// Token: 0x04000E43 RID: 3651
		public const string NoStore = "no-store";

		// Token: 0x04000E44 RID: 3652
		public const string MustRevalidate = "must-revalidate";

		// Token: 0x04000E45 RID: 3653
		public const string GZipEncoding = "gzip";

		// Token: 0x04000E46 RID: 3654
		public const string AcceptJson = "application/json";

		// Token: 0x04000E47 RID: 3655
		private const string AadConsistencyHeaderTemplate = "Organization;ScenarioId={0}";

		// Token: 0x04000E48 RID: 3656
		public const string HstsHeaderValue = "max-age=31536000; includeSubDomains";

		// Token: 0x04000E49 RID: 3657
		public const long AccessControlMaxAgeNumericValue = 300L;

		// Token: 0x04000E4A RID: 3658
		public static readonly string AccessControlMaxAgeValue = 300L.ToString(CultureInfo.InvariantCulture);
	}
}
