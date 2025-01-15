using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200048B RID: 1163
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class RDEvent : Attribute
	{
		// Token: 0x06002872 RID: 10354 RVA: 0x0007A01D File Offset: 0x0007821D
		public RDEvent(int eventID, TraceEventType eventType)
		{
			this.RDEventID = eventID;
			this.RDEventType = eventType;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x0007A033 File Offset: 0x00078233
		public RDEvent(int eventID, TraceEventType eventType, bool excludeFromE2ETracing)
		{
			this.RDEventID = eventID;
			this.RDEventType = eventType;
			this.ExcludeFromE2ETracing = excludeFromE2ETracing;
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x0007A050 File Offset: 0x00078250
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x0007A058 File Offset: 0x00078258
		public int RDEventID { get; set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x0007A061 File Offset: 0x00078261
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x0007A069 File Offset: 0x00078269
		public TraceEventType RDEventType { get; set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x0007A072 File Offset: 0x00078272
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x0007A07A File Offset: 0x0007827A
		public bool ExcludeFromE2ETracing { get; set; }
	}
}
