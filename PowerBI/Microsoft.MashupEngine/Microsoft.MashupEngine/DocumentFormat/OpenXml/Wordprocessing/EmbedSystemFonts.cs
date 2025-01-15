using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB5 RID: 11701
	[GeneratedCode("DomGen", "2.0")]
	public class EmbedSystemFonts : OnOffType
	{
		// Token: 0x170087BB RID: 34747
		// (get) Token: 0x06018E36 RID: 101942 RVA: 0x00345050 File Offset: 0x00343250
		public override string LocalName
		{
			get
			{
				return "embedSystemFonts";
			}
		}

		// Token: 0x170087BC RID: 34748
		// (get) Token: 0x06018E37 RID: 101943 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087BD RID: 34749
		// (get) Token: 0x06018E38 RID: 101944 RVA: 0x00345057 File Offset: 0x00343257
		internal override int ElementTypeId
		{
			get
			{
				return 11969;
			}
		}

		// Token: 0x06018E39 RID: 101945 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E3B RID: 101947 RVA: 0x0034505E File Offset: 0x0034325E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbedSystemFonts>(deep);
		}

		// Token: 0x0400A58A RID: 42378
		private const string tagName = "embedSystemFonts";

		// Token: 0x0400A58B RID: 42379
		private const byte tagNsId = 23;

		// Token: 0x0400A58C RID: 42380
		internal const int ElementTypeIdConst = 11969;
	}
}
