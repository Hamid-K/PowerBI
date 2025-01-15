using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F9D RID: 12189
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StartNumberingValue))]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(LevelRestart))]
	[ChildElementInfo(typeof(ParagraphStyleIdInLevel))]
	[ChildElementInfo(typeof(IsLegalNumberingStyle))]
	[ChildElementInfo(typeof(LevelSuffix))]
	[ChildElementInfo(typeof(LevelText))]
	[ChildElementInfo(typeof(LevelPictureBulletId))]
	[ChildElementInfo(typeof(LegacyNumbering))]
	[ChildElementInfo(typeof(LevelJustification))]
	[ChildElementInfo(typeof(PreviousParagraphProperties))]
	[ChildElementInfo(typeof(NumberingSymbolRunProperties))]
	internal class Level : OpenXmlCompositeElement
	{
		// Token: 0x1700925F RID: 37471
		// (get) Token: 0x0601A505 RID: 107781 RVA: 0x002F3AB9 File Offset: 0x002F1CB9
		public override string LocalName
		{
			get
			{
				return "lvl";
			}
		}

		// Token: 0x17009260 RID: 37472
		// (get) Token: 0x0601A506 RID: 107782 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009261 RID: 37473
		// (get) Token: 0x0601A507 RID: 107783 RVA: 0x00360754 File Offset: 0x0035E954
		internal override int ElementTypeId
		{
			get
			{
				return 11880;
			}
		}

		// Token: 0x0601A508 RID: 107784 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009262 RID: 37474
		// (get) Token: 0x0601A509 RID: 107785 RVA: 0x0036075B File Offset: 0x0035E95B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Level.attributeTagNames;
			}
		}

		// Token: 0x17009263 RID: 37475
		// (get) Token: 0x0601A50A RID: 107786 RVA: 0x00360762 File Offset: 0x0035E962
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Level.attributeNamespaceIds;
			}
		}

		// Token: 0x17009264 RID: 37476
		// (get) Token: 0x0601A50B RID: 107787 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A50C RID: 107788 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "ilvl")]
		public Int32Value LevelIndex
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009265 RID: 37477
		// (get) Token: 0x0601A50D RID: 107789 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x0601A50E RID: 107790 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "tplc")]
		public HexBinaryValue TemplateCode
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009266 RID: 37478
		// (get) Token: 0x0601A50F RID: 107791 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601A510 RID: 107792 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "tentative")]
		public OnOffValue Tentative
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601A511 RID: 107793 RVA: 0x00293ECF File Offset: 0x002920CF
		public Level()
		{
		}

		// Token: 0x0601A512 RID: 107794 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Level(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A513 RID: 107795 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Level(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A514 RID: 107796 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Level(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A515 RID: 107797 RVA: 0x0036076C File Offset: 0x0035E96C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "start" == name)
			{
				return new StartNumberingValue();
			}
			if (23 == namespaceId && "numFmt" == name)
			{
				return new NumberingFormat();
			}
			if (23 == namespaceId && "lvlRestart" == name)
			{
				return new LevelRestart();
			}
			if (23 == namespaceId && "pStyle" == name)
			{
				return new ParagraphStyleIdInLevel();
			}
			if (23 == namespaceId && "isLgl" == name)
			{
				return new IsLegalNumberingStyle();
			}
			if (23 == namespaceId && "suff" == name)
			{
				return new LevelSuffix();
			}
			if (23 == namespaceId && "lvlText" == name)
			{
				return new LevelText();
			}
			if (23 == namespaceId && "lvlPicBulletId" == name)
			{
				return new LevelPictureBulletId();
			}
			if (23 == namespaceId && "legacy" == name)
			{
				return new LegacyNumbering();
			}
			if (23 == namespaceId && "lvlJc" == name)
			{
				return new LevelJustification();
			}
			if (23 == namespaceId && "pPr" == name)
			{
				return new PreviousParagraphProperties();
			}
			if (23 == namespaceId && "rPr" == name)
			{
				return new NumberingSymbolRunProperties();
			}
			return null;
		}

		// Token: 0x17009267 RID: 37479
		// (get) Token: 0x0601A516 RID: 107798 RVA: 0x0036089A File Offset: 0x0035EA9A
		internal override string[] ElementTagNames
		{
			get
			{
				return Level.eleTagNames;
			}
		}

		// Token: 0x17009268 RID: 37480
		// (get) Token: 0x0601A517 RID: 107799 RVA: 0x003608A1 File Offset: 0x0035EAA1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Level.eleNamespaceIds;
			}
		}

		// Token: 0x17009269 RID: 37481
		// (get) Token: 0x0601A518 RID: 107800 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700926A RID: 37482
		// (get) Token: 0x0601A519 RID: 107801 RVA: 0x003608A8 File Offset: 0x0035EAA8
		// (set) Token: 0x0601A51A RID: 107802 RVA: 0x003608B1 File Offset: 0x0035EAB1
		public StartNumberingValue StartNumberingValue
		{
			get
			{
				return base.GetElement<StartNumberingValue>(0);
			}
			set
			{
				base.SetElement<StartNumberingValue>(0, value);
			}
		}

		// Token: 0x1700926B RID: 37483
		// (get) Token: 0x0601A51B RID: 107803 RVA: 0x00346B9F File Offset: 0x00344D9F
		// (set) Token: 0x0601A51C RID: 107804 RVA: 0x00346BA8 File Offset: 0x00344DA8
		public NumberingFormat NumberingFormat
		{
			get
			{
				return base.GetElement<NumberingFormat>(1);
			}
			set
			{
				base.SetElement<NumberingFormat>(1, value);
			}
		}

		// Token: 0x1700926C RID: 37484
		// (get) Token: 0x0601A51D RID: 107805 RVA: 0x003608BB File Offset: 0x0035EABB
		// (set) Token: 0x0601A51E RID: 107806 RVA: 0x003608C4 File Offset: 0x0035EAC4
		public LevelRestart LevelRestart
		{
			get
			{
				return base.GetElement<LevelRestart>(2);
			}
			set
			{
				base.SetElement<LevelRestart>(2, value);
			}
		}

		// Token: 0x1700926D RID: 37485
		// (get) Token: 0x0601A51F RID: 107807 RVA: 0x003608CE File Offset: 0x0035EACE
		// (set) Token: 0x0601A520 RID: 107808 RVA: 0x003608D7 File Offset: 0x0035EAD7
		public ParagraphStyleIdInLevel ParagraphStyleIdInLevel
		{
			get
			{
				return base.GetElement<ParagraphStyleIdInLevel>(3);
			}
			set
			{
				base.SetElement<ParagraphStyleIdInLevel>(3, value);
			}
		}

		// Token: 0x1700926E RID: 37486
		// (get) Token: 0x0601A521 RID: 107809 RVA: 0x003608E1 File Offset: 0x0035EAE1
		// (set) Token: 0x0601A522 RID: 107810 RVA: 0x003608EA File Offset: 0x0035EAEA
		public IsLegalNumberingStyle IsLegalNumberingStyle
		{
			get
			{
				return base.GetElement<IsLegalNumberingStyle>(4);
			}
			set
			{
				base.SetElement<IsLegalNumberingStyle>(4, value);
			}
		}

		// Token: 0x1700926F RID: 37487
		// (get) Token: 0x0601A523 RID: 107811 RVA: 0x003608F4 File Offset: 0x0035EAF4
		// (set) Token: 0x0601A524 RID: 107812 RVA: 0x003608FD File Offset: 0x0035EAFD
		public LevelSuffix LevelSuffix
		{
			get
			{
				return base.GetElement<LevelSuffix>(5);
			}
			set
			{
				base.SetElement<LevelSuffix>(5, value);
			}
		}

		// Token: 0x17009270 RID: 37488
		// (get) Token: 0x0601A525 RID: 107813 RVA: 0x00360907 File Offset: 0x0035EB07
		// (set) Token: 0x0601A526 RID: 107814 RVA: 0x00360910 File Offset: 0x0035EB10
		public LevelText LevelText
		{
			get
			{
				return base.GetElement<LevelText>(6);
			}
			set
			{
				base.SetElement<LevelText>(6, value);
			}
		}

		// Token: 0x17009271 RID: 37489
		// (get) Token: 0x0601A527 RID: 107815 RVA: 0x0036091A File Offset: 0x0035EB1A
		// (set) Token: 0x0601A528 RID: 107816 RVA: 0x00360923 File Offset: 0x0035EB23
		public LevelPictureBulletId LevelPictureBulletId
		{
			get
			{
				return base.GetElement<LevelPictureBulletId>(7);
			}
			set
			{
				base.SetElement<LevelPictureBulletId>(7, value);
			}
		}

		// Token: 0x17009272 RID: 37490
		// (get) Token: 0x0601A529 RID: 107817 RVA: 0x0036092D File Offset: 0x0035EB2D
		// (set) Token: 0x0601A52A RID: 107818 RVA: 0x00360936 File Offset: 0x0035EB36
		public LegacyNumbering LegacyNumbering
		{
			get
			{
				return base.GetElement<LegacyNumbering>(8);
			}
			set
			{
				base.SetElement<LegacyNumbering>(8, value);
			}
		}

		// Token: 0x17009273 RID: 37491
		// (get) Token: 0x0601A52B RID: 107819 RVA: 0x00360940 File Offset: 0x0035EB40
		// (set) Token: 0x0601A52C RID: 107820 RVA: 0x0036094A File Offset: 0x0035EB4A
		public LevelJustification LevelJustification
		{
			get
			{
				return base.GetElement<LevelJustification>(9);
			}
			set
			{
				base.SetElement<LevelJustification>(9, value);
			}
		}

		// Token: 0x17009274 RID: 37492
		// (get) Token: 0x0601A52D RID: 107821 RVA: 0x00360955 File Offset: 0x0035EB55
		// (set) Token: 0x0601A52E RID: 107822 RVA: 0x0036095F File Offset: 0x0035EB5F
		public PreviousParagraphProperties PreviousParagraphProperties
		{
			get
			{
				return base.GetElement<PreviousParagraphProperties>(10);
			}
			set
			{
				base.SetElement<PreviousParagraphProperties>(10, value);
			}
		}

		// Token: 0x17009275 RID: 37493
		// (get) Token: 0x0601A52F RID: 107823 RVA: 0x0036096A File Offset: 0x0035EB6A
		// (set) Token: 0x0601A530 RID: 107824 RVA: 0x00360974 File Offset: 0x0035EB74
		public NumberingSymbolRunProperties NumberingSymbolRunProperties
		{
			get
			{
				return base.GetElement<NumberingSymbolRunProperties>(11);
			}
			set
			{
				base.SetElement<NumberingSymbolRunProperties>(11, value);
			}
		}

		// Token: 0x0601A531 RID: 107825 RVA: 0x00360980 File Offset: 0x0035EB80
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "ilvl" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "tplc" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "tentative" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A532 RID: 107826 RVA: 0x003609DD File Offset: 0x0035EBDD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level>(deep);
		}

		// Token: 0x0400AC9A RID: 44186
		private const string tagName = "lvl";

		// Token: 0x0400AC9B RID: 44187
		private const byte tagNsId = 23;

		// Token: 0x0400AC9C RID: 44188
		internal const int ElementTypeIdConst = 11880;

		// Token: 0x0400AC9D RID: 44189
		private static string[] attributeTagNames = new string[] { "ilvl", "tplc", "tentative" };

		// Token: 0x0400AC9E RID: 44190
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400AC9F RID: 44191
		private static readonly string[] eleTagNames = new string[]
		{
			"start", "numFmt", "lvlRestart", "pStyle", "isLgl", "suff", "lvlText", "lvlPicBulletId", "legacy", "lvlJc",
			"pPr", "rPr"
		};

		// Token: 0x0400ACA0 RID: 44192
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23
		};
	}
}
