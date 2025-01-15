using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public class AdomdErrorLocation
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00022080 File Offset: 0x00020280
		public int StartLine
		{
			get
			{
				return this.startLine;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00022088 File Offset: 0x00020288
		public int StartColumn
		{
			get
			{
				return this.startColumn;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00022090 File Offset: 0x00020290
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00022098 File Offset: 0x00020298
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000220A0 File Offset: 0x000202A0
		public int LineOffset
		{
			get
			{
				return this.lineOffset;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000220A8 File Offset: 0x000202A8
		public int TextLength
		{
			get
			{
				return this.textLength;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000220B0 File Offset: 0x000202B0
		internal AdomdErrorLocation(int startLine, int startColumn, int endLine, int endColumn, int lineOffset, int textLength)
		{
			this.startLine = startLine;
			this.startColumn = startColumn;
			this.endLine = endLine;
			this.endColumn = endColumn;
			this.lineOffset = lineOffset;
			this.textLength = textLength;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002211A File Offset: 0x0002031A
		internal AdomdErrorLocation(XmlaMessageLocation location)
			: this(location.StartLine, location.StartColumn, location.EndLine, location.EndColumn, location.LineOffset, location.TextLength)
		{
		}

		// Token: 0x04000449 RID: 1097
		private int startLine = -1;

		// Token: 0x0400044A RID: 1098
		private int startColumn = -1;

		// Token: 0x0400044B RID: 1099
		private int endLine = -1;

		// Token: 0x0400044C RID: 1100
		private int endColumn = -1;

		// Token: 0x0400044D RID: 1101
		private int lineOffset = -1;

		// Token: 0x0400044E RID: 1102
		private int textLength = -1;
	}
}
