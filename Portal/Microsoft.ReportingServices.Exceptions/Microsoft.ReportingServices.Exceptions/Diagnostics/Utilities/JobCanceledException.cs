using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	internal sealed class JobCanceledException : ReportCatalogException
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00004D4F File Offset: 0x00002F4F
		public JobCanceledException(Exception innerException)
			: base(ErrorCode.rsJobWasCanceled, ErrorStringsWrapper.rsJobWasCanceled, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00004D65 File Offset: 0x00002F65
		private JobCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
