using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200274B RID: 10059
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletFont : TextFontType
	{
		// Token: 0x17006088 RID: 24712
		// (get) Token: 0x060135C1 RID: 79297 RVA: 0x00306527 File Offset: 0x00304727
		public override string LocalName
		{
			get
			{
				return "buFont";
			}
		}

		// Token: 0x17006089 RID: 24713
		// (get) Token: 0x060135C2 RID: 79298 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700608A RID: 24714
		// (get) Token: 0x060135C3 RID: 79299 RVA: 0x0030652E File Offset: 0x0030472E
		internal override int ElementTypeId
		{
			get
			{
				return 10108;
			}
		}

		// Token: 0x060135C4 RID: 79300 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135C6 RID: 79302 RVA: 0x0030653D File Offset: 0x0030473D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletFont>(deep);
		}

		// Token: 0x040085D2 RID: 34258
		private const string tagName = "buFont";

		// Token: 0x040085D3 RID: 34259
		private const byte tagNsId = 10;

		// Token: 0x040085D4 RID: 34260
		internal const int ElementTypeIdConst = 10108;
	}
}
