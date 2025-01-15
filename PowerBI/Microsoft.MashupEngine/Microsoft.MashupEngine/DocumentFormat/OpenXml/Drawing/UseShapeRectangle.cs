using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A2 RID: 10146
	[GeneratedCode("DomGen", "2.0")]
	internal class UseShapeRectangle : OpenXmlLeafElement
	{
		// Token: 0x1700627F RID: 25215
		// (get) Token: 0x06013A4D RID: 80461 RVA: 0x0030A3B8 File Offset: 0x003085B8
		public override string LocalName
		{
			get
			{
				return "useSpRect";
			}
		}

		// Token: 0x17006280 RID: 25216
		// (get) Token: 0x06013A4E RID: 80462 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006281 RID: 25217
		// (get) Token: 0x06013A4F RID: 80463 RVA: 0x0030A3BF File Offset: 0x003085BF
		internal override int ElementTypeId
		{
			get
			{
				return 10179;
			}
		}

		// Token: 0x06013A50 RID: 80464 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013A52 RID: 80466 RVA: 0x0030A3C6 File Offset: 0x003085C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseShapeRectangle>(deep);
		}

		// Token: 0x0400871C RID: 34588
		private const string tagName = "useSpRect";

		// Token: 0x0400871D RID: 34589
		private const byte tagNsId = 10;

		// Token: 0x0400871E RID: 34590
		internal const int ElementTypeIdConst = 10179;
	}
}
