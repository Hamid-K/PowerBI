using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	internal sealed class NotEnabledException : ReportCatalogException
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00003B9B File Offset: 0x00001D9B
		public NotEnabledException()
			: base(ErrorCode.rsNotEnabled, ErrorStringsWrapper.rsNotEnabled, null, null, TraceLevel.Verbose, Array.Empty<object>())
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003BB2 File Offset: 0x00001DB2
		private NotEnabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
