using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002784 RID: 10116
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent5Color : Color2Type
	{
		// Token: 0x170061C2 RID: 25026
		// (get) Token: 0x060138AE RID: 80046 RVA: 0x003082E7 File Offset: 0x003064E7
		public override string LocalName
		{
			get
			{
				return "accent5";
			}
		}

		// Token: 0x170061C3 RID: 25027
		// (get) Token: 0x060138AF RID: 80047 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061C4 RID: 25028
		// (get) Token: 0x060138B0 RID: 80048 RVA: 0x003082EE File Offset: 0x003064EE
		internal override int ElementTypeId
		{
			get
			{
				return 10155;
			}
		}

		// Token: 0x060138B1 RID: 80049 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138B2 RID: 80050 RVA: 0x0030821A File Offset: 0x0030641A
		public Accent5Color()
		{
		}

		// Token: 0x060138B3 RID: 80051 RVA: 0x00308222 File Offset: 0x00306422
		public Accent5Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138B4 RID: 80052 RVA: 0x0030822B File Offset: 0x0030642B
		public Accent5Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138B5 RID: 80053 RVA: 0x00308234 File Offset: 0x00306434
		public Accent5Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060138B6 RID: 80054 RVA: 0x003082F5 File Offset: 0x003064F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent5Color>(deep);
		}

		// Token: 0x040086A9 RID: 34473
		private const string tagName = "accent5";

		// Token: 0x040086AA RID: 34474
		private const byte tagNsId = 10;

		// Token: 0x040086AB RID: 34475
		internal const int ElementTypeIdConst = 10155;
	}
}
