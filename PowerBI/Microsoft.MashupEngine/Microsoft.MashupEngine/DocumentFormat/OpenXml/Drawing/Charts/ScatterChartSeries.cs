using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D9 RID: 9689
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Marker))]
	[ChildElementInfo(typeof(DataPoint))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(Trendline))]
	[ChildElementInfo(typeof(ErrorBars))]
	[ChildElementInfo(typeof(XValues))]
	[ChildElementInfo(typeof(YValues))]
	[ChildElementInfo(typeof(Smooth))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	internal class ScatterChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x17005848 RID: 22600
		// (get) Token: 0x0601236E RID: 74606 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005849 RID: 22601
		// (get) Token: 0x0601236F RID: 74607 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700584A RID: 22602
		// (get) Token: 0x06012370 RID: 74608 RVA: 0x002F7517 File Offset: 0x002F5717
		internal override int ElementTypeId
		{
			get
			{
				return 10531;
			}
		}

		// Token: 0x06012371 RID: 74609 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012372 RID: 74610 RVA: 0x00293ECF File Offset: 0x002920CF
		public ScatterChartSeries()
		{
		}

		// Token: 0x06012373 RID: 74611 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ScatterChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012374 RID: 74612 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ScatterChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012375 RID: 74613 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ScatterChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012376 RID: 74614 RVA: 0x002F7520 File Offset: 0x002F5720
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
			if (11 == namespaceId && "xVal" == name)
			{
				return new XValues();
			}
			if (11 == namespaceId && "yVal" == name)
			{
				return new YValues();
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

		// Token: 0x1700584B RID: 22603
		// (get) Token: 0x06012377 RID: 74615 RVA: 0x002F767E File Offset: 0x002F587E
		internal override string[] ElementTagNames
		{
			get
			{
				return ScatterChartSeries.eleTagNames;
			}
		}

		// Token: 0x1700584C RID: 22604
		// (get) Token: 0x06012378 RID: 74616 RVA: 0x002F7685 File Offset: 0x002F5885
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ScatterChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x1700584D RID: 22605
		// (get) Token: 0x06012379 RID: 74617 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700584E RID: 22606
		// (get) Token: 0x0601237A RID: 74618 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x0601237B RID: 74619 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x1700584F RID: 22607
		// (get) Token: 0x0601237C RID: 74620 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x0601237D RID: 74621 RVA: 0x002F1CD4 File Offset: 0x002EFED4
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

		// Token: 0x17005850 RID: 22608
		// (get) Token: 0x0601237E RID: 74622 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x0601237F RID: 74623 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
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

		// Token: 0x17005851 RID: 22609
		// (get) Token: 0x06012380 RID: 74624 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06012381 RID: 74625 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x17005852 RID: 22610
		// (get) Token: 0x06012382 RID: 74626 RVA: 0x002F1D04 File Offset: 0x002EFF04
		// (set) Token: 0x06012383 RID: 74627 RVA: 0x002F1D0D File Offset: 0x002EFF0D
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

		// Token: 0x06012384 RID: 74628 RVA: 0x002F768C File Offset: 0x002F588C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScatterChartSeries>(deep);
		}

		// Token: 0x04007EAC RID: 32428
		private const string tagName = "ser";

		// Token: 0x04007EAD RID: 32429
		private const byte tagNsId = 11;

		// Token: 0x04007EAE RID: 32430
		internal const int ElementTypeIdConst = 10531;

		// Token: 0x04007EAF RID: 32431
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "marker", "dPt", "dLbls", "trendline", "errBars", "xVal",
			"yVal", "smooth", "bubble3D", "extLst"
		};

		// Token: 0x04007EB0 RID: 32432
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11
		};
	}
}
