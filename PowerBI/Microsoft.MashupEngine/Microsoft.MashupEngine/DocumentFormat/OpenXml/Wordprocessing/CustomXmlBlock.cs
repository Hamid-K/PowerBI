using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ECA RID: 11978
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(InsertedRun))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlProperties))]
	[ChildElementInfo(typeof(CustomXmlBlock))]
	[ChildElementInfo(typeof(SdtBlock))]
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
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[ChildElementInfo(typeof(CustomXmlDelRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeEnd))]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeStart))]
	[ChildElementInfo(typeof(DeletedRun))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	internal class CustomXmlBlock : CustomXmlElement
	{
		// Token: 0x17008C99 RID: 35993
		// (get) Token: 0x0601985F RID: 104543 RVA: 0x0034A455 File Offset: 0x00348655
		public override string LocalName
		{
			get
			{
				return "customXml";
			}
		}

		// Token: 0x17008C9A RID: 35994
		// (get) Token: 0x06019860 RID: 104544 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C9B RID: 35995
		// (get) Token: 0x06019861 RID: 104545 RVA: 0x0034DA88 File Offset: 0x0034BC88
		internal override int ElementTypeId
		{
			get
			{
				return 11633;
			}
		}

		// Token: 0x06019862 RID: 104546 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C9C RID: 35996
		// (get) Token: 0x06019863 RID: 104547 RVA: 0x0034DA8F File Offset: 0x0034BC8F
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomXmlBlock.attributeTagNames;
			}
		}

		// Token: 0x17008C9D RID: 35997
		// (get) Token: 0x06019864 RID: 104548 RVA: 0x0034DA96 File Offset: 0x0034BC96
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomXmlBlock.attributeNamespaceIds;
			}
		}

		// Token: 0x06019865 RID: 104549 RVA: 0x0034A471 File Offset: 0x00348671
		public CustomXmlBlock()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x06019866 RID: 104550 RVA: 0x0034A47F File Offset: 0x0034867F
		public CustomXmlBlock(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019867 RID: 104551 RVA: 0x0034A488 File Offset: 0x00348688
		public CustomXmlBlock(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019868 RID: 104552 RVA: 0x0034A491 File Offset: 0x00348691
		public CustomXmlBlock(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019869 RID: 104553 RVA: 0x0034DAA0 File Offset: 0x0034BCA0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "customXmlPr" == name)
			{
				return new CustomXmlProperties();
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

		// Token: 0x17008C9E RID: 35998
		// (get) Token: 0x0601986A RID: 104554 RVA: 0x0034DDF6 File Offset: 0x0034BFF6
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomXmlBlock.eleTagNames;
			}
		}

		// Token: 0x17008C9F RID: 35999
		// (get) Token: 0x0601986B RID: 104555 RVA: 0x0034DDFD File Offset: 0x0034BFFD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomXmlBlock.eleNamespaceIds;
			}
		}

		// Token: 0x17008CA0 RID: 36000
		// (get) Token: 0x0601986C RID: 104556 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x0601986D RID: 104557 RVA: 0x0034AA28 File Offset: 0x00348C28
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "element" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601986E RID: 104558 RVA: 0x0034DE04 File Offset: 0x0034C004
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlBlock>(deep);
		}

		// Token: 0x0400A93B RID: 43323
		private const string tagName = "customXml";

		// Token: 0x0400A93C RID: 43324
		private const byte tagNsId = 23;

		// Token: 0x0400A93D RID: 43325
		internal const int ElementTypeIdConst = 11633;

		// Token: 0x0400A93E RID: 43326
		private static string[] attributeTagNames = new string[] { "uri", "element" };

		// Token: 0x0400A93F RID: 43327
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };

		// Token: 0x0400A940 RID: 43328
		private static readonly string[] eleTagNames = new string[]
		{
			"customXmlPr", "customXml", "sdt", "p", "tbl", "proofErr", "permStart", "permEnd", "bookmarkStart", "bookmarkEnd",
			"commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd",
			"customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins", "del",
			"moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel"
		};

		// Token: 0x0400A941 RID: 43329
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 52, 52, 52, 52, 23, 23,
			23, 23, 23, 52, 52
		};
	}
}
