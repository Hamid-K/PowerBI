using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public sealed class XmlaMessageLocation
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0001FF20 File Offset: 0x0001E120
		internal XmlaMessageLocation(int startLine, int startColumn, int endLine, int endColumn, int lineOffset, int textLength, XmlaLocationReference sourceObject, XmlaLocationReference dependsOnObject, long rowNumber)
		{
			this.startLine = startLine;
			this.startColumn = startColumn;
			this.endLine = endLine;
			this.endColumn = endColumn;
			this.lineOffset = lineOffset;
			this.textLength = textLength;
			this.sourceObject = sourceObject;
			this.dependsOnObject = dependsOnObject;
			this.rowNumber = rowNumber;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001FFAA File Offset: 0x0001E1AA
		public int StartLine
		{
			get
			{
				return this.startLine;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001FFB2 File Offset: 0x0001E1B2
		public int StartColumn
		{
			get
			{
				return this.startColumn;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001FFBA File Offset: 0x0001E1BA
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001FFC2 File Offset: 0x0001E1C2
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001FFCA File Offset: 0x0001E1CA
		public int LineOffset
		{
			get
			{
				return this.lineOffset;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001FFD2 File Offset: 0x0001E1D2
		public int TextLength
		{
			get
			{
				return this.textLength;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001FFDA File Offset: 0x0001E1DA
		public XmlaLocationReference SourceObject
		{
			get
			{
				return this.sourceObject;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001FFE2 File Offset: 0x0001E1E2
		public XmlaLocationReference DependsOnObject
		{
			get
			{
				return this.dependsOnObject;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001FFEA File Offset: 0x0001E1EA
		public long RowNumber
		{
			get
			{
				return this.rowNumber;
			}
		}

		// Token: 0x040003E5 RID: 997
		private int startLine = -1;

		// Token: 0x040003E6 RID: 998
		private int startColumn = -1;

		// Token: 0x040003E7 RID: 999
		private int endLine = -1;

		// Token: 0x040003E8 RID: 1000
		private int endColumn = -1;

		// Token: 0x040003E9 RID: 1001
		private int lineOffset = -1;

		// Token: 0x040003EA RID: 1002
		private int textLength = -1;

		// Token: 0x040003EB RID: 1003
		private XmlaLocationReference sourceObject;

		// Token: 0x040003EC RID: 1004
		private XmlaLocationReference dependsOnObject;

		// Token: 0x040003ED RID: 1005
		private long rowNumber = -1L;
	}
}
