using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F25 RID: 12069
	[ChildElementInfo(typeof(EastAsianLayout))]
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(WebHidden))]
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
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(FitText))]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(RightToLeftText))]
	[ChildElementInfo(typeof(ComplexScript))]
	[ChildElementInfo(typeof(Emphasis))]
	[ChildElementInfo(typeof(Languages))]
	[ChildElementInfo(typeof(SpecVanish))]
	[ChildElementInfo(typeof(Glow), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Shadow), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Reflection), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TextOutlineEffect), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FillTextEffect), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Scene3D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Properties3D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Ligatures), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NumberingFormat), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NumberSpacing), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(StylisticSets), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ContextualAlternatives), FileFormatVersions.Office2010)]
	internal class PreviousRunProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008F27 RID: 36647
		// (get) Token: 0x06019E0D RID: 105997 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17008F28 RID: 36648
		// (get) Token: 0x06019E0E RID: 105998 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F29 RID: 36649
		// (get) Token: 0x06019E0F RID: 105999 RVA: 0x00358286 File Offset: 0x00356486
		internal override int ElementTypeId
		{
			get
			{
				return 11710;
			}
		}

		// Token: 0x06019E10 RID: 106000 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019E11 RID: 106001 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousRunProperties()
		{
		}

		// Token: 0x06019E12 RID: 106002 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E13 RID: 106003 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E14 RID: 106004 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019E15 RID: 106005 RVA: 0x00358290 File Offset: 0x00356490
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
			return null;
		}

		// Token: 0x06019E16 RID: 106006 RVA: 0x0035874E File Offset: 0x0035694E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousRunProperties>(deep);
		}

		// Token: 0x0400AAAE RID: 43694
		private const string tagName = "rPr";

		// Token: 0x0400AAAF RID: 43695
		private const byte tagNsId = 23;

		// Token: 0x0400AAB0 RID: 43696
		internal const int ElementTypeIdConst = 11710;
	}
}
