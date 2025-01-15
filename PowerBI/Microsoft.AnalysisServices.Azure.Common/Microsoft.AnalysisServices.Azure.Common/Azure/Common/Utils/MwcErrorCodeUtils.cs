using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000145 RID: 325
	public static class MwcErrorCodeUtils
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x00046EC4 File Offset: 0x000450C4
		public static Exception TranslateAmoConnectionException(ConnectionException ex, string databaseId)
		{
			string mwcErrorCode = ex.GetMwcErrorCode();
			MwcErrorCodeUtils.MwcErrorType mwcErrorType = MwcErrorCodeUtils.GetMwcErrorType(mwcErrorCode);
			if (mwcErrorType == MwcErrorCodeUtils.MwcErrorType.InvalidCapacity)
			{
				return new ProcessDatabaseFailedInvalidCapacityException(databaseId, mwcErrorCode);
			}
			return new ProcessDatabaseFailedException("Processing for {0} failed due to {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00046F0E File Offset: 0x0004510E
		public static bool IsModelOperationUserError(string errorCode)
		{
			return MwcErrorCodeUtils.InvalidCapacityErrorCodes.Contains(errorCode);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00046F1C File Offset: 0x0004511C
		private static string GetMwcErrorCode(this ConnectionException ex)
		{
			string text = null;
			if (ex.Data != null && ex.Data.Contains("Code"))
			{
				text = ex.Data["Code"] as string;
			}
			return text;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00046F5C File Offset: 0x0004515C
		private static MwcErrorCodeUtils.MwcErrorType GetMwcErrorType(string errorCode)
		{
			if (MwcErrorCodeUtils.InvalidCapacityErrorCodes.Contains(errorCode))
			{
				return MwcErrorCodeUtils.MwcErrorType.InvalidCapacity;
			}
			return MwcErrorCodeUtils.MwcErrorType.None;
		}

		// Token: 0x040003EE RID: 1006
		private const string MwcErrorCodeKey = "Code";

		// Token: 0x040003EF RID: 1007
		internal static HashSet<string> InvalidCapacityErrorCodes = new HashSet<string> { "NotFound", "CapacityMissingOrBadState" };

		// Token: 0x02000168 RID: 360
		public enum MwcErrorType
		{
			// Token: 0x0400044D RID: 1101
			None,
			// Token: 0x0400044E RID: 1102
			InvalidCapacity
		}
	}
}
