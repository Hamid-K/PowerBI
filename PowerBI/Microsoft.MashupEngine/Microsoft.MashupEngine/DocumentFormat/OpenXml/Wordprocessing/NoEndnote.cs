using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D7D RID: 11645
	[GeneratedCode("DomGen", "2.0")]
	internal class NoEndnote : OnOffType
	{
		// Token: 0x17008713 RID: 34579
		// (get) Token: 0x06018CE6 RID: 101606 RVA: 0x00344B8E File Offset: 0x00342D8E
		public override string LocalName
		{
			get
			{
				return "noEndnote";
			}
		}

		// Token: 0x17008714 RID: 34580
		// (get) Token: 0x06018CE7 RID: 101607 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008715 RID: 34581
		// (get) Token: 0x06018CE8 RID: 101608 RVA: 0x00344B95 File Offset: 0x00342D95
		internal override int ElementTypeId
		{
			get
			{
				return 11538;
			}
		}

		// Token: 0x06018CE9 RID: 101609 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CEB RID: 101611 RVA: 0x00344B9C File Offset: 0x00342D9C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoEndnote>(deep);
		}

		// Token: 0x0400A4E2 RID: 42210
		private const string tagName = "noEndnote";

		// Token: 0x0400A4E3 RID: 42211
		private const byte tagNsId = 23;

		// Token: 0x0400A4E4 RID: 42212
		internal const int ElementTypeIdConst = 11538;
	}
}
