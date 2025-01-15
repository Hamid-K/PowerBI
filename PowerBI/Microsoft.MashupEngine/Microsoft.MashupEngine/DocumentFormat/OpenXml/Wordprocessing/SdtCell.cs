using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED3 RID: 11987
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SdtEndCharProperties))]
	[ChildElementInfo(typeof(SdtContentCell))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
	[ChildElementInfo(typeof(MoveToRangeStart))]
	[ChildElementInfo(typeof(SdtProperties))]
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
	internal class SdtCell : SdtElement
	{
		// Token: 0x17008CEA RID: 36074
		// (get) Token: 0x06019910 RID: 104720 RVA: 0x0034C15B File Offset: 0x0034A35B
		public override string LocalName
		{
			get
			{
				return "sdt";
			}
		}

		// Token: 0x17008CEB RID: 36075
		// (get) Token: 0x06019911 RID: 104721 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CEC RID: 36076
		// (get) Token: 0x06019912 RID: 104722 RVA: 0x00350893 File Offset: 0x0034EA93
		internal override int ElementTypeId
		{
			get
			{
				return 11642;
			}
		}

		// Token: 0x06019913 RID: 104723 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019914 RID: 104724 RVA: 0x0034C169 File Offset: 0x0034A369
		public SdtCell()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x06019915 RID: 104725 RVA: 0x0034C177 File Offset: 0x0034A377
		public SdtCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019916 RID: 104726 RVA: 0x0034C180 File Offset: 0x0034A380
		public SdtCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019917 RID: 104727 RVA: 0x0034C189 File Offset: 0x0034A389
		public SdtCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019918 RID: 104728 RVA: 0x0035089C File Offset: 0x0034EA9C
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
				return new SdtContentCell();
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

		// Token: 0x17008CED RID: 36077
		// (get) Token: 0x06019919 RID: 104729 RVA: 0x00350AD2 File Offset: 0x0034ECD2
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtCell.eleTagNames;
			}
		}

		// Token: 0x17008CEE RID: 36078
		// (get) Token: 0x0601991A RID: 104730 RVA: 0x00350AD9 File Offset: 0x0034ECD9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtCell.eleNamespaceIds;
			}
		}

		// Token: 0x17008CEF RID: 36079
		// (get) Token: 0x0601991B RID: 104731 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008CF0 RID: 36080
		// (get) Token: 0x0601991C RID: 104732 RVA: 0x00350AE0 File Offset: 0x0034ECE0
		// (set) Token: 0x0601991D RID: 104733 RVA: 0x00350AE9 File Offset: 0x0034ECE9
		public SdtContentCell SdtContentCell
		{
			get
			{
				return base.GetElement<SdtContentCell>(2);
			}
			set
			{
				base.SetElement<SdtContentCell>(2, value);
			}
		}

		// Token: 0x0601991E RID: 104734 RVA: 0x00350AF3 File Offset: 0x0034ECF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtCell>(deep);
		}

		// Token: 0x0400A970 RID: 43376
		private const string tagName = "sdt";

		// Token: 0x0400A971 RID: 43377
		private const byte tagNsId = 23;

		// Token: 0x0400A972 RID: 43378
		internal const int ElementTypeIdConst = 11642;

		// Token: 0x0400A973 RID: 43379
		private static readonly string[] eleTagNames = new string[]
		{
			"sdtPr", "sdtEndPr", "sdtContent", "bookmarkStart", "bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart",
			"moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart",
			"customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd"
		};

		// Token: 0x0400A974 RID: 43380
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 52,
			52, 52, 52
		};
	}
}
