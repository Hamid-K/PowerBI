using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EBA RID: 11962
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Superscript))]
	[ChildElementInfo(typeof(Run))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlRuby))]
	[ChildElementInfo(typeof(SimpleFieldRuby))]
	[ChildElementInfo(typeof(HyperlinkRuby))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(SdtRunRuby))]
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
	[ChildElementInfo(typeof(FieldData))]
	[ChildElementInfo(typeof(SubSuperscript))]
	internal class SimpleFieldRuby : OpenXmlCompositeElement
	{
		// Token: 0x17008C2C RID: 35884
		// (get) Token: 0x06019766 RID: 104294 RVA: 0x0034ACD3 File Offset: 0x00348ED3
		public override string LocalName
		{
			get
			{
				return "fldSimple";
			}
		}

		// Token: 0x17008C2D RID: 35885
		// (get) Token: 0x06019767 RID: 104295 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C2E RID: 35886
		// (get) Token: 0x06019768 RID: 104296 RVA: 0x0034ACDA File Offset: 0x00348EDA
		internal override int ElementTypeId
		{
			get
			{
				return 11619;
			}
		}

		// Token: 0x06019769 RID: 104297 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C2F RID: 35887
		// (get) Token: 0x0601976A RID: 104298 RVA: 0x0034ACE1 File Offset: 0x00348EE1
		internal override string[] AttributeTagNames
		{
			get
			{
				return SimpleFieldRuby.attributeTagNames;
			}
		}

		// Token: 0x17008C30 RID: 35888
		// (get) Token: 0x0601976B RID: 104299 RVA: 0x0034ACE8 File Offset: 0x00348EE8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SimpleFieldRuby.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C31 RID: 35889
		// (get) Token: 0x0601976C RID: 104300 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601976D RID: 104301 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008C32 RID: 35890
		// (get) Token: 0x0601976E RID: 104302 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601976F RID: 104303 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008C33 RID: 35891
		// (get) Token: 0x06019770 RID: 104304 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x06019771 RID: 104305 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06019772 RID: 104306 RVA: 0x00293ECF File Offset: 0x002920CF
		public SimpleFieldRuby()
		{
		}

		// Token: 0x06019773 RID: 104307 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SimpleFieldRuby(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019774 RID: 104308 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SimpleFieldRuby(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019775 RID: 104309 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SimpleFieldRuby(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019776 RID: 104310 RVA: 0x0034ACF0 File Offset: 0x00348EF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fldData" == name)
			{
				return new FieldData();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlRuby();
			}
			if (23 == namespaceId && "fldSimple" == name)
			{
				return new SimpleFieldRuby();
			}
			if (23 == namespaceId && "hyperlink" == name)
			{
				return new HyperlinkRuby();
			}
			if (23 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtRunRuby();
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
			return null;
		}

		// Token: 0x17008C34 RID: 35892
		// (get) Token: 0x06019777 RID: 104311 RVA: 0x0034B26E File Offset: 0x0034946E
		internal override string[] ElementTagNames
		{
			get
			{
				return SimpleFieldRuby.eleTagNames;
			}
		}

		// Token: 0x17008C35 RID: 35893
		// (get) Token: 0x06019778 RID: 104312 RVA: 0x0034B275 File Offset: 0x00349475
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SimpleFieldRuby.eleNamespaceIds;
			}
		}

		// Token: 0x17008C36 RID: 35894
		// (get) Token: 0x06019779 RID: 104313 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008C37 RID: 35895
		// (get) Token: 0x0601977A RID: 104314 RVA: 0x00348164 File Offset: 0x00346364
		// (set) Token: 0x0601977B RID: 104315 RVA: 0x0034816D File Offset: 0x0034636D
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

		// Token: 0x0601977C RID: 104316 RVA: 0x0034B27C File Offset: 0x0034947C
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

		// Token: 0x0601977D RID: 104317 RVA: 0x0034B2D9 File Offset: 0x003494D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SimpleFieldRuby>(deep);
		}

		// Token: 0x0400A8F5 RID: 43253
		private const string tagName = "fldSimple";

		// Token: 0x0400A8F6 RID: 43254
		private const byte tagNsId = 23;

		// Token: 0x0400A8F7 RID: 43255
		internal const int ElementTypeIdConst = 11619;

		// Token: 0x0400A8F8 RID: 43256
		private static string[] attributeTagNames = new string[] { "instr", "fldLock", "dirty" };

		// Token: 0x0400A8F9 RID: 43257
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A8FA RID: 43258
		private static readonly string[] eleTagNames = new string[]
		{
			"fldData", "customXml", "fldSimple", "hyperlink", "r", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart",
			"bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart",
			"customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins",
			"del", "moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel", "oMathPara", "oMath", "acc", "bar",
			"box", "borderBox", "d", "eqArr", "f", "func", "groupChr", "limLow", "limUpp", "m",
			"nary", "phant", "rad", "sPre", "sSub", "sSubSup", "sSup", "r"
		};

		// Token: 0x0400A8FB RID: 43259
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 52, 52, 52, 52, 23,
			23, 23, 23, 23, 52, 52, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 21, 21
		};
	}
}
