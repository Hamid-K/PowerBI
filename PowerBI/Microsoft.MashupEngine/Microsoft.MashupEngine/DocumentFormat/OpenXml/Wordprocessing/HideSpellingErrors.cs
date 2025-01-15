using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DBD RID: 11709
	[GeneratedCode("DomGen", "2.0")]
	internal class HideSpellingErrors : OnOffType
	{
		// Token: 0x170087D3 RID: 34771
		// (get) Token: 0x06018E66 RID: 101990 RVA: 0x00345108 File Offset: 0x00343308
		public override string LocalName
		{
			get
			{
				return "hideSpellingErrors";
			}
		}

		// Token: 0x170087D4 RID: 34772
		// (get) Token: 0x06018E67 RID: 101991 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087D5 RID: 34773
		// (get) Token: 0x06018E68 RID: 101992 RVA: 0x0034510F File Offset: 0x0034330F
		internal override int ElementTypeId
		{
			get
			{
				return 11977;
			}
		}

		// Token: 0x06018E69 RID: 101993 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E6B RID: 101995 RVA: 0x00345116 File Offset: 0x00343316
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideSpellingErrors>(deep);
		}

		// Token: 0x0400A5A2 RID: 42402
		private const string tagName = "hideSpellingErrors";

		// Token: 0x0400A5A3 RID: 42403
		private const byte tagNsId = 23;

		// Token: 0x0400A5A4 RID: 42404
		internal const int ElementTypeIdConst = 11977;
	}
}
