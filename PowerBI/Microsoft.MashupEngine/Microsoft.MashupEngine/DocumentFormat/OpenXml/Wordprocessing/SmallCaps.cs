using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D85 RID: 11653
	[GeneratedCode("DomGen", "2.0")]
	internal class SmallCaps : OnOffType
	{
		// Token: 0x1700872B RID: 34603
		// (get) Token: 0x06018D16 RID: 101654 RVA: 0x00344C38 File Offset: 0x00342E38
		public override string LocalName
		{
			get
			{
				return "smallCaps";
			}
		}

		// Token: 0x1700872C RID: 34604
		// (get) Token: 0x06018D17 RID: 101655 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700872D RID: 34605
		// (get) Token: 0x06018D18 RID: 101656 RVA: 0x00344C3F File Offset: 0x00342E3F
		internal override int ElementTypeId
		{
			get
			{
				return 11582;
			}
		}

		// Token: 0x06018D19 RID: 101657 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D1B RID: 101659 RVA: 0x00344C46 File Offset: 0x00342E46
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmallCaps>(deep);
		}

		// Token: 0x0400A4FA RID: 42234
		private const string tagName = "smallCaps";

		// Token: 0x0400A4FB RID: 42235
		private const byte tagNsId = 23;

		// Token: 0x0400A4FC RID: 42236
		internal const int ElementTypeIdConst = 11582;
	}
}
