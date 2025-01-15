using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	internal sealed class ReportParameterValueNotSetException : ReportCatalogException
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000422F File Offset: 0x0000242F
		public ReportParameterValueNotSetException(string parameterName)
			: base(ErrorCode.rsReportParameterValueNotSet, ErrorStringsWrapper.rsReportParameterValueNotSet(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004246 File Offset: 0x00002446
		private ReportParameterValueNotSetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
