using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	internal sealed class RequestEncodingFormatException : ReportCatalogException
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00005A5C File Offset: 0x00003C5C
		public RequestEncodingFormatException(Exception innerException)
			: base(ErrorCode.rsRequestEncodingFormatException, ErrorStringsWrapper.rsRequestEncodingFormatException, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00005A75 File Offset: 0x00003C75
		private RequestEncodingFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
