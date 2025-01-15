using System;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000017 RID: 23
	public sealed class Notification
	{
		// Token: 0x06000056 RID: 86 RVA: 0x0000231D File Offset: 0x0000051D
		public Notification()
		{
			this.Id = Guid.Empty;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002330 File Offset: 0x00000530
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002338 File Offset: 0x00000538
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002341 File Offset: 0x00000541
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002349 File Offset: 0x00000549
		public IssueType IssueType { get; set; }
	}
}
