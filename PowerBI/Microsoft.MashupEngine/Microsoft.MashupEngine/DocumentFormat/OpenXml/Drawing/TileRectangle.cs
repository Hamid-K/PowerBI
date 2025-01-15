using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027BF RID: 10175
	[GeneratedCode("DomGen", "2.0")]
	internal class TileRectangle : RelativeRectangleType
	{
		// Token: 0x1700635D RID: 25437
		// (get) Token: 0x06013C27 RID: 80935 RVA: 0x0030B757 File Offset: 0x00309957
		public override string LocalName
		{
			get
			{
				return "tileRect";
			}
		}

		// Token: 0x1700635E RID: 25438
		// (get) Token: 0x06013C28 RID: 80936 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700635F RID: 25439
		// (get) Token: 0x06013C29 RID: 80937 RVA: 0x0030B75E File Offset: 0x0030995E
		internal override int ElementTypeId
		{
			get
			{
				return 10210;
			}
		}

		// Token: 0x06013C2A RID: 80938 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C2C RID: 80940 RVA: 0x0030B765 File Offset: 0x00309965
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TileRectangle>(deep);
		}

		// Token: 0x040087A4 RID: 34724
		private const string tagName = "tileRect";

		// Token: 0x040087A5 RID: 34725
		private const byte tagNsId = 10;

		// Token: 0x040087A6 RID: 34726
		internal const int ElementTypeIdConst = 10210;
	}
}
