using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	internal sealed class NativeModePropertyDisabledException : ReportCatalogException
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00005166 File Offset: 0x00003366
		public NativeModePropertyDisabledException()
			: base(ErrorCode.rsPropertyDisabledNativeMode, ErrorStringsWrapper.rsPropertyDisabledNativeMode, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000517F File Offset: 0x0000337F
		private NativeModePropertyDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
