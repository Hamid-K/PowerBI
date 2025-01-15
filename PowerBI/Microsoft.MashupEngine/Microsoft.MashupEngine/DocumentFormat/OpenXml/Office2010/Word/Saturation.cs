using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002490 RID: 9360
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Saturation : PercentageType
	{
		// Token: 0x17005185 RID: 20869
		// (get) Token: 0x06011475 RID: 70773 RVA: 0x002ECA64 File Offset: 0x002EAC64
		public override string LocalName
		{
			get
			{
				return "sat";
			}
		}

		// Token: 0x17005186 RID: 20870
		// (get) Token: 0x06011476 RID: 70774 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005187 RID: 20871
		// (get) Token: 0x06011477 RID: 70775 RVA: 0x002ECA6B File Offset: 0x002EAC6B
		internal override int ElementTypeId
		{
			get
			{
				return 12836;
			}
		}

		// Token: 0x06011478 RID: 70776 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601147A RID: 70778 RVA: 0x002ECA7A File Offset: 0x002EAC7A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Saturation>(deep);
		}

		// Token: 0x04007909 RID: 30985
		private const string tagName = "sat";

		// Token: 0x0400790A RID: 30986
		private const byte tagNsId = 52;

		// Token: 0x0400790B RID: 30987
		internal const int ElementTypeIdConst = 12836;
	}
}
