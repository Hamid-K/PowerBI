using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ECE RID: 11982
	[ChildElementInfo(typeof(DeletedRun))]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TablePropertyExceptions))]
	[ChildElementInfo(typeof(TableRowProperties))]
	[ChildElementInfo(typeof(TableCell))]
	[ChildElementInfo(typeof(CustomXmlCell))]
	[ChildElementInfo(typeof(SdtCell))]
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
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	internal class TableRow : OpenXmlCompositeElement
	{
		// Token: 0x17008CBC RID: 36028
		// (get) Token: 0x060198AD RID: 104621 RVA: 0x0030E261 File Offset: 0x0030C461
		public override string LocalName
		{
			get
			{
				return "tr";
			}
		}

		// Token: 0x17008CBD RID: 36029
		// (get) Token: 0x060198AE RID: 104622 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CBE RID: 36030
		// (get) Token: 0x060198AF RID: 104623 RVA: 0x0034F023 File Offset: 0x0034D223
		internal override int ElementTypeId
		{
			get
			{
				return 11637;
			}
		}

		// Token: 0x060198B0 RID: 104624 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008CBF RID: 36031
		// (get) Token: 0x060198B1 RID: 104625 RVA: 0x0034F02A File Offset: 0x0034D22A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableRow.attributeTagNames;
			}
		}

		// Token: 0x17008CC0 RID: 36032
		// (get) Token: 0x060198B2 RID: 104626 RVA: 0x0034F031 File Offset: 0x0034D231
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableRow.attributeNamespaceIds;
			}
		}

		// Token: 0x17008CC1 RID: 36033
		// (get) Token: 0x060198B3 RID: 104627 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x060198B4 RID: 104628 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "rsidRPr")]
		public HexBinaryValue RsidTableRowMarkRevision
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

		// Token: 0x17008CC2 RID: 36034
		// (get) Token: 0x060198B5 RID: 104629 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x060198B6 RID: 104630 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "rsidR")]
		public HexBinaryValue RsidTableRowAddition
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

		// Token: 0x17008CC3 RID: 36035
		// (get) Token: 0x060198B7 RID: 104631 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x060198B8 RID: 104632 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "rsidDel")]
		public HexBinaryValue RsidTableRowDeletion
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

		// Token: 0x17008CC4 RID: 36036
		// (get) Token: 0x060198B9 RID: 104633 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x060198BA RID: 104634 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "rsidTr")]
		public HexBinaryValue RsidTableRowProperties
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

		// Token: 0x17008CC5 RID: 36037
		// (get) Token: 0x060198BB RID: 104635 RVA: 0x002EB784 File Offset: 0x002E9984
		// (set) Token: 0x060198BC RID: 104636 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(52, "paraId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue ParagraphId
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

		// Token: 0x17008CC6 RID: 36038
		// (get) Token: 0x060198BD RID: 104637 RVA: 0x003137E6 File Offset: 0x003119E6
		// (set) Token: 0x060198BE RID: 104638 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(52, "textId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue TextId
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

		// Token: 0x060198BF RID: 104639 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableRow()
		{
		}

		// Token: 0x060198C0 RID: 104640 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198C1 RID: 104641 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198C2 RID: 104642 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060198C3 RID: 104643 RVA: 0x0034F038 File Offset: 0x0034D238
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tblPrEx" == name)
			{
				return new TablePropertyExceptions();
			}
			if (23 == namespaceId && "trPr" == name)
			{
				return new TableRowProperties();
			}
			if (23 == namespaceId && "tc" == name)
			{
				return new TableCell();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlCell();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtCell();
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

		// Token: 0x17008CC7 RID: 36039
		// (get) Token: 0x060198C4 RID: 104644 RVA: 0x0034F38E File Offset: 0x0034D58E
		internal override string[] ElementTagNames
		{
			get
			{
				return TableRow.eleTagNames;
			}
		}

		// Token: 0x17008CC8 RID: 36040
		// (get) Token: 0x060198C5 RID: 104645 RVA: 0x0034F395 File Offset: 0x0034D595
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableRow.eleNamespaceIds;
			}
		}

		// Token: 0x17008CC9 RID: 36041
		// (get) Token: 0x060198C6 RID: 104646 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008CCA RID: 36042
		// (get) Token: 0x060198C7 RID: 104647 RVA: 0x0034F39C File Offset: 0x0034D59C
		// (set) Token: 0x060198C8 RID: 104648 RVA: 0x0034F3A5 File Offset: 0x0034D5A5
		public TablePropertyExceptions TablePropertyExceptions
		{
			get
			{
				return base.GetElement<TablePropertyExceptions>(0);
			}
			set
			{
				base.SetElement<TablePropertyExceptions>(0, value);
			}
		}

		// Token: 0x17008CCB RID: 36043
		// (get) Token: 0x060198C9 RID: 104649 RVA: 0x0034F3AF File Offset: 0x0034D5AF
		// (set) Token: 0x060198CA RID: 104650 RVA: 0x0034F3B8 File Offset: 0x0034D5B8
		public TableRowProperties TableRowProperties
		{
			get
			{
				return base.GetElement<TableRowProperties>(1);
			}
			set
			{
				base.SetElement<TableRowProperties>(1, value);
			}
		}

		// Token: 0x060198CB RID: 104651 RVA: 0x0034F3C4 File Offset: 0x0034D5C4
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
			if (23 == namespaceId && "rsidTr" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060198CC RID: 104652 RVA: 0x0034F469 File Offset: 0x0034D669
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableRow>(deep);
		}

		// Token: 0x0400A951 RID: 43345
		private const string tagName = "tr";

		// Token: 0x0400A952 RID: 43346
		private const byte tagNsId = 23;

		// Token: 0x0400A953 RID: 43347
		internal const int ElementTypeIdConst = 11637;

		// Token: 0x0400A954 RID: 43348
		private static string[] attributeTagNames = new string[] { "rsidRPr", "rsidR", "rsidDel", "rsidTr", "paraId", "textId" };

		// Token: 0x0400A955 RID: 43349
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 52, 52 };

		// Token: 0x0400A956 RID: 43350
		private static readonly string[] eleTagNames = new string[]
		{
			"tblPrEx", "trPr", "tc", "customXml", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart", "bookmarkEnd",
			"commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd",
			"customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins", "del",
			"moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel"
		};

		// Token: 0x0400A957 RID: 43351
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 52, 52, 52, 52, 23, 23,
			23, 23, 23, 52, 52
		};
	}
}
