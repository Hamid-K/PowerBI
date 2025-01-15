using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002951 RID: 10577
	[ChildElementInfo(typeof(CarriageReturn))]
	[ChildElementInfo(typeof(Picture))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(Break))]
	[ChildElementInfo(typeof(Text))]
	[ChildElementInfo(typeof(DeletedText))]
	[ChildElementInfo(typeof(FieldCode))]
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
	[ChildElementInfo(typeof(FootnoteReferenceMark))]
	[ChildElementInfo(typeof(EndnoteReferenceMark))]
	[ChildElementInfo(typeof(SeparatorMark))]
	[ChildElementInfo(typeof(ContinuationSeparatorMark))]
	[ChildElementInfo(typeof(SymbolChar))]
	[ChildElementInfo(typeof(PageNumber))]
	[ChildElementInfo(typeof(TabChar))]
	[ChildElementInfo(typeof(EmbeddedObject))]
	[ChildElementInfo(typeof(FieldChar))]
	[ChildElementInfo(typeof(Ruby))]
	[ChildElementInfo(typeof(FootnoteReference))]
	[ChildElementInfo(typeof(EndnoteReference))]
	[ChildElementInfo(typeof(CommentReference))]
	[ChildElementInfo(typeof(Drawing))]
	[ChildElementInfo(typeof(PositionalTab))]
	[ChildElementInfo(typeof(LastRenderedPageBreak))]
	[ChildElementInfo(typeof(Text))]
	internal class Run : OpenXmlCompositeElement
	{
		// Token: 0x17006B7D RID: 27517
		// (get) Token: 0x06014F57 RID: 85847 RVA: 0x002BF737 File Offset: 0x002BD937
		public override string LocalName
		{
			get
			{
				return "r";
			}
		}

		// Token: 0x17006B7E RID: 27518
		// (get) Token: 0x06014F58 RID: 85848 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B7F RID: 27519
		// (get) Token: 0x06014F59 RID: 85849 RVA: 0x003190F4 File Offset: 0x003172F4
		internal override int ElementTypeId
		{
			get
			{
				return 10841;
			}
		}

		// Token: 0x06014F5A RID: 85850 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F5B RID: 85851 RVA: 0x00293ECF File Offset: 0x002920CF
		public Run()
		{
		}

		// Token: 0x06014F5C RID: 85852 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Run(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F5D RID: 85853 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Run(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F5E RID: 85854 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Run(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F5F RID: 85855 RVA: 0x003190FC File Offset: 0x003172FC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
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
			if (21 == namespaceId && "t" == name)
			{
				return new Text();
			}
			return null;
		}

		// Token: 0x17006B80 RID: 27520
		// (get) Token: 0x06014F60 RID: 85856 RVA: 0x00319452 File Offset: 0x00317652
		internal override string[] ElementTagNames
		{
			get
			{
				return Run.eleTagNames;
			}
		}

		// Token: 0x17006B81 RID: 27521
		// (get) Token: 0x06014F61 RID: 85857 RVA: 0x00319459 File Offset: 0x00317659
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Run.eleNamespaceIds;
			}
		}

		// Token: 0x17006B82 RID: 27522
		// (get) Token: 0x06014F62 RID: 85858 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B83 RID: 27523
		// (get) Token: 0x06014F63 RID: 85859 RVA: 0x00319460 File Offset: 0x00317660
		// (set) Token: 0x06014F64 RID: 85860 RVA: 0x00319469 File Offset: 0x00317669
		public RunProperties MathRunProperties
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

		// Token: 0x17006B84 RID: 27524
		// (get) Token: 0x06014F65 RID: 85861 RVA: 0x00319473 File Offset: 0x00317673
		// (set) Token: 0x06014F66 RID: 85862 RVA: 0x0031947C File Offset: 0x0031767C
		public RunProperties RunProperties
		{
			get
			{
				return base.GetElement<RunProperties>(1);
			}
			set
			{
				base.SetElement<RunProperties>(1, value);
			}
		}

		// Token: 0x06014F67 RID: 85863 RVA: 0x00319486 File Offset: 0x00317686
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Run>(deep);
		}

		// Token: 0x040090D4 RID: 37076
		private const string tagName = "r";

		// Token: 0x040090D5 RID: 37077
		private const byte tagNsId = 21;

		// Token: 0x040090D6 RID: 37078
		internal const int ElementTypeIdConst = 10841;

		// Token: 0x040090D7 RID: 37079
		private static readonly string[] eleTagNames = new string[]
		{
			"rPr", "rPr", "br", "t", "delText", "instrText", "delInstrText", "noBreakHyphen", "softHyphen", "dayShort",
			"monthShort", "yearShort", "dayLong", "monthLong", "yearLong", "annotationRef", "footnoteRef", "endnoteRef", "separator", "continuationSeparator",
			"sym", "pgNum", "cr", "tab", "object", "pict", "fldChar", "ruby", "footnoteReference", "endnoteReference",
			"commentReference", "drawing", "ptab", "lastRenderedPageBreak", "t"
		};

		// Token: 0x040090D8 RID: 37080
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			21, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 21
		};
	}
}
