using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EBC RID: 11964
	[ChildElementInfo(typeof(FieldCode))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FootnoteReferenceMark))]
	[ChildElementInfo(typeof(EndnoteReferenceMark))]
	[ChildElementInfo(typeof(LastRenderedPageBreak))]
	[ChildElementInfo(typeof(Break))]
	[ChildElementInfo(typeof(Text))]
	[ChildElementInfo(typeof(DeletedText))]
	[ChildElementInfo(typeof(DeletedFieldCode))]
	[ChildElementInfo(typeof(NoBreakHyphen))]
	[ChildElementInfo(typeof(SoftHyphen))]
	[ChildElementInfo(typeof(DayShort))]
	[ChildElementInfo(typeof(MonthShort))]
	[ChildElementInfo(typeof(YearShort))]
	[ChildElementInfo(typeof(DayLong))]
	[ChildElementInfo(typeof(MonthLong))]
	[ChildElementInfo(typeof(YearLong))]
	[ChildElementInfo(typeof(AnnotationReferenceMark))]
	[ChildElementInfo(typeof(EmbeddedObject))]
	[ChildElementInfo(typeof(PositionalTab))]
	[ChildElementInfo(typeof(SeparatorMark))]
	[ChildElementInfo(typeof(ContinuationSeparatorMark))]
	[ChildElementInfo(typeof(SymbolChar))]
	[ChildElementInfo(typeof(PageNumber))]
	[ChildElementInfo(typeof(CarriageReturn))]
	[ChildElementInfo(typeof(TabChar))]
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(FieldChar))]
	[ChildElementInfo(typeof(Ruby))]
	[ChildElementInfo(typeof(FootnoteReference))]
	[ChildElementInfo(typeof(EndnoteReference))]
	[ChildElementInfo(typeof(CommentReference))]
	[ChildElementInfo(typeof(Drawing))]
	internal class Run : OpenXmlCompositeElement
	{
		// Token: 0x17008C43 RID: 35907
		// (get) Token: 0x06019799 RID: 104345 RVA: 0x002BF737 File Offset: 0x002BD937
		public override string LocalName
		{
			get
			{
				return "r";
			}
		}

		// Token: 0x17008C44 RID: 35908
		// (get) Token: 0x0601979A RID: 104346 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C45 RID: 35909
		// (get) Token: 0x0601979B RID: 104347 RVA: 0x0034BBE0 File Offset: 0x00349DE0
		internal override int ElementTypeId
		{
			get
			{
				return 11621;
			}
		}

		// Token: 0x0601979C RID: 104348 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C46 RID: 35910
		// (get) Token: 0x0601979D RID: 104349 RVA: 0x0034BBE7 File Offset: 0x00349DE7
		internal override string[] AttributeTagNames
		{
			get
			{
				return Run.attributeTagNames;
			}
		}

		// Token: 0x17008C47 RID: 35911
		// (get) Token: 0x0601979E RID: 104350 RVA: 0x0034BBEE File Offset: 0x00349DEE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Run.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C48 RID: 35912
		// (get) Token: 0x0601979F RID: 104351 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x060197A0 RID: 104352 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "rsidRPr")]
		public HexBinaryValue RsidRunProperties
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

		// Token: 0x17008C49 RID: 35913
		// (get) Token: 0x060197A1 RID: 104353 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x060197A2 RID: 104354 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "rsidDel")]
		public HexBinaryValue RsidRunDeletion
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

		// Token: 0x17008C4A RID: 35914
		// (get) Token: 0x060197A3 RID: 104355 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x060197A4 RID: 104356 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "rsidR")]
		public HexBinaryValue RsidRunAddition
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

		// Token: 0x060197A5 RID: 104357 RVA: 0x00293ECF File Offset: 0x002920CF
		public Run()
		{
		}

		// Token: 0x060197A6 RID: 104358 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Run(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060197A7 RID: 104359 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Run(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060197A8 RID: 104360 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Run(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060197A9 RID: 104361 RVA: 0x0034BBF8 File Offset: 0x00349DF8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (23 == namespaceId && "br" == name)
			{
				return new Break();
			}
			if (23 == namespaceId && "t" == name)
			{
				return new Text();
			}
			if (23 == namespaceId && "delText" == name)
			{
				return new DeletedText();
			}
			if (23 == namespaceId && "instrText" == name)
			{
				return new FieldCode();
			}
			if (23 == namespaceId && "delInstrText" == name)
			{
				return new DeletedFieldCode();
			}
			if (23 == namespaceId && "noBreakHyphen" == name)
			{
				return new NoBreakHyphen();
			}
			if (23 == namespaceId && "softHyphen" == name)
			{
				return new SoftHyphen();
			}
			if (23 == namespaceId && "dayShort" == name)
			{
				return new DayShort();
			}
			if (23 == namespaceId && "monthShort" == name)
			{
				return new MonthShort();
			}
			if (23 == namespaceId && "yearShort" == name)
			{
				return new YearShort();
			}
			if (23 == namespaceId && "dayLong" == name)
			{
				return new DayLong();
			}
			if (23 == namespaceId && "monthLong" == name)
			{
				return new MonthLong();
			}
			if (23 == namespaceId && "yearLong" == name)
			{
				return new YearLong();
			}
			if (23 == namespaceId && "annotationRef" == name)
			{
				return new AnnotationReferenceMark();
			}
			if (23 == namespaceId && "footnoteRef" == name)
			{
				return new FootnoteReferenceMark();
			}
			if (23 == namespaceId && "endnoteRef" == name)
			{
				return new EndnoteReferenceMark();
			}
			if (23 == namespaceId && "separator" == name)
			{
				return new SeparatorMark();
			}
			if (23 == namespaceId && "continuationSeparator" == name)
			{
				return new ContinuationSeparatorMark();
			}
			if (23 == namespaceId && "sym" == name)
			{
				return new SymbolChar();
			}
			if (23 == namespaceId && "pgNum" == name)
			{
				return new PageNumber();
			}
			if (23 == namespaceId && "cr" == name)
			{
				return new CarriageReturn();
			}
			if (23 == namespaceId && "tab" == name)
			{
				return new TabChar();
			}
			if (23 == namespaceId && "object" == name)
			{
				return new EmbeddedObject();
			}
			if (23 == namespaceId && "pict" == name)
			{
				return new Picture();
			}
			if (23 == namespaceId && "fldChar" == name)
			{
				return new FieldChar();
			}
			if (23 == namespaceId && "ruby" == name)
			{
				return new Ruby();
			}
			if (23 == namespaceId && "footnoteReference" == name)
			{
				return new FootnoteReference();
			}
			if (23 == namespaceId && "endnoteReference" == name)
			{
				return new EndnoteReference();
			}
			if (23 == namespaceId && "commentReference" == name)
			{
				return new CommentReference();
			}
			if (23 == namespaceId && "drawing" == name)
			{
				return new Drawing();
			}
			if (23 == namespaceId && "ptab" == name)
			{
				return new PositionalTab();
			}
			if (23 == namespaceId && "lastRenderedPageBreak" == name)
			{
				return new LastRenderedPageBreak();
			}
			return null;
		}

		// Token: 0x17008C4B RID: 35915
		// (get) Token: 0x060197AA RID: 104362 RVA: 0x0034BF1E File Offset: 0x0034A11E
		internal override string[] ElementTagNames
		{
			get
			{
				return Run.eleTagNames;
			}
		}

		// Token: 0x17008C4C RID: 35916
		// (get) Token: 0x060197AB RID: 104363 RVA: 0x0034BF25 File Offset: 0x0034A125
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Run.eleNamespaceIds;
			}
		}

		// Token: 0x17008C4D RID: 35917
		// (get) Token: 0x060197AC RID: 104364 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008C4E RID: 35918
		// (get) Token: 0x060197AD RID: 104365 RVA: 0x0034BF2C File Offset: 0x0034A12C
		// (set) Token: 0x060197AE RID: 104366 RVA: 0x0034BF35 File Offset: 0x0034A135
		public RunProperties RunProperties
		{
			get
			{
				return base.GetElement<RunProperties>(0);
			}
			set
			{
				base.SetElement<RunProperties>(0, value);
			}
		}

		// Token: 0x060197AF RID: 104367 RVA: 0x0034BF40 File Offset: 0x0034A140
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060197B0 RID: 104368 RVA: 0x0034BF9D File Offset: 0x0034A19D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Run>(deep);
		}

		// Token: 0x0400A901 RID: 43265
		private const string tagName = "r";

		// Token: 0x0400A902 RID: 43266
		private const byte tagNsId = 23;

		// Token: 0x0400A903 RID: 43267
		internal const int ElementTypeIdConst = 11621;

		// Token: 0x0400A904 RID: 43268
		private static string[] attributeTagNames = new string[] { "rsidRPr", "rsidDel", "rsidR" };

		// Token: 0x0400A905 RID: 43269
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A906 RID: 43270
		private static readonly string[] eleTagNames = new string[]
		{
			"rPr", "br", "t", "delText", "instrText", "delInstrText", "noBreakHyphen", "softHyphen", "dayShort", "monthShort",
			"yearShort", "dayLong", "monthLong", "yearLong", "annotationRef", "footnoteRef", "endnoteRef", "separator", "continuationSeparator", "sym",
			"pgNum", "cr", "tab", "object", "pict", "fldChar", "ruby", "footnoteReference", "endnoteReference", "commentReference",
			"drawing", "ptab", "lastRenderedPageBreak"
		};

		// Token: 0x0400A907 RID: 43271
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23
		};
	}
}
