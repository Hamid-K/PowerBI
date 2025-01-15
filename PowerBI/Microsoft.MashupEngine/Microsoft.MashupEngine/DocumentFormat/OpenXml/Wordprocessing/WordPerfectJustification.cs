using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE2 RID: 11746
	[GeneratedCode("DomGen", "2.0")]
	internal class WordPerfectJustification : OnOffType
	{
		// Token: 0x17008842 RID: 34882
		// (get) Token: 0x06018F44 RID: 102212 RVA: 0x0034545B File Offset: 0x0034365B
		public override string LocalName
		{
			get
			{
				return "wpJustification";
			}
		}

		// Token: 0x17008843 RID: 34883
		// (get) Token: 0x06018F45 RID: 102213 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008844 RID: 34884
		// (get) Token: 0x06018F46 RID: 102214 RVA: 0x00345462 File Offset: 0x00343662
		internal override int ElementTypeId
		{
			get
			{
				return 12056;
			}
		}

		// Token: 0x06018F47 RID: 102215 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F49 RID: 102217 RVA: 0x00345469 File Offset: 0x00343669
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordPerfectJustification>(deep);
		}

		// Token: 0x0400A611 RID: 42513
		private const string tagName = "wpJustification";

		// Token: 0x0400A612 RID: 42514
		private const byte tagNsId = 23;

		// Token: 0x0400A613 RID: 42515
		internal const int ElementTypeIdConst = 12056;
	}
}
