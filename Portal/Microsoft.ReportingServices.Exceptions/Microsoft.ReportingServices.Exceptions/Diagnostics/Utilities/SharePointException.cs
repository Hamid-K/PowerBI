using System;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	internal sealed class SharePointException : ReportCatalogException
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00005034 File Offset: 0x00003234
		public SharePointException(Exception innerException)
			: base(ErrorCode.rsSharePointError, SharePointException.GetExceptionMessage(innerException), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000504E File Offset: 0x0000324E
		private SharePointException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00005058 File Offset: 0x00003258
		private static string GetExceptionMessage(Exception innerException)
		{
			if (innerException is SqlException)
			{
				return ErrorStringsWrapper.rsSharePointContentDBAccessError;
			}
			return ErrorStringsWrapper.rsSharePointError;
		}
	}
}
