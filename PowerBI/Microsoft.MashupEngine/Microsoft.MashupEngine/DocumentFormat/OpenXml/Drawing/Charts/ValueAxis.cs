using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F0 RID: 9712
	[ChildElementInfo(typeof(MajorUnit))]
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
	[ChildElementInfo(typeof(CrossingAxis))]
	[ChildElementInfo(typeof(Crosses))]
	[ChildElementInfo(typeof(CrossesAt))]
	[ChildElementInfo(typeof(CrossBetween))]
	[ChildElementInfo(typeof(MinorUnit))]
	[ChildElementInfo(typeof(DisplayUnits))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ValueAxis : OpenXmlCompositeElement
	{
		// Token: 0x170058FB RID: 22779
		// (get) Token: 0x060124FC RID: 75004 RVA: 0x002F9364 File Offset: 0x002F7564
		public override string LocalName
		{
			get
			{
				return "valAx";
			}
		}

		// Token: 0x170058FC RID: 22780
		// (get) Token: 0x060124FD RID: 75005 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058FD RID: 22781
		// (get) Token: 0x060124FE RID: 75006 RVA: 0x002F936B File Offset: 0x002F756B
		internal override int ElementTypeId
		{
			get
			{
				return 10557;
			}
		}

		// Token: 0x060124FF RID: 75007 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012500 RID: 75008 RVA: 0x00293ECF File Offset: 0x002920CF
		public ValueAxis()
		{
		}

		// Token: 0x06012501 RID: 75009 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ValueAxis(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012502 RID: 75010 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ValueAxis(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012503 RID: 75011 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ValueAxis(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012504 RID: 75012 RVA: 0x002F9374 File Offset: 0x002F7574
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
			if (11 == namespaceId && "crossBetween" == name)
			{
				return new CrossBetween();
			}
			if (11 == namespaceId && "majorUnit" == name)
			{
				return new MajorUnit();
			}
			if (11 == namespaceId && "minorUnit" == name)
			{
				return new MinorUnit();
			}
			if (11 == namespaceId && "dispUnits" == name)
			{
				return new DisplayUnits();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170058FE RID: 22782
		// (get) Token: 0x06012505 RID: 75013 RVA: 0x002F957A File Offset: 0x002F777A
		internal override string[] ElementTagNames
		{
			get
			{
				return ValueAxis.eleTagNames;
			}
		}

		// Token: 0x170058FF RID: 22783
		// (get) Token: 0x06012506 RID: 75014 RVA: 0x002F9581 File Offset: 0x002F7781
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ValueAxis.eleNamespaceIds;
			}
		}

		// Token: 0x17005900 RID: 22784
		// (get) Token: 0x06012507 RID: 75015 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005901 RID: 22785
		// (get) Token: 0x06012508 RID: 75016 RVA: 0x002F9588 File Offset: 0x002F7788
		// (set) Token: 0x06012509 RID: 75017 RVA: 0x002F9591 File Offset: 0x002F7791
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

		// Token: 0x17005902 RID: 22786
		// (get) Token: 0x0601250A RID: 75018 RVA: 0x002F959B File Offset: 0x002F779B
		// (set) Token: 0x0601250B RID: 75019 RVA: 0x002F95A4 File Offset: 0x002F77A4
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

		// Token: 0x17005903 RID: 22787
		// (get) Token: 0x0601250C RID: 75020 RVA: 0x002F95AE File Offset: 0x002F77AE
		// (set) Token: 0x0601250D RID: 75021 RVA: 0x002F95B7 File Offset: 0x002F77B7
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

		// Token: 0x17005904 RID: 22788
		// (get) Token: 0x0601250E RID: 75022 RVA: 0x002F95C1 File Offset: 0x002F77C1
		// (set) Token: 0x0601250F RID: 75023 RVA: 0x002F95CA File Offset: 0x002F77CA
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

		// Token: 0x17005905 RID: 22789
		// (get) Token: 0x06012510 RID: 75024 RVA: 0x002F95D4 File Offset: 0x002F77D4
		// (set) Token: 0x06012511 RID: 75025 RVA: 0x002F95DD File Offset: 0x002F77DD
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

		// Token: 0x17005906 RID: 22790
		// (get) Token: 0x06012512 RID: 75026 RVA: 0x002F95E7 File Offset: 0x002F77E7
		// (set) Token: 0x06012513 RID: 75027 RVA: 0x002F95F0 File Offset: 0x002F77F0
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

		// Token: 0x17005907 RID: 22791
		// (get) Token: 0x06012514 RID: 75028 RVA: 0x002F95FA File Offset: 0x002F77FA
		// (set) Token: 0x06012515 RID: 75029 RVA: 0x002F9603 File Offset: 0x002F7803
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

		// Token: 0x17005908 RID: 22792
		// (get) Token: 0x06012516 RID: 75030 RVA: 0x002F960D File Offset: 0x002F780D
		// (set) Token: 0x06012517 RID: 75031 RVA: 0x002F9616 File Offset: 0x002F7816
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

		// Token: 0x17005909 RID: 22793
		// (get) Token: 0x06012518 RID: 75032 RVA: 0x002F9620 File Offset: 0x002F7820
		// (set) Token: 0x06012519 RID: 75033 RVA: 0x002F9629 File Offset: 0x002F7829
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

		// Token: 0x1700590A RID: 22794
		// (get) Token: 0x0601251A RID: 75034 RVA: 0x002F9633 File Offset: 0x002F7833
		// (set) Token: 0x0601251B RID: 75035 RVA: 0x002F963D File Offset: 0x002F783D
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

		// Token: 0x1700590B RID: 22795
		// (get) Token: 0x0601251C RID: 75036 RVA: 0x002F9648 File Offset: 0x002F7848
		// (set) Token: 0x0601251D RID: 75037 RVA: 0x002F9652 File Offset: 0x002F7852
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

		// Token: 0x1700590C RID: 22796
		// (get) Token: 0x0601251E RID: 75038 RVA: 0x002F965D File Offset: 0x002F785D
		// (set) Token: 0x0601251F RID: 75039 RVA: 0x002F9667 File Offset: 0x002F7867
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

		// Token: 0x1700590D RID: 22797
		// (get) Token: 0x06012520 RID: 75040 RVA: 0x002F9672 File Offset: 0x002F7872
		// (set) Token: 0x06012521 RID: 75041 RVA: 0x002F967C File Offset: 0x002F787C
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

		// Token: 0x1700590E RID: 22798
		// (get) Token: 0x06012522 RID: 75042 RVA: 0x002F9687 File Offset: 0x002F7887
		// (set) Token: 0x06012523 RID: 75043 RVA: 0x002F9691 File Offset: 0x002F7891
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

		// Token: 0x06012524 RID: 75044 RVA: 0x002F969C File Offset: 0x002F789C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ValueAxis>(deep);
		}

		// Token: 0x04007F1D RID: 32541
		private const string tagName = "valAx";

		// Token: 0x04007F1E RID: 32542
		private const byte tagNsId = 11;

		// Token: 0x04007F1F RID: 32543
		internal const int ElementTypeIdConst = 10557;

		// Token: 0x04007F20 RID: 32544
		private static readonly string[] eleTagNames = new string[]
		{
			"axId", "scaling", "delete", "axPos", "majorGridlines", "minorGridlines", "title", "numFmt", "majorTickMark", "minorTickMark",
			"tickLblPos", "spPr", "txPr", "crossAx", "crosses", "crossesAt", "crossBetween", "majorUnit", "minorUnit", "dispUnits",
			"extLst"
		};

		// Token: 0x04007F21 RID: 32545
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11
		};
	}
}
