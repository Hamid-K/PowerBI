using System;

namespace System.Data.Entity.Migrations.Sql
{
	// Token: 0x020000A9 RID: 169
	public class MigrationStatement
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0001FA78 File Offset: 0x0001DC78
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public string Sql { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0001FA89 File Offset: 0x0001DC89
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0001FA91 File Offset: 0x0001DC91
		public bool SuppressTransaction { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0001FA9A File Offset: 0x0001DC9A
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0001FAA2 File Offset: 0x0001DCA2
		public string BatchTerminator { get; set; }
	}
}
