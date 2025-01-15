using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ECC RID: 11980
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(LimitLower))]
	[ChildElementInfo(typeof(LimitUpper))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ParagraphProperties))]
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
	[ChildElementInfo(typeof(SubDocumentReference))]
	internal class Paragraph : OpenXmlCompositeElement
	{
		// Token: 0x17008CA8 RID: 36008
		// (get) Token: 0x06019880 RID: 104576 RVA: 0x002EA9F7 File Offset: 0x002E8BF7
		public override string LocalName
		{
			get
			{
				return "p";
			}
		}

		// Token: 0x17008CA9 RID: 36009
		// (get) Token: 0x06019881 RID: 104577 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CAA RID: 36010
		// (get) Token: 0x06019882 RID: 104578 RVA: 0x0034E308 File Offset: 0x0034C508
		internal override int ElementTypeId
		{
			get
			{
				return 11635;
			}
		}

		// Token: 0x06019883 RID: 104579 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008CAB RID: 36011
		// (get) Token: 0x06019884 RID: 104580 RVA: 0x0034E30F File Offset: 0x0034C50F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Paragraph.attributeTagNames;
			}
		}

		// Token: 0x17008CAC RID: 36012
		// (get) Token: 0x06019885 RID: 104581 RVA: 0x0034E316 File Offset: 0x0034C516
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Paragraph.attributeNamespaceIds;
			}
		}

		// Token: 0x17008CAD RID: 36013
		// (get) Token: 0x06019886 RID: 104582 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06019887 RID: 104583 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "rsidRPr")]
		public HexBinaryValue RsidParagraphMarkRevision
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

		// Token: 0x17008CAE RID: 36014
		// (get) Token: 0x06019888 RID: 104584 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06019889 RID: 104585 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "rsidR")]
		public HexBinaryValue RsidParagraphAddition
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

		// Token: 0x17008CAF RID: 36015
		// (get) Token: 0x0601988A RID: 104586 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x0601988B RID: 104587 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "rsidDel")]
		public HexBinaryValue RsidParagraphDeletion
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

		// Token: 0x17008CB0 RID: 36016
		// (get) Token: 0x0601988C RID: 104588 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x0601988D RID: 104589 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "rsidP")]
		public HexBinaryValue RsidParagraphProperties
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

		// Token: 0x17008CB1 RID: 36017
		// (get) Token: 0x0601988E RID: 104590 RVA: 0x002EB784 File Offset: 0x002E9984
		// (set) Token: 0x0601988F RID: 104591 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "rsidRDefault")]
		public HexBinaryValue RsidRunAdditionDefault
		{
			get
			{
				return (HexBinaryValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008CB2 RID: 36018
		// (get) Token: 0x06019890 RID: 104592 RVA: 0x003137E6 File Offset: 0x003119E6
		// (set) Token: 0x06019891 RID: 104593 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(52, "paraId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue ParagraphId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008CB3 RID: 36019
		// (get) Token: 0x06019892 RID: 104594 RVA: 0x0032ED05 File Offset: 0x0032CF05
		// (set) Token: 0x06019893 RID: 104595 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(52, "textId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue TextId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008CB4 RID: 36020
		// (get) Token: 0x06019894 RID: 104596 RVA: 0x00348E89 File Offset: 0x00347089
		// (set) Token: 0x06019895 RID: 104597 RVA: 0x002BD516 File Offset: 0x002BB716
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(52, "noSpellErr")]
		public OnOffValue NoSpellError
		{
			get
			{
				return (OnOffValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06019896 RID: 104598 RVA: 0x00293ECF File Offset: 0x002920CF
		public Paragraph()
		{
		}

		// Token: 0x06019897 RID: 104599 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Paragraph(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019898 RID: 104600 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Paragraph(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019899 RID: 104601 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Paragraph(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601989A RID: 104602 RVA: 0x0034E320 File Offset: 0x0034C520
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pPr" == name)
			{
				return new ParagraphProperties();
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

		// Token: 0x17008CB5 RID: 36021
		// (get) Token: 0x0601989B RID: 104603 RVA: 0x0034E8FE File Offset: 0x0034CAFE
		internal override string[] ElementTagNames
		{
			get
			{
				return Paragraph.eleTagNames;
			}
		}

		// Token: 0x17008CB6 RID: 36022
		// (get) Token: 0x0601989C RID: 104604 RVA: 0x0034E905 File Offset: 0x0034CB05
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Paragraph.eleNamespaceIds;
			}
		}

		// Token: 0x17008CB7 RID: 36023
		// (get) Token: 0x0601989D RID: 104605 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008CB8 RID: 36024
		// (get) Token: 0x0601989E RID: 104606 RVA: 0x0034E90C File Offset: 0x0034CB0C
		// (set) Token: 0x0601989F RID: 104607 RVA: 0x0034E915 File Offset: 0x0034CB15
		public ParagraphProperties ParagraphProperties
		{
			get
			{
				return base.GetElement<ParagraphProperties>(0);
			}
			set
			{
				base.SetElement<ParagraphProperties>(0, value);
			}
		}

		// Token: 0x060198A0 RID: 104608 RVA: 0x0034E920 File Offset: 0x0034CB20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rsidRPr" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidR" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidDel" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidP" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "rsidRDefault" == name)
			{
				return new HexBinaryValue();
			}
			if (52 == namespaceId && "paraId" == name)
			{
				return new HexBinaryValue();
			}
			if (52 == namespaceId && "textId" == name)
			{
				return new HexBinaryValue();
			}
			if (52 == namespaceId && "noSpellErr" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060198A1 RID: 104609 RVA: 0x0034E9F5 File Offset: 0x0034CBF5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Paragraph>(deep);
		}

		// Token: 0x0400A947 RID: 43335
		private const string tagName = "p";

		// Token: 0x0400A948 RID: 43336
		private const byte tagNsId = 23;

		// Token: 0x0400A949 RID: 43337
		internal const int ElementTypeIdConst = 11635;

		// Token: 0x0400A94A RID: 43338
		private static string[] attributeTagNames = new string[] { "rsidRPr", "rsidR", "rsidDel", "rsidP", "rsidRDefault", "paraId", "textId", "noSpellErr" };

		// Token: 0x0400A94B RID: 43339
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 52, 52, 52 };

		// Token: 0x0400A94C RID: 43340
		private static readonly string[] eleTagNames = new string[]
		{
			"pPr", "customXml", "fldSimple", "hyperlink", "smartTag", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart",
			"bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart",
			"customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins",
			"del", "moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel", "oMathPara", "oMath", "acc", "bar",
			"box", "borderBox", "d", "eqArr", "f", "func", "groupChr", "limLow", "limUpp", "m",
			"nary", "phant", "rad", "sPre", "sSub", "sSubSup", "sSup", "r", "r", "bdo",
			"dir", "subDoc"
		};

		// Token: 0x0400A94D RID: 43341
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
