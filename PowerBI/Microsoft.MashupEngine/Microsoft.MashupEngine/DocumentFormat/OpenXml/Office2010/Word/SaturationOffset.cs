using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002491 RID: 9361
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SaturationOffset : PercentageType
	{
		// Token: 0x17005188 RID: 20872
		// (get) Token: 0x0601147B RID: 70779 RVA: 0x002ECA83 File Offset: 0x002EAC83
		public override string LocalName
		{
			get
			{
				return "satOff";
			}
		}

		// Token: 0x17005189 RID: 20873
		// (get) Token: 0x0601147C RID: 70780 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700518A RID: 20874
		// (get) Token: 0x0601147D RID: 70781 RVA: 0x002ECA8A File Offset: 0x002EAC8A
		internal override int ElementTypeId
		{
			get
			{
				return 12837;
			}
		}

		// Token: 0x0601147E RID: 70782 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011480 RID: 70784 RVA: 0x002ECA91 File Offset: 0x002EAC91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaturationOffset>(deep);
		}

		// Token: 0x0400790C RID: 30988
		private const string tagName = "satOff";

		// Token: 0x0400790D RID: 30989
		private const byte tagNsId = 52;

		// Token: 0x0400790E RID: 30990
		internal const int ElementTypeIdConst = 12837;
	}
}
