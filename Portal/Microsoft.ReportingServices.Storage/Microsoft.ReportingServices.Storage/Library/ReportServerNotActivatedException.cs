using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	internal sealed class ReportServerNotActivatedException : ReportCatalogException
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00007E3F File Offset: 0x0000603F
		public ReportServerNotActivatedException(Exception innerException)
			: base(ErrorCode.rsReportServerNotActivated, ErrorStrings.rsReportServerNotActivated, innerException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.NotActivated, new object[] { RSEventLog.Current.SourceName });
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007D2D File Offset: 0x00005F2D
		private ReportServerNotActivatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
