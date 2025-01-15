using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal sealed class SystemResourcePackageReferencedItemMissingException : SystemResourcePackageException
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00010834 File Offset: 0x0000EA34
		public SystemResourcePackageReferencedItemMissingException(string itemName)
			: base(ErrorCode.rsSystemResourcePackageReferencedItemMissing, string.Format(ErrorStringsWrapper.rsSystemResourcePackageReferencedItemMissing(itemName), itemName), null, itemName, Array.Empty<object>())
		{
		}
	}
}
