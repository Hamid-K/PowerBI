using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002539 RID: 9529
	[GeneratedCode("DomGen", "2.0")]
	internal class DropLines : ChartLinesType
	{
		// Token: 0x170054D0 RID: 21712
		// (get) Token: 0x06011BAB RID: 72619 RVA: 0x002F1886 File Offset: 0x002EFA86
		public override string LocalName
		{
			get
			{
				return "dropLines";
			}
		}

		// Token: 0x170054D1 RID: 21713
		// (get) Token: 0x06011BAC RID: 72620 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054D2 RID: 21714
		// (get) Token: 0x06011BAD RID: 72621 RVA: 0x002F188D File Offset: 0x002EFA8D
		internal override int ElementTypeId
		{
			get
			{
				return 10364;
			}
		}

		// Token: 0x06011BAE RID: 72622 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BAF RID: 72623 RVA: 0x002F185A File Offset: 0x002EFA5A
		public DropLines()
		{
		}

		// Token: 0x06011BB0 RID: 72624 RVA: 0x002F1862 File Offset: 0x002EFA62
		public DropLines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BB1 RID: 72625 RVA: 0x002F186B File Offset: 0x002EFA6B
		public DropLines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BB2 RID: 72626 RVA: 0x002F1874 File Offset: 0x002EFA74
		public DropLines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011BB3 RID: 72627 RVA: 0x002F1894 File Offset: 0x002EFA94
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropLines>(deep);
		}

		// Token: 0x04007C42 RID: 31810
		private const string tagName = "dropLines";

		// Token: 0x04007C43 RID: 31811
		private const byte tagNsId = 11;

		// Token: 0x04007C44 RID: 31812
		internal const int ElementTypeIdConst = 10364;
	}
}
