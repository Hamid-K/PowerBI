using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025DA RID: 9690
	[ChildElementInfo(typeof(XValues))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(Trendline))]
	[ChildElementInfo(typeof(Smooth))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(InvertIfNegative))]
	[ChildElementInfo(typeof(DataPoint))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ErrorBars))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(YValues))]
	[ChildElementInfo(typeof(BubbleSize))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(BubbleSerExtensionList))]
	internal class BubbleChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x17005853 RID: 22611
		// (get) Token: 0x06012386 RID: 74630 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005854 RID: 22612
		// (get) Token: 0x06012387 RID: 74631 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005855 RID: 22613
		// (get) Token: 0x06012388 RID: 74632 RVA: 0x002F773F File Offset: 0x002F593F
		internal override int ElementTypeId
		{
			get
			{
				return 10532;
			}
		}

		// Token: 0x06012389 RID: 74633 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601238A RID: 74634 RVA: 0x00293ECF File Offset: 0x002920CF
		public BubbleChartSeries()
		{
		}

		// Token: 0x0601238B RID: 74635 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BubbleChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601238C RID: 74636 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BubbleChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601238D RID: 74637 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BubbleChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601238E RID: 74638 RVA: 0x002F7748 File Offset: 0x002F5948
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
			if (11 == namespaceId && "invertIfNegative" == name)
			{
				return new InvertIfNegative();
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
			if (11 == namespaceId && "bubbleSize" == name)
			{
				return new BubbleSize();
			}
			if (11 == namespaceId && "bubble3D" == name)
			{
				return new Bubble3D();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new BubbleSerExtensionList();
			}
			if (11 == namespaceId && "smooth" == name)
			{
				return new Smooth();
			}
			return null;
		}

		// Token: 0x17005856 RID: 22614
		// (get) Token: 0x0601238F RID: 74639 RVA: 0x002F78D6 File Offset: 0x002F5AD6
		internal override string[] ElementTagNames
		{
			get
			{
				return BubbleChartSeries.eleTagNames;
			}
		}

		// Token: 0x17005857 RID: 22615
		// (get) Token: 0x06012390 RID: 74640 RVA: 0x002F78DD File Offset: 0x002F5ADD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BubbleChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x17005858 RID: 22616
		// (get) Token: 0x06012391 RID: 74641 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005859 RID: 22617
		// (get) Token: 0x06012392 RID: 74642 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06012393 RID: 74643 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x1700585A RID: 22618
		// (get) Token: 0x06012394 RID: 74644 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x06012395 RID: 74645 RVA: 0x002F1CD4 File Offset: 0x002EFED4
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

		// Token: 0x1700585B RID: 22619
		// (get) Token: 0x06012396 RID: 74646 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x06012397 RID: 74647 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
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

		// Token: 0x1700585C RID: 22620
		// (get) Token: 0x06012398 RID: 74648 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06012399 RID: 74649 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x1700585D RID: 22621
		// (get) Token: 0x0601239A RID: 74650 RVA: 0x002F2478 File Offset: 0x002F0678
		// (set) Token: 0x0601239B RID: 74651 RVA: 0x002F2481 File Offset: 0x002F0681
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

		// Token: 0x1700585E RID: 22622
		// (get) Token: 0x0601239C RID: 74652 RVA: 0x002F78E4 File Offset: 0x002F5AE4
		// (set) Token: 0x0601239D RID: 74653 RVA: 0x002F78ED File Offset: 0x002F5AED
		public InvertIfNegative InvertIfNegative
		{
			get
			{
				return base.GetElement<InvertIfNegative>(5);
			}
			set
			{
				base.SetElement<InvertIfNegative>(5, value);
			}
		}

		// Token: 0x0601239E RID: 74654 RVA: 0x002F78F7 File Offset: 0x002F5AF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BubbleChartSeries>(deep);
		}

		// Token: 0x04007EB1 RID: 32433
		private const string tagName = "ser";

		// Token: 0x04007EB2 RID: 32434
		private const byte tagNsId = 11;

		// Token: 0x04007EB3 RID: 32435
		internal const int ElementTypeIdConst = 10532;

		// Token: 0x04007EB4 RID: 32436
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "pictureOptions", "invertIfNegative", "dPt", "dLbls", "trendline", "errBars",
			"xVal", "yVal", "bubbleSize", "bubble3D", "extLst", "smooth"
		};

		// Token: 0x04007EB5 RID: 32437
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11
		};
	}
}
