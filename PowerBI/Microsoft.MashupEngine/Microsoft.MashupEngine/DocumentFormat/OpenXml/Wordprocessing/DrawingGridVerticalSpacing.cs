using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F8F RID: 12175
	[GeneratedCode("DomGen", "2.0")]
	internal class DrawingGridVerticalSpacing : TwipsMeasureType
	{
		// Token: 0x170091C5 RID: 37317
		// (get) Token: 0x0601A3C5 RID: 107461 RVA: 0x0035F586 File Offset: 0x0035D786
		public override string LocalName
		{
			get
			{
				return "drawingGridVerticalSpacing";
			}
		}

		// Token: 0x170091C6 RID: 37318
		// (get) Token: 0x0601A3C6 RID: 107462 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091C7 RID: 37319
		// (get) Token: 0x0601A3C7 RID: 107463 RVA: 0x0035F58D File Offset: 0x0035D78D
		internal override int ElementTypeId
		{
			get
			{
				return 12010;
			}
		}

		// Token: 0x0601A3C8 RID: 107464 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3CA RID: 107466 RVA: 0x0035F594 File Offset: 0x0035D794
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DrawingGridVerticalSpacing>(deep);
		}

		// Token: 0x0400AC5C RID: 44124
		private const string tagName = "drawingGridVerticalSpacing";

		// Token: 0x0400AC5D RID: 44125
		private const byte tagNsId = 23;

		// Token: 0x0400AC5E RID: 44126
		internal const int ElementTypeIdConst = 12010;
	}
}
