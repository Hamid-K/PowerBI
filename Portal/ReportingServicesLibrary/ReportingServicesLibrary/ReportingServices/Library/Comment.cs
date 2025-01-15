using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001D RID: 29
	internal class Comment
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00005025 File Offset: 0x00003225
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000502D File Offset: 0x0000322D
		public long Id { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005036 File Offset: 0x00003236
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000503E File Offset: 0x0000323E
		public Guid ItemId { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005047 File Offset: 0x00003247
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000504F File Offset: 0x0000324F
		public string UserName { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005058 File Offset: 0x00003258
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00005060 File Offset: 0x00003260
		public long? ThreadId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005069 File Offset: 0x00003269
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00005071 File Offset: 0x00003271
		public string AttachmentPath { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000507A File Offset: 0x0000327A
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00005082 File Offset: 0x00003282
		public string Text { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000508B File Offset: 0x0000328B
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00005093 File Offset: 0x00003293
		public DateTime CreatedDate { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000509C File Offset: 0x0000329C
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000050A4 File Offset: 0x000032A4
		public DateTime? ModifiedDate { get; set; }
	}
}
