using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200274D RID: 10061
	[GeneratedCode("DomGen", "2.0")]
	internal class EastAsianFont : TextFontType
	{
		// Token: 0x1700608E RID: 24718
		// (get) Token: 0x060135CD RID: 79309 RVA: 0x0030655D File Offset: 0x0030475D
		public override string LocalName
		{
			get
			{
				return "ea";
			}
		}

		// Token: 0x1700608F RID: 24719
		// (get) Token: 0x060135CE RID: 79310 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006090 RID: 24720
		// (get) Token: 0x060135CF RID: 79311 RVA: 0x00306564 File Offset: 0x00304764
		internal override int ElementTypeId
		{
			get
			{
				return 10132;
			}
		}

		// Token: 0x060135D0 RID: 79312 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135D2 RID: 79314 RVA: 0x0030656B File Offset: 0x0030476B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EastAsianFont>(deep);
		}

		// Token: 0x040085D8 RID: 34264
		private const string tagName = "ea";

		// Token: 0x040085D9 RID: 34265
		private const byte tagNsId = 10;

		// Token: 0x040085DA RID: 34266
		internal const int ElementTypeIdConst = 10132;
	}
}
