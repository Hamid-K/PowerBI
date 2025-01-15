using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public sealed class ReportParameterTypeMismatchException : ReportCatalogException
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00004274 File Offset: 0x00002474
		public ReportParameterTypeMismatchException(string parameterName)
			: base(ErrorCode.rsReportParameterTypeMismatch, ErrorStringsWrapper.rsReportParameterTypeMismatch(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000428B File Offset: 0x0000248B
		private ReportParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
