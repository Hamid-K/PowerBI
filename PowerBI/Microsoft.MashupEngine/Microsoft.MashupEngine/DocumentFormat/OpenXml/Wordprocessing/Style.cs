using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FAF RID: 12207
	[ChildElementInfo(typeof(Rsid))]
	[ChildElementInfo(typeof(StyleParagraphProperties))]
	[ChildElementInfo(typeof(StyleRunProperties))]
	[ChildElementInfo(typeof(UnhideWhenUsed))]
	[ChildElementInfo(typeof(PrimaryStyle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StyleName))]
	[ChildElementInfo(typeof(Aliases))]
	[ChildElementInfo(typeof(BasedOn))]
	[ChildElementInfo(typeof(NextParagraphStyle))]
	[ChildElementInfo(typeof(LinkedStyle))]
	[ChildElementInfo(typeof(AutoRedefine))]
	[ChildElementInfo(typeof(StyleHidden))]
	[ChildElementInfo(typeof(UIPriority))]
	[ChildElementInfo(typeof(SemiHidden))]
	[ChildElementInfo(typeof(Locked))]
	[ChildElementInfo(typeof(Personal))]
	[ChildElementInfo(typeof(PersonalCompose))]
	[ChildElementInfo(typeof(PersonalReply))]
	[ChildElementInfo(typeof(StyleTableProperties))]
	[ChildElementInfo(typeof(TableStyleConditionalFormattingTableRowProperties))]
	[ChildElementInfo(typeof(StyleTableCellProperties))]
	[ChildElementInfo(typeof(TableStyleProperties))]
	internal class Style : OpenXmlCompositeElement
	{
		// Token: 0x1700935C RID: 37724
		// (get) Token: 0x0601A71B RID: 108315 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x1700935D RID: 37725
		// (get) Token: 0x0601A71C RID: 108316 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700935E RID: 37726
		// (get) Token: 0x0601A71D RID: 108317 RVA: 0x00362548 File Offset: 0x00360748
		internal override int ElementTypeId
		{
			get
			{
				return 11914;
			}
		}

		// Token: 0x0601A71E RID: 108318 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700935F RID: 37727
		// (get) Token: 0x0601A71F RID: 108319 RVA: 0x0036254F File Offset: 0x0036074F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Style.attributeTagNames;
			}
		}

		// Token: 0x17009360 RID: 37728
		// (get) Token: 0x0601A720 RID: 108320 RVA: 0x00362556 File Offset: 0x00360756
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Style.attributeNamespaceIds;
			}
		}

		// Token: 0x17009361 RID: 37729
		// (get) Token: 0x0601A721 RID: 108321 RVA: 0x0036255D File Offset: 0x0036075D
		// (set) Token: 0x0601A722 RID: 108322 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<StyleValues> Type
		{
			get
			{
				return (EnumValue<StyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009362 RID: 37730
		// (get) Token: 0x0601A723 RID: 108323 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A724 RID: 108324 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "styleId")]
		public StringValue StyleId
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009363 RID: 37731
		// (get) Token: 0x0601A725 RID: 108325 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601A726 RID: 108326 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "default")]
		public OnOffValue Default
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

		// Token: 0x17009364 RID: 37732
		// (get) Token: 0x0601A727 RID: 108327 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601A728 RID: 108328 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "customStyle")]
		public OnOffValue CustomStyle
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601A729 RID: 108329 RVA: 0x00293ECF File Offset: 0x002920CF
		public Style()
		{
		}

		// Token: 0x0601A72A RID: 108330 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Style(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A72B RID: 108331 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Style(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A72C RID: 108332 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Style(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A72D RID: 108333 RVA: 0x0036256C File Offset: 0x0036076C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StyleName();
			}
			if (23 == namespaceId && "aliases" == name)
			{
				return new Aliases();
			}
			if (23 == namespaceId && "basedOn" == name)
			{
				return new BasedOn();
			}
			if (23 == namespaceId && "next" == name)
			{
				return new NextParagraphStyle();
			}
			if (23 == namespaceId && "link" == name)
			{
				return new LinkedStyle();
			}
			if (23 == namespaceId && "autoRedefine" == name)
			{
				return new AutoRedefine();
			}
			if (23 == namespaceId && "hidden" == name)
			{
				return new StyleHidden();
			}
			if (23 == namespaceId && "uiPriority" == name)
			{
				return new UIPriority();
			}
			if (23 == namespaceId && "semiHidden" == name)
			{
				return new SemiHidden();
			}
			if (23 == namespaceId && "unhideWhenUsed" == name)
			{
				return new UnhideWhenUsed();
			}
			if (23 == namespaceId && "qFormat" == name)
			{
				return new PrimaryStyle();
			}
			if (23 == namespaceId && "locked" == name)
			{
				return new Locked();
			}
			if (23 == namespaceId && "personal" == name)
			{
				return new Personal();
			}
			if (23 == namespaceId && "personalCompose" == name)
			{
				return new PersonalCompose();
			}
			if (23 == namespaceId && "personalReply" == name)
			{
				return new PersonalReply();
			}
			if (23 == namespaceId && "rsid" == name)
			{
				return new Rsid();
			}
			if (23 == namespaceId && "pPr" == name)
			{
				return new StyleParagraphProperties();
			}
			if (23 == namespaceId && "rPr" == name)
			{
				return new StyleRunProperties();
			}
			if (23 == namespaceId && "tblPr" == name)
			{
				return new StyleTableProperties();
			}
			if (23 == namespaceId && "trPr" == name)
			{
				return new TableStyleConditionalFormattingTableRowProperties();
			}
			if (23 == namespaceId && "tcPr" == name)
			{
				return new StyleTableCellProperties();
			}
			if (23 == namespaceId && "tblStylePr" == name)
			{
				return new TableStyleProperties();
			}
			return null;
		}

		// Token: 0x17009365 RID: 37733
		// (get) Token: 0x0601A72E RID: 108334 RVA: 0x0036278A File Offset: 0x0036098A
		internal override string[] ElementTagNames
		{
			get
			{
				return Style.eleTagNames;
			}
		}

		// Token: 0x17009366 RID: 37734
		// (get) Token: 0x0601A72F RID: 108335 RVA: 0x00362791 File Offset: 0x00360991
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Style.eleNamespaceIds;
			}
		}

		// Token: 0x17009367 RID: 37735
		// (get) Token: 0x0601A730 RID: 108336 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009368 RID: 37736
		// (get) Token: 0x0601A731 RID: 108337 RVA: 0x00362798 File Offset: 0x00360998
		// (set) Token: 0x0601A732 RID: 108338 RVA: 0x003627A1 File Offset: 0x003609A1
		public StyleName StyleName
		{
			get
			{
				return base.GetElement<StyleName>(0);
			}
			set
			{
				base.SetElement<StyleName>(0, value);
			}
		}

		// Token: 0x17009369 RID: 37737
		// (get) Token: 0x0601A733 RID: 108339 RVA: 0x003627AB File Offset: 0x003609AB
		// (set) Token: 0x0601A734 RID: 108340 RVA: 0x003627B4 File Offset: 0x003609B4
		public Aliases Aliases
		{
			get
			{
				return base.GetElement<Aliases>(1);
			}
			set
			{
				base.SetElement<Aliases>(1, value);
			}
		}

		// Token: 0x1700936A RID: 37738
		// (get) Token: 0x0601A735 RID: 108341 RVA: 0x003627BE File Offset: 0x003609BE
		// (set) Token: 0x0601A736 RID: 108342 RVA: 0x003627C7 File Offset: 0x003609C7
		public BasedOn BasedOn
		{
			get
			{
				return base.GetElement<BasedOn>(2);
			}
			set
			{
				base.SetElement<BasedOn>(2, value);
			}
		}

		// Token: 0x1700936B RID: 37739
		// (get) Token: 0x0601A737 RID: 108343 RVA: 0x003627D1 File Offset: 0x003609D1
		// (set) Token: 0x0601A738 RID: 108344 RVA: 0x003627DA File Offset: 0x003609DA
		public NextParagraphStyle NextParagraphStyle
		{
			get
			{
				return base.GetElement<NextParagraphStyle>(3);
			}
			set
			{
				base.SetElement<NextParagraphStyle>(3, value);
			}
		}

		// Token: 0x1700936C RID: 37740
		// (get) Token: 0x0601A739 RID: 108345 RVA: 0x003627E4 File Offset: 0x003609E4
		// (set) Token: 0x0601A73A RID: 108346 RVA: 0x003627ED File Offset: 0x003609ED
		public LinkedStyle LinkedStyle
		{
			get
			{
				return base.GetElement<LinkedStyle>(4);
			}
			set
			{
				base.SetElement<LinkedStyle>(4, value);
			}
		}

		// Token: 0x1700936D RID: 37741
		// (get) Token: 0x0601A73B RID: 108347 RVA: 0x003627F7 File Offset: 0x003609F7
		// (set) Token: 0x0601A73C RID: 108348 RVA: 0x00362800 File Offset: 0x00360A00
		public AutoRedefine AutoRedefine
		{
			get
			{
				return base.GetElement<AutoRedefine>(5);
			}
			set
			{
				base.SetElement<AutoRedefine>(5, value);
			}
		}

		// Token: 0x1700936E RID: 37742
		// (get) Token: 0x0601A73D RID: 108349 RVA: 0x0036280A File Offset: 0x00360A0A
		// (set) Token: 0x0601A73E RID: 108350 RVA: 0x00362813 File Offset: 0x00360A13
		public StyleHidden StyleHidden
		{
			get
			{
				return base.GetElement<StyleHidden>(6);
			}
			set
			{
				base.SetElement<StyleHidden>(6, value);
			}
		}

		// Token: 0x1700936F RID: 37743
		// (get) Token: 0x0601A73F RID: 108351 RVA: 0x0036281D File Offset: 0x00360A1D
		// (set) Token: 0x0601A740 RID: 108352 RVA: 0x00362826 File Offset: 0x00360A26
		public UIPriority UIPriority
		{
			get
			{
				return base.GetElement<UIPriority>(7);
			}
			set
			{
				base.SetElement<UIPriority>(7, value);
			}
		}

		// Token: 0x17009370 RID: 37744
		// (get) Token: 0x0601A741 RID: 108353 RVA: 0x00362830 File Offset: 0x00360A30
		// (set) Token: 0x0601A742 RID: 108354 RVA: 0x00362839 File Offset: 0x00360A39
		public SemiHidden SemiHidden
		{
			get
			{
				return base.GetElement<SemiHidden>(8);
			}
			set
			{
				base.SetElement<SemiHidden>(8, value);
			}
		}

		// Token: 0x17009371 RID: 37745
		// (get) Token: 0x0601A743 RID: 108355 RVA: 0x00362843 File Offset: 0x00360A43
		// (set) Token: 0x0601A744 RID: 108356 RVA: 0x0036284D File Offset: 0x00360A4D
		public UnhideWhenUsed UnhideWhenUsed
		{
			get
			{
				return base.GetElement<UnhideWhenUsed>(9);
			}
			set
			{
				base.SetElement<UnhideWhenUsed>(9, value);
			}
		}

		// Token: 0x17009372 RID: 37746
		// (get) Token: 0x0601A745 RID: 108357 RVA: 0x00362858 File Offset: 0x00360A58
		// (set) Token: 0x0601A746 RID: 108358 RVA: 0x00362862 File Offset: 0x00360A62
		public PrimaryStyle PrimaryStyle
		{
			get
			{
				return base.GetElement<PrimaryStyle>(10);
			}
			set
			{
				base.SetElement<PrimaryStyle>(10, value);
			}
		}

		// Token: 0x17009373 RID: 37747
		// (get) Token: 0x0601A747 RID: 108359 RVA: 0x0036286D File Offset: 0x00360A6D
		// (set) Token: 0x0601A748 RID: 108360 RVA: 0x00362877 File Offset: 0x00360A77
		public Locked Locked
		{
			get
			{
				return base.GetElement<Locked>(11);
			}
			set
			{
				base.SetElement<Locked>(11, value);
			}
		}

		// Token: 0x17009374 RID: 37748
		// (get) Token: 0x0601A749 RID: 108361 RVA: 0x00362882 File Offset: 0x00360A82
		// (set) Token: 0x0601A74A RID: 108362 RVA: 0x0036288C File Offset: 0x00360A8C
		public Personal Personal
		{
			get
			{
				return base.GetElement<Personal>(12);
			}
			set
			{
				base.SetElement<Personal>(12, value);
			}
		}

		// Token: 0x17009375 RID: 37749
		// (get) Token: 0x0601A74B RID: 108363 RVA: 0x00362897 File Offset: 0x00360A97
		// (set) Token: 0x0601A74C RID: 108364 RVA: 0x003628A1 File Offset: 0x00360AA1
		public PersonalCompose PersonalCompose
		{
			get
			{
				return base.GetElement<PersonalCompose>(13);
			}
			set
			{
				base.SetElement<PersonalCompose>(13, value);
			}
		}

		// Token: 0x17009376 RID: 37750
		// (get) Token: 0x0601A74D RID: 108365 RVA: 0x003628AC File Offset: 0x00360AAC
		// (set) Token: 0x0601A74E RID: 108366 RVA: 0x003628B6 File Offset: 0x00360AB6
		public PersonalReply PersonalReply
		{
			get
			{
				return base.GetElement<PersonalReply>(14);
			}
			set
			{
				base.SetElement<PersonalReply>(14, value);
			}
		}

		// Token: 0x17009377 RID: 37751
		// (get) Token: 0x0601A74F RID: 108367 RVA: 0x003628C1 File Offset: 0x00360AC1
		// (set) Token: 0x0601A750 RID: 108368 RVA: 0x003628CB File Offset: 0x00360ACB
		public Rsid Rsid
		{
			get
			{
				return base.GetElement<Rsid>(15);
			}
			set
			{
				base.SetElement<Rsid>(15, value);
			}
		}

		// Token: 0x17009378 RID: 37752
		// (get) Token: 0x0601A751 RID: 108369 RVA: 0x003628D6 File Offset: 0x00360AD6
		// (set) Token: 0x0601A752 RID: 108370 RVA: 0x003628E0 File Offset: 0x00360AE0
		public StyleParagraphProperties StyleParagraphProperties
		{
			get
			{
				return base.GetElement<StyleParagraphProperties>(16);
			}
			set
			{
				base.SetElement<StyleParagraphProperties>(16, value);
			}
		}

		// Token: 0x17009379 RID: 37753
		// (get) Token: 0x0601A753 RID: 108371 RVA: 0x003628EB File Offset: 0x00360AEB
		// (set) Token: 0x0601A754 RID: 108372 RVA: 0x003628F5 File Offset: 0x00360AF5
		public StyleRunProperties StyleRunProperties
		{
			get
			{
				return base.GetElement<StyleRunProperties>(17);
			}
			set
			{
				base.SetElement<StyleRunProperties>(17, value);
			}
		}

		// Token: 0x1700937A RID: 37754
		// (get) Token: 0x0601A755 RID: 108373 RVA: 0x00362900 File Offset: 0x00360B00
		// (set) Token: 0x0601A756 RID: 108374 RVA: 0x0036290A File Offset: 0x00360B0A
		public StyleTableProperties StyleTableProperties
		{
			get
			{
				return base.GetElement<StyleTableProperties>(18);
			}
			set
			{
				base.SetElement<StyleTableProperties>(18, value);
			}
		}

		// Token: 0x1700937B RID: 37755
		// (get) Token: 0x0601A757 RID: 108375 RVA: 0x00362915 File Offset: 0x00360B15
		// (set) Token: 0x0601A758 RID: 108376 RVA: 0x0036291F File Offset: 0x00360B1F
		public TableStyleConditionalFormattingTableRowProperties TableStyleConditionalFormattingTableRowProperties
		{
			get
			{
				return base.GetElement<TableStyleConditionalFormattingTableRowProperties>(19);
			}
			set
			{
				base.SetElement<TableStyleConditionalFormattingTableRowProperties>(19, value);
			}
		}

		// Token: 0x1700937C RID: 37756
		// (get) Token: 0x0601A759 RID: 108377 RVA: 0x0036292A File Offset: 0x00360B2A
		// (set) Token: 0x0601A75A RID: 108378 RVA: 0x00362934 File Offset: 0x00360B34
		public StyleTableCellProperties StyleTableCellProperties
		{
			get
			{
				return base.GetElement<StyleTableCellProperties>(20);
			}
			set
			{
				base.SetElement<StyleTableCellProperties>(20, value);
			}
		}

		// Token: 0x0601A75B RID: 108379 RVA: 0x00362940 File Offset: 0x00360B40
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<StyleValues>();
			}
			if (23 == namespaceId && "styleId" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "default" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "customStyle" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A75C RID: 108380 RVA: 0x003629B5 File Offset: 0x00360BB5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Style>(deep);
		}

		// Token: 0x0400ACFE RID: 44286
		private const string tagName = "style";

		// Token: 0x0400ACFF RID: 44287
		private const byte tagNsId = 23;

		// Token: 0x0400AD00 RID: 44288
		internal const int ElementTypeIdConst = 11914;

		// Token: 0x0400AD01 RID: 44289
		private static string[] attributeTagNames = new string[] { "type", "styleId", "default", "customStyle" };

		// Token: 0x0400AD02 RID: 44290
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };

		// Token: 0x0400AD03 RID: 44291
		private static readonly string[] eleTagNames = new string[]
		{
			"name", "aliases", "basedOn", "next", "link", "autoRedefine", "hidden", "uiPriority", "semiHidden", "unhideWhenUsed",
			"qFormat", "locked", "personal", "personalCompose", "personalReply", "rsid", "pPr", "rPr", "tblPr", "trPr",
			"tcPr", "tblStylePr"
		};

		// Token: 0x0400AD04 RID: 44292
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23
		};
	}
}
