using System;
using System.Data.Common;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020000DE RID: 222
	internal sealed class LegacyHistoryContext : DbContext
	{
		// Token: 0x060010E1 RID: 4321 RVA: 0x00027A0E File Offset: 0x00025C0E
		public LegacyHistoryContext(DbConnection existingConnection)
			: base(existingConnection, false)
		{
			this.InternalContext.InitializerDisabled = true;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00027A24 File Offset: 0x00025C24
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x00027A2C File Offset: 0x00025C2C
		public IDbSet<LegacyHistoryRow> History { get; set; }
	}
}
