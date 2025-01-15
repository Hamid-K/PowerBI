using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002819 RID: 10265
	[GeneratedCode("DomGen", "2.0")]
	internal class Level5ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x1700658A RID: 25994
		// (get) Token: 0x06014157 RID: 82263 RVA: 0x0030F0AD File Offset: 0x0030D2AD
		public override string LocalName
		{
			get
			{
				return "lvl5pPr";
			}
		}

		// Token: 0x1700658B RID: 25995
		// (get) Token: 0x06014158 RID: 82264 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700658C RID: 25996
		// (get) Token: 0x06014159 RID: 82265 RVA: 0x0030F0B4 File Offset: 0x0030D2B4
		internal override int ElementTypeId
		{
			get
			{
				return 10301;
			}
		}

		// Token: 0x0601415A RID: 82266 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601415B RID: 82267 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level5ParagraphProperties()
		{
		}

		// Token: 0x0601415C RID: 82268 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level5ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601415D RID: 82269 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level5ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601415E RID: 82270 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level5ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601415F RID: 82271 RVA: 0x0030F0BB File Offset: 0x0030D2BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level5ParagraphProperties>(deep);
		}

		// Token: 0x040088F5 RID: 35061
		private const string tagName = "lvl5pPr";

		// Token: 0x040088F6 RID: 35062
		private const byte tagNsId = 10;

		// Token: 0x040088F7 RID: 35063
		internal const int ElementTypeIdConst = 10301;
	}
}
