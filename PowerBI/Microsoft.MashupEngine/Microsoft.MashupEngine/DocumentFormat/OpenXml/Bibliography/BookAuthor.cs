using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F1 RID: 10481
	[GeneratedCode("DomGen", "2.0")]
	internal class BookAuthor : NameType
	{
		// Token: 0x1700697C RID: 27004
		// (get) Token: 0x06014A82 RID: 84610 RVA: 0x00315402 File Offset: 0x00313602
		public override string LocalName
		{
			get
			{
				return "BookAuthor";
			}
		}

		// Token: 0x1700697D RID: 27005
		// (get) Token: 0x06014A83 RID: 84611 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700697E RID: 27006
		// (get) Token: 0x06014A84 RID: 84612 RVA: 0x00315409 File Offset: 0x00313609
		internal override int ElementTypeId
		{
			get
			{
				return 10767;
			}
		}

		// Token: 0x06014A85 RID: 84613 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A86 RID: 84614 RVA: 0x003153D6 File Offset: 0x003135D6
		public BookAuthor()
		{
		}

		// Token: 0x06014A87 RID: 84615 RVA: 0x003153DE File Offset: 0x003135DE
		public BookAuthor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A88 RID: 84616 RVA: 0x003153E7 File Offset: 0x003135E7
		public BookAuthor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A89 RID: 84617 RVA: 0x003153F0 File Offset: 0x003135F0
		public BookAuthor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014A8A RID: 84618 RVA: 0x00315410 File Offset: 0x00313610
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookAuthor>(deep);
		}

		// Token: 0x04008F5E RID: 36702
		private const string tagName = "BookAuthor";

		// Token: 0x04008F5F RID: 36703
		private const byte tagNsId = 9;

		// Token: 0x04008F60 RID: 36704
		internal const int ElementTypeIdConst = 10767;
	}
}
