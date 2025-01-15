using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ECB RID: 11979
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(SdtProperties))]
	[ChildElementInfo(typeof(SdtEndCharProperties))]
	[ChildElementInfo(typeof(SdtContentBlock))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
	[ChildElementInfo(typeof(MoveToRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[ChildElementInfo(typeof(CustomXmlDelRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	internal class SdtBlock : SdtElement
	{
		// Token: 0x17008CA1 RID: 36001
		// (get) Token: 0x06019870 RID: 104560 RVA: 0x0034C15B File Offset: 0x0034A35B
		public override string LocalName
		{
			get
			{
				return "sdt";
			}
		}

		// Token: 0x17008CA2 RID: 36002
		// (get) Token: 0x06019871 RID: 104561 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CA3 RID: 36003
		// (get) Token: 0x06019872 RID: 104562 RVA: 0x0034DFA8 File Offset: 0x0034C1A8
		internal override int ElementTypeId
		{
			get
			{
				return 11634;
			}
		}

		// Token: 0x06019873 RID: 104563 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019874 RID: 104564 RVA: 0x0034C169 File Offset: 0x0034A369
		public SdtBlock()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x06019875 RID: 104565 RVA: 0x0034C177 File Offset: 0x0034A377
		public SdtBlock(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019876 RID: 104566 RVA: 0x0034C180 File Offset: 0x0034A380
		public SdtBlock(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019877 RID: 104567 RVA: 0x0034C189 File Offset: 0x0034A389
		public SdtBlock(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019878 RID: 104568 RVA: 0x0034DFB0 File Offset: 0x0034C1B0
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
				return new SdtContentBlock();
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

		// Token: 0x17008CA4 RID: 36004
		// (get) Token: 0x06019879 RID: 104569 RVA: 0x0034E1E6 File Offset: 0x0034C3E6
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtBlock.eleTagNames;
			}
		}

		// Token: 0x17008CA5 RID: 36005
		// (get) Token: 0x0601987A RID: 104570 RVA: 0x0034E1ED File Offset: 0x0034C3ED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtBlock.eleNamespaceIds;
			}
		}

		// Token: 0x17008CA6 RID: 36006
		// (get) Token: 0x0601987B RID: 104571 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008CA7 RID: 36007
		// (get) Token: 0x0601987C RID: 104572 RVA: 0x0034E1F4 File Offset: 0x0034C3F4
		// (set) Token: 0x0601987D RID: 104573 RVA: 0x0034E1FD File Offset: 0x0034C3FD
		public SdtContentBlock SdtContentBlock
		{
			get
			{
				return base.GetElement<SdtContentBlock>(2);
			}
			set
			{
				base.SetElement<SdtContentBlock>(2, value);
			}
		}

		// Token: 0x0601987E RID: 104574 RVA: 0x0034E207 File Offset: 0x0034C407
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtBlock>(deep);
		}

		// Token: 0x0400A942 RID: 43330
		private const string tagName = "sdt";

		// Token: 0x0400A943 RID: 43331
		private const byte tagNsId = 23;

		// Token: 0x0400A944 RID: 43332
		internal const int ElementTypeIdConst = 11634;

		// Token: 0x0400A945 RID: 43333
		private static readonly string[] eleTagNames = new string[]
		{
			"sdtPr", "sdtEndPr", "sdtContent", "bookmarkStart", "bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart",
			"moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart",
			"customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd"
		};

		// Token: 0x0400A946 RID: 43334
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 52,
			52, 52, 52
		};
	}
}
