using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000276 RID: 630
	public sealed class PreviewItemMailbox
	{
		// Token: 0x06001621 RID: 5665 RVA: 0x0003D88D File Offset: 0x0003C88D
		public PreviewItemMailbox()
		{
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0003D895 File Offset: 0x0003C895
		public PreviewItemMailbox(string mailboxId, string primarySmtpAddress)
		{
			this.MailboxId = mailboxId;
			this.PrimarySmtpAddress = primarySmtpAddress;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0003D8AB File Offset: 0x0003C8AB
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x0003D8B3 File Offset: 0x0003C8B3
		public string MailboxId { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0003D8BC File Offset: 0x0003C8BC
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x0003D8C4 File Offset: 0x0003C8C4
		public string PrimarySmtpAddress { get; set; }
	}
}
