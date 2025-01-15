using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200248D RID: 9357
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Alpha : PositiveFixedPercentageType
	{
		// Token: 0x17005179 RID: 20857
		// (get) Token: 0x0601145C RID: 70748 RVA: 0x002EC9AE File Offset: 0x002EABAE
		public override string LocalName
		{
			get
			{
				return "alpha";
			}
		}

		// Token: 0x1700517A RID: 20858
		// (get) Token: 0x0601145D RID: 70749 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700517B RID: 20859
		// (get) Token: 0x0601145E RID: 70750 RVA: 0x002EC9B5 File Offset: 0x002EABB5
		internal override int ElementTypeId
		{
			get
			{
				return 12834;
			}
		}

		// Token: 0x0601145F RID: 70751 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011461 RID: 70753 RVA: 0x002EC9BC File Offset: 0x002EABBC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Alpha>(deep);
		}

		// Token: 0x040078FF RID: 30975
		private const string tagName = "alpha";

		// Token: 0x04007900 RID: 30976
		private const byte tagNsId = 52;

		// Token: 0x04007901 RID: 30977
		internal const int ElementTypeIdConst = 12834;
	}
}
