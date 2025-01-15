using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200253B RID: 9531
	[GeneratedCode("DomGen", "2.0")]
	internal class MinorGridlines : ChartLinesType
	{
		// Token: 0x170054D6 RID: 21718
		// (get) Token: 0x06011BBD RID: 72637 RVA: 0x002F18B4 File Offset: 0x002EFAB4
		public override string LocalName
		{
			get
			{
				return "minorGridlines";
			}
		}

		// Token: 0x170054D7 RID: 21719
		// (get) Token: 0x06011BBE RID: 72638 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054D8 RID: 21720
		// (get) Token: 0x06011BBF RID: 72639 RVA: 0x002F18BB File Offset: 0x002EFABB
		internal override int ElementTypeId
		{
			get
			{
				return 10378;
			}
		}

		// Token: 0x06011BC0 RID: 72640 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BC1 RID: 72641 RVA: 0x002F185A File Offset: 0x002EFA5A
		public MinorGridlines()
		{
		}

		// Token: 0x06011BC2 RID: 72642 RVA: 0x002F1862 File Offset: 0x002EFA62
		public MinorGridlines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BC3 RID: 72643 RVA: 0x002F186B File Offset: 0x002EFA6B
		public MinorGridlines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BC4 RID: 72644 RVA: 0x002F1874 File Offset: 0x002EFA74
		public MinorGridlines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011BC5 RID: 72645 RVA: 0x002F18C2 File Offset: 0x002EFAC2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinorGridlines>(deep);
		}

		// Token: 0x04007C48 RID: 31816
		private const string tagName = "minorGridlines";

		// Token: 0x04007C49 RID: 31817
		private const byte tagNsId = 11;

		// Token: 0x04007C4A RID: 31818
		internal const int ElementTypeIdConst = 10378;
	}
}
