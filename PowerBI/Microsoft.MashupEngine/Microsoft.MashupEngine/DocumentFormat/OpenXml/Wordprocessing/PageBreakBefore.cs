using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D6C RID: 11628
	[GeneratedCode("DomGen", "2.0")]
	internal class PageBreakBefore : OnOffType
	{
		// Token: 0x170086E0 RID: 34528
		// (get) Token: 0x06018C80 RID: 101504 RVA: 0x00344A0E File Offset: 0x00342C0E
		public override string LocalName
		{
			get
			{
				return "pageBreakBefore";
			}
		}

		// Token: 0x170086E1 RID: 34529
		// (get) Token: 0x06018C81 RID: 101505 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086E2 RID: 34530
		// (get) Token: 0x06018C82 RID: 101506 RVA: 0x00344A15 File Offset: 0x00342C15
		internal override int ElementTypeId
		{
			get
			{
				return 11495;
			}
		}

		// Token: 0x06018C83 RID: 101507 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C85 RID: 101509 RVA: 0x00344A1C File Offset: 0x00342C1C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageBreakBefore>(deep);
		}

		// Token: 0x0400A4AF RID: 42159
		private const string tagName = "pageBreakBefore";

		// Token: 0x0400A4B0 RID: 42160
		private const byte tagNsId = 23;

		// Token: 0x0400A4B1 RID: 42161
		internal const int ElementTypeIdConst = 11495;
	}
}
