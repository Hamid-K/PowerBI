using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200001A RID: 26
	[DataContract]
	public class OAuthError
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000490E File Offset: 0x00002B0E
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00004916 File Offset: 0x00002B16
		[DataMember(Name = "error", IsRequired = true)]
		public string Error { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000491F File Offset: 0x00002B1F
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004927 File Offset: 0x00002B27
		[DataMember(Name = "error_description", IsRequired = true)]
		public string ErrorDescription { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004930 File Offset: 0x00002B30
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004938 File Offset: 0x00002B38
		[DataMember(Name = "error_uri", IsRequired = false)]
		public string ErrorUri { get; set; }

		// Token: 0x0200002F RID: 47
		public sealed class OAuthErrorCodes
		{
			// Token: 0x04000106 RID: 262
			public const string InvalidRequest = "invalid_request";

			// Token: 0x04000107 RID: 263
			public const string InvalidClient = "invalid_client";

			// Token: 0x04000108 RID: 264
			public const string InvalidGrant = "invalid_grant";

			// Token: 0x04000109 RID: 265
			public const string InvalidScope = "invalid_scope";

			// Token: 0x0400010A RID: 266
			public const string UnauthorizedClient = "unauthorized_client";

			// Token: 0x0400010B RID: 267
			public const string UnsupportedGrantType = "unsupported_grant_type";
		}
	}
}
