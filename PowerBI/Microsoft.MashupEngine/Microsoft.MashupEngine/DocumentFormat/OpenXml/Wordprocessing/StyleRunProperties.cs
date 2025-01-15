using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA8 RID: 12200
	[ChildElementInfo(typeof(RunFonts))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(FitText))]
	[ChildElementInfo(typeof(FontSizeComplexScript))]
	[ChildElementInfo(typeof(Position))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Italic))]
	[ChildElementInfo(typeof(ItalicComplexScript))]
	[ChildElementInfo(typeof(Caps))]
	[ChildElementInfo(typeof(SmallCaps))]
	[ChildElementInfo(typeof(Strike))]
	[ChildElementInfo(typeof(DoubleStrike))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(Emboss))]
	[ChildElementInfo(typeof(Imprint))]
	[ChildElementInfo(typeof(NoProof))]
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(Vanish))]
	[ChildElementInfo(typeof(WebHidden))]
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(Spacing))]
	[ChildElementInfo(typeof(CharacterScale))]
	[ChildElementInfo(typeof(Kern))]
	[ChildElementInfo(typeof(BoldComplexScript))]
	[ChildElementInfo(typeof(FontSize))]
	[ChildElementInfo(typeof(Bold))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(TextEffect))]
	[ChildElementInfo(typeof(Border))]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(Emphasis))]
	[ChildElementInfo(typeof(Languages))]
	[ChildElementInfo(typeof(EastAsianLayout))]
	[ChildElementInfo(typeof(SpecVanish))]
	[ChildElementInfo(typeof(RunPropertiesChange))]
	internal class StyleRunProperties : OpenXmlCompositeElement
	{
		// Token: 0x170092EF RID: 37615
		// (get) Token: 0x0601A634 RID: 108084 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x170092F0 RID: 37616
		// (get) Token: 0x0601A635 RID: 108085 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092F1 RID: 37617
		// (get) Token: 0x0601A636 RID: 108086 RVA: 0x0036185C File Offset: 0x0035FA5C
		internal override int ElementTypeId
		{
			get
			{
				return 11907;
			}
		}

		// Token: 0x0601A637 RID: 108087 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A638 RID: 108088 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleRunProperties()
		{
		}

		// Token: 0x0601A639 RID: 108089 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A63A RID: 108090 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A63B RID: 108091 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A63C RID: 108092 RVA: 0x00361864 File Offset: 0x0035FA64
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rFonts" == name)
			{
				return new RunFonts();
			}
			if (23 == namespaceId && "b" == name)
			{
				return new Bold();
			}
			if (23 == namespaceId && "bCs" == name)
			{
				return new BoldComplexScript();
			}
			if (23 == namespaceId && "i" == name)
			{
				return new Italic();
			}
			if (23 == namespaceId && "iCs" == name)
			{
				return new ItalicComplexScript();
			}
			if (23 == namespaceId && "caps" == name)
			{
				return new Caps();
			}
			if (23 == namespaceId && "smallCaps" == name)
			{
				return new SmallCaps();
			}
			if (23 == namespaceId && "strike" == name)
			{
				return new Strike();
			}
			if (23 == namespaceId && "dstrike" == name)
			{
				return new DoubleStrike();
			}
			if (23 == namespaceId && "outline" == name)
			{
				return new Outline();
			}
			if (23 == namespaceId && "shadow" == name)
			{
				return new Shadow();
			}
			if (23 == namespaceId && "emboss" == name)
			{
				return new Emboss();
			}
			if (23 == namespaceId && "imprint" == name)
			{
				return new Imprint();
			}
			if (23 == namespaceId && "noProof" == name)
			{
				return new NoProof();
			}
			if (23 == namespaceId && "snapToGrid" == name)
			{
				return new SnapToGrid();
			}
			if (23 == namespaceId && "vanish" == name)
			{
				return new Vanish();
			}
			if (23 == namespaceId && "webHidden" == name)
			{
				return new WebHidden();
			}
			if (23 == namespaceId && "color" == name)
			{
				return new Color();
			}
			if (23 == namespaceId && "spacing" == name)
			{
				return new Spacing();
			}
			if (23 == namespaceId && "w" == name)
			{
				return new CharacterScale();
			}
			if (23 == namespaceId && "kern" == name)
			{
				return new Kern();
			}
			if (23 == namespaceId && "position" == name)
			{
				return new Position();
			}
			if (23 == namespaceId && "sz" == name)
			{
				return new FontSize();
			}
			if (23 == namespaceId && "szCs" == name)
			{
				return new FontSizeComplexScript();
			}
			if (23 == namespaceId && "u" == name)
			{
				return new Underline();
			}
			if (23 == namespaceId && "effect" == name)
			{
				return new TextEffect();
			}
			if (23 == namespaceId && "bdr" == name)
			{
				return new Border();
			}
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "fitText" == name)
			{
				return new FitText();
			}
			if (23 == namespaceId && "vertAlign" == name)
			{
				return new VerticalTextAlignment();
			}
			if (23 == namespaceId && "em" == name)
			{
				return new Emphasis();
			}
			if (23 == namespaceId && "lang" == name)
			{
				return new Languages();
			}
			if (23 == namespaceId && "eastAsianLayout" == name)
			{
				return new EastAsianLayout();
			}
			if (23 == namespaceId && "specVanish" == name)
			{
				return new SpecVanish();
			}
			if (23 == namespaceId && "rPrChange" == name)
			{
				return new RunPropertiesChange();
			}
			return null;
		}

		// Token: 0x170092F2 RID: 37618
		// (get) Token: 0x0601A63D RID: 108093 RVA: 0x00361BBA File Offset: 0x0035FDBA
		internal override string[] ElementTagNames
		{
			get
			{
				return StyleRunProperties.eleTagNames;
			}
		}

		// Token: 0x170092F3 RID: 37619
		// (get) Token: 0x0601A63E RID: 108094 RVA: 0x00361BC1 File Offset: 0x0035FDC1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StyleRunProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170092F4 RID: 37620
		// (get) Token: 0x0601A63F RID: 108095 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170092F5 RID: 37621
		// (get) Token: 0x0601A640 RID: 108096 RVA: 0x0035E598 File Offset: 0x0035C798
		// (set) Token: 0x0601A641 RID: 108097 RVA: 0x0035E5A1 File Offset: 0x0035C7A1
		public RunFonts RunFonts
		{
			get
			{
				return base.GetElement<RunFonts>(0);
			}
			set
			{
				base.SetElement<RunFonts>(0, value);
			}
		}

		// Token: 0x170092F6 RID: 37622
		// (get) Token: 0x0601A642 RID: 108098 RVA: 0x0035E5AB File Offset: 0x0035C7AB
		// (set) Token: 0x0601A643 RID: 108099 RVA: 0x0035E5B4 File Offset: 0x0035C7B4
		public Bold Bold
		{
			get
			{
				return base.GetElement<Bold>(1);
			}
			set
			{
				base.SetElement<Bold>(1, value);
			}
		}

		// Token: 0x170092F7 RID: 37623
		// (get) Token: 0x0601A644 RID: 108100 RVA: 0x0035E5BE File Offset: 0x0035C7BE
		// (set) Token: 0x0601A645 RID: 108101 RVA: 0x0035E5C7 File Offset: 0x0035C7C7
		public BoldComplexScript BoldComplexScript
		{
			get
			{
				return base.GetElement<BoldComplexScript>(2);
			}
			set
			{
				base.SetElement<BoldComplexScript>(2, value);
			}
		}

		// Token: 0x170092F8 RID: 37624
		// (get) Token: 0x0601A646 RID: 108102 RVA: 0x0035E5D1 File Offset: 0x0035C7D1
		// (set) Token: 0x0601A647 RID: 108103 RVA: 0x0035E5DA File Offset: 0x0035C7DA
		public Italic Italic
		{
			get
			{
				return base.GetElement<Italic>(3);
			}
			set
			{
				base.SetElement<Italic>(3, value);
			}
		}

		// Token: 0x170092F9 RID: 37625
		// (get) Token: 0x0601A648 RID: 108104 RVA: 0x0035E5E4 File Offset: 0x0035C7E4
		// (set) Token: 0x0601A649 RID: 108105 RVA: 0x0035E5ED File Offset: 0x0035C7ED
		public ItalicComplexScript ItalicComplexScript
		{
			get
			{
				return base.GetElement<ItalicComplexScript>(4);
			}
			set
			{
				base.SetElement<ItalicComplexScript>(4, value);
			}
		}

		// Token: 0x170092FA RID: 37626
		// (get) Token: 0x0601A64A RID: 108106 RVA: 0x0035E5F7 File Offset: 0x0035C7F7
		// (set) Token: 0x0601A64B RID: 108107 RVA: 0x0035E600 File Offset: 0x0035C800
		public Caps Caps
		{
			get
			{
				return base.GetElement<Caps>(5);
			}
			set
			{
				base.SetElement<Caps>(5, value);
			}
		}

		// Token: 0x170092FB RID: 37627
		// (get) Token: 0x0601A64C RID: 108108 RVA: 0x0035E60A File Offset: 0x0035C80A
		// (set) Token: 0x0601A64D RID: 108109 RVA: 0x0035E613 File Offset: 0x0035C813
		public SmallCaps SmallCaps
		{
			get
			{
				return base.GetElement<SmallCaps>(6);
			}
			set
			{
				base.SetElement<SmallCaps>(6, value);
			}
		}

		// Token: 0x170092FC RID: 37628
		// (get) Token: 0x0601A64E RID: 108110 RVA: 0x0035E61D File Offset: 0x0035C81D
		// (set) Token: 0x0601A64F RID: 108111 RVA: 0x0035E626 File Offset: 0x0035C826
		public Strike Strike
		{
			get
			{
				return base.GetElement<Strike>(7);
			}
			set
			{
				base.SetElement<Strike>(7, value);
			}
		}

		// Token: 0x170092FD RID: 37629
		// (get) Token: 0x0601A650 RID: 108112 RVA: 0x0035E630 File Offset: 0x0035C830
		// (set) Token: 0x0601A651 RID: 108113 RVA: 0x0035E639 File Offset: 0x0035C839
		public DoubleStrike DoubleStrike
		{
			get
			{
				return base.GetElement<DoubleStrike>(8);
			}
			set
			{
				base.SetElement<DoubleStrike>(8, value);
			}
		}

		// Token: 0x170092FE RID: 37630
		// (get) Token: 0x0601A652 RID: 108114 RVA: 0x0035E643 File Offset: 0x0035C843
		// (set) Token: 0x0601A653 RID: 108115 RVA: 0x0035E64D File Offset: 0x0035C84D
		public Outline Outline
		{
			get
			{
				return base.GetElement<Outline>(9);
			}
			set
			{
				base.SetElement<Outline>(9, value);
			}
		}

		// Token: 0x170092FF RID: 37631
		// (get) Token: 0x0601A654 RID: 108116 RVA: 0x0035E658 File Offset: 0x0035C858
		// (set) Token: 0x0601A655 RID: 108117 RVA: 0x0035E662 File Offset: 0x0035C862
		public Shadow Shadow
		{
			get
			{
				return base.GetElement<Shadow>(10);
			}
			set
			{
				base.SetElement<Shadow>(10, value);
			}
		}

		// Token: 0x17009300 RID: 37632
		// (get) Token: 0x0601A656 RID: 108118 RVA: 0x0035E66D File Offset: 0x0035C86D
		// (set) Token: 0x0601A657 RID: 108119 RVA: 0x0035E677 File Offset: 0x0035C877
		public Emboss Emboss
		{
			get
			{
				return base.GetElement<Emboss>(11);
			}
			set
			{
				base.SetElement<Emboss>(11, value);
			}
		}

		// Token: 0x17009301 RID: 37633
		// (get) Token: 0x0601A658 RID: 108120 RVA: 0x0035E682 File Offset: 0x0035C882
		// (set) Token: 0x0601A659 RID: 108121 RVA: 0x0035E68C File Offset: 0x0035C88C
		public Imprint Imprint
		{
			get
			{
				return base.GetElement<Imprint>(12);
			}
			set
			{
				base.SetElement<Imprint>(12, value);
			}
		}

		// Token: 0x17009302 RID: 37634
		// (get) Token: 0x0601A65A RID: 108122 RVA: 0x0035E697 File Offset: 0x0035C897
		// (set) Token: 0x0601A65B RID: 108123 RVA: 0x0035E6A1 File Offset: 0x0035C8A1
		public NoProof NoProof
		{
			get
			{
				return base.GetElement<NoProof>(13);
			}
			set
			{
				base.SetElement<NoProof>(13, value);
			}
		}

		// Token: 0x17009303 RID: 37635
		// (get) Token: 0x0601A65C RID: 108124 RVA: 0x0035E6AC File Offset: 0x0035C8AC
		// (set) Token: 0x0601A65D RID: 108125 RVA: 0x0035E6B6 File Offset: 0x0035C8B6
		public SnapToGrid SnapToGrid
		{
			get
			{
				return base.GetElement<SnapToGrid>(14);
			}
			set
			{
				base.SetElement<SnapToGrid>(14, value);
			}
		}

		// Token: 0x17009304 RID: 37636
		// (get) Token: 0x0601A65E RID: 108126 RVA: 0x0035E6C1 File Offset: 0x0035C8C1
		// (set) Token: 0x0601A65F RID: 108127 RVA: 0x0035E6CB File Offset: 0x0035C8CB
		public Vanish Vanish
		{
			get
			{
				return base.GetElement<Vanish>(15);
			}
			set
			{
				base.SetElement<Vanish>(15, value);
			}
		}

		// Token: 0x17009305 RID: 37637
		// (get) Token: 0x0601A660 RID: 108128 RVA: 0x0035E6D6 File Offset: 0x0035C8D6
		// (set) Token: 0x0601A661 RID: 108129 RVA: 0x0035E6E0 File Offset: 0x0035C8E0
		public WebHidden WebHidden
		{
			get
			{
				return base.GetElement<WebHidden>(16);
			}
			set
			{
				base.SetElement<WebHidden>(16, value);
			}
		}

		// Token: 0x17009306 RID: 37638
		// (get) Token: 0x0601A662 RID: 108130 RVA: 0x0035E6EB File Offset: 0x0035C8EB
		// (set) Token: 0x0601A663 RID: 108131 RVA: 0x0035E6F5 File Offset: 0x0035C8F5
		public Color Color
		{
			get
			{
				return base.GetElement<Color>(17);
			}
			set
			{
				base.SetElement<Color>(17, value);
			}
		}

		// Token: 0x17009307 RID: 37639
		// (get) Token: 0x0601A664 RID: 108132 RVA: 0x0035E700 File Offset: 0x0035C900
		// (set) Token: 0x0601A665 RID: 108133 RVA: 0x0035E70A File Offset: 0x0035C90A
		public Spacing Spacing
		{
			get
			{
				return base.GetElement<Spacing>(18);
			}
			set
			{
				base.SetElement<Spacing>(18, value);
			}
		}

		// Token: 0x17009308 RID: 37640
		// (get) Token: 0x0601A666 RID: 108134 RVA: 0x0035E715 File Offset: 0x0035C915
		// (set) Token: 0x0601A667 RID: 108135 RVA: 0x0035E71F File Offset: 0x0035C91F
		public CharacterScale CharacterScale
		{
			get
			{
				return base.GetElement<CharacterScale>(19);
			}
			set
			{
				base.SetElement<CharacterScale>(19, value);
			}
		}

		// Token: 0x17009309 RID: 37641
		// (get) Token: 0x0601A668 RID: 108136 RVA: 0x0035E72A File Offset: 0x0035C92A
		// (set) Token: 0x0601A669 RID: 108137 RVA: 0x0035E734 File Offset: 0x0035C934
		public Kern Kern
		{
			get
			{
				return base.GetElement<Kern>(20);
			}
			set
			{
				base.SetElement<Kern>(20, value);
			}
		}

		// Token: 0x1700930A RID: 37642
		// (get) Token: 0x0601A66A RID: 108138 RVA: 0x0035E73F File Offset: 0x0035C93F
		// (set) Token: 0x0601A66B RID: 108139 RVA: 0x0035E749 File Offset: 0x0035C949
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(21);
			}
			set
			{
				base.SetElement<Position>(21, value);
			}
		}

		// Token: 0x1700930B RID: 37643
		// (get) Token: 0x0601A66C RID: 108140 RVA: 0x0035E754 File Offset: 0x0035C954
		// (set) Token: 0x0601A66D RID: 108141 RVA: 0x0035E75E File Offset: 0x0035C95E
		public FontSize FontSize
		{
			get
			{
				return base.GetElement<FontSize>(22);
			}
			set
			{
				base.SetElement<FontSize>(22, value);
			}
		}

		// Token: 0x1700930C RID: 37644
		// (get) Token: 0x0601A66E RID: 108142 RVA: 0x0035E769 File Offset: 0x0035C969
		// (set) Token: 0x0601A66F RID: 108143 RVA: 0x0035E773 File Offset: 0x0035C973
		public FontSizeComplexScript FontSizeComplexScript
		{
			get
			{
				return base.GetElement<FontSizeComplexScript>(23);
			}
			set
			{
				base.SetElement<FontSizeComplexScript>(23, value);
			}
		}

		// Token: 0x1700930D RID: 37645
		// (get) Token: 0x0601A670 RID: 108144 RVA: 0x0035E77E File Offset: 0x0035C97E
		// (set) Token: 0x0601A671 RID: 108145 RVA: 0x0035E788 File Offset: 0x0035C988
		public Underline Underline
		{
			get
			{
				return base.GetElement<Underline>(24);
			}
			set
			{
				base.SetElement<Underline>(24, value);
			}
		}

		// Token: 0x1700930E RID: 37646
		// (get) Token: 0x0601A672 RID: 108146 RVA: 0x0035E793 File Offset: 0x0035C993
		// (set) Token: 0x0601A673 RID: 108147 RVA: 0x0035E79D File Offset: 0x0035C99D
		public TextEffect TextEffect
		{
			get
			{
				return base.GetElement<TextEffect>(25);
			}
			set
			{
				base.SetElement<TextEffect>(25, value);
			}
		}

		// Token: 0x1700930F RID: 37647
		// (get) Token: 0x0601A674 RID: 108148 RVA: 0x0035E7A8 File Offset: 0x0035C9A8
		// (set) Token: 0x0601A675 RID: 108149 RVA: 0x0035E7B2 File Offset: 0x0035C9B2
		public Border Border
		{
			get
			{
				return base.GetElement<Border>(26);
			}
			set
			{
				base.SetElement<Border>(26, value);
			}
		}

		// Token: 0x17009310 RID: 37648
		// (get) Token: 0x0601A676 RID: 108150 RVA: 0x0035E7BD File Offset: 0x0035C9BD
		// (set) Token: 0x0601A677 RID: 108151 RVA: 0x0035E7C7 File Offset: 0x0035C9C7
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(27);
			}
			set
			{
				base.SetElement<Shading>(27, value);
			}
		}

		// Token: 0x17009311 RID: 37649
		// (get) Token: 0x0601A678 RID: 108152 RVA: 0x0035E7D2 File Offset: 0x0035C9D2
		// (set) Token: 0x0601A679 RID: 108153 RVA: 0x0035E7DC File Offset: 0x0035C9DC
		public FitText FitText
		{
			get
			{
				return base.GetElement<FitText>(28);
			}
			set
			{
				base.SetElement<FitText>(28, value);
			}
		}

		// Token: 0x17009312 RID: 37650
		// (get) Token: 0x0601A67A RID: 108154 RVA: 0x0035E7E7 File Offset: 0x0035C9E7
		// (set) Token: 0x0601A67B RID: 108155 RVA: 0x0035E7F1 File Offset: 0x0035C9F1
		public VerticalTextAlignment VerticalTextAlignment
		{
			get
			{
				return base.GetElement<VerticalTextAlignment>(29);
			}
			set
			{
				base.SetElement<VerticalTextAlignment>(29, value);
			}
		}

		// Token: 0x17009313 RID: 37651
		// (get) Token: 0x0601A67C RID: 108156 RVA: 0x0035E7FC File Offset: 0x0035C9FC
		// (set) Token: 0x0601A67D RID: 108157 RVA: 0x0035E806 File Offset: 0x0035CA06
		public Emphasis Emphasis
		{
			get
			{
				return base.GetElement<Emphasis>(30);
			}
			set
			{
				base.SetElement<Emphasis>(30, value);
			}
		}

		// Token: 0x17009314 RID: 37652
		// (get) Token: 0x0601A67E RID: 108158 RVA: 0x0035E811 File Offset: 0x0035CA11
		// (set) Token: 0x0601A67F RID: 108159 RVA: 0x0035E81B File Offset: 0x0035CA1B
		public Languages Languages
		{
			get
			{
				return base.GetElement<Languages>(31);
			}
			set
			{
				base.SetElement<Languages>(31, value);
			}
		}

		// Token: 0x17009315 RID: 37653
		// (get) Token: 0x0601A680 RID: 108160 RVA: 0x0035E826 File Offset: 0x0035CA26
		// (set) Token: 0x0601A681 RID: 108161 RVA: 0x0035E830 File Offset: 0x0035CA30
		public EastAsianLayout EastAsianLayout
		{
			get
			{
				return base.GetElement<EastAsianLayout>(32);
			}
			set
			{
				base.SetElement<EastAsianLayout>(32, value);
			}
		}

		// Token: 0x17009316 RID: 37654
		// (get) Token: 0x0601A682 RID: 108162 RVA: 0x0035E83B File Offset: 0x0035CA3B
		// (set) Token: 0x0601A683 RID: 108163 RVA: 0x0035E845 File Offset: 0x0035CA45
		public SpecVanish SpecVanish
		{
			get
			{
				return base.GetElement<SpecVanish>(33);
			}
			set
			{
				base.SetElement<SpecVanish>(33, value);
			}
		}

		// Token: 0x17009317 RID: 37655
		// (get) Token: 0x0601A684 RID: 108164 RVA: 0x00361BC8 File Offset: 0x0035FDC8
		// (set) Token: 0x0601A685 RID: 108165 RVA: 0x00361BD2 File Offset: 0x0035FDD2
		public RunPropertiesChange RunPropertiesChange
		{
			get
			{
				return base.GetElement<RunPropertiesChange>(34);
			}
			set
			{
				base.SetElement<RunPropertiesChange>(34, value);
			}
		}

		// Token: 0x0601A686 RID: 108166 RVA: 0x00361BDD File Offset: 0x0035FDDD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleRunProperties>(deep);
		}

		// Token: 0x0400ACD9 RID: 44249
		private const string tagName = "rPr";

		// Token: 0x0400ACDA RID: 44250
		private const byte tagNsId = 23;

		// Token: 0x0400ACDB RID: 44251
		internal const int ElementTypeIdConst = 11907;

		// Token: 0x0400ACDC RID: 44252
		private static readonly string[] eleTagNames = new string[]
		{
			"rFonts", "b", "bCs", "i", "iCs", "caps", "smallCaps", "strike", "dstrike", "outline",
			"shadow", "emboss", "imprint", "noProof", "snapToGrid", "vanish", "webHidden", "color", "spacing", "w",
			"kern", "position", "sz", "szCs", "u", "effect", "bdr", "shd", "fitText", "vertAlign",
			"em", "lang", "eastAsianLayout", "specVanish", "rPrChange"
		};

		// Token: 0x0400ACDD RID: 44253
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23
		};
	}
}
