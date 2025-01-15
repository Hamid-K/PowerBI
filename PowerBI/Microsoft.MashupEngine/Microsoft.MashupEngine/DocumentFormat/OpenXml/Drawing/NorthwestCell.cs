using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002811 RID: 10257
	[GeneratedCode("DomGen", "2.0")]
	internal class NorthwestCell : TablePartStyleType
	{
		// Token: 0x17006562 RID: 25954
		// (get) Token: 0x060140F0 RID: 82160 RVA: 0x0030EB8C File Offset: 0x0030CD8C
		public override string LocalName
		{
			get
			{
				return "nwCell";
			}
		}

		// Token: 0x17006563 RID: 25955
		// (get) Token: 0x060140F1 RID: 82161 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006564 RID: 25956
		// (get) Token: 0x060140F2 RID: 82162 RVA: 0x0030EB93 File Offset: 0x0030CD93
		internal override int ElementTypeId
		{
			get
			{
				return 10292;
			}
		}

		// Token: 0x060140F3 RID: 82163 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140F4 RID: 82164 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public NorthwestCell()
		{
		}

		// Token: 0x060140F5 RID: 82165 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public NorthwestCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140F6 RID: 82166 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public NorthwestCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140F7 RID: 82167 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public NorthwestCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140F8 RID: 82168 RVA: 0x0030EB9A File Offset: 0x0030CD9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NorthwestCell>(deep);
		}

		// Token: 0x040088DC RID: 35036
		private const string tagName = "nwCell";

		// Token: 0x040088DD RID: 35037
		private const byte tagNsId = 10;

		// Token: 0x040088DE RID: 35038
		internal const int ElementTypeIdConst = 10292;
	}
}
