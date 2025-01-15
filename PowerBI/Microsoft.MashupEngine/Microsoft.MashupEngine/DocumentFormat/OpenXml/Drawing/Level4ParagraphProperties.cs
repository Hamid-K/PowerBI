using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002818 RID: 10264
	[GeneratedCode("DomGen", "2.0")]
	internal class Level4ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006587 RID: 25991
		// (get) Token: 0x0601414E RID: 82254 RVA: 0x0030F096 File Offset: 0x0030D296
		public override string LocalName
		{
			get
			{
				return "lvl4pPr";
			}
		}

		// Token: 0x17006588 RID: 25992
		// (get) Token: 0x0601414F RID: 82255 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006589 RID: 25993
		// (get) Token: 0x06014150 RID: 82256 RVA: 0x0030F09D File Offset: 0x0030D29D
		internal override int ElementTypeId
		{
			get
			{
				return 10300;
			}
		}

		// Token: 0x06014151 RID: 82257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014152 RID: 82258 RVA: 0x0030F00E File Offset: 0x0030D20E
		public Level4ParagraphProperties()
		{
		}

		// Token: 0x06014153 RID: 82259 RVA: 0x0030F016 File Offset: 0x0030D216
		public Level4ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014154 RID: 82260 RVA: 0x0030F01F File Offset: 0x0030D21F
		public Level4ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014155 RID: 82261 RVA: 0x0030F028 File Offset: 0x0030D228
		public Level4ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014156 RID: 82262 RVA: 0x0030F0A4 File Offset: 0x0030D2A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level4ParagraphProperties>(deep);
		}

		// Token: 0x040088F2 RID: 35058
		private const string tagName = "lvl4pPr";

		// Token: 0x040088F3 RID: 35059
		private const byte tagNsId = 10;

		// Token: 0x040088F4 RID: 35060
		internal const int ElementTypeIdConst = 10300;
	}
}
