using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E67 RID: 11879
	[GeneratedCode("DomGen", "2.0")]
	internal class EndnoteReferenceMark : EmptyType
	{
		// Token: 0x17008A76 RID: 35446
		// (get) Token: 0x060193CD RID: 103373 RVA: 0x00347AFA File Offset: 0x00345CFA
		public override string LocalName
		{
			get
			{
				return "endnoteRef";
			}
		}

		// Token: 0x17008A77 RID: 35447
		// (get) Token: 0x060193CE RID: 103374 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A78 RID: 35448
		// (get) Token: 0x060193CF RID: 103375 RVA: 0x00347B01 File Offset: 0x00345D01
		internal override int ElementTypeId
		{
			get
			{
				return 11558;
			}
		}

		// Token: 0x060193D0 RID: 103376 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193D2 RID: 103378 RVA: 0x00347B08 File Offset: 0x00345D08
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndnoteReferenceMark>(deep);
		}

		// Token: 0x0400A7CD RID: 42957
		private const string tagName = "endnoteRef";

		// Token: 0x0400A7CE RID: 42958
		private const byte tagNsId = 23;

		// Token: 0x0400A7CF RID: 42959
		internal const int ElementTypeIdConst = 11558;
	}
}
