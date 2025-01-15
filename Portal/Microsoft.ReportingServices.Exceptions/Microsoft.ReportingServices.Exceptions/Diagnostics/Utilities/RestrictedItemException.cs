using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	internal sealed class RestrictedItemException : ReportCatalogException
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00003E9E File Offset: 0x0000209E
		public RestrictedItemException(string itemPath)
			: base(ErrorCode.rsItemNotFound, ErrorStringsWrapper.rsRestrictedItem(itemPath), null, null, Array.Empty<object>())
		{
		}
	}
}
