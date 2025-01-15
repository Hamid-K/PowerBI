using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000102 RID: 258
	public static class ReasonCodes
	{
		// Token: 0x0400022E RID: 558
		public const string DataSourceError = "DataSource.Error";

		// Token: 0x0400022F RID: 559
		public const string DataSourceNotFound = "DataSource.NotFound";

		// Token: 0x04000230 RID: 560
		public const string DataSourceMissingClientLibrary = "DataSource.MissingClientLibrary";

		// Token: 0x04000231 RID: 561
		public const string DataSourceChanged = "DataSource.Changed";

		// Token: 0x04000232 RID: 562
		public const string DataSourceCapacityExceeded = "DataSource.CapacityExceeded";

		// Token: 0x04000233 RID: 563
		public const string DataFormatError = "DataFormat.Error";

		// Token: 0x04000234 RID: 564
		public const string ExpressionError = "Expression.Error";

		// Token: 0x04000235 RID: 565
		public const string MissingCredential = "CredentialMissing";

		// Token: 0x04000236 RID: 566
		public const string InvalidCredential = "CredentialInvalid";

		// Token: 0x04000237 RID: 567
		public const string EncryptedConnectionFailed = "EncryptedConnectionFailed";

		// Token: 0x04000238 RID: 568
		public const string PrincipleNameMismatch = "PrincipleNameMismatch";

		// Token: 0x04000239 RID: 569
		public const string AccessDenied = "AccessUnauthorized";

		// Token: 0x0400023A RID: 570
		public const string AccessForbidden = "AccessForbidden";

		// Token: 0x0400023B RID: 571
		public const string CredentialNeedsRefresh = "CredentialNeedsRefresh";

		// Token: 0x0400023C RID: 572
		public const string ActionNotAllowed = "ActionNotAllowed";

		// Token: 0x0400023D RID: 573
		public const string ParameterError = "Parameter.Error";
	}
}
