using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public class AdomdErrorLocation
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00021D50 File Offset: 0x0001FF50
		public int StartLine
		{
			get
			{
				return this.startLine;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00021D58 File Offset: 0x0001FF58
		public int StartColumn
		{
			get
			{
				return this.startColumn;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00021D60 File Offset: 0x0001FF60
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00021D68 File Offset: 0x0001FF68
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00021D70 File Offset: 0x0001FF70
		public int LineOffset
		{
			get
			{
				return this.lineOffset;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00021D78 File Offset: 0x0001FF78
		public int TextLength
		{
			get
			{
				return this.textLength;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00021D80 File Offset: 0x0001FF80
		internal AdomdErrorLocation(int startLine, int startColumn, int endLine, int endColumn, int lineOffset, int textLength)
		{
			this.startLine = startLine;
			this.startColumn = startColumn;
			this.endLine = endLine;
			this.endColumn = endColumn;
			this.lineOffset = lineOffset;
			this.textLength = textLength;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00021DEA File Offset: 0x0001FFEA
		internal AdomdErrorLocation(XmlaMessageLocation location)
			: this(location.StartLine, location.StartColumn, location.EndLine, location.EndColumn, location.LineOffset, location.TextLength)
		{
		}

		// Token: 0x0400043C RID: 1084
		private int startLine = -1;

		// Token: 0x0400043D RID: 1085
		private int startColumn = -1;

		// Token: 0x0400043E RID: 1086
		private int endLine = -1;

		// Token: 0x0400043F RID: 1087
		private int endColumn = -1;

		// Token: 0x04000440 RID: 1088
		private int lineOffset = -1;

		// Token: 0x04000441 RID: 1089
		private int textLength = -1;
	}
}
