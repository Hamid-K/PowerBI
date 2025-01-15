using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public abstract class ReportCatalogException : RSException
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00003AA6 File Offset: 0x00001CA6
		public ReportCatalogException(ErrorCode errorCode, string localizedMessage, Exception innerException, string additionalTraceMessage, params object[] exceptionData)
			: base(errorCode, localizedMessage, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, additionalTraceMessage, exceptionData)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003AC4 File Offset: 0x00001CC4
		protected ReportCatalogException(ErrorCode errorCode, string localizedMessage, Exception innerException, string additionalTraceMessage, TraceLevel traceLevel, params object[] exceptionData)
			: base(errorCode, localizedMessage, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, additionalTraceMessage, traceLevel, exceptionData)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003AE4 File Offset: 0x00001CE4
		protected ReportCatalogException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
