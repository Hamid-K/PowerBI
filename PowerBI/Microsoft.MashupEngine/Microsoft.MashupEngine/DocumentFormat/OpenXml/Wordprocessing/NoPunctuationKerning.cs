using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DCF RID: 11727
	[GeneratedCode("DomGen", "2.0")]
	internal class NoPunctuationKerning : OnOffType
	{
		// Token: 0x17008809 RID: 34825
		// (get) Token: 0x06018ED2 RID: 102098 RVA: 0x003452A6 File Offset: 0x003434A6
		public override string LocalName
		{
			get
			{
				return "noPunctuationKerning";
			}
		}

		// Token: 0x1700880A RID: 34826
		// (get) Token: 0x06018ED3 RID: 102099 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700880B RID: 34827
		// (get) Token: 0x06018ED4 RID: 102100 RVA: 0x003452AD File Offset: 0x003434AD
		internal override int ElementTypeId
		{
			get
			{
				return 12017;
			}
		}

		// Token: 0x06018ED5 RID: 102101 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018ED7 RID: 102103 RVA: 0x003452B4 File Offset: 0x003434B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoPunctuationKerning>(deep);
		}

		// Token: 0x0400A5D8 RID: 42456
		private const string tagName = "noPunctuationKerning";

		// Token: 0x0400A5D9 RID: 42457
		private const byte tagNsId = 23;

		// Token: 0x0400A5DA RID: 42458
		internal const int ElementTypeIdConst = 12017;
	}
}
