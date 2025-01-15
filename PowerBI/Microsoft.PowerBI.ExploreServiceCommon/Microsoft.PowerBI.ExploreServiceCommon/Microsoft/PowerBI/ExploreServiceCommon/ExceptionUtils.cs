using System;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x02000022 RID: 34
	public static class ExceptionUtils
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004265 File Offset: 0x00002465
		public static void SetContainsPII(this Exception ex)
		{
			ex.Data[ExceptionUtils.ErrorMessageCouldContainPII] = true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000427D File Offset: 0x0000247D
		public static bool ContainsPII(this Exception ex)
		{
			return ex.Data.Contains(ExceptionUtils.ErrorMessageCouldContainPII) && (bool)ex.Data[ExceptionUtils.ErrorMessageCouldContainPII];
		}

		// Token: 0x040000CF RID: 207
		private static readonly string ErrorMessageCouldContainPII = "ErrorMessageCouldContainPII";
	}
}
