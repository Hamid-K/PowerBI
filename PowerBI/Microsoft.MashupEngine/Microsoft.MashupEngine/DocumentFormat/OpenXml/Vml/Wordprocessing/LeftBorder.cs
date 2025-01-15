using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x02002238 RID: 8760
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftBorder : BorderType
	{
		// Token: 0x17003966 RID: 14694
		// (get) Token: 0x0600E07C RID: 57468 RVA: 0x002BFE42 File Offset: 0x002BE042
		public override string LocalName
		{
			get
			{
				return "borderleft";
			}
		}

		// Token: 0x17003967 RID: 14695
		// (get) Token: 0x0600E07D RID: 57469 RVA: 0x002BFE26 File Offset: 0x002BE026
		internal override byte NamespaceId
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x17003968 RID: 14696
		// (get) Token: 0x0600E07E RID: 57470 RVA: 0x002BFE49 File Offset: 0x002BE049
		internal override int ElementTypeId
		{
			get
			{
				return 12431;
			}
		}

		// Token: 0x0600E07F RID: 57471 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E081 RID: 57473 RVA: 0x002BFE50 File Offset: 0x002BE050
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftBorder>(deep);
		}

		// Token: 0x04006E50 RID: 28240
		private const string tagName = "borderleft";

		// Token: 0x04006E51 RID: 28241
		private const byte tagNsId = 28;

		// Token: 0x04006E52 RID: 28242
		internal const int ElementTypeIdConst = 12431;
	}
}
