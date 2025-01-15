using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DBE RID: 11710
	[GeneratedCode("DomGen", "2.0")]
	internal class HideGrammaticalErrors : OnOffType
	{
		// Token: 0x170087D6 RID: 34774
		// (get) Token: 0x06018E6C RID: 101996 RVA: 0x0034511F File Offset: 0x0034331F
		public override string LocalName
		{
			get
			{
				return "hideGrammaticalErrors";
			}
		}

		// Token: 0x170087D7 RID: 34775
		// (get) Token: 0x06018E6D RID: 101997 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087D8 RID: 34776
		// (get) Token: 0x06018E6E RID: 101998 RVA: 0x00345126 File Offset: 0x00343326
		internal override int ElementTypeId
		{
			get
			{
				return 11978;
			}
		}

		// Token: 0x06018E6F RID: 101999 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E71 RID: 102001 RVA: 0x0034512D File Offset: 0x0034332D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideGrammaticalErrors>(deep);
		}

		// Token: 0x0400A5A5 RID: 42405
		private const string tagName = "hideGrammaticalErrors";

		// Token: 0x0400A5A6 RID: 42406
		private const byte tagNsId = 23;

		// Token: 0x0400A5A7 RID: 42407
		internal const int ElementTypeIdConst = 11978;
	}
}
