using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED0 RID: 11984
	[ChildElementInfo(typeof(MoveToRangeStart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SdtEndCharProperties))]
	[ChildElementInfo(typeof(SdtContentRow))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(SdtProperties))]
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
	internal class SdtRow : SdtElement
	{
		// Token: 0x17008CD4 RID: 36052
		// (get) Token: 0x060198DF RID: 104671 RVA: 0x0034C15B File Offset: 0x0034A35B
		public override string LocalName
		{
			get
			{
				return "sdt";
			}
		}

		// Token: 0x17008CD5 RID: 36053
		// (get) Token: 0x060198E0 RID: 104672 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CD6 RID: 36054
		// (get) Token: 0x060198E1 RID: 104673 RVA: 0x0034FB27 File Offset: 0x0034DD27
		internal override int ElementTypeId
		{
			get
			{
				return 11639;
			}
		}

		// Token: 0x060198E2 RID: 104674 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060198E3 RID: 104675 RVA: 0x0034C169 File Offset: 0x0034A369
		public SdtRow()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x060198E4 RID: 104676 RVA: 0x0034C177 File Offset: 0x0034A377
		public SdtRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198E5 RID: 104677 RVA: 0x0034C180 File Offset: 0x0034A380
		public SdtRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060198E6 RID: 104678 RVA: 0x0034C189 File Offset: 0x0034A389
		public SdtRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060198E7 RID: 104679 RVA: 0x0034FB30 File Offset: 0x0034DD30
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "sdtPr" == name)
			{
				return new SdtProperties();
			}
			if (23 == namespaceId && "sdtEndPr" == name)
			{
				return new SdtEndCharProperties();
			}
			if (23 == namespaceId && "sdtContent" == name)
			{
				return new SdtContentRow();
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
			return null;
		}

		// Token: 0x17008CD7 RID: 36055
		// (get) Token: 0x060198E8 RID: 104680 RVA: 0x0034FD66 File Offset: 0x0034DF66
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtRow.eleTagNames;
			}
		}

		// Token: 0x17008CD8 RID: 36056
		// (get) Token: 0x060198E9 RID: 104681 RVA: 0x0034FD6D File Offset: 0x0034DF6D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtRow.eleNamespaceIds;
			}
		}

		// Token: 0x17008CD9 RID: 36057
		// (get) Token: 0x060198EA RID: 104682 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008CDA RID: 36058
		// (get) Token: 0x060198EB RID: 104683 RVA: 0x0034FD74 File Offset: 0x0034DF74
		// (set) Token: 0x060198EC RID: 104684 RVA: 0x0034FD7D File Offset: 0x0034DF7D
		public SdtContentRow SdtContentRow
		{
			get
			{
				return base.GetElement<SdtContentRow>(2);
			}
			set
			{
				base.SetElement<SdtContentRow>(2, value);
			}
		}

		// Token: 0x060198ED RID: 104685 RVA: 0x0034FD87 File Offset: 0x0034DF87
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtRow>(deep);
		}

		// Token: 0x0400A95F RID: 43359
		private const string tagName = "sdt";

		// Token: 0x0400A960 RID: 43360
		private const byte tagNsId = 23;

		// Token: 0x0400A961 RID: 43361
		internal const int ElementTypeIdConst = 11639;

		// Token: 0x0400A962 RID: 43362
		private static readonly string[] eleTagNames = new string[]
		{
			"sdtPr", "sdtEndPr", "sdtContent", "bookmarkStart", "bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart",
			"moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart",
			"customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd"
		};

		// Token: 0x0400A963 RID: 43363
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 52,
			52, 52, 52
		};
	}
}
