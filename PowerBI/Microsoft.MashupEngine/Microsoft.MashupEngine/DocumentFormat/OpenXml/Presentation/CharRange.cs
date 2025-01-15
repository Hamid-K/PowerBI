using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F0 RID: 10736
	[GeneratedCode("DomGen", "2.0")]
	internal class CharRange : IndexRangeType
	{
		// Token: 0x17006E6A RID: 28266
		// (get) Token: 0x060155C4 RID: 87492 RVA: 0x0031E30E File Offset: 0x0031C50E
		public override string LocalName
		{
			get
			{
				return "charRg";
			}
		}

		// Token: 0x17006E6B RID: 28267
		// (get) Token: 0x060155C5 RID: 87493 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E6C RID: 28268
		// (get) Token: 0x060155C6 RID: 87494 RVA: 0x0031E315 File Offset: 0x0031C515
		internal override int ElementTypeId
		{
			get
			{
				return 12193;
			}
		}

		// Token: 0x060155C7 RID: 87495 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155C9 RID: 87497 RVA: 0x0031E31C File Offset: 0x0031C51C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharRange>(deep);
		}

		// Token: 0x04009329 RID: 37673
		private const string tagName = "charRg";

		// Token: 0x0400932A RID: 37674
		private const byte tagNsId = 24;

		// Token: 0x0400932B RID: 37675
		internal const int ElementTypeIdConst = 12193;
	}
}
