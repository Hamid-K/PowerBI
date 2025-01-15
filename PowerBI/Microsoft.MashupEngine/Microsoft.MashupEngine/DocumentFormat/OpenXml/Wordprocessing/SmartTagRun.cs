using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC8 RID: 11976
	[ChildElementInfo(typeof(SimpleField))]
	[ChildElementInfo(typeof(MoveFromRun))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlRun))]
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
	[ChildElementInfo(typeof(SmartTagProperties))]
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
	[ChildElementInfo(typeof(LimitUpper))]
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
	[ChildElementInfo(typeof(SubDocumentReference))]
	internal class SmartTagRun : OpenXmlCompositeElement
	{
		// Token: 0x17008C87 RID: 35975
		// (get) Token: 0x06019838 RID: 104504 RVA: 0x0034CE3C File Offset: 0x0034B03C
		public override string LocalName
		{
			get
			{
				return "smartTag";
			}
		}

		// Token: 0x17008C88 RID: 35976
		// (get) Token: 0x06019839 RID: 104505 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C89 RID: 35977
		// (get) Token: 0x0601983A RID: 104506 RVA: 0x0034CE43 File Offset: 0x0034B043
		internal override int ElementTypeId
		{
			get
			{
				return 11631;
			}
		}

		// Token: 0x0601983B RID: 104507 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C8A RID: 35978
		// (get) Token: 0x0601983C RID: 104508 RVA: 0x0034CE4A File Offset: 0x0034B04A
		internal override string[] AttributeTagNames
		{
			get
			{
				return SmartTagRun.attributeTagNames;
			}
		}

		// Token: 0x17008C8B RID: 35979
		// (get) Token: 0x0601983D RID: 104509 RVA: 0x0034CE51 File Offset: 0x0034B051
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SmartTagRun.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C8C RID: 35980
		// (get) Token: 0x0601983E RID: 104510 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601983F RID: 104511 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "uri")]
		public StringValue Uri
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

		// Token: 0x17008C8D RID: 35981
		// (get) Token: 0x06019840 RID: 104512 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019841 RID: 104513 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "element")]
		public StringValue Element
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

		// Token: 0x06019842 RID: 104514 RVA: 0x00293ECF File Offset: 0x002920CF
		public SmartTagRun()
		{
		}

		// Token: 0x06019843 RID: 104515 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SmartTagRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019844 RID: 104516 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SmartTagRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019845 RID: 104517 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SmartTagRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019846 RID: 104518 RVA: 0x0034CE58 File Offset: 0x0034B058
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "smartTagPr" == name)
			{
				return new SmartTagProperties();
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

		// Token: 0x17008C8E RID: 35982
		// (get) Token: 0x06019847 RID: 104519 RVA: 0x0034D436 File Offset: 0x0034B636
		internal override string[] ElementTagNames
		{
			get
			{
				return SmartTagRun.eleTagNames;
			}
		}

		// Token: 0x17008C8F RID: 35983
		// (get) Token: 0x06019848 RID: 104520 RVA: 0x0034D43D File Offset: 0x0034B63D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SmartTagRun.eleNamespaceIds;
			}
		}

		// Token: 0x17008C90 RID: 35984
		// (get) Token: 0x06019849 RID: 104521 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008C91 RID: 35985
		// (get) Token: 0x0601984A RID: 104522 RVA: 0x0034D444 File Offset: 0x0034B644
		// (set) Token: 0x0601984B RID: 104523 RVA: 0x0034D44D File Offset: 0x0034B64D
		public SmartTagProperties SmartTagProperties
		{
			get
			{
				return base.GetElement<SmartTagProperties>(0);
			}
			set
			{
				base.SetElement<SmartTagProperties>(0, value);
			}
		}

		// Token: 0x0601984C RID: 104524 RVA: 0x0034D457 File Offset: 0x0034B657
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

		// Token: 0x0601984D RID: 104525 RVA: 0x0034D491 File Offset: 0x0034B691
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagRun>(deep);
		}

		// Token: 0x0400A92F RID: 43311
		private const string tagName = "smartTag";

		// Token: 0x0400A930 RID: 43312
		private const byte tagNsId = 23;

		// Token: 0x0400A931 RID: 43313
		internal const int ElementTypeIdConst = 11631;

		// Token: 0x0400A932 RID: 43314
		private static string[] attributeTagNames = new string[] { "uri", "element" };

		// Token: 0x0400A933 RID: 43315
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };

		// Token: 0x0400A934 RID: 43316
		private static readonly string[] eleTagNames = new string[]
		{
			"smartTagPr", "customXml", "fldSimple", "hyperlink", "smartTag", "sdt", "proofErr", "permStart", "permEnd", "bookmarkStart",
			"bookmarkEnd", "commentRangeStart", "commentRangeEnd", "moveFromRangeStart", "moveFromRangeEnd", "moveToRangeStart", "moveToRangeEnd", "customXmlInsRangeStart", "customXmlInsRangeEnd", "customXmlDelRangeStart",
			"customXmlDelRangeEnd", "customXmlMoveFromRangeStart", "customXmlMoveFromRangeEnd", "customXmlMoveToRangeStart", "customXmlMoveToRangeEnd", "customXmlConflictInsRangeStart", "customXmlConflictInsRangeEnd", "customXmlConflictDelRangeStart", "customXmlConflictDelRangeEnd", "ins",
			"del", "moveFrom", "moveTo", "contentPart", "conflictIns", "conflictDel", "oMathPara", "oMath", "acc", "bar",
			"box", "borderBox", "d", "eqArr", "f", "func", "groupChr", "limLow", "limUpp", "m",
			"nary", "phant", "rad", "sPre", "sSub", "sSubSup", "sSup", "r", "r", "bdo",
			"dir", "subDoc"
		};

		// Token: 0x0400A935 RID: 43317
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
