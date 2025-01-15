using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DCB RID: 11723
	[GeneratedCode("DomGen", "2.0")]
	internal class BookFoldReversePrinting : OnOffType
	{
		// Token: 0x170087FD RID: 34813
		// (get) Token: 0x06018EBA RID: 102074 RVA: 0x0034524A File Offset: 0x0034344A
		public override string LocalName
		{
			get
			{
				return "bookFoldRevPrinting";
			}
		}

		// Token: 0x170087FE RID: 34814
		// (get) Token: 0x06018EBB RID: 102075 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087FF RID: 34815
		// (get) Token: 0x06018EBC RID: 102076 RVA: 0x00345251 File Offset: 0x00343451
		internal override int ElementTypeId
		{
			get
			{
				return 12006;
			}
		}

		// Token: 0x06018EBD RID: 102077 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EBF RID: 102079 RVA: 0x00345258 File Offset: 0x00343458
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookFoldReversePrinting>(deep);
		}

		// Token: 0x0400A5CC RID: 42444
		private const string tagName = "bookFoldRevPrinting";

		// Token: 0x0400A5CD RID: 42445
		private const byte tagNsId = 23;

		// Token: 0x0400A5CE RID: 42446
		internal const int ElementTypeIdConst = 12006;
	}
}
