using System;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	internal sealed class SystemResourcePackageMetadataInvalidException : SystemResourcePackageException
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00010802 File Offset: 0x0000EA02
		public SystemResourcePackageMetadataInvalidException(Exception e)
			: base(ErrorCode.rsSystemResourcePackageMetadataValidationFailure, ErrorStrings.rsSystemResourcePackageMetadataInvalid, e, null, Array.Empty<object>())
		{
		}
	}
}
