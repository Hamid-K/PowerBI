using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002640 RID: 9792
	[GeneratedCode("DomGen", "2.0")]
	internal class FromAnchor : MarkerType
	{
		// Token: 0x17005AD7 RID: 23255
		// (get) Token: 0x0601290A RID: 76042 RVA: 0x002FCA49 File Offset: 0x002FAC49
		public override string LocalName
		{
			get
			{
				return "from";
			}
		}

		// Token: 0x17005AD8 RID: 23256
		// (get) Token: 0x0601290B RID: 76043 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AD9 RID: 23257
		// (get) Token: 0x0601290C RID: 76044 RVA: 0x002FCA50 File Offset: 0x002FAC50
		internal override int ElementTypeId
		{
			get
			{
				return 10610;
			}
		}

		// Token: 0x0601290D RID: 76045 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601290E RID: 76046 RVA: 0x002FCA57 File Offset: 0x002FAC57
		public FromAnchor()
		{
		}

		// Token: 0x0601290F RID: 76047 RVA: 0x002FCA5F File Offset: 0x002FAC5F
		public FromAnchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012910 RID: 76048 RVA: 0x002FCA68 File Offset: 0x002FAC68
		public FromAnchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012911 RID: 76049 RVA: 0x002FCA71 File Offset: 0x002FAC71
		public FromAnchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012912 RID: 76050 RVA: 0x002FCA7A File Offset: 0x002FAC7A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FromAnchor>(deep);
		}

		// Token: 0x040080AF RID: 32943
		private const string tagName = "from";

		// Token: 0x040080B0 RID: 32944
		private const byte tagNsId = 12;

		// Token: 0x040080B1 RID: 32945
		internal const int ElementTypeIdConst = 10610;
	}
}
