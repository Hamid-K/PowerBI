using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E32 RID: 11826
	[GeneratedCode("DomGen", "2.0")]
	internal class GridBefore : DecimalNumberType
	{
		// Token: 0x17008982 RID: 35202
		// (get) Token: 0x060191CD RID: 102861 RVA: 0x0034681E File Offset: 0x00344A1E
		public override string LocalName
		{
			get
			{
				return "gridBefore";
			}
		}

		// Token: 0x17008983 RID: 35203
		// (get) Token: 0x060191CE RID: 102862 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008984 RID: 35204
		// (get) Token: 0x060191CF RID: 102863 RVA: 0x00346825 File Offset: 0x00344A25
		internal override int ElementTypeId
		{
			get
			{
				return 11661;
			}
		}

		// Token: 0x060191D0 RID: 102864 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191D2 RID: 102866 RVA: 0x0034682C File Offset: 0x00344A2C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GridBefore>(deep);
		}

		// Token: 0x0400A714 RID: 42772
		private const string tagName = "gridBefore";

		// Token: 0x0400A715 RID: 42773
		private const byte tagNsId = 23;

		// Token: 0x0400A716 RID: 42774
		internal const int ElementTypeIdConst = 11661;
	}
}
