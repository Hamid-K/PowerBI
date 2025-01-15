using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020000DF RID: 223
	[Table("__MigrationHistory")]
	internal sealed class LegacyHistoryRow
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00027A35 File Offset: 0x00025C35
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x00027A3D File Offset: 0x00025C3D
		public int Id { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00027A46 File Offset: 0x00025C46
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x00027A4E File Offset: 0x00025C4E
		public DateTime CreatedOn { get; set; }
	}
}
