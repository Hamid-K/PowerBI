using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF0 RID: 11760
	[GeneratedCode("DomGen", "2.0")]
	internal class WordPerfectSpaceWidth : OnOffType
	{
		// Token: 0x1700886C RID: 34924
		// (get) Token: 0x06018F98 RID: 102296 RVA: 0x0034559D File Offset: 0x0034379D
		public override string LocalName
		{
			get
			{
				return "wpSpaceWidth";
			}
		}

		// Token: 0x1700886D RID: 34925
		// (get) Token: 0x06018F99 RID: 102297 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700886E RID: 34926
		// (get) Token: 0x06018F9A RID: 102298 RVA: 0x003455A4 File Offset: 0x003437A4
		internal override int ElementTypeId
		{
			get
			{
				return 12070;
			}
		}

		// Token: 0x06018F9B RID: 102299 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F9D RID: 102301 RVA: 0x003455AB File Offset: 0x003437AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordPerfectSpaceWidth>(deep);
		}

		// Token: 0x0400A63B RID: 42555
		private const string tagName = "wpSpaceWidth";

		// Token: 0x0400A63C RID: 42556
		private const byte tagNsId = 23;

		// Token: 0x0400A63D RID: 42557
		internal const int ElementTypeIdConst = 12070;
	}
}
