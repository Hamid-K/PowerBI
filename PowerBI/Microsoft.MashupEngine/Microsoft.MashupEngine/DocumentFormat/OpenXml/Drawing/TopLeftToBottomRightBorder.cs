using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027FE RID: 10238
	[GeneratedCode("DomGen", "2.0")]
	internal class TopLeftToBottomRightBorder : ThemeableLineStyleType
	{
		// Token: 0x17006513 RID: 25875
		// (get) Token: 0x06014019 RID: 81945 RVA: 0x0030E432 File Offset: 0x0030C632
		public override string LocalName
		{
			get
			{
				return "tl2br";
			}
		}

		// Token: 0x17006514 RID: 25876
		// (get) Token: 0x0601401A RID: 81946 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006515 RID: 25877
		// (get) Token: 0x0601401B RID: 81947 RVA: 0x0030E439 File Offset: 0x0030C639
		internal override int ElementTypeId
		{
			get
			{
				return 10274;
			}
		}

		// Token: 0x0601401C RID: 81948 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601401D RID: 81949 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public TopLeftToBottomRightBorder()
		{
		}

		// Token: 0x0601401E RID: 81950 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public TopLeftToBottomRightBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601401F RID: 81951 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public TopLeftToBottomRightBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014020 RID: 81952 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public TopLeftToBottomRightBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014021 RID: 81953 RVA: 0x0030E440 File Offset: 0x0030C640
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopLeftToBottomRightBorder>(deep);
		}

		// Token: 0x0400889E RID: 34974
		private const string tagName = "tl2br";

		// Token: 0x0400889F RID: 34975
		private const byte tagNsId = 10;

		// Token: 0x040088A0 RID: 34976
		internal const int ElementTypeIdConst = 10274;
	}
}
