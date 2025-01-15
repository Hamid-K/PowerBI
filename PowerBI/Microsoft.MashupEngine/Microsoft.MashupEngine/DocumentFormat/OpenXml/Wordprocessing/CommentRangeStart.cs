using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D43 RID: 11587
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentRangeStart : MarkupRangeType
	{
		// Token: 0x1700865F RID: 34399
		// (get) Token: 0x06018B7A RID: 101242 RVA: 0x00344470 File Offset: 0x00342670
		public override string LocalName
		{
			get
			{
				return "commentRangeStart";
			}
		}

		// Token: 0x17008660 RID: 34400
		// (get) Token: 0x06018B7B RID: 101243 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008661 RID: 34401
		// (get) Token: 0x06018B7C RID: 101244 RVA: 0x00344477 File Offset: 0x00342677
		internal override int ElementTypeId
		{
			get
			{
				return 11478;
			}
		}

		// Token: 0x06018B7D RID: 101245 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B7F RID: 101247 RVA: 0x0034447E File Offset: 0x0034267E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentRangeStart>(deep);
		}

		// Token: 0x0400A438 RID: 42040
		private const string tagName = "commentRangeStart";

		// Token: 0x0400A439 RID: 42041
		private const byte tagNsId = 23;

		// Token: 0x0400A43A RID: 42042
		internal const int ElementTypeIdConst = 11478;
	}
}
