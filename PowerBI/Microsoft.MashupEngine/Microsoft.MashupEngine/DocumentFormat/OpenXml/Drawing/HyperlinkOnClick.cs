using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002799 RID: 10137
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinkOnClick : HyperlinkType
	{
		// Token: 0x1700624B RID: 25163
		// (get) Token: 0x060139D6 RID: 80342 RVA: 0x00308DFC File Offset: 0x00306FFC
		public override string LocalName
		{
			get
			{
				return "hlinkClick";
			}
		}

		// Token: 0x1700624C RID: 25164
		// (get) Token: 0x060139D7 RID: 80343 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700624D RID: 25165
		// (get) Token: 0x060139D8 RID: 80344 RVA: 0x00308E03 File Offset: 0x00307003
		internal override int ElementTypeId
		{
			get
			{
				return 10172;
			}
		}

		// Token: 0x060139D9 RID: 80345 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060139DA RID: 80346 RVA: 0x00308E0A File Offset: 0x0030700A
		public HyperlinkOnClick()
		{
		}

		// Token: 0x060139DB RID: 80347 RVA: 0x00308E12 File Offset: 0x00307012
		public HyperlinkOnClick(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139DC RID: 80348 RVA: 0x00308E1B File Offset: 0x0030701B
		public HyperlinkOnClick(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139DD RID: 80349 RVA: 0x00308E24 File Offset: 0x00307024
		public HyperlinkOnClick(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060139DE RID: 80350 RVA: 0x00308E2D File Offset: 0x0030702D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkOnClick>(deep);
		}

		// Token: 0x040086F5 RID: 34549
		private const string tagName = "hlinkClick";

		// Token: 0x040086F6 RID: 34550
		private const byte tagNsId = 10;

		// Token: 0x040086F7 RID: 34551
		internal const int ElementTypeIdConst = 10172;
	}
}
