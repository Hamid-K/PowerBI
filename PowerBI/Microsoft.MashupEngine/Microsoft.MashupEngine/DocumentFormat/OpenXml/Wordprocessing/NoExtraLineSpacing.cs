using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE8 RID: 11752
	[GeneratedCode("DomGen", "2.0")]
	internal class NoExtraLineSpacing : OnOffType
	{
		// Token: 0x17008854 RID: 34900
		// (get) Token: 0x06018F68 RID: 102248 RVA: 0x003454E5 File Offset: 0x003436E5
		public override string LocalName
		{
			get
			{
				return "noExtraLineSpacing";
			}
		}

		// Token: 0x17008855 RID: 34901
		// (get) Token: 0x06018F69 RID: 102249 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008856 RID: 34902
		// (get) Token: 0x06018F6A RID: 102250 RVA: 0x003454EC File Offset: 0x003436EC
		internal override int ElementTypeId
		{
			get
			{
				return 12062;
			}
		}

		// Token: 0x06018F6B RID: 102251 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F6D RID: 102253 RVA: 0x003454F3 File Offset: 0x003436F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoExtraLineSpacing>(deep);
		}

		// Token: 0x0400A623 RID: 42531
		private const string tagName = "noExtraLineSpacing";

		// Token: 0x0400A624 RID: 42532
		private const byte tagNsId = 23;

		// Token: 0x0400A625 RID: 42533
		internal const int ElementTypeIdConst = 12062;
	}
}
