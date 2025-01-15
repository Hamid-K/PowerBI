using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ECF RID: 11983
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlProperties))]
	[ChildElementInfo(typeof(TableRow))]
	[ChildElementInfo(typeof(CustomXmlRow))]
	[ChildElementInfo(typeof(SdtRow))]
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
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[ChildElementInfo(typeof(CustomXmlDelRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(InsertedRun))]
	[ChildElementInfo(typeof(DeletedRun))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	internal class CustomXmlRow : CustomXmlElement
	{
		// Token: 0x17008CCC RID: 36044
		// (get) Token: 0x060198CE RID: 104654 RVA: 0x0034A455 File Offset: 0x00348655
		public override string LocalName
		{
			get
			{
				return "customXml";
			}
		}

		// Token: 0x17008CCD RID: 36045
		// (get) Token: 0x060198CF RID: 104655 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CCE RID: 36046
		// (get) Token: 0x060198D0 RID: 104656 RVA: 0x0034F62B File Offset: 0x0034D82B
		internal override int ElementTypeId
		{
			get
			{
				return 11638;
			}
		}

		// Token: 0x060198D1 RID: 104657 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008CCF RID: 36047
		// (get) Token: 0x060198D2 RID: 104658 RVA: 0x0034F632 File Offset: 0x0034D832
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomXmlRow.attributeTagNames;
			}
		}

		// Token: 0x17008CD0 RID: 36048
		// (get) Token: 0x060198D3 RID: 104659 RVA: 0x0034F639 File Offset: 0x0034D839
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomXmlRow.attributeNamespaceIds;
			}
		}

		// Token: 0x060198D4 RID: 104660 RVA: 0x0034A471 File Offset: 0x00348671
		public CustomXmlRow()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x060198D5 RID: 104661 RVA: 0x0034A47F File Offset: 0x0034867F
		public CustomXmlRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198D6 RID: 104662 RVA: 0x0034A488 File Offset: 0x00348688
		public CustomXmlRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198D7 RID: 104663 RVA: 0x0034A491 File Offset: 0x00348691
		public CustomXmlRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060198D8 RID: 104664 RVA: 0x0034F640 File Offset: 0x0034D840
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "customXmlPr" == name)
			{
				return new CustomXmlProperties();
			}
			if (23 == namespaceId && "tr" == name)
			{
				return new TableRow();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlRow();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtRow();
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

		// Token: 0x17008CD1 RID: 36049
		// (get) Token: 0x060198D9 RID: 104665 RVA: 0x0034F97E File Offset: 0x0034DB7E
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomXmlRow.eleTagNames;
			}
		}

		// Token: 0x17008CD2 RID: 36050
		// (get) Token: 0x060198DA RID: 104666 RVA: 0x0034F985 File Offset: 0x0034DB85
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomXmlRow.eleNamespaceIds;
			}
		}

		// Token: 0x17008CD3 RID: 36051
		// (get) Token: 0x060198DB RID: 104667 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x060198DC RID: 104668 RVA: 0x0034AA28 File Offset: 0x00348C28
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

		// Token: 0x060198DD RID: 104669 RVA: 0x0034F98C File Offset: 0x0034DB8C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlRow>(deep);
		}

		// Token: 0x0400A958 RID: 43352
		private const string tagName = "customXml";

		// Token: 0x0400A959 RID: 43353
		private const byte tagNsId = 23;

		// Token: 0x0400A95A RID: 43354
		internal const int ElementTypeIdConst = 11638;

		// Token: 0x0400A95B RID: 43355
		private static string[] attributeTagNames = new string[] { "uri", "element" };

		// Token: 0x0400A95C RID: 43356
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };

		// Token: 0x0400A95D RID: 43357
		private static readonly string[] eleTagNames = new string[]
		{
			"customXmlPr", "tr", "customXml", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart", "bookmarkEnd", "commentRangeStart",
			"commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart",
			"customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins", "del", "moveFrom",
			"moveTo", "contentPart", "conflictIns", "conflictDel"
		};

		// Token: 0x0400A95E RID: 43358
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 52, 52, 52, 52, 23, 23, 23,
			23, 23, 52, 52
		};
	}
}
