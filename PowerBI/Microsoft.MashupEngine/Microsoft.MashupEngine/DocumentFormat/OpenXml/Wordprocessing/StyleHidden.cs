using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF5 RID: 12021
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleHidden : OnOffOnlyType
	{
		// Token: 0x17008D93 RID: 36243
		// (get) Token: 0x06019A76 RID: 105078 RVA: 0x00344D50 File Offset: 0x00342F50
		public override string LocalName
		{
			get
			{
				return "hidden";
			}
		}

		// Token: 0x17008D94 RID: 36244
		// (get) Token: 0x06019A77 RID: 105079 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D95 RID: 36245
		// (get) Token: 0x06019A78 RID: 105080 RVA: 0x00353965 File Offset: 0x00351B65
		internal override int ElementTypeId
		{
			get
			{
				return 11898;
			}
		}

		// Token: 0x06019A79 RID: 105081 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A7B RID: 105083 RVA: 0x0035396C File Offset: 0x00351B6C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleHidden>(deep);
		}

		// Token: 0x0400A9EC RID: 43500
		private const string tagName = "hidden";

		// Token: 0x0400A9ED RID: 43501
		private const byte tagNsId = 23;

		// Token: 0x0400A9EE RID: 43502
		internal const int ElementTypeIdConst = 11898;
	}
}
