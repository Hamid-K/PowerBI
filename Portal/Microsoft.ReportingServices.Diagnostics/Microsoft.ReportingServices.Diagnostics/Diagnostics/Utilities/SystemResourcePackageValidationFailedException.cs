using System;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	internal sealed class SystemResourcePackageValidationFailedException : SystemResourcePackageValidationException
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00010883 File Offset: 0x0000EA83
		public SystemResourcePackageValidationFailedException()
			: base(ErrorCode.rsSystemResourcePackageValidationFailed, ErrorStrings.rsSystemResourcePackageValidationFailed, null, null, Array.Empty<object>())
		{
		}
	}
}
