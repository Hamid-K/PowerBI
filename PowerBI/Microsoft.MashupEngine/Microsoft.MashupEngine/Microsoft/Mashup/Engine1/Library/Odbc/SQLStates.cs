using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200067E RID: 1662
	internal static class SQLStates
	{
		// Token: 0x04001768 RID: 5992
		public const string StringDataRightTruncated = "01004";

		// Token: 0x04001769 RID: 5993
		public const string FractionalDataTruncated = "01S07";

		// Token: 0x0400176A RID: 5994
		public const string ConnectionNotOpen = "08003";

		// Token: 0x0400176B RID: 5995
		public const string CommunicationLinkFailure = "08S01";

		// Token: 0x0400176C RID: 5996
		public const string NumericValueOutOfRange = "22003";

		// Token: 0x0400176D RID: 5997
		public const string MemoryAllocationError = "HY001";

		// Token: 0x0400176E RID: 5998
		public const string MemoryManagementError = "HY013";

		// Token: 0x0400176F RID: 5999
		public const string InvalidStringOrBufferLength = "HY090";

		// Token: 0x04001770 RID: 6000
		public const string InformationTypeOutOfRange = "HY096";

		// Token: 0x04001771 RID: 6001
		public const string OptionalFieldNotImplemented = "HYC00";

		// Token: 0x04001772 RID: 6002
		public const string ConnectionTimeoutExpired = "HYT01";

		// Token: 0x04001773 RID: 6003
		public const string FunctionNotSupported = "IM001";

		// Token: 0x04001774 RID: 6004
		public const string GeneralWarning = "01000";

		// Token: 0x04001775 RID: 6005
		public const string FeatureNotImplemented = "S1C00";

		// Token: 0x04001776 RID: 6006
		public static readonly HashSet<string> NonTransientErrors = new HashSet<string> { "HY096", "HYC00", "IM001", "S1C00", "01000" };
	}
}
