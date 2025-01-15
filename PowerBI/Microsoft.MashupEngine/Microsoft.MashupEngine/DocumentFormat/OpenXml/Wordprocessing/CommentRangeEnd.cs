using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D44 RID: 11588
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentRangeEnd : MarkupRangeType
	{
		// Token: 0x17008662 RID: 34402
		// (get) Token: 0x06018B80 RID: 101248 RVA: 0x00344487 File Offset: 0x00342687
		public override string LocalName
		{
			get
			{
				return "commentRangeEnd";
			}
		}

		// Token: 0x17008663 RID: 34403
		// (get) Token: 0x06018B81 RID: 101249 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008664 RID: 34404
		// (get) Token: 0x06018B82 RID: 101250 RVA: 0x0034448E File Offset: 0x0034268E
		internal override int ElementTypeId
		{
			get
			{
				return 11479;
			}
		}

		// Token: 0x06018B83 RID: 101251 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B85 RID: 101253 RVA: 0x00344495 File Offset: 0x00342695
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentRangeEnd>(deep);
		}

		// Token: 0x0400A43B RID: 42043
		private const string tagName = "commentRangeEnd";

		// Token: 0x0400A43C RID: 42044
		private const byte tagNsId = 23;

		// Token: 0x0400A43D RID: 42045
		internal const int ElementTypeIdConst = 11479;
	}
}
