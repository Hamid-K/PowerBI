using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002965 RID: 10597
	[ChildElementInfo(typeof(MoveFromRangeStart))]
	[ChildElementInfo(typeof(ParagraphProperties))]
	[ChildElementInfo(typeof(OfficeMath))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(ProofError))]
	[ChildElementInfo(typeof(PermStart))]
	[ChildElementInfo(typeof(PermEnd))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Wordprocessing.ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Run))]
	internal class Paragraph : OpenXmlCompositeElement
	{
		// Token: 0x17006C27 RID: 27687
		// (get) Token: 0x060150D3 RID: 86227 RVA: 0x0031A5AC File Offset: 0x003187AC
		public override string LocalName
		{
			get
			{
				return "oMathPara";
			}
		}

		// Token: 0x17006C28 RID: 27688
		// (get) Token: 0x060150D4 RID: 86228 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C29 RID: 27689
		// (get) Token: 0x060150D5 RID: 86229 RVA: 0x0031A5B3 File Offset: 0x003187B3
		internal override int ElementTypeId
		{
			get
			{
				return 10861;
			}
		}

		// Token: 0x060150D6 RID: 86230 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060150D7 RID: 86231 RVA: 0x00293ECF File Offset: 0x002920CF
		public Paragraph()
		{
		}

		// Token: 0x060150D8 RID: 86232 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Paragraph(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150D9 RID: 86233 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Paragraph(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150DA RID: 86234 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Paragraph(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060150DB RID: 86235 RVA: 0x0031A5BC File Offset: 0x003187BC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "oMathParaPr" == name)
			{
				return new ParagraphProperties();
			}
			if (21 == namespaceId && "oMath" == name)
			{
				return new OfficeMath();
			}
			if (21 == namespaceId && "r" == name)
			{
				return new Run();
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
				return new DocumentFormat.OpenXml.Wordprocessing.ContentPart();
			}
			if (52 == namespaceId && "conflictIns" == name)
			{
				return new RunConflictInsertion();
			}
			if (52 == namespaceId && "conflictDel" == name)
			{
				return new RunConflictDeletion();
			}
			if (23 == namespaceId && "r" == name)
			{
				return new Run();
			}
			return null;
		}

		// Token: 0x17006C2A RID: 27690
		// (get) Token: 0x060150DC RID: 86236 RVA: 0x0031A8FA File Offset: 0x00318AFA
		internal override string[] ElementTagNames
		{
			get
			{
				return Paragraph.eleTagNames;
			}
		}

		// Token: 0x17006C2B RID: 27691
		// (get) Token: 0x060150DD RID: 86237 RVA: 0x0031A901 File Offset: 0x00318B01
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Paragraph.eleNamespaceIds;
			}
		}

		// Token: 0x17006C2C RID: 27692
		// (get) Token: 0x060150DE RID: 86238 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006C2D RID: 27693
		// (get) Token: 0x060150DF RID: 86239 RVA: 0x0031A908 File Offset: 0x00318B08
		// (set) Token: 0x060150E0 RID: 86240 RVA: 0x0031A911 File Offset: 0x00318B11
		public ParagraphProperties ParagraphProperties
		{
			get
			{
				return base.GetElement<ParagraphProperties>(0);
			}
			set
			{
				base.SetElement<ParagraphProperties>(0, value);
			}
		}

		// Token: 0x060150E1 RID: 86241 RVA: 0x0031A91B File Offset: 0x00318B1B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Paragraph>(deep);
		}

		// Token: 0x04009138 RID: 37176
		private const string tagName = "oMathPara";

		// Token: 0x04009139 RID: 37177
		private const byte tagNsId = 21;

		// Token: 0x0400913A RID: 37178
		internal const int ElementTypeIdConst = 10861;

		// Token: 0x0400913B RID: 37179
		private static readonly string[] eleTagNames = new string[]
		{
			"oMathParaPr", "oMath", "r", "proofErr", "permStart", "permEnd", "bookmarkStart", "bookmarkEnd", "commentRangeStart", "commentRangeEnd",
			"moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart", "customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd",
			"customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins", "del", "moveFrom", "moveTo",
			"contentPart", "conflictIns", "conflictDel", "r"
		};

		// Token: 0x0400913C RID: 37180
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			21, 21, 21, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 52, 52, 52, 52, 23, 23, 23, 23,
			23, 52, 52, 23
		};
	}
}
