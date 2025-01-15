using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E19 RID: 11801
	[GeneratedCode("DomGen", "2.0")]
	internal class AutofitToFirstFixedWidthCell : OnOffType
	{
		// Token: 0x170088E7 RID: 35047
		// (get) Token: 0x0601908E RID: 102542 RVA: 0x0034594C File Offset: 0x00343B4C
		public override string LocalName
		{
			get
			{
				return "autofitToFirstFixedWidthCell";
			}
		}

		// Token: 0x170088E8 RID: 35048
		// (get) Token: 0x0601908F RID: 102543 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088E9 RID: 35049
		// (get) Token: 0x06019090 RID: 102544 RVA: 0x00345953 File Offset: 0x00343B53
		internal override int ElementTypeId
		{
			get
			{
				return 12111;
			}
		}

		// Token: 0x06019091 RID: 102545 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019093 RID: 102547 RVA: 0x0034595A File Offset: 0x00343B5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutofitToFirstFixedWidthCell>(deep);
		}

		// Token: 0x0400A6B6 RID: 42678
		private const string tagName = "autofitToFirstFixedWidthCell";

		// Token: 0x0400A6B7 RID: 42679
		private const byte tagNsId = 23;

		// Token: 0x0400A6B8 RID: 42680
		internal const int ElementTypeIdConst = 12111;
	}
}
