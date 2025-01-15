using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal sealed class UserCannotOwnSubscriptionException : ReportCatalogException
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00007EFE File Offset: 0x000060FE
		public UserCannotOwnSubscriptionException(string additionalTraceMessage)
			: base(ErrorCode.rsUserCannotOwnSubscription, ErrorStrings.rsUserCannotOwnSubscription, null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007D2D File Offset: 0x00005F2D
		private UserCannotOwnSubscriptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
