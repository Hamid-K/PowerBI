using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F6A RID: 12138
	[GeneratedCode("DomGen", "2.0")]
	internal class EndnoteSpecialReference : FootnoteEndnoteSeparatorReferenceType
	{
		// Token: 0x170090CE RID: 37070
		// (get) Token: 0x0601A1B8 RID: 106936 RVA: 0x0035D8DB File Offset: 0x0035BADB
		public override string LocalName
		{
			get
			{
				return "endnote";
			}
		}

		// Token: 0x170090CF RID: 37071
		// (get) Token: 0x0601A1B9 RID: 106937 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090D0 RID: 37072
		// (get) Token: 0x0601A1BA RID: 106938 RVA: 0x0035D8E2 File Offset: 0x0035BAE2
		internal override int ElementTypeId
		{
			get
			{
				return 11795;
			}
		}

		// Token: 0x0601A1BB RID: 106939 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A1BD RID: 106941 RVA: 0x0035D8E9 File Offset: 0x0035BAE9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndnoteSpecialReference>(deep);
		}

		// Token: 0x0400ABD0 RID: 43984
		private const string tagName = "endnote";

		// Token: 0x0400ABD1 RID: 43985
		private const byte tagNsId = 23;

		// Token: 0x0400ABD2 RID: 43986
		internal const int ElementTypeIdConst = 11795;
	}
}
