using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F8E RID: 12174
	[GeneratedCode("DomGen", "2.0")]
	internal class DrawingGridHorizontalSpacing : TwipsMeasureType
	{
		// Token: 0x170091C2 RID: 37314
		// (get) Token: 0x0601A3BF RID: 107455 RVA: 0x0035F56F File Offset: 0x0035D76F
		public override string LocalName
		{
			get
			{
				return "drawingGridHorizontalSpacing";
			}
		}

		// Token: 0x170091C3 RID: 37315
		// (get) Token: 0x0601A3C0 RID: 107456 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091C4 RID: 37316
		// (get) Token: 0x0601A3C1 RID: 107457 RVA: 0x0035F576 File Offset: 0x0035D776
		internal override int ElementTypeId
		{
			get
			{
				return 12009;
			}
		}

		// Token: 0x0601A3C2 RID: 107458 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3C4 RID: 107460 RVA: 0x0035F57D File Offset: 0x0035D77D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DrawingGridHorizontalSpacing>(deep);
		}

		// Token: 0x0400AC59 RID: 44121
		private const string tagName = "drawingGridHorizontalSpacing";

		// Token: 0x0400AC5A RID: 44122
		private const byte tagNsId = 23;

		// Token: 0x0400AC5B RID: 44123
		internal const int ElementTypeIdConst = 12009;
	}
}
