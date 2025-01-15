using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	internal sealed class InvalidSessionCatalogItemsException : ReportCatalogException
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00005A38 File Offset: 0x00003C38
		public InvalidSessionCatalogItemsException(Exception innerException, string errorString)
			: base(ErrorCode.rsInvalidSessionCatalogItems, ErrorStringsWrapper.rsInvalidSessionCatalogItems(errorString), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00005A52 File Offset: 0x00003C52
		private InvalidSessionCatalogItemsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
