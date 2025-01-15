using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	internal sealed class CachingNotEnabledException : ReportCatalogException
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00004121 File Offset: 0x00002321
		public CachingNotEnabledException(string itemPath)
			: base(ErrorCode.rsCachingNotEnabled, ErrorStringsWrapper.rsCachingNotEnabled(itemPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000413B File Offset: 0x0000233B
		private CachingNotEnabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
