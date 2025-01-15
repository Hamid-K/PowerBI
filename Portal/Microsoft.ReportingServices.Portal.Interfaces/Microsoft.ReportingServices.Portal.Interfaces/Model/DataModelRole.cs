using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000035 RID: 53
	public sealed class DataModelRole
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00002AE7 File Offset: 0x00000CE7
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00002AEF File Offset: 0x00000CEF
		[Required]
		[Key]
		public Guid ModelRoleId { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00002AF8 File Offset: 0x00000CF8
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00002B00 File Offset: 0x00000D00
		public string ModelRoleName { get; set; }
	}
}
