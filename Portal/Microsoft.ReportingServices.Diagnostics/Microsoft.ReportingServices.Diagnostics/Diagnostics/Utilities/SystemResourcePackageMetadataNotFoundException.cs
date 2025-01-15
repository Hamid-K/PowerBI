using System;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	internal sealed class SystemResourcePackageMetadataNotFoundException : SystemResourcePackageException
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001081B File Offset: 0x0000EA1B
		public SystemResourcePackageMetadataNotFoundException()
			: base(ErrorCode.rsSystemResourcePackageMetadataNotFound, ErrorStrings.rsSystemResourcePackageMetadataNotFound, null, null, Array.Empty<object>())
		{
		}
	}
}
