using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC5 RID: 11717
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleLockThemesPart : OnOffType
	{
		// Token: 0x170087EB RID: 34795
		// (get) Token: 0x06018E96 RID: 102038 RVA: 0x003451C0 File Offset: 0x003433C0
		public override string LocalName
		{
			get
			{
				return "styleLockTheme";
			}
		}

		// Token: 0x170087EC RID: 34796
		// (get) Token: 0x06018E97 RID: 102039 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087ED RID: 34797
		// (get) Token: 0x06018E98 RID: 102040 RVA: 0x003451C7 File Offset: 0x003433C7
		internal override int ElementTypeId
		{
			get
			{
				return 11994;
			}
		}

		// Token: 0x06018E99 RID: 102041 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E9B RID: 102043 RVA: 0x003451CE File Offset: 0x003433CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleLockThemesPart>(deep);
		}

		// Token: 0x0400A5BA RID: 42426
		private const string tagName = "styleLockTheme";

		// Token: 0x0400A5BB RID: 42427
		private const byte tagNsId = 23;

		// Token: 0x0400A5BC RID: 42428
		internal const int ElementTypeIdConst = 11994;
	}
}
