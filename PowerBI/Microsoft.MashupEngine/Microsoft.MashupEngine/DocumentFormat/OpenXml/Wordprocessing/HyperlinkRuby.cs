using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EBB RID: 11963
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlRuby))]
	[ChildElementInfo(typeof(SimpleFieldRuby))]
	[ChildElementInfo(typeof(HyperlinkRuby))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(SdtRunRuby))]
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
	[ChildElementInfo(typeof(Delimiter))]
	[ChildElementInfo(typeof(InsertedRun))]
	[ChildElementInfo(typeof(DeletedRun))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[ChildElementInfo(typeof(MoveToRun))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictInsertion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Paragraph))]
	[ChildElementInfo(typeof(OfficeMath))]
	[ChildElementInfo(typeof(Accent))]
	[ChildElementInfo(typeof(Bar))]
	[ChildElementInfo(typeof(Box))]
	[ChildElementInfo(typeof(BorderBox))]
	[ChildElementInfo(typeof(Matrix))]
	[ChildElementInfo(typeof(EquationArray))]
	[ChildElementInfo(typeof(Fraction))]
	[ChildElementInfo(typeof(MathFunction))]
	[ChildElementInfo(typeof(GroupChar))]
	[ChildElementInfo(typeof(LimitLower))]
	[ChildElementInfo(typeof(LimitUpper))]
	[ChildElementInfo(typeof(PreSubSuper))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(Phantom))]
	[ChildElementInfo(typeof(Radical))]
	[ChildElementInfo(typeof(Nary))]
	[ChildElementInfo(typeof(Subscript))]
	[ChildElementInfo(typeof(SubSuperscript))]
	[ChildElementInfo(typeof(Superscript))]
	internal class HyperlinkRuby : OpenXmlCompositeElement
	{
		// Token: 0x17008C38 RID: 35896
		// (get) Token: 0x0601977F RID: 104319 RVA: 0x002D9347 File Offset: 0x002D7547
		public override string LocalName
		{
			get
			{
				return "hyperlink";
			}
		}

		// Token: 0x17008C39 RID: 35897
		// (get) Token: 0x06019780 RID: 104320 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C3A RID: 35898
		// (get) Token: 0x06019781 RID: 104321 RVA: 0x0034B552 File Offset: 0x00349752
		internal override int ElementTypeId
		{
			get
			{
				return 11620;
			}
		}

		// Token: 0x06019782 RID: 104322 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C3B RID: 35899
		// (get) Token: 0x06019783 RID: 104323 RVA: 0x0034B559 File Offset: 0x00349759
		internal override string[] AttributeTagNames
		{
			get
			{
				return HyperlinkRuby.attributeTagNames;
			}
		}

		// Token: 0x17008C3C RID: 35900
		// (get) Token: 0x06019784 RID: 104324 RVA: 0x0034B560 File Offset: 0x00349760
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HyperlinkRuby.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C3D RID: 35901
		// (get) Token: 0x06019785 RID: 104325 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019786 RID: 104326 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "tgtFrame")]
		public StringValue TargetFrame
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008C3E RID: 35902
		// (get) Token: 0x06019787 RID: 104327 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019788 RID: 104328 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "tooltip")]
		public StringValue Tooltip
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008C3F RID: 35903
		// (get) Token: 0x06019789 RID: 104329 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601978A RID: 104330 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "docLocation")]
		public StringValue DocLocation
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008C40 RID: 35904
		// (get) Token: 0x0601978B RID: 104331 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601978C RID: 104332 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "history")]
		public OnOffValue History
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008C41 RID: 35905
		// (get) Token: 0x0601978D RID: 104333 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601978E RID: 104334 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "anchor")]
		public StringValue Anchor
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008C42 RID: 35906
		// (get) Token: 0x0601978F RID: 104335 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06019790 RID: 104336 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06019791 RID: 104337 RVA: 0x00293ECF File Offset: 0x002920CF
		public HyperlinkRuby()
		{
		}

		// Token: 0x06019792 RID: 104338 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HyperlinkRuby(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019793 RID: 104339 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HyperlinkRuby(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019794 RID: 104340 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HyperlinkRuby(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019795 RID: 104341 RVA: 0x0034B568 File Offset: 0x00349768
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlRuby();
			}
			if (23 == namespaceId && "fldSimple" == name)
			{
				return new SimpleFieldRuby();
			}
			if (23 == namespaceId && "hyperlink" == name)
			{
				return new HyperlinkRuby();
			}
			if (23 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtRunRuby();
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
			if (21 == namespaceId && "oMathPara" == name)
			{
				return new Paragraph();
			}
			if (21 == namespaceId && "oMath" == name)
			{
				return new OfficeMath();
			}
			if (21 == namespaceId && "acc" == name)
			{
				return new Accent();
			}
			if (21 == namespaceId && "bar" == name)
			{
				return new Bar();
			}
			if (21 == namespaceId && "box" == name)
			{
				return new Box();
			}
			if (21 == namespaceId && "borderBox" == name)
			{
				return new BorderBox();
			}
			if (21 == namespaceId && "d" == name)
			{
				return new Delimiter();
			}
			if (21 == namespaceId && "eqArr" == name)
			{
				return new EquationArray();
			}
			if (21 == namespaceId && "f" == name)
			{
				return new Fraction();
			}
			if (21 == namespaceId && "func" == name)
			{
				return new MathFunction();
			}
			if (21 == namespaceId && "groupChr" == name)
			{
				return new GroupChar();
			}
			if (21 == namespaceId && "limLow" == name)
			{
				return new LimitLower();
			}
			if (21 == namespaceId && "limUpp" == name)
			{
				return new LimitUpper();
			}
			if (21 == namespaceId && "m" == name)
			{
				return new Matrix();
			}
			if (21 == namespaceId && "nary" == name)
			{
				return new Nary();
			}
			if (21 == namespaceId && "phant" == name)
			{
				return new Phantom();
			}
			if (21 == namespaceId && "rad" == name)
			{
				return new Radical();
			}
			if (21 == namespaceId && "sPre" == name)
			{
				return new PreSubSuper();
			}
			if (21 == namespaceId && "sSub" == name)
			{
				return new Subscript();
			}
			if (21 == namespaceId && "sSubSup" == name)
			{
				return new SubSuperscript();
			}
			if (21 == namespaceId && "sSup" == name)
			{
				return new Superscript();
			}
			if (21 == namespaceId && "r" == name)
			{
				return new Run();
			}
			return null;
		}

		// Token: 0x06019796 RID: 104342 RVA: 0x0034BAD0 File Offset: 0x00349CD0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tgtFrame" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "tooltip" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "docLocation" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "history" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "anchor" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019797 RID: 104343 RVA: 0x0034BB75 File Offset: 0x00349D75
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkRuby>(deep);
		}

		// Token: 0x0400A8FC RID: 43260
		private const string tagName = "hyperlink";

		// Token: 0x0400A8FD RID: 43261
		private const byte tagNsId = 23;

		// Token: 0x0400A8FE RID: 43262
		internal const int ElementTypeIdConst = 11620;

		// Token: 0x0400A8FF RID: 43263
		private static string[] attributeTagNames = new string[] { "tgtFrame", "tooltip", "docLocation", "history", "anchor", "id" };

		// Token: 0x0400A900 RID: 43264
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 19 };
	}
}
