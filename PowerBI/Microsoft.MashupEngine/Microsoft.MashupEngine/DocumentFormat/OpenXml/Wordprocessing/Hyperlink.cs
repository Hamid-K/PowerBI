using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED6 RID: 11990
	[ChildElementInfo(typeof(BookmarkStart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlRun))]
	[ChildElementInfo(typeof(SimpleField))]
	[ChildElementInfo(typeof(Hyperlink))]
	[ChildElementInfo(typeof(SmartTagRun))]
	[ChildElementInfo(typeof(SdtRun))]
	[ChildElementInfo(typeof(ProofError))]
	[ChildElementInfo(typeof(PermStart))]
	[ChildElementInfo(typeof(PermEnd))]
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
	[ChildElementInfo(typeof(SubDocumentReference))]
	internal class Hyperlink : OpenXmlCompositeElement
	{
		// Token: 0x17008D05 RID: 36101
		// (get) Token: 0x0601994A RID: 104778 RVA: 0x002D9347 File Offset: 0x002D7547
		public override string LocalName
		{
			get
			{
				return "hyperlink";
			}
		}

		// Token: 0x17008D06 RID: 36102
		// (get) Token: 0x0601994B RID: 104779 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D07 RID: 36103
		// (get) Token: 0x0601994C RID: 104780 RVA: 0x00351D8A File Offset: 0x0034FF8A
		internal override int ElementTypeId
		{
			get
			{
				return 11645;
			}
		}

		// Token: 0x0601994D RID: 104781 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008D08 RID: 36104
		// (get) Token: 0x0601994E RID: 104782 RVA: 0x00351D91 File Offset: 0x0034FF91
		internal override string[] AttributeTagNames
		{
			get
			{
				return Hyperlink.attributeTagNames;
			}
		}

		// Token: 0x17008D09 RID: 36105
		// (get) Token: 0x0601994F RID: 104783 RVA: 0x00351D98 File Offset: 0x0034FF98
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Hyperlink.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D0A RID: 36106
		// (get) Token: 0x06019950 RID: 104784 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019951 RID: 104785 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "tgtFrame")]
		public StringValue TargetFrame
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

		// Token: 0x17008D0B RID: 36107
		// (get) Token: 0x06019952 RID: 104786 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019953 RID: 104787 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "tooltip")]
		public StringValue Tooltip
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

		// Token: 0x17008D0C RID: 36108
		// (get) Token: 0x06019954 RID: 104788 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019955 RID: 104789 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "docLocation")]
		public StringValue DocLocation
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008D0D RID: 36109
		// (get) Token: 0x06019956 RID: 104790 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x06019957 RID: 104791 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "history")]
		public OnOffValue History
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

		// Token: 0x17008D0E RID: 36110
		// (get) Token: 0x06019958 RID: 104792 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06019959 RID: 104793 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "anchor")]
		public StringValue Anchor
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008D0F RID: 36111
		// (get) Token: 0x0601995A RID: 104794 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601995B RID: 104795 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0601995C RID: 104796 RVA: 0x00293ECF File Offset: 0x002920CF
		public Hyperlink()
		{
		}

		// Token: 0x0601995D RID: 104797 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Hyperlink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601995E RID: 104798 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Hyperlink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601995F RID: 104799 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Hyperlink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019960 RID: 104800 RVA: 0x00351DA0 File Offset: 0x0034FFA0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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

		// Token: 0x06019961 RID: 104801 RVA: 0x00352368 File Offset: 0x00350568
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tgtFrame" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "tooltip" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "docLocation" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "history" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "anchor" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019962 RID: 104802 RVA: 0x0035240D File Offset: 0x0035060D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hyperlink>(deep);
		}

		// Token: 0x0400A983 RID: 43395
		private const string tagName = "hyperlink";

		// Token: 0x0400A984 RID: 43396
		private const byte tagNsId = 23;

		// Token: 0x0400A985 RID: 43397
		internal const int ElementTypeIdConst = 11645;

		// Token: 0x0400A986 RID: 43398
		private static string[] attributeTagNames = new string[] { "tgtFrame", "tooltip", "docLocation", "history", "anchor", "id" };

		// Token: 0x0400A987 RID: 43399
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 19 };
	}
}
