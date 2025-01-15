using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000016 RID: 22
	public class DataModelRoleAssignmentEntity
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000025E0 File Offset: 0x000007E0
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000025E8 File Offset: 0x000007E8
		public Guid UserId { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000025F1 File Offset: 0x000007F1
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000025F9 File Offset: 0x000007F9
		public string UserName { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002602 File Offset: 0x00000802
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000260A File Offset: 0x0000080A
		public IList<DataModelRoleEntity> DataModelRoles { get; set; }
	}
}
