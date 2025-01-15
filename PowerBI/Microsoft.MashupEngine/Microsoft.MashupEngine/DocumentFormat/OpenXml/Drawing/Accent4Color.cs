using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002783 RID: 10115
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent4Color : Color2Type
	{
		// Token: 0x170061BF RID: 25023
		// (get) Token: 0x060138A5 RID: 80037 RVA: 0x003082D0 File Offset: 0x003064D0
		public override string LocalName
		{
			get
			{
				return "accent4";
			}
		}

		// Token: 0x170061C0 RID: 25024
		// (get) Token: 0x060138A6 RID: 80038 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061C1 RID: 25025
		// (get) Token: 0x060138A7 RID: 80039 RVA: 0x003082D7 File Offset: 0x003064D7
		internal override int ElementTypeId
		{
			get
			{
				return 10154;
			}
		}

		// Token: 0x060138A8 RID: 80040 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138A9 RID: 80041 RVA: 0x0030821A File Offset: 0x0030641A
		public Accent4Color()
		{
		}

		// Token: 0x060138AA RID: 80042 RVA: 0x00308222 File Offset: 0x00306422
		public Accent4Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138AB RID: 80043 RVA: 0x0030822B File Offset: 0x0030642B
		public Accent4Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138AC RID: 80044 RVA: 0x00308234 File Offset: 0x00306434
		public Accent4Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060138AD RID: 80045 RVA: 0x003082DE File Offset: 0x003064DE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent4Color>(deep);
		}

		// Token: 0x040086A6 RID: 34470
		private const string tagName = "accent4";

		// Token: 0x040086A7 RID: 34471
		private const byte tagNsId = 10;

		// Token: 0x040086A8 RID: 34472
		internal const int ElementTypeIdConst = 10154;
	}
}
