using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200254F RID: 9551
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(Explosion))]
	[ChildElementInfo(typeof(DataPoint))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(CategoryAxisData))]
	[ChildElementInfo(typeof(Values))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Smooth))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PieChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x1700553A RID: 21818
		// (get) Token: 0x06011C9B RID: 72859 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x1700553B RID: 21819
		// (get) Token: 0x06011C9C RID: 72860 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700553C RID: 21820
		// (get) Token: 0x06011C9D RID: 72861 RVA: 0x002F253B File Offset: 0x002F073B
		internal override int ElementTypeId
		{
			get
			{
				return 10369;
			}
		}

		// Token: 0x06011C9E RID: 72862 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C9F RID: 72863 RVA: 0x00293ECF File Offset: 0x002920CF
		public PieChartSeries()
		{
		}

		// Token: 0x06011CA0 RID: 72864 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PieChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CA1 RID: 72865 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PieChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CA2 RID: 72866 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PieChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011CA3 RID: 72867 RVA: 0x002F2544 File Offset: 0x002F0744
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "order" == name)
			{
				return new Order();
			}
			if (11 == namespaceId && "tx" == name)
			{
				return new SeriesText();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "pictureOptions" == name)
			{
				return new PictureOptions();
			}
			if (11 == namespaceId && "explosion" == name)
			{
				return new Explosion();
			}
			if (11 == namespaceId && "dPt" == name)
			{
				return new DataPoint();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "cat" == name)
			{
				return new CategoryAxisData();
			}
			if (11 == namespaceId && "val" == name)
			{
				return new Values();
			}
			if (11 == namespaceId && "bubble3D" == name)
			{
				return new Bubble3D();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			if (11 == namespaceId && "smooth" == name)
			{
				return new Smooth();
			}
			return null;
		}

		// Token: 0x1700553D RID: 21821
		// (get) Token: 0x06011CA4 RID: 72868 RVA: 0x002F268A File Offset: 0x002F088A
		internal override string[] ElementTagNames
		{
			get
			{
				return PieChartSeries.eleTagNames;
			}
		}

		// Token: 0x1700553E RID: 21822
		// (get) Token: 0x06011CA5 RID: 72869 RVA: 0x002F2691 File Offset: 0x002F0891
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PieChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x1700553F RID: 21823
		// (get) Token: 0x06011CA6 RID: 72870 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005540 RID: 21824
		// (get) Token: 0x06011CA7 RID: 72871 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06011CA8 RID: 72872 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public Index Index
		{
			get
			{
				return base.GetElement<Index>(0);
			}
			set
			{
				base.SetElement<Index>(0, value);
			}
		}

		// Token: 0x17005541 RID: 21825
		// (get) Token: 0x06011CA9 RID: 72873 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x06011CAA RID: 72874 RVA: 0x002F1CD4 File Offset: 0x002EFED4
		public Order Order
		{
			get
			{
				return base.GetElement<Order>(1);
			}
			set
			{
				base.SetElement<Order>(1, value);
			}
		}

		// Token: 0x17005542 RID: 21826
		// (get) Token: 0x06011CAB RID: 72875 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x06011CAC RID: 72876 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
		public SeriesText SeriesText
		{
			get
			{
				return base.GetElement<SeriesText>(2);
			}
			set
			{
				base.SetElement<SeriesText>(2, value);
			}
		}

		// Token: 0x17005543 RID: 21827
		// (get) Token: 0x06011CAD RID: 72877 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06011CAE RID: 72878 RVA: 0x002F1CFA File Offset: 0x002EFEFA
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(3);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(3, value);
			}
		}

		// Token: 0x17005544 RID: 21828
		// (get) Token: 0x06011CAF RID: 72879 RVA: 0x002F2478 File Offset: 0x002F0678
		// (set) Token: 0x06011CB0 RID: 72880 RVA: 0x002F2481 File Offset: 0x002F0681
		public PictureOptions PictureOptions
		{
			get
			{
				return base.GetElement<PictureOptions>(4);
			}
			set
			{
				base.SetElement<PictureOptions>(4, value);
			}
		}

		// Token: 0x17005545 RID: 21829
		// (get) Token: 0x06011CB1 RID: 72881 RVA: 0x002F2698 File Offset: 0x002F0898
		// (set) Token: 0x06011CB2 RID: 72882 RVA: 0x002F26A1 File Offset: 0x002F08A1
		public Explosion Explosion
		{
			get
			{
				return base.GetElement<Explosion>(5);
			}
			set
			{
				base.SetElement<Explosion>(5, value);
			}
		}

		// Token: 0x06011CB3 RID: 72883 RVA: 0x002F26AB File Offset: 0x002F08AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PieChartSeries>(deep);
		}

		// Token: 0x04007C91 RID: 31889
		private const string tagName = "ser";

		// Token: 0x04007C92 RID: 31890
		private const byte tagNsId = 11;

		// Token: 0x04007C93 RID: 31891
		internal const int ElementTypeIdConst = 10369;

		// Token: 0x04007C94 RID: 31892
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "pictureOptions", "explosion", "dPt", "dLbls", "cat", "val",
			"bubble3D", "extLst", "smooth"
		};

		// Token: 0x04007C95 RID: 31893
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11
		};
	}
}
