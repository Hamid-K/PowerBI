using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002768 RID: 10088
	[GeneratedCode("DomGen", "2.0")]
	internal class ThemeManager : EmptyType
	{
		// Token: 0x17006122 RID: 24866
		// (get) Token: 0x0601372B RID: 79659 RVA: 0x003073B0 File Offset: 0x003055B0
		public override string LocalName
		{
			get
			{
				return "themeManager";
			}
		}

		// Token: 0x17006123 RID: 24867
		// (get) Token: 0x0601372C RID: 79660 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006124 RID: 24868
		// (get) Token: 0x0601372D RID: 79661 RVA: 0x003073B7 File Offset: 0x003055B7
		internal override int ElementTypeId
		{
			get
			{
				return 10124;
			}
		}

		// Token: 0x0601372E RID: 79662 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013730 RID: 79664 RVA: 0x003073C6 File Offset: 0x003055C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ThemeManager>(deep);
		}

		// Token: 0x0400863F RID: 34367
		private const string tagName = "themeManager";

		// Token: 0x04008640 RID: 34368
		private const byte tagNsId = 10;

		// Token: 0x04008641 RID: 34369
		internal const int ElementTypeIdConst = 10124;
	}
}
