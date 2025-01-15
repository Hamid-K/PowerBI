using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002492 RID: 9362
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SaturationModulation : PercentageType
	{
		// Token: 0x1700518B RID: 20875
		// (get) Token: 0x06011481 RID: 70785 RVA: 0x002ECA9A File Offset: 0x002EAC9A
		public override string LocalName
		{
			get
			{
				return "satMod";
			}
		}

		// Token: 0x1700518C RID: 20876
		// (get) Token: 0x06011482 RID: 70786 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700518D RID: 20877
		// (get) Token: 0x06011483 RID: 70787 RVA: 0x002ECAA1 File Offset: 0x002EACA1
		internal override int ElementTypeId
		{
			get
			{
				return 12838;
			}
		}

		// Token: 0x06011484 RID: 70788 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011486 RID: 70790 RVA: 0x002ECAA8 File Offset: 0x002EACA8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaturationModulation>(deep);
		}

		// Token: 0x0400790F RID: 30991
		private const string tagName = "satMod";

		// Token: 0x04007910 RID: 30992
		private const byte tagNsId = 52;

		// Token: 0x04007911 RID: 30993
		internal const int ElementTypeIdConst = 12838;
	}
}
