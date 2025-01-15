using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EDF RID: 11999
	[GeneratedCode("DomGen", "2.0")]
	internal class TableWidth : TableWidthType
	{
		// Token: 0x17008D3E RID: 36158
		// (get) Token: 0x060199C9 RID: 104905 RVA: 0x00353449 File Offset: 0x00351649
		public override string LocalName
		{
			get
			{
				return "tblW";
			}
		}

		// Token: 0x17008D3F RID: 36159
		// (get) Token: 0x060199CA RID: 104906 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D40 RID: 36160
		// (get) Token: 0x060199CB RID: 104907 RVA: 0x00353450 File Offset: 0x00351650
		internal override int ElementTypeId
		{
			get
			{
				return 11677;
			}
		}

		// Token: 0x060199CC RID: 104908 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199CE RID: 104910 RVA: 0x00353457 File Offset: 0x00351657
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableWidth>(deep);
		}

		// Token: 0x0400A9A5 RID: 43429
		private const string tagName = "tblW";

		// Token: 0x0400A9A6 RID: 43430
		private const byte tagNsId = 23;

		// Token: 0x0400A9A7 RID: 43431
		internal const int ElementTypeIdConst = 11677;
	}
}
