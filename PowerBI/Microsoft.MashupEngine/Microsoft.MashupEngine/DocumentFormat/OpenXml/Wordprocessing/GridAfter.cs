using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E33 RID: 11827
	[GeneratedCode("DomGen", "2.0")]
	internal class GridAfter : DecimalNumberType
	{
		// Token: 0x17008985 RID: 35205
		// (get) Token: 0x060191D3 RID: 102867 RVA: 0x00346835 File Offset: 0x00344A35
		public override string LocalName
		{
			get
			{
				return "gridAfter";
			}
		}

		// Token: 0x17008986 RID: 35206
		// (get) Token: 0x060191D4 RID: 102868 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008987 RID: 35207
		// (get) Token: 0x060191D5 RID: 102869 RVA: 0x0034683C File Offset: 0x00344A3C
		internal override int ElementTypeId
		{
			get
			{
				return 11662;
			}
		}

		// Token: 0x060191D6 RID: 102870 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191D8 RID: 102872 RVA: 0x00346843 File Offset: 0x00344A43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GridAfter>(deep);
		}

		// Token: 0x0400A717 RID: 42775
		private const string tagName = "gridAfter";

		// Token: 0x0400A718 RID: 42776
		private const byte tagNsId = 23;

		// Token: 0x0400A719 RID: 42777
		internal const int ElementTypeIdConst = 11662;
	}
}
