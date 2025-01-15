using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200270B RID: 9995
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaFloor : OpenXmlLeafElement
	{
		// Token: 0x17005EB5 RID: 24245
		// (get) Token: 0x060131C6 RID: 78278 RVA: 0x00303CCA File Offset: 0x00301ECA
		public override string LocalName
		{
			get
			{
				return "alphaFloor";
			}
		}

		// Token: 0x17005EB6 RID: 24246
		// (get) Token: 0x060131C7 RID: 78279 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EB7 RID: 24247
		// (get) Token: 0x060131C8 RID: 78280 RVA: 0x00303CD1 File Offset: 0x00301ED1
		internal override int ElementTypeId
		{
			get
			{
				return 10057;
			}
		}

		// Token: 0x060131C9 RID: 78281 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060131CB RID: 78283 RVA: 0x00303CD8 File Offset: 0x00301ED8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaFloor>(deep);
		}

		// Token: 0x040084BA RID: 33978
		private const string tagName = "alphaFloor";

		// Token: 0x040084BB RID: 33979
		private const byte tagNsId = 10;

		// Token: 0x040084BC RID: 33980
		internal const int ElementTypeIdConst = 10057;
	}
}
