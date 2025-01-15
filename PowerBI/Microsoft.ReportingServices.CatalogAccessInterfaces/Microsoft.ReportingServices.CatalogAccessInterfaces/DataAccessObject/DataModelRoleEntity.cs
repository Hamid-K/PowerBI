using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000017 RID: 23
	public class DataModelRoleEntity
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002613 File Offset: 0x00000813
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000261B File Offset: 0x0000081B
		public long DataModelRoleId { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000262C File Offset: 0x0000082C
		public Guid ItemId { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002635 File Offset: 0x00000835
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000263D File Offset: 0x0000083D
		public Guid ModelRoleId { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002646 File Offset: 0x00000846
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000264E File Offset: 0x0000084E
		public string ModelRoleName { get; set; }
	}
}
