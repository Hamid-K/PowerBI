using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200276A RID: 10090
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableProperties))]
	[ChildElementInfo(typeof(TableGrid))]
	[ChildElementInfo(typeof(TableRow))]
	internal class Table : OpenXmlCompositeElement
	{
		// Token: 0x17006128 RID: 24872
		// (get) Token: 0x06013737 RID: 79671 RVA: 0x003073E6 File Offset: 0x003055E6
		public override string LocalName
		{
			get
			{
				return "tbl";
			}
		}

		// Token: 0x17006129 RID: 24873
		// (get) Token: 0x06013738 RID: 79672 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700612A RID: 24874
		// (get) Token: 0x06013739 RID: 79673 RVA: 0x003073ED File Offset: 0x003055ED
		internal override int ElementTypeId
		{
			get
			{
				return 10125;
			}
		}

		// Token: 0x0601373A RID: 79674 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601373B RID: 79675 RVA: 0x00293ECF File Offset: 0x002920CF
		public Table()
		{
		}

		// Token: 0x0601373C RID: 79676 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Table(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601373D RID: 79677 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Table(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601373E RID: 79678 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Table(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601373F RID: 79679 RVA: 0x003073F4 File Offset: 0x003055F4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tblPr" == name)
			{
				return new TableProperties();
			}
			if (10 == namespaceId && "tblGrid" == name)
			{
				return new TableGrid();
			}
			if (10 == namespaceId && "tr" == name)
			{
				return new TableRow();
			}
			return null;
		}

		// Token: 0x1700612B RID: 24875
		// (get) Token: 0x06013740 RID: 79680 RVA: 0x0030744A File Offset: 0x0030564A
		internal override string[] ElementTagNames
		{
			get
			{
				return Table.eleTagNames;
			}
		}

		// Token: 0x1700612C RID: 24876
		// (get) Token: 0x06013741 RID: 79681 RVA: 0x00307451 File Offset: 0x00305651
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Table.eleNamespaceIds;
			}
		}

		// Token: 0x1700612D RID: 24877
		// (get) Token: 0x06013742 RID: 79682 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700612E RID: 24878
		// (get) Token: 0x06013743 RID: 79683 RVA: 0x00307458 File Offset: 0x00305658
		// (set) Token: 0x06013744 RID: 79684 RVA: 0x00307461 File Offset: 0x00305661
		public TableProperties TableProperties
		{
			get
			{
				return base.GetElement<TableProperties>(0);
			}
			set
			{
				base.SetElement<TableProperties>(0, value);
			}
		}

		// Token: 0x1700612F RID: 24879
		// (get) Token: 0x06013745 RID: 79685 RVA: 0x0030746B File Offset: 0x0030566B
		// (set) Token: 0x06013746 RID: 79686 RVA: 0x00307474 File Offset: 0x00305674
		public TableGrid TableGrid
		{
			get
			{
				return base.GetElement<TableGrid>(1);
			}
			set
			{
				base.SetElement<TableGrid>(1, value);
			}
		}

		// Token: 0x06013747 RID: 79687 RVA: 0x0030747E File Offset: 0x0030567E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Table>(deep);
		}

		// Token: 0x04008645 RID: 34373
		private const string tagName = "tbl";

		// Token: 0x04008646 RID: 34374
		private const byte tagNsId = 10;

		// Token: 0x04008647 RID: 34375
		internal const int ElementTypeIdConst = 10125;

		// Token: 0x04008648 RID: 34376
		private static readonly string[] eleTagNames = new string[] { "tblPr", "tblGrid", "tr" };

		// Token: 0x04008649 RID: 34377
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
