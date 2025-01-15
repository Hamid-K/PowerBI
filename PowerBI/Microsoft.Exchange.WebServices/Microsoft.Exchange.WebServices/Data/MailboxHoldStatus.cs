using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000272 RID: 626
	public sealed class MailboxHoldStatus
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x0003D527 File Offset: 0x0003C527
		public MailboxHoldStatus()
		{
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0003D52F File Offset: 0x0003C52F
		public MailboxHoldStatus(string mailbox, HoldStatus status, string additionalInfo)
		{
			this.Mailbox = mailbox;
			this.Status = status;
			this.AdditionalInfo = additionalInfo;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0003D54C File Offset: 0x0003C54C
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x0003D554 File Offset: 0x0003C554
		public string Mailbox { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0003D55D File Offset: 0x0003C55D
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x0003D565 File Offset: 0x0003C565
		public HoldStatus Status { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0003D56E File Offset: 0x0003C56E
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x0003D576 File Offset: 0x0003C576
		public string AdditionalInfo { get; set; }
	}
}
