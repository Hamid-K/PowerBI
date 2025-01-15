using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FBD RID: 12221
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftMarginDiv : SignedTwipsMeasureType
	{
		// Token: 0x170093CE RID: 37838
		// (get) Token: 0x0601A803 RID: 108547 RVA: 0x00363284 File Offset: 0x00361484
		public override string LocalName
		{
			get
			{
				return "marLeft";
			}
		}

		// Token: 0x170093CF RID: 37839
		// (get) Token: 0x0601A804 RID: 108548 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093D0 RID: 37840
		// (get) Token: 0x0601A805 RID: 108549 RVA: 0x0036328B File Offset: 0x0036148B
		internal override int ElementTypeId
		{
			get
			{
				return 11929;
			}
		}

		// Token: 0x0601A806 RID: 108550 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A808 RID: 108552 RVA: 0x0036329A File Offset: 0x0036149A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftMarginDiv>(deep);
		}

		// Token: 0x0400AD3A RID: 44346
		private const string tagName = "marLeft";

		// Token: 0x0400AD3B RID: 44347
		private const byte tagNsId = 23;

		// Token: 0x0400AD3C RID: 44348
		internal const int ElementTypeIdConst = 11929;
	}
}
