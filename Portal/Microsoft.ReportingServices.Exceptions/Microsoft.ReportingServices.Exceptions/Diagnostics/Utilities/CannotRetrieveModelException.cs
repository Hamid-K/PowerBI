using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal sealed class CannotRetrieveModelException : ReportCatalogException
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000467C File Offset: 0x0000287C
		public static bool IsCannotRetrieveModelErrorCode(ErrorCode errorCode)
		{
			return errorCode == ErrorCode.rsCannotRetrieveModel || errorCode == ErrorCode.rsUnsupportedMetadataVersionRequested || errorCode == ErrorCode.rsInvalidPerspectiveAndVersion;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004699 File Offset: 0x00002899
		public CannotRetrieveModelException(ErrorCode errorCode, string itemName, Exception innerException)
			: base(errorCode, ErrorStringsWrapper.rsCannotRetrieveModel(itemName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000046AF File Offset: 0x000028AF
		public CannotRetrieveModelException(string itemName, Exception innerException)
			: base(ErrorCode.rsCannotRetrieveModel, ErrorStringsWrapper.rsCannotRetrieveModel(itemName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000046C9 File Offset: 0x000028C9
		private CannotRetrieveModelException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
