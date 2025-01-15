using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	internal sealed class SPSiteNotFoundException : ReportCatalogException
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00003F37 File Offset: 0x00002137
		public SPSiteNotFoundException(string siteId)
			: base(ErrorCode.rsSPSiteNotFound, ErrorStringsWrapper.rsSPSiteNotFound(siteId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003F4E File Offset: 0x0000214E
		private SPSiteNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
