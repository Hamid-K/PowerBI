using System;
using AngleSharp.Attributes;

namespace AngleSharp.Parser.Html
{
	// Token: 0x0200006B RID: 107
	public enum HtmlParseError : byte
	{
		// Token: 0x04000243 RID: 579
		[DomDescription("Unexpected end of the given file.")]
		EOF,
		// Token: 0x04000244 RID: 580
		[DomDescription("NULL character replaced by repl. character.")]
		Null,
		// Token: 0x04000245 RID: 581
		[DomDescription("Bogus comment detected.")]
		BogusComment = 26,
		// Token: 0x04000246 RID: 582
		[DomDescription("Ambiguous open tag.")]
		AmbiguousOpenTag,
		// Token: 0x04000247 RID: 583
		[DomDescription("The tag has been closed unexpectedly.")]
		TagClosedWrong,
		// Token: 0x04000248 RID: 584
		[DomDescription("The closing slash has been misplaced.")]
		ClosingSlashMisplaced,
		// Token: 0x04000249 RID: 585
		[DomDescription("Undefined markup declaration found.")]
		UndefinedMarkupDeclaration,
		// Token: 0x0400024A RID: 586
		[DomDescription("Comment ended with an exclamation mark.")]
		CommentEndedWithEM,
		// Token: 0x0400024B RID: 587
		[DomDescription("Comment ended with a dash.")]
		CommentEndedWithDash,
		// Token: 0x0400024C RID: 588
		[DomDescription("Comment ended with an unexpected character.")]
		CommentEndedUnexpected,
		// Token: 0x0400024D RID: 589
		[DomDescription("The given tag cannot be self-closed.")]
		TagCannotBeSelfClosed,
		// Token: 0x0400024E RID: 590
		[DomDescription("End tags can never be self-closed.")]
		EndTagCannotBeSelfClosed,
		// Token: 0x0400024F RID: 591
		[DomDescription("End tags cannot carry attributes.")]
		EndTagCannotHaveAttributes,
		// Token: 0x04000250 RID: 592
		[DomDescription("No caption tag has been found within the local scope.")]
		CaptionNotInScope,
		// Token: 0x04000251 RID: 593
		[DomDescription("No select tag has been found within the local scope.")]
		SelectNotInScope,
		// Token: 0x04000252 RID: 594
		[DomDescription("No table row has been found within the local scope.")]
		TableRowNotInScope,
		// Token: 0x04000253 RID: 595
		[DomDescription("No table has been found within the local scope.")]
		TableNotInScope,
		// Token: 0x04000254 RID: 596
		[DomDescription("No paragraph has been found within the local scope.")]
		ParagraphNotInScope,
		// Token: 0x04000255 RID: 597
		[DomDescription("No body has been found within the local scope.")]
		BodyNotInScope,
		// Token: 0x04000256 RID: 598
		[DomDescription("No block element has been found within the local scope.")]
		BlockNotInScope,
		// Token: 0x04000257 RID: 599
		[DomDescription("No table cell has been found within the local scope.")]
		TableCellNotInScope,
		// Token: 0x04000258 RID: 600
		[DomDescription("No table section has been found within the local scope.")]
		TableSectionNotInScope,
		// Token: 0x04000259 RID: 601
		[DomDescription("No object element has been found within the local scope.")]
		ObjectNotInScope,
		// Token: 0x0400025A RID: 602
		[DomDescription("No heading element has been found within the local scope.")]
		HeadingNotInScope,
		// Token: 0x0400025B RID: 603
		[DomDescription("No list item has been found within the local scope.")]
		ListItemNotInScope,
		// Token: 0x0400025C RID: 604
		[DomDescription("No form has been found within the local scope.")]
		FormNotInScope,
		// Token: 0x0400025D RID: 605
		[DomDescription("No button has been found within the local scope.")]
		ButtonInScope,
		// Token: 0x0400025E RID: 606
		[DomDescription("No nobr element has been found within the local scope.")]
		NobrInScope,
		// Token: 0x0400025F RID: 607
		[DomDescription("No element has been found within the local scope.")]
		ElementNotInScope,
		// Token: 0x04000260 RID: 608
		[DomDescription("Character reference found no numbers.")]
		CharacterReferenceWrongNumber,
		// Token: 0x04000261 RID: 609
		[DomDescription("Character reference found no semicolon.")]
		CharacterReferenceSemicolonMissing,
		// Token: 0x04000262 RID: 610
		[DomDescription("Character reference within an invalid range.")]
		CharacterReferenceInvalidRange,
		// Token: 0x04000263 RID: 611
		[DomDescription("Character reference is an invalid number.")]
		CharacterReferenceInvalidNumber,
		// Token: 0x04000264 RID: 612
		[DomDescription("Character reference is an invalid code.")]
		CharacterReferenceInvalidCode,
		// Token: 0x04000265 RID: 613
		[DomDescription("Character reference is not terminated by a semicolon.")]
		CharacterReferenceNotTerminated,
		// Token: 0x04000266 RID: 614
		[DomDescription("Character reference in attribute contains an invalid character (=).")]
		CharacterReferenceAttributeEqualsFound,
		// Token: 0x04000267 RID: 615
		[DomDescription("The specified item has not been found.")]
		ItemNotFound,
		// Token: 0x04000268 RID: 616
		[DomDescription("The encoding operation (either encoded or decoding) failed.")]
		EncodingError,
		// Token: 0x04000269 RID: 617
		[DomDescription("Doctype unexpected character after the name detected.")]
		DoctypeUnexpectedAfterName = 64,
		// Token: 0x0400026A RID: 618
		[DomDescription("Invalid character in the public identifier detected.")]
		DoctypePublicInvalid,
		// Token: 0x0400026B RID: 619
		[DomDescription("Invalid character in the doctype detected.")]
		DoctypeInvalidCharacter,
		// Token: 0x0400026C RID: 620
		[DomDescription("Invalid character in the system identifier detected.")]
		DoctypeSystemInvalid,
		// Token: 0x0400026D RID: 621
		[DomDescription("The doctype tag is misplaced and ignored.")]
		DoctypeTagInappropriate,
		// Token: 0x0400026E RID: 622
		[DomDescription("The given doctype tag is invalid.")]
		DoctypeInvalid,
		// Token: 0x0400026F RID: 623
		[DomDescription("Doctype encountered unexpected character.")]
		DoctypeUnexpected,
		// Token: 0x04000270 RID: 624
		[DomDescription("The doctype tag is missing.")]
		DoctypeMissing,
		// Token: 0x04000271 RID: 625
		[DomDescription("The given public identifier for the notation declaration is invalid.")]
		NotationPublicInvalid,
		// Token: 0x04000272 RID: 626
		[DomDescription("The given system identifier for the notation declaration is invalid.")]
		NotationSystemInvalid,
		// Token: 0x04000273 RID: 627
		[DomDescription("The type declaration is missing a valid definition.")]
		TypeDeclarationUndefined,
		// Token: 0x04000274 RID: 628
		[DomDescription("A required quantifier is missing in the provided expression.")]
		QuantifierMissing,
		// Token: 0x04000275 RID: 629
		[DomDescription("The double quotation marks have been misplaced.")]
		DoubleQuotationMarkUnexpected = 80,
		// Token: 0x04000276 RID: 630
		[DomDescription("The single quotation marks have been misplaced.")]
		SingleQuotationMarkUnexpected,
		// Token: 0x04000277 RID: 631
		[DomDescription("The attribute's name contains an invalid character.")]
		AttributeNameInvalid = 96,
		// Token: 0x04000278 RID: 632
		[DomDescription("The attribute's value contains an invalid character.")]
		AttributeValueInvalid,
		// Token: 0x04000279 RID: 633
		[DomDescription("The beginning of a new attribute has been expected.")]
		AttributeNameExpected,
		// Token: 0x0400027A RID: 634
		[DomDescription("The attribute has already been added.")]
		AttributeDuplicateOmitted,
		// Token: 0x0400027B RID: 635
		[DomDescription("The given tag must be placed in head tag.")]
		TagMustBeInHead = 112,
		// Token: 0x0400027C RID: 636
		[DomDescription("The given tag is not appropriate for the current position.")]
		TagInappropriate,
		// Token: 0x0400027D RID: 637
		[DomDescription("The given tag cannot end at the current position.")]
		TagCannotEndHere,
		// Token: 0x0400027E RID: 638
		[DomDescription("The given tag cannot start at the current position.")]
		TagCannotStartHere,
		// Token: 0x0400027F RID: 639
		[DomDescription("The given form cannot be placed at the current position.")]
		FormInappropriate,
		// Token: 0x04000280 RID: 640
		[DomDescription("The given input cannot be placed at the current position.")]
		InputUnexpected,
		// Token: 0x04000281 RID: 641
		[DomDescription("The closing tag and the currently open tag do not match.")]
		TagClosingMismatch,
		// Token: 0x04000282 RID: 642
		[DomDescription("The given end tag does not match the current node.")]
		TagDoesNotMatchCurrentNode,
		// Token: 0x04000283 RID: 643
		[DomDescription("This position does not support a linebreak (LF, FF).")]
		LineBreakUnexpected,
		// Token: 0x04000284 RID: 644
		[DomDescription("The head tag can only be placed once inside the html tag.")]
		HeadTagMisplaced = 128,
		// Token: 0x04000285 RID: 645
		[DomDescription("The html tag can only be placed once as the root element.")]
		HtmlTagMisplaced,
		// Token: 0x04000286 RID: 646
		[DomDescription("The body tag can only be placed once inside the html tag.")]
		BodyTagMisplaced,
		// Token: 0x04000287 RID: 647
		[DomDescription("The image tag has been named image instead of img.")]
		ImageTagNamedWrong,
		// Token: 0x04000288 RID: 648
		[DomDescription("Tables cannot be nested.")]
		TableNesting,
		// Token: 0x04000289 RID: 649
		[DomDescription("An illegal element has been detected in a table.")]
		IllegalElementInTableDetected,
		// Token: 0x0400028A RID: 650
		[DomDescription("Select elements cannot be nested.")]
		SelectNesting,
		// Token: 0x0400028B RID: 651
		[DomDescription("An illegal element has been detected in a select.")]
		IllegalElementInSelectDetected,
		// Token: 0x0400028C RID: 652
		[DomDescription("The frameset element has been misplaced.")]
		FramesetMisplaced,
		// Token: 0x0400028D RID: 653
		[DomDescription("Headings cannot be nested.")]
		HeadingNested,
		// Token: 0x0400028E RID: 654
		[DomDescription("Anchor elements cannot be nested.")]
		AnchorNested,
		// Token: 0x0400028F RID: 655
		[DomDescription("The given token cannot be inserted here.")]
		TokenNotPossible = 144,
		// Token: 0x04000290 RID: 656
		[DomDescription("The current node is not the root element.")]
		CurrentNodeIsNotRoot,
		// Token: 0x04000291 RID: 657
		[DomDescription("The current node is the root element.")]
		CurrentNodeIsRoot,
		// Token: 0x04000292 RID: 658
		[DomDescription("This tag is invalid in fragment mode.")]
		TagInvalidInFragmentMode,
		// Token: 0x04000293 RID: 659
		[DomDescription("There is already an open form.")]
		FormAlreadyOpen,
		// Token: 0x04000294 RID: 660
		[DomDescription("The form has been closed wrong.")]
		FormClosedWrong,
		// Token: 0x04000295 RID: 661
		[DomDescription("The body has been closed wrong.")]
		BodyClosedWrong,
		// Token: 0x04000296 RID: 662
		[DomDescription("An expected formatting element has not been found.")]
		FormattingElementNotFound
	}
}
