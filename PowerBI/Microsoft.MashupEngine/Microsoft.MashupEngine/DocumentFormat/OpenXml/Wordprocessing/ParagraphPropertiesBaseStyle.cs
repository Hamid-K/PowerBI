using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F7E RID: 12158
	[ChildElementInfo(typeof(BiDi))]
	[ChildElementInfo(typeof(AdjustRightIndent))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(KeepNext))]
	[ChildElementInfo(typeof(KeepLines))]
	[ChildElementInfo(typeof(PageBreakBefore))]
	[ChildElementInfo(typeof(FrameProperties))]
	[ChildElementInfo(typeof(WidowControl))]
	[ChildElementInfo(typeof(NumberingProperties))]
	[ChildElementInfo(typeof(SuppressLineNumbers))]
	[ChildElementInfo(typeof(ParagraphBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(Tabs))]
	[ChildElementInfo(typeof(SuppressAutoHyphens))]
	[ChildElementInfo(typeof(Kinsoku))]
	[ChildElementInfo(typeof(WordWrap))]
	[ChildElementInfo(typeof(OverflowPunctuation))]
	[ChildElementInfo(typeof(TopLinePunctuation))]
	[ChildElementInfo(typeof(AutoSpaceDE))]
	[ChildElementInfo(typeof(AutoSpaceDN))]
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(SpacingBetweenLines))]
	[ChildElementInfo(typeof(Indentation))]
	[ChildElementInfo(typeof(ContextualSpacing))]
	[ChildElementInfo(typeof(MirrorIndents))]
	[ChildElementInfo(typeof(SuppressOverlap))]
	[ChildElementInfo(typeof(Justification))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TextAlignment))]
	[ChildElementInfo(typeof(TextBoxTightWrap))]
	[ChildElementInfo(typeof(OutlineLevel))]
	internal class ParagraphPropertiesBaseStyle : OpenXmlCompositeElement
	{
		// Token: 0x1700915D RID: 37213
		// (get) Token: 0x0601A2E0 RID: 107232 RVA: 0x0030F000 File Offset: 0x0030D200
		public override string LocalName
		{
			get
			{
				return "pPr";
			}
		}

		// Token: 0x1700915E RID: 37214
		// (get) Token: 0x0601A2E1 RID: 107233 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700915F RID: 37215
		// (get) Token: 0x0601A2E2 RID: 107234 RVA: 0x0035E9B7 File Offset: 0x0035CBB7
		internal override int ElementTypeId
		{
			get
			{
				return 11832;
			}
		}

		// Token: 0x0601A2E3 RID: 107235 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A2E4 RID: 107236 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphPropertiesBaseStyle()
		{
		}

		// Token: 0x0601A2E5 RID: 107237 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphPropertiesBaseStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A2E6 RID: 107238 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphPropertiesBaseStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A2E7 RID: 107239 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphPropertiesBaseStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A2E8 RID: 107240 RVA: 0x0035E9C0 File Offset: 0x0035CBC0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "keepNext" == name)
			{
				return new KeepNext();
			}
			if (23 == namespaceId && "keepLines" == name)
			{
				return new KeepLines();
			}
			if (23 == namespaceId && "pageBreakBefore" == name)
			{
				return new PageBreakBefore();
			}
			if (23 == namespaceId && "framePr" == name)
			{
				return new FrameProperties();
			}
			if (23 == namespaceId && "widowControl" == name)
			{
				return new WidowControl();
			}
			if (23 == namespaceId && "numPr" == name)
			{
				return new NumberingProperties();
			}
			if (23 == namespaceId && "suppressLineNumbers" == name)
			{
				return new SuppressLineNumbers();
			}
			if (23 == namespaceId && "pBdr" == name)
			{
				return new ParagraphBorders();
			}
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "tabs" == name)
			{
				return new Tabs();
			}
			if (23 == namespaceId && "suppressAutoHyphens" == name)
			{
				return new SuppressAutoHyphens();
			}
			if (23 == namespaceId && "kinsoku" == name)
			{
				return new Kinsoku();
			}
			if (23 == namespaceId && "wordWrap" == name)
			{
				return new WordWrap();
			}
			if (23 == namespaceId && "overflowPunct" == name)
			{
				return new OverflowPunctuation();
			}
			if (23 == namespaceId && "topLinePunct" == name)
			{
				return new TopLinePunctuation();
			}
			if (23 == namespaceId && "autoSpaceDE" == name)
			{
				return new AutoSpaceDE();
			}
			if (23 == namespaceId && "autoSpaceDN" == name)
			{
				return new AutoSpaceDN();
			}
			if (23 == namespaceId && "bidi" == name)
			{
				return new BiDi();
			}
			if (23 == namespaceId && "adjustRightInd" == name)
			{
				return new AdjustRightIndent();
			}
			if (23 == namespaceId && "snapToGrid" == name)
			{
				return new SnapToGrid();
			}
			if (23 == namespaceId && "spacing" == name)
			{
				return new SpacingBetweenLines();
			}
			if (23 == namespaceId && "ind" == name)
			{
				return new Indentation();
			}
			if (23 == namespaceId && "contextualSpacing" == name)
			{
				return new ContextualSpacing();
			}
			if (23 == namespaceId && "mirrorIndents" == name)
			{
				return new MirrorIndents();
			}
			if (23 == namespaceId && "suppressOverlap" == name)
			{
				return new SuppressOverlap();
			}
			if (23 == namespaceId && "jc" == name)
			{
				return new Justification();
			}
			if (23 == namespaceId && "textDirection" == name)
			{
				return new TextDirection();
			}
			if (23 == namespaceId && "textAlignment" == name)
			{
				return new TextAlignment();
			}
			if (23 == namespaceId && "textboxTightWrap" == name)
			{
				return new TextBoxTightWrap();
			}
			if (23 == namespaceId && "outlineLvl" == name)
			{
				return new OutlineLevel();
			}
			return null;
		}

		// Token: 0x17009160 RID: 37216
		// (get) Token: 0x0601A2E9 RID: 107241 RVA: 0x0035EC9E File Offset: 0x0035CE9E
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphPropertiesBaseStyle.eleTagNames;
			}
		}

		// Token: 0x17009161 RID: 37217
		// (get) Token: 0x0601A2EA RID: 107242 RVA: 0x0035ECA5 File Offset: 0x0035CEA5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphPropertiesBaseStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17009162 RID: 37218
		// (get) Token: 0x0601A2EB RID: 107243 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009163 RID: 37219
		// (get) Token: 0x0601A2EC RID: 107244 RVA: 0x0035ECAC File Offset: 0x0035CEAC
		// (set) Token: 0x0601A2ED RID: 107245 RVA: 0x0035ECB5 File Offset: 0x0035CEB5
		public KeepNext KeepNext
		{
			get
			{
				return base.GetElement<KeepNext>(0);
			}
			set
			{
				base.SetElement<KeepNext>(0, value);
			}
		}

		// Token: 0x17009164 RID: 37220
		// (get) Token: 0x0601A2EE RID: 107246 RVA: 0x0035ECBF File Offset: 0x0035CEBF
		// (set) Token: 0x0601A2EF RID: 107247 RVA: 0x0035ECC8 File Offset: 0x0035CEC8
		public KeepLines KeepLines
		{
			get
			{
				return base.GetElement<KeepLines>(1);
			}
			set
			{
				base.SetElement<KeepLines>(1, value);
			}
		}

		// Token: 0x17009165 RID: 37221
		// (get) Token: 0x0601A2F0 RID: 107248 RVA: 0x0035ECD2 File Offset: 0x0035CED2
		// (set) Token: 0x0601A2F1 RID: 107249 RVA: 0x0035ECDB File Offset: 0x0035CEDB
		public PageBreakBefore PageBreakBefore
		{
			get
			{
				return base.GetElement<PageBreakBefore>(2);
			}
			set
			{
				base.SetElement<PageBreakBefore>(2, value);
			}
		}

		// Token: 0x17009166 RID: 37222
		// (get) Token: 0x0601A2F2 RID: 107250 RVA: 0x0035ECE5 File Offset: 0x0035CEE5
		// (set) Token: 0x0601A2F3 RID: 107251 RVA: 0x0035ECEE File Offset: 0x0035CEEE
		public FrameProperties FrameProperties
		{
			get
			{
				return base.GetElement<FrameProperties>(3);
			}
			set
			{
				base.SetElement<FrameProperties>(3, value);
			}
		}

		// Token: 0x17009167 RID: 37223
		// (get) Token: 0x0601A2F4 RID: 107252 RVA: 0x0035ECF8 File Offset: 0x0035CEF8
		// (set) Token: 0x0601A2F5 RID: 107253 RVA: 0x0035ED01 File Offset: 0x0035CF01
		public WidowControl WidowControl
		{
			get
			{
				return base.GetElement<WidowControl>(4);
			}
			set
			{
				base.SetElement<WidowControl>(4, value);
			}
		}

		// Token: 0x17009168 RID: 37224
		// (get) Token: 0x0601A2F6 RID: 107254 RVA: 0x0035ED0B File Offset: 0x0035CF0B
		// (set) Token: 0x0601A2F7 RID: 107255 RVA: 0x0035ED14 File Offset: 0x0035CF14
		public NumberingProperties NumberingProperties
		{
			get
			{
				return base.GetElement<NumberingProperties>(5);
			}
			set
			{
				base.SetElement<NumberingProperties>(5, value);
			}
		}

		// Token: 0x17009169 RID: 37225
		// (get) Token: 0x0601A2F8 RID: 107256 RVA: 0x0035ED1E File Offset: 0x0035CF1E
		// (set) Token: 0x0601A2F9 RID: 107257 RVA: 0x0035ED27 File Offset: 0x0035CF27
		public SuppressLineNumbers SuppressLineNumbers
		{
			get
			{
				return base.GetElement<SuppressLineNumbers>(6);
			}
			set
			{
				base.SetElement<SuppressLineNumbers>(6, value);
			}
		}

		// Token: 0x1700916A RID: 37226
		// (get) Token: 0x0601A2FA RID: 107258 RVA: 0x0035ED31 File Offset: 0x0035CF31
		// (set) Token: 0x0601A2FB RID: 107259 RVA: 0x0035ED3A File Offset: 0x0035CF3A
		public ParagraphBorders ParagraphBorders
		{
			get
			{
				return base.GetElement<ParagraphBorders>(7);
			}
			set
			{
				base.SetElement<ParagraphBorders>(7, value);
			}
		}

		// Token: 0x1700916B RID: 37227
		// (get) Token: 0x0601A2FC RID: 107260 RVA: 0x0035ED44 File Offset: 0x0035CF44
		// (set) Token: 0x0601A2FD RID: 107261 RVA: 0x0035ED4D File Offset: 0x0035CF4D
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(8);
			}
			set
			{
				base.SetElement<Shading>(8, value);
			}
		}

		// Token: 0x1700916C RID: 37228
		// (get) Token: 0x0601A2FE RID: 107262 RVA: 0x0035ED57 File Offset: 0x0035CF57
		// (set) Token: 0x0601A2FF RID: 107263 RVA: 0x0035ED61 File Offset: 0x0035CF61
		public Tabs Tabs
		{
			get
			{
				return base.GetElement<Tabs>(9);
			}
			set
			{
				base.SetElement<Tabs>(9, value);
			}
		}

		// Token: 0x1700916D RID: 37229
		// (get) Token: 0x0601A300 RID: 107264 RVA: 0x0035ED6C File Offset: 0x0035CF6C
		// (set) Token: 0x0601A301 RID: 107265 RVA: 0x0035ED76 File Offset: 0x0035CF76
		public SuppressAutoHyphens SuppressAutoHyphens
		{
			get
			{
				return base.GetElement<SuppressAutoHyphens>(10);
			}
			set
			{
				base.SetElement<SuppressAutoHyphens>(10, value);
			}
		}

		// Token: 0x1700916E RID: 37230
		// (get) Token: 0x0601A302 RID: 107266 RVA: 0x0035ED81 File Offset: 0x0035CF81
		// (set) Token: 0x0601A303 RID: 107267 RVA: 0x0035ED8B File Offset: 0x0035CF8B
		public Kinsoku Kinsoku
		{
			get
			{
				return base.GetElement<Kinsoku>(11);
			}
			set
			{
				base.SetElement<Kinsoku>(11, value);
			}
		}

		// Token: 0x1700916F RID: 37231
		// (get) Token: 0x0601A304 RID: 107268 RVA: 0x0035ED96 File Offset: 0x0035CF96
		// (set) Token: 0x0601A305 RID: 107269 RVA: 0x0035EDA0 File Offset: 0x0035CFA0
		public WordWrap WordWrap
		{
			get
			{
				return base.GetElement<WordWrap>(12);
			}
			set
			{
				base.SetElement<WordWrap>(12, value);
			}
		}

		// Token: 0x17009170 RID: 37232
		// (get) Token: 0x0601A306 RID: 107270 RVA: 0x0035EDAB File Offset: 0x0035CFAB
		// (set) Token: 0x0601A307 RID: 107271 RVA: 0x0035EDB5 File Offset: 0x0035CFB5
		public OverflowPunctuation OverflowPunctuation
		{
			get
			{
				return base.GetElement<OverflowPunctuation>(13);
			}
			set
			{
				base.SetElement<OverflowPunctuation>(13, value);
			}
		}

		// Token: 0x17009171 RID: 37233
		// (get) Token: 0x0601A308 RID: 107272 RVA: 0x0035EDC0 File Offset: 0x0035CFC0
		// (set) Token: 0x0601A309 RID: 107273 RVA: 0x0035EDCA File Offset: 0x0035CFCA
		public TopLinePunctuation TopLinePunctuation
		{
			get
			{
				return base.GetElement<TopLinePunctuation>(14);
			}
			set
			{
				base.SetElement<TopLinePunctuation>(14, value);
			}
		}

		// Token: 0x17009172 RID: 37234
		// (get) Token: 0x0601A30A RID: 107274 RVA: 0x0035EDD5 File Offset: 0x0035CFD5
		// (set) Token: 0x0601A30B RID: 107275 RVA: 0x0035EDDF File Offset: 0x0035CFDF
		public AutoSpaceDE AutoSpaceDE
		{
			get
			{
				return base.GetElement<AutoSpaceDE>(15);
			}
			set
			{
				base.SetElement<AutoSpaceDE>(15, value);
			}
		}

		// Token: 0x17009173 RID: 37235
		// (get) Token: 0x0601A30C RID: 107276 RVA: 0x0035EDEA File Offset: 0x0035CFEA
		// (set) Token: 0x0601A30D RID: 107277 RVA: 0x0035EDF4 File Offset: 0x0035CFF4
		public AutoSpaceDN AutoSpaceDN
		{
			get
			{
				return base.GetElement<AutoSpaceDN>(16);
			}
			set
			{
				base.SetElement<AutoSpaceDN>(16, value);
			}
		}

		// Token: 0x17009174 RID: 37236
		// (get) Token: 0x0601A30E RID: 107278 RVA: 0x0035EDFF File Offset: 0x0035CFFF
		// (set) Token: 0x0601A30F RID: 107279 RVA: 0x0035EE09 File Offset: 0x0035D009
		public BiDi BiDi
		{
			get
			{
				return base.GetElement<BiDi>(17);
			}
			set
			{
				base.SetElement<BiDi>(17, value);
			}
		}

		// Token: 0x17009175 RID: 37237
		// (get) Token: 0x0601A310 RID: 107280 RVA: 0x0035EE14 File Offset: 0x0035D014
		// (set) Token: 0x0601A311 RID: 107281 RVA: 0x0035EE1E File Offset: 0x0035D01E
		public AdjustRightIndent AdjustRightIndent
		{
			get
			{
				return base.GetElement<AdjustRightIndent>(18);
			}
			set
			{
				base.SetElement<AdjustRightIndent>(18, value);
			}
		}

		// Token: 0x17009176 RID: 37238
		// (get) Token: 0x0601A312 RID: 107282 RVA: 0x0035EE29 File Offset: 0x0035D029
		// (set) Token: 0x0601A313 RID: 107283 RVA: 0x0035EE33 File Offset: 0x0035D033
		public SnapToGrid SnapToGrid
		{
			get
			{
				return base.GetElement<SnapToGrid>(19);
			}
			set
			{
				base.SetElement<SnapToGrid>(19, value);
			}
		}

		// Token: 0x17009177 RID: 37239
		// (get) Token: 0x0601A314 RID: 107284 RVA: 0x0035EE3E File Offset: 0x0035D03E
		// (set) Token: 0x0601A315 RID: 107285 RVA: 0x0035EE48 File Offset: 0x0035D048
		public SpacingBetweenLines SpacingBetweenLines
		{
			get
			{
				return base.GetElement<SpacingBetweenLines>(20);
			}
			set
			{
				base.SetElement<SpacingBetweenLines>(20, value);
			}
		}

		// Token: 0x17009178 RID: 37240
		// (get) Token: 0x0601A316 RID: 107286 RVA: 0x0035EE53 File Offset: 0x0035D053
		// (set) Token: 0x0601A317 RID: 107287 RVA: 0x0035EE5D File Offset: 0x0035D05D
		public Indentation Indentation
		{
			get
			{
				return base.GetElement<Indentation>(21);
			}
			set
			{
				base.SetElement<Indentation>(21, value);
			}
		}

		// Token: 0x17009179 RID: 37241
		// (get) Token: 0x0601A318 RID: 107288 RVA: 0x0035EE68 File Offset: 0x0035D068
		// (set) Token: 0x0601A319 RID: 107289 RVA: 0x0035EE72 File Offset: 0x0035D072
		public ContextualSpacing ContextualSpacing
		{
			get
			{
				return base.GetElement<ContextualSpacing>(22);
			}
			set
			{
				base.SetElement<ContextualSpacing>(22, value);
			}
		}

		// Token: 0x1700917A RID: 37242
		// (get) Token: 0x0601A31A RID: 107290 RVA: 0x0035EE7D File Offset: 0x0035D07D
		// (set) Token: 0x0601A31B RID: 107291 RVA: 0x0035EE87 File Offset: 0x0035D087
		public MirrorIndents MirrorIndents
		{
			get
			{
				return base.GetElement<MirrorIndents>(23);
			}
			set
			{
				base.SetElement<MirrorIndents>(23, value);
			}
		}

		// Token: 0x1700917B RID: 37243
		// (get) Token: 0x0601A31C RID: 107292 RVA: 0x0035EE92 File Offset: 0x0035D092
		// (set) Token: 0x0601A31D RID: 107293 RVA: 0x0035EE9C File Offset: 0x0035D09C
		public SuppressOverlap SuppressOverlap
		{
			get
			{
				return base.GetElement<SuppressOverlap>(24);
			}
			set
			{
				base.SetElement<SuppressOverlap>(24, value);
			}
		}

		// Token: 0x1700917C RID: 37244
		// (get) Token: 0x0601A31E RID: 107294 RVA: 0x0035EEA7 File Offset: 0x0035D0A7
		// (set) Token: 0x0601A31F RID: 107295 RVA: 0x0035EEB1 File Offset: 0x0035D0B1
		public Justification Justification
		{
			get
			{
				return base.GetElement<Justification>(25);
			}
			set
			{
				base.SetElement<Justification>(25, value);
			}
		}

		// Token: 0x1700917D RID: 37245
		// (get) Token: 0x0601A320 RID: 107296 RVA: 0x0035EEBC File Offset: 0x0035D0BC
		// (set) Token: 0x0601A321 RID: 107297 RVA: 0x0035EEC6 File Offset: 0x0035D0C6
		public TextDirection TextDirection
		{
			get
			{
				return base.GetElement<TextDirection>(26);
			}
			set
			{
				base.SetElement<TextDirection>(26, value);
			}
		}

		// Token: 0x1700917E RID: 37246
		// (get) Token: 0x0601A322 RID: 107298 RVA: 0x0035EED1 File Offset: 0x0035D0D1
		// (set) Token: 0x0601A323 RID: 107299 RVA: 0x0035EEDB File Offset: 0x0035D0DB
		public TextAlignment TextAlignment
		{
			get
			{
				return base.GetElement<TextAlignment>(27);
			}
			set
			{
				base.SetElement<TextAlignment>(27, value);
			}
		}

		// Token: 0x1700917F RID: 37247
		// (get) Token: 0x0601A324 RID: 107300 RVA: 0x0035EEE6 File Offset: 0x0035D0E6
		// (set) Token: 0x0601A325 RID: 107301 RVA: 0x0035EEF0 File Offset: 0x0035D0F0
		public TextBoxTightWrap TextBoxTightWrap
		{
			get
			{
				return base.GetElement<TextBoxTightWrap>(28);
			}
			set
			{
				base.SetElement<TextBoxTightWrap>(28, value);
			}
		}

		// Token: 0x17009180 RID: 37248
		// (get) Token: 0x0601A326 RID: 107302 RVA: 0x0035EEFB File Offset: 0x0035D0FB
		// (set) Token: 0x0601A327 RID: 107303 RVA: 0x0035EF05 File Offset: 0x0035D105
		public OutlineLevel OutlineLevel
		{
			get
			{
				return base.GetElement<OutlineLevel>(29);
			}
			set
			{
				base.SetElement<OutlineLevel>(29, value);
			}
		}

		// Token: 0x0601A328 RID: 107304 RVA: 0x0035EF10 File Offset: 0x0035D110
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphPropertiesBaseStyle>(deep);
		}

		// Token: 0x0400AC20 RID: 44064
		private const string tagName = "pPr";

		// Token: 0x0400AC21 RID: 44065
		private const byte tagNsId = 23;

		// Token: 0x0400AC22 RID: 44066
		internal const int ElementTypeIdConst = 11832;

		// Token: 0x0400AC23 RID: 44067
		private static readonly string[] eleTagNames = new string[]
		{
			"keepNext", "keepLines", "pageBreakBefore", "framePr", "widowControl", "numPr", "suppressLineNumbers", "pBdr", "shd", "tabs",
			"suppressAutoHyphens", "kinsoku", "wordWrap", "overflowPunct", "topLinePunct", "autoSpaceDE", "autoSpaceDN", "bidi", "adjustRightInd", "snapToGrid",
			"spacing", "ind", "contextualSpacing", "mirrorIndents", "suppressOverlap", "jc", "textDirection", "textAlignment", "textboxTightWrap", "outlineLvl"
		};

		// Token: 0x0400AC24 RID: 44068
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23
		};
	}
}
