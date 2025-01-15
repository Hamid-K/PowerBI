using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F4C RID: 12108
	[ChildElementInfo(typeof(Fraction))]
	[ChildElementInfo(typeof(Delimiter))]
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(EquationArray))]
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
	internal abstract class RubyContentType : OpenXmlCompositeElement
	{
		// Token: 0x06019FFC RID: 106492 RVA: 0x0035AB50 File Offset: 0x00358D50
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

		// Token: 0x06019FFD RID: 106493 RVA: 0x00293ECF File Offset: 0x002920CF
		protected RubyContentType()
		{
		}

		// Token: 0x06019FFE RID: 106494 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected RubyContentType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FFF RID: 106495 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected RubyContentType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A000 RID: 106496 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected RubyContentType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
