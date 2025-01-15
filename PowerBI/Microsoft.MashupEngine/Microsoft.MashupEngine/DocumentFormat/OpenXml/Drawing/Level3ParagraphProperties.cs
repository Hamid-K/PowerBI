using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002817 RID: 10263
	[GeneratedCode("DomGen", "2.0")]
	internal class Level3ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006584 RID: 25988
		// (get) Token: 0x06014145 RID: 82245 RVA: 0x0030F07F File Offset: 0x0030D27F
		public override string LocalName
		{
			get
			{
				return "lvl3pPr";
			}
		}

		// Token: 0x17006585 RID: 25989
		// (get) Token: 0x06014146 RID: 82246 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006586 RID: 25990
		// (get) Token: 0x06014147 RID: 82247 RVA: 0x0030F086 File Offset: 0x0030D286
		internal override int ElementTypeId
		{
			get
			{
				return 10299;
			}
		}

		// Token: 0x06014148 RID: 82248 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014149 RID: 82249 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level3ParagraphProperties()
		{
		}

		// Token: 0x0601414A RID: 82250 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level3ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601414B RID: 82251 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level3ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601414C RID: 82252 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level3ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601414D RID: 82253 RVA: 0x0030F08D File Offset: 0x0030D28D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level3ParagraphProperties>(deep);
		}

		// Token: 0x040088EF RID: 35055
		private const string tagName = "lvl3pPr";

		// Token: 0x040088F0 RID: 35056
		private const byte tagNsId = 10;

		// Token: 0x040088F1 RID: 35057
		internal const int ElementTypeIdConst = 10299;
	}
}
