using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	internal sealed class ReportServerAppDomainManagerException : ReportCatalogException
	{
		// Token: 0x06000280 RID: 640 RVA: 0x000051AC File Offset: 0x000033AC
		public ReportServerAppDomainManagerException(Exception innerException, string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsAppDomainManagerError, ErrorStringsWrapper.rsAppDomainManagerError(appDomain), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000051C6 File Offset: 0x000033C6
		public ReportServerAppDomainManagerException(string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsAppDomainManagerError, ErrorStringsWrapper.rsAppDomainManagerError(appDomain), null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000051E0 File Offset: 0x000033E0
		private ReportServerAppDomainManagerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
