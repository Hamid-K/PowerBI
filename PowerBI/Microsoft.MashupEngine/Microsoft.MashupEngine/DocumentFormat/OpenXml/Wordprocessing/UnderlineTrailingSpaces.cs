using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DEA RID: 11754
	[GeneratedCode("DomGen", "2.0")]
	internal class UnderlineTrailingSpaces : OnOffType
	{
		// Token: 0x1700885A RID: 34906
		// (get) Token: 0x06018F74 RID: 102260 RVA: 0x00345513 File Offset: 0x00343713
		public override string LocalName
		{
			get
			{
				return "ulTrailSpace";
			}
		}

		// Token: 0x1700885B RID: 34907
		// (get) Token: 0x06018F75 RID: 102261 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700885C RID: 34908
		// (get) Token: 0x06018F76 RID: 102262 RVA: 0x0034551A File Offset: 0x0034371A
		internal override int ElementTypeId
		{
			get
			{
				return 12064;
			}
		}

		// Token: 0x06018F77 RID: 102263 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F79 RID: 102265 RVA: 0x00345521 File Offset: 0x00343721
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnderlineTrailingSpaces>(deep);
		}

		// Token: 0x0400A629 RID: 42537
		private const string tagName = "ulTrailSpace";

		// Token: 0x0400A62A RID: 42538
		private const byte tagNsId = 23;

		// Token: 0x0400A62B RID: 42539
		internal const int ElementTypeIdConst = 12064;
	}
}
