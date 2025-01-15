using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F89 RID: 12169
	[GeneratedCode("DomGen", "2.0")]
	internal class MarginHeight : PixelsMeasureType
	{
		// Token: 0x170091B0 RID: 37296
		// (get) Token: 0x0601A39A RID: 107418 RVA: 0x0035F45B File Offset: 0x0035D65B
		public override string LocalName
		{
			get
			{
				return "marH";
			}
		}

		// Token: 0x170091B1 RID: 37297
		// (get) Token: 0x0601A39B RID: 107419 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091B2 RID: 37298
		// (get) Token: 0x0601A39C RID: 107420 RVA: 0x0035F462 File Offset: 0x0035D662
		internal override int ElementTypeId
		{
			get
			{
				return 11852;
			}
		}

		// Token: 0x0601A39D RID: 107421 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A39F RID: 107423 RVA: 0x0035F469 File Offset: 0x0035D669
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MarginHeight>(deep);
		}

		// Token: 0x0400AC49 RID: 44105
		private const string tagName = "marH";

		// Token: 0x0400AC4A RID: 44106
		private const byte tagNsId = 23;

		// Token: 0x0400AC4B RID: 44107
		internal const int ElementTypeIdConst = 11852;
	}
}
