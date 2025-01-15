using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D65 RID: 11621
	[GeneratedCode("DomGen", "2.0")]
	internal class DecimalSymbol : StringType
	{
		// Token: 0x170086CB RID: 34507
		// (get) Token: 0x06018C55 RID: 101461 RVA: 0x0034491D File Offset: 0x00342B1D
		public override string LocalName
		{
			get
			{
				return "decimalSymbol";
			}
		}

		// Token: 0x170086CC RID: 34508
		// (get) Token: 0x06018C56 RID: 101462 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086CD RID: 34509
		// (get) Token: 0x06018C57 RID: 101463 RVA: 0x00344924 File Offset: 0x00342B24
		internal override int ElementTypeId
		{
			get
			{
				return 12053;
			}
		}

		// Token: 0x06018C58 RID: 101464 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C5A RID: 101466 RVA: 0x0034492B File Offset: 0x00342B2B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DecimalSymbol>(deep);
		}

		// Token: 0x0400A49B RID: 42139
		private const string tagName = "decimalSymbol";

		// Token: 0x0400A49C RID: 42140
		private const byte tagNsId = 23;

		// Token: 0x0400A49D RID: 42141
		internal const int ElementTypeIdConst = 12053;
	}
}
