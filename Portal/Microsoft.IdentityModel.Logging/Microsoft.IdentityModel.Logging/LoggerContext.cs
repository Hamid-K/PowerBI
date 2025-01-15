using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x02000009 RID: 9
	public class LoggerContext
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000284B File Offset: 0x00000A4B
		public LoggerContext()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002874 File Offset: 0x00000A74
		public LoggerContext(Guid activityId)
		{
			this.ActivityId = activityId;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000028A4 File Offset: 0x00000AA4
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000028AC File Offset: 0x00000AAC
		public Guid ActivityId { get; set; } = Guid.Empty;

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028B5 File Offset: 0x00000AB5
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000028BD File Offset: 0x00000ABD
		public bool CaptureLogs { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000028C6 File Offset: 0x00000AC6
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000028CE File Offset: 0x00000ACE
		public virtual string DebugId { get; set; } = string.Empty;

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000028D7 File Offset: 0x00000AD7
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000028DF File Offset: 0x00000ADF
		public ICollection<string> Logs { get; private set; } = new Collection<string>();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000028F0 File Offset: 0x00000AF0
		public IDictionary<string, object> PropertyBag { get; set; }
	}
}
