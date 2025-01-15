using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200026A RID: 618
	public class CustomPluralizationEntry
	{
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00056E0A File Offset: 0x0005500A
		// (set) Token: 0x06001F63 RID: 8035 RVA: 0x00056E12 File Offset: 0x00055012
		public string Singular { get; private set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x00056E1B File Offset: 0x0005501B
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x00056E23 File Offset: 0x00055023
		public string Plural { get; private set; }

		// Token: 0x06001F66 RID: 8038 RVA: 0x00056E2C File Offset: 0x0005502C
		public CustomPluralizationEntry(string singular, string plural)
		{
			Check.NotEmpty(singular, "singular");
			Check.NotEmpty(plural, "plural");
			this.Singular = singular;
			this.Plural = plural;
		}
	}
}
