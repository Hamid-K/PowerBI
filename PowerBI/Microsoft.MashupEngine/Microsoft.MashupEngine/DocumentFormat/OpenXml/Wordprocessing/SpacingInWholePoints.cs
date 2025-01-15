using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DEC RID: 11756
	[GeneratedCode("DomGen", "2.0")]
	internal class SpacingInWholePoints : OnOffType
	{
		// Token: 0x17008860 RID: 34912
		// (get) Token: 0x06018F80 RID: 102272 RVA: 0x00345541 File Offset: 0x00343741
		public override string LocalName
		{
			get
			{
				return "spacingInWholePoints";
			}
		}

		// Token: 0x17008861 RID: 34913
		// (get) Token: 0x06018F81 RID: 102273 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008862 RID: 34914
		// (get) Token: 0x06018F82 RID: 102274 RVA: 0x00345548 File Offset: 0x00343748
		internal override int ElementTypeId
		{
			get
			{
				return 12066;
			}
		}

		// Token: 0x06018F83 RID: 102275 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F85 RID: 102277 RVA: 0x0034554F File Offset: 0x0034374F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpacingInWholePoints>(deep);
		}

		// Token: 0x0400A62F RID: 42543
		private const string tagName = "spacingInWholePoints";

		// Token: 0x0400A630 RID: 42544
		private const byte tagNsId = 23;

		// Token: 0x0400A631 RID: 42545
		internal const int ElementTypeIdConst = 12066;
	}
}
