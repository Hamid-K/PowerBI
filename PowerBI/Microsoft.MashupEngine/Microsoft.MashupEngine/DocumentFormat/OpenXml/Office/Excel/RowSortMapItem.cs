using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x02002384 RID: 9092
	[GeneratedCode("DomGen", "2.0")]
	internal class RowSortMapItem : SortMapItemType
	{
		// Token: 0x17004B62 RID: 19298
		// (get) Token: 0x060106A0 RID: 67232 RVA: 0x002E3583 File Offset: 0x002E1783
		public override string LocalName
		{
			get
			{
				return "row";
			}
		}

		// Token: 0x17004B63 RID: 19299
		// (get) Token: 0x060106A1 RID: 67233 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B64 RID: 19300
		// (get) Token: 0x060106A2 RID: 67234 RVA: 0x002E358A File Offset: 0x002E178A
		internal override int ElementTypeId
		{
			get
			{
				return 12535;
			}
		}

		// Token: 0x060106A3 RID: 67235 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060106A5 RID: 67237 RVA: 0x002E3599 File Offset: 0x002E1799
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowSortMapItem>(deep);
		}

		// Token: 0x04007480 RID: 29824
		private const string tagName = "row";

		// Token: 0x04007481 RID: 29825
		private const byte tagNsId = 32;

		// Token: 0x04007482 RID: 29826
		internal const int ElementTypeIdConst = 12535;
	}
}
