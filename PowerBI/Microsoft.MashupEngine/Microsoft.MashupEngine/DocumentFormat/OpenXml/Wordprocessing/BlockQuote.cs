using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DAB RID: 11691
	[GeneratedCode("DomGen", "2.0")]
	internal class BlockQuote : OnOffType
	{
		// Token: 0x1700879D RID: 34717
		// (get) Token: 0x06018DFA RID: 101882 RVA: 0x00344F6A File Offset: 0x0034316A
		public override string LocalName
		{
			get
			{
				return "blockQuote";
			}
		}

		// Token: 0x1700879E RID: 34718
		// (get) Token: 0x06018DFB RID: 101883 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700879F RID: 34719
		// (get) Token: 0x06018DFC RID: 101884 RVA: 0x00344F71 File Offset: 0x00343171
		internal override int ElementTypeId
		{
			get
			{
				return 11927;
			}
		}

		// Token: 0x06018DFD RID: 101885 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DFF RID: 101887 RVA: 0x00344F78 File Offset: 0x00343178
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlockQuote>(deep);
		}

		// Token: 0x0400A56C RID: 42348
		private const string tagName = "blockQuote";

		// Token: 0x0400A56D RID: 42349
		private const byte tagNsId = 23;

		// Token: 0x0400A56E RID: 42350
		internal const int ElementTypeIdConst = 11927;
	}
}
