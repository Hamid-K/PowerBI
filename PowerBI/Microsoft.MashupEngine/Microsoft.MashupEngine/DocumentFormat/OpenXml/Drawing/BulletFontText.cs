using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002749 RID: 10057
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletFontText : OpenXmlLeafElement
	{
		// Token: 0x1700607F RID: 24703
		// (get) Token: 0x060135AE RID: 79278 RVA: 0x0030642B File Offset: 0x0030462B
		public override string LocalName
		{
			get
			{
				return "buFontTx";
			}
		}

		// Token: 0x17006080 RID: 24704
		// (get) Token: 0x060135AF RID: 79279 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006081 RID: 24705
		// (get) Token: 0x060135B0 RID: 79280 RVA: 0x00306432 File Offset: 0x00304632
		internal override int ElementTypeId
		{
			get
			{
				return 10107;
			}
		}

		// Token: 0x060135B1 RID: 79281 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135B3 RID: 79283 RVA: 0x00306439 File Offset: 0x00304639
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletFontText>(deep);
		}

		// Token: 0x040085CD RID: 34253
		private const string tagName = "buFontTx";

		// Token: 0x040085CE RID: 34254
		private const byte tagNsId = 10;

		// Token: 0x040085CF RID: 34255
		internal const int ElementTypeIdConst = 10107;
	}
}
