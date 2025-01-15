using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000047 RID: 71
	internal sealed class XmlaMessageLocation
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0001C55C File Offset: 0x0001A75C
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001C5E6 File Offset: 0x0001A7E6
		public int StartLine
		{
			get
			{
				return this.startLine;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001C5EE File Offset: 0x0001A7EE
		public int StartColumn
		{
			get
			{
				return this.startColumn;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001C5F6 File Offset: 0x0001A7F6
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001C5FE File Offset: 0x0001A7FE
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001C606 File Offset: 0x0001A806
		public int LineOffset
		{
			get
			{
				return this.lineOffset;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001C60E File Offset: 0x0001A80E
		public int TextLength
		{
			get
			{
				return this.textLength;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0001C616 File Offset: 0x0001A816
		public XmlaLocationReference SourceObject
		{
			get
			{
				return this.sourceObject;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001C61E File Offset: 0x0001A81E
		public XmlaLocationReference DependsOnObject
		{
			get
			{
				return this.dependsOnObject;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0001C626 File Offset: 0x0001A826
		public long RowNumber
		{
			get
			{
				return this.rowNumber;
			}
		}

		// Token: 0x040003B6 RID: 950
		private int startLine = -1;

		// Token: 0x040003B7 RID: 951
		private int startColumn = -1;

		// Token: 0x040003B8 RID: 952
		private int endLine = -1;

		// Token: 0x040003B9 RID: 953
		private int endColumn = -1;

		// Token: 0x040003BA RID: 954
		private int lineOffset = -1;

		// Token: 0x040003BB RID: 955
		private int textLength = -1;

		// Token: 0x040003BC RID: 956
		private XmlaLocationReference sourceObject;

		// Token: 0x040003BD RID: 957
		private XmlaLocationReference dependsOnObject;

		// Token: 0x040003BE RID: 958
		private long rowNumber = -1L;
	}
}
