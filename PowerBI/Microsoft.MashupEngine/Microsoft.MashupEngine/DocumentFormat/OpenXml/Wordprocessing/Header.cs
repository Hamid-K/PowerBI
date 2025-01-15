using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F15 RID: 12053
	[ChildElementInfo(typeof(MoveFromRangeEnd))]
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
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	internal class Header : OpenXmlPartRootElement
	{
		// Token: 0x17008E4C RID: 36428
		// (get) Token: 0x06019C10 RID: 105488 RVA: 0x00354C13 File Offset: 0x00352E13
		public override string LocalName
		{
			get
			{
				return "hdr";
			}
		}

		// Token: 0x17008E4D RID: 36429
		// (get) Token: 0x06019C11 RID: 105489 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E4E RID: 36430
		// (get) Token: 0x06019C12 RID: 105490 RVA: 0x00354C1A File Offset: 0x00352E1A
		internal override int ElementTypeId
		{
			get
			{
				return 11695;
			}
		}

		// Token: 0x06019C13 RID: 105491 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019C14 RID: 105492 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Header(HeaderPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019C15 RID: 105493 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(HeaderPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E4F RID: 36431
		// (get) Token: 0x06019C16 RID: 105494 RVA: 0x00354C21 File Offset: 0x00352E21
		// (set) Token: 0x06019C17 RID: 105495 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public HeaderPart HeaderPart
		{
			get
			{
				return base.OpenXmlPart as HeaderPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019C18 RID: 105496 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Header(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C19 RID: 105497 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Header(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C1A RID: 105498 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Header(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019C1B RID: 105499 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Header()
		{
		}

		// Token: 0x06019C1C RID: 105500 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(HeaderPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019C1D RID: 105501 RVA: 0x00354C30 File Offset: 0x00352E30
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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

		// Token: 0x06019C1E RID: 105502 RVA: 0x00354F86 File Offset: 0x00353186
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Header>(deep);
		}

		// Token: 0x0400AA6B RID: 43627
		private const string tagName = "hdr";

		// Token: 0x0400AA6C RID: 43628
		private const byte tagNsId = 23;

		// Token: 0x0400AA6D RID: 43629
		internal const int ElementTypeIdConst = 11695;
	}
}
