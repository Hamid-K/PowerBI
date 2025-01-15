using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EAF RID: 11951
	[GeneratedCode("DomGen", "2.0")]
	internal class ThemeFontLanguages : LanguageType
	{
		// Token: 0x17008BB4 RID: 35764
		// (get) Token: 0x0601965B RID: 104027 RVA: 0x00349378 File Offset: 0x00347578
		public override string LocalName
		{
			get
			{
				return "themeFontLang";
			}
		}

		// Token: 0x17008BB5 RID: 35765
		// (get) Token: 0x0601965C RID: 104028 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BB6 RID: 35766
		// (get) Token: 0x0601965D RID: 104029 RVA: 0x0034937F File Offset: 0x0034757F
		internal override int ElementTypeId
		{
			get
			{
				return 12043;
			}
		}

		// Token: 0x0601965E RID: 104030 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019660 RID: 104032 RVA: 0x00349386 File Offset: 0x00347586
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ThemeFontLanguages>(deep);
		}

		// Token: 0x0400A8C8 RID: 43208
		private const string tagName = "themeFontLang";

		// Token: 0x0400A8C9 RID: 43209
		private const byte tagNsId = 23;

		// Token: 0x0400A8CA RID: 43210
		internal const int ElementTypeIdConst = 12043;
	}
}
