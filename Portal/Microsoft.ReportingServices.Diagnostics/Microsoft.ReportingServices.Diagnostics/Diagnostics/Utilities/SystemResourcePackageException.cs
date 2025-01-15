using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	internal abstract class SystemResourcePackageException : RSException
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public SystemResourcePackageException(ErrorCode errorCode, string localizedMessage, Exception innerException, string additionalTraceMessage, params object[] exceptionData)
			: base(errorCode, localizedMessage, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, additionalTraceMessage, exceptionData)
		{
		}
	}
}
