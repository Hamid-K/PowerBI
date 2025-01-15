using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002827 RID: 10279
	[GeneratedCode("DomGen", "2.0")]
	internal class LineSpacing : TextSpacingType
	{
		// Token: 0x170065DA RID: 26074
		// (get) Token: 0x06014216 RID: 82454 RVA: 0x0030FA91 File Offset: 0x0030DC91
		public override string LocalName
		{
			get
			{
				return "lnSpc";
			}
		}

		// Token: 0x170065DB RID: 26075
		// (get) Token: 0x06014217 RID: 82455 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065DC RID: 26076
		// (get) Token: 0x06014218 RID: 82456 RVA: 0x0030FA98 File Offset: 0x0030DC98
		internal override int ElementTypeId
		{
			get
			{
				return 10311;
			}
		}

		// Token: 0x06014219 RID: 82457 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601421A RID: 82458 RVA: 0x0030FA9F File Offset: 0x0030DC9F
		public LineSpacing()
		{
		}

		// Token: 0x0601421B RID: 82459 RVA: 0x0030FAA7 File Offset: 0x0030DCA7
		public LineSpacing(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601421C RID: 82460 RVA: 0x0030FAB0 File Offset: 0x0030DCB0
		public LineSpacing(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601421D RID: 82461 RVA: 0x0030FAB9 File Offset: 0x0030DCB9
		public LineSpacing(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601421E RID: 82462 RVA: 0x0030FAC2 File Offset: 0x0030DCC2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineSpacing>(deep);
		}

		// Token: 0x04008927 RID: 35111
		private const string tagName = "lnSpc";

		// Token: 0x04008928 RID: 35112
		private const byte tagNsId = 10;

		// Token: 0x04008929 RID: 35113
		internal const int ElementTypeIdConst = 10311;
	}
}
