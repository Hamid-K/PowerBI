using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC4 RID: 12228
	[ChildElementInfo(typeof(SdtBlock))]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AltChunk))]
	[ChildElementInfo(typeof(CustomXmlBlock))]
	[ChildElementInfo(typeof(Paragraph))]
	[ChildElementInfo(typeof(Table))]
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
	internal abstract class FootnoteEndnoteType : OpenXmlCompositeElement
	{
		// Token: 0x170093FD RID: 37885
		// (get) Token: 0x0601A868 RID: 108648 RVA: 0x00363804 File Offset: 0x00361A04
		internal override string[] AttributeTagNames
		{
			get
			{
				return FootnoteEndnoteType.attributeTagNames;
			}
		}

		// Token: 0x170093FE RID: 37886
		// (get) Token: 0x0601A869 RID: 108649 RVA: 0x0036380B File Offset: 0x00361A0B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FootnoteEndnoteType.attributeNamespaceIds;
			}
		}

		// Token: 0x170093FF RID: 37887
		// (get) Token: 0x0601A86A RID: 108650 RVA: 0x00363812 File Offset: 0x00361A12
		// (set) Token: 0x0601A86B RID: 108651 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<FootnoteEndnoteValues> Type
		{
			get
			{
				return (EnumValue<FootnoteEndnoteValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009400 RID: 37888
		// (get) Token: 0x0601A86C RID: 108652 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x0601A86D RID: 108653 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "id")]
		public IntegerValue Id
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601A86E RID: 108654 RVA: 0x00363824 File Offset: 0x00361A24
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "altChunk" == name)
			{
				return new AltChunk();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlBlock();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtBlock();
			}
			if (23 == namespaceId && "p" == name)
			{
				return new Paragraph();
			}
			if (23 == namespaceId && "tbl" == name)
			{
				return new Table();
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
			return null;
		}

		// Token: 0x0601A86F RID: 108655 RVA: 0x00363B7A File Offset: 0x00361D7A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<FootnoteEndnoteValues>();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A870 RID: 108656 RVA: 0x00293ECF File Offset: 0x002920CF
		protected FootnoteEndnoteType()
		{
		}

		// Token: 0x0601A871 RID: 108657 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected FootnoteEndnoteType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A872 RID: 108658 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected FootnoteEndnoteType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A873 RID: 108659 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected FootnoteEndnoteType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400AD57 RID: 44375
		private static string[] attributeTagNames = new string[] { "type", "id" };

		// Token: 0x0400AD58 RID: 44376
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
