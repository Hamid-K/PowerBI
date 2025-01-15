using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000275 RID: 629
	public sealed class MailboxSearchScope
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x0003D821 File Offset: 0x0003C821
		public MailboxSearchScope(string mailbox, MailboxSearchLocation searchScope)
		{
			this.Mailbox = mailbox;
			this.searchScope = searchScope;
			this.ExtendedAttributes = new ExtendedAttributes();
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0003D849 File Offset: 0x0003C849
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x0003D851 File Offset: 0x0003C851
		public string Mailbox { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0003D85A File Offset: 0x0003C85A
		// (set) Token: 0x0600161C RID: 5660 RVA: 0x0003D862 File Offset: 0x0003C862
		public MailboxSearchLocation SearchScope
		{
			get
			{
				return this.searchScope;
			}
			set
			{
				this.searchScope = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0003D86B File Offset: 0x0003C86B
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x0003D873 File Offset: 0x0003C873
		internal MailboxSearchScopeType SearchScopeType
		{
			get
			{
				return this.scopeType;
			}
			set
			{
				this.scopeType = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0003D87C File Offset: 0x0003C87C
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x0003D884 File Offset: 0x0003C884
		public ExtendedAttributes ExtendedAttributes { get; private set; }

		// Token: 0x040012D4 RID: 4820
		private MailboxSearchLocation searchScope = MailboxSearchLocation.All;

		// Token: 0x040012D5 RID: 4821
		private MailboxSearchScopeType scopeType;
	}
}
