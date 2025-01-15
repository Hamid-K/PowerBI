using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	internal sealed class SystemResourcePackageRequiredItemMissingException : SystemResourcePackageValidationException
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00010854 File Offset: 0x0000EA54
		public SystemResourcePackageRequiredItemMissingException(string itemName)
			: base(ErrorCode.rsSystemResourcePackageRequiredItemMissing, string.Format(ErrorStringsWrapper.rsSystemResourcePackageRequiredItemMissing(itemName), itemName), null, itemName, Array.Empty<object>())
		{
		}
	}
}
