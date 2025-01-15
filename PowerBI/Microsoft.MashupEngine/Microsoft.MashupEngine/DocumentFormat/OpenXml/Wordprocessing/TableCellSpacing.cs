using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EDE RID: 11998
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellSpacing : TableWidthType
	{
		// Token: 0x17008D3B RID: 36155
		// (get) Token: 0x060199C3 RID: 104899 RVA: 0x00353432 File Offset: 0x00351632
		public override string LocalName
		{
			get
			{
				return "tblCellSpacing";
			}
		}

		// Token: 0x17008D3C RID: 36156
		// (get) Token: 0x060199C4 RID: 104900 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D3D RID: 36157
		// (get) Token: 0x060199C5 RID: 104901 RVA: 0x00353439 File Offset: 0x00351639
		internal override int ElementTypeId
		{
			get
			{
				return 11669;
			}
		}

		// Token: 0x060199C6 RID: 104902 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199C8 RID: 104904 RVA: 0x00353440 File Offset: 0x00351640
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellSpacing>(deep);
		}

		// Token: 0x0400A9A2 RID: 43426
		private const string tagName = "tblCellSpacing";

		// Token: 0x0400A9A3 RID: 43427
		private const byte tagNsId = 23;

		// Token: 0x0400A9A4 RID: 43428
		internal const int ElementTypeIdConst = 11669;
	}
}
