using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D57 RID: 11607
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCaption : StringType
	{
		// Token: 0x170086A1 RID: 34465
		// (get) Token: 0x06018C01 RID: 101377 RVA: 0x003447FE File Offset: 0x003429FE
		public override string LocalName
		{
			get
			{
				return "tblCaption";
			}
		}

		// Token: 0x170086A2 RID: 34466
		// (get) Token: 0x06018C02 RID: 101378 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086A3 RID: 34467
		// (get) Token: 0x06018C03 RID: 101379 RVA: 0x00344805 File Offset: 0x00342A05
		internal override int ElementTypeId
		{
			get
			{
				return 11785;
			}
		}

		// Token: 0x06018C04 RID: 101380 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C06 RID: 101382 RVA: 0x0034480C File Offset: 0x00342A0C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCaption>(deep);
		}

		// Token: 0x0400A471 RID: 42097
		private const string tagName = "tblCaption";

		// Token: 0x0400A472 RID: 42098
		private const byte tagNsId = 23;

		// Token: 0x0400A473 RID: 42099
		internal const int ElementTypeIdConst = 11785;
	}
}
