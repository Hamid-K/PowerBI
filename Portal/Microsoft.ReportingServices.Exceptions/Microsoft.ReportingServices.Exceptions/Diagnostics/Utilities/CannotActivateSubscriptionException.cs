using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	internal sealed class CannotActivateSubscriptionException : ReportCatalogException
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x000047EE File Offset: 0x000029EE
		public CannotActivateSubscriptionException()
			: base(ErrorCode.rsCannotActivateSubscription, ErrorStringsWrapper.rsCannotActivateSubscription, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004804 File Offset: 0x00002A04
		private CannotActivateSubscriptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
