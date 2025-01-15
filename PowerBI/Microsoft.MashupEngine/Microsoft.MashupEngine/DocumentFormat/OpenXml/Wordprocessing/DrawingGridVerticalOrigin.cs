using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F91 RID: 12177
	[GeneratedCode("DomGen", "2.0")]
	internal class DrawingGridVerticalOrigin : TwipsMeasureType
	{
		// Token: 0x170091CB RID: 37323
		// (get) Token: 0x0601A3D1 RID: 107473 RVA: 0x0035F5B4 File Offset: 0x0035D7B4
		public override string LocalName
		{
			get
			{
				return "drawingGridVerticalOrigin";
			}
		}

		// Token: 0x170091CC RID: 37324
		// (get) Token: 0x0601A3D2 RID: 107474 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091CD RID: 37325
		// (get) Token: 0x0601A3D3 RID: 107475 RVA: 0x0035F5BB File Offset: 0x0035D7BB
		internal override int ElementTypeId
		{
			get
			{
				return 12015;
			}
		}

		// Token: 0x0601A3D4 RID: 107476 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3D6 RID: 107478 RVA: 0x0035F5C2 File Offset: 0x0035D7C2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DrawingGridVerticalOrigin>(deep);
		}

		// Token: 0x0400AC62 RID: 44130
		private const string tagName = "drawingGridVerticalOrigin";

		// Token: 0x0400AC63 RID: 44131
		private const byte tagNsId = 23;

		// Token: 0x0400AC64 RID: 44132
		internal const int ElementTypeIdConst = 12015;
	}
}
