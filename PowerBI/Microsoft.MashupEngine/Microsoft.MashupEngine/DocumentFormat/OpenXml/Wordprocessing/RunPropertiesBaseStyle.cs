using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F7D RID: 12157
	[ChildElementInfo(typeof(Caps))]
	[ChildElementInfo(typeof(WebHidden))]
	[ChildElementInfo(typeof(SpecVanish))]
	[ChildElementInfo(typeof(Bold))]
	[ChildElementInfo(typeof(BoldComplexScript))]
	[ChildElementInfo(typeof(Italic))]
	[ChildElementInfo(typeof(ItalicComplexScript))]
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(RunFonts))]
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(Spacing))]
	[ChildElementInfo(typeof(CharacterScale))]
	[ChildElementInfo(typeof(Kern))]
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(FontSize))]
	[ChildElementInfo(typeof(FontSizeComplexScript))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(TextEffect))]
	[ChildElementInfo(typeof(Border))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(FitText))]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(Emphasis))]
	[ChildElementInfo(typeof(Languages))]
	[ChildElementInfo(typeof(EastAsianLayout))]
	internal class RunPropertiesBaseStyle : OpenXmlCompositeElement
	{
		// Token: 0x17009135 RID: 37173
		// (get) Token: 0x0601A28E RID: 107150 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17009136 RID: 37174
		// (get) Token: 0x0601A28F RID: 107151 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009137 RID: 37175
		// (get) Token: 0x0601A290 RID: 107152 RVA: 0x0035E245 File Offset: 0x0035C445
		internal override int ElementTypeId
		{
			get
			{
				return 11831;
			}
		}

		// Token: 0x0601A291 RID: 107153 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A292 RID: 107154 RVA: 0x00293ECF File Offset: 0x002920CF
		public RunPropertiesBaseStyle()
		{
		}

		// Token: 0x0601A293 RID: 107155 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RunPropertiesBaseStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A294 RID: 107156 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RunPropertiesBaseStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A295 RID: 107157 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RunPropertiesBaseStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A296 RID: 107158 RVA: 0x0035E24C File Offset: 0x0035C44C
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
			return null;
		}

		// Token: 0x17009138 RID: 37176
		// (get) Token: 0x0601A297 RID: 107159 RVA: 0x0035E58A File Offset: 0x0035C78A
		internal override string[] ElementTagNames
		{
			get
			{
				return RunPropertiesBaseStyle.eleTagNames;
			}
		}

		// Token: 0x17009139 RID: 37177
		// (get) Token: 0x0601A298 RID: 107160 RVA: 0x0035E591 File Offset: 0x0035C791
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RunPropertiesBaseStyle.eleNamespaceIds;
			}
		}

		// Token: 0x1700913A RID: 37178
		// (get) Token: 0x0601A299 RID: 107161 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700913B RID: 37179
		// (get) Token: 0x0601A29A RID: 107162 RVA: 0x0035E598 File Offset: 0x0035C798
		// (set) Token: 0x0601A29B RID: 107163 RVA: 0x0035E5A1 File Offset: 0x0035C7A1
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

		// Token: 0x1700913C RID: 37180
		// (get) Token: 0x0601A29C RID: 107164 RVA: 0x0035E5AB File Offset: 0x0035C7AB
		// (set) Token: 0x0601A29D RID: 107165 RVA: 0x0035E5B4 File Offset: 0x0035C7B4
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

		// Token: 0x1700913D RID: 37181
		// (get) Token: 0x0601A29E RID: 107166 RVA: 0x0035E5BE File Offset: 0x0035C7BE
		// (set) Token: 0x0601A29F RID: 107167 RVA: 0x0035E5C7 File Offset: 0x0035C7C7
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

		// Token: 0x1700913E RID: 37182
		// (get) Token: 0x0601A2A0 RID: 107168 RVA: 0x0035E5D1 File Offset: 0x0035C7D1
		// (set) Token: 0x0601A2A1 RID: 107169 RVA: 0x0035E5DA File Offset: 0x0035C7DA
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

		// Token: 0x1700913F RID: 37183
		// (get) Token: 0x0601A2A2 RID: 107170 RVA: 0x0035E5E4 File Offset: 0x0035C7E4
		// (set) Token: 0x0601A2A3 RID: 107171 RVA: 0x0035E5ED File Offset: 0x0035C7ED
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

		// Token: 0x17009140 RID: 37184
		// (get) Token: 0x0601A2A4 RID: 107172 RVA: 0x0035E5F7 File Offset: 0x0035C7F7
		// (set) Token: 0x0601A2A5 RID: 107173 RVA: 0x0035E600 File Offset: 0x0035C800
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

		// Token: 0x17009141 RID: 37185
		// (get) Token: 0x0601A2A6 RID: 107174 RVA: 0x0035E60A File Offset: 0x0035C80A
		// (set) Token: 0x0601A2A7 RID: 107175 RVA: 0x0035E613 File Offset: 0x0035C813
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

		// Token: 0x17009142 RID: 37186
		// (get) Token: 0x0601A2A8 RID: 107176 RVA: 0x0035E61D File Offset: 0x0035C81D
		// (set) Token: 0x0601A2A9 RID: 107177 RVA: 0x0035E626 File Offset: 0x0035C826
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

		// Token: 0x17009143 RID: 37187
		// (get) Token: 0x0601A2AA RID: 107178 RVA: 0x0035E630 File Offset: 0x0035C830
		// (set) Token: 0x0601A2AB RID: 107179 RVA: 0x0035E639 File Offset: 0x0035C839
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

		// Token: 0x17009144 RID: 37188
		// (get) Token: 0x0601A2AC RID: 107180 RVA: 0x0035E643 File Offset: 0x0035C843
		// (set) Token: 0x0601A2AD RID: 107181 RVA: 0x0035E64D File Offset: 0x0035C84D
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

		// Token: 0x17009145 RID: 37189
		// (get) Token: 0x0601A2AE RID: 107182 RVA: 0x0035E658 File Offset: 0x0035C858
		// (set) Token: 0x0601A2AF RID: 107183 RVA: 0x0035E662 File Offset: 0x0035C862
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

		// Token: 0x17009146 RID: 37190
		// (get) Token: 0x0601A2B0 RID: 107184 RVA: 0x0035E66D File Offset: 0x0035C86D
		// (set) Token: 0x0601A2B1 RID: 107185 RVA: 0x0035E677 File Offset: 0x0035C877
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

		// Token: 0x17009147 RID: 37191
		// (get) Token: 0x0601A2B2 RID: 107186 RVA: 0x0035E682 File Offset: 0x0035C882
		// (set) Token: 0x0601A2B3 RID: 107187 RVA: 0x0035E68C File Offset: 0x0035C88C
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

		// Token: 0x17009148 RID: 37192
		// (get) Token: 0x0601A2B4 RID: 107188 RVA: 0x0035E697 File Offset: 0x0035C897
		// (set) Token: 0x0601A2B5 RID: 107189 RVA: 0x0035E6A1 File Offset: 0x0035C8A1
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

		// Token: 0x17009149 RID: 37193
		// (get) Token: 0x0601A2B6 RID: 107190 RVA: 0x0035E6AC File Offset: 0x0035C8AC
		// (set) Token: 0x0601A2B7 RID: 107191 RVA: 0x0035E6B6 File Offset: 0x0035C8B6
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

		// Token: 0x1700914A RID: 37194
		// (get) Token: 0x0601A2B8 RID: 107192 RVA: 0x0035E6C1 File Offset: 0x0035C8C1
		// (set) Token: 0x0601A2B9 RID: 107193 RVA: 0x0035E6CB File Offset: 0x0035C8CB
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

		// Token: 0x1700914B RID: 37195
		// (get) Token: 0x0601A2BA RID: 107194 RVA: 0x0035E6D6 File Offset: 0x0035C8D6
		// (set) Token: 0x0601A2BB RID: 107195 RVA: 0x0035E6E0 File Offset: 0x0035C8E0
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

		// Token: 0x1700914C RID: 37196
		// (get) Token: 0x0601A2BC RID: 107196 RVA: 0x0035E6EB File Offset: 0x0035C8EB
		// (set) Token: 0x0601A2BD RID: 107197 RVA: 0x0035E6F5 File Offset: 0x0035C8F5
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

		// Token: 0x1700914D RID: 37197
		// (get) Token: 0x0601A2BE RID: 107198 RVA: 0x0035E700 File Offset: 0x0035C900
		// (set) Token: 0x0601A2BF RID: 107199 RVA: 0x0035E70A File Offset: 0x0035C90A
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

		// Token: 0x1700914E RID: 37198
		// (get) Token: 0x0601A2C0 RID: 107200 RVA: 0x0035E715 File Offset: 0x0035C915
		// (set) Token: 0x0601A2C1 RID: 107201 RVA: 0x0035E71F File Offset: 0x0035C91F
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

		// Token: 0x1700914F RID: 37199
		// (get) Token: 0x0601A2C2 RID: 107202 RVA: 0x0035E72A File Offset: 0x0035C92A
		// (set) Token: 0x0601A2C3 RID: 107203 RVA: 0x0035E734 File Offset: 0x0035C934
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

		// Token: 0x17009150 RID: 37200
		// (get) Token: 0x0601A2C4 RID: 107204 RVA: 0x0035E73F File Offset: 0x0035C93F
		// (set) Token: 0x0601A2C5 RID: 107205 RVA: 0x0035E749 File Offset: 0x0035C949
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

		// Token: 0x17009151 RID: 37201
		// (get) Token: 0x0601A2C6 RID: 107206 RVA: 0x0035E754 File Offset: 0x0035C954
		// (set) Token: 0x0601A2C7 RID: 107207 RVA: 0x0035E75E File Offset: 0x0035C95E
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

		// Token: 0x17009152 RID: 37202
		// (get) Token: 0x0601A2C8 RID: 107208 RVA: 0x0035E769 File Offset: 0x0035C969
		// (set) Token: 0x0601A2C9 RID: 107209 RVA: 0x0035E773 File Offset: 0x0035C973
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

		// Token: 0x17009153 RID: 37203
		// (get) Token: 0x0601A2CA RID: 107210 RVA: 0x0035E77E File Offset: 0x0035C97E
		// (set) Token: 0x0601A2CB RID: 107211 RVA: 0x0035E788 File Offset: 0x0035C988
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

		// Token: 0x17009154 RID: 37204
		// (get) Token: 0x0601A2CC RID: 107212 RVA: 0x0035E793 File Offset: 0x0035C993
		// (set) Token: 0x0601A2CD RID: 107213 RVA: 0x0035E79D File Offset: 0x0035C99D
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

		// Token: 0x17009155 RID: 37205
		// (get) Token: 0x0601A2CE RID: 107214 RVA: 0x0035E7A8 File Offset: 0x0035C9A8
		// (set) Token: 0x0601A2CF RID: 107215 RVA: 0x0035E7B2 File Offset: 0x0035C9B2
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

		// Token: 0x17009156 RID: 37206
		// (get) Token: 0x0601A2D0 RID: 107216 RVA: 0x0035E7BD File Offset: 0x0035C9BD
		// (set) Token: 0x0601A2D1 RID: 107217 RVA: 0x0035E7C7 File Offset: 0x0035C9C7
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

		// Token: 0x17009157 RID: 37207
		// (get) Token: 0x0601A2D2 RID: 107218 RVA: 0x0035E7D2 File Offset: 0x0035C9D2
		// (set) Token: 0x0601A2D3 RID: 107219 RVA: 0x0035E7DC File Offset: 0x0035C9DC
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

		// Token: 0x17009158 RID: 37208
		// (get) Token: 0x0601A2D4 RID: 107220 RVA: 0x0035E7E7 File Offset: 0x0035C9E7
		// (set) Token: 0x0601A2D5 RID: 107221 RVA: 0x0035E7F1 File Offset: 0x0035C9F1
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

		// Token: 0x17009159 RID: 37209
		// (get) Token: 0x0601A2D6 RID: 107222 RVA: 0x0035E7FC File Offset: 0x0035C9FC
		// (set) Token: 0x0601A2D7 RID: 107223 RVA: 0x0035E806 File Offset: 0x0035CA06
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

		// Token: 0x1700915A RID: 37210
		// (get) Token: 0x0601A2D8 RID: 107224 RVA: 0x0035E811 File Offset: 0x0035CA11
		// (set) Token: 0x0601A2D9 RID: 107225 RVA: 0x0035E81B File Offset: 0x0035CA1B
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

		// Token: 0x1700915B RID: 37211
		// (get) Token: 0x0601A2DA RID: 107226 RVA: 0x0035E826 File Offset: 0x0035CA26
		// (set) Token: 0x0601A2DB RID: 107227 RVA: 0x0035E830 File Offset: 0x0035CA30
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

		// Token: 0x1700915C RID: 37212
		// (get) Token: 0x0601A2DC RID: 107228 RVA: 0x0035E83B File Offset: 0x0035CA3B
		// (set) Token: 0x0601A2DD RID: 107229 RVA: 0x0035E845 File Offset: 0x0035CA45
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

		// Token: 0x0601A2DE RID: 107230 RVA: 0x0035E850 File Offset: 0x0035CA50
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunPropertiesBaseStyle>(deep);
		}

		// Token: 0x0400AC1B RID: 44059
		private const string tagName = "rPr";

		// Token: 0x0400AC1C RID: 44060
		private const byte tagNsId = 23;

		// Token: 0x0400AC1D RID: 44061
		internal const int ElementTypeIdConst = 11831;

		// Token: 0x0400AC1E RID: 44062
		private static readonly string[] eleTagNames = new string[]
		{
			"rFonts", "b", "bCs", "i", "iCs", "caps", "smallCaps", "strike", "dstrike", "outline",
			"shadow", "emboss", "imprint", "noProof", "snapToGrid", "vanish", "webHidden", "color", "spacing", "w",
			"kern", "position", "sz", "szCs", "u", "effect", "bdr", "shd", "fitText", "vertAlign",
			"em", "lang", "eastAsianLayout", "specVanish"
		};

		// Token: 0x0400AC1F RID: 44063
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23
		};
	}
}
