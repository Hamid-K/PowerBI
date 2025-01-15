using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	internal sealed class UnknownReportParameterException : ReportCatalogException
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000041EA File Offset: 0x000023EA
		public UnknownReportParameterException(string parameterName)
			: base(ErrorCode.rsUnknownReportParameter, ErrorStringsWrapper.rsUnknownReportParameter(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004201 File Offset: 0x00002401
		private UnknownReportParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
