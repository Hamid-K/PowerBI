using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D42 RID: 11586
	[GeneratedCode("DomGen", "2.0")]
	internal class BookmarkEnd : MarkupRangeType
	{
		// Token: 0x1700865C RID: 34396
		// (get) Token: 0x06018B74 RID: 101236 RVA: 0x00344451 File Offset: 0x00342651
		public override string LocalName
		{
			get
			{
				return "bookmarkEnd";
			}
		}

		// Token: 0x1700865D RID: 34397
		// (get) Token: 0x06018B75 RID: 101237 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700865E RID: 34398
		// (get) Token: 0x06018B76 RID: 101238 RVA: 0x00344458 File Offset: 0x00342658
		internal override int ElementTypeId
		{
			get
			{
				return 11477;
			}
		}

		// Token: 0x06018B77 RID: 101239 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B79 RID: 101241 RVA: 0x00344467 File Offset: 0x00342667
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookmarkEnd>(deep);
		}

		// Token: 0x0400A435 RID: 42037
		private const string tagName = "bookmarkEnd";

		// Token: 0x0400A436 RID: 42038
		private const byte tagNsId = 23;

		// Token: 0x0400A437 RID: 42039
		internal const int ElementTypeIdConst = 11477;
	}
}
