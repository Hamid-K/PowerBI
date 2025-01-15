using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	internal sealed class SubscriptionNotFoundException : ReportCatalogException
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000480E File Offset: 0x00002A0E
		public SubscriptionNotFoundException(string idOrData)
			: base(ErrorCode.rsSubscriptionNotFound, ErrorStringsWrapper.rsSubscriptionNotFound(idOrData), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004825 File Offset: 0x00002A25
		private SubscriptionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
