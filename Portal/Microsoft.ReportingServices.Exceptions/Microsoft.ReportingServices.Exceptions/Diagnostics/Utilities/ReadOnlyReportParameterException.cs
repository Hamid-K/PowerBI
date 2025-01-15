using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal sealed class ReadOnlyReportParameterException : ReportCatalogException
	{
		// Token: 0x06000196 RID: 406 RVA: 0x000041A5 File Offset: 0x000023A5
		public ReadOnlyReportParameterException(string parameterName)
			: base(ErrorCode.rsReadOnlyReportParameter, ErrorStringsWrapper.rsReadOnlyReportParameter(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000041BC File Offset: 0x000023BC
		private ReadOnlyReportParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
