using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	internal sealed class ReportSerializationException : ReportCatalogException
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00004794 File Offset: 0x00002994
		public ReportSerializationException()
			: this(null)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000479D File Offset: 0x0000299D
		public ReportSerializationException(Exception innerException)
			: base(ErrorCode.rsReportSerializationError, ErrorStringsWrapper.rsReportSerializationError, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000047B6 File Offset: 0x000029B6
		private ReportSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
