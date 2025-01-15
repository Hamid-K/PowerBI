using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal sealed class ReportServerDatabaseUnavailableException : ReportCatalogException
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00007D37 File Offset: 0x00005F37
		public ReportServerDatabaseUnavailableException(Exception innerSqlException)
			: base(ErrorCode.rsReportServerDatabaseUnavailable, ErrorStrings.rsReportServerDatabaseUnavailable, innerSqlException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.CouldNotCommunicateToCatalog, new object[] { RSEventLog.Current.SourceName });
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007D6C File Offset: 0x00005F6C
		public ReportServerDatabaseUnavailableException(string additionalTraceMessage)
			: base(ErrorCode.rsReportServerDatabaseUnavailable, ErrorStrings.rsReportServerDatabaseUnavailable, null, additionalTraceMessage, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.CouldNotCommunicateToCatalog, new object[] { RSEventLog.Current.SourceName });
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007D2D File Offset: 0x00005F2D
		private ReportServerDatabaseUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
