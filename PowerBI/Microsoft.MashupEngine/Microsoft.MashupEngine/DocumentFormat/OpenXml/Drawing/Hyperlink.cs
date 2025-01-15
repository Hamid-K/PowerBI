using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002786 RID: 10118
	[GeneratedCode("DomGen", "2.0")]
	internal class Hyperlink : Color2Type
	{
		// Token: 0x170061C8 RID: 25032
		// (get) Token: 0x060138C0 RID: 80064 RVA: 0x00308315 File Offset: 0x00306515
		public override string LocalName
		{
			get
			{
				return "hlink";
			}
		}

		// Token: 0x170061C9 RID: 25033
		// (get) Token: 0x060138C1 RID: 80065 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061CA RID: 25034
		// (get) Token: 0x060138C2 RID: 80066 RVA: 0x0030831C File Offset: 0x0030651C
		internal override int ElementTypeId
		{
			get
			{
				return 10157;
			}
		}

		// Token: 0x060138C3 RID: 80067 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138C4 RID: 80068 RVA: 0x0030821A File Offset: 0x0030641A
		public Hyperlink()
		{
		}

		// Token: 0x060138C5 RID: 80069 RVA: 0x00308222 File Offset: 0x00306422
		public Hyperlink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138C6 RID: 80070 RVA: 0x0030822B File Offset: 0x0030642B
		public Hyperlink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138C7 RID: 80071 RVA: 0x00308234 File Offset: 0x00306434
		public Hyperlink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060138C8 RID: 80072 RVA: 0x00308323 File Offset: 0x00306523
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hyperlink>(deep);
		}

		// Token: 0x040086AF RID: 34479
		private const string tagName = "hlink";

		// Token: 0x040086B0 RID: 34480
		private const byte tagNsId = 10;

		// Token: 0x040086B1 RID: 34481
		internal const int ElementTypeIdConst = 10157;
	}
}
