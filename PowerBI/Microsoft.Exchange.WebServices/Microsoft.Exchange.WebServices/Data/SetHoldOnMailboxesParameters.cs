using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000285 RID: 645
	public sealed class SetHoldOnMailboxesParameters
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0003EC0D File Offset: 0x0003DC0D
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0003EC15 File Offset: 0x0003DC15
		public HoldAction ActionType { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0003EC1E File Offset: 0x0003DC1E
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x0003EC26 File Offset: 0x0003DC26
		public string HoldId { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0003EC2F File Offset: 0x0003DC2F
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x0003EC37 File Offset: 0x0003DC37
		public string Query { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0003EC40 File Offset: 0x0003DC40
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x0003EC48 File Offset: 0x0003DC48
		public string[] Mailboxes { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0003EC51 File Offset: 0x0003DC51
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x0003EC59 File Offset: 0x0003DC59
		public string Language { get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0003EC62 File Offset: 0x0003DC62
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x0003EC6A File Offset: 0x0003DC6A
		public string InPlaceHoldIdentity { get; set; }
	}
}
