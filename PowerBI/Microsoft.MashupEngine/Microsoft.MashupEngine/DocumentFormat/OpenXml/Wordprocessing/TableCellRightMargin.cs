using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FFF RID: 12287
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellRightMargin : TableWidthDxaNilType
	{
		// Token: 0x170095DE RID: 38366
		// (get) Token: 0x0601AC77 RID: 109687 RVA: 0x002BF396 File Offset: 0x002BD596
		public override string LocalName
		{
			get
			{
				return "right";
			}
		}

		// Token: 0x170095DF RID: 38367
		// (get) Token: 0x0601AC78 RID: 109688 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095E0 RID: 38368
		// (get) Token: 0x0601AC79 RID: 109689 RVA: 0x0036777D File Offset: 0x0036597D
		internal override int ElementTypeId
		{
			get
			{
				return 12129;
			}
		}

		// Token: 0x0601AC7A RID: 109690 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AC7C RID: 109692 RVA: 0x00367784 File Offset: 0x00365984
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellRightMargin>(deep);
		}

		// Token: 0x0400AE4A RID: 44618
		private const string tagName = "right";

		// Token: 0x0400AE4B RID: 44619
		private const byte tagNsId = 23;

		// Token: 0x0400AE4C RID: 44620
		internal const int ElementTypeIdConst = 12129;
	}
}
