using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F8D RID: 12173
	[GeneratedCode("DomGen", "2.0")]
	internal class HyphenationZone : TwipsMeasureType
	{
		// Token: 0x170091BF RID: 37311
		// (get) Token: 0x0601A3B9 RID: 107449 RVA: 0x0035F558 File Offset: 0x0035D758
		public override string LocalName
		{
			get
			{
				return "hyphenationZone";
			}
		}

		// Token: 0x170091C0 RID: 37312
		// (get) Token: 0x0601A3BA RID: 107450 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091C1 RID: 37313
		// (get) Token: 0x0601A3BB RID: 107451 RVA: 0x0035F55F File Offset: 0x0035D75F
		internal override int ElementTypeId
		{
			get
			{
				return 11999;
			}
		}

		// Token: 0x0601A3BC RID: 107452 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3BE RID: 107454 RVA: 0x0035F566 File Offset: 0x0035D766
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyphenationZone>(deep);
		}

		// Token: 0x0400AC56 RID: 44118
		private const string tagName = "hyphenationZone";

		// Token: 0x0400AC57 RID: 44119
		private const byte tagNsId = 23;

		// Token: 0x0400AC58 RID: 44120
		internal const int ElementTypeIdConst = 11999;
	}
}
