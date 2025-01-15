using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal sealed class CatalogItemContentInvalidException : ReportCatalogException
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00003EB5 File Offset: 0x000020B5
		public CatalogItemContentInvalidException(string itemPath)
			: base(ErrorCode.rsItemContentInvalid, ErrorStringsWrapper.rsItemContentInvalid(itemPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003ECC File Offset: 0x000020CC
		private CatalogItemContentInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
