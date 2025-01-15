using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E0F RID: 11791
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotUseEastAsianBreakRules : OnOffType
	{
		// Token: 0x170088C9 RID: 35017
		// (get) Token: 0x06019052 RID: 102482 RVA: 0x00345866 File Offset: 0x00343A66
		public override string LocalName
		{
			get
			{
				return "doNotUseEastAsianBreakRules";
			}
		}

		// Token: 0x170088CA RID: 35018
		// (get) Token: 0x06019053 RID: 102483 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088CB RID: 35019
		// (get) Token: 0x06019054 RID: 102484 RVA: 0x0034586D File Offset: 0x00343A6D
		internal override int ElementTypeId
		{
			get
			{
				return 12101;
			}
		}

		// Token: 0x06019055 RID: 102485 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019057 RID: 102487 RVA: 0x00345874 File Offset: 0x00343A74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotUseEastAsianBreakRules>(deep);
		}

		// Token: 0x0400A698 RID: 42648
		private const string tagName = "doNotUseEastAsianBreakRules";

		// Token: 0x0400A699 RID: 42649
		private const byte tagNsId = 23;

		// Token: 0x0400A69A RID: 42650
		internal const int ElementTypeIdConst = 12101;
	}
}
