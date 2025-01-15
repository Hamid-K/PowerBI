using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F90 RID: 12176
	[GeneratedCode("DomGen", "2.0")]
	internal class DrawingGridHorizontalOrigin : TwipsMeasureType
	{
		// Token: 0x170091C8 RID: 37320
		// (get) Token: 0x0601A3CB RID: 107467 RVA: 0x0035F59D File Offset: 0x0035D79D
		public override string LocalName
		{
			get
			{
				return "drawingGridHorizontalOrigin";
			}
		}

		// Token: 0x170091C9 RID: 37321
		// (get) Token: 0x0601A3CC RID: 107468 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091CA RID: 37322
		// (get) Token: 0x0601A3CD RID: 107469 RVA: 0x0035F5A4 File Offset: 0x0035D7A4
		internal override int ElementTypeId
		{
			get
			{
				return 12014;
			}
		}

		// Token: 0x0601A3CE RID: 107470 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3D0 RID: 107472 RVA: 0x0035F5AB File Offset: 0x0035D7AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DrawingGridHorizontalOrigin>(deep);
		}

		// Token: 0x0400AC5F RID: 44127
		private const string tagName = "drawingGridHorizontalOrigin";

		// Token: 0x0400AC60 RID: 44128
		private const byte tagNsId = 23;

		// Token: 0x0400AC61 RID: 44129
		internal const int ElementTypeIdConst = 12014;
	}
}
