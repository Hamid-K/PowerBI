using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025CA RID: 9674
	[ChildElementInfo(typeof(StockChart))]
	[ChildElementInfo(typeof(CategoryAxis))]
	[ChildElementInfo(typeof(DateAxis))]
	[ChildElementInfo(typeof(SeriesAxis))]
	[ChildElementInfo(typeof(DataTable))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RadarChart))]
	[ChildElementInfo(typeof(ScatterChart))]
	[ChildElementInfo(typeof(PieChart))]
	[ChildElementInfo(typeof(Pie3DChart))]
	[ChildElementInfo(typeof(DoughnutChart))]
	[ChildElementInfo(typeof(BarChart))]
	[ChildElementInfo(typeof(Bar3DChart))]
	[ChildElementInfo(typeof(OfPieChart))]
	[ChildElementInfo(typeof(SurfaceChart))]
	[ChildElementInfo(typeof(Surface3DChart))]
	[ChildElementInfo(typeof(BubbleChart))]
	[ChildElementInfo(typeof(ValueAxis))]
	[ChildElementInfo(typeof(Layout))]
	[ChildElementInfo(typeof(AreaChart))]
	[ChildElementInfo(typeof(Area3DChart))]
	[ChildElementInfo(typeof(LineChart))]
	[ChildElementInfo(typeof(Line3DChart))]
	internal class PlotArea : OpenXmlCompositeElement
	{
		// Token: 0x170057B7 RID: 22455
		// (get) Token: 0x06012237 RID: 74295 RVA: 0x002F5F24 File Offset: 0x002F4124
		public override string LocalName
		{
			get
			{
				return "plotArea";
			}
		}

		// Token: 0x170057B8 RID: 22456
		// (get) Token: 0x06012238 RID: 74296 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057B9 RID: 22457
		// (get) Token: 0x06012239 RID: 74297 RVA: 0x002F5F2B File Offset: 0x002F412B
		internal override int ElementTypeId
		{
			get
			{
				return 10500;
			}
		}

		// Token: 0x0601223A RID: 74298 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601223B RID: 74299 RVA: 0x00293ECF File Offset: 0x002920CF
		public PlotArea()
		{
		}

		// Token: 0x0601223C RID: 74300 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PlotArea(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601223D RID: 74301 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PlotArea(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601223E RID: 74302 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PlotArea(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601223F RID: 74303 RVA: 0x002F5F34 File Offset: 0x002F4134
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "layout" == name)
			{
				return new Layout();
			}
			if (11 == namespaceId && "areaChart" == name)
			{
				return new AreaChart();
			}
			if (11 == namespaceId && "area3DChart" == name)
			{
				return new Area3DChart();
			}
			if (11 == namespaceId && "lineChart" == name)
			{
				return new LineChart();
			}
			if (11 == namespaceId && "line3DChart" == name)
			{
				return new Line3DChart();
			}
			if (11 == namespaceId && "stockChart" == name)
			{
				return new StockChart();
			}
			if (11 == namespaceId && "radarChart" == name)
			{
				return new RadarChart();
			}
			if (11 == namespaceId && "scatterChart" == name)
			{
				return new ScatterChart();
			}
			if (11 == namespaceId && "pieChart" == name)
			{
				return new PieChart();
			}
			if (11 == namespaceId && "pie3DChart" == name)
			{
				return new Pie3DChart();
			}
			if (11 == namespaceId && "doughnutChart" == name)
			{
				return new DoughnutChart();
			}
			if (11 == namespaceId && "barChart" == name)
			{
				return new BarChart();
			}
			if (11 == namespaceId && "bar3DChart" == name)
			{
				return new Bar3DChart();
			}
			if (11 == namespaceId && "ofPieChart" == name)
			{
				return new OfPieChart();
			}
			if (11 == namespaceId && "surfaceChart" == name)
			{
				return new SurfaceChart();
			}
			if (11 == namespaceId && "surface3DChart" == name)
			{
				return new Surface3DChart();
			}
			if (11 == namespaceId && "bubbleChart" == name)
			{
				return new BubbleChart();
			}
			if (11 == namespaceId && "valAx" == name)
			{
				return new ValueAxis();
			}
			if (11 == namespaceId && "catAx" == name)
			{
				return new CategoryAxis();
			}
			if (11 == namespaceId && "dateAx" == name)
			{
				return new DateAxis();
			}
			if (11 == namespaceId && "serAx" == name)
			{
				return new SeriesAxis();
			}
			if (11 == namespaceId && "dTable" == name)
			{
				return new DataTable();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170057BA RID: 22458
		// (get) Token: 0x06012240 RID: 74304 RVA: 0x002F6182 File Offset: 0x002F4382
		internal override string[] ElementTagNames
		{
			get
			{
				return PlotArea.eleTagNames;
			}
		}

		// Token: 0x170057BB RID: 22459
		// (get) Token: 0x06012241 RID: 74305 RVA: 0x002F6189 File Offset: 0x002F4389
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PlotArea.eleNamespaceIds;
			}
		}

		// Token: 0x170057BC RID: 22460
		// (get) Token: 0x06012242 RID: 74306 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170057BD RID: 22461
		// (get) Token: 0x06012243 RID: 74307 RVA: 0x002F4AD8 File Offset: 0x002F2CD8
		// (set) Token: 0x06012244 RID: 74308 RVA: 0x002F4AE1 File Offset: 0x002F2CE1
		public Layout Layout
		{
			get
			{
				return base.GetElement<Layout>(0);
			}
			set
			{
				base.SetElement<Layout>(0, value);
			}
		}

		// Token: 0x06012245 RID: 74309 RVA: 0x002F6190 File Offset: 0x002F4390
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PlotArea>(deep);
		}

		// Token: 0x04007E64 RID: 32356
		private const string tagName = "plotArea";

		// Token: 0x04007E65 RID: 32357
		private const byte tagNsId = 11;

		// Token: 0x04007E66 RID: 32358
		internal const int ElementTypeIdConst = 10500;

		// Token: 0x04007E67 RID: 32359
		private static readonly string[] eleTagNames = new string[]
		{
			"layout", "areaChart", "area3DChart", "lineChart", "line3DChart", "stockChart", "radarChart", "scatterChart", "pieChart", "pie3DChart",
			"doughnutChart", "barChart", "bar3DChart", "ofPieChart", "surfaceChart", "surface3DChart", "bubbleChart", "valAx", "catAx", "dateAx",
			"serAx", "dTable", "spPr", "extLst"
		};

		// Token: 0x04007E68 RID: 32360
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11
		};
	}
}
