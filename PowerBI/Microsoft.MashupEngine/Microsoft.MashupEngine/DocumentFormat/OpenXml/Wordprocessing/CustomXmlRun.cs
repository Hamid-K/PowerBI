using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002ED4 RID: 11988
	[ChildElementInfo(typeof(LimitUpper))]
	[ChildElementInfo(typeof(SubDocumentReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlProperties))]
	[ChildElementInfo(typeof(CustomXmlRun))]
	[ChildElementInfo(typeof(SimpleField))]
	[ChildElementInfo(typeof(Hyperlink))]
	[ChildElementInfo(typeof(SmartTagRun))]
	[ChildElementInfo(typeof(SdtRun))]
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
	[ChildElementInfo(typeof(RunConflictDeletion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Paragraph))]
	[ChildElementInfo(typeof(OfficeMath))]
	[ChildElementInfo(typeof(Accent))]
	[ChildElementInfo(typeof(Bar))]
	[ChildElementInfo(typeof(Box))]
	[ChildElementInfo(typeof(BorderBox))]
	[ChildElementInfo(typeof(Delimiter))]
	[ChildElementInfo(typeof(EquationArray))]
	[ChildElementInfo(typeof(Fraction))]
	[ChildElementInfo(typeof(MathFunction))]
	[ChildElementInfo(typeof(GroupChar))]
	[ChildElementInfo(typeof(LimitLower))]
	[ChildElementInfo(typeof(Matrix))]
	[ChildElementInfo(typeof(Nary))]
	[ChildElementInfo(typeof(Phantom))]
	[ChildElementInfo(typeof(Radical))]
	[ChildElementInfo(typeof(PreSubSuper))]
	[ChildElementInfo(typeof(Subscript))]
	[ChildElementInfo(typeof(SubSuperscript))]
	[ChildElementInfo(typeof(Superscript))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(BidirectionalOverride), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BidirectionalEmbedding), FileFormatVersions.Office2010)]
	internal class CustomXmlRun : CustomXmlElement
	{
		// Token: 0x17008CF1 RID: 36081
		// (get) Token: 0x06019920 RID: 104736 RVA: 0x0034A455 File Offset: 0x00348655
		public override string LocalName
		{
			get
			{
				return "customXml";
			}
		}

		// Token: 0x17008CF2 RID: 36082
		// (get) Token: 0x06019921 RID: 104737 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008CF3 RID: 36083
		// (get) Token: 0x06019922 RID: 104738 RVA: 0x00350BF4 File Offset: 0x0034EDF4
		internal override int ElementTypeId
		{
			get
			{
				return 11643;
			}
		}

		// Token: 0x06019923 RID: 104739 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008CF4 RID: 36084
		// (get) Token: 0x06019924 RID: 104740 RVA: 0x00350BFB File Offset: 0x0034EDFB
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomXmlRun.attributeTagNames;
			}
		}

		// Token: 0x17008CF5 RID: 36085
		// (get) Token: 0x06019925 RID: 104741 RVA: 0x00350C02 File Offset: 0x0034EE02
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomXmlRun.attributeNamespaceIds;
			}
		}

		// Token: 0x06019926 RID: 104742 RVA: 0x0034A471 File Offset: 0x00348671
		public CustomXmlRun()
			: base(new OpenXmlElement[0])
		{
		}

		// Token: 0x06019927 RID: 104743 RVA: 0x0034A47F File Offset: 0x0034867F
		public CustomXmlRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019928 RID: 104744 RVA: 0x0034A488 File Offset: 0x00348688
		public CustomXmlRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019929 RID: 104745 RVA: 0x0034A491 File Offset: 0x00348691
		public CustomXmlRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601992A RID: 104746 RVA: 0x00350C0C File Offset: 0x0034EE0C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "customXmlPr" == name)
			{
				return new CustomXmlProperties();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlRun();
			}
			if (23 == namespaceId && "fldSimple" == name)
			{
				return new SimpleField();
			}
			if (23 == namespaceId && "hyperlink" == name)
			{
				return new Hyperlink();
			}
			if (23 == namespaceId && "smartTag" == name)
			{
				return new SmartTagRun();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtRun();
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
			if (23 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (23 == namespaceId && "bdo" == name)
			{
				return new BidirectionalOverride();
			}
			if (23 == namespaceId && "dir" == name)
			{
				return new BidirectionalEmbedding();
			}
			if (23 == namespaceId && "subDoc" == name)
			{
				return new SubDocumentReference();
			}
			return null;
		}

		// Token: 0x17008CF6 RID: 36086
		// (get) Token: 0x0601992B RID: 104747 RVA: 0x003511EA File Offset: 0x0034F3EA
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomXmlRun.eleTagNames;
			}
		}

		// Token: 0x17008CF7 RID: 36087
		// (get) Token: 0x0601992C RID: 104748 RVA: 0x003511F1 File Offset: 0x0034F3F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomXmlRun.eleNamespaceIds;
			}
		}

		// Token: 0x17008CF8 RID: 36088
		// (get) Token: 0x0601992D RID: 104749 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x0601992E RID: 104750 RVA: 0x0034AA28 File Offset: 0x00348C28
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

		// Token: 0x0601992F RID: 104751 RVA: 0x003511F8 File Offset: 0x0034F3F8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlRun>(deep);
		}

		// Token: 0x0400A975 RID: 43381
		private const string tagName = "customXml";

		// Token: 0x0400A976 RID: 43382
		private const byte tagNsId = 23;

		// Token: 0x0400A977 RID: 43383
		internal const int ElementTypeIdConst = 11643;

		// Token: 0x0400A978 RID: 43384
		private static string[] attributeTagNames = new string[] { "uri", "element" };

		// Token: 0x0400A979 RID: 43385
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };

		// Token: 0x0400A97A RID: 43386
		private static readonly string[] eleTagNames = new string[]
		{
			"customXmlPr", "customXml", "fldSimple", "hyperlink", "smartTag", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart",
			"bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart",
			"customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins",
			"del", "moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel", "oMathPara", "oMath", "acc", "bar",
			"box", "borderBox", "d", "eqArr", "f", "func", "groupChr", "limLow", "limUpp", "m",
			"nary", "phant", "rad", "sPre", "sSub", "sSubSup", "sSup", "r", "r", "bdo",
			"dir", "subDoc"
		};

		// Token: 0x0400A97B RID: 43387
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 52, 52, 52, 52, 23,
			23, 23, 23, 23, 52, 52, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 21, 21, 23, 23,
			23, 23
		};
	}
}
