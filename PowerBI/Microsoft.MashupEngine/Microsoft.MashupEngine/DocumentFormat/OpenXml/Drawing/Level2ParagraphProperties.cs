using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002816 RID: 10262
	[GeneratedCode("DomGen", "2.0")]
	internal class Level2ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006581 RID: 25985
		// (get) Token: 0x0601413C RID: 82236 RVA: 0x0030F068 File Offset: 0x0030D268
		public override string LocalName
		{
			get
			{
				return "lvl2pPr";
			}
		}

		// Token: 0x17006582 RID: 25986
		// (get) Token: 0x0601413D RID: 82237 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006583 RID: 25987
		// (get) Token: 0x0601413E RID: 82238 RVA: 0x0030F06F File Offset: 0x0030D26F
		internal override int ElementTypeId
		{
			get
			{
				return 10298;
			}
		}

		// Token: 0x0601413F RID: 82239 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014140 RID: 82240 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level2ParagraphProperties()
		{
		}

		// Token: 0x06014141 RID: 82241 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level2ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014142 RID: 82242 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level2ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014143 RID: 82243 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level2ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014144 RID: 82244 RVA: 0x0030F076 File Offset: 0x0030D276
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level2ParagraphProperties>(deep);
		}

		// Token: 0x040088EC RID: 35052
		private const string tagName = "lvl2pPr";

		// Token: 0x040088ED RID: 35053
		private const byte tagNsId = 10;

		// Token: 0x040088EE RID: 35054
		internal const int ElementTypeIdConst = 10298;
	}
}
