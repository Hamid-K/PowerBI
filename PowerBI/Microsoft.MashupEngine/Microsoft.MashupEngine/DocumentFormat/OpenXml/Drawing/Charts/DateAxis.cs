using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F2 RID: 9714
	[ChildElementInfo(typeof(AxisId))]
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(AutoLabeled))]
	[ChildElementInfo(typeof(LabelOffset))]
	[ChildElementInfo(typeof(BaseTimeUnit))]
	[ChildElementInfo(typeof(MajorUnit))]
	[ChildElementInfo(typeof(MajorTimeUnit))]
	[ChildElementInfo(typeof(MinorUnit))]
	[ChildElementInfo(typeof(MinorTimeUnit))]
	[ChildElementInfo(typeof(NoMultiLevelLabels))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class DateAxis : OpenXmlCompositeElement
	{
		// Token: 0x17005923 RID: 22819
		// (get) Token: 0x06012550 RID: 75088 RVA: 0x002F9AE4 File Offset: 0x002F7CE4
		public override string LocalName
		{
			get
			{
				return "dateAx";
			}
		}

		// Token: 0x17005924 RID: 22820
		// (get) Token: 0x06012551 RID: 75089 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005925 RID: 22821
		// (get) Token: 0x06012552 RID: 75090 RVA: 0x002F9AEB File Offset: 0x002F7CEB
		internal override int ElementTypeId
		{
			get
			{
				return 10559;
			}
		}

		// Token: 0x06012553 RID: 75091 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012554 RID: 75092 RVA: 0x00293ECF File Offset: 0x002920CF
		public DateAxis()
		{
		}

		// Token: 0x06012555 RID: 75093 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DateAxis(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012556 RID: 75094 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DateAxis(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012557 RID: 75095 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DateAxis(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012558 RID: 75096 RVA: 0x002F9AF4 File Offset: 0x002F7CF4
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
			if (11 == namespaceId && "lblOffset" == name)
			{
				return new LabelOffset();
			}
			if (11 == namespaceId && "baseTimeUnit" == name)
			{
				return new BaseTimeUnit();
			}
			if (11 == namespaceId && "majorUnit" == name)
			{
				return new MajorUnit();
			}
			if (11 == namespaceId && "majorTimeUnit" == name)
			{
				return new MajorTimeUnit();
			}
			if (11 == namespaceId && "minorUnit" == name)
			{
				return new MinorUnit();
			}
			if (11 == namespaceId && "minorTimeUnit" == name)
			{
				return new MinorTimeUnit();
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

		// Token: 0x17005926 RID: 22822
		// (get) Token: 0x06012559 RID: 75097 RVA: 0x002F9D5A File Offset: 0x002F7F5A
		internal override string[] ElementTagNames
		{
			get
			{
				return DateAxis.eleTagNames;
			}
		}

		// Token: 0x17005927 RID: 22823
		// (get) Token: 0x0601255A RID: 75098 RVA: 0x002F9D61 File Offset: 0x002F7F61
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DateAxis.eleNamespaceIds;
			}
		}

		// Token: 0x17005928 RID: 22824
		// (get) Token: 0x0601255B RID: 75099 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005929 RID: 22825
		// (get) Token: 0x0601255C RID: 75100 RVA: 0x002F9588 File Offset: 0x002F7788
		// (set) Token: 0x0601255D RID: 75101 RVA: 0x002F9591 File Offset: 0x002F7791
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

		// Token: 0x1700592A RID: 22826
		// (get) Token: 0x0601255E RID: 75102 RVA: 0x002F959B File Offset: 0x002F779B
		// (set) Token: 0x0601255F RID: 75103 RVA: 0x002F95A4 File Offset: 0x002F77A4
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

		// Token: 0x1700592B RID: 22827
		// (get) Token: 0x06012560 RID: 75104 RVA: 0x002F95AE File Offset: 0x002F77AE
		// (set) Token: 0x06012561 RID: 75105 RVA: 0x002F95B7 File Offset: 0x002F77B7
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

		// Token: 0x1700592C RID: 22828
		// (get) Token: 0x06012562 RID: 75106 RVA: 0x002F95C1 File Offset: 0x002F77C1
		// (set) Token: 0x06012563 RID: 75107 RVA: 0x002F95CA File Offset: 0x002F77CA
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

		// Token: 0x1700592D RID: 22829
		// (get) Token: 0x06012564 RID: 75108 RVA: 0x002F95D4 File Offset: 0x002F77D4
		// (set) Token: 0x06012565 RID: 75109 RVA: 0x002F95DD File Offset: 0x002F77DD
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

		// Token: 0x1700592E RID: 22830
		// (get) Token: 0x06012566 RID: 75110 RVA: 0x002F95E7 File Offset: 0x002F77E7
		// (set) Token: 0x06012567 RID: 75111 RVA: 0x002F95F0 File Offset: 0x002F77F0
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

		// Token: 0x1700592F RID: 22831
		// (get) Token: 0x06012568 RID: 75112 RVA: 0x002F95FA File Offset: 0x002F77FA
		// (set) Token: 0x06012569 RID: 75113 RVA: 0x002F9603 File Offset: 0x002F7803
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

		// Token: 0x17005930 RID: 22832
		// (get) Token: 0x0601256A RID: 75114 RVA: 0x002F960D File Offset: 0x002F780D
		// (set) Token: 0x0601256B RID: 75115 RVA: 0x002F9616 File Offset: 0x002F7816
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

		// Token: 0x17005931 RID: 22833
		// (get) Token: 0x0601256C RID: 75116 RVA: 0x002F9620 File Offset: 0x002F7820
		// (set) Token: 0x0601256D RID: 75117 RVA: 0x002F9629 File Offset: 0x002F7829
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

		// Token: 0x17005932 RID: 22834
		// (get) Token: 0x0601256E RID: 75118 RVA: 0x002F9633 File Offset: 0x002F7833
		// (set) Token: 0x0601256F RID: 75119 RVA: 0x002F963D File Offset: 0x002F783D
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

		// Token: 0x17005933 RID: 22835
		// (get) Token: 0x06012570 RID: 75120 RVA: 0x002F9648 File Offset: 0x002F7848
		// (set) Token: 0x06012571 RID: 75121 RVA: 0x002F9652 File Offset: 0x002F7852
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

		// Token: 0x17005934 RID: 22836
		// (get) Token: 0x06012572 RID: 75122 RVA: 0x002F965D File Offset: 0x002F785D
		// (set) Token: 0x06012573 RID: 75123 RVA: 0x002F9667 File Offset: 0x002F7867
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

		// Token: 0x17005935 RID: 22837
		// (get) Token: 0x06012574 RID: 75124 RVA: 0x002F9672 File Offset: 0x002F7872
		// (set) Token: 0x06012575 RID: 75125 RVA: 0x002F967C File Offset: 0x002F787C
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

		// Token: 0x17005936 RID: 22838
		// (get) Token: 0x06012576 RID: 75126 RVA: 0x002F9687 File Offset: 0x002F7887
		// (set) Token: 0x06012577 RID: 75127 RVA: 0x002F9691 File Offset: 0x002F7891
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

		// Token: 0x06012578 RID: 75128 RVA: 0x002F9D68 File Offset: 0x002F7F68
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DateAxis>(deep);
		}

		// Token: 0x04007F27 RID: 32551
		private const string tagName = "dateAx";

		// Token: 0x04007F28 RID: 32552
		private const byte tagNsId = 11;

		// Token: 0x04007F29 RID: 32553
		internal const int ElementTypeIdConst = 10559;

		// Token: 0x04007F2A RID: 32554
		private static readonly string[] eleTagNames = new string[]
		{
			"axId", "scaling", "delete", "axPos", "majorGridlines", "minorGridlines", "title", "numFmt", "majorTickMark", "minorTickMark",
			"tickLblPos", "spPr", "txPr", "crossAx", "crosses", "crossesAt", "auto", "lblOffset", "baseTimeUnit", "majorUnit",
			"majorTimeUnit", "minorUnit", "minorTimeUnit", "noMultiLvlLbl", "extLst"
		};

		// Token: 0x04007F2B RID: 32555
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11
		};
	}
}
