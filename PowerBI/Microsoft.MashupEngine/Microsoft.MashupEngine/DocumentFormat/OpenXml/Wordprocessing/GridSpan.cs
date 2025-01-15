using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E31 RID: 11825
	[GeneratedCode("DomGen", "2.0")]
	internal class GridSpan : DecimalNumberType
	{
		// Token: 0x1700897F RID: 35199
		// (get) Token: 0x060191C7 RID: 102855 RVA: 0x00346807 File Offset: 0x00344A07
		public override string LocalName
		{
			get
			{
				return "gridSpan";
			}
		}

		// Token: 0x17008980 RID: 35200
		// (get) Token: 0x060191C8 RID: 102856 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008981 RID: 35201
		// (get) Token: 0x060191C9 RID: 102857 RVA: 0x0034680E File Offset: 0x00344A0E
		internal override int ElementTypeId
		{
			get
			{
				return 11651;
			}
		}

		// Token: 0x060191CA RID: 102858 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191CC RID: 102860 RVA: 0x00346815 File Offset: 0x00344A15
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GridSpan>(deep);
		}

		// Token: 0x0400A711 RID: 42769
		private const string tagName = "gridSpan";

		// Token: 0x0400A712 RID: 42770
		private const byte tagNsId = 23;

		// Token: 0x0400A713 RID: 42771
		internal const int ElementTypeIdConst = 11651;
	}
}
