using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200254D RID: 9549
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(InvertIfNegative))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(DataPoint))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(Trendline))]
	[ChildElementInfo(typeof(ErrorBars))]
	[ChildElementInfo(typeof(CategoryAxisData))]
	[ChildElementInfo(typeof(Values))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(Smooth))]
	[ChildElementInfo(typeof(BarSerExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BarChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x17005523 RID: 21795
		// (get) Token: 0x06011C69 RID: 72809 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005524 RID: 21796
		// (get) Token: 0x06011C6A RID: 72810 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005525 RID: 21797
		// (get) Token: 0x06011C6B RID: 72811 RVA: 0x002F208B File Offset: 0x002F028B
		internal override int ElementTypeId
		{
			get
			{
				return 10367;
			}
		}

		// Token: 0x06011C6C RID: 72812 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C6D RID: 72813 RVA: 0x00293ECF File Offset: 0x002920CF
		public BarChartSeries()
		{
		}

		// Token: 0x06011C6E RID: 72814 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BarChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C6F RID: 72815 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BarChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C70 RID: 72816 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BarChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011C71 RID: 72817 RVA: 0x002F2094 File Offset: 0x002F0294
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
			if (11 == namespaceId && "invertIfNegative" == name)
			{
				return new InvertIfNegative();
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
			if (11 == namespaceId && "shape" == name)
			{
				return new Shape();
			}
			if (11 == namespaceId && "bubble3D" == name)
			{
				return new Bubble3D();
			}
			if (11 == namespaceId && "smooth" == name)
			{
				return new Smooth();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new BarSerExtensionList();
			}
			return null;
		}

		// Token: 0x17005526 RID: 21798
		// (get) Token: 0x06011C72 RID: 72818 RVA: 0x002F2222 File Offset: 0x002F0422
		internal override string[] ElementTagNames
		{
			get
			{
				return BarChartSeries.eleTagNames;
			}
		}

		// Token: 0x17005527 RID: 21799
		// (get) Token: 0x06011C73 RID: 72819 RVA: 0x002F2229 File Offset: 0x002F0429
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BarChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x17005528 RID: 21800
		// (get) Token: 0x06011C74 RID: 72820 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005529 RID: 21801
		// (get) Token: 0x06011C75 RID: 72821 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06011C76 RID: 72822 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x1700552A RID: 21802
		// (get) Token: 0x06011C77 RID: 72823 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x06011C78 RID: 72824 RVA: 0x002F1CD4 File Offset: 0x002EFED4
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

		// Token: 0x1700552B RID: 21803
		// (get) Token: 0x06011C79 RID: 72825 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x06011C7A RID: 72826 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
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

		// Token: 0x1700552C RID: 21804
		// (get) Token: 0x06011C7B RID: 72827 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06011C7C RID: 72828 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x1700552D RID: 21805
		// (get) Token: 0x06011C7D RID: 72829 RVA: 0x002F2230 File Offset: 0x002F0430
		// (set) Token: 0x06011C7E RID: 72830 RVA: 0x002F2239 File Offset: 0x002F0439
		public InvertIfNegative InvertIfNegative
		{
			get
			{
				return base.GetElement<InvertIfNegative>(4);
			}
			set
			{
				base.SetElement<InvertIfNegative>(4, value);
			}
		}

		// Token: 0x1700552E RID: 21806
		// (get) Token: 0x06011C7F RID: 72831 RVA: 0x002F1D17 File Offset: 0x002EFF17
		// (set) Token: 0x06011C80 RID: 72832 RVA: 0x002F1D20 File Offset: 0x002EFF20
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

		// Token: 0x06011C81 RID: 72833 RVA: 0x002F2243 File Offset: 0x002F0443
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarChartSeries>(deep);
		}

		// Token: 0x04007C87 RID: 31879
		private const string tagName = "ser";

		// Token: 0x04007C88 RID: 31880
		private const byte tagNsId = 11;

		// Token: 0x04007C89 RID: 31881
		internal const int ElementTypeIdConst = 10367;

		// Token: 0x04007C8A RID: 31882
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "invertIfNegative", "pictureOptions", "dPt", "dLbls", "trendline", "errBars",
			"cat", "val", "shape", "bubble3D", "smooth", "extLst"
		};

		// Token: 0x04007C8B RID: 31883
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11
		};
	}
}
