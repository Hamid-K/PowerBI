using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200253F RID: 9535
	[GeneratedCode("DomGen", "2.0")]
	internal class Index : UnsignedIntegerType
	{
		// Token: 0x170054E2 RID: 21730
		// (get) Token: 0x06011BDF RID: 72671 RVA: 0x002F1937 File Offset: 0x002EFB37
		public override string LocalName
		{
			get
			{
				return "idx";
			}
		}

		// Token: 0x170054E3 RID: 21731
		// (get) Token: 0x06011BE0 RID: 72672 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054E4 RID: 21732
		// (get) Token: 0x06011BE1 RID: 72673 RVA: 0x002F193E File Offset: 0x002EFB3E
		internal override int ElementTypeId
		{
			get
			{
				return 10357;
			}
		}

		// Token: 0x06011BE2 RID: 72674 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BE4 RID: 72676 RVA: 0x002F194D File Offset: 0x002EFB4D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Index>(deep);
		}

		// Token: 0x04007C53 RID: 31827
		private const string tagName = "idx";

		// Token: 0x04007C54 RID: 31828
		private const byte tagNsId = 11;

		// Token: 0x04007C55 RID: 31829
		internal const int ElementTypeIdConst = 10357;
	}
}
