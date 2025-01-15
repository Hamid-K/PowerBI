using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200281B RID: 10267
	[GeneratedCode("DomGen", "2.0")]
	internal class Level7ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006590 RID: 26000
		// (get) Token: 0x06014169 RID: 82281 RVA: 0x0030F0DB File Offset: 0x0030D2DB
		public override string LocalName
		{
			get
			{
				return "lvl7pPr";
			}
		}

		// Token: 0x17006591 RID: 26001
		// (get) Token: 0x0601416A RID: 82282 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006592 RID: 26002
		// (get) Token: 0x0601416B RID: 82283 RVA: 0x0030F0E2 File Offset: 0x0030D2E2
		internal override int ElementTypeId
		{
			get
			{
				return 10303;
			}
		}

		// Token: 0x0601416C RID: 82284 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601416D RID: 82285 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level7ParagraphProperties()
		{
		}

		// Token: 0x0601416E RID: 82286 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level7ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601416F RID: 82287 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level7ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014170 RID: 82288 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level7ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014171 RID: 82289 RVA: 0x0030F0E9 File Offset: 0x0030D2E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level7ParagraphProperties>(deep);
		}

		// Token: 0x040088FB RID: 35067
		private const string tagName = "lvl7pPr";

		// Token: 0x040088FC RID: 35068
		private const byte tagNsId = 10;

		// Token: 0x040088FD RID: 35069
		internal const int ElementTypeIdConst = 10303;
	}
}
