using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED5 RID: 11989
	[ChildElementInfo(typeof(SubDocumentReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FieldData))]
	[ChildElementInfo(typeof(CustomXmlRun))]
	[ChildElementInfo(typeof(SimpleField))]
	[ChildElementInfo(typeof(Hyperlink))]
	[ChildElementInfo(typeof(SmartTagRun))]
	[ChildElementInfo(typeof(SdtRun))]
	[ChildElementInfo(typeof(ProofError))]
	[ChildElementInfo(typeof(PermStart))]
	[ChildElementInfo(typeof(PermEnd))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
	[ChildElementInfo(typeof(MoveToRangeStart))]
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[ChildElementInfo(typeof(CustomXmlDelRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(InsertedRun))]
	[ChildElementInfo(typeof(DeletedRun))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Paragraph))]
	[ChildElementInfo(typeof(OfficeMath))]
	[ChildElementInfo(typeof(Accent))]
	[ChildElementInfo(typeof(Bar))]
	[ChildElementInfo(typeof(Box))]
	[ChildElementInfo(typeof(BorderBox))]
	[ChildElementInfo(typeof(Delimiter))]
	[ChildElementInfo(typeof(EquationArray))]
	[ChildElementInfo(typeof(Fraction))]
	[ChildElementInfo(typeof(MathFunction))]
	[ChildElementInfo(typeof(GroupChar))]
	[ChildElementInfo(typeof(LimitLower))]
	[ChildElementInfo(typeof(LimitUpper))]
	[ChildElementInfo(typeof(Matrix))]
	[ChildElementInfo(typeof(Nary))]
	[ChildElementInfo(typeof(Phantom))]
	[ChildElementInfo(typeof(Radical))]
	[ChildElementInfo(typeof(PreSubSuper))]
	[ChildElementInfo(typeof(Subscript))]
	[ChildElementInfo(typeof(SubSuperscript))]
	[ChildElementInfo(typeof(Superscript))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(BidirectionalOverride), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BidirectionalEmbedding), FileFormatVersions.Office2010)]
	internal class SimpleField : OpenXmlCompositeElement
	{
		// Token: 0x17008CF9 RID: 36089
		// (get) Token: 0x06019931 RID: 104753 RVA: 0x0034ACD3 File Offset: 0x00348ED3
		public override string LocalName
		{
			get
			{
				return "fldSimple";
			}
		}

		// Token: 0x17008CFA RID: 36090
		// (get) Token: 0x06019932 RID: 104754 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CFB RID: 36091
		// (get) Token: 0x06019933 RID: 104755 RVA: 0x0035148F File Offset: 0x0034F68F
		internal override int ElementTypeId
		{
			get
			{
				return 11644;
			}
		}

		// Token: 0x06019934 RID: 104756 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008CFC RID: 36092
		// (get) Token: 0x06019935 RID: 104757 RVA: 0x00351496 File Offset: 0x0034F696
		internal override string[] AttributeTagNames
		{
			get
			{
				return SimpleField.attributeTagNames;
			}
		}

		// Token: 0x17008CFD RID: 36093
		// (get) Token: 0x06019936 RID: 104758 RVA: 0x0035149D File Offset: 0x0034F69D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SimpleField.attributeNamespaceIds;
			}
		}

		// Token: 0x17008CFE RID: 36094
		// (get) Token: 0x06019937 RID: 104759 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019938 RID: 104760 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "instr")]
		public StringValue Instruction
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008CFF RID: 36095
		// (get) Token: 0x06019939 RID: 104761 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601993A RID: 104762 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "fldLock")]
		public OnOffValue FieldLock
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008D00 RID: 36096
		// (get) Token: 0x0601993B RID: 104763 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601993C RID: 104764 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "dirty")]
		public OnOffValue Dirty
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

		// Token: 0x0601993D RID: 104765 RVA: 0x00293ECF File Offset: 0x002920CF
		public SimpleField()
		{
		}

		// Token: 0x0601993E RID: 104766 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SimpleField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601993F RID: 104767 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SimpleField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019940 RID: 104768 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SimpleField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019941 RID: 104769 RVA: 0x003514A4 File Offset: 0x0034F6A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fldData" == name)
			{
				return new FieldData();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlRun();
			}
			if (23 == namespaceId && "fldSimple" == name)
			{
				return new SimpleField();
			}
			if (23 == namespaceId && "hyperlink" == name)
			{
				return new Hyperlink();
			}
			if (23 == namespaceId && "smartTag" == name)
			{
				return new SmartTagRun();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtRun();
			}
			if (23 == namespaceId && "proofErr" == name)
			{
				return new ProofError();
			}
			if (23 == namespaceId && "permStart" == name)
			{
				return new PermStart();
			}
			if (23 == namespaceId && "permEnd" == name)
			{
				return new PermEnd();
			}
			if (23 == namespaceId && "bookmarkStart" == name)
			{
				return new BookmarkStart();
			}
			if (23 == namespaceId && "bookmarkEnd" == name)
			{
				return new BookmarkEnd();
			}
			if (23 == namespaceId && "commentRangeStart" == name)
			{
				return new CommentRangeStart();
			}
			if (23 == namespaceId && "commentRangeEnd" == name)
			{
				return new CommentRangeEnd();
			}
			if (23 == namespaceId && "moveFromRangeStart" == name)
			{
				return new MoveFromRangeStart();
			}
			if (23 == namespaceId && "moveFromRangeEnd" == name)
			{
				return new MoveFromRangeEnd();
			}
			if (23 == namespaceId && "moveToRangeStart" == name)
			{
				return new MoveToRangeStart();
			}
			if (23 == namespaceId && "moveToRangeEnd" == name)
			{
				return new MoveToRangeEnd();
			}
			if (23 == namespaceId && "customXmlInsRangeStart" == name)
			{
				return new CustomXmlInsRangeStart();
			}
			if (23 == namespaceId && "customXmlInsRangeEnd" == name)
			{
				return new CustomXmlInsRangeEnd();
			}
			if (23 == namespaceId && "customXmlDelRangeStart" == name)
			{
				return new CustomXmlDelRangeStart();
			}
			if (23 == namespaceId && "customXmlDelRangeEnd" == name)
			{
				return new CustomXmlDelRangeEnd();
			}
			if (23 == namespaceId && "customXmlMoveFromRangeStart" == name)
			{
				return new CustomXmlMoveFromRangeStart();
			}
			if (23 == namespaceId && "customXmlMoveFromRangeEnd" == name)
			{
				return new CustomXmlMoveFromRangeEnd();
			}
			if (23 == namespaceId && "customXmlMoveToRangeStart" == name)
			{
				return new CustomXmlMoveToRangeStart();
			}
			if (23 == namespaceId && "customXmlMoveToRangeEnd" == name)
			{
				return new CustomXmlMoveToRangeEnd();
			}
			if (52 == namespaceId && "customXmlConflictInsRangeStart" == name)
			{
				return new CustomXmlConflictInsertionRangeStart();
			}
			if (52 == namespaceId && "customXmlConflictInsRangeEnd" == name)
			{
				return new CustomXmlConflictInsertionRangeEnd();
			}
			if (52 == namespaceId && "customXmlConflictDelRangeStart" == name)
			{
				return new CustomXmlConflictDeletionRangeStart();
			}
			if (52 == namespaceId && "customXmlConflictDelRangeEnd" == name)
			{
				return new CustomXmlConflictDeletionRangeEnd();
			}
			if (23 == namespaceId && "ins" == name)
			{
				return new InsertedRun();
			}
			if (23 == namespaceId && "del" == name)
			{
				return new DeletedRun();
			}
			if (23 == namespaceId && "moveFrom" == name)
			{
				return new MoveFromRun();
			}
			if (23 == namespaceId && "moveTo" == name)
			{
				return new MoveToRun();
			}
			if (23 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			if (52 == namespaceId && "conflictIns" == name)
			{
				return new RunConflictInsertion();
			}
			if (52 == namespaceId && "conflictDel" == name)
			{
				return new RunConflictDeletion();
			}
			if (21 == namespaceId && "oMathPara" == name)
			{
				return new Paragraph();
			}
			if (21 == namespaceId && "oMath" == name)
			{
				return new OfficeMath();
			}
			if (21 == namespaceId && "acc" == name)
			{
				return new Accent();
			}
			if (21 == namespaceId && "bar" == name)
			{
				return new Bar();
			}
			if (21 == namespaceId && "box" == name)
			{
				return new Box();
			}
			if (21 == namespaceId && "borderBox" == name)
			{
				return new BorderBox();
			}
			if (21 == namespaceId && "d" == name)
			{
				return new Delimiter();
			}
			if (21 == namespaceId && "eqArr" == name)
			{
				return new EquationArray();
			}
			if (21 == namespaceId && "f" == name)
			{
				return new Fraction();
			}
			if (21 == namespaceId && "func" == name)
			{
				return new MathFunction();
			}
			if (21 == namespaceId && "groupChr" == name)
			{
				return new GroupChar();
			}
			if (21 == namespaceId && "limLow" == name)
			{
				return new LimitLower();
			}
			if (21 == namespaceId && "limUpp" == name)
			{
				return new LimitUpper();
			}
			if (21 == namespaceId && "m" == name)
			{
				return new Matrix();
			}
			if (21 == namespaceId && "nary" == name)
			{
				return new Nary();
			}
			if (21 == namespaceId && "phant" == name)
			{
				return new Phantom();
			}
			if (21 == namespaceId && "rad" == name)
			{
				return new Radical();
			}
			if (21 == namespaceId && "sPre" == name)
			{
				return new PreSubSuper();
			}
			if (21 == namespaceId && "sSub" == name)
			{
				return new Subscript();
			}
			if (21 == namespaceId && "sSubSup" == name)
			{
				return new SubSuperscript();
			}
			if (21 == namespaceId && "sSup" == name)
			{
				return new Superscript();
			}
			if (21 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (23 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (23 == namespaceId && "bdo" == name)
			{
				return new BidirectionalOverride();
			}
			if (23 == namespaceId && "dir" == name)
			{
				return new BidirectionalEmbedding();
			}
			if (23 == namespaceId && "subDoc" == name)
			{
				return new SubDocumentReference();
			}
			return null;
		}

		// Token: 0x17008D01 RID: 36097
		// (get) Token: 0x06019942 RID: 104770 RVA: 0x00351A82 File Offset: 0x0034FC82
		internal override string[] ElementTagNames
		{
			get
			{
				return SimpleField.eleTagNames;
			}
		}

		// Token: 0x17008D02 RID: 36098
		// (get) Token: 0x06019943 RID: 104771 RVA: 0x00351A89 File Offset: 0x0034FC89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SimpleField.eleNamespaceIds;
			}
		}

		// Token: 0x17008D03 RID: 36099
		// (get) Token: 0x06019944 RID: 104772 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008D04 RID: 36100
		// (get) Token: 0x06019945 RID: 104773 RVA: 0x00348164 File Offset: 0x00346364
		// (set) Token: 0x06019946 RID: 104774 RVA: 0x0034816D File Offset: 0x0034636D
		public FieldData FieldData
		{
			get
			{
				return base.GetElement<FieldData>(0);
			}
			set
			{
				base.SetElement<FieldData>(0, value);
			}
		}

		// Token: 0x06019947 RID: 104775 RVA: 0x00351A90 File Offset: 0x0034FC90
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "instr" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "fldLock" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "dirty" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019948 RID: 104776 RVA: 0x00351AED File Offset: 0x0034FCED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SimpleField>(deep);
		}

		// Token: 0x0400A97C RID: 43388
		private const string tagName = "fldSimple";

		// Token: 0x0400A97D RID: 43389
		private const byte tagNsId = 23;

		// Token: 0x0400A97E RID: 43390
		internal const int ElementTypeIdConst = 11644;

		// Token: 0x0400A97F RID: 43391
		private static string[] attributeTagNames = new string[] { "instr", "fldLock", "dirty" };

		// Token: 0x0400A980 RID: 43392
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A981 RID: 43393
		private static readonly string[] eleTagNames = new string[]
		{
			"fldData", "customXml", "fldSimple", "hyperlink", "smartTag", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart",
			"bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart",
			"customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins",
			"del", "moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel", "oMathPara", "oMath", "acc", "bar",
			"box", "borderBox", "d", "eqArr", "f", "func", "groupChr", "limLow", "limUpp", "m",
			"nary", "phant", "rad", "sPre", "sSub", "sSubSup", "sSup", "r", "r", "bdo",
			"dir", "subDoc"
		};

		// Token: 0x0400A982 RID: 43394
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 52, 52, 52, 52, 23,
			23, 23, 23, 23, 52, 52, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 21, 21, 23, 23,
			23, 23
		};
	}
}
