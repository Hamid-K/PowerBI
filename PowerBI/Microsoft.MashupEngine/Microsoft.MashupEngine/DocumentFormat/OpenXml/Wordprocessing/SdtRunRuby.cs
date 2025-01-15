using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EBE RID: 11966
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlInsRangeStart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SdtProperties))]
	[ChildElementInfo(typeof(SdtEndCharProperties))]
	[ChildElementInfo(typeof(SdtContentRunRuby))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
	[ChildElementInfo(typeof(MoveToRangeStart))]
	[ChildElementInfo(typeof(CustomXmlInsRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlDelRangeStart))]
	[ChildElementInfo(typeof(CustomXmlDelRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveFromRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeStart))]
	[ChildElementInfo(typeof(CustomXmlMoveToRangeEnd))]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	internal class SdtRunRuby : SdtElement
	{
		// Token: 0x17008C51 RID: 35921
		// (get) Token: 0x060197B9 RID: 104377 RVA: 0x0034C15B File Offset: 0x0034A35B
		public override string LocalName
		{
			get
			{
				return "sdt";
			}
		}

		// Token: 0x17008C52 RID: 35922
		// (get) Token: 0x060197BA RID: 104378 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C53 RID: 35923
		// (get) Token: 0x060197BB RID: 104379 RVA: 0x0034C162 File Offset: 0x0034A362
		internal override int ElementTypeId
		{
			get
			{
				return 11622;
			}
		}

		// Token: 0x060197BC RID: 104380 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060197BD RID: 104381 RVA: 0x0034C169 File Offset: 0x0034A369
		public SdtRunRuby()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x060197BE RID: 104382 RVA: 0x0034C177 File Offset: 0x0034A377
		public SdtRunRuby(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060197BF RID: 104383 RVA: 0x0034C180 File Offset: 0x0034A380
		public SdtRunRuby(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060197C0 RID: 104384 RVA: 0x0034C189 File Offset: 0x0034A389
		public SdtRunRuby(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060197C1 RID: 104385 RVA: 0x0034C194 File Offset: 0x0034A394
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
				return new SdtContentRunRuby();
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

		// Token: 0x17008C54 RID: 35924
		// (get) Token: 0x060197C2 RID: 104386 RVA: 0x0034C3CA File Offset: 0x0034A5CA
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtRunRuby.eleTagNames;
			}
		}

		// Token: 0x17008C55 RID: 35925
		// (get) Token: 0x060197C3 RID: 104387 RVA: 0x0034C3D1 File Offset: 0x0034A5D1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtRunRuby.eleNamespaceIds;
			}
		}

		// Token: 0x17008C56 RID: 35926
		// (get) Token: 0x060197C4 RID: 104388 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008C57 RID: 35927
		// (get) Token: 0x060197C5 RID: 104389 RVA: 0x0034C3D8 File Offset: 0x0034A5D8
		// (set) Token: 0x060197C6 RID: 104390 RVA: 0x0034C3E1 File Offset: 0x0034A5E1
		public SdtContentRunRuby SdtContentRunRuby
		{
			get
			{
				return base.GetElement<SdtContentRunRuby>(2);
			}
			set
			{
				base.SetElement<SdtContentRunRuby>(2, value);
			}
		}

		// Token: 0x060197C7 RID: 104391 RVA: 0x0034C3EB File Offset: 0x0034A5EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtRunRuby>(deep);
		}

		// Token: 0x0400A908 RID: 43272
		private const string tagName = "sdt";

		// Token: 0x0400A909 RID: 43273
		private const byte tagNsId = 23;

		// Token: 0x0400A90A RID: 43274
		internal const int ElementTypeIdConst = 11622;

		// Token: 0x0400A90B RID: 43275
		private static readonly string[] eleTagNames = new string[]
		{
			"sdtPr", "sdtEndPr", "sdtContent", "bookmarkStart", "bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart",
			"moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart",
			"customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd"
		};

		// Token: 0x0400A90C RID: 43276
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 52,
			52, 52, 52
		};
	}
}
