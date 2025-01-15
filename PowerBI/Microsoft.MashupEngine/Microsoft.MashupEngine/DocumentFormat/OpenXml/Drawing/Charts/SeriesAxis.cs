using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F3 RID: 9715
	[ChildElementInfo(typeof(TickMarkSkip))]
	[ChildElementInfo(typeof(CrossesAt))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(Scaling))]
	[ChildElementInfo(typeof(Delete))]
	[ChildElementInfo(typeof(AxisPosition))]
	[ChildElementInfo(typeof(MajorGridlines))]
	[ChildElementInfo(typeof(MinorGridlines))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(MajorTickMark))]
	[ChildElementInfo(typeof(MinorTickMark))]
	[ChildElementInfo(typeof(TickLabelPosition))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(CrossingAxis))]
	[ChildElementInfo(typeof(Crosses))]
	[ChildElementInfo(typeof(Title))]
	[ChildElementInfo(typeof(TickLabelSkip))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class SeriesAxis : OpenXmlCompositeElement
	{
		// Token: 0x17005937 RID: 22839
		// (get) Token: 0x0601257A RID: 75130 RVA: 0x002F9E7E File Offset: 0x002F807E
		public override string LocalName
		{
			get
			{
				return "serAx";
			}
		}

		// Token: 0x17005938 RID: 22840
		// (get) Token: 0x0601257B RID: 75131 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005939 RID: 22841
		// (get) Token: 0x0601257C RID: 75132 RVA: 0x002F9E85 File Offset: 0x002F8085
		internal override int ElementTypeId
		{
			get
			{
				return 10560;
			}
		}

		// Token: 0x0601257D RID: 75133 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601257E RID: 75134 RVA: 0x00293ECF File Offset: 0x002920CF
		public SeriesAxis()
		{
		}

		// Token: 0x0601257F RID: 75135 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SeriesAxis(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012580 RID: 75136 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SeriesAxis(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012581 RID: 75137 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SeriesAxis(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012582 RID: 75138 RVA: 0x002F9E8C File Offset: 0x002F808C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "axId" == name)
			{
				return new AxisId();
			}
			if (11 == namespaceId && "scaling" == name)
			{
				return new Scaling();
			}
			if (11 == namespaceId && "delete" == name)
			{
				return new Delete();
			}
			if (11 == namespaceId && "axPos" == name)
			{
				return new AxisPosition();
			}
			if (11 == namespaceId && "majorGridlines" == name)
			{
				return new MajorGridlines();
			}
			if (11 == namespaceId && "minorGridlines" == name)
			{
				return new MinorGridlines();
			}
			if (11 == namespaceId && "title" == name)
			{
				return new Title();
			}
			if (11 == namespaceId && "numFmt" == name)
			{
				return new NumberingFormat();
			}
			if (11 == namespaceId && "majorTickMark" == name)
			{
				return new MajorTickMark();
			}
			if (11 == namespaceId && "minorTickMark" == name)
			{
				return new MinorTickMark();
			}
			if (11 == namespaceId && "tickLblPos" == name)
			{
				return new TickLabelPosition();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			if (11 == namespaceId && "crossAx" == name)
			{
				return new CrossingAxis();
			}
			if (11 == namespaceId && "crosses" == name)
			{
				return new Crosses();
			}
			if (11 == namespaceId && "crossesAt" == name)
			{
				return new CrossesAt();
			}
			if (11 == namespaceId && "tickLblSkip" == name)
			{
				return new TickLabelSkip();
			}
			if (11 == namespaceId && "tickMarkSkip" == name)
			{
				return new TickMarkSkip();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700593A RID: 22842
		// (get) Token: 0x06012583 RID: 75139 RVA: 0x002FA062 File Offset: 0x002F8262
		internal override string[] ElementTagNames
		{
			get
			{
				return SeriesAxis.eleTagNames;
			}
		}

		// Token: 0x1700593B RID: 22843
		// (get) Token: 0x06012584 RID: 75140 RVA: 0x002FA069 File Offset: 0x002F8269
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SeriesAxis.eleNamespaceIds;
			}
		}

		// Token: 0x1700593C RID: 22844
		// (get) Token: 0x06012585 RID: 75141 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700593D RID: 22845
		// (get) Token: 0x06012586 RID: 75142 RVA: 0x002F9588 File Offset: 0x002F7788
		// (set) Token: 0x06012587 RID: 75143 RVA: 0x002F9591 File Offset: 0x002F7791
		public AxisId AxisId
		{
			get
			{
				return base.GetElement<AxisId>(0);
			}
			set
			{
				base.SetElement<AxisId>(0, value);
			}
		}

		// Token: 0x1700593E RID: 22846
		// (get) Token: 0x06012588 RID: 75144 RVA: 0x002F959B File Offset: 0x002F779B
		// (set) Token: 0x06012589 RID: 75145 RVA: 0x002F95A4 File Offset: 0x002F77A4
		public Scaling Scaling
		{
			get
			{
				return base.GetElement<Scaling>(1);
			}
			set
			{
				base.SetElement<Scaling>(1, value);
			}
		}

		// Token: 0x1700593F RID: 22847
		// (get) Token: 0x0601258A RID: 75146 RVA: 0x002F95AE File Offset: 0x002F77AE
		// (set) Token: 0x0601258B RID: 75147 RVA: 0x002F95B7 File Offset: 0x002F77B7
		public Delete Delete
		{
			get
			{
				return base.GetElement<Delete>(2);
			}
			set
			{
				base.SetElement<Delete>(2, value);
			}
		}

		// Token: 0x17005940 RID: 22848
		// (get) Token: 0x0601258C RID: 75148 RVA: 0x002F95C1 File Offset: 0x002F77C1
		// (set) Token: 0x0601258D RID: 75149 RVA: 0x002F95CA File Offset: 0x002F77CA
		public AxisPosition AxisPosition
		{
			get
			{
				return base.GetElement<AxisPosition>(3);
			}
			set
			{
				base.SetElement<AxisPosition>(3, value);
			}
		}

		// Token: 0x17005941 RID: 22849
		// (get) Token: 0x0601258E RID: 75150 RVA: 0x002F95D4 File Offset: 0x002F77D4
		// (set) Token: 0x0601258F RID: 75151 RVA: 0x002F95DD File Offset: 0x002F77DD
		public MajorGridlines MajorGridlines
		{
			get
			{
				return base.GetElement<MajorGridlines>(4);
			}
			set
			{
				base.SetElement<MajorGridlines>(4, value);
			}
		}

		// Token: 0x17005942 RID: 22850
		// (get) Token: 0x06012590 RID: 75152 RVA: 0x002F95E7 File Offset: 0x002F77E7
		// (set) Token: 0x06012591 RID: 75153 RVA: 0x002F95F0 File Offset: 0x002F77F0
		public MinorGridlines MinorGridlines
		{
			get
			{
				return base.GetElement<MinorGridlines>(5);
			}
			set
			{
				base.SetElement<MinorGridlines>(5, value);
			}
		}

		// Token: 0x17005943 RID: 22851
		// (get) Token: 0x06012592 RID: 75154 RVA: 0x002F95FA File Offset: 0x002F77FA
		// (set) Token: 0x06012593 RID: 75155 RVA: 0x002F9603 File Offset: 0x002F7803
		public Title Title
		{
			get
			{
				return base.GetElement<Title>(6);
			}
			set
			{
				base.SetElement<Title>(6, value);
			}
		}

		// Token: 0x17005944 RID: 22852
		// (get) Token: 0x06012594 RID: 75156 RVA: 0x002F960D File Offset: 0x002F780D
		// (set) Token: 0x06012595 RID: 75157 RVA: 0x002F9616 File Offset: 0x002F7816
		public NumberingFormat NumberingFormat
		{
			get
			{
				return base.GetElement<NumberingFormat>(7);
			}
			set
			{
				base.SetElement<NumberingFormat>(7, value);
			}
		}

		// Token: 0x17005945 RID: 22853
		// (get) Token: 0x06012596 RID: 75158 RVA: 0x002F9620 File Offset: 0x002F7820
		// (set) Token: 0x06012597 RID: 75159 RVA: 0x002F9629 File Offset: 0x002F7829
		public MajorTickMark MajorTickMark
		{
			get
			{
				return base.GetElement<MajorTickMark>(8);
			}
			set
			{
				base.SetElement<MajorTickMark>(8, value);
			}
		}

		// Token: 0x17005946 RID: 22854
		// (get) Token: 0x06012598 RID: 75160 RVA: 0x002F9633 File Offset: 0x002F7833
		// (set) Token: 0x06012599 RID: 75161 RVA: 0x002F963D File Offset: 0x002F783D
		public MinorTickMark MinorTickMark
		{
			get
			{
				return base.GetElement<MinorTickMark>(9);
			}
			set
			{
				base.SetElement<MinorTickMark>(9, value);
			}
		}

		// Token: 0x17005947 RID: 22855
		// (get) Token: 0x0601259A RID: 75162 RVA: 0x002F9648 File Offset: 0x002F7848
		// (set) Token: 0x0601259B RID: 75163 RVA: 0x002F9652 File Offset: 0x002F7852
		public TickLabelPosition TickLabelPosition
		{
			get
			{
				return base.GetElement<TickLabelPosition>(10);
			}
			set
			{
				base.SetElement<TickLabelPosition>(10, value);
			}
		}

		// Token: 0x17005948 RID: 22856
		// (get) Token: 0x0601259C RID: 75164 RVA: 0x002F965D File Offset: 0x002F785D
		// (set) Token: 0x0601259D RID: 75165 RVA: 0x002F9667 File Offset: 0x002F7867
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(11);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(11, value);
			}
		}

		// Token: 0x17005949 RID: 22857
		// (get) Token: 0x0601259E RID: 75166 RVA: 0x002F9672 File Offset: 0x002F7872
		// (set) Token: 0x0601259F RID: 75167 RVA: 0x002F967C File Offset: 0x002F787C
		public TextProperties TextProperties
		{
			get
			{
				return base.GetElement<TextProperties>(12);
			}
			set
			{
				base.SetElement<TextProperties>(12, value);
			}
		}

		// Token: 0x1700594A RID: 22858
		// (get) Token: 0x060125A0 RID: 75168 RVA: 0x002F9687 File Offset: 0x002F7887
		// (set) Token: 0x060125A1 RID: 75169 RVA: 0x002F9691 File Offset: 0x002F7891
		public CrossingAxis CrossingAxis
		{
			get
			{
				return base.GetElement<CrossingAxis>(13);
			}
			set
			{
				base.SetElement<CrossingAxis>(13, value);
			}
		}

		// Token: 0x060125A2 RID: 75170 RVA: 0x002FA070 File Offset: 0x002F8270
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeriesAxis>(deep);
		}

		// Token: 0x04007F2C RID: 32556
		private const string tagName = "serAx";

		// Token: 0x04007F2D RID: 32557
		private const byte tagNsId = 11;

		// Token: 0x04007F2E RID: 32558
		internal const int ElementTypeIdConst = 10560;

		// Token: 0x04007F2F RID: 32559
		private static readonly string[] eleTagNames = new string[]
		{
			"axId", "scaling", "delete", "axPos", "majorGridlines", "minorGridlines", "title", "numFmt", "majorTickMark", "minorTickMark",
			"tickLblPos", "spPr", "txPr", "crossAx", "crosses", "crossesAt", "tickLblSkip", "tickMarkSkip", "extLst"
		};

		// Token: 0x04007F30 RID: 32560
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11
		};
	}
}
