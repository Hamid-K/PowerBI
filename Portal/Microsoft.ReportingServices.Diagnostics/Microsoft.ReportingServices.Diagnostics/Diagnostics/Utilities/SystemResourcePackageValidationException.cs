using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	internal abstract class SystemResourcePackageValidationException : SystemResourcePackageException
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00010874 File Offset: 0x0000EA74
		public SystemResourcePackageValidationException(ErrorCode errorCode, string localizedMessage, Exception innerException, string additionalTraceMessage, params object[] exceptionData)
			: base(errorCode, localizedMessage, innerException, additionalTraceMessage, exceptionData)
		{
		}
	}
}
