using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	internal sealed class InvalidReportParameterException : ReportCatalogException
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00004295 File Offset: 0x00002495
		public InvalidReportParameterException(string parameterName)
			: base(ErrorCode.rsInvalidReportParameter, ErrorStringsWrapper.rsInvalidReportParameter(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000042AC File Offset: 0x000024AC
		private InvalidReportParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
