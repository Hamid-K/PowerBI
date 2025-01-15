using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200253A RID: 9530
	[GeneratedCode("DomGen", "2.0")]
	internal class MajorGridlines : ChartLinesType
	{
		// Token: 0x170054D3 RID: 21715
		// (get) Token: 0x06011BB4 RID: 72628 RVA: 0x002F189D File Offset: 0x002EFA9D
		public override string LocalName
		{
			get
			{
				return "majorGridlines";
			}
		}

		// Token: 0x170054D4 RID: 21716
		// (get) Token: 0x06011BB5 RID: 72629 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054D5 RID: 21717
		// (get) Token: 0x06011BB6 RID: 72630 RVA: 0x002F18A4 File Offset: 0x002EFAA4
		internal override int ElementTypeId
		{
			get
			{
				return 10377;
			}
		}

		// Token: 0x06011BB7 RID: 72631 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011BB8 RID: 72632 RVA: 0x002F185A File Offset: 0x002EFA5A
		public MajorGridlines()
		{
		}

		// Token: 0x06011BB9 RID: 72633 RVA: 0x002F1862 File Offset: 0x002EFA62
		public MajorGridlines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BBA RID: 72634 RVA: 0x002F186B File Offset: 0x002EFA6B
		public MajorGridlines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011BBB RID: 72635 RVA: 0x002F1874 File Offset: 0x002EFA74
		public MajorGridlines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011BBC RID: 72636 RVA: 0x002F18AB File Offset: 0x002EFAAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MajorGridlines>(deep);
		}

		// Token: 0x04007C45 RID: 31813
		private const string tagName = "majorGridlines";

		// Token: 0x04007C46 RID: 31814
		private const byte tagNsId = 11;

		// Token: 0x04007C47 RID: 31815
		internal const int ElementTypeIdConst = 10377;
	}
}
