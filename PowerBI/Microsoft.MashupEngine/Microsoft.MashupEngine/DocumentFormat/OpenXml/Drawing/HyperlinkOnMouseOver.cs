using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200279A RID: 10138
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinkOnMouseOver : HyperlinkType
	{
		// Token: 0x1700624E RID: 25166
		// (get) Token: 0x060139DF RID: 80351 RVA: 0x00308E36 File Offset: 0x00307036
		public override string LocalName
		{
			get
			{
				return "hlinkMouseOver";
			}
		}

		// Token: 0x1700624F RID: 25167
		// (get) Token: 0x060139E0 RID: 80352 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006250 RID: 25168
		// (get) Token: 0x060139E1 RID: 80353 RVA: 0x00308E3D File Offset: 0x0030703D
		internal override int ElementTypeId
		{
			get
			{
				return 10326;
			}
		}

		// Token: 0x060139E2 RID: 80354 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060139E3 RID: 80355 RVA: 0x00308E0A File Offset: 0x0030700A
		public HyperlinkOnMouseOver()
		{
		}

		// Token: 0x060139E4 RID: 80356 RVA: 0x00308E12 File Offset: 0x00307012
		public HyperlinkOnMouseOver(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139E5 RID: 80357 RVA: 0x00308E1B File Offset: 0x0030701B
		public HyperlinkOnMouseOver(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139E6 RID: 80358 RVA: 0x00308E24 File Offset: 0x00307024
		public HyperlinkOnMouseOver(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060139E7 RID: 80359 RVA: 0x00308E44 File Offset: 0x00307044
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkOnMouseOver>(deep);
		}

		// Token: 0x040086F8 RID: 34552
		private const string tagName = "hlinkMouseOver";

		// Token: 0x040086F9 RID: 34553
		private const byte tagNsId = 10;

		// Token: 0x040086FA RID: 34554
		internal const int ElementTypeIdConst = 10326;
	}
}
