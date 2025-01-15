using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200281D RID: 10269
	[GeneratedCode("DomGen", "2.0")]
	internal class Level9ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006596 RID: 26006
		// (get) Token: 0x0601417B RID: 82299 RVA: 0x0030F109 File Offset: 0x0030D309
		public override string LocalName
		{
			get
			{
				return "lvl9pPr";
			}
		}

		// Token: 0x17006597 RID: 26007
		// (get) Token: 0x0601417C RID: 82300 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006598 RID: 26008
		// (get) Token: 0x0601417D RID: 82301 RVA: 0x0030F110 File Offset: 0x0030D310
		internal override int ElementTypeId
		{
			get
			{
				return 10305;
			}
		}

		// Token: 0x0601417E RID: 82302 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601417F RID: 82303 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level9ParagraphProperties()
		{
		}

		// Token: 0x06014180 RID: 82304 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level9ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014181 RID: 82305 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level9ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014182 RID: 82306 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level9ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014183 RID: 82307 RVA: 0x0030F117 File Offset: 0x0030D317
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level9ParagraphProperties>(deep);
		}

		// Token: 0x04008901 RID: 35073
		private const string tagName = "lvl9pPr";

		// Token: 0x04008902 RID: 35074
		private const byte tagNsId = 10;

		// Token: 0x04008903 RID: 35075
		internal const int ElementTypeIdConst = 10305;
	}
}
