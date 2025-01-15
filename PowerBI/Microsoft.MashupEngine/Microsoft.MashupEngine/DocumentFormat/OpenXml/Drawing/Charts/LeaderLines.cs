using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002538 RID: 9528
	[GeneratedCode("DomGen", "2.0")]
	internal class LeaderLines : ChartLinesType
	{
		// Token: 0x170054CD RID: 21709
		// (get) Token: 0x06011BA2 RID: 72610 RVA: 0x002F184C File Offset: 0x002EFA4C
		public override string LocalName
		{
			get
			{
				return "leaderLines";
			}
		}

		// Token: 0x170054CE RID: 21710
		// (get) Token: 0x06011BA3 RID: 72611 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054CF RID: 21711
		// (get) Token: 0x06011BA4 RID: 72612 RVA: 0x002F1853 File Offset: 0x002EFA53
		internal override int ElementTypeId
		{
			get
			{
				return 10356;
			}
		}

		// Token: 0x06011BA5 RID: 72613 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BA6 RID: 72614 RVA: 0x002F185A File Offset: 0x002EFA5A
		public LeaderLines()
		{
		}

		// Token: 0x06011BA7 RID: 72615 RVA: 0x002F1862 File Offset: 0x002EFA62
		public LeaderLines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BA8 RID: 72616 RVA: 0x002F186B File Offset: 0x002EFA6B
		public LeaderLines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BA9 RID: 72617 RVA: 0x002F1874 File Offset: 0x002EFA74
		public LeaderLines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011BAA RID: 72618 RVA: 0x002F187D File Offset: 0x002EFA7D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeaderLines>(deep);
		}

		// Token: 0x04007C3F RID: 31807
		private const string tagName = "leaderLines";

		// Token: 0x04007C40 RID: 31808
		private const byte tagNsId = 11;

		// Token: 0x04007C41 RID: 31809
		internal const int ElementTypeIdConst = 10356;
	}
}
