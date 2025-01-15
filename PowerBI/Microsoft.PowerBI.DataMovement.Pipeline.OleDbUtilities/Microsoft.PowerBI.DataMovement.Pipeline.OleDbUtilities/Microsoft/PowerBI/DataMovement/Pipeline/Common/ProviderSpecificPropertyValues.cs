using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000009 RID: 9
	internal static class ProviderSpecificPropertyValues
	{
		// Token: 0x0200000D RID: 13
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class MSOLAP
		{
			// Token: 0x04000029 RID: 41
			internal const int MDXC_DEFAULT = 0;

			// Token: 0x0400002A RID: 42
			internal const int MDXC_70 = 1;

			// Token: 0x0400002B RID: 43
			internal const int MDXC_7X = 2;

			// Token: 0x0400002C RID: 44
			internal const int SO_DEFAULT = 0;

			// Token: 0x0400002D RID: 45
			internal const int SO_ALLOW_ALL = 1;

			// Token: 0x0400002E RID: 46
			internal const int SO_ALLOW_SAFE = 2;

			// Token: 0x0400002F RID: 47
			internal const int SO_ALLOW_NONE = 3;

			// Token: 0x04000030 RID: 48
			internal const int SQ_UNRESTRICTED = 0;

			// Token: 0x04000031 RID: 49
			internal const int SQ_RESTRICTED = 1;

			// Token: 0x04000032 RID: 50
			internal const int SQ_CALC_RESTRICTED2 = 2;

			// Token: 0x04000033 RID: 51
			internal const int UIL_ISOLATED = 1;

			// Token: 0x04000034 RID: 52
			internal const int UIL_NOT_ISOLATED = 2;

			// Token: 0x04000035 RID: 53
			internal const string DBMSName_SQLServerVNextAnalysisService = "Analysis Server";

			// Token: 0x04000036 RID: 54
			internal const string DBMSVer_SQLServerVNextAnalysisService = "16.0.00.00";
		}
	}
}
