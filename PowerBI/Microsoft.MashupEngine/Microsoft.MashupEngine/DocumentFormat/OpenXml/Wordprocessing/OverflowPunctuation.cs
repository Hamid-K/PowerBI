using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D72 RID: 11634
	[GeneratedCode("DomGen", "2.0")]
	internal class OverflowPunctuation : OnOffType
	{
		// Token: 0x170086F2 RID: 34546
		// (get) Token: 0x06018CA4 RID: 101540 RVA: 0x00344A91 File Offset: 0x00342C91
		public override string LocalName
		{
			get
			{
				return "overflowPunct";
			}
		}

		// Token: 0x170086F3 RID: 34547
		// (get) Token: 0x06018CA5 RID: 101541 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086F4 RID: 34548
		// (get) Token: 0x06018CA6 RID: 101542 RVA: 0x00344A98 File Offset: 0x00342C98
		internal override int ElementTypeId
		{
			get
			{
				return 11506;
			}
		}

		// Token: 0x06018CA7 RID: 101543 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CA9 RID: 101545 RVA: 0x00344A9F File Offset: 0x00342C9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OverflowPunctuation>(deep);
		}

		// Token: 0x0400A4C1 RID: 42177
		private const string tagName = "overflowPunct";

		// Token: 0x0400A4C2 RID: 42178
		private const byte tagNsId = 23;

		// Token: 0x0400A4C3 RID: 42179
		internal const int ElementTypeIdConst = 11506;
	}
}
