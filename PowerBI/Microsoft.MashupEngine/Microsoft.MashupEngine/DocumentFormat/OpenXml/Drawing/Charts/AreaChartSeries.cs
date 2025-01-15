using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200254E RID: 9550
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(DataPoint))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(Trendline))]
	[ChildElementInfo(typeof(ErrorBars))]
	[ChildElementInfo(typeof(CategoryAxisData))]
	[ChildElementInfo(typeof(Values))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Smooth))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AreaChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x1700552F RID: 21807
		// (get) Token: 0x06011C83 RID: 72835 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005530 RID: 21808
		// (get) Token: 0x06011C84 RID: 72836 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005531 RID: 21809
		// (get) Token: 0x06011C85 RID: 72837 RVA: 0x002F2305 File Offset: 0x002F0505
		internal override int ElementTypeId
		{
			get
			{
				return 10368;
			}
		}

		// Token: 0x06011C86 RID: 72838 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C87 RID: 72839 RVA: 0x00293ECF File Offset: 0x002920CF
		public AreaChartSeries()
		{
		}

		// Token: 0x06011C88 RID: 72840 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AreaChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C89 RID: 72841 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AreaChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C8A RID: 72842 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AreaChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011C8B RID: 72843 RVA: 0x002F230C File Offset: 0x002F050C
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
			if (11 == namespaceId && "dPt" == name)
			{
				return new DataPoint();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "trendline" == name)
			{
				return new Trendline();
			}
			if (11 == namespaceId && "errBars" == name)
			{
				return new ErrorBars();
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

		// Token: 0x17005532 RID: 21810
		// (get) Token: 0x06011C8C RID: 72844 RVA: 0x002F246A File Offset: 0x002F066A
		internal override string[] ElementTagNames
		{
			get
			{
				return AreaChartSeries.eleTagNames;
			}
		}

		// Token: 0x17005533 RID: 21811
		// (get) Token: 0x06011C8D RID: 72845 RVA: 0x002F2471 File Offset: 0x002F0671
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AreaChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x17005534 RID: 21812
		// (get) Token: 0x06011C8E RID: 72846 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005535 RID: 21813
		// (get) Token: 0x06011C8F RID: 72847 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06011C90 RID: 72848 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x17005536 RID: 21814
		// (get) Token: 0x06011C91 RID: 72849 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x06011C92 RID: 72850 RVA: 0x002F1CD4 File Offset: 0x002EFED4
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

		// Token: 0x17005537 RID: 21815
		// (get) Token: 0x06011C93 RID: 72851 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x06011C94 RID: 72852 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
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

		// Token: 0x17005538 RID: 21816
		// (get) Token: 0x06011C95 RID: 72853 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06011C96 RID: 72854 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x17005539 RID: 21817
		// (get) Token: 0x06011C97 RID: 72855 RVA: 0x002F2478 File Offset: 0x002F0678
		// (set) Token: 0x06011C98 RID: 72856 RVA: 0x002F2481 File Offset: 0x002F0681
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

		// Token: 0x06011C99 RID: 72857 RVA: 0x002F248B File Offset: 0x002F068B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AreaChartSeries>(deep);
		}

		// Token: 0x04007C8C RID: 31884
		private const string tagName = "ser";

		// Token: 0x04007C8D RID: 31885
		private const byte tagNsId = 11;

		// Token: 0x04007C8E RID: 31886
		internal const int ElementTypeIdConst = 10368;

		// Token: 0x04007C8F RID: 31887
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "pictureOptions", "dPt", "dLbls", "trendline", "errBars", "cat",
			"val", "bubble3D", "extLst", "smooth"
		};

		// Token: 0x04007C90 RID: 31888
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11
		};
	}
}
