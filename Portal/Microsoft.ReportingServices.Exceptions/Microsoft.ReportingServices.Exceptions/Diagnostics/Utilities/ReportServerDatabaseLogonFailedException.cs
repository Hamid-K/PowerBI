using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	internal sealed class ReportServerDatabaseLogonFailedException : ReportCatalogException
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00004D6F File Offset: 0x00002F6F
		public ReportServerDatabaseLogonFailedException(Exception innerException)
			: base(ErrorCode.rsReportServerDatabaseLogonFailed, ErrorStringsWrapper.rsReportServerDatabaseLogonFailed, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00004D85 File Offset: 0x00002F85
		private ReportServerDatabaseLogonFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
