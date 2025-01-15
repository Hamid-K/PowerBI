using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	internal sealed class InvalidSubscriptionException : ReportCatalogException
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00007E74 File Offset: 0x00006074
		public InvalidSubscriptionException(Guid ID, Exception innerException)
			: base(ErrorCode.rsInvalidSubscription, ErrorStringsWrapper.rsInvalidSubscription(ID.ToString().ToUpper(CultureInfo.InvariantCulture)), innerException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.InternalError, new object[] { this.Message });
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidSubscriptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
