using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE3 RID: 11747
	[GeneratedCode("DomGen", "2.0")]
	internal class NoTabHangIndent : OnOffType
	{
		// Token: 0x17008845 RID: 34885
		// (get) Token: 0x06018F4A RID: 102218 RVA: 0x00345472 File Offset: 0x00343672
		public override string LocalName
		{
			get
			{
				return "noTabHangInd";
			}
		}

		// Token: 0x17008846 RID: 34886
		// (get) Token: 0x06018F4B RID: 102219 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008847 RID: 34887
		// (get) Token: 0x06018F4C RID: 102220 RVA: 0x00345479 File Offset: 0x00343679
		internal override int ElementTypeId
		{
			get
			{
				return 12057;
			}
		}

		// Token: 0x06018F4D RID: 102221 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F4F RID: 102223 RVA: 0x00345480 File Offset: 0x00343680
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoTabHangIndent>(deep);
		}

		// Token: 0x0400A614 RID: 42516
		private const string tagName = "noTabHangInd";

		// Token: 0x0400A615 RID: 42517
		private const byte tagNsId = 23;

		// Token: 0x0400A616 RID: 42518
		internal const int ElementTypeIdConst = 12057;
	}
}
