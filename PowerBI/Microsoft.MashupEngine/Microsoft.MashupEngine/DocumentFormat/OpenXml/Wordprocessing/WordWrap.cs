using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D71 RID: 11633
	[GeneratedCode("DomGen", "2.0")]
	internal class WordWrap : OnOffType
	{
		// Token: 0x170086EF RID: 34543
		// (get) Token: 0x06018C9E RID: 101534 RVA: 0x00344A7A File Offset: 0x00342C7A
		public override string LocalName
		{
			get
			{
				return "wordWrap";
			}
		}

		// Token: 0x170086F0 RID: 34544
		// (get) Token: 0x06018C9F RID: 101535 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086F1 RID: 34545
		// (get) Token: 0x06018CA0 RID: 101536 RVA: 0x00344A81 File Offset: 0x00342C81
		internal override int ElementTypeId
		{
			get
			{
				return 11505;
			}
		}

		// Token: 0x06018CA1 RID: 101537 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CA3 RID: 101539 RVA: 0x00344A88 File Offset: 0x00342C88
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordWrap>(deep);
		}

		// Token: 0x0400A4BE RID: 42174
		private const string tagName = "wordWrap";

		// Token: 0x0400A4BF RID: 42175
		private const byte tagNsId = 23;

		// Token: 0x0400A4C0 RID: 42176
		internal const int ElementTypeIdConst = 11505;
	}
}
