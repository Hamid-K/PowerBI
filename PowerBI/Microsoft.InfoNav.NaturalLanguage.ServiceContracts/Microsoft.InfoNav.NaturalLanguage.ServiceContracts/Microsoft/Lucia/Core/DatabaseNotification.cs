using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200004A RID: 74
	public sealed class DatabaseNotification
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003F99 File Offset: 0x00002199
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003FA1 File Offset: 0x000021A1
		public string DatabaseName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003FAA File Offset: 0x000021AA
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00003FB2 File Offset: 0x000021B2
		public ChangeType ChangeType { get; set; }
	}
}
