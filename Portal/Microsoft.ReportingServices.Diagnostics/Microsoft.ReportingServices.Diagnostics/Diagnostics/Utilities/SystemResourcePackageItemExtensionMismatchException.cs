using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	internal sealed class SystemResourcePackageItemExtensionMismatchException : SystemResourcePackageValidationException
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public SystemResourcePackageItemExtensionMismatchException(string itemName, string extension)
			: base(ErrorCode.rsSystemResourcePackageCannotValidateItemExtension, string.Format(ErrorStringsWrapper.rsSystemResourcePackageItemExtensionMismatch(itemName, extension), itemName, extension), null, itemName, Array.Empty<object>())
		{
		}
	}
}
