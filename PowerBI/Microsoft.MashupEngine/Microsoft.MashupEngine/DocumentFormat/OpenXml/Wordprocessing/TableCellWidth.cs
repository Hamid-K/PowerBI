using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EDB RID: 11995
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellWidth : TableWidthType
	{
		// Token: 0x17008D32 RID: 36146
		// (get) Token: 0x060199B1 RID: 104881 RVA: 0x003533E5 File Offset: 0x003515E5
		public override string LocalName
		{
			get
			{
				return "tcW";
			}
		}

		// Token: 0x17008D33 RID: 36147
		// (get) Token: 0x060199B2 RID: 104882 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D34 RID: 36148
		// (get) Token: 0x060199B3 RID: 104883 RVA: 0x003533EC File Offset: 0x003515EC
		internal override int ElementTypeId
		{
			get
			{
				return 11650;
			}
		}

		// Token: 0x060199B4 RID: 104884 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199B6 RID: 104886 RVA: 0x003533FB File Offset: 0x003515FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellWidth>(deep);
		}

		// Token: 0x0400A999 RID: 43417
		private const string tagName = "tcW";

		// Token: 0x0400A99A RID: 43418
		private const byte tagNsId = 23;

		// Token: 0x0400A99B RID: 43419
		internal const int ElementTypeIdConst = 11650;
	}
}
