using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F2F RID: 12079
	[ChildElementInfo(typeof(PageSize))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(HeaderReference))]
	[ChildElementInfo(typeof(FooterReference))]
	[ChildElementInfo(typeof(FootnoteProperties))]
	[ChildElementInfo(typeof(EndnoteProperties))]
	[ChildElementInfo(typeof(SectionType))]
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
	[ChildElementInfo(typeof(BiDi))]
	[ChildElementInfo(typeof(GutterOnRight))]
	[ChildElementInfo(typeof(DocGrid))]
	[ChildElementInfo(typeof(PrinterSettingsReference))]
	[ChildElementInfo(typeof(SectionPropertiesChange))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SectionProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008F61 RID: 36705
		// (get) Token: 0x06019E8A RID: 106122 RVA: 0x00357648 File Offset: 0x00355848
		public override string LocalName
		{
			get
			{
				return "sectPr";
			}
		}

		// Token: 0x17008F62 RID: 36706
		// (get) Token: 0x06019E8B RID: 106123 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F63 RID: 36707
		// (get) Token: 0x06019E8C RID: 106124 RVA: 0x003599DF File Offset: 0x00357BDF
		internal override int ElementTypeId
		{
			get
			{
				return 11723;
			}
		}

		// Token: 0x06019E8D RID: 106125 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F64 RID: 36708
		// (get) Token: 0x06019E8E RID: 106126 RVA: 0x003599E6 File Offset: 0x00357BE6
		internal override string[] AttributeTagNames
		{
			get
			{
				return SectionProperties.attributeTagNames;
			}
		}

		// Token: 0x17008F65 RID: 36709
		// (get) Token: 0x06019E8F RID: 106127 RVA: 0x003599ED File Offset: 0x00357BED
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SectionProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F66 RID: 36710
		// (get) Token: 0x06019E90 RID: 106128 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06019E91 RID: 106129 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008F67 RID: 36711
		// (get) Token: 0x06019E92 RID: 106130 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06019E93 RID: 106131 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008F68 RID: 36712
		// (get) Token: 0x06019E94 RID: 106132 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x06019E95 RID: 106133 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17008F69 RID: 36713
		// (get) Token: 0x06019E96 RID: 106134 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x06019E97 RID: 106135 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x06019E98 RID: 106136 RVA: 0x00293ECF File Offset: 0x002920CF
		public SectionProperties()
		{
		}

		// Token: 0x06019E99 RID: 106137 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SectionProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E9A RID: 106138 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SectionProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019E9B RID: 106139 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SectionProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019E9C RID: 106140 RVA: 0x003599F4 File Offset: 0x00357BF4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "headerReference" == name)
			{
				return new HeaderReference();
			}
			if (23 == namespaceId && "footerReference" == name)
			{
				return new FooterReference();
			}
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
			if (23 == namespaceId && "sectPrChange" == name)
			{
				return new SectionPropertiesChange();
			}
			return null;
		}

		// Token: 0x06019E9D RID: 106141 RVA: 0x00359C14 File Offset: 0x00357E14
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

		// Token: 0x06019E9E RID: 106142 RVA: 0x00359C89 File Offset: 0x00357E89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionProperties>(deep);
		}

		// Token: 0x0400AAD5 RID: 43733
		private const string tagName = "sectPr";

		// Token: 0x0400AAD6 RID: 43734
		private const byte tagNsId = 23;

		// Token: 0x0400AAD7 RID: 43735
		internal const int ElementTypeIdConst = 11723;

		// Token: 0x0400AAD8 RID: 43736
		private static string[] attributeTagNames = new string[] { "rsidRPr", "rsidDel", "rsidR", "rsidSect" };

		// Token: 0x0400AAD9 RID: 43737
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
