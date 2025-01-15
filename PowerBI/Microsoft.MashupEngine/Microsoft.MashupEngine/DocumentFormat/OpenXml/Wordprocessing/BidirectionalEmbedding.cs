using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED8 RID: 11992
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(DeletedRun))]
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
	[ChildElementInfo(typeof(LimitUpper))]
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
	[ChildElementInfo(typeof(Superscript))]
	[ChildElementInfo(typeof(Matrix))]
	[ChildElementInfo(typeof(Nary))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Radical))]
	[ChildElementInfo(typeof(PreSubSuper))]
	[ChildElementInfo(typeof(Subscript))]
	[ChildElementInfo(typeof(SubSuperscript))]
	[ChildElementInfo(typeof(Phantom))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(BidirectionalOverride), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BidirectionalEmbedding), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SubDocumentReference))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BidirectionalEmbedding : OpenXmlCompositeElement
	{
		// Token: 0x17008D16 RID: 36118
		// (get) Token: 0x06019974 RID: 104820 RVA: 0x002FFF23 File Offset: 0x002FE123
		public override string LocalName
		{
			get
			{
				return "dir";
			}
		}

		// Token: 0x17008D17 RID: 36119
		// (get) Token: 0x06019975 RID: 104821 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D18 RID: 36120
		// (get) Token: 0x06019976 RID: 104822 RVA: 0x00352ACC File Offset: 0x00350CCC
		internal override int ElementTypeId
		{
			get
			{
				return 11647;
			}
		}

		// Token: 0x06019977 RID: 104823 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17008D19 RID: 36121
		// (get) Token: 0x06019978 RID: 104824 RVA: 0x00352AD3 File Offset: 0x00350CD3
		internal override string[] AttributeTagNames
		{
			get
			{
				return BidirectionalEmbedding.attributeTagNames;
			}
		}

		// Token: 0x17008D1A RID: 36122
		// (get) Token: 0x06019979 RID: 104825 RVA: 0x00352ADA File Offset: 0x00350CDA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BidirectionalEmbedding.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D1B RID: 36123
		// (get) Token: 0x0601997A RID: 104826 RVA: 0x00352494 File Offset: 0x00350694
		// (set) Token: 0x0601997B RID: 104827 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<DirectionValues> Val
		{
			get
			{
				return (EnumValue<DirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601997C RID: 104828 RVA: 0x00293ECF File Offset: 0x002920CF
		public BidirectionalEmbedding()
		{
		}

		// Token: 0x0601997D RID: 104829 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BidirectionalEmbedding(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601997E RID: 104830 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BidirectionalEmbedding(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601997F RID: 104831 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BidirectionalEmbedding(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019980 RID: 104832 RVA: 0x00352AE4 File Offset: 0x00350CE4
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

		// Token: 0x06019981 RID: 104833 RVA: 0x00352A6A File Offset: 0x00350C6A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019982 RID: 104834 RVA: 0x003530AA File Offset: 0x003512AA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BidirectionalEmbedding>(deep);
		}

		// Token: 0x0400A98D RID: 43405
		private const string tagName = "dir";

		// Token: 0x0400A98E RID: 43406
		private const byte tagNsId = 23;

		// Token: 0x0400A98F RID: 43407
		internal const int ElementTypeIdConst = 11647;

		// Token: 0x0400A990 RID: 43408
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A991 RID: 43409
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
