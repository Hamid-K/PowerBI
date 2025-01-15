using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F2E RID: 12078
	[ChildElementInfo(typeof(Deleted))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Inserted))]
	[ChildElementInfo(typeof(MoveFrom))]
	[ChildElementInfo(typeof(MoveTo))]
	[ChildElementInfo(typeof(ConflictInsertion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConflictDeletion), FileFormatVersions.Office2010)]
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
	[ChildElementInfo(typeof(EastAsianLayout))]
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
	[ChildElementInfo(typeof(OfficeMath))]
	[ChildElementInfo(typeof(ParagraphMarkRunPropertiesChange))]
	internal class ParagraphMarkRunProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008F57 RID: 36695
		// (get) Token: 0x06019E74 RID: 106100 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17008F58 RID: 36696
		// (get) Token: 0x06019E75 RID: 106101 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F59 RID: 36697
		// (get) Token: 0x06019E76 RID: 106102 RVA: 0x0035920C File Offset: 0x0035740C
		internal override int ElementTypeId
		{
			get
			{
				return 11722;
			}
		}

		// Token: 0x06019E77 RID: 106103 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019E78 RID: 106104 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphMarkRunProperties()
		{
		}

		// Token: 0x06019E79 RID: 106105 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphMarkRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E7A RID: 106106 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphMarkRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E7B RID: 106107 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphMarkRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019E7C RID: 106108 RVA: 0x00359214 File Offset: 0x00357414
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "ins" == name)
			{
				return new Inserted();
			}
			if (23 == namespaceId && "del" == name)
			{
				return new Deleted();
			}
			if (23 == namespaceId && "moveFrom" == name)
			{
				return new MoveFrom();
			}
			if (23 == namespaceId && "moveTo" == name)
			{
				return new MoveTo();
			}
			if (52 == namespaceId && "conflictIns" == name)
			{
				return new ConflictInsertion();
			}
			if (52 == namespaceId && "conflictDel" == name)
			{
				return new ConflictDeletion();
			}
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
			if (23 == namespaceId && "oMath" == name)
			{
				return new OfficeMath();
			}
			if (23 == namespaceId && "rPrChange" == name)
			{
				return new ParagraphMarkRunPropertiesChange();
			}
			return null;
		}

		// Token: 0x17008F5A RID: 36698
		// (get) Token: 0x06019E7D RID: 106109 RVA: 0x00359792 File Offset: 0x00357992
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphMarkRunProperties.eleTagNames;
			}
		}

		// Token: 0x17008F5B RID: 36699
		// (get) Token: 0x06019E7E RID: 106110 RVA: 0x00359799 File Offset: 0x00357999
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphMarkRunProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008F5C RID: 36700
		// (get) Token: 0x06019E7F RID: 106111 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008F5D RID: 36701
		// (get) Token: 0x06019E80 RID: 106112 RVA: 0x00358CD4 File Offset: 0x00356ED4
		// (set) Token: 0x06019E81 RID: 106113 RVA: 0x00358CDD File Offset: 0x00356EDD
		public Inserted Inserted
		{
			get
			{
				return base.GetElement<Inserted>(0);
			}
			set
			{
				base.SetElement<Inserted>(0, value);
			}
		}

		// Token: 0x17008F5E RID: 36702
		// (get) Token: 0x06019E82 RID: 106114 RVA: 0x00358CE7 File Offset: 0x00356EE7
		// (set) Token: 0x06019E83 RID: 106115 RVA: 0x00358CF0 File Offset: 0x00356EF0
		public Deleted Deleted
		{
			get
			{
				return base.GetElement<Deleted>(1);
			}
			set
			{
				base.SetElement<Deleted>(1, value);
			}
		}

		// Token: 0x17008F5F RID: 36703
		// (get) Token: 0x06019E84 RID: 106116 RVA: 0x00358CFA File Offset: 0x00356EFA
		// (set) Token: 0x06019E85 RID: 106117 RVA: 0x00358D03 File Offset: 0x00356F03
		public MoveFrom MoveFrom
		{
			get
			{
				return base.GetElement<MoveFrom>(2);
			}
			set
			{
				base.SetElement<MoveFrom>(2, value);
			}
		}

		// Token: 0x17008F60 RID: 36704
		// (get) Token: 0x06019E86 RID: 106118 RVA: 0x00358D0D File Offset: 0x00356F0D
		// (set) Token: 0x06019E87 RID: 106119 RVA: 0x00358D16 File Offset: 0x00356F16
		public MoveTo MoveTo
		{
			get
			{
				return base.GetElement<MoveTo>(3);
			}
			set
			{
				base.SetElement<MoveTo>(3, value);
			}
		}

		// Token: 0x06019E88 RID: 106120 RVA: 0x003597A0 File Offset: 0x003579A0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphMarkRunProperties>(deep);
		}

		// Token: 0x0400AAD0 RID: 43728
		private const string tagName = "rPr";

		// Token: 0x0400AAD1 RID: 43729
		private const byte tagNsId = 23;

		// Token: 0x0400AAD2 RID: 43730
		internal const int ElementTypeIdConst = 11722;

		// Token: 0x0400AAD3 RID: 43731
		private static readonly string[] eleTagNames = new string[]
		{
			"ins", "del", "moveFrom", "moveTo", "conflictIns", "conflictDel", "rStyle", "rFonts", "b", "bCs",
			"i", "iCs", "caps", "smallCaps", "strike", "dstrike", "outline", "shadow", "emboss", "imprint",
			"noProof", "snapToGrid", "vanish", "webHidden", "color", "spacing", "w", "kern", "position", "sz",
			"szCs", "highlight", "u", "effect", "bdr", "shd", "fitText", "vertAlign", "rtl", "cs",
			"em", "lang", "eastAsianLayout", "specVanish", "glow", "shadow", "reflection", "textOutline", "textFill", "scene3d",
			"props3d", "ligatures", "numForm", "numSpacing", "stylisticSets", "cntxtAlts", "oMath", "rPrChange"
		};

		// Token: 0x0400AAD4 RID: 43732
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 52, 52, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 52, 52, 52, 52, 52, 52,
			52, 52, 52, 52, 52, 52, 23, 23
		};
	}
}
