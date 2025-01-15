using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	internal sealed class SystemResourcePackageItemContentTypeMismatchException : SystemResourcePackageValidationException
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x000107BE File Offset: 0x0000E9BE
		public SystemResourcePackageItemContentTypeMismatchException(string itemName, string contentType)
			: base(ErrorCode.rsSystemResourcePackageCannotValidateItemContentType, string.Format(ErrorStringsWrapper.rsSystemResourcePackageItemContentTypeMismatch(itemName, contentType), itemName, contentType), null, itemName, Array.Empty<object>())
		{
		}
	}
}
