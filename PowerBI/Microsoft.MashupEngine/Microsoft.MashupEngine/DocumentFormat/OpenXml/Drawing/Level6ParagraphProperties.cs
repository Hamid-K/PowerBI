using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200281A RID: 10266
	[GeneratedCode("DomGen", "2.0")]
	internal class Level6ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x1700658D RID: 25997
		// (get) Token: 0x06014160 RID: 82272 RVA: 0x0030F0C4 File Offset: 0x0030D2C4
		public override string LocalName
		{
			get
			{
				return "lvl6pPr";
			}
		}

		// Token: 0x1700658E RID: 25998
		// (get) Token: 0x06014161 RID: 82273 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700658F RID: 25999
		// (get) Token: 0x06014162 RID: 82274 RVA: 0x0030F0CB File Offset: 0x0030D2CB
		internal override int ElementTypeId
		{
			get
			{
				return 10302;
			}
		}

		// Token: 0x06014163 RID: 82275 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014164 RID: 82276 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level6ParagraphProperties()
		{
		}

		// Token: 0x06014165 RID: 82277 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level6ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014166 RID: 82278 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level6ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014167 RID: 82279 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level6ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014168 RID: 82280 RVA: 0x0030F0D2 File Offset: 0x0030D2D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level6ParagraphProperties>(deep);
		}

		// Token: 0x040088F8 RID: 35064
		private const string tagName = "lvl6pPr";

		// Token: 0x040088F9 RID: 35065
		private const byte tagNsId = 10;

		// Token: 0x040088FA RID: 35066
		internal const int ElementTypeIdConst = 10302;
	}
}
