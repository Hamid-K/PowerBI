using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC9 RID: 11977
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[ChildElementInfo(typeof(SdtProperties))]
	[ChildElementInfo(typeof(SdtEndCharProperties))]
	[ChildElementInfo(typeof(SdtContentRun))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
	[ChildElementInfo(typeof(MoveToRangeStart))]
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeStart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[ChildElementInfo(typeof(CustomXmlDelRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	internal class SdtRun : SdtElement
	{
		// Token: 0x17008C92 RID: 35986
		// (get) Token: 0x0601984F RID: 104527 RVA: 0x0034C15B File Offset: 0x0034A35B
		public override string LocalName
		{
			get
			{
				return "sdt";
			}
		}

		// Token: 0x17008C93 RID: 35987
		// (get) Token: 0x06019850 RID: 104528 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C94 RID: 35988
		// (get) Token: 0x06019851 RID: 104529 RVA: 0x0034D727 File Offset: 0x0034B927
		internal override int ElementTypeId
		{
			get
			{
				return 11632;
			}
		}

		// Token: 0x06019852 RID: 104530 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019853 RID: 104531 RVA: 0x0034C169 File Offset: 0x0034A369
		public SdtRun()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x06019854 RID: 104532 RVA: 0x0034C177 File Offset: 0x0034A377
		public SdtRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019855 RID: 104533 RVA: 0x0034C180 File Offset: 0x0034A380
		public SdtRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019856 RID: 104534 RVA: 0x0034C189 File Offset: 0x0034A389
		public SdtRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019857 RID: 104535 RVA: 0x0034D730 File Offset: 0x0034B930
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
				return new SdtContentRun();
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

		// Token: 0x17008C95 RID: 35989
		// (get) Token: 0x06019858 RID: 104536 RVA: 0x0034D966 File Offset: 0x0034BB66
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtRun.eleTagNames;
			}
		}

		// Token: 0x17008C96 RID: 35990
		// (get) Token: 0x06019859 RID: 104537 RVA: 0x0034D96D File Offset: 0x0034BB6D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtRun.eleNamespaceIds;
			}
		}

		// Token: 0x17008C97 RID: 35991
		// (get) Token: 0x0601985A RID: 104538 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008C98 RID: 35992
		// (get) Token: 0x0601985B RID: 104539 RVA: 0x0034D974 File Offset: 0x0034BB74
		// (set) Token: 0x0601985C RID: 104540 RVA: 0x0034D97D File Offset: 0x0034BB7D
		public SdtContentRun SdtContentRun
		{
			get
			{
				return base.GetElement<SdtContentRun>(2);
			}
			set
			{
				base.SetElement<SdtContentRun>(2, value);
			}
		}

		// Token: 0x0601985D RID: 104541 RVA: 0x0034D987 File Offset: 0x0034BB87
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtRun>(deep);
		}

		// Token: 0x0400A936 RID: 43318
		private const string tagName = "sdt";

		// Token: 0x0400A937 RID: 43319
		private const byte tagNsId = 23;

		// Token: 0x0400A938 RID: 43320
		internal const int ElementTypeIdConst = 11632;

		// Token: 0x0400A939 RID: 43321
		private static readonly string[] eleTagNames = new string[]
		{
			"sdtPr", "sdtEndPr", "sdtContent", "bookmarkStart", "bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart",
			"moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart",
			"customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd"
		};

		// Token: 0x0400A93A RID: 43322
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 52,
			52, 52, 52
		};
	}
}
