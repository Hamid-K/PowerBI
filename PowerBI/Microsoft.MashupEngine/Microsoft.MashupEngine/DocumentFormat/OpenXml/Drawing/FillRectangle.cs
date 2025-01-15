using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C0 RID: 10176
	[GeneratedCode("DomGen", "2.0")]
	internal class FillRectangle : RelativeRectangleType
	{
		// Token: 0x17006360 RID: 25440
		// (get) Token: 0x06013C2D RID: 80941 RVA: 0x0030B76E File Offset: 0x0030996E
		public override string LocalName
		{
			get
			{
				return "fillRect";
			}
		}

		// Token: 0x17006361 RID: 25441
		// (get) Token: 0x06013C2E RID: 80942 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006362 RID: 25442
		// (get) Token: 0x06013C2F RID: 80943 RVA: 0x0030B775 File Offset: 0x00309975
		internal override int ElementTypeId
		{
			get
			{
				return 10211;
			}
		}

		// Token: 0x06013C30 RID: 80944 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C32 RID: 80946 RVA: 0x0030B77C File Offset: 0x0030997C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillRectangle>(deep);
		}

		// Token: 0x040087A7 RID: 34727
		private const string tagName = "fillRect";

		// Token: 0x040087A8 RID: 34728
		private const byte tagNsId = 10;

		// Token: 0x040087A9 RID: 34729
		internal const int ElementTypeIdConst = 10211;
	}
}
