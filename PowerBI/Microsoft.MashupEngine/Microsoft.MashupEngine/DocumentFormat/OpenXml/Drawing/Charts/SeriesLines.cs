using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200253D RID: 9533
	[GeneratedCode("DomGen", "2.0")]
	internal class SeriesLines : ChartLinesType
	{
		// Token: 0x170054DC RID: 21724
		// (get) Token: 0x06011BCF RID: 72655 RVA: 0x002F18E2 File Offset: 0x002EFAE2
		public override string LocalName
		{
			get
			{
				return "serLines";
			}
		}

		// Token: 0x170054DD RID: 21725
		// (get) Token: 0x06011BD0 RID: 72656 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054DE RID: 21726
		// (get) Token: 0x06011BD1 RID: 72657 RVA: 0x002F18E9 File Offset: 0x002EFAE9
		internal override int ElementTypeId
		{
			get
			{
				return 10467;
			}
		}

		// Token: 0x06011BD2 RID: 72658 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BD3 RID: 72659 RVA: 0x002F185A File Offset: 0x002EFA5A
		public SeriesLines()
		{
		}

		// Token: 0x06011BD4 RID: 72660 RVA: 0x002F1862 File Offset: 0x002EFA62
		public SeriesLines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BD5 RID: 72661 RVA: 0x002F186B File Offset: 0x002EFA6B
		public SeriesLines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BD6 RID: 72662 RVA: 0x002F1874 File Offset: 0x002EFA74
		public SeriesLines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011BD7 RID: 72663 RVA: 0x002F18F0 File Offset: 0x002EFAF0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeriesLines>(deep);
		}

		// Token: 0x04007C4E RID: 31822
		private const string tagName = "serLines";

		// Token: 0x04007C4F RID: 31823
		private const byte tagNsId = 11;

		// Token: 0x04007C50 RID: 31824
		internal const int ElementTypeIdConst = 10467;
	}
}
