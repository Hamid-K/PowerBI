using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002494 RID: 9364
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class LuminanceOffset : PercentageType
	{
		// Token: 0x17005191 RID: 20881
		// (get) Token: 0x0601148D RID: 70797 RVA: 0x002ECAC8 File Offset: 0x002EACC8
		public override string LocalName
		{
			get
			{
				return "lumOff";
			}
		}

		// Token: 0x17005192 RID: 20882
		// (get) Token: 0x0601148E RID: 70798 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005193 RID: 20883
		// (get) Token: 0x0601148F RID: 70799 RVA: 0x002ECACF File Offset: 0x002EACCF
		internal override int ElementTypeId
		{
			get
			{
				return 12840;
			}
		}

		// Token: 0x06011490 RID: 70800 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011492 RID: 70802 RVA: 0x002ECAD6 File Offset: 0x002EACD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LuminanceOffset>(deep);
		}

		// Token: 0x04007915 RID: 30997
		private const string tagName = "lumOff";

		// Token: 0x04007916 RID: 30998
		private const byte tagNsId = 52;

		// Token: 0x04007917 RID: 30999
		internal const int ElementTypeIdConst = 12840;
	}
}
