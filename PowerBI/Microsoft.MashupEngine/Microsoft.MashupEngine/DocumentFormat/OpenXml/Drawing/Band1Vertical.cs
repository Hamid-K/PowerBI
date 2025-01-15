using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002808 RID: 10248
	[GeneratedCode("DomGen", "2.0")]
	internal class Band1Vertical : TablePartStyleType
	{
		// Token: 0x17006547 RID: 25927
		// (get) Token: 0x0601409F RID: 82079 RVA: 0x0030EABD File Offset: 0x0030CCBD
		public override string LocalName
		{
			get
			{
				return "band1V";
			}
		}

		// Token: 0x17006548 RID: 25928
		// (get) Token: 0x060140A0 RID: 82080 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006549 RID: 25929
		// (get) Token: 0x060140A1 RID: 82081 RVA: 0x0030EAC4 File Offset: 0x0030CCC4
		internal override int ElementTypeId
		{
			get
			{
				return 10283;
			}
		}

		// Token: 0x060140A2 RID: 82082 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140A3 RID: 82083 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public Band1Vertical()
		{
		}

		// Token: 0x060140A4 RID: 82084 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public Band1Vertical(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140A5 RID: 82085 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public Band1Vertical(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140A6 RID: 82086 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public Band1Vertical(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140A7 RID: 82087 RVA: 0x0030EACB File Offset: 0x0030CCCB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Band1Vertical>(deep);
		}

		// Token: 0x040088C1 RID: 35009
		private const string tagName = "band1V";

		// Token: 0x040088C2 RID: 35010
		private const byte tagNsId = 10;

		// Token: 0x040088C3 RID: 35011
		internal const int ElementTypeIdConst = 10283;
	}
}
