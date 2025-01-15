using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	internal sealed class ClosingRegisteredStreamException : ReportCatalogException
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00005A15 File Offset: 0x00003C15
		public ClosingRegisteredStreamException(Exception innerException)
			: base(ErrorCode.rsClosingRegisteredStreamException, ErrorStringsWrapper.rsClosingRegisteredStreamException, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00005A2E File Offset: 0x00003C2E
		private ClosingRegisteredStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
