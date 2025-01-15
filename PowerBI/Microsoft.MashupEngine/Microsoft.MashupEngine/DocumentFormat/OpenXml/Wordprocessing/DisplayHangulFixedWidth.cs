using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E1B RID: 11803
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayHangulFixedWidth : OnOffType
	{
		// Token: 0x170088ED RID: 35053
		// (get) Token: 0x0601909A RID: 102554 RVA: 0x0034597A File Offset: 0x00343B7A
		public override string LocalName
		{
			get
			{
				return "displayHangulFixedWidth";
			}
		}

		// Token: 0x170088EE RID: 35054
		// (get) Token: 0x0601909B RID: 102555 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088EF RID: 35055
		// (get) Token: 0x0601909C RID: 102556 RVA: 0x00345981 File Offset: 0x00343B81
		internal override int ElementTypeId
		{
			get
			{
				return 12113;
			}
		}

		// Token: 0x0601909D RID: 102557 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601909F RID: 102559 RVA: 0x00345988 File Offset: 0x00343B88
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayHangulFixedWidth>(deep);
		}

		// Token: 0x0400A6BC RID: 42684
		private const string tagName = "displayHangulFixedWidth";

		// Token: 0x0400A6BD RID: 42685
		private const byte tagNsId = 23;

		// Token: 0x0400A6BE RID: 42686
		internal const int ElementTypeIdConst = 12113;
	}
}
