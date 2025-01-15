using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002780 RID: 10112
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent1Color : Color2Type
	{
		// Token: 0x170061B6 RID: 25014
		// (get) Token: 0x0601388A RID: 80010 RVA: 0x0030828B File Offset: 0x0030648B
		public override string LocalName
		{
			get
			{
				return "accent1";
			}
		}

		// Token: 0x170061B7 RID: 25015
		// (get) Token: 0x0601388B RID: 80011 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061B8 RID: 25016
		// (get) Token: 0x0601388C RID: 80012 RVA: 0x00308292 File Offset: 0x00306492
		internal override int ElementTypeId
		{
			get
			{
				return 10151;
			}
		}

		// Token: 0x0601388D RID: 80013 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601388E RID: 80014 RVA: 0x0030821A File Offset: 0x0030641A
		public Accent1Color()
		{
		}

		// Token: 0x0601388F RID: 80015 RVA: 0x00308222 File Offset: 0x00306422
		public Accent1Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013890 RID: 80016 RVA: 0x0030822B File Offset: 0x0030642B
		public Accent1Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013891 RID: 80017 RVA: 0x00308234 File Offset: 0x00306434
		public Accent1Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013892 RID: 80018 RVA: 0x00308299 File Offset: 0x00306499
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent1Color>(deep);
		}

		// Token: 0x0400869D RID: 34461
		private const string tagName = "accent1";

		// Token: 0x0400869E RID: 34462
		private const byte tagNsId = 10;

		// Token: 0x0400869F RID: 34463
		internal const int ElementTypeIdConst = 10151;
	}
}
