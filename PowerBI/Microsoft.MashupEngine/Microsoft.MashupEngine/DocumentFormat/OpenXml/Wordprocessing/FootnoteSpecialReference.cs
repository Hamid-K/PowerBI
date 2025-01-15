using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F69 RID: 12137
	[GeneratedCode("DomGen", "2.0")]
	internal class FootnoteSpecialReference : FootnoteEndnoteSeparatorReferenceType
	{
		// Token: 0x170090CB RID: 37067
		// (get) Token: 0x0601A1B2 RID: 106930 RVA: 0x0035D8BC File Offset: 0x0035BABC
		public override string LocalName
		{
			get
			{
				return "footnote";
			}
		}

		// Token: 0x170090CC RID: 37068
		// (get) Token: 0x0601A1B3 RID: 106931 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090CD RID: 37069
		// (get) Token: 0x0601A1B4 RID: 106932 RVA: 0x0035D8C3 File Offset: 0x0035BAC3
		internal override int ElementTypeId
		{
			get
			{
				return 11794;
			}
		}

		// Token: 0x0601A1B5 RID: 106933 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A1B7 RID: 106935 RVA: 0x0035D8D2 File Offset: 0x0035BAD2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnoteSpecialReference>(deep);
		}

		// Token: 0x0400ABCD RID: 43981
		private const string tagName = "footnote";

		// Token: 0x0400ABCE RID: 43982
		private const byte tagNsId = 23;

		// Token: 0x0400ABCF RID: 43983
		internal const int ElementTypeIdConst = 11794;
	}
}
