using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200279B RID: 10139
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinkOnHover : HyperlinkType
	{
		// Token: 0x17006251 RID: 25169
		// (get) Token: 0x060139E8 RID: 80360 RVA: 0x00308E4D File Offset: 0x0030704D
		public override string LocalName
		{
			get
			{
				return "hlinkHover";
			}
		}

		// Token: 0x17006252 RID: 25170
		// (get) Token: 0x060139E9 RID: 80361 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006253 RID: 25171
		// (get) Token: 0x060139EA RID: 80362 RVA: 0x00308E54 File Offset: 0x00307054
		internal override int ElementTypeId
		{
			get
			{
				return 10340;
			}
		}

		// Token: 0x060139EB RID: 80363 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060139EC RID: 80364 RVA: 0x00308E0A File Offset: 0x0030700A
		public HyperlinkOnHover()
		{
		}

		// Token: 0x060139ED RID: 80365 RVA: 0x00308E12 File Offset: 0x00307012
		public HyperlinkOnHover(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139EE RID: 80366 RVA: 0x00308E1B File Offset: 0x0030701B
		public HyperlinkOnHover(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139EF RID: 80367 RVA: 0x00308E24 File Offset: 0x00307024
		public HyperlinkOnHover(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060139F0 RID: 80368 RVA: 0x00308E5B File Offset: 0x0030705B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkOnHover>(deep);
		}

		// Token: 0x040086FB RID: 34555
		private const string tagName = "hlinkHover";

		// Token: 0x040086FC RID: 34556
		private const byte tagNsId = 10;

		// Token: 0x040086FD RID: 34557
		internal const int ElementTypeIdConst = 10340;
	}
}
