using System;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x0200012B RID: 299
	internal static class MsoIdCrlConsts
	{
		// Token: 0x04000A73 RID: 2675
		public const int IDCRL_API_VERSION_1 = 1;

		// Token: 0x04000A74 RID: 2676
		public const int IDCRL_API_VERSION_CURRENT = 1;

		// Token: 0x04000A75 RID: 2677
		public const string PPCRL_CREDTYPE_PASSWORD = "ps:password";

		// Token: 0x04000A76 RID: 2678
		public const string PPCRL_CREDTYPE_MEMBERNAMEONLY = "ps:membernameonly";

		// Token: 0x04000A77 RID: 2679
		public const string PPCRL_CREDTYPE_ACTIVEUSER = "ps:active";

		// Token: 0x04000A78 RID: 2680
		public const string PPCRL_CREDTYPE_VIRUTUALAPPPrefix = "ps:virtualapp=";

		// Token: 0x04000A79 RID: 2681
		public const string PPCRL_CREDTYPE_PIN = "ps:pin";

		// Token: 0x04000A7A RID: 2682
		public const string PPCRL_CREDTYPE_EID = "ps:eid";

		// Token: 0x04000A7B RID: 2683
		internal const int S_OK = 0;

		// Token: 0x04000A7C RID: 2684
		internal const int S_FAILED = -1;

		// Token: 0x04000A7D RID: 2685
		internal const int AUTHENTICATED_USING_PASSWORD = 296963;
	}
}
