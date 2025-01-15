using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	internal sealed class EventLogSourceNotFound : ReportCatalogException
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public EventLogSourceNotFound(string source)
			: base(ErrorCode.rsEventLogSourceNotFound, ErrorStringsWrapper.rsEventLogSourceNotFound(source), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004DF0 File Offset: 0x00002FF0
		private EventLogSourceNotFound(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
