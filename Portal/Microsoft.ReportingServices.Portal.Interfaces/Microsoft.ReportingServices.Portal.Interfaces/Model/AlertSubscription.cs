using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000031 RID: 49
	public class AlertSubscription
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00002A06 File Offset: 0x00000C06
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00002A0E File Offset: 0x00000C0E
		[ReadOnly(true)]
		[Editable(false)]
		public long Id { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00002A17 File Offset: 0x00000C17
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00002A1F File Offset: 0x00000C1F
		[Required]
		public Guid ItemId { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00002A28 File Offset: 0x00000C28
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00002A30 File Offset: 0x00000C30
		[Required]
		public string AlertType { get; set; }
	}
}
