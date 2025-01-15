using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000047 RID: 71
	internal sealed class XmlaMessageLocation
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0001C22C File Offset: 0x0001A42C
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001C2B6 File Offset: 0x0001A4B6
		public int StartLine
		{
			get
			{
				return this.startLine;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001C2BE File Offset: 0x0001A4BE
		public int StartColumn
		{
			get
			{
				return this.startColumn;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001C2C6 File Offset: 0x0001A4C6
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001C2CE File Offset: 0x0001A4CE
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001C2D6 File Offset: 0x0001A4D6
		public int LineOffset
		{
			get
			{
				return this.lineOffset;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001C2DE File Offset: 0x0001A4DE
		public int TextLength
		{
			get
			{
				return this.textLength;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001C2E6 File Offset: 0x0001A4E6
		public XmlaLocationReference SourceObject
		{
			get
			{
				return this.sourceObject;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001C2EE File Offset: 0x0001A4EE
		public XmlaLocationReference DependsOnObject
		{
			get
			{
				return this.dependsOnObject;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001C2F6 File Offset: 0x0001A4F6
		public long RowNumber
		{
			get
			{
				return this.rowNumber;
			}
		}

		// Token: 0x040003A9 RID: 937
		private int startLine = -1;

		// Token: 0x040003AA RID: 938
		private int startColumn = -1;

		// Token: 0x040003AB RID: 939
		private int endLine = -1;

		// Token: 0x040003AC RID: 940
		private int endColumn = -1;

		// Token: 0x040003AD RID: 941
		private int lineOffset = -1;

		// Token: 0x040003AE RID: 942
		private int textLength = -1;

		// Token: 0x040003AF RID: 943
		private XmlaLocationReference sourceObject;

		// Token: 0x040003B0 RID: 944
		private XmlaLocationReference dependsOnObject;

		// Token: 0x040003B1 RID: 945
		private long rowNumber = -1L;
	}
}
