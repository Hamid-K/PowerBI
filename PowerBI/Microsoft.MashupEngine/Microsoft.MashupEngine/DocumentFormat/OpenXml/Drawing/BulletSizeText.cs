using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002746 RID: 10054
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletSizeText : OpenXmlLeafElement
	{
		// Token: 0x17006070 RID: 24688
		// (get) Token: 0x06013590 RID: 79248 RVA: 0x00306369 File Offset: 0x00304569
		public override string LocalName
		{
			get
			{
				return "buSzTx";
			}
		}

		// Token: 0x17006071 RID: 24689
		// (get) Token: 0x06013591 RID: 79249 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006072 RID: 24690
		// (get) Token: 0x06013592 RID: 79250 RVA: 0x00306370 File Offset: 0x00304570
		internal override int ElementTypeId
		{
			get
			{
				return 10104;
			}
		}

		// Token: 0x06013593 RID: 79251 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013595 RID: 79253 RVA: 0x00306377 File Offset: 0x00304577
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletSizeText>(deep);
		}

		// Token: 0x040085C0 RID: 34240
		private const string tagName = "buSzTx";

		// Token: 0x040085C1 RID: 34241
		private const byte tagNsId = 10;

		// Token: 0x040085C2 RID: 34242
		internal const int ElementTypeIdConst = 10104;
	}
}
