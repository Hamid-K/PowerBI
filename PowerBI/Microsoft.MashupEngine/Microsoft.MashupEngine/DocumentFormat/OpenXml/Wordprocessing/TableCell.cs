using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED1 RID: 11985
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableCellProperties))]
	[ChildElementInfo(typeof(AltChunk))]
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
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	internal class TableCell : OpenXmlCompositeElement
	{
		// Token: 0x17008CDB RID: 36059
		// (get) Token: 0x060198EF RID: 104687 RVA: 0x0030D955 File Offset: 0x0030BB55
		public override string LocalName
		{
			get
			{
				return "tc";
			}
		}

		// Token: 0x17008CDC RID: 36060
		// (get) Token: 0x060198F0 RID: 104688 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CDD RID: 36061
		// (get) Token: 0x060198F1 RID: 104689 RVA: 0x0034FE88 File Offset: 0x0034E088
		internal override int ElementTypeId
		{
			get
			{
				return 11640;
			}
		}

		// Token: 0x060198F2 RID: 104690 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060198F3 RID: 104691 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCell()
		{
		}

		// Token: 0x060198F4 RID: 104692 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198F5 RID: 104693 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198F6 RID: 104694 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060198F7 RID: 104695 RVA: 0x0034FE90 File Offset: 0x0034E090
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tcPr" == name)
			{
				return new TableCellProperties();
			}
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

		// Token: 0x17008CDE RID: 36062
		// (get) Token: 0x060198F8 RID: 104696 RVA: 0x003501FE File Offset: 0x0034E3FE
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCell.eleTagNames;
			}
		}

		// Token: 0x17008CDF RID: 36063
		// (get) Token: 0x060198F9 RID: 104697 RVA: 0x00350205 File Offset: 0x0034E405
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCell.eleNamespaceIds;
			}
		}

		// Token: 0x17008CE0 RID: 36064
		// (get) Token: 0x060198FA RID: 104698 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008CE1 RID: 36065
		// (get) Token: 0x060198FB RID: 104699 RVA: 0x0035020C File Offset: 0x0034E40C
		// (set) Token: 0x060198FC RID: 104700 RVA: 0x00350215 File Offset: 0x0034E415
		public TableCellProperties TableCellProperties
		{
			get
			{
				return base.GetElement<TableCellProperties>(0);
			}
			set
			{
				base.SetElement<TableCellProperties>(0, value);
			}
		}

		// Token: 0x060198FD RID: 104701 RVA: 0x0035021F File Offset: 0x0034E41F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCell>(deep);
		}

		// Token: 0x0400A964 RID: 43364
		private const string tagName = "tc";

		// Token: 0x0400A965 RID: 43365
		private const byte tagNsId = 23;

		// Token: 0x0400A966 RID: 43366
		internal const int ElementTypeIdConst = 11640;

		// Token: 0x0400A967 RID: 43367
		private static readonly string[] eleTagNames = new string[]
		{
			"tcPr", "altChunk", "customXml", "sdt", "p", "tbl", "proofErr", "permStart", "permEnd", "bookmarkStart",
			"bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart",
			"customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins",
			"del", "moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel"
		};

		// Token: 0x0400A968 RID: 43368
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 52, 52, 52, 52, 23,
			23, 23, 23, 23, 52, 52
		};
	}
}
