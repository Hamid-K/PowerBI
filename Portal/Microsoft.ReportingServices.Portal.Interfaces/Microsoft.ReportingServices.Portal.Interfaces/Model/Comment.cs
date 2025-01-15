using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000005 RID: 5
	public class Comment
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A7 File Offset: 0x000002A7
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020AF File Offset: 0x000002AF
		[ReadOnly(true)]
		[Editable(false)]
		public long Id { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B8 File Offset: 0x000002B8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020C0 File Offset: 0x000002C0
		[Required]
		public Guid? ItemId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C9 File Offset: 0x000002C9
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020D1 File Offset: 0x000002D1
		[ReadOnly(true)]
		[Editable(false)]
		public string UserName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020DA File Offset: 0x000002DA
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020E2 File Offset: 0x000002E2
		public long? ThreadId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020EB File Offset: 0x000002EB
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020F3 File Offset: 0x000002F3
		public string AttachmentPath { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002104 File Offset: 0x00000304
		[Required]
		[StringLength(2048)]
		public string Text { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000210D File Offset: 0x0000030D
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002115 File Offset: 0x00000315
		[Editable(false)]
		[ReadOnly(true)]
		public DateTimeOffset CreatedDate { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002126 File Offset: 0x00000326
		[Editable(false)]
		[ReadOnly(true)]
		public DateTimeOffset? ModifiedDate { get; set; }
	}
}
