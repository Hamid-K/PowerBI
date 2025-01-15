using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000028 RID: 40
	public static class MashupCredentialExceptionReason
	{
		// Token: 0x0400012C RID: 300
		public static string MissingCredential = "CredentialMissing";

		// Token: 0x0400012D RID: 301
		public static string InvalidCredential = "CredentialInvalid";

		// Token: 0x0400012E RID: 302
		public static string EncryptedConnectionFailed = "EncryptedConnectionFailed";

		// Token: 0x0400012F RID: 303
		public static string PrincipleNameMismatch = "PrincipleNameMismatch";

		// Token: 0x04000130 RID: 304
		public static string AccessDenied = "AccessUnauthorized";

		// Token: 0x04000131 RID: 305
		public static string AccessForbidden = "AccessForbidden";

		// Token: 0x04000132 RID: 306
		public static string CredentialNeedsRefresh = "CredentialNeedsRefresh";

		// Token: 0x04000133 RID: 307
		public static string ActionNotAllowed = "ActionNotAllowed";
	}
}
