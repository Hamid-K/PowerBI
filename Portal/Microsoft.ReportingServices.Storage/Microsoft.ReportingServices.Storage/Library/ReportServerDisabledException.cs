using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	internal sealed class ReportServerDisabledException : ReportCatalogException
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00007DA1 File Offset: 0x00005FA1
		public ReportServerDisabledException(Exception innerException)
			: base(ErrorCode.rsReportServerDisabled, ErrorStrings.rsReportServerDisabled, innerException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.IsDisabled, Array.Empty<object>());
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007D2D File Offset: 0x00005F2D
		private ReportServerDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
