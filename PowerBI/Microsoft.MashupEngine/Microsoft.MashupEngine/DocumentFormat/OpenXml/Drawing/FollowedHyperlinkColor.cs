using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002787 RID: 10119
	[GeneratedCode("DomGen", "2.0")]
	internal class FollowedHyperlinkColor : Color2Type
	{
		// Token: 0x170061CB RID: 25035
		// (get) Token: 0x060138C9 RID: 80073 RVA: 0x0030832C File Offset: 0x0030652C
		public override string LocalName
		{
			get
			{
				return "folHlink";
			}
		}

		// Token: 0x170061CC RID: 25036
		// (get) Token: 0x060138CA RID: 80074 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061CD RID: 25037
		// (get) Token: 0x060138CB RID: 80075 RVA: 0x00308333 File Offset: 0x00306533
		internal override int ElementTypeId
		{
			get
			{
				return 10158;
			}
		}

		// Token: 0x060138CC RID: 80076 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138CD RID: 80077 RVA: 0x0030821A File Offset: 0x0030641A
		public FollowedHyperlinkColor()
		{
		}

		// Token: 0x060138CE RID: 80078 RVA: 0x00308222 File Offset: 0x00306422
		public FollowedHyperlinkColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138CF RID: 80079 RVA: 0x0030822B File Offset: 0x0030642B
		public FollowedHyperlinkColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138D0 RID: 80080 RVA: 0x00308234 File Offset: 0x00306434
		public FollowedHyperlinkColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060138D1 RID: 80081 RVA: 0x0030833A File Offset: 0x0030653A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FollowedHyperlinkColor>(deep);
		}

		// Token: 0x040086B2 RID: 34482
		private const string tagName = "folHlink";

		// Token: 0x040086B3 RID: 34483
		private const byte tagNsId = 10;

		// Token: 0x040086B4 RID: 34484
		internal const int ElementTypeIdConst = 10158;
	}
}
