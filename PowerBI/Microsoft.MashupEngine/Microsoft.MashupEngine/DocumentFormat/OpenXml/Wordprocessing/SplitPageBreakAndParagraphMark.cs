using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E1C RID: 11804
	[GeneratedCode("DomGen", "2.0")]
	internal class SplitPageBreakAndParagraphMark : OnOffType
	{
		// Token: 0x170088F0 RID: 35056
		// (get) Token: 0x060190A0 RID: 102560 RVA: 0x00345991 File Offset: 0x00343B91
		public override string LocalName
		{
			get
			{
				return "splitPgBreakAndParaMark";
			}
		}

		// Token: 0x170088F1 RID: 35057
		// (get) Token: 0x060190A1 RID: 102561 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088F2 RID: 35058
		// (get) Token: 0x060190A2 RID: 102562 RVA: 0x00345998 File Offset: 0x00343B98
		internal override int ElementTypeId
		{
			get
			{
				return 12114;
			}
		}

		// Token: 0x060190A3 RID: 102563 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190A5 RID: 102565 RVA: 0x0034599F File Offset: 0x00343B9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitPageBreakAndParagraphMark>(deep);
		}

		// Token: 0x0400A6BF RID: 42687
		private const string tagName = "splitPgBreakAndParaMark";

		// Token: 0x0400A6C0 RID: 42688
		private const byte tagNsId = 23;

		// Token: 0x0400A6C1 RID: 42689
		internal const int ElementTypeIdConst = 12114;
	}
}
