using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E06 RID: 11782
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotUseHTMLParagraphAutoSpacing : OnOffType
	{
		// Token: 0x170088AE RID: 34990
		// (get) Token: 0x0601901C RID: 102428 RVA: 0x00345797 File Offset: 0x00343997
		public override string LocalName
		{
			get
			{
				return "doNotUseHTMLParagraphAutoSpacing";
			}
		}

		// Token: 0x170088AF RID: 34991
		// (get) Token: 0x0601901D RID: 102429 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088B0 RID: 34992
		// (get) Token: 0x0601901E RID: 102430 RVA: 0x0034579E File Offset: 0x0034399E
		internal override int ElementTypeId
		{
			get
			{
				return 12092;
			}
		}

		// Token: 0x0601901F RID: 102431 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019021 RID: 102433 RVA: 0x003457A5 File Offset: 0x003439A5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotUseHTMLParagraphAutoSpacing>(deep);
		}

		// Token: 0x0400A67D RID: 42621
		private const string tagName = "doNotUseHTMLParagraphAutoSpacing";

		// Token: 0x0400A67E RID: 42622
		private const byte tagNsId = 23;

		// Token: 0x0400A67F RID: 42623
		internal const int ElementTypeIdConst = 12092;
	}
}
