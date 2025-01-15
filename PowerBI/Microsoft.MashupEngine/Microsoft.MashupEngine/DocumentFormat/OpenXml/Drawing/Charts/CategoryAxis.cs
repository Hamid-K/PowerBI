using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F1 RID: 9713
	[ChildElementInfo(typeof(CrossingAxis))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(Scaling))]
	[ChildElementInfo(typeof(Delete))]
	[ChildElementInfo(typeof(AxisPosition))]
	[ChildElementInfo(typeof(MajorGridlines))]
	[ChildElementInfo(typeof(MinorGridlines))]
	[ChildElementInfo(typeof(Title))]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(MajorTickMark))]
	[ChildElementInfo(typeof(MinorTickMark))]
	[ChildElementInfo(typeof(TickLabelPosition))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(Crosses))]
	[ChildElementInfo(typeof(CrossesAt))]
	[ChildElementInfo(typeof(AutoLabeled))]
	[ChildElementInfo(typeof(LabelAlignment))]
	[ChildElementInfo(typeof(LabelOffset))]
	[ChildElementInfo(typeof(TickLabelSkip))]
	[ChildElementInfo(typeof(TickMarkSkip))]
	[ChildElementInfo(typeof(NoMultiLevelLabels))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CategoryAxis : OpenXmlCompositeElement
	{
		// Token: 0x1700590F RID: 22799
		// (get) Token: 0x06012526 RID: 75046 RVA: 0x002F978E File Offset: 0x002F798E
		public override string LocalName
		{
			get
			{
				return "catAx";
			}
		}

		// Token: 0x17005910 RID: 22800
		// (get) Token: 0x06012527 RID: 75047 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005911 RID: 22801
		// (get) Token: 0x06012528 RID: 75048 RVA: 0x002F9795 File Offset: 0x002F7995
		internal override int ElementTypeId
		{
			get
			{
				return 10558;
			}
		}

		// Token: 0x06012529 RID: 75049 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601252A RID: 75050 RVA: 0x00293ECF File Offset: 0x002920CF
		public CategoryAxis()
		{
		}

		// Token: 0x0601252B RID: 75051 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CategoryAxis(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601252C RID: 75052 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CategoryAxis(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601252D RID: 75053 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CategoryAxis(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601252E RID: 75054 RVA: 0x002F979C File Offset: 0x002F799C
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
			if (11 == namespaceId && "auto" == name)
			{
				return new AutoLabeled();
			}
			if (11 == namespaceId && "lblAlgn" == name)
			{
				return new LabelAlignment();
			}
			if (11 == namespaceId && "lblOffset" == name)
			{
				return new LabelOffset();
			}
			if (11 == namespaceId && "tickLblSkip" == name)
			{
				return new TickLabelSkip();
			}
			if (11 == namespaceId && "tickMarkSkip" == name)
			{
				return new TickMarkSkip();
			}
			if (11 == namespaceId && "noMultiLvlLbl" == name)
			{
				return new NoMultiLevelLabels();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005912 RID: 22802
		// (get) Token: 0x0601252F RID: 75055 RVA: 0x002F99D2 File Offset: 0x002F7BD2
		internal override string[] ElementTagNames
		{
			get
			{
				return CategoryAxis.eleTagNames;
			}
		}

		// Token: 0x17005913 RID: 22803
		// (get) Token: 0x06012530 RID: 75056 RVA: 0x002F99D9 File Offset: 0x002F7BD9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CategoryAxis.eleNamespaceIds;
			}
		}

		// Token: 0x17005914 RID: 22804
		// (get) Token: 0x06012531 RID: 75057 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005915 RID: 22805
		// (get) Token: 0x06012532 RID: 75058 RVA: 0x002F9588 File Offset: 0x002F7788
		// (set) Token: 0x06012533 RID: 75059 RVA: 0x002F9591 File Offset: 0x002F7791
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

		// Token: 0x17005916 RID: 22806
		// (get) Token: 0x06012534 RID: 75060 RVA: 0x002F959B File Offset: 0x002F779B
		// (set) Token: 0x06012535 RID: 75061 RVA: 0x002F95A4 File Offset: 0x002F77A4
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

		// Token: 0x17005917 RID: 22807
		// (get) Token: 0x06012536 RID: 75062 RVA: 0x002F95AE File Offset: 0x002F77AE
		// (set) Token: 0x06012537 RID: 75063 RVA: 0x002F95B7 File Offset: 0x002F77B7
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

		// Token: 0x17005918 RID: 22808
		// (get) Token: 0x06012538 RID: 75064 RVA: 0x002F95C1 File Offset: 0x002F77C1
		// (set) Token: 0x06012539 RID: 75065 RVA: 0x002F95CA File Offset: 0x002F77CA
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

		// Token: 0x17005919 RID: 22809
		// (get) Token: 0x0601253A RID: 75066 RVA: 0x002F95D4 File Offset: 0x002F77D4
		// (set) Token: 0x0601253B RID: 75067 RVA: 0x002F95DD File Offset: 0x002F77DD
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

		// Token: 0x1700591A RID: 22810
		// (get) Token: 0x0601253C RID: 75068 RVA: 0x002F95E7 File Offset: 0x002F77E7
		// (set) Token: 0x0601253D RID: 75069 RVA: 0x002F95F0 File Offset: 0x002F77F0
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

		// Token: 0x1700591B RID: 22811
		// (get) Token: 0x0601253E RID: 75070 RVA: 0x002F95FA File Offset: 0x002F77FA
		// (set) Token: 0x0601253F RID: 75071 RVA: 0x002F9603 File Offset: 0x002F7803
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

		// Token: 0x1700591C RID: 22812
		// (get) Token: 0x06012540 RID: 75072 RVA: 0x002F960D File Offset: 0x002F780D
		// (set) Token: 0x06012541 RID: 75073 RVA: 0x002F9616 File Offset: 0x002F7816
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

		// Token: 0x1700591D RID: 22813
		// (get) Token: 0x06012542 RID: 75074 RVA: 0x002F9620 File Offset: 0x002F7820
		// (set) Token: 0x06012543 RID: 75075 RVA: 0x002F9629 File Offset: 0x002F7829
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

		// Token: 0x1700591E RID: 22814
		// (get) Token: 0x06012544 RID: 75076 RVA: 0x002F9633 File Offset: 0x002F7833
		// (set) Token: 0x06012545 RID: 75077 RVA: 0x002F963D File Offset: 0x002F783D
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

		// Token: 0x1700591F RID: 22815
		// (get) Token: 0x06012546 RID: 75078 RVA: 0x002F9648 File Offset: 0x002F7848
		// (set) Token: 0x06012547 RID: 75079 RVA: 0x002F9652 File Offset: 0x002F7852
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

		// Token: 0x17005920 RID: 22816
		// (get) Token: 0x06012548 RID: 75080 RVA: 0x002F965D File Offset: 0x002F785D
		// (set) Token: 0x06012549 RID: 75081 RVA: 0x002F9667 File Offset: 0x002F7867
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

		// Token: 0x17005921 RID: 22817
		// (get) Token: 0x0601254A RID: 75082 RVA: 0x002F9672 File Offset: 0x002F7872
		// (set) Token: 0x0601254B RID: 75083 RVA: 0x002F967C File Offset: 0x002F787C
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

		// Token: 0x17005922 RID: 22818
		// (get) Token: 0x0601254C RID: 75084 RVA: 0x002F9687 File Offset: 0x002F7887
		// (set) Token: 0x0601254D RID: 75085 RVA: 0x002F9691 File Offset: 0x002F7891
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

		// Token: 0x0601254E RID: 75086 RVA: 0x002F99E0 File Offset: 0x002F7BE0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CategoryAxis>(deep);
		}

		// Token: 0x04007F22 RID: 32546
		private const string tagName = "catAx";

		// Token: 0x04007F23 RID: 32547
		private const byte tagNsId = 11;

		// Token: 0x04007F24 RID: 32548
		internal const int ElementTypeIdConst = 10558;

		// Token: 0x04007F25 RID: 32549
		private static readonly string[] eleTagNames = new string[]
		{
			"axId", "scaling", "delete", "axPos", "majorGridlines", "minorGridlines", "title", "numFmt", "majorTickMark", "minorTickMark",
			"tickLblPos", "spPr", "txPr", "crossAx", "crosses", "crossesAt", "auto", "lblAlgn", "lblOffset", "tickLblSkip",
			"tickMarkSkip", "noMultiLvlLbl", "extLst"
		};

		// Token: 0x04007F26 RID: 32550
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11
		};
	}
}
