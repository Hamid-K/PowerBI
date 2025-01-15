using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal sealed class InternalCatalogException : ReportCatalogException
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00003AEE File Offset: 0x00001CEE
		public InternalCatalogException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsInternalError, ErrorStringsWrapper.rsInternalError, innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003B04 File Offset: 0x00001D04
		public InternalCatalogException(string additionalTraceMessage)
			: this(null, additionalTraceMessage)
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003B0E File Offset: 0x00001D0E
		public InternalCatalogException(Exception innerException, string additionalTraceMessage, params object[] exceptionData)
			: base(ErrorCode.rsInternalError, ErrorStringsWrapper.rsInternalError, innerException, additionalTraceMessage, exceptionData)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003B20 File Offset: 0x00001D20
		public InternalCatalogException(string additionalTraceMessage, params object[] exceptionData)
			: this(null, additionalTraceMessage, exceptionData)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003B2B File Offset: 0x00001D2B
		private InternalCatalogException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
