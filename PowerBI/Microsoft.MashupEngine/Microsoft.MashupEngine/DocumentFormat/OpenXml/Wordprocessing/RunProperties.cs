using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB2 RID: 11954
	[ChildElementInfo(typeof(WebHidden))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(FitText))]
	[ChildElementInfo(typeof(RunStyle))]
	[ChildElementInfo(typeof(RunFonts))]
	[ChildElementInfo(typeof(Bold))]
	[ChildElementInfo(typeof(BoldComplexScript))]
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
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(Spacing))]
	[ChildElementInfo(typeof(CharacterScale))]
	[ChildElementInfo(typeof(Kern))]
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(FontSize))]
	[ChildElementInfo(typeof(FontSizeComplexScript))]
	[ChildElementInfo(typeof(Highlight))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(TextEffect))]
	[ChildElementInfo(typeof(Border))]
	[ChildElementInfo(typeof(Properties3D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(RightToLeftText))]
	[ChildElementInfo(typeof(ComplexScript))]
	[ChildElementInfo(typeof(Emphasis))]
	[ChildElementInfo(typeof(Languages))]
	[ChildElementInfo(typeof(EastAsianLayout))]
	[ChildElementInfo(typeof(SpecVanish))]
	[ChildElementInfo(typeof(Glow), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Shadow), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Reflection), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TextOutlineEffect), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FillTextEffect), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Scene3D), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Ligatures), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NumberingFormat), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NumberSpacing), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(StylisticSets), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ContextualAlternatives), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunPropertiesChange))]
	internal class RunProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008BCD RID: 35789
		// (get) Token: 0x0601968E RID: 104078 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17008BCE RID: 35790
		// (get) Token: 0x0601968F RID: 104079 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BCF RID: 35791
		// (get) Token: 0x06019690 RID: 104080 RVA: 0x003495DB File Offset: 0x003477DB
		internal override int ElementTypeId
		{
			get
			{
				return 11613;
			}
		}

		// Token: 0x06019691 RID: 104081 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019692 RID: 104082 RVA: 0x00293ECF File Offset: 0x002920CF
		public RunProperties()
		{
		}

		// Token: 0x06019693 RID: 104083 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019694 RID: 104084 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019695 RID: 104085 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019696 RID: 104086 RVA: 0x003495E4 File Offset: 0x003477E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rStyle" == name)
			{
				return new RunStyle();
			}
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
			if (23 == namespaceId && "highlight" == name)
			{
				return new Highlight();
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
			if (23 == namespaceId && "rtl" == name)
			{
				return new RightToLeftText();
			}
			if (23 == namespaceId && "cs" == name)
			{
				return new ComplexScript();
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
			if (52 == namespaceId && "glow" == name)
			{
				return new Glow();
			}
			if (52 == namespaceId && "shadow" == name)
			{
				return new Shadow();
			}
			if (52 == namespaceId && "reflection" == name)
			{
				return new Reflection();
			}
			if (52 == namespaceId && "textOutline" == name)
			{
				return new TextOutlineEffect();
			}
			if (52 == namespaceId && "textFill" == name)
			{
				return new FillTextEffect();
			}
			if (52 == namespaceId && "scene3d" == name)
			{
				return new Scene3D();
			}
			if (52 == namespaceId && "props3d" == name)
			{
				return new Properties3D();
			}
			if (52 == namespaceId && "ligatures" == name)
			{
				return new Ligatures();
			}
			if (52 == namespaceId && "numForm" == name)
			{
				return new NumberingFormat();
			}
			if (52 == namespaceId && "numSpacing" == name)
			{
				return new NumberSpacing();
			}
			if (52 == namespaceId && "stylisticSets" == name)
			{
				return new StylisticSets();
			}
			if (52 == namespaceId && "cntxtAlts" == name)
			{
				return new ContextualAlternatives();
			}
			if (23 == namespaceId && "rPrChange" == name)
			{
				return new RunPropertiesChange();
			}
			return null;
		}

		// Token: 0x17008BD0 RID: 35792
		// (get) Token: 0x06019697 RID: 104087 RVA: 0x00349ABA File Offset: 0x00347CBA
		internal override string[] ElementTagNames
		{
			get
			{
				return RunProperties.eleTagNames;
			}
		}

		// Token: 0x17008BD1 RID: 35793
		// (get) Token: 0x06019698 RID: 104088 RVA: 0x00349AC1 File Offset: 0x00347CC1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RunProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008BD2 RID: 35794
		// (get) Token: 0x06019699 RID: 104089 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008BD3 RID: 35795
		// (get) Token: 0x0601969A RID: 104090 RVA: 0x00349AC8 File Offset: 0x00347CC8
		// (set) Token: 0x0601969B RID: 104091 RVA: 0x00349AD1 File Offset: 0x00347CD1
		public RunStyle RunStyle
		{
			get
			{
				return base.GetElement<RunStyle>(0);
			}
			set
			{
				base.SetElement<RunStyle>(0, value);
			}
		}

		// Token: 0x17008BD4 RID: 35796
		// (get) Token: 0x0601969C RID: 104092 RVA: 0x00349ADB File Offset: 0x00347CDB
		// (set) Token: 0x0601969D RID: 104093 RVA: 0x00349AE4 File Offset: 0x00347CE4
		public RunFonts RunFonts
		{
			get
			{
				return base.GetElement<RunFonts>(1);
			}
			set
			{
				base.SetElement<RunFonts>(1, value);
			}
		}

		// Token: 0x17008BD5 RID: 35797
		// (get) Token: 0x0601969E RID: 104094 RVA: 0x00349AEE File Offset: 0x00347CEE
		// (set) Token: 0x0601969F RID: 104095 RVA: 0x00349AF7 File Offset: 0x00347CF7
		public Bold Bold
		{
			get
			{
				return base.GetElement<Bold>(2);
			}
			set
			{
				base.SetElement<Bold>(2, value);
			}
		}

		// Token: 0x17008BD6 RID: 35798
		// (get) Token: 0x060196A0 RID: 104096 RVA: 0x00349B01 File Offset: 0x00347D01
		// (set) Token: 0x060196A1 RID: 104097 RVA: 0x00349B0A File Offset: 0x00347D0A
		public BoldComplexScript BoldComplexScript
		{
			get
			{
				return base.GetElement<BoldComplexScript>(3);
			}
			set
			{
				base.SetElement<BoldComplexScript>(3, value);
			}
		}

		// Token: 0x17008BD7 RID: 35799
		// (get) Token: 0x060196A2 RID: 104098 RVA: 0x00349B14 File Offset: 0x00347D14
		// (set) Token: 0x060196A3 RID: 104099 RVA: 0x00349B1D File Offset: 0x00347D1D
		public Italic Italic
		{
			get
			{
				return base.GetElement<Italic>(4);
			}
			set
			{
				base.SetElement<Italic>(4, value);
			}
		}

		// Token: 0x17008BD8 RID: 35800
		// (get) Token: 0x060196A4 RID: 104100 RVA: 0x00349B27 File Offset: 0x00347D27
		// (set) Token: 0x060196A5 RID: 104101 RVA: 0x00349B30 File Offset: 0x00347D30
		public ItalicComplexScript ItalicComplexScript
		{
			get
			{
				return base.GetElement<ItalicComplexScript>(5);
			}
			set
			{
				base.SetElement<ItalicComplexScript>(5, value);
			}
		}

		// Token: 0x17008BD9 RID: 35801
		// (get) Token: 0x060196A6 RID: 104102 RVA: 0x00349B3A File Offset: 0x00347D3A
		// (set) Token: 0x060196A7 RID: 104103 RVA: 0x00349B43 File Offset: 0x00347D43
		public Caps Caps
		{
			get
			{
				return base.GetElement<Caps>(6);
			}
			set
			{
				base.SetElement<Caps>(6, value);
			}
		}

		// Token: 0x17008BDA RID: 35802
		// (get) Token: 0x060196A8 RID: 104104 RVA: 0x00349B4D File Offset: 0x00347D4D
		// (set) Token: 0x060196A9 RID: 104105 RVA: 0x00349B56 File Offset: 0x00347D56
		public SmallCaps SmallCaps
		{
			get
			{
				return base.GetElement<SmallCaps>(7);
			}
			set
			{
				base.SetElement<SmallCaps>(7, value);
			}
		}

		// Token: 0x17008BDB RID: 35803
		// (get) Token: 0x060196AA RID: 104106 RVA: 0x00349B60 File Offset: 0x00347D60
		// (set) Token: 0x060196AB RID: 104107 RVA: 0x00349B69 File Offset: 0x00347D69
		public Strike Strike
		{
			get
			{
				return base.GetElement<Strike>(8);
			}
			set
			{
				base.SetElement<Strike>(8, value);
			}
		}

		// Token: 0x17008BDC RID: 35804
		// (get) Token: 0x060196AC RID: 104108 RVA: 0x00349B73 File Offset: 0x00347D73
		// (set) Token: 0x060196AD RID: 104109 RVA: 0x00349B7D File Offset: 0x00347D7D
		public DoubleStrike DoubleStrike
		{
			get
			{
				return base.GetElement<DoubleStrike>(9);
			}
			set
			{
				base.SetElement<DoubleStrike>(9, value);
			}
		}

		// Token: 0x17008BDD RID: 35805
		// (get) Token: 0x060196AE RID: 104110 RVA: 0x00349B88 File Offset: 0x00347D88
		// (set) Token: 0x060196AF RID: 104111 RVA: 0x00349B92 File Offset: 0x00347D92
		public Outline Outline
		{
			get
			{
				return base.GetElement<Outline>(10);
			}
			set
			{
				base.SetElement<Outline>(10, value);
			}
		}

		// Token: 0x17008BDE RID: 35806
		// (get) Token: 0x060196B0 RID: 104112 RVA: 0x00349B9D File Offset: 0x00347D9D
		// (set) Token: 0x060196B1 RID: 104113 RVA: 0x00349BA7 File Offset: 0x00347DA7
		public Shadow Shadow
		{
			get
			{
				return base.GetElement<Shadow>(11);
			}
			set
			{
				base.SetElement<Shadow>(11, value);
			}
		}

		// Token: 0x17008BDF RID: 35807
		// (get) Token: 0x060196B2 RID: 104114 RVA: 0x00349BB2 File Offset: 0x00347DB2
		// (set) Token: 0x060196B3 RID: 104115 RVA: 0x00349BBC File Offset: 0x00347DBC
		public Emboss Emboss
		{
			get
			{
				return base.GetElement<Emboss>(12);
			}
			set
			{
				base.SetElement<Emboss>(12, value);
			}
		}

		// Token: 0x17008BE0 RID: 35808
		// (get) Token: 0x060196B4 RID: 104116 RVA: 0x00349BC7 File Offset: 0x00347DC7
		// (set) Token: 0x060196B5 RID: 104117 RVA: 0x00349BD1 File Offset: 0x00347DD1
		public Imprint Imprint
		{
			get
			{
				return base.GetElement<Imprint>(13);
			}
			set
			{
				base.SetElement<Imprint>(13, value);
			}
		}

		// Token: 0x17008BE1 RID: 35809
		// (get) Token: 0x060196B6 RID: 104118 RVA: 0x00349BDC File Offset: 0x00347DDC
		// (set) Token: 0x060196B7 RID: 104119 RVA: 0x00349BE6 File Offset: 0x00347DE6
		public NoProof NoProof
		{
			get
			{
				return base.GetElement<NoProof>(14);
			}
			set
			{
				base.SetElement<NoProof>(14, value);
			}
		}

		// Token: 0x17008BE2 RID: 35810
		// (get) Token: 0x060196B8 RID: 104120 RVA: 0x00349BF1 File Offset: 0x00347DF1
		// (set) Token: 0x060196B9 RID: 104121 RVA: 0x00349BFB File Offset: 0x00347DFB
		public SnapToGrid SnapToGrid
		{
			get
			{
				return base.GetElement<SnapToGrid>(15);
			}
			set
			{
				base.SetElement<SnapToGrid>(15, value);
			}
		}

		// Token: 0x17008BE3 RID: 35811
		// (get) Token: 0x060196BA RID: 104122 RVA: 0x00349C06 File Offset: 0x00347E06
		// (set) Token: 0x060196BB RID: 104123 RVA: 0x00349C10 File Offset: 0x00347E10
		public Vanish Vanish
		{
			get
			{
				return base.GetElement<Vanish>(16);
			}
			set
			{
				base.SetElement<Vanish>(16, value);
			}
		}

		// Token: 0x17008BE4 RID: 35812
		// (get) Token: 0x060196BC RID: 104124 RVA: 0x00349C1B File Offset: 0x00347E1B
		// (set) Token: 0x060196BD RID: 104125 RVA: 0x00349C25 File Offset: 0x00347E25
		public WebHidden WebHidden
		{
			get
			{
				return base.GetElement<WebHidden>(17);
			}
			set
			{
				base.SetElement<WebHidden>(17, value);
			}
		}

		// Token: 0x17008BE5 RID: 35813
		// (get) Token: 0x060196BE RID: 104126 RVA: 0x00349C30 File Offset: 0x00347E30
		// (set) Token: 0x060196BF RID: 104127 RVA: 0x00349C3A File Offset: 0x00347E3A
		public Color Color
		{
			get
			{
				return base.GetElement<Color>(18);
			}
			set
			{
				base.SetElement<Color>(18, value);
			}
		}

		// Token: 0x17008BE6 RID: 35814
		// (get) Token: 0x060196C0 RID: 104128 RVA: 0x00349C45 File Offset: 0x00347E45
		// (set) Token: 0x060196C1 RID: 104129 RVA: 0x00349C4F File Offset: 0x00347E4F
		public Spacing Spacing
		{
			get
			{
				return base.GetElement<Spacing>(19);
			}
			set
			{
				base.SetElement<Spacing>(19, value);
			}
		}

		// Token: 0x17008BE7 RID: 35815
		// (get) Token: 0x060196C2 RID: 104130 RVA: 0x00349C5A File Offset: 0x00347E5A
		// (set) Token: 0x060196C3 RID: 104131 RVA: 0x00349C64 File Offset: 0x00347E64
		public CharacterScale CharacterScale
		{
			get
			{
				return base.GetElement<CharacterScale>(20);
			}
			set
			{
				base.SetElement<CharacterScale>(20, value);
			}
		}

		// Token: 0x17008BE8 RID: 35816
		// (get) Token: 0x060196C4 RID: 104132 RVA: 0x00349C6F File Offset: 0x00347E6F
		// (set) Token: 0x060196C5 RID: 104133 RVA: 0x00349C79 File Offset: 0x00347E79
		public Kern Kern
		{
			get
			{
				return base.GetElement<Kern>(21);
			}
			set
			{
				base.SetElement<Kern>(21, value);
			}
		}

		// Token: 0x17008BE9 RID: 35817
		// (get) Token: 0x060196C6 RID: 104134 RVA: 0x00349C84 File Offset: 0x00347E84
		// (set) Token: 0x060196C7 RID: 104135 RVA: 0x00349C8E File Offset: 0x00347E8E
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(22);
			}
			set
			{
				base.SetElement<Position>(22, value);
			}
		}

		// Token: 0x17008BEA RID: 35818
		// (get) Token: 0x060196C8 RID: 104136 RVA: 0x00349C99 File Offset: 0x00347E99
		// (set) Token: 0x060196C9 RID: 104137 RVA: 0x00349CA3 File Offset: 0x00347EA3
		public FontSize FontSize
		{
			get
			{
				return base.GetElement<FontSize>(23);
			}
			set
			{
				base.SetElement<FontSize>(23, value);
			}
		}

		// Token: 0x17008BEB RID: 35819
		// (get) Token: 0x060196CA RID: 104138 RVA: 0x00349CAE File Offset: 0x00347EAE
		// (set) Token: 0x060196CB RID: 104139 RVA: 0x00349CB8 File Offset: 0x00347EB8
		public FontSizeComplexScript FontSizeComplexScript
		{
			get
			{
				return base.GetElement<FontSizeComplexScript>(24);
			}
			set
			{
				base.SetElement<FontSizeComplexScript>(24, value);
			}
		}

		// Token: 0x17008BEC RID: 35820
		// (get) Token: 0x060196CC RID: 104140 RVA: 0x00349CC3 File Offset: 0x00347EC3
		// (set) Token: 0x060196CD RID: 104141 RVA: 0x00349CCD File Offset: 0x00347ECD
		public Highlight Highlight
		{
			get
			{
				return base.GetElement<Highlight>(25);
			}
			set
			{
				base.SetElement<Highlight>(25, value);
			}
		}

		// Token: 0x17008BED RID: 35821
		// (get) Token: 0x060196CE RID: 104142 RVA: 0x00349CD8 File Offset: 0x00347ED8
		// (set) Token: 0x060196CF RID: 104143 RVA: 0x00349CE2 File Offset: 0x00347EE2
		public Underline Underline
		{
			get
			{
				return base.GetElement<Underline>(26);
			}
			set
			{
				base.SetElement<Underline>(26, value);
			}
		}

		// Token: 0x17008BEE RID: 35822
		// (get) Token: 0x060196D0 RID: 104144 RVA: 0x00349CED File Offset: 0x00347EED
		// (set) Token: 0x060196D1 RID: 104145 RVA: 0x00349CF7 File Offset: 0x00347EF7
		public TextEffect TextEffect
		{
			get
			{
				return base.GetElement<TextEffect>(27);
			}
			set
			{
				base.SetElement<TextEffect>(27, value);
			}
		}

		// Token: 0x17008BEF RID: 35823
		// (get) Token: 0x060196D2 RID: 104146 RVA: 0x00349D02 File Offset: 0x00347F02
		// (set) Token: 0x060196D3 RID: 104147 RVA: 0x00349D0C File Offset: 0x00347F0C
		public Border Border
		{
			get
			{
				return base.GetElement<Border>(28);
			}
			set
			{
				base.SetElement<Border>(28, value);
			}
		}

		// Token: 0x17008BF0 RID: 35824
		// (get) Token: 0x060196D4 RID: 104148 RVA: 0x00349D17 File Offset: 0x00347F17
		// (set) Token: 0x060196D5 RID: 104149 RVA: 0x00349D21 File Offset: 0x00347F21
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(29);
			}
			set
			{
				base.SetElement<Shading>(29, value);
			}
		}

		// Token: 0x17008BF1 RID: 35825
		// (get) Token: 0x060196D6 RID: 104150 RVA: 0x00349D2C File Offset: 0x00347F2C
		// (set) Token: 0x060196D7 RID: 104151 RVA: 0x00349D36 File Offset: 0x00347F36
		public FitText FitText
		{
			get
			{
				return base.GetElement<FitText>(30);
			}
			set
			{
				base.SetElement<FitText>(30, value);
			}
		}

		// Token: 0x17008BF2 RID: 35826
		// (get) Token: 0x060196D8 RID: 104152 RVA: 0x00349D41 File Offset: 0x00347F41
		// (set) Token: 0x060196D9 RID: 104153 RVA: 0x00349D4B File Offset: 0x00347F4B
		public VerticalTextAlignment VerticalTextAlignment
		{
			get
			{
				return base.GetElement<VerticalTextAlignment>(31);
			}
			set
			{
				base.SetElement<VerticalTextAlignment>(31, value);
			}
		}

		// Token: 0x17008BF3 RID: 35827
		// (get) Token: 0x060196DA RID: 104154 RVA: 0x00349D56 File Offset: 0x00347F56
		// (set) Token: 0x060196DB RID: 104155 RVA: 0x00349D60 File Offset: 0x00347F60
		public RightToLeftText RightToLeftText
		{
			get
			{
				return base.GetElement<RightToLeftText>(32);
			}
			set
			{
				base.SetElement<RightToLeftText>(32, value);
			}
		}

		// Token: 0x17008BF4 RID: 35828
		// (get) Token: 0x060196DC RID: 104156 RVA: 0x00349D6B File Offset: 0x00347F6B
		// (set) Token: 0x060196DD RID: 104157 RVA: 0x00349D75 File Offset: 0x00347F75
		public ComplexScript ComplexScript
		{
			get
			{
				return base.GetElement<ComplexScript>(33);
			}
			set
			{
				base.SetElement<ComplexScript>(33, value);
			}
		}

		// Token: 0x17008BF5 RID: 35829
		// (get) Token: 0x060196DE RID: 104158 RVA: 0x00349D80 File Offset: 0x00347F80
		// (set) Token: 0x060196DF RID: 104159 RVA: 0x00349D8A File Offset: 0x00347F8A
		public Emphasis Emphasis
		{
			get
			{
				return base.GetElement<Emphasis>(34);
			}
			set
			{
				base.SetElement<Emphasis>(34, value);
			}
		}

		// Token: 0x17008BF6 RID: 35830
		// (get) Token: 0x060196E0 RID: 104160 RVA: 0x00349D95 File Offset: 0x00347F95
		// (set) Token: 0x060196E1 RID: 104161 RVA: 0x00349D9F File Offset: 0x00347F9F
		public Languages Languages
		{
			get
			{
				return base.GetElement<Languages>(35);
			}
			set
			{
				base.SetElement<Languages>(35, value);
			}
		}

		// Token: 0x17008BF7 RID: 35831
		// (get) Token: 0x060196E2 RID: 104162 RVA: 0x00349DAA File Offset: 0x00347FAA
		// (set) Token: 0x060196E3 RID: 104163 RVA: 0x00349DB4 File Offset: 0x00347FB4
		public EastAsianLayout EastAsianLayout
		{
			get
			{
				return base.GetElement<EastAsianLayout>(36);
			}
			set
			{
				base.SetElement<EastAsianLayout>(36, value);
			}
		}

		// Token: 0x17008BF8 RID: 35832
		// (get) Token: 0x060196E4 RID: 104164 RVA: 0x00349DBF File Offset: 0x00347FBF
		// (set) Token: 0x060196E5 RID: 104165 RVA: 0x00349DC9 File Offset: 0x00347FC9
		public SpecVanish SpecVanish
		{
			get
			{
				return base.GetElement<SpecVanish>(37);
			}
			set
			{
				base.SetElement<SpecVanish>(37, value);
			}
		}

		// Token: 0x17008BF9 RID: 35833
		// (get) Token: 0x060196E6 RID: 104166 RVA: 0x00349DD4 File Offset: 0x00347FD4
		// (set) Token: 0x060196E7 RID: 104167 RVA: 0x00349DDE File Offset: 0x00347FDE
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Glow Glow
		{
			get
			{
				return base.GetElement<Glow>(38);
			}
			set
			{
				base.SetElement<Glow>(38, value);
			}
		}

		// Token: 0x17008BFA RID: 35834
		// (get) Token: 0x060196E8 RID: 104168 RVA: 0x00349DE9 File Offset: 0x00347FE9
		// (set) Token: 0x060196E9 RID: 104169 RVA: 0x00349DF3 File Offset: 0x00347FF3
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Shadow Shadow14
		{
			get
			{
				return base.GetElement<Shadow>(39);
			}
			set
			{
				base.SetElement<Shadow>(39, value);
			}
		}

		// Token: 0x17008BFB RID: 35835
		// (get) Token: 0x060196EA RID: 104170 RVA: 0x00349DFE File Offset: 0x00347FFE
		// (set) Token: 0x060196EB RID: 104171 RVA: 0x00349E08 File Offset: 0x00348008
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Reflection Reflection
		{
			get
			{
				return base.GetElement<Reflection>(40);
			}
			set
			{
				base.SetElement<Reflection>(40, value);
			}
		}

		// Token: 0x17008BFC RID: 35836
		// (get) Token: 0x060196EC RID: 104172 RVA: 0x00349E13 File Offset: 0x00348013
		// (set) Token: 0x060196ED RID: 104173 RVA: 0x00349E1D File Offset: 0x0034801D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public TextOutlineEffect TextOutlineEffect
		{
			get
			{
				return base.GetElement<TextOutlineEffect>(41);
			}
			set
			{
				base.SetElement<TextOutlineEffect>(41, value);
			}
		}

		// Token: 0x17008BFD RID: 35837
		// (get) Token: 0x060196EE RID: 104174 RVA: 0x00349E28 File Offset: 0x00348028
		// (set) Token: 0x060196EF RID: 104175 RVA: 0x00349E32 File Offset: 0x00348032
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public FillTextEffect FillTextEffect
		{
			get
			{
				return base.GetElement<FillTextEffect>(42);
			}
			set
			{
				base.SetElement<FillTextEffect>(42, value);
			}
		}

		// Token: 0x17008BFE RID: 35838
		// (get) Token: 0x060196F0 RID: 104176 RVA: 0x00349E3D File Offset: 0x0034803D
		// (set) Token: 0x060196F1 RID: 104177 RVA: 0x00349E47 File Offset: 0x00348047
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Scene3D Scene3D
		{
			get
			{
				return base.GetElement<Scene3D>(43);
			}
			set
			{
				base.SetElement<Scene3D>(43, value);
			}
		}

		// Token: 0x17008BFF RID: 35839
		// (get) Token: 0x060196F2 RID: 104178 RVA: 0x00349E52 File Offset: 0x00348052
		// (set) Token: 0x060196F3 RID: 104179 RVA: 0x00349E5C File Offset: 0x0034805C
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Properties3D Properties3D
		{
			get
			{
				return base.GetElement<Properties3D>(44);
			}
			set
			{
				base.SetElement<Properties3D>(44, value);
			}
		}

		// Token: 0x17008C00 RID: 35840
		// (get) Token: 0x060196F4 RID: 104180 RVA: 0x00349E67 File Offset: 0x00348067
		// (set) Token: 0x060196F5 RID: 104181 RVA: 0x00349E71 File Offset: 0x00348071
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Ligatures Ligatures
		{
			get
			{
				return base.GetElement<Ligatures>(45);
			}
			set
			{
				base.SetElement<Ligatures>(45, value);
			}
		}

		// Token: 0x17008C01 RID: 35841
		// (get) Token: 0x060196F6 RID: 104182 RVA: 0x00349E7C File Offset: 0x0034807C
		// (set) Token: 0x060196F7 RID: 104183 RVA: 0x00349E86 File Offset: 0x00348086
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public NumberingFormat NumberingFormat
		{
			get
			{
				return base.GetElement<NumberingFormat>(46);
			}
			set
			{
				base.SetElement<NumberingFormat>(46, value);
			}
		}

		// Token: 0x17008C02 RID: 35842
		// (get) Token: 0x060196F8 RID: 104184 RVA: 0x00349E91 File Offset: 0x00348091
		// (set) Token: 0x060196F9 RID: 104185 RVA: 0x00349E9B File Offset: 0x0034809B
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public NumberSpacing NumberSpacing
		{
			get
			{
				return base.GetElement<NumberSpacing>(47);
			}
			set
			{
				base.SetElement<NumberSpacing>(47, value);
			}
		}

		// Token: 0x17008C03 RID: 35843
		// (get) Token: 0x060196FA RID: 104186 RVA: 0x00349EA6 File Offset: 0x003480A6
		// (set) Token: 0x060196FB RID: 104187 RVA: 0x00349EB0 File Offset: 0x003480B0
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StylisticSets StylisticSets
		{
			get
			{
				return base.GetElement<StylisticSets>(48);
			}
			set
			{
				base.SetElement<StylisticSets>(48, value);
			}
		}

		// Token: 0x17008C04 RID: 35844
		// (get) Token: 0x060196FC RID: 104188 RVA: 0x00349EBB File Offset: 0x003480BB
		// (set) Token: 0x060196FD RID: 104189 RVA: 0x00349EC5 File Offset: 0x003480C5
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public ContextualAlternatives ContextualAlternatives
		{
			get
			{
				return base.GetElement<ContextualAlternatives>(49);
			}
			set
			{
				base.SetElement<ContextualAlternatives>(49, value);
			}
		}

		// Token: 0x17008C05 RID: 35845
		// (get) Token: 0x060196FE RID: 104190 RVA: 0x00349ED0 File Offset: 0x003480D0
		// (set) Token: 0x060196FF RID: 104191 RVA: 0x00349EDA File Offset: 0x003480DA
		public RunPropertiesChange RunPropertiesChange
		{
			get
			{
				return base.GetElement<RunPropertiesChange>(50);
			}
			set
			{
				base.SetElement<RunPropertiesChange>(50, value);
			}
		}

		// Token: 0x06019700 RID: 104192 RVA: 0x00349EE5 File Offset: 0x003480E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunProperties>(deep);
		}

		// Token: 0x0400A8D7 RID: 43223
		private const string tagName = "rPr";

		// Token: 0x0400A8D8 RID: 43224
		private const byte tagNsId = 23;

		// Token: 0x0400A8D9 RID: 43225
		internal const int ElementTypeIdConst = 11613;

		// Token: 0x0400A8DA RID: 43226
		private static readonly string[] eleTagNames = new string[]
		{
			"rStyle", "rFonts", "b", "bCs", "i", "iCs", "caps", "smallCaps", "strike", "dstrike",
			"outline", "shadow", "emboss", "imprint", "noProof", "snapToGrid", "vanish", "webHidden", "color", "spacing",
			"w", "kern", "position", "sz", "szCs", "highlight", "u", "effect", "bdr", "shd",
			"fitText", "vertAlign", "rtl", "cs", "em", "lang", "eastAsianLayout", "specVanish", "glow", "shadow",
			"reflection", "textOutline", "textFill", "scene3d", "props3d", "ligatures", "numForm", "numSpacing", "stylisticSets", "cntxtAlts",
			"rPrChange"
		};

		// Token: 0x0400A8DB RID: 43227
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 52, 52,
			52, 52, 52, 52, 52, 52, 52, 52, 52, 52,
			23
		};
	}
}
