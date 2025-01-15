using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000034 RID: 52
	public sealed class DataModelRoleAssignment
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00002AC5 File Offset: 0x00000CC5
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00002ACD File Offset: 0x00000CCD
		[Required]
		[Key]
		public string GroupUserName { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002AD6 File Offset: 0x00000CD6
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00002ADE File Offset: 0x00000CDE
		public IList<Guid> DataModelRoles { get; set; }
	}
}
