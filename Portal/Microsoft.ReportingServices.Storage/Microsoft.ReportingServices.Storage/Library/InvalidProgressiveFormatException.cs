using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	internal sealed class InvalidProgressiveFormatException : ReportCatalogException
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00007F62 File Offset: 0x00006162
		public InvalidProgressiveFormatException(string command, Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsInvalidProgressiveFormatError, ErrorStringsWrapper.rsInvalidProgressiveFormatError(command), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidProgressiveFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
