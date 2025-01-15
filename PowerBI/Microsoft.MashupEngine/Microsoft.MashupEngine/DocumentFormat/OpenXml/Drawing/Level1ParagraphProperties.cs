using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002815 RID: 10261
	[GeneratedCode("DomGen", "2.0")]
	internal class Level1ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x1700657E RID: 25982
		// (get) Token: 0x06014133 RID: 82227 RVA: 0x0030F051 File Offset: 0x0030D251
		public override string LocalName
		{
			get
			{
				return "lvl1pPr";
			}
		}

		// Token: 0x1700657F RID: 25983
		// (get) Token: 0x06014134 RID: 82228 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006580 RID: 25984
		// (get) Token: 0x06014135 RID: 82229 RVA: 0x0030F058 File Offset: 0x0030D258
		internal override int ElementTypeId
		{
			get
			{
				return 10297;
			}
		}

		// Token: 0x06014136 RID: 82230 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014137 RID: 82231 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level1ParagraphProperties()
		{
		}

		// Token: 0x06014138 RID: 82232 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level1ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014139 RID: 82233 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level1ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601413A RID: 82234 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level1ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601413B RID: 82235 RVA: 0x0030F05F File Offset: 0x0030D25F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level1ParagraphProperties>(deep);
		}

		// Token: 0x040088E9 RID: 35049
		private const string tagName = "lvl1pPr";

		// Token: 0x040088EA RID: 35050
		private const byte tagNsId = 10;

		// Token: 0x040088EB RID: 35051
		internal const int ElementTypeIdConst = 10297;
	}
}
