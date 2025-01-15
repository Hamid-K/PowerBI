using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F23 RID: 12067
	[ChildElementInfo(typeof(EndnoteProperties))]
	[ChildElementInfo(typeof(FootnoteProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SectionType))]
	[ChildElementInfo(typeof(PageSize))]
	[ChildElementInfo(typeof(PageMargin))]
	[ChildElementInfo(typeof(PaperSource))]
	[ChildElementInfo(typeof(PageBorders))]
	[ChildElementInfo(typeof(LineNumberType))]
	[ChildElementInfo(typeof(PageNumberType))]
	[ChildElementInfo(typeof(Columns))]
	[ChildElementInfo(typeof(FormProtection))]
	[ChildElementInfo(typeof(VerticalTextAlignmentOnPage))]
	[ChildElementInfo(typeof(NoEndnote))]
	[ChildElementInfo(typeof(TitlePage))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(BiDi))]
	[ChildElementInfo(typeof(GutterOnRight))]
	[ChildElementInfo(typeof(DocGrid))]
	[ChildElementInfo(typeof(PrinterSettingsReference))]
	internal class PreviousSectionProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008EE1 RID: 36577
		// (get) Token: 0x06019D7E RID: 105854 RVA: 0x00357648 File Offset: 0x00355848
		public override string LocalName
		{
			get
			{
				return "sectPr";
			}
		}

		// Token: 0x17008EE2 RID: 36578
		// (get) Token: 0x06019D7F RID: 105855 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008EE3 RID: 36579
		// (get) Token: 0x06019D80 RID: 105856 RVA: 0x0035764F File Offset: 0x0035584F
		internal override int ElementTypeId
		{
			get
			{
				return 11708;
			}
		}

		// Token: 0x06019D81 RID: 105857 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008EE4 RID: 36580
		// (get) Token: 0x06019D82 RID: 105858 RVA: 0x00357656 File Offset: 0x00355856
		internal override string[] AttributeTagNames
		{
			get
			{
				return PreviousSectionProperties.attributeTagNames;
			}
		}

		// Token: 0x17008EE5 RID: 36581
		// (get) Token: 0x06019D83 RID: 105859 RVA: 0x0035765D File Offset: 0x0035585D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PreviousSectionProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008EE6 RID: 36582
		// (get) Token: 0x06019D84 RID: 105860 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06019D85 RID: 105861 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "rsidRPr")]
		public HexBinaryValue RsidRPr
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008EE7 RID: 36583
		// (get) Token: 0x06019D86 RID: 105862 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06019D87 RID: 105863 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "rsidDel")]
		public HexBinaryValue RsidDel
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

		// Token: 0x17008EE8 RID: 36584
		// (get) Token: 0x06019D88 RID: 105864 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x06019D89 RID: 105865 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "rsidR")]
		public HexBinaryValue RsidR
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008EE9 RID: 36585
		// (get) Token: 0x06019D8A RID: 105866 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x06019D8B RID: 105867 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "rsidSect")]
		public HexBinaryValue RsidSect
		{
			get
			{
				return (HexBinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06019D8C RID: 105868 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousSectionProperties()
		{
		}

		// Token: 0x06019D8D RID: 105869 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousSectionProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D8E RID: 105870 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousSectionProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D8F RID: 105871 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousSectionProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019D90 RID: 105872 RVA: 0x00357664 File Offset: 0x00355864
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "footnotePr" == name)
			{
				return new FootnoteProperties();
			}
			if (23 == namespaceId && "endnotePr" == name)
			{
				return new EndnoteProperties();
			}
			if (23 == namespaceId && "type" == name)
			{
				return new SectionType();
			}
			if (23 == namespaceId && "pgSz" == name)
			{
				return new PageSize();
			}
			if (23 == namespaceId && "pgMar" == name)
			{
				return new PageMargin();
			}
			if (23 == namespaceId && "paperSrc" == name)
			{
				return new PaperSource();
			}
			if (23 == namespaceId && "pgBorders" == name)
			{
				return new PageBorders();
			}
			if (23 == namespaceId && "lnNumType" == name)
			{
				return new LineNumberType();
			}
			if (23 == namespaceId && "pgNumType" == name)
			{
				return new PageNumberType();
			}
			if (23 == namespaceId && "cols" == name)
			{
				return new Columns();
			}
			if (23 == namespaceId && "formProt" == name)
			{
				return new FormProtection();
			}
			if (23 == namespaceId && "vAlign" == name)
			{
				return new VerticalTextAlignmentOnPage();
			}
			if (23 == namespaceId && "noEndnote" == name)
			{
				return new NoEndnote();
			}
			if (23 == namespaceId && "titlePg" == name)
			{
				return new TitlePage();
			}
			if (23 == namespaceId && "textDirection" == name)
			{
				return new TextDirection();
			}
			if (23 == namespaceId && "bidi" == name)
			{
				return new BiDi();
			}
			if (23 == namespaceId && "rtlGutter" == name)
			{
				return new GutterOnRight();
			}
			if (23 == namespaceId && "docGrid" == name)
			{
				return new DocGrid();
			}
			if (23 == namespaceId && "printerSettings" == name)
			{
				return new PrinterSettingsReference();
			}
			return null;
		}

		// Token: 0x17008EEA RID: 36586
		// (get) Token: 0x06019D91 RID: 105873 RVA: 0x0035783A File Offset: 0x00355A3A
		internal override string[] ElementTagNames
		{
			get
			{
				return PreviousSectionProperties.eleTagNames;
			}
		}

		// Token: 0x17008EEB RID: 36587
		// (get) Token: 0x06019D92 RID: 105874 RVA: 0x00357841 File Offset: 0x00355A41
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreviousSectionProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008EEC RID: 36588
		// (get) Token: 0x06019D93 RID: 105875 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008EED RID: 36589
		// (get) Token: 0x06019D94 RID: 105876 RVA: 0x00357848 File Offset: 0x00355A48
		// (set) Token: 0x06019D95 RID: 105877 RVA: 0x00357851 File Offset: 0x00355A51
		public FootnoteProperties FootnoteProperties
		{
			get
			{
				return base.GetElement<FootnoteProperties>(0);
			}
			set
			{
				base.SetElement<FootnoteProperties>(0, value);
			}
		}

		// Token: 0x17008EEE RID: 36590
		// (get) Token: 0x06019D96 RID: 105878 RVA: 0x0035785B File Offset: 0x00355A5B
		// (set) Token: 0x06019D97 RID: 105879 RVA: 0x00357864 File Offset: 0x00355A64
		public EndnoteProperties EndnoteProperties
		{
			get
			{
				return base.GetElement<EndnoteProperties>(1);
			}
			set
			{
				base.SetElement<EndnoteProperties>(1, value);
			}
		}

		// Token: 0x17008EEF RID: 36591
		// (get) Token: 0x06019D98 RID: 105880 RVA: 0x0035786E File Offset: 0x00355A6E
		// (set) Token: 0x06019D99 RID: 105881 RVA: 0x00357877 File Offset: 0x00355A77
		public SectionType SectionType
		{
			get
			{
				return base.GetElement<SectionType>(2);
			}
			set
			{
				base.SetElement<SectionType>(2, value);
			}
		}

		// Token: 0x17008EF0 RID: 36592
		// (get) Token: 0x06019D9A RID: 105882 RVA: 0x00357881 File Offset: 0x00355A81
		// (set) Token: 0x06019D9B RID: 105883 RVA: 0x0035788A File Offset: 0x00355A8A
		public PageSize PageSize
		{
			get
			{
				return base.GetElement<PageSize>(3);
			}
			set
			{
				base.SetElement<PageSize>(3, value);
			}
		}

		// Token: 0x17008EF1 RID: 36593
		// (get) Token: 0x06019D9C RID: 105884 RVA: 0x00357894 File Offset: 0x00355A94
		// (set) Token: 0x06019D9D RID: 105885 RVA: 0x0035789D File Offset: 0x00355A9D
		public PageMargin PageMargin
		{
			get
			{
				return base.GetElement<PageMargin>(4);
			}
			set
			{
				base.SetElement<PageMargin>(4, value);
			}
		}

		// Token: 0x17008EF2 RID: 36594
		// (get) Token: 0x06019D9E RID: 105886 RVA: 0x003578A7 File Offset: 0x00355AA7
		// (set) Token: 0x06019D9F RID: 105887 RVA: 0x003578B0 File Offset: 0x00355AB0
		public PaperSource PaperSource
		{
			get
			{
				return base.GetElement<PaperSource>(5);
			}
			set
			{
				base.SetElement<PaperSource>(5, value);
			}
		}

		// Token: 0x17008EF3 RID: 36595
		// (get) Token: 0x06019DA0 RID: 105888 RVA: 0x003578BA File Offset: 0x00355ABA
		// (set) Token: 0x06019DA1 RID: 105889 RVA: 0x003578C3 File Offset: 0x00355AC3
		public PageBorders PageBorders
		{
			get
			{
				return base.GetElement<PageBorders>(6);
			}
			set
			{
				base.SetElement<PageBorders>(6, value);
			}
		}

		// Token: 0x17008EF4 RID: 36596
		// (get) Token: 0x06019DA2 RID: 105890 RVA: 0x003578CD File Offset: 0x00355ACD
		// (set) Token: 0x06019DA3 RID: 105891 RVA: 0x003578D6 File Offset: 0x00355AD6
		public LineNumberType LineNumberType
		{
			get
			{
				return base.GetElement<LineNumberType>(7);
			}
			set
			{
				base.SetElement<LineNumberType>(7, value);
			}
		}

		// Token: 0x17008EF5 RID: 36597
		// (get) Token: 0x06019DA4 RID: 105892 RVA: 0x003578E0 File Offset: 0x00355AE0
		// (set) Token: 0x06019DA5 RID: 105893 RVA: 0x003578E9 File Offset: 0x00355AE9
		public PageNumberType PageNumberType
		{
			get
			{
				return base.GetElement<PageNumberType>(8);
			}
			set
			{
				base.SetElement<PageNumberType>(8, value);
			}
		}

		// Token: 0x17008EF6 RID: 36598
		// (get) Token: 0x06019DA6 RID: 105894 RVA: 0x003578F3 File Offset: 0x00355AF3
		// (set) Token: 0x06019DA7 RID: 105895 RVA: 0x003578FD File Offset: 0x00355AFD
		public Columns Columns
		{
			get
			{
				return base.GetElement<Columns>(9);
			}
			set
			{
				base.SetElement<Columns>(9, value);
			}
		}

		// Token: 0x17008EF7 RID: 36599
		// (get) Token: 0x06019DA8 RID: 105896 RVA: 0x00357908 File Offset: 0x00355B08
		// (set) Token: 0x06019DA9 RID: 105897 RVA: 0x00357912 File Offset: 0x00355B12
		public FormProtection FormProtection
		{
			get
			{
				return base.GetElement<FormProtection>(10);
			}
			set
			{
				base.SetElement<FormProtection>(10, value);
			}
		}

		// Token: 0x17008EF8 RID: 36600
		// (get) Token: 0x06019DAA RID: 105898 RVA: 0x0035791D File Offset: 0x00355B1D
		// (set) Token: 0x06019DAB RID: 105899 RVA: 0x00357927 File Offset: 0x00355B27
		public VerticalTextAlignmentOnPage VerticalTextAlignmentOnPage
		{
			get
			{
				return base.GetElement<VerticalTextAlignmentOnPage>(11);
			}
			set
			{
				base.SetElement<VerticalTextAlignmentOnPage>(11, value);
			}
		}

		// Token: 0x17008EF9 RID: 36601
		// (get) Token: 0x06019DAC RID: 105900 RVA: 0x00357932 File Offset: 0x00355B32
		// (set) Token: 0x06019DAD RID: 105901 RVA: 0x0035793C File Offset: 0x00355B3C
		public NoEndnote NoEndnote
		{
			get
			{
				return base.GetElement<NoEndnote>(12);
			}
			set
			{
				base.SetElement<NoEndnote>(12, value);
			}
		}

		// Token: 0x17008EFA RID: 36602
		// (get) Token: 0x06019DAE RID: 105902 RVA: 0x00357947 File Offset: 0x00355B47
		// (set) Token: 0x06019DAF RID: 105903 RVA: 0x00357951 File Offset: 0x00355B51
		public TitlePage TitlePage
		{
			get
			{
				return base.GetElement<TitlePage>(13);
			}
			set
			{
				base.SetElement<TitlePage>(13, value);
			}
		}

		// Token: 0x17008EFB RID: 36603
		// (get) Token: 0x06019DB0 RID: 105904 RVA: 0x0035795C File Offset: 0x00355B5C
		// (set) Token: 0x06019DB1 RID: 105905 RVA: 0x00357966 File Offset: 0x00355B66
		public TextDirection TextDirection
		{
			get
			{
				return base.GetElement<TextDirection>(14);
			}
			set
			{
				base.SetElement<TextDirection>(14, value);
			}
		}

		// Token: 0x17008EFC RID: 36604
		// (get) Token: 0x06019DB2 RID: 105906 RVA: 0x00357971 File Offset: 0x00355B71
		// (set) Token: 0x06019DB3 RID: 105907 RVA: 0x0035797B File Offset: 0x00355B7B
		public BiDi BiDi
		{
			get
			{
				return base.GetElement<BiDi>(15);
			}
			set
			{
				base.SetElement<BiDi>(15, value);
			}
		}

		// Token: 0x17008EFD RID: 36605
		// (get) Token: 0x06019DB4 RID: 105908 RVA: 0x00357986 File Offset: 0x00355B86
		// (set) Token: 0x06019DB5 RID: 105909 RVA: 0x00357990 File Offset: 0x00355B90
		public GutterOnRight GutterOnRight
		{
			get
			{
				return base.GetElement<GutterOnRight>(16);
			}
			set
			{
				base.SetElement<GutterOnRight>(16, value);
			}
		}

		// Token: 0x17008EFE RID: 36606
		// (get) Token: 0x06019DB6 RID: 105910 RVA: 0x0035799B File Offset: 0x00355B9B
		// (set) Token: 0x06019DB7 RID: 105911 RVA: 0x003579A5 File Offset: 0x00355BA5
		public DocGrid DocGrid
		{
			get
			{
				return base.GetElement<DocGrid>(17);
			}
			set
			{
				base.SetElement<DocGrid>(17, value);
			}
		}

		// Token: 0x17008EFF RID: 36607
		// (get) Token: 0x06019DB8 RID: 105912 RVA: 0x003579B0 File Offset: 0x00355BB0
		// (set) Token: 0x06019DB9 RID: 105913 RVA: 0x003579BA File Offset: 0x00355BBA
		public PrinterSettingsReference PrinterSettingsReference
		{
			get
			{
				return base.GetElement<PrinterSettingsReference>(18);
			}
			set
			{
				base.SetElement<PrinterSettingsReference>(18, value);
			}
		}

		// Token: 0x06019DBA RID: 105914 RVA: 0x003579C8 File Offset: 0x00355BC8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rsidRPr" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidDel" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidR" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidSect" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019DBB RID: 105915 RVA: 0x00357A3D File Offset: 0x00355C3D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousSectionProperties>(deep);
		}

		// Token: 0x0400AAA2 RID: 43682
		private const string tagName = "sectPr";

		// Token: 0x0400AAA3 RID: 43683
		private const byte tagNsId = 23;

		// Token: 0x0400AAA4 RID: 43684
		internal const int ElementTypeIdConst = 11708;

		// Token: 0x0400AAA5 RID: 43685
		private static string[] attributeTagNames = new string[] { "rsidRPr", "rsidDel", "rsidR", "rsidSect" };

		// Token: 0x0400AAA6 RID: 43686
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };

		// Token: 0x0400AAA7 RID: 43687
		private static readonly string[] eleTagNames = new string[]
		{
			"footnotePr", "endnotePr", "type", "pgSz", "pgMar", "paperSrc", "pgBorders", "lnNumType", "pgNumType", "cols",
			"formProt", "vAlign", "noEndnote", "titlePg", "textDirection", "bidi", "rtlGutter", "docGrid", "printerSettings"
		};

		// Token: 0x0400AAA8 RID: 43688
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23
		};
	}
}
