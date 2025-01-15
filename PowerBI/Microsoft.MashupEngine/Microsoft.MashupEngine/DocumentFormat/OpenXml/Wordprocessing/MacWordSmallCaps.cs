using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DFB RID: 11771
	[GeneratedCode("DomGen", "2.0")]
	internal class MacWordSmallCaps : OnOffType
	{
		// Token: 0x1700888D RID: 34957
		// (get) Token: 0x06018FDA RID: 102362 RVA: 0x0034569A File Offset: 0x0034389A
		public override string LocalName
		{
			get
			{
				return "mwSmallCaps";
			}
		}

		// Token: 0x1700888E RID: 34958
		// (get) Token: 0x06018FDB RID: 102363 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700888F RID: 34959
		// (get) Token: 0x06018FDC RID: 102364 RVA: 0x003456A1 File Offset: 0x003438A1
		internal override int ElementTypeId
		{
			get
			{
				return 12081;
			}
		}

		// Token: 0x06018FDD RID: 102365 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FDF RID: 102367 RVA: 0x003456A8 File Offset: 0x003438A8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MacWordSmallCaps>(deep);
		}

		// Token: 0x0400A65C RID: 42588
		private const string tagName = "mwSmallCaps";

		// Token: 0x0400A65D RID: 42589
		private const byte tagNsId = 23;

		// Token: 0x0400A65E RID: 42590
		internal const int ElementTypeIdConst = 12081;
	}
}
