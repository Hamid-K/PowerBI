using System;
using System.Diagnostics;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007B9 RID: 1977
	public class DrdaEvent
	{
		// Token: 0x040029F6 RID: 10742
		public int eventId;

		// Token: 0x040029F7 RID: 10743
		public EventLogEntryType eventType;

		// Token: 0x040029F8 RID: 10744
		public short category;

		// Token: 0x040029F9 RID: 10745
		public string generalText;

		// Token: 0x040029FA RID: 10746
		public string description;
	}
}
