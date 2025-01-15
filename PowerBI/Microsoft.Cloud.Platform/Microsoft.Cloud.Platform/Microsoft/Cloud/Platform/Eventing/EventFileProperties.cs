using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000385 RID: 901
	public class EventFileProperties
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0006A78A File Offset: 0x0006898A
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0006A792 File Offset: 0x00068992
		public DateTime CreationTime { get; private set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0006A79B File Offset: 0x0006899B
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x0006A7A3 File Offset: 0x000689A3
		public DateTime LastModifiedTime { get; private set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0006A7AC File Offset: 0x000689AC
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x0006A7B4 File Offset: 0x000689B4
		public ElementId ElementId { get; private set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0006A7BD File Offset: 0x000689BD
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0006A7C5 File Offset: 0x000689C5
		public int Counter { get; private set; }

		// Token: 0x06001BED RID: 7149 RVA: 0x0006A7CE File Offset: 0x000689CE
		public EventFileProperties(DateTime creation, DateTime modified, ElementId elementId, int counter)
		{
			this.CreationTime = creation;
			this.LastModifiedTime = modified;
			this.ElementId = elementId;
			this.Counter = counter;
		}
	}
}
