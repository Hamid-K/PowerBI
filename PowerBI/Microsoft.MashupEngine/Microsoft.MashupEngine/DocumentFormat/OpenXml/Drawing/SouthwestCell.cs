using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200280E RID: 10254
	[GeneratedCode("DomGen", "2.0")]
	internal class SouthwestCell : TablePartStyleType
	{
		// Token: 0x17006559 RID: 25945
		// (get) Token: 0x060140D5 RID: 82133 RVA: 0x0030EB47 File Offset: 0x0030CD47
		public override string LocalName
		{
			get
			{
				return "swCell";
			}
		}

		// Token: 0x1700655A RID: 25946
		// (get) Token: 0x060140D6 RID: 82134 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700655B RID: 25947
		// (get) Token: 0x060140D7 RID: 82135 RVA: 0x0030EB4E File Offset: 0x0030CD4E
		internal override int ElementTypeId
		{
			get
			{
				return 10289;
			}
		}

		// Token: 0x060140D8 RID: 82136 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140D9 RID: 82137 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public SouthwestCell()
		{
		}

		// Token: 0x060140DA RID: 82138 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public SouthwestCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140DB RID: 82139 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public SouthwestCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140DC RID: 82140 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public SouthwestCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140DD RID: 82141 RVA: 0x0030EB55 File Offset: 0x0030CD55
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SouthwestCell>(deep);
		}

		// Token: 0x040088D3 RID: 35027
		private const string tagName = "swCell";

		// Token: 0x040088D4 RID: 35028
		private const byte tagNsId = 10;

		// Token: 0x040088D5 RID: 35029
		internal const int ElementTypeIdConst = 10289;
	}
}
