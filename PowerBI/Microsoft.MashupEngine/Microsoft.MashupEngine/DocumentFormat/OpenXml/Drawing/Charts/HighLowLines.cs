using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200253C RID: 9532
	[GeneratedCode("DomGen", "2.0")]
	internal class HighLowLines : ChartLinesType
	{
		// Token: 0x170054D9 RID: 21721
		// (get) Token: 0x06011BC6 RID: 72646 RVA: 0x002F18CB File Offset: 0x002EFACB
		public override string LocalName
		{
			get
			{
				return "hiLowLines";
			}
		}

		// Token: 0x170054DA RID: 21722
		// (get) Token: 0x06011BC7 RID: 72647 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054DB RID: 21723
		// (get) Token: 0x06011BC8 RID: 72648 RVA: 0x002F18D2 File Offset: 0x002EFAD2
		internal override int ElementTypeId
		{
			get
			{
				return 10456;
			}
		}

		// Token: 0x06011BC9 RID: 72649 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BCA RID: 72650 RVA: 0x002F185A File Offset: 0x002EFA5A
		public HighLowLines()
		{
		}

		// Token: 0x06011BCB RID: 72651 RVA: 0x002F1862 File Offset: 0x002EFA62
		public HighLowLines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BCC RID: 72652 RVA: 0x002F186B File Offset: 0x002EFA6B
		public HighLowLines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BCD RID: 72653 RVA: 0x002F1874 File Offset: 0x002EFA74
		public HighLowLines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011BCE RID: 72654 RVA: 0x002F18D9 File Offset: 0x002EFAD9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HighLowLines>(deep);
		}

		// Token: 0x04007C4B RID: 31819
		private const string tagName = "hiLowLines";

		// Token: 0x04007C4C RID: 31820
		private const byte tagNsId = 11;

		// Token: 0x04007C4D RID: 31821
		internal const int ElementTypeIdConst = 10456;
	}
}
