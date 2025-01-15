using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200248C RID: 9356
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Shade : PositiveFixedPercentageType
	{
		// Token: 0x17005176 RID: 20854
		// (get) Token: 0x06011456 RID: 70742 RVA: 0x002EC997 File Offset: 0x002EAB97
		public override string LocalName
		{
			get
			{
				return "shade";
			}
		}

		// Token: 0x17005177 RID: 20855
		// (get) Token: 0x06011457 RID: 70743 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005178 RID: 20856
		// (get) Token: 0x06011458 RID: 70744 RVA: 0x002EC99E File Offset: 0x002EAB9E
		internal override int ElementTypeId
		{
			get
			{
				return 12833;
			}
		}

		// Token: 0x06011459 RID: 70745 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601145B RID: 70747 RVA: 0x002EC9A5 File Offset: 0x002EABA5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shade>(deep);
		}

		// Token: 0x040078FC RID: 30972
		private const string tagName = "shade";

		// Token: 0x040078FD RID: 30973
		private const byte tagNsId = 52;

		// Token: 0x040078FE RID: 30974
		internal const int ElementTypeIdConst = 12833;
	}
}
