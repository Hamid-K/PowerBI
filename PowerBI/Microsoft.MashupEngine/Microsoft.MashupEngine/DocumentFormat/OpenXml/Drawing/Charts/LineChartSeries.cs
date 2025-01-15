using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002549 RID: 9545
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(Marker))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(DataPoint))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(Trendline))]
	[ChildElementInfo(typeof(ErrorBars))]
	[ChildElementInfo(typeof(CategoryAxisData))]
	[ChildElementInfo(typeof(Values))]
	[ChildElementInfo(typeof(Smooth))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(Index))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LineChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x17005508 RID: 21768
		// (get) Token: 0x06011C2D RID: 72749 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005509 RID: 21769
		// (get) Token: 0x06011C2E RID: 72750 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700550A RID: 21770
		// (get) Token: 0x06011C2F RID: 72751 RVA: 0x002F1B2A File Offset: 0x002EFD2A
		internal override int ElementTypeId
		{
			get
			{
				return 10362;
			}
		}

		// Token: 0x06011C30 RID: 72752 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C31 RID: 72753 RVA: 0x00293ECF File Offset: 0x002920CF
		public LineChartSeries()
		{
		}

		// Token: 0x06011C32 RID: 72754 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LineChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C33 RID: 72755 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LineChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C34 RID: 72756 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LineChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011C35 RID: 72757 RVA: 0x002F1B34 File Offset: 0x002EFD34
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
			if (11 == namespaceId && "marker" == name)
			{
				return new Marker();
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
			if (11 == namespaceId && "smooth" == name)
			{
				return new Smooth();
			}
			if (11 == namespaceId && "bubble3D" == name)
			{
				return new Bubble3D();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700550B RID: 21771
		// (get) Token: 0x06011C36 RID: 72758 RVA: 0x002F1CAA File Offset: 0x002EFEAA
		internal override string[] ElementTagNames
		{
			get
			{
				return LineChartSeries.eleTagNames;
			}
		}

		// Token: 0x1700550C RID: 21772
		// (get) Token: 0x06011C37 RID: 72759 RVA: 0x002F1CB1 File Offset: 0x002EFEB1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LineChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x1700550D RID: 21773
		// (get) Token: 0x06011C38 RID: 72760 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700550E RID: 21774
		// (get) Token: 0x06011C39 RID: 72761 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06011C3A RID: 72762 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x1700550F RID: 21775
		// (get) Token: 0x06011C3B RID: 72763 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x06011C3C RID: 72764 RVA: 0x002F1CD4 File Offset: 0x002EFED4
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

		// Token: 0x17005510 RID: 21776
		// (get) Token: 0x06011C3D RID: 72765 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x06011C3E RID: 72766 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
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

		// Token: 0x17005511 RID: 21777
		// (get) Token: 0x06011C3F RID: 72767 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06011C40 RID: 72768 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x17005512 RID: 21778
		// (get) Token: 0x06011C41 RID: 72769 RVA: 0x002F1D04 File Offset: 0x002EFF04
		// (set) Token: 0x06011C42 RID: 72770 RVA: 0x002F1D0D File Offset: 0x002EFF0D
		public Marker Marker
		{
			get
			{
				return base.GetElement<Marker>(4);
			}
			set
			{
				base.SetElement<Marker>(4, value);
			}
		}

		// Token: 0x17005513 RID: 21779
		// (get) Token: 0x06011C43 RID: 72771 RVA: 0x002F1D17 File Offset: 0x002EFF17
		// (set) Token: 0x06011C44 RID: 72772 RVA: 0x002F1D20 File Offset: 0x002EFF20
		public PictureOptions PictureOptions
		{
			get
			{
				return base.GetElement<PictureOptions>(5);
			}
			set
			{
				base.SetElement<PictureOptions>(5, value);
			}
		}

		// Token: 0x06011C45 RID: 72773 RVA: 0x002F1D2A File Offset: 0x002EFF2A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineChartSeries>(deep);
		}

		// Token: 0x04007C75 RID: 31861
		private const string tagName = "ser";

		// Token: 0x04007C76 RID: 31862
		private const byte tagNsId = 11;

		// Token: 0x04007C77 RID: 31863
		internal const int ElementTypeIdConst = 10362;

		// Token: 0x04007C78 RID: 31864
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "marker", "pictureOptions", "dPt", "dLbls", "trendline", "errBars",
			"cat", "val", "smooth", "bubble3D", "extLst"
		};

		// Token: 0x04007C79 RID: 31865
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11
		};
	}
}
