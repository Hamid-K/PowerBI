using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED2 RID: 11986
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlProperties))]
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
	[ChildElementInfo(typeof(DeletedRun))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	internal class CustomXmlCell : CustomXmlElement
	{
		// Token: 0x17008CE2 RID: 36066
		// (get) Token: 0x060198FF RID: 104703 RVA: 0x0034A455 File Offset: 0x00348655
		public override string LocalName
		{
			get
			{
				return "customXml";
			}
		}

		// Token: 0x17008CE3 RID: 36067
		// (get) Token: 0x06019900 RID: 104704 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CE4 RID: 36068
		// (get) Token: 0x06019901 RID: 104705 RVA: 0x00350395 File Offset: 0x0034E595
		internal override int ElementTypeId
		{
			get
			{
				return 11641;
			}
		}

		// Token: 0x06019902 RID: 104706 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008CE5 RID: 36069
		// (get) Token: 0x06019903 RID: 104707 RVA: 0x0035039C File Offset: 0x0034E59C
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomXmlCell.attributeTagNames;
			}
		}

		// Token: 0x17008CE6 RID: 36070
		// (get) Token: 0x06019904 RID: 104708 RVA: 0x003503A3 File Offset: 0x0034E5A3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomXmlCell.attributeNamespaceIds;
			}
		}

		// Token: 0x06019905 RID: 104709 RVA: 0x0034A471 File Offset: 0x00348671
		public CustomXmlCell()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x06019906 RID: 104710 RVA: 0x0034A47F File Offset: 0x0034867F
		public CustomXmlCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019907 RID: 104711 RVA: 0x0034A488 File Offset: 0x00348688
		public CustomXmlCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019908 RID: 104712 RVA: 0x0034A491 File Offset: 0x00348691
		public CustomXmlCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019909 RID: 104713 RVA: 0x003503AC File Offset: 0x0034E5AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "customXmlPr" == name)
			{
				return new CustomXmlProperties();
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

		// Token: 0x17008CE7 RID: 36071
		// (get) Token: 0x0601990A RID: 104714 RVA: 0x003506EA File Offset: 0x0034E8EA
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomXmlCell.eleTagNames;
			}
		}

		// Token: 0x17008CE8 RID: 36072
		// (get) Token: 0x0601990B RID: 104715 RVA: 0x003506F1 File Offset: 0x0034E8F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomXmlCell.eleNamespaceIds;
			}
		}

		// Token: 0x17008CE9 RID: 36073
		// (get) Token: 0x0601990C RID: 104716 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x0601990D RID: 104717 RVA: 0x0034AA28 File Offset: 0x00348C28
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

		// Token: 0x0601990E RID: 104718 RVA: 0x003506F8 File Offset: 0x0034E8F8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlCell>(deep);
		}

		// Token: 0x0400A969 RID: 43369
		private const string tagName = "customXml";

		// Token: 0x0400A96A RID: 43370
		private const byte tagNsId = 23;

		// Token: 0x0400A96B RID: 43371
		internal const int ElementTypeIdConst = 11641;

		// Token: 0x0400A96C RID: 43372
		private static string[] attributeTagNames = new string[] { "uri", "element" };

		// Token: 0x0400A96D RID: 43373
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };

		// Token: 0x0400A96E RID: 43374
		private static readonly string[] eleTagNames = new string[]
		{
			"customXmlPr", "tc", "customXml", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart", "bookmarkEnd", "commentRangeStart",
			"commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart",
			"customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins", "del", "moveFrom",
			"moveTo", "contentPart", "conflictIns", "conflictDel"
		};

		// Token: 0x0400A96F RID: 43375
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 52, 52, 52, 52, 23, 23, 23,
			23, 23, 52, 52
		};
	}
}
