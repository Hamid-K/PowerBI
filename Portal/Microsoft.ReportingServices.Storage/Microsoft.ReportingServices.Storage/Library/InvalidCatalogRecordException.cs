using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	internal sealed class InvalidCatalogRecordException : ReportCatalogException
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00007F17 File Offset: 0x00006117
		public InvalidCatalogRecordException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsInvalidCatalogRecord, ErrorStrings.rsInvalidCatalogRecord, innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007F30 File Offset: 0x00006130
		public InvalidCatalogRecordException(Exception innerException)
			: base(ErrorCode.rsInvalidCatalogRecord, ErrorStrings.rsInvalidCatalogRecord, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007F49 File Offset: 0x00006149
		public InvalidCatalogRecordException(string additionalTraceMessage)
			: base(ErrorCode.rsInvalidCatalogRecord, ErrorStrings.rsInvalidCatalogRecord, null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidCatalogRecordException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
