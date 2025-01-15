using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200248B RID: 9355
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Tint : PositiveFixedPercentageType
	{
		// Token: 0x17005173 RID: 20851
		// (get) Token: 0x06011450 RID: 70736 RVA: 0x002EC978 File Offset: 0x002EAB78
		public override string LocalName
		{
			get
			{
				return "tint";
			}
		}

		// Token: 0x17005174 RID: 20852
		// (get) Token: 0x06011451 RID: 70737 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005175 RID: 20853
		// (get) Token: 0x06011452 RID: 70738 RVA: 0x002EC97F File Offset: 0x002EAB7F
		internal override int ElementTypeId
		{
			get
			{
				return 12832;
			}
		}

		// Token: 0x06011453 RID: 70739 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011455 RID: 70741 RVA: 0x002EC98E File Offset: 0x002EAB8E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tint>(deep);
		}

		// Token: 0x040078F9 RID: 30969
		private const string tagName = "tint";

		// Token: 0x040078FA RID: 30970
		private const byte tagNsId = 52;

		// Token: 0x040078FB RID: 30971
		internal const int ElementTypeIdConst = 12832;
	}
}
