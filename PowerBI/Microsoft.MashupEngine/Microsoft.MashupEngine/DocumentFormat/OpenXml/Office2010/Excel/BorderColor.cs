using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002407 RID: 9223
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BorderColor : ColorType
	{
		// Token: 0x17004EBD RID: 20157
		// (get) Token: 0x06010E03 RID: 69123 RVA: 0x002E84AD File Offset: 0x002E66AD
		public override string LocalName
		{
			get
			{
				return "borderColor";
			}
		}

		// Token: 0x17004EBE RID: 20158
		// (get) Token: 0x06010E04 RID: 69124 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EBF RID: 20159
		// (get) Token: 0x06010E05 RID: 69125 RVA: 0x002E84B4 File Offset: 0x002E66B4
		internal override int ElementTypeId
		{
			get
			{
				return 12968;
			}
		}

		// Token: 0x06010E06 RID: 69126 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E08 RID: 69128 RVA: 0x002E84BB File Offset: 0x002E66BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BorderColor>(deep);
		}

		// Token: 0x040076A4 RID: 30372
		private const string tagName = "borderColor";

		// Token: 0x040076A5 RID: 30373
		private const byte tagNsId = 53;

		// Token: 0x040076A6 RID: 30374
		internal const int ElementTypeIdConst = 12968;
	}
}
