using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	internal sealed class FailedToExportSymmetricKeyException : ReportCatalogException
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00004EFA File Offset: 0x000030FA
		public FailedToExportSymmetricKeyException()
			: base(ErrorCode.rsFailedToExportSymmetricKey, ErrorStringsWrapper.rsFailedToExportSymmetricKey, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00004F10 File Offset: 0x00003110
		private FailedToExportSymmetricKeyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
