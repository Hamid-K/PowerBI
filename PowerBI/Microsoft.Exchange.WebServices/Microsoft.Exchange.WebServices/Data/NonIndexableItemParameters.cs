using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200027A RID: 634
	public abstract class NonIndexableItemParameters
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0003DBF2 File Offset: 0x0003CBF2
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0003DBFA File Offset: 0x0003CBFA
		public string[] Mailboxes { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0003DC03 File Offset: 0x0003CC03
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x0003DC0B File Offset: 0x0003CC0B
		public bool SearchArchiveOnly { get; set; }
	}
}
