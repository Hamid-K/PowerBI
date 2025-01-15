using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FFE RID: 12286
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellLeftMargin : TableWidthDxaNilType
	{
		// Token: 0x170095DB RID: 38363
		// (get) Token: 0x0601AC71 RID: 109681 RVA: 0x002BF360 File Offset: 0x002BD560
		public override string LocalName
		{
			get
			{
				return "left";
			}
		}

		// Token: 0x170095DC RID: 38364
		// (get) Token: 0x0601AC72 RID: 109682 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095DD RID: 38365
		// (get) Token: 0x0601AC73 RID: 109683 RVA: 0x00367765 File Offset: 0x00365965
		internal override int ElementTypeId
		{
			get
			{
				return 12126;
			}
		}

		// Token: 0x0601AC74 RID: 109684 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AC76 RID: 109686 RVA: 0x00367774 File Offset: 0x00365974
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellLeftMargin>(deep);
		}

		// Token: 0x0400AE47 RID: 44615
		private const string tagName = "left";

		// Token: 0x0400AE48 RID: 44616
		private const byte tagNsId = 23;

		// Token: 0x0400AE49 RID: 44617
		internal const int ElementTypeIdConst = 12126;
	}
}
