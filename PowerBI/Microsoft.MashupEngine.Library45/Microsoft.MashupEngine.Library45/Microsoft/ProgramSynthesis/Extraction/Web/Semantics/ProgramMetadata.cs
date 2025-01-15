using System;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x0200117B RID: 4475
	public class ProgramMetadata
	{
		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06008521 RID: 34081 RVA: 0x001C020B File Offset: 0x001BE40B
		public string TableTitle { get; }

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06008522 RID: 34082 RVA: 0x001C0213 File Offset: 0x001BE413
		// (set) Token: 0x06008523 RID: 34083 RVA: 0x001C021B File Offset: 0x001BE41B
		public TableKind TableKind { get; set; }

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06008524 RID: 34084 RVA: 0x001C0224 File Offset: 0x001BE424
		// (set) Token: 0x06008525 RID: 34085 RVA: 0x001C022C File Offset: 0x001BE42C
		public int RowCount { get; set; }

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06008526 RID: 34086 RVA: 0x001C0235 File Offset: 0x001BE435
		// (set) Token: 0x06008527 RID: 34087 RVA: 0x001C023D File Offset: 0x001BE43D
		public string[] Attributes { get; set; }

		// Token: 0x06008528 RID: 34088 RVA: 0x001C0246 File Offset: 0x001BE446
		public ProgramMetadata(string tableTitle, TableKind kind = TableKind.Unknown, int rowCount = 0, string[] attributes = null)
		{
			this.TableTitle = tableTitle;
			this.TableKind = kind;
			this.RowCount = rowCount;
			this.Attributes = attributes;
		}
	}
}
