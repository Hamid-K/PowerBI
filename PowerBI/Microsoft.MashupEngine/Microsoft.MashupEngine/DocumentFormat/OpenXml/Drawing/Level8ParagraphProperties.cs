using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200281C RID: 10268
	[GeneratedCode("DomGen", "2.0")]
	internal class Level8ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006593 RID: 26003
		// (get) Token: 0x06014172 RID: 82290 RVA: 0x0030F0F2 File Offset: 0x0030D2F2
		public override string LocalName
		{
			get
			{
				return "lvl8pPr";
			}
		}

		// Token: 0x17006594 RID: 26004
		// (get) Token: 0x06014173 RID: 82291 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006595 RID: 26005
		// (get) Token: 0x06014174 RID: 82292 RVA: 0x0030F0F9 File Offset: 0x0030D2F9
		internal override int ElementTypeId
		{
			get
			{
				return 10304;
			}
		}

		// Token: 0x06014175 RID: 82293 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014176 RID: 82294 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level8ParagraphProperties()
		{
		}

		// Token: 0x06014177 RID: 82295 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level8ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014178 RID: 82296 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level8ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014179 RID: 82297 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level8ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601417A RID: 82298 RVA: 0x0030F100 File Offset: 0x0030D300
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level8ParagraphProperties>(deep);
		}

		// Token: 0x040088FE RID: 35070
		private const string tagName = "lvl8pPr";

		// Token: 0x040088FF RID: 35071
		private const byte tagNsId = 10;

		// Token: 0x04008900 RID: 35072
		internal const int ElementTypeIdConst = 10304;
	}
}
