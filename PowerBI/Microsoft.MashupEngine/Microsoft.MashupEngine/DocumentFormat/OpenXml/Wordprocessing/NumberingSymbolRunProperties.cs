using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F9B RID: 12187
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(ComplexScript))]
	[ChildElementInfo(typeof(RightToLeftText))]
	[ChildElementInfo(typeof(Border))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(Bold))]
	[ChildElementInfo(typeof(BoldComplexScript))]
	[ChildElementInfo(typeof(Italic))]
	[ChildElementInfo(typeof(ItalicComplexScript))]
	[ChildElementInfo(typeof(Caps))]
	[ChildElementInfo(typeof(SmallCaps))]
	[ChildElementInfo(typeof(Strike))]
	[ChildElementInfo(typeof(DoubleStrike))]
	[ChildElementInfo(typeof(FontSize))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Emboss))]
	[ChildElementInfo(typeof(Imprint))]
	[ChildElementInfo(typeof(NoProof))]
	[ChildElementInfo(typeof(Vanish))]
	[ChildElementInfo(typeof(WebHidden))]
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(Spacing))]
	[ChildElementInfo(typeof(CharacterScale))]
	[ChildElementInfo(typeof(Kern))]
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(FontSizeComplexScript))]
	[ChildElementInfo(typeof(TextEffect))]
	[ChildElementInfo(typeof(SpecVanish))]
	[ChildElementInfo(typeof(FitText))]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(RunFonts))]
	[ChildElementInfo(typeof(Emphasis))]
	[ChildElementInfo(typeof(Languages))]
	[ChildElementInfo(typeof(EastAsianLayout))]
	internal class NumberingSymbolRunProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700922F RID: 37423
		// (get) Token: 0x0601A4A3 RID: 107683 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17009230 RID: 37424
		// (get) Token: 0x0601A4A4 RID: 107684 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009231 RID: 37425
		// (get) Token: 0x0601A4A5 RID: 107685 RVA: 0x00360150 File Offset: 0x0035E350
		internal override int ElementTypeId
		{
			get
			{
				return 11873;
			}
		}

		// Token: 0x0601A4A6 RID: 107686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A4A7 RID: 107687 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberingSymbolRunProperties()
		{
		}

		// Token: 0x0601A4A8 RID: 107688 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberingSymbolRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A4A9 RID: 107689 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberingSymbolRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A4AA RID: 107690 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberingSymbolRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A4AB RID: 107691 RVA: 0x00360158 File Offset: 0x0035E358
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
			return null;
		}

		// Token: 0x17009232 RID: 37426
		// (get) Token: 0x0601A4AC RID: 107692 RVA: 0x003604C6 File Offset: 0x0035E6C6
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberingSymbolRunProperties.eleTagNames;
			}
		}

		// Token: 0x17009233 RID: 37427
		// (get) Token: 0x0601A4AD RID: 107693 RVA: 0x003604CD File Offset: 0x0035E6CD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberingSymbolRunProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009234 RID: 37428
		// (get) Token: 0x0601A4AE RID: 107694 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009235 RID: 37429
		// (get) Token: 0x0601A4AF RID: 107695 RVA: 0x0035E598 File Offset: 0x0035C798
		// (set) Token: 0x0601A4B0 RID: 107696 RVA: 0x0035E5A1 File Offset: 0x0035C7A1
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

		// Token: 0x17009236 RID: 37430
		// (get) Token: 0x0601A4B1 RID: 107697 RVA: 0x0035E5AB File Offset: 0x0035C7AB
		// (set) Token: 0x0601A4B2 RID: 107698 RVA: 0x0035E5B4 File Offset: 0x0035C7B4
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

		// Token: 0x17009237 RID: 37431
		// (get) Token: 0x0601A4B3 RID: 107699 RVA: 0x0035E5BE File Offset: 0x0035C7BE
		// (set) Token: 0x0601A4B4 RID: 107700 RVA: 0x0035E5C7 File Offset: 0x0035C7C7
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

		// Token: 0x17009238 RID: 37432
		// (get) Token: 0x0601A4B5 RID: 107701 RVA: 0x0035E5D1 File Offset: 0x0035C7D1
		// (set) Token: 0x0601A4B6 RID: 107702 RVA: 0x0035E5DA File Offset: 0x0035C7DA
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

		// Token: 0x17009239 RID: 37433
		// (get) Token: 0x0601A4B7 RID: 107703 RVA: 0x0035E5E4 File Offset: 0x0035C7E4
		// (set) Token: 0x0601A4B8 RID: 107704 RVA: 0x0035E5ED File Offset: 0x0035C7ED
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

		// Token: 0x1700923A RID: 37434
		// (get) Token: 0x0601A4B9 RID: 107705 RVA: 0x0035E5F7 File Offset: 0x0035C7F7
		// (set) Token: 0x0601A4BA RID: 107706 RVA: 0x0035E600 File Offset: 0x0035C800
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

		// Token: 0x1700923B RID: 37435
		// (get) Token: 0x0601A4BB RID: 107707 RVA: 0x0035E60A File Offset: 0x0035C80A
		// (set) Token: 0x0601A4BC RID: 107708 RVA: 0x0035E613 File Offset: 0x0035C813
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

		// Token: 0x1700923C RID: 37436
		// (get) Token: 0x0601A4BD RID: 107709 RVA: 0x0035E61D File Offset: 0x0035C81D
		// (set) Token: 0x0601A4BE RID: 107710 RVA: 0x0035E626 File Offset: 0x0035C826
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

		// Token: 0x1700923D RID: 37437
		// (get) Token: 0x0601A4BF RID: 107711 RVA: 0x0035E630 File Offset: 0x0035C830
		// (set) Token: 0x0601A4C0 RID: 107712 RVA: 0x0035E639 File Offset: 0x0035C839
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

		// Token: 0x1700923E RID: 37438
		// (get) Token: 0x0601A4C1 RID: 107713 RVA: 0x0035E643 File Offset: 0x0035C843
		// (set) Token: 0x0601A4C2 RID: 107714 RVA: 0x0035E64D File Offset: 0x0035C84D
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

		// Token: 0x1700923F RID: 37439
		// (get) Token: 0x0601A4C3 RID: 107715 RVA: 0x0035E658 File Offset: 0x0035C858
		// (set) Token: 0x0601A4C4 RID: 107716 RVA: 0x0035E662 File Offset: 0x0035C862
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

		// Token: 0x17009240 RID: 37440
		// (get) Token: 0x0601A4C5 RID: 107717 RVA: 0x0035E66D File Offset: 0x0035C86D
		// (set) Token: 0x0601A4C6 RID: 107718 RVA: 0x0035E677 File Offset: 0x0035C877
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

		// Token: 0x17009241 RID: 37441
		// (get) Token: 0x0601A4C7 RID: 107719 RVA: 0x0035E682 File Offset: 0x0035C882
		// (set) Token: 0x0601A4C8 RID: 107720 RVA: 0x0035E68C File Offset: 0x0035C88C
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

		// Token: 0x17009242 RID: 37442
		// (get) Token: 0x0601A4C9 RID: 107721 RVA: 0x0035E697 File Offset: 0x0035C897
		// (set) Token: 0x0601A4CA RID: 107722 RVA: 0x0035E6A1 File Offset: 0x0035C8A1
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

		// Token: 0x17009243 RID: 37443
		// (get) Token: 0x0601A4CB RID: 107723 RVA: 0x0035E6AC File Offset: 0x0035C8AC
		// (set) Token: 0x0601A4CC RID: 107724 RVA: 0x0035E6B6 File Offset: 0x0035C8B6
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

		// Token: 0x17009244 RID: 37444
		// (get) Token: 0x0601A4CD RID: 107725 RVA: 0x0035E6C1 File Offset: 0x0035C8C1
		// (set) Token: 0x0601A4CE RID: 107726 RVA: 0x0035E6CB File Offset: 0x0035C8CB
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

		// Token: 0x17009245 RID: 37445
		// (get) Token: 0x0601A4CF RID: 107727 RVA: 0x0035E6D6 File Offset: 0x0035C8D6
		// (set) Token: 0x0601A4D0 RID: 107728 RVA: 0x0035E6E0 File Offset: 0x0035C8E0
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

		// Token: 0x17009246 RID: 37446
		// (get) Token: 0x0601A4D1 RID: 107729 RVA: 0x0035E6EB File Offset: 0x0035C8EB
		// (set) Token: 0x0601A4D2 RID: 107730 RVA: 0x0035E6F5 File Offset: 0x0035C8F5
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

		// Token: 0x17009247 RID: 37447
		// (get) Token: 0x0601A4D3 RID: 107731 RVA: 0x0035E700 File Offset: 0x0035C900
		// (set) Token: 0x0601A4D4 RID: 107732 RVA: 0x0035E70A File Offset: 0x0035C90A
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

		// Token: 0x17009248 RID: 37448
		// (get) Token: 0x0601A4D5 RID: 107733 RVA: 0x0035E715 File Offset: 0x0035C915
		// (set) Token: 0x0601A4D6 RID: 107734 RVA: 0x0035E71F File Offset: 0x0035C91F
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

		// Token: 0x17009249 RID: 37449
		// (get) Token: 0x0601A4D7 RID: 107735 RVA: 0x0035E72A File Offset: 0x0035C92A
		// (set) Token: 0x0601A4D8 RID: 107736 RVA: 0x0035E734 File Offset: 0x0035C934
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

		// Token: 0x1700924A RID: 37450
		// (get) Token: 0x0601A4D9 RID: 107737 RVA: 0x0035E73F File Offset: 0x0035C93F
		// (set) Token: 0x0601A4DA RID: 107738 RVA: 0x0035E749 File Offset: 0x0035C949
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

		// Token: 0x1700924B RID: 37451
		// (get) Token: 0x0601A4DB RID: 107739 RVA: 0x0035E754 File Offset: 0x0035C954
		// (set) Token: 0x0601A4DC RID: 107740 RVA: 0x0035E75E File Offset: 0x0035C95E
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

		// Token: 0x1700924C RID: 37452
		// (get) Token: 0x0601A4DD RID: 107741 RVA: 0x0035E769 File Offset: 0x0035C969
		// (set) Token: 0x0601A4DE RID: 107742 RVA: 0x0035E773 File Offset: 0x0035C973
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

		// Token: 0x1700924D RID: 37453
		// (get) Token: 0x0601A4DF RID: 107743 RVA: 0x0035E77E File Offset: 0x0035C97E
		// (set) Token: 0x0601A4E0 RID: 107744 RVA: 0x0035E788 File Offset: 0x0035C988
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

		// Token: 0x1700924E RID: 37454
		// (get) Token: 0x0601A4E1 RID: 107745 RVA: 0x0035E793 File Offset: 0x0035C993
		// (set) Token: 0x0601A4E2 RID: 107746 RVA: 0x0035E79D File Offset: 0x0035C99D
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

		// Token: 0x1700924F RID: 37455
		// (get) Token: 0x0601A4E3 RID: 107747 RVA: 0x0035E7A8 File Offset: 0x0035C9A8
		// (set) Token: 0x0601A4E4 RID: 107748 RVA: 0x0035E7B2 File Offset: 0x0035C9B2
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

		// Token: 0x17009250 RID: 37456
		// (get) Token: 0x0601A4E5 RID: 107749 RVA: 0x0035E7BD File Offset: 0x0035C9BD
		// (set) Token: 0x0601A4E6 RID: 107750 RVA: 0x0035E7C7 File Offset: 0x0035C9C7
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

		// Token: 0x17009251 RID: 37457
		// (get) Token: 0x0601A4E7 RID: 107751 RVA: 0x0035E7D2 File Offset: 0x0035C9D2
		// (set) Token: 0x0601A4E8 RID: 107752 RVA: 0x0035E7DC File Offset: 0x0035C9DC
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

		// Token: 0x17009252 RID: 37458
		// (get) Token: 0x0601A4E9 RID: 107753 RVA: 0x0035E7E7 File Offset: 0x0035C9E7
		// (set) Token: 0x0601A4EA RID: 107754 RVA: 0x0035E7F1 File Offset: 0x0035C9F1
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

		// Token: 0x17009253 RID: 37459
		// (get) Token: 0x0601A4EB RID: 107755 RVA: 0x003604D4 File Offset: 0x0035E6D4
		// (set) Token: 0x0601A4EC RID: 107756 RVA: 0x003604DE File Offset: 0x0035E6DE
		public RightToLeftText RightToLeftText
		{
			get
			{
				return base.GetElement<RightToLeftText>(30);
			}
			set
			{
				base.SetElement<RightToLeftText>(30, value);
			}
		}

		// Token: 0x17009254 RID: 37460
		// (get) Token: 0x0601A4ED RID: 107757 RVA: 0x003604E9 File Offset: 0x0035E6E9
		// (set) Token: 0x0601A4EE RID: 107758 RVA: 0x003604F3 File Offset: 0x0035E6F3
		public ComplexScript ComplexScript
		{
			get
			{
				return base.GetElement<ComplexScript>(31);
			}
			set
			{
				base.SetElement<ComplexScript>(31, value);
			}
		}

		// Token: 0x17009255 RID: 37461
		// (get) Token: 0x0601A4EF RID: 107759 RVA: 0x003604FE File Offset: 0x0035E6FE
		// (set) Token: 0x0601A4F0 RID: 107760 RVA: 0x00360508 File Offset: 0x0035E708
		public Emphasis Emphasis
		{
			get
			{
				return base.GetElement<Emphasis>(32);
			}
			set
			{
				base.SetElement<Emphasis>(32, value);
			}
		}

		// Token: 0x17009256 RID: 37462
		// (get) Token: 0x0601A4F1 RID: 107761 RVA: 0x00360513 File Offset: 0x0035E713
		// (set) Token: 0x0601A4F2 RID: 107762 RVA: 0x0036051D File Offset: 0x0035E71D
		public Languages Languages
		{
			get
			{
				return base.GetElement<Languages>(33);
			}
			set
			{
				base.SetElement<Languages>(33, value);
			}
		}

		// Token: 0x17009257 RID: 37463
		// (get) Token: 0x0601A4F3 RID: 107763 RVA: 0x00360528 File Offset: 0x0035E728
		// (set) Token: 0x0601A4F4 RID: 107764 RVA: 0x00360532 File Offset: 0x0035E732
		public EastAsianLayout EastAsianLayout
		{
			get
			{
				return base.GetElement<EastAsianLayout>(34);
			}
			set
			{
				base.SetElement<EastAsianLayout>(34, value);
			}
		}

		// Token: 0x17009258 RID: 37464
		// (get) Token: 0x0601A4F5 RID: 107765 RVA: 0x0036053D File Offset: 0x0035E73D
		// (set) Token: 0x0601A4F6 RID: 107766 RVA: 0x00360547 File Offset: 0x0035E747
		public SpecVanish SpecVanish
		{
			get
			{
				return base.GetElement<SpecVanish>(35);
			}
			set
			{
				base.SetElement<SpecVanish>(35, value);
			}
		}

		// Token: 0x0601A4F7 RID: 107767 RVA: 0x00360552 File Offset: 0x0035E752
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingSymbolRunProperties>(deep);
		}

		// Token: 0x0400AC90 RID: 44176
		private const string tagName = "rPr";

		// Token: 0x0400AC91 RID: 44177
		private const byte tagNsId = 23;

		// Token: 0x0400AC92 RID: 44178
		internal const int ElementTypeIdConst = 11873;

		// Token: 0x0400AC93 RID: 44179
		private static readonly string[] eleTagNames = new string[]
		{
			"rFonts", "b", "bCs", "i", "iCs", "caps", "smallCaps", "strike", "dstrike", "outline",
			"shadow", "emboss", "imprint", "noProof", "snapToGrid", "vanish", "webHidden", "color", "spacing", "w",
			"kern", "position", "sz", "szCs", "u", "effect", "bdr", "shd", "fitText", "vertAlign",
			"rtl", "cs", "em", "lang", "eastAsianLayout", "specVanish"
		};

		// Token: 0x0400AC94 RID: 44180
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
