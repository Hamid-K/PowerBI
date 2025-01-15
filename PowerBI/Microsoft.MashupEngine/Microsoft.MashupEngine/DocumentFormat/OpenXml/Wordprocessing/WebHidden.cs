using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D8E RID: 11662
	[GeneratedCode("DomGen", "2.0")]
	internal class WebHidden : OnOffType
	{
		// Token: 0x17008746 RID: 34630
		// (get) Token: 0x06018D4C RID: 101708 RVA: 0x00344CF2 File Offset: 0x00342EF2
		public override string LocalName
		{
			get
			{
				return "webHidden";
			}
		}

		// Token: 0x17008747 RID: 34631
		// (get) Token: 0x06018D4D RID: 101709 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008748 RID: 34632
		// (get) Token: 0x06018D4E RID: 101710 RVA: 0x00344CF9 File Offset: 0x00342EF9
		internal override int ElementTypeId
		{
			get
			{
				return 11591;
			}
		}

		// Token: 0x06018D4F RID: 101711 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D51 RID: 101713 RVA: 0x00344D00 File Offset: 0x00342F00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebHidden>(deep);
		}

		// Token: 0x0400A515 RID: 42261
		private const string tagName = "webHidden";

		// Token: 0x0400A516 RID: 42262
		private const byte tagNsId = 23;

		// Token: 0x0400A517 RID: 42263
		internal const int ElementTypeIdConst = 11591;
	}
}
