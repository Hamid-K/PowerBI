using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002406 RID: 9222
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class FillColor : ColorType
	{
		// Token: 0x17004EBA RID: 20154
		// (get) Token: 0x06010DFD RID: 69117 RVA: 0x002E8496 File Offset: 0x002E6696
		public override string LocalName
		{
			get
			{
				return "fillColor";
			}
		}

		// Token: 0x17004EBB RID: 20155
		// (get) Token: 0x06010DFE RID: 69118 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EBC RID: 20156
		// (get) Token: 0x06010DFF RID: 69119 RVA: 0x002E849D File Offset: 0x002E669D
		internal override int ElementTypeId
		{
			get
			{
				return 12967;
			}
		}

		// Token: 0x06010E00 RID: 69120 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E02 RID: 69122 RVA: 0x002E84A4 File Offset: 0x002E66A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillColor>(deep);
		}

		// Token: 0x040076A1 RID: 30369
		private const string tagName = "fillColor";

		// Token: 0x040076A2 RID: 30370
		private const byte tagNsId = 53;

		// Token: 0x040076A3 RID: 30371
		internal const int ElementTypeIdConst = 12967;
	}
}
