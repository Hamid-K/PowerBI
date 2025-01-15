using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200280D RID: 10253
	[GeneratedCode("DomGen", "2.0")]
	internal class SoutheastCell : TablePartStyleType
	{
		// Token: 0x17006556 RID: 25942
		// (get) Token: 0x060140CC RID: 82124 RVA: 0x0030EB30 File Offset: 0x0030CD30
		public override string LocalName
		{
			get
			{
				return "seCell";
			}
		}

		// Token: 0x17006557 RID: 25943
		// (get) Token: 0x060140CD RID: 82125 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006558 RID: 25944
		// (get) Token: 0x060140CE RID: 82126 RVA: 0x0030EB37 File Offset: 0x0030CD37
		internal override int ElementTypeId
		{
			get
			{
				return 10288;
			}
		}

		// Token: 0x060140CF RID: 82127 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140D0 RID: 82128 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public SoutheastCell()
		{
		}

		// Token: 0x060140D1 RID: 82129 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public SoutheastCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140D2 RID: 82130 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public SoutheastCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140D3 RID: 82131 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public SoutheastCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140D4 RID: 82132 RVA: 0x0030EB3E File Offset: 0x0030CD3E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SoutheastCell>(deep);
		}

		// Token: 0x040088D0 RID: 35024
		private const string tagName = "seCell";

		// Token: 0x040088D1 RID: 35025
		private const byte tagNsId = 10;

		// Token: 0x040088D2 RID: 35026
		internal const int ElementTypeIdConst = 10288;
	}
}
