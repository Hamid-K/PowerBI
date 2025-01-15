using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018AC RID: 6316
	internal static class MAst
	{
		// Token: 0x040053EB RID: 21483
		public const string Kind = "Kind";

		// Token: 0x040053EC RID: 21484
		public const string Binary = "Binary";

		// Token: 0x040053ED RID: 21485
		public const string Constant = "Constant";

		// Token: 0x040053EE RID: 21486
		public const string ElementAccess = "ElementAccess";

		// Token: 0x040053EF RID: 21487
		public const string Exports = "Exports";

		// Token: 0x040053F0 RID: 21488
		public const string FieldAccess = "FieldAccess";

		// Token: 0x040053F1 RID: 21489
		public const string Function = "Function";

		// Token: 0x040053F2 RID: 21490
		public const string FunctionType = "FunctionType";

		// Token: 0x040053F3 RID: 21491
		public const string Identifier = "Identifier";

		// Token: 0x040053F4 RID: 21492
		public const string If = "If";

		// Token: 0x040053F5 RID: 21493
		public const string ImplicitIdentifier = "ImplicitIdentifier";

		// Token: 0x040053F6 RID: 21494
		public const string Invocation = "Invocation";

		// Token: 0x040053F7 RID: 21495
		public const string Let = "Let";

		// Token: 0x040053F8 RID: 21496
		public const string List = "List";

		// Token: 0x040053F9 RID: 21497
		public const string ListType = "ListType";

		// Token: 0x040053FA RID: 21498
		public const string MultiFieldRecordProjection = "MultiFieldRecordProjection";

		// Token: 0x040053FB RID: 21499
		public const string NotImplemented = "NotImplemented";

		// Token: 0x040053FC RID: 21500
		public const string NullableType = "NullableType";

		// Token: 0x040053FD RID: 21501
		public const string Parentheses = "Parentheses";

		// Token: 0x040053FE RID: 21502
		public const string RangeList = "RangeList";

		// Token: 0x040053FF RID: 21503
		public const string Record = "Record";

		// Token: 0x04005400 RID: 21504
		public const string RecordType = "RecordType";

		// Token: 0x04005401 RID: 21505
		public const string SectionIdentifier = "SectionIdentifier";

		// Token: 0x04005402 RID: 21506
		public const string TableType = "TableType";

		// Token: 0x04005403 RID: 21507
		public const string Throw = "Throw";

		// Token: 0x04005404 RID: 21508
		public const string TryCatch = "TryCatch";

		// Token: 0x04005405 RID: 21509
		public const string Unary = "Unary";

		// Token: 0x04005406 RID: 21510
		public const string Verbatim = "Verbatim";

		// Token: 0x04005407 RID: 21511
		public const string Arguments = "Arguments";

		// Token: 0x04005408 RID: 21512
		public const string Collection = "Collection";

		// Token: 0x04005409 RID: 21513
		public const string Condition = "Condition";

		// Token: 0x0400540A RID: 21514
		public const string ExceptionCase = "ExceptionCase";

		// Token: 0x0400540B RID: 21515
		public const string Expression = "Expression";

		// Token: 0x0400540C RID: 21516
		public const string FalseCase = "FalseCase";

		// Token: 0x0400540D RID: 21517
		public const string Fields = "Fields";

		// Token: 0x0400540E RID: 21518
		public const string IsInclusive = "IsInclusive";

		// Token: 0x0400540F RID: 21519
		public const string IsOptional = "IsOptional";

		// Token: 0x04005410 RID: 21520
		public const string ItemType = "ItemType";

		// Token: 0x04005411 RID: 21521
		public const string Key = "Key";

		// Token: 0x04005412 RID: 21522
		public const string Left = "Left";

		// Token: 0x04005413 RID: 21523
		public const string Lower = "Lower";

		// Token: 0x04005414 RID: 21524
		public const string MemberName = "MemberName";

		// Token: 0x04005415 RID: 21525
		public const string MemberNames = "MemberNames";

		// Token: 0x04005416 RID: 21526
		public const string Members = "Members";

		// Token: 0x04005417 RID: 21527
		public const string Min = "Min";

		// Token: 0x04005418 RID: 21528
		public const string Name = "Name";

		// Token: 0x04005419 RID: 21529
		public const string Operator = "Operator";

		// Token: 0x0400541A RID: 21530
		public const string Optional = "Optional";

		// Token: 0x0400541B RID: 21531
		public const string Parameters = "Parameters";

		// Token: 0x0400541C RID: 21532
		public const string ReturnType = "ReturnType";

		// Token: 0x0400541D RID: 21533
		public const string Right = "Right";

		// Token: 0x0400541E RID: 21534
		public const string RowType = "RowType";

		// Token: 0x0400541F RID: 21535
		public const string Section = "Section";

		// Token: 0x04005420 RID: 21536
		public const string Text = "Text";

		// Token: 0x04005421 RID: 21537
		public const string TrueCase = "TrueCase";

		// Token: 0x04005422 RID: 21538
		public const string Try = "Try";

		// Token: 0x04005423 RID: 21539
		public const string Type = "Type";

		// Token: 0x04005424 RID: 21540
		public const string Upper = "Upper";

		// Token: 0x04005425 RID: 21541
		public const string Value = "Value";

		// Token: 0x04005426 RID: 21542
		public const string Variable = "Variable";

		// Token: 0x04005427 RID: 21543
		public const string Variables = "Variables";

		// Token: 0x04005428 RID: 21544
		public const string Wildcard = "Wildcard";

		// Token: 0x04005429 RID: 21545
		public static readonly TextValue BinaryKind = TextValue.New("Binary");

		// Token: 0x0400542A RID: 21546
		public static readonly TextValue ConstantKind = TextValue.New("Constant");

		// Token: 0x0400542B RID: 21547
		public static readonly TextValue ElementAccessKind = TextValue.New("ElementAccess");

		// Token: 0x0400542C RID: 21548
		public static readonly TextValue ExportsKind = TextValue.New("Exports");

		// Token: 0x0400542D RID: 21549
		public static readonly TextValue FieldAccessKind = TextValue.New("FieldAccess");

		// Token: 0x0400542E RID: 21550
		public static readonly TextValue FunctionKind = TextValue.New("Function");

		// Token: 0x0400542F RID: 21551
		public static readonly TextValue FunctionTypeKind = TextValue.New("FunctionType");

		// Token: 0x04005430 RID: 21552
		public static readonly TextValue IdentifierKind = TextValue.New("Identifier");

		// Token: 0x04005431 RID: 21553
		public static readonly TextValue IfKind = TextValue.New("If");

		// Token: 0x04005432 RID: 21554
		public static readonly TextValue ImplicitIdentifierKind = TextValue.New("ImplicitIdentifier");

		// Token: 0x04005433 RID: 21555
		public static readonly TextValue InvocationKind = TextValue.New("Invocation");

		// Token: 0x04005434 RID: 21556
		public static readonly TextValue LetKind = TextValue.New("Let");

		// Token: 0x04005435 RID: 21557
		public static readonly TextValue ListKind = TextValue.New("List");

		// Token: 0x04005436 RID: 21558
		public static readonly TextValue ListTypeKind = TextValue.New("ListType");

		// Token: 0x04005437 RID: 21559
		public static readonly TextValue MultiFieldRecordProjectionKind = TextValue.New("MultiFieldRecordProjection");

		// Token: 0x04005438 RID: 21560
		public static readonly TextValue NotImplementedKind = TextValue.New("NotImplemented");

		// Token: 0x04005439 RID: 21561
		public static readonly TextValue NullableTypeKind = TextValue.New("NullableType");

		// Token: 0x0400543A RID: 21562
		public static readonly TextValue ParenthesesKind = TextValue.New("Parentheses");

		// Token: 0x0400543B RID: 21563
		public static readonly TextValue RangeListKind = TextValue.New("RangeList");

		// Token: 0x0400543C RID: 21564
		public static readonly TextValue RecordKind = TextValue.New("Record");

		// Token: 0x0400543D RID: 21565
		public static readonly TextValue RecordTypeKind = TextValue.New("RecordType");

		// Token: 0x0400543E RID: 21566
		public static readonly TextValue SectionIdentifierKind = TextValue.New("SectionIdentifier");

		// Token: 0x0400543F RID: 21567
		public static readonly TextValue TableTypeKind = TextValue.New("TableType");

		// Token: 0x04005440 RID: 21568
		public static readonly TextValue ThrowKind = TextValue.New("Throw");

		// Token: 0x04005441 RID: 21569
		public static readonly TextValue TryCatchKind = TextValue.New("TryCatch");

		// Token: 0x04005442 RID: 21570
		public static readonly TextValue TypeKind = TextValue.New("Type");

		// Token: 0x04005443 RID: 21571
		public static readonly TextValue UnaryKind = TextValue.New("Unary");

		// Token: 0x04005444 RID: 21572
		public static readonly TextValue VerbatimKind = TextValue.New("Verbatim");

		// Token: 0x04005445 RID: 21573
		public static readonly Keys BinaryKeys = Keys.New("Kind", "Operator", "Left", "Right");

		// Token: 0x04005446 RID: 21574
		public static readonly Keys ConstantKeys = Keys.New("Kind", "Value");

		// Token: 0x04005447 RID: 21575
		public static readonly Keys ElementAccessKeys = Keys.New("Kind", "Collection", "Key", "IsOptional");

		// Token: 0x04005448 RID: 21576
		public static readonly Keys ExceptionCaseKeys = Keys.New("Variable", "Expression");

		// Token: 0x04005449 RID: 21577
		public static readonly Keys ExportsKeys = Keys.New("Kind", "Name");

		// Token: 0x0400544A RID: 21578
		public static readonly Keys FieldAccessKeys = Keys.New("Kind", "IsOptional", "MemberName", "Expression");

		// Token: 0x0400544B RID: 21579
		public static readonly Keys FieldTypeKeys = Keys.New("Name", "Type", "Optional");

		// Token: 0x0400544C RID: 21580
		public static readonly Keys FunctionKeys = Keys.New("Kind", "FunctionType", "Expression");

		// Token: 0x0400544D RID: 21581
		public static readonly Keys FunctionTypeKeys = Keys.New("Kind", "ReturnType", "Parameters", "Min");

		// Token: 0x0400544E RID: 21582
		public static readonly Keys IdentifierKeys = Keys.New("Kind", "Name", "IsInclusive");

		// Token: 0x0400544F RID: 21583
		public static readonly Keys IfKeys = Keys.New("Kind", "Condition", "TrueCase", "FalseCase");

		// Token: 0x04005450 RID: 21584
		public static readonly Keys InvocationKeys = Keys.New("Kind", "Function", "Arguments");

		// Token: 0x04005451 RID: 21585
		public static readonly Keys LetKeys = Keys.New("Kind", "Variables", "Expression");

		// Token: 0x04005452 RID: 21586
		public static readonly Keys ListKeys = Keys.New("Kind", "Members");

		// Token: 0x04005453 RID: 21587
		public static readonly Keys ListTypeKeys = Keys.New("Kind", "ItemType");

		// Token: 0x04005454 RID: 21588
		public static readonly Keys MultiFieldRecordProjectionKeys = Keys.New("Kind", "Expression", "MemberNames", "IsOptional");

		// Token: 0x04005455 RID: 21589
		public static readonly Keys NotImplementedKeys = Keys.New("Kind");

		// Token: 0x04005456 RID: 21590
		public static readonly Keys NullableTypeKeys = Keys.New("Kind", "ItemType");

		// Token: 0x04005457 RID: 21591
		public static readonly Keys ParameterKeys = Keys.New("Identifier", "Type");

		// Token: 0x04005458 RID: 21592
		public static readonly Keys ParenthesesKeys = Keys.New("Kind", "Expression");

		// Token: 0x04005459 RID: 21593
		public static readonly Keys RangeExpressionKeys = Keys.New("Lower", "Upper");

		// Token: 0x0400545A RID: 21594
		public static readonly Keys RangeListKeys = Keys.New("Kind", "Members");

		// Token: 0x0400545B RID: 21595
		public static readonly Keys RecordKeys = Keys.New("Kind", "Identifier", "Members");

		// Token: 0x0400545C RID: 21596
		public static readonly Keys RecordTypeKeys = Keys.New("Kind", "Fields", "Wildcard");

		// Token: 0x0400545D RID: 21597
		public static readonly Keys SectionIdentifierKeys = Keys.New("Kind", "Section", "Name");

		// Token: 0x0400545E RID: 21598
		public static readonly Keys TableTypeKeys = Keys.New("Kind", "RowType");

		// Token: 0x0400545F RID: 21599
		public static readonly Keys ThrowKeys = Keys.New("Kind", "Expression");

		// Token: 0x04005460 RID: 21600
		public static readonly Keys TryCatchKeys = Keys.New("Kind", "Try", "ExceptionCase");

		// Token: 0x04005461 RID: 21601
		public static readonly Keys TypeKeys = Keys.New("Kind", "Expression");

		// Token: 0x04005462 RID: 21602
		public static readonly Keys UnaryKeys = Keys.New("Kind", "Operator", "Expression");

		// Token: 0x04005463 RID: 21603
		public static readonly Keys VariableKeys = Keys.New("Name", "Value");

		// Token: 0x04005464 RID: 21604
		public static readonly Keys VerbatimKeys = Keys.New("Kind", "Text");
	}
}
