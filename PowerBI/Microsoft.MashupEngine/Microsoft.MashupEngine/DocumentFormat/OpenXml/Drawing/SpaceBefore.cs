using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002828 RID: 10280
	[GeneratedCode("DomGen", "2.0")]
	internal class SpaceBefore : TextSpacingType
	{
		// Token: 0x170065DD RID: 26077
		// (get) Token: 0x0601421F RID: 82463 RVA: 0x0030FACB File Offset: 0x0030DCCB
		public override string LocalName
		{
			get
			{
				return "spcBef";
			}
		}

		// Token: 0x170065DE RID: 26078
		// (get) Token: 0x06014220 RID: 82464 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065DF RID: 26079
		// (get) Token: 0x06014221 RID: 82465 RVA: 0x0030FAD2 File Offset: 0x0030DCD2
		internal override int ElementTypeId
		{
			get
			{
				return 10312;
			}
		}

		// Token: 0x06014222 RID: 82466 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014223 RID: 82467 RVA: 0x0030FA9F File Offset: 0x0030DC9F
		public SpaceBefore()
		{
		}

		// Token: 0x06014224 RID: 82468 RVA: 0x0030FAA7 File Offset: 0x0030DCA7
		public SpaceBefore(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014225 RID: 82469 RVA: 0x0030FAB0 File Offset: 0x0030DCB0
		public SpaceBefore(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014226 RID: 82470 RVA: 0x0030FAB9 File Offset: 0x0030DCB9
		public SpaceBefore(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014227 RID: 82471 RVA: 0x0030FAD9 File Offset: 0x0030DCD9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpaceBefore>(deep);
		}

		// Token: 0x0400892A RID: 35114
		private const string tagName = "spcBef";

		// Token: 0x0400892B RID: 35115
		private const byte tagNsId = 10;

		// Token: 0x0400892C RID: 35116
		internal const int ElementTypeIdConst = 10312;
	}
}
