using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB4 RID: 11700
	[GeneratedCode("DomGen", "2.0")]
	internal class EmbedTrueTypeFonts : OnOffType
	{
		// Token: 0x170087B8 RID: 34744
		// (get) Token: 0x06018E30 RID: 101936 RVA: 0x00345039 File Offset: 0x00343239
		public override string LocalName
		{
			get
			{
				return "embedTrueTypeFonts";
			}
		}

		// Token: 0x170087B9 RID: 34745
		// (get) Token: 0x06018E31 RID: 101937 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087BA RID: 34746
		// (get) Token: 0x06018E32 RID: 101938 RVA: 0x00345040 File Offset: 0x00343240
		internal override int ElementTypeId
		{
			get
			{
				return 11968;
			}
		}

		// Token: 0x06018E33 RID: 101939 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E35 RID: 101941 RVA: 0x00345047 File Offset: 0x00343247
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbedTrueTypeFonts>(deep);
		}

		// Token: 0x0400A587 RID: 42375
		private const string tagName = "embedTrueTypeFonts";

		// Token: 0x0400A588 RID: 42376
		private const byte tagNsId = 23;

		// Token: 0x0400A589 RID: 42377
		internal const int ElementTypeIdConst = 11968;
	}
}
