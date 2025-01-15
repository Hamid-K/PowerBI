using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002495 RID: 9365
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class LuminanceModulation : PercentageType
	{
		// Token: 0x17005194 RID: 20884
		// (get) Token: 0x06011493 RID: 70803 RVA: 0x002ECADF File Offset: 0x002EACDF
		public override string LocalName
		{
			get
			{
				return "lumMod";
			}
		}

		// Token: 0x17005195 RID: 20885
		// (get) Token: 0x06011494 RID: 70804 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005196 RID: 20886
		// (get) Token: 0x06011495 RID: 70805 RVA: 0x002ECAE6 File Offset: 0x002EACE6
		internal override int ElementTypeId
		{
			get
			{
				return 12841;
			}
		}

		// Token: 0x06011496 RID: 70806 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011498 RID: 70808 RVA: 0x002ECAED File Offset: 0x002EACED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LuminanceModulation>(deep);
		}

		// Token: 0x04007918 RID: 31000
		private const string tagName = "lumMod";

		// Token: 0x04007919 RID: 31001
		private const byte tagNsId = 52;

		// Token: 0x0400791A RID: 31002
		internal const int ElementTypeIdConst = 12841;
	}
}
