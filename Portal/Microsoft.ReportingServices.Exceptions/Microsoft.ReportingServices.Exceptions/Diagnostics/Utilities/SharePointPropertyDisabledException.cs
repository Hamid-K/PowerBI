using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	internal sealed class SharePointPropertyDisabledException : ReportCatalogException
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00005143 File Offset: 0x00003343
		public SharePointPropertyDisabledException()
			: base(ErrorCode.rsPropertyDisabled, ErrorStringsWrapper.rsPropertyDisabled, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000515C File Offset: 0x0000335C
		private SharePointPropertyDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
