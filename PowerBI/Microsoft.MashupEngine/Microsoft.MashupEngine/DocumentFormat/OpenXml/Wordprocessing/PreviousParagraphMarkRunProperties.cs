using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F26 RID: 12070
	[ChildElementInfo(typeof(MoveFrom))]
	[ChildElementInfo(typeof(Kern))]
	[ChildElementInfo(typeof(Position))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Inserted))]
	[ChildElementInfo(typeof(Deleted))]
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
	internal class PreviousParagraphMarkRunProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008F2A RID: 36650
		// (get) Token: 0x06019E17 RID: 106007 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17008F2B RID: 36651
		// (get) Token: 0x06019E18 RID: 106008 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F2C RID: 36652
		// (get) Token: 0x06019E19 RID: 106009 RVA: 0x00358757 File Offset: 0x00356957
		internal override int ElementTypeId
		{
			get
			{
				return 11711;
			}
		}

		// Token: 0x06019E1A RID: 106010 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019E1B RID: 106011 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousParagraphMarkRunProperties()
		{
		}

		// Token: 0x06019E1C RID: 106012 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousParagraphMarkRunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E1D RID: 106013 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousParagraphMarkRunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E1E RID: 106014 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousParagraphMarkRunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019E1F RID: 106015 RVA: 0x00358760 File Offset: 0x00356960
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
			return null;
		}

		// Token: 0x17008F2D RID: 36653
		// (get) Token: 0x06019E20 RID: 106016 RVA: 0x00358CC6 File Offset: 0x00356EC6
		internal override string[] ElementTagNames
		{
			get
			{
				return PreviousParagraphMarkRunProperties.eleTagNames;
			}
		}

		// Token: 0x17008F2E RID: 36654
		// (get) Token: 0x06019E21 RID: 106017 RVA: 0x00358CCD File Offset: 0x00356ECD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreviousParagraphMarkRunProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008F2F RID: 36655
		// (get) Token: 0x06019E22 RID: 106018 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008F30 RID: 36656
		// (get) Token: 0x06019E23 RID: 106019 RVA: 0x00358CD4 File Offset: 0x00356ED4
		// (set) Token: 0x06019E24 RID: 106020 RVA: 0x00358CDD File Offset: 0x00356EDD
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

		// Token: 0x17008F31 RID: 36657
		// (get) Token: 0x06019E25 RID: 106021 RVA: 0x00358CE7 File Offset: 0x00356EE7
		// (set) Token: 0x06019E26 RID: 106022 RVA: 0x00358CF0 File Offset: 0x00356EF0
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

		// Token: 0x17008F32 RID: 36658
		// (get) Token: 0x06019E27 RID: 106023 RVA: 0x00358CFA File Offset: 0x00356EFA
		// (set) Token: 0x06019E28 RID: 106024 RVA: 0x00358D03 File Offset: 0x00356F03
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

		// Token: 0x17008F33 RID: 36659
		// (get) Token: 0x06019E29 RID: 106025 RVA: 0x00358D0D File Offset: 0x00356F0D
		// (set) Token: 0x06019E2A RID: 106026 RVA: 0x00358D16 File Offset: 0x00356F16
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

		// Token: 0x06019E2B RID: 106027 RVA: 0x00358D20 File Offset: 0x00356F20
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousParagraphMarkRunProperties>(deep);
		}

		// Token: 0x0400AAB1 RID: 43697
		private const string tagName = "rPr";

		// Token: 0x0400AAB2 RID: 43698
		private const byte tagNsId = 23;

		// Token: 0x0400AAB3 RID: 43699
		internal const int ElementTypeIdConst = 11711;

		// Token: 0x0400AAB4 RID: 43700
		private static readonly string[] eleTagNames = new string[]
		{
			"ins", "del", "moveFrom", "moveTo", "conflictIns", "conflictDel", "rStyle", "rFonts", "b", "bCs",
			"i", "iCs", "caps", "smallCaps", "strike", "dstrike", "outline", "shadow", "emboss", "imprint",
			"noProof", "snapToGrid", "vanish", "webHidden", "color", "spacing", "w", "kern", "position", "sz",
			"szCs", "highlight", "u", "effect", "bdr", "shd", "fitText", "vertAlign", "rtl", "cs",
			"em", "lang", "eastAsianLayout", "specVanish", "glow", "shadow", "reflection", "textOutline", "textFill", "scene3d",
			"props3d", "ligatures", "numForm", "numSpacing", "stylisticSets", "cntxtAlts", "oMath"
		};

		// Token: 0x0400AAB5 RID: 43701
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 52, 52, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 52, 52, 52, 52, 52, 52,
			52, 52, 52, 52, 52, 52, 23
		};
	}
}
