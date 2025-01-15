using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002967 RID: 10599
	[ChildElementInfo(typeof(WrapIndent))]
	[ChildElementInfo(typeof(RightMargin))]
	[ChildElementInfo(typeof(DefaultJustification))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MathFont))]
	[ChildElementInfo(typeof(BreakBinary))]
	[ChildElementInfo(typeof(BreakBinarySubtraction))]
	[ChildElementInfo(typeof(SmallFraction))]
	[ChildElementInfo(typeof(DisplayDefaults))]
	[ChildElementInfo(typeof(LeftMargin))]
	[ChildElementInfo(typeof(PreSpacing))]
	[ChildElementInfo(typeof(PostSpacing))]
	[ChildElementInfo(typeof(InterSpacing))]
	[ChildElementInfo(typeof(IntraSpacing))]
	[ChildElementInfo(typeof(WrapRight))]
	[ChildElementInfo(typeof(IntegralLimitLocation))]
	[ChildElementInfo(typeof(NaryLimitLocation))]
	internal class MathProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006C31 RID: 27697
		// (get) Token: 0x060150ED RID: 86253 RVA: 0x0031AFFF File Offset: 0x003191FF
		public override string LocalName
		{
			get
			{
				return "mathPr";
			}
		}

		// Token: 0x17006C32 RID: 27698
		// (get) Token: 0x060150EE RID: 86254 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C33 RID: 27699
		// (get) Token: 0x060150EF RID: 86255 RVA: 0x0031B006 File Offset: 0x00319206
		internal override int ElementTypeId
		{
			get
			{
				return 10863;
			}
		}

		// Token: 0x060150F0 RID: 86256 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060150F1 RID: 86257 RVA: 0x00293ECF File Offset: 0x002920CF
		public MathProperties()
		{
		}

		// Token: 0x060150F2 RID: 86258 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MathProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150F3 RID: 86259 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MathProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150F4 RID: 86260 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MathProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060150F5 RID: 86261 RVA: 0x0031B010 File Offset: 0x00319210
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "mathFont" == name)
			{
				return new MathFont();
			}
			if (21 == namespaceId && "brkBin" == name)
			{
				return new BreakBinary();
			}
			if (21 == namespaceId && "brkBinSub" == name)
			{
				return new BreakBinarySubtraction();
			}
			if (21 == namespaceId && "smallFrac" == name)
			{
				return new SmallFraction();
			}
			if (21 == namespaceId && "dispDef" == name)
			{
				return new DisplayDefaults();
			}
			if (21 == namespaceId && "lMargin" == name)
			{
				return new LeftMargin();
			}
			if (21 == namespaceId && "rMargin" == name)
			{
				return new RightMargin();
			}
			if (21 == namespaceId && "defJc" == name)
			{
				return new DefaultJustification();
			}
			if (21 == namespaceId && "preSp" == name)
			{
				return new PreSpacing();
			}
			if (21 == namespaceId && "postSp" == name)
			{
				return new PostSpacing();
			}
			if (21 == namespaceId && "interSp" == name)
			{
				return new InterSpacing();
			}
			if (21 == namespaceId && "intraSp" == name)
			{
				return new IntraSpacing();
			}
			if (21 == namespaceId && "wrapIndent" == name)
			{
				return new WrapIndent();
			}
			if (21 == namespaceId && "wrapRight" == name)
			{
				return new WrapRight();
			}
			if (21 == namespaceId && "intLim" == name)
			{
				return new IntegralLimitLocation();
			}
			if (21 == namespaceId && "naryLim" == name)
			{
				return new NaryLimitLocation();
			}
			return null;
		}

		// Token: 0x17006C34 RID: 27700
		// (get) Token: 0x060150F6 RID: 86262 RVA: 0x0031B19E File Offset: 0x0031939E
		internal override string[] ElementTagNames
		{
			get
			{
				return MathProperties.eleTagNames;
			}
		}

		// Token: 0x17006C35 RID: 27701
		// (get) Token: 0x060150F7 RID: 86263 RVA: 0x0031B1A5 File Offset: 0x003193A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MathProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006C36 RID: 27702
		// (get) Token: 0x060150F8 RID: 86264 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006C37 RID: 27703
		// (get) Token: 0x060150F9 RID: 86265 RVA: 0x0031B1AC File Offset: 0x003193AC
		// (set) Token: 0x060150FA RID: 86266 RVA: 0x0031B1B5 File Offset: 0x003193B5
		public MathFont MathFont
		{
			get
			{
				return base.GetElement<MathFont>(0);
			}
			set
			{
				base.SetElement<MathFont>(0, value);
			}
		}

		// Token: 0x17006C38 RID: 27704
		// (get) Token: 0x060150FB RID: 86267 RVA: 0x0031B1BF File Offset: 0x003193BF
		// (set) Token: 0x060150FC RID: 86268 RVA: 0x0031B1C8 File Offset: 0x003193C8
		public BreakBinary BreakBinary
		{
			get
			{
				return base.GetElement<BreakBinary>(1);
			}
			set
			{
				base.SetElement<BreakBinary>(1, value);
			}
		}

		// Token: 0x17006C39 RID: 27705
		// (get) Token: 0x060150FD RID: 86269 RVA: 0x0031B1D2 File Offset: 0x003193D2
		// (set) Token: 0x060150FE RID: 86270 RVA: 0x0031B1DB File Offset: 0x003193DB
		public BreakBinarySubtraction BreakBinarySubtraction
		{
			get
			{
				return base.GetElement<BreakBinarySubtraction>(2);
			}
			set
			{
				base.SetElement<BreakBinarySubtraction>(2, value);
			}
		}

		// Token: 0x17006C3A RID: 27706
		// (get) Token: 0x060150FF RID: 86271 RVA: 0x0031B1E5 File Offset: 0x003193E5
		// (set) Token: 0x06015100 RID: 86272 RVA: 0x0031B1EE File Offset: 0x003193EE
		public SmallFraction SmallFraction
		{
			get
			{
				return base.GetElement<SmallFraction>(3);
			}
			set
			{
				base.SetElement<SmallFraction>(3, value);
			}
		}

		// Token: 0x17006C3B RID: 27707
		// (get) Token: 0x06015101 RID: 86273 RVA: 0x0031B1F8 File Offset: 0x003193F8
		// (set) Token: 0x06015102 RID: 86274 RVA: 0x0031B201 File Offset: 0x00319401
		public DisplayDefaults DisplayDefaults
		{
			get
			{
				return base.GetElement<DisplayDefaults>(4);
			}
			set
			{
				base.SetElement<DisplayDefaults>(4, value);
			}
		}

		// Token: 0x17006C3C RID: 27708
		// (get) Token: 0x06015103 RID: 86275 RVA: 0x0031B20B File Offset: 0x0031940B
		// (set) Token: 0x06015104 RID: 86276 RVA: 0x0031B214 File Offset: 0x00319414
		public LeftMargin LeftMargin
		{
			get
			{
				return base.GetElement<LeftMargin>(5);
			}
			set
			{
				base.SetElement<LeftMargin>(5, value);
			}
		}

		// Token: 0x17006C3D RID: 27709
		// (get) Token: 0x06015105 RID: 86277 RVA: 0x0031B21E File Offset: 0x0031941E
		// (set) Token: 0x06015106 RID: 86278 RVA: 0x0031B227 File Offset: 0x00319427
		public RightMargin RightMargin
		{
			get
			{
				return base.GetElement<RightMargin>(6);
			}
			set
			{
				base.SetElement<RightMargin>(6, value);
			}
		}

		// Token: 0x17006C3E RID: 27710
		// (get) Token: 0x06015107 RID: 86279 RVA: 0x0031B231 File Offset: 0x00319431
		// (set) Token: 0x06015108 RID: 86280 RVA: 0x0031B23A File Offset: 0x0031943A
		public DefaultJustification DefaultJustification
		{
			get
			{
				return base.GetElement<DefaultJustification>(7);
			}
			set
			{
				base.SetElement<DefaultJustification>(7, value);
			}
		}

		// Token: 0x17006C3F RID: 27711
		// (get) Token: 0x06015109 RID: 86281 RVA: 0x0031B244 File Offset: 0x00319444
		// (set) Token: 0x0601510A RID: 86282 RVA: 0x0031B24D File Offset: 0x0031944D
		public PreSpacing PreSpacing
		{
			get
			{
				return base.GetElement<PreSpacing>(8);
			}
			set
			{
				base.SetElement<PreSpacing>(8, value);
			}
		}

		// Token: 0x17006C40 RID: 27712
		// (get) Token: 0x0601510B RID: 86283 RVA: 0x0031B257 File Offset: 0x00319457
		// (set) Token: 0x0601510C RID: 86284 RVA: 0x0031B261 File Offset: 0x00319461
		public PostSpacing PostSpacing
		{
			get
			{
				return base.GetElement<PostSpacing>(9);
			}
			set
			{
				base.SetElement<PostSpacing>(9, value);
			}
		}

		// Token: 0x17006C41 RID: 27713
		// (get) Token: 0x0601510D RID: 86285 RVA: 0x0031B26C File Offset: 0x0031946C
		// (set) Token: 0x0601510E RID: 86286 RVA: 0x0031B276 File Offset: 0x00319476
		public InterSpacing InterSpacing
		{
			get
			{
				return base.GetElement<InterSpacing>(10);
			}
			set
			{
				base.SetElement<InterSpacing>(10, value);
			}
		}

		// Token: 0x17006C42 RID: 27714
		// (get) Token: 0x0601510F RID: 86287 RVA: 0x0031B281 File Offset: 0x00319481
		// (set) Token: 0x06015110 RID: 86288 RVA: 0x0031B28B File Offset: 0x0031948B
		public IntraSpacing IntraSpacing
		{
			get
			{
				return base.GetElement<IntraSpacing>(11);
			}
			set
			{
				base.SetElement<IntraSpacing>(11, value);
			}
		}

		// Token: 0x06015111 RID: 86289 RVA: 0x0031B296 File Offset: 0x00319496
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MathProperties>(deep);
		}

		// Token: 0x04009140 RID: 37184
		private const string tagName = "mathPr";

		// Token: 0x04009141 RID: 37185
		private const byte tagNsId = 21;

		// Token: 0x04009142 RID: 37186
		internal const int ElementTypeIdConst = 10863;

		// Token: 0x04009143 RID: 37187
		private static readonly string[] eleTagNames = new string[]
		{
			"mathFont", "brkBin", "brkBinSub", "smallFrac", "dispDef", "lMargin", "rMargin", "defJc", "preSp", "postSp",
			"interSp", "intraSp", "wrapIndent", "wrapRight", "intLim", "naryLim"
		};

		// Token: 0x04009144 RID: 37188
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21
		};
	}
}
