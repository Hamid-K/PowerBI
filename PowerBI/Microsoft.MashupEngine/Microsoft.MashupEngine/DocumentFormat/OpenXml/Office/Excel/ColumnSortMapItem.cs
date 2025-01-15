using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x02002385 RID: 9093
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnSortMapItem : SortMapItemType
	{
		// Token: 0x17004B65 RID: 19301
		// (get) Token: 0x060106A6 RID: 67238 RVA: 0x002E35A2 File Offset: 0x002E17A2
		public override string LocalName
		{
			get
			{
				return "col";
			}
		}

		// Token: 0x17004B66 RID: 19302
		// (get) Token: 0x060106A7 RID: 67239 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B67 RID: 19303
		// (get) Token: 0x060106A8 RID: 67240 RVA: 0x002E35A9 File Offset: 0x002E17A9
		internal override int ElementTypeId
		{
			get
			{
				return 12536;
			}
		}

		// Token: 0x060106A9 RID: 67241 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060106AB RID: 67243 RVA: 0x002E35B0 File Offset: 0x002E17B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnSortMapItem>(deep);
		}

		// Token: 0x04007483 RID: 29827
		private const string tagName = "col";

		// Token: 0x04007484 RID: 29828
		private const byte tagNsId = 32;

		// Token: 0x04007485 RID: 29829
		internal const int ElementTypeIdConst = 12536;
	}
}
