using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000009 RID: 9
	public static class AuthenticationKind
	{
		// Token: 0x04000010 RID: 16
		public const string Windows = "Windows";

		// Token: 0x04000011 RID: 17
		public const string UsernamePassword = "UsernamePassword";

		// Token: 0x04000012 RID: 18
		public const string Anonymous = "Anonymous";

		// Token: 0x04000013 RID: 19
		public const string Key = "Key";

		// Token: 0x04000014 RID: 20
		public const string OAuth2 = "OAuth2";

		// Token: 0x04000015 RID: 21
		public const string Aad = "AAD";

		// Token: 0x04000016 RID: 22
		public const string WebApi = "WebApi";

		// Token: 0x04000017 RID: 23
		public const string MacSandboxFileAccess = "MacSandboxFileAccess";

		// Token: 0x04000018 RID: 24
		internal static string[] KnownAuthKinds = new string[] { "Windows", "UsernamePassword", "Anonymous", "Key", "OAuth2", "WebApi", "AAD" };
	}
}
