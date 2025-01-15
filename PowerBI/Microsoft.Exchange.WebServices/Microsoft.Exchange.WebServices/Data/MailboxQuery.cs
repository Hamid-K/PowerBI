using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000274 RID: 628
	public sealed class MailboxQuery
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x0003D7E9 File Offset: 0x0003C7E9
		public MailboxQuery(string query, MailboxSearchScope[] searchScopes)
		{
			this.Query = query;
			this.MailboxSearchScopes = searchScopes;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0003D7FF File Offset: 0x0003C7FF
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x0003D807 File Offset: 0x0003C807
		public string Query { get; set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0003D810 File Offset: 0x0003C810
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x0003D818 File Offset: 0x0003C818
		public MailboxSearchScope[] MailboxSearchScopes { get; set; }
	}
}
