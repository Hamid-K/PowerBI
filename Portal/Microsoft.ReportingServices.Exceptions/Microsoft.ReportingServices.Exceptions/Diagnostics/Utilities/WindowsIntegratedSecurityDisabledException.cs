using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	internal sealed class WindowsIntegratedSecurityDisabledException : ReportCatalogException
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00004514 File Offset: 0x00002714
		public WindowsIntegratedSecurityDisabledException()
			: base(ErrorCode.rsWindowsIntegratedSecurityDisabled, ErrorStringsWrapper.rsWindowsIntegratedSecurityDisabled, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000452A File Offset: 0x0000272A
		private WindowsIntegratedSecurityDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
