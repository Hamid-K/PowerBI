using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	internal sealed class ItemAlreadyExistsException : ReportCatalogException
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00003F79 File Offset: 0x00002179
		public ItemAlreadyExistsException(string itemPath)
			: base(ErrorCode.rsItemAlreadyExists, ErrorStringsWrapper.rsItemAlreadyExists(itemPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003F90 File Offset: 0x00002190
		private ItemAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
