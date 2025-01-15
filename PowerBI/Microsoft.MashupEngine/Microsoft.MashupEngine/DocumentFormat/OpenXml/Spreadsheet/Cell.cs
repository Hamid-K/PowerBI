using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC3 RID: 11203
	[GeneratedCode("DomGen", "2.0")]
	internal class Cell : CellType
	{
		// Token: 0x17007C8E RID: 31886
		// (get) Token: 0x0601753D RID: 95549 RVA: 0x0032D4D3 File Offset: 0x0032B6D3
		public override string LocalName
		{
			get
			{
				return "c";
			}
		}

		// Token: 0x17007C8F RID: 31887
		// (get) Token: 0x0601753E RID: 95550 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C90 RID: 31888
		// (get) Token: 0x0601753F RID: 95551 RVA: 0x003357A0 File Offset: 0x003339A0
		internal override int ElementTypeId
		{
			get
			{
				return 11385;
			}
		}

		// Token: 0x06017540 RID: 95552 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017541 RID: 95553 RVA: 0x00335774 File Offset: 0x00333974
		public Cell()
		{
		}

		// Token: 0x06017542 RID: 95554 RVA: 0x0033577C File Offset: 0x0033397C
		public Cell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017543 RID: 95555 RVA: 0x00335785 File Offset: 0x00333985
		public Cell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017544 RID: 95556 RVA: 0x0033578E File Offset: 0x0033398E
		public Cell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017545 RID: 95557 RVA: 0x003357A7 File Offset: 0x003339A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Cell>(deep);
		}

		// Token: 0x04009BF5 RID: 39925
		private const string tagName = "c";

		// Token: 0x04009BF6 RID: 39926
		private const byte tagNsId = 22;

		// Token: 0x04009BF7 RID: 39927
		internal const int ElementTypeIdConst = 11385;
	}
}
