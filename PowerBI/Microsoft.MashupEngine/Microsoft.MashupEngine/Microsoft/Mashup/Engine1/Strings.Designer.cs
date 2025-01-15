using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x02000234 RID: 564
	internal class Strings
	{
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0002DFC2 File Offset: 0x0002C1C2
		public static ResourceManager ResourceManager
		{
			get
			{
				return Strings.ResourceLoader.Resources;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0002DFC9 File Offset: 0x0002C1C9
		public static Message0 Auth_Username
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Auth_Username");
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0002DFD5 File Offset: 0x0002C1D5
		public static Message0 Auth_Password
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Auth_Password");
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0002DFE1 File Offset: 0x0002C1E1
		public static Message0 Auth_Ldap
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Auth_Ldap");
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0002DFED File Offset: 0x0002C1ED
		public static Message0 Auth_SAS
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Auth_SAS");
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0002DFF9 File Offset: 0x0002C1F9
		public static Message0 Auth_Token
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Auth_Token");
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0002E005 File Offset: 0x0002C205
		public static Message1 Culture_NotSupported_Parsing(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Culture_NotSupported_Parsing", p0);
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0002E012 File Offset: 0x0002C212
		public static Message0 Binary_InvalidEncoding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Binary_InvalidEncoding");
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0002E01E File Offset: 0x0002C21E
		public static Message0 Compression_InvalidType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Compression_InvalidType");
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0002E02A File Offset: 0x0002C22A
		public static Message0 Encryption_InvalidAlgorithm
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Encryption_InvalidAlgorithm");
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0002E036 File Offset: 0x0002C236
		public static Message0 Bytes_FromHexString_HexStringRequiresEvenLength
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Bytes_FromHexString_HexStringRequiresEvenLength");
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0002E042 File Offset: 0x0002C242
		public static Message0 Bytes_GetHexDigit_InvalidHexDigit
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Bytes_GetHexDigit_InvalidHexDigit");
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0002E04E File Offset: 0x0002C24E
		public static Message0 Calendar_InvalidDayOfWeekError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Calendar_InvalidDayOfWeekError");
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0002E05A File Offset: 0x0002C25A
		public static Message0 Character_FromFunction_NotConvertibleToCharacter
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Character_FromFunction_NotConvertibleToCharacter");
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0002E066 File Offset: 0x0002C266
		public static Message0 CountOrCondition_CountOrConditionValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CountOrCondition_CountOrConditionValueExpectedError");
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0002E072 File Offset: 0x0002C272
		public static Message0 FieldsSelector_FieldsSelectorValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FieldsSelector_FieldsSelectorValueExpectedError");
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0002E07E File Offset: 0x0002C27E
		public static Message1 FunctionOperatesOnlyOnNonNegativeIntegers(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FunctionOperatesOnlyOnNonNegativeIntegers", p0);
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0002E08B File Offset: 0x0002C28B
		public static Message0 Guid_FromFunction_NotConvertibleToGuid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Guid_FromFunction_NotConvertibleToGuid");
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0002E097 File Offset: 0x0002C297
		public static Message0 Date_CannotParseTextAsDateTimeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_CannotParseTextAsDateTimeError");
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0002E0A3 File Offset: 0x0002C2A3
		public static Message0 Date_DateTimeValueWithDaysPartExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_DateTimeValueWithDaysPartExpectedError");
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0002E0AF File Offset: 0x0002C2AF
		public static Message0 Date_OutOfRangeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_OutOfRangeError");
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0002E0BB File Offset: 0x0002C2BB
		public static Message1 Date_DateTimeMissingComponent(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Date_DateTimeMissingComponent", p0);
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0002E0C8 File Offset: 0x0002C2C8
		public static Message0 Time_CannotParseTextAsDateTimeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Time_CannotParseTextAsDateTimeError");
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
		public static Message0 Time_InvalidOutputFormatError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Time_InvalidOutputFormatError");
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		public static Message0 Time_OutOfRangeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Time_OutOfRangeError");
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0002E0EC File Offset: 0x0002C2EC
		public static Message1 Time_DateTimeMissingComponent(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Time_DateTimeMissingComponent", p0);
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0002E0F9 File Offset: 0x0002C2F9
		public static Message0 DateTime_CannotParseTextAsDateTimeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTime_CannotParseTextAsDateTimeError");
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0002E105 File Offset: 0x0002C305
		public static Message0 DateTime_InvalidOutputFormatError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTime_InvalidOutputFormatError");
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0002E111 File Offset: 0x0002C311
		public static Message0 DateTime_OutOfRangeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTime_OutOfRangeError");
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0002E11D File Offset: 0x0002C31D
		public static Message1 DateTime_DateTimeMissingComponent(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DateTime_DateTimeMissingComponent", p0);
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0002E12A File Offset: 0x0002C32A
		public static Message0 DateTime_InvalidDayValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTime_InvalidDayValue");
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0002E136 File Offset: 0x0002C336
		public static Message0 DateTimeZone_InvalidOutputFormatError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTimeZone_InvalidOutputFormatError");
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0002E142 File Offset: 0x0002C342
		public static Message0 DateTimeZone_OutOfRangeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTimeZone_OutOfRangeError");
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0002E14E File Offset: 0x0002C34E
		public static Message0 DateTimeZone_CannotParseTextAsDateTimeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTimeZone_CannotParseTextAsDateTimeError");
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0002E15A File Offset: 0x0002C35A
		public static Message0 Duration_OutOfRangeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Duration_OutOfRangeError");
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0002E166 File Offset: 0x0002C366
		public static Message1 Duration_TicksExceedsMaximumValueError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Duration_TicksExceedsMaximumValueError", p0);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0002E173 File Offset: 0x0002C373
		public static Message0 ListComparisonCriteria_ListComparisonCriteriaValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ListComparisonCriteria_ListComparisonCriteriaValueExpectedError");
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0002E17F File Offset: 0x0002C37F
		public static Message0 ListReplacementOperations_ListReplacementOperationsValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ListReplacementOperations_ListReplacementOperationsValueExpectedError");
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0002E18B File Offset: 0x0002C38B
		public static Message0 ComparisonCriteria_NoFunctions
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ComparisonCriteria_NoFunctions");
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0002E197 File Offset: 0x0002C397
		public static Message0 List_IndexCannotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("List_IndexCannotBeNegative");
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0002E1A3 File Offset: 0x0002C3A3
		public static Message2 RecordValue_New_MismatchedKeysAndValues(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("RecordValue_New_MismatchedKeysAndValues", p0, p1);
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0002E1B1 File Offset: 0x0002C3B1
		public static Message0 Number_FormatFunction_ExpectedIntegerValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Number_FormatFunction_ExpectedIntegerValue");
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0002E1BD File Offset: 0x0002C3BD
		public static Message0 Number_FromFunction_NotConvertibleToNumber
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Number_FromFunction_NotConvertibleToNumber");
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0002E1C9 File Offset: 0x0002C3C9
		public static Message0 Number_RandBetween_TopLessThanBottom
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Number_RandBetween_TopLessThanBottom");
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0002E1D5 File Offset: 0x0002C3D5
		public static Message0 RenameOperations_RenameOperationsValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RenameOperations_RenameOperationsValueExpectedError");
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0002E1E1 File Offset: 0x0002C3E1
		public static Message0 Text_At_IndexOutOfRange
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Text_At_IndexOutOfRange");
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0002E1ED File Offset: 0x0002C3ED
		public static Message0 Text_GetEncoding_InvalidEncoding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Text_GetEncoding_InvalidEncoding");
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0002E1F9 File Offset: 0x0002C3F9
		public static Message0 TransformOperations_TransformOperationsValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TransformOperations_TransformOperationsValueExpectedError");
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0002E205 File Offset: 0x0002C405
		public static Message0 TransformOperation_TransformOperationValueExpectedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TransformOperation_TransformOperationValueExpectedError");
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0002E211 File Offset: 0x0002C411
		public static Message0 UnexpectedEndOfBuffer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnexpectedEndOfBuffer");
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0002E21D File Offset: 0x0002C41D
		public static Message0 ValueException_TextIndexOutOfRange
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_TextIndexOutOfRange");
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0002E229 File Offset: 0x0002C429
		public static Message3 ValueException_BinaryOperatorAddTypeMismatch(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_BinaryOperatorAddTypeMismatch", p0, p1, p2);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0002E238 File Offset: 0x0002C438
		public static Message3 ValueException_BinaryOperatorTypeMismatch(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_BinaryOperatorTypeMismatch", p0, p1, p2);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0002E247 File Offset: 0x0002C447
		public static Message1 ValueException_CaptureVariableHasMultipleDeclarations(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_CaptureVariableHasMultipleDeclarations", p0);
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0002E254 File Offset: 0x0002C454
		public static Message0 ValueException_CastTypeMismatch
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_CastTypeMismatch");
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0002E260 File Offset: 0x0002C460
		public static Message0 ValueException_CyclicReference
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_CyclicReference");
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0002E26C File Offset: 0x0002C46C
		public static Message1 ValueException_DuplicateExport(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_DuplicateExport", p0);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0002E279 File Offset: 0x0002C479
		public static Message1 ValueException_DuplicateField(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_DuplicateField", p0);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0002E286 File Offset: 0x0002C486
		public static Message1 ValueException_ElementAccessTypeMismatch(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_ElementAccessTypeMismatch", p0);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0002E293 File Offset: 0x0002C493
		public static Message1 ValueException_ElementAccessIndexTypeMismatch(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_ElementAccessIndexTypeMismatch", p0);
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public static Message0 ValueException_HistogramStartAndEnd
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_HistogramStartAndEnd");
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		public static Message0 ValueException_HistogramStartLessThanEnd
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_HistogramStartLessThanEnd");
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		public static Message0 ValueException_KeyNotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_KeyNotFound");
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
		public static Message0 ValueException_KeyNotUnique
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_KeyNotUnique");
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		public static Message0 ValueException_InsufficientElements
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_InsufficientElements");
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0002E2DC File Offset: 0x0002C4DC
		public static Message1 ValueException_InvalidArguments_Generic(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidArguments_Generic", p0);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0002E2E9 File Offset: 0x0002C4E9
		public static Message2 ValueException_InvalidArguments_WrongNumber_Exact(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidArguments_WrongNumber_Exact", p0, p1);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0002E2F7 File Offset: 0x0002C4F7
		public static Message2 ValueException_InvalidArguments_WrongNumber_TooFew(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidArguments_WrongNumber_TooFew", p0, p1);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0002E305 File Offset: 0x0002C505
		public static Message3 ValueException_InvalidArguments_WrongNumber_TooFewOrTooMany(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidArguments_WrongNumber_TooFewOrTooMany", p0, p1, p2);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0002E314 File Offset: 0x0002C514
		public static Message1 ValueException_InvalidArguments_WrongListLength(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidArguments_WrongListLength", p0);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0002E321 File Offset: 0x0002C521
		public static Message1 ValueException_InvalidDimensionAttribute(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidDimensionAttribute", p0);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0002E32E File Offset: 0x0002C52E
		public static Message1 ValueException_InvalidMeasure(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_InvalidMeasure", p0);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0002E33B File Offset: 0x0002C53B
		public static Message1 ValueException_MissingField(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_MissingField", p0);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0002E348 File Offset: 0x0002C548
		public static Message1 ValueException_UnexpectedField(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_UnexpectedField", p0);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0002E355 File Offset: 0x0002C555
		public static Message1 ValueException_DuplicatedField(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_DuplicatedField", p0);
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0002E362 File Offset: 0x0002C562
		public static Message0 ValueException_NoMatch
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_NoMatch");
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0002E36E File Offset: 0x0002C56E
		public static Message2 ValueException_ReasonMessageFormat(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_ReasonMessageFormat", p0, p1);
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0002E37C File Offset: 0x0002C57C
		public static Message0 ValueException_RecordIndexOutOfRange
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_RecordIndexOutOfRange");
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0002E388 File Offset: 0x0002C588
		public static Message0 ValueException_TooManyElements
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_TooManyElements");
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0002E394 File Offset: 0x0002C594
		public static Message2 ValueException_UnaryOperatorTypeMismatch(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_UnaryOperatorTypeMismatch", p0, p1);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0002E3A2 File Offset: 0x0002C5A2
		public static Message1 ValueException_UnknownImport(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_UnknownImport", p0);
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0002E3AF File Offset: 0x0002C5AF
		public static Message0 ValueMarshaller_CannotMarshalToClr
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueMarshaller_CannotMarshalToClr");
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0002E3BB File Offset: 0x0002C5BB
		public static Message0 ValueMarshaller_CannotMarshalToValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueMarshaller_CannotMarshalToValue");
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0002E3C7 File Offset: 0x0002C5C7
		public static Message0 ValueException_FunctionTypeIsAbstract
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_FunctionTypeIsAbstract");
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0002E3D3 File Offset: 0x0002C5D3
		public static Message2 DurationIsTooLarge(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DurationIsTooLarge", p0, p1);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0002E3E1 File Offset: 0x0002C5E1
		public static Message2 DurationIsTooSmall(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DurationIsTooSmall", p0, p1);
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0002E3EF File Offset: 0x0002C5EF
		public static Message0 FunctionAlreadyEnumerated
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FunctionAlreadyEnumerated");
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0002E3FB File Offset: 0x0002C5FB
		public static Message0 Duration_CannotParseDurationLiteralError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Duration_CannotParseDurationLiteralError");
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0002E407 File Offset: 0x0002C607
		public static Message0 NotImplementedFunction_NotImplemented
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NotImplementedFunction_NotImplemented");
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0002E413 File Offset: 0x0002C613
		public static Message1 ArgumentOutOfRange(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ArgumentOutOfRange", p0);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0002E420 File Offset: 0x0002C620
		public static Message1 CommaFollowedByTerminator(object p0)
		{
			return Strings.ResourceLoader.GetMessage("CommaFollowedByTerminator", p0);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0002E42D File Offset: 0x0002C62D
		public static Message1 DocumentReader_ExpectedToken(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DocumentReader_ExpectedToken", p0);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0002E43A File Offset: 0x0002C63A
		public static Message1 SemanticError_DuplicateField(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SemanticError_DuplicateField", p0);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0002E447 File Offset: 0x0002C647
		public static Message1 SemanticError_DuplicateInitializer(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SemanticError_DuplicateInitializer", p0);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0002E454 File Offset: 0x0002C654
		public static Message1 SemanticError_DuplicateParameter(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SemanticError_DuplicateParameter", p0);
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0002E461 File Offset: 0x0002C661
		public static Message0 SemanticsBuilder_ExpressionExpected
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SemanticsBuilder_ExpressionExpected");
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0002E46D File Offset: 0x0002C66D
		public static Message0 SemanticsBuilder_InvalidIdentifier
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SemanticsBuilder_InvalidIdentifier");
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0002E479 File Offset: 0x0002C679
		public static Message0 SemanticsBuilder_InvalidLiteral
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SemanticsBuilder_InvalidLiteral");
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0002E485 File Offset: 0x0002C685
		public static Message0 SemanticsBuilder_VebatimLiteralDoesNotContainError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SemanticsBuilder_VebatimLiteralDoesNotContainError");
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0002E491 File Offset: 0x0002C691
		public static Message1 SourceError_DuplicateExport(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_DuplicateExport", p0);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0002E49E File Offset: 0x0002C69E
		public static Message1 SourceError_DuplicateFieldName(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_DuplicateFieldName", p0);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0002E4AB File Offset: 0x0002C6AB
		public static Message1 SourceError_DuplicateLocal(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_DuplicateLocal", p0);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		public static Message1 SourceError_DuplicateParameter(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_DuplicateParameter", p0);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0002E4C5 File Offset: 0x0002C6C5
		public static Message1 SourceError_DuplicateSection(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_DuplicateSection", p0);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0002E4D2 File Offset: 0x0002C6D2
		public static Message1 SourceError_UnknownIdentifier(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_UnknownIdentifier", p0);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0002E4DF File Offset: 0x0002C6DF
		public static Message2 SourceError_UnknownSectionIdentifier(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_UnknownSectionIdentifier", p0, p1);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0002E4ED File Offset: 0x0002C6ED
		public static Message1 UnknownNumberPattern(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnknownNumberPattern", p0);
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0002E4FA File Offset: 0x0002C6FA
		public static Message0 NumberOutOfRangeByte
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NumberOutOfRangeByte");
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0002E506 File Offset: 0x0002C706
		public static Message0 NumberOutOfRangeDecimal
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NumberOutOfRangeDecimal");
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0002E512 File Offset: 0x0002C712
		public static Message0 NumberOutOfRangeInt32
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NumberOutOfRangeInt32");
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0002E51E File Offset: 0x0002C71E
		public static Message0 NumberOutOfRangeInt64
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NumberOutOfRangeInt64");
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0002E52A File Offset: 0x0002C72A
		public static Message2 CurrencyOutOfRange(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("CurrencyOutOfRange", p0, p1);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0002E538 File Offset: 0x0002C738
		public static Message1 Constants_ExpectedConstantValue(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Constants_ExpectedConstantValue", p0);
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0002E545 File Offset: 0x0002C745
		public static Message0 Evaluate_EvaluatedTextCannotContainSections
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Evaluate_EvaluatedTextCannotContainSections");
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0002E551 File Offset: 0x0002C751
		public static Message0 UnreachableCodePath
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnreachableCodePath");
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0002E55D File Offset: 0x0002C75D
		public static Message0 BoundFunctionNotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BoundFunctionNotFound");
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0002E569 File Offset: 0x0002C769
		public static Message0 UnboundFunctionNotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnboundFunctionNotFound");
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0002E575 File Offset: 0x0002C775
		public static Message0 ContentTypeParameterValidationError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ContentTypeParameterValidationError");
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0002E581 File Offset: 0x0002C781
		public static Message3 WebContentsFailed(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("WebContentsFailed", p0, p1, p2);
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0002E590 File Offset: 0x0002C790
		public static Message0 UriInvalidArgument
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UriInvalidArgument");
			}
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0002E59C File Offset: 0x0002C79C
		public static Message1 UriInputInvalid(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UriInputInvalid", p0);
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0002E5A9 File Offset: 0x0002C7A9
		public static Message0 UriInvalidHttps
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UriInvalidHttps");
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0002E5B5 File Offset: 0x0002C7B5
		public static Message3 RequestFailedWithStatusCode(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("RequestFailedWithStatusCode", p0, p1, p2);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		public static Message2 RequestFailedWithoutStatusCode(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("RequestFailedWithoutStatusCode", p0, p1);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0002E5D2 File Offset: 0x0002C7D2
		public static Message2 RequestFailedUsingDifferentVersions(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("RequestFailedUsingDifferentVersions", p0, p1);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
		public static Message1 ODataCannotDetermineEntityType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataCannotDetermineEntityType", p0);
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0002E5ED File Offset: 0x0002C7ED
		public static Message0 ODataUnknownOptionType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataUnknownOptionType");
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0002E5F9 File Offset: 0x0002C7F9
		public static Message0 ODataInvalidQueryOption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataInvalidQueryOption");
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0002E605 File Offset: 0x0002C805
		public static Message0 ODataTwoHeaderOptions
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataTwoHeaderOptions");
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0002E611 File Offset: 0x0002C811
		public static Message0 ODataDuplicateProperty
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataDuplicateProperty");
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0002E61D File Offset: 0x0002C81D
		public static Message0 RequestRedirectionProtocolError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RequestRedirectionProtocolError");
			}
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0002E629 File Offset: 0x0002C829
		public static Message2 ODataInvalidReaderState(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataInvalidReaderState", p0, p1);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0002E637 File Offset: 0x0002C837
		public static Message1 ODataUnsupportedFormat(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataUnsupportedFormat", p0);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0002E644 File Offset: 0x0002C844
		public static Message1 ODataAnnotationInvalidPatternEmptySegment(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataAnnotationInvalidPatternEmptySegment", p0);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0002E651 File Offset: 0x0002C851
		public static Message1 ODataAnnotationInvalidPatternWildCardInSegment(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataAnnotationInvalidPatternWildCardInSegment", p0);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0002E65E File Offset: 0x0002C85E
		public static Message1 ODataAnnotationInvalidPatternMissingDot(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataAnnotationInvalidPatternMissingDot", p0);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0002E66B File Offset: 0x0002C86B
		public static Message1 ODataAnnotationInvalidPatternWildCardMustBeInLastSegment(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataAnnotationInvalidPatternWildCardMustBeInLastSegment", p0);
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0002E678 File Offset: 0x0002C878
		public static Message0 InvalidArgument
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidArgument");
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0002E684 File Offset: 0x0002C884
		public static Message0 GenericUnsupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GenericUnsupported");
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0002E690 File Offset: 0x0002C890
		public static Message2 InvalidEnumValue(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("InvalidEnumValue", p0, p1);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0002E69E File Offset: 0x0002C89E
		public static Message1 FilePathExpected(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FilePathExpected", p0);
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0002E6AB File Offset: 0x0002C8AB
		public static Message0 RelationalAlgebra_JoinMustHaveSameColumnCountAndType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RelationalAlgebra_JoinMustHaveSameColumnCountAndType");
			}
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0002E6B7 File Offset: 0x0002C8B7
		public static Message1 RelationalAlgebra_JoinMustNotHaveColumnOverlap(object p0)
		{
			return Strings.ResourceLoader.GetMessage("RelationalAlgebra_JoinMustNotHaveColumnOverlap", p0);
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x0002E6C4 File Offset: 0x0002C8C4
		public static Message0 CultureUnawareComparer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CultureUnawareComparer");
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		public static Message0 UnsupportedCulture
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnsupportedCulture");
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x0002E6DC File Offset: 0x0002C8DC
		public static Message0 CustomComparersNotAllowed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CustomComparersNotAllowed");
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0002E6E8 File Offset: 0x0002C8E8
		public static Message0 CustomComparersResultedInInvalidSort
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CustomComparersResultedInInvalidSort");
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0002E6F4 File Offset: 0x0002C8F4
		public static Message0 InvalidComparerFormat
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidComparerFormat");
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0002E700 File Offset: 0x0002C900
		public static Message1 WebContentsManualStatusHandlingInvalidStatus(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebContentsManualStatusHandlingInvalidStatus", p0);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0002E70D File Offset: 0x0002C90D
		public static Message1 WebContentsInvalidOption(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebContentsInvalidOption", p0);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0002E71A File Offset: 0x0002C91A
		public static Message1 WebContentsInvalidPagedOption(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebContentsInvalidPagedOption", p0);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0002E727 File Offset: 0x0002C927
		public static Message3 InvalidOption(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("InvalidOption", p0, p1, p2);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0002E736 File Offset: 0x0002C936
		public static Message1 InvalidOptionWithNoValidOptions(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidOptionWithNoValidOptions", p0);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0002E743 File Offset: 0x0002C943
		public static Message1 InvalidOptionValue(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidOptionValue", p0);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0002E750 File Offset: 0x0002C950
		public static Message2 InvalidValueForOption(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("InvalidValueForOption", p0, p1);
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0002E75E File Offset: 0x0002C95E
		public static Message0 Resource_FilePath_Absolute
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_FilePath_Absolute");
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0002E76A File Offset: 0x0002C96A
		public static Message0 Resource_FolderPath_Absolute
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_FolderPath_Absolute");
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0002E776 File Offset: 0x0002C976
		public static Message0 Resource_FtpUrl_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_FtpUrl_Invalid");
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0002E782 File Offset: 0x0002C982
		public static Message0 Resource_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_Invalid");
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0002E78E File Offset: 0x0002C98E
		public static Message0 Resource_DbPath_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_DbPath_Invalid");
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0002E79A File Offset: 0x0002C99A
		public static Message0 Resource_WebUrl_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_WebUrl_Invalid");
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0002E7A6 File Offset: 0x0002C9A6
		public static Message0 Resource_CurrentWorkbookPath_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_CurrentWorkbookPath_Invalid");
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0002E7B2 File Offset: 0x0002C9B2
		public static Message0 Resource_FileUrl_Absolute
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_FileUrl_Absolute");
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0002E7BE File Offset: 0x0002C9BE
		public static Message0 ValueException_RowTypeExpected
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_RowTypeExpected");
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0002E7CA File Offset: 0x0002C9CA
		public static Message2 List_CountTooLarge(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("List_CountTooLarge", p0, p1);
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		public static Message0 HttpCredentialsBasicAuthRequiresHttps
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HttpCredentialsBasicAuthRequiresHttps");
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		public static Message0 HttpCredentialsWebApiKeyOnlyUsedWithApiKeyName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HttpCredentialsWebApiKeyOnlyUsedWithApiKeyName");
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
		public static Message0 HttpCredentialsWebApiKeyRequiresApiKeyName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HttpCredentialsWebApiKeyRequiresApiKeyName");
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		public static Message0 HttpCredentialsFeedKeyRequiresHttps
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HttpCredentialsFeedKeyRequiresHttps");
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0002E808 File Offset: 0x0002CA08
		public static Message1 WebContentsSchemeUnsupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebContentsSchemeUnsupported", p0);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0002E815 File Offset: 0x0002CA15
		public static Message1 WebActionSchemeUnsupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebActionSchemeUnsupported", p0);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0002E822 File Offset: 0x0002CA22
		public static Message1 JsonDuplicateNameError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("JsonDuplicateNameError", p0);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0002E82F File Offset: 0x0002CA2F
		public static Message2 JsonUnexpectedEndOfContent(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("JsonUnexpectedEndOfContent", p0, p1);
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0002E83D File Offset: 0x0002CA3D
		public static Message0 JsonUnexpectedToken
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonUnexpectedToken");
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0002E849 File Offset: 0x0002CA49
		public static Message0 JsonExtraCharactersAtTheEnd
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonExtraCharactersAtTheEnd");
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0002E855 File Offset: 0x0002CA55
		public static Message0 JsonInvalidBooleanError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidBooleanError");
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0002E861 File Offset: 0x0002CA61
		public static Message0 JsonInvalidNumberError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidNumberError");
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0002E86D File Offset: 0x0002CA6D
		public static Message0 JsonInvalidHexDigitError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidHexDigitError");
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0002E879 File Offset: 0x0002CA79
		public static Message0 JsonTooLongNumericLiteralError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonTooLongNumericLiteralError");
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0002E885 File Offset: 0x0002CA85
		public static Message0 JsonInvalidJsonTextError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidJsonTextError");
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0002E891 File Offset: 0x0002CA91
		public static Message0 JsonInvalidEscapeSequence
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidEscapeSequence");
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0002E89D File Offset: 0x0002CA9D
		public static Message0 JsonInvalidCharacterBeforeRecordError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidCharacterBeforeRecordError");
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0002E8A9 File Offset: 0x0002CAA9
		public static Message0 JsonInvalidRecordFormatError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidRecordFormatError");
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0002E8B5 File Offset: 0x0002CAB5
		public static Message0 JsonInvalidRecordKeyError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidRecordKeyError");
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0002E8C1 File Offset: 0x0002CAC1
		public static Message0 JsonInvalidListFormatError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonInvalidListFormatError");
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0002E8CD File Offset: 0x0002CACD
		public static Message1 JsonExtraCloseBraceError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("JsonExtraCloseBraceError", p0);
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0002E8DA File Offset: 0x0002CADA
		public static Message0 JsonLiteralUnsupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("JsonLiteralUnsupported");
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0002E8E6 File Offset: 0x0002CAE6
		public static Message2 UnsupportedQueryOption(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedQueryOption", p0, p1);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0002E8F4 File Offset: 0x0002CAF4
		public static Message1 UnsupportedExpressionKind(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedExpressionKind", p0);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0002E901 File Offset: 0x0002CB01
		public static Message1 UnsupportedMAStKind(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedMAStKind", p0);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0002E90E File Offset: 0x0002CB0E
		public static Message1 Extension_MissingDataSourceParameter(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Extension_MissingDataSourceParameter", p0);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0002E91B File Offset: 0x0002CB1B
		public static Message1 SourceError_DuplicateMember(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_DuplicateMember", p0);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0002E928 File Offset: 0x0002CB28
		public static Message1 SourceError_UnknownSection(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SourceError_UnknownSection", p0);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0002E935 File Offset: 0x0002CB35
		public static Message1 ODataInvalidUriError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataInvalidUriError", p0);
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0002E942 File Offset: 0x0002CB42
		public static Message0 SharePointCannotEnumerateDocumentAsTable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointCannotEnumerateDocumentAsTable");
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0002E94E File Offset: 0x0002CB4E
		public static Message1 ODataFailedToParseODataResult(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataFailedToParseODataResult", p0);
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0002E95B File Offset: 0x0002CB5B
		public static Message0 TableAddKey_PrimaryKeyAlreadyExists
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableAddKey_PrimaryKeyAlreadyExists");
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0002E967 File Offset: 0x0002CB67
		public static Message1 Table_ReplaceError_ColumnAlreadyExists(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Table_ReplaceError_ColumnAlreadyExists", p0);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0002E974 File Offset: 0x0002CB74
		public static Message1 Table_ColumnAlreadyExistsInTable(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Table_ColumnAlreadyExistsInTable", p0);
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0002E981 File Offset: 0x0002CB81
		public static Message0 Table_RelationshipsWithoutKey
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Table_RelationshipsWithoutKey");
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0002E98D File Offset: 0x0002CB8D
		public static Message0 SourceError_UnderscoreOutsideEach
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SourceError_UnderscoreOutsideEach");
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0002E999 File Offset: 0x0002CB99
		public static Message1 ODataUnsupportedPayload(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataUnsupportedPayload", p0);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0002E9A6 File Offset: 0x0002CBA6
		public static Message1 ODataServiceDocumentCouldNotBeParsed(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataServiceDocumentCouldNotBeParsed", p0);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0002E9B3 File Offset: 0x0002CBB3
		public static Message2 ODataFeedContainsNoServiceUri(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataFeedContainsNoServiceUri", p0, p1);
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0002E9C1 File Offset: 0x0002CBC1
		public static Message0 ODataCannotWriteData
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataCannotWriteData");
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0002E9CD File Offset: 0x0002CBCD
		public static Message0 CannotBuildCanonicalUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CannotBuildCanonicalUrl");
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0002E9D9 File Offset: 0x0002CBD9
		public static Message0 ODataCannotReadWrittenData
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataCannotReadWrittenData");
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0002E9E5 File Offset: 0x0002CBE5
		public static Message0 ODataCannotDeleteSingleton
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataCannotDeleteSingleton");
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0002E9F1 File Offset: 0x0002CBF1
		public static Message0 ODataBatchError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataBatchError");
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0002E9FD File Offset: 0x0002CBFD
		public static Message0 ODataSpatialTypeIncorrectFormat
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataSpatialTypeIncorrectFormat");
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0002EA09 File Offset: 0x0002CC09
		public static Message0 ODataInsertRestriction
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataInsertRestriction");
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0002EA15 File Offset: 0x0002CC15
		public static Message0 ODataDeleteRestriction
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataDeleteRestriction");
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0002EA21 File Offset: 0x0002CC21
		public static Message0 ODataUpdateRestriction
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataUpdateRestriction");
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0002EA2D File Offset: 0x0002CC2D
		public static Message0 Lines_RangeLength_OutOfRange
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Lines_RangeLength_OutOfRange");
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0002EA39 File Offset: 0x0002CC39
		public static Message0 Lines_RangeOffset_OutOfRange
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Lines_RangeOffset_OutOfRange");
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0002EA45 File Offset: 0x0002CC45
		public static Message1 Lines_LineTooLong(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Lines_LineTooLong", p0);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0002EA52 File Offset: 0x0002CC52
		public static Message1 Lines_LineTooLongOom(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Lines_LineTooLongOom", p0);
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0002EA5F File Offset: 0x0002CC5F
		public static Message0 List_Normalizer_TooManyColumns
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("List_Normalizer_TooManyColumns");
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0002EA6B File Offset: 0x0002CC6B
		public static Message0 TableType_FromValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableType_FromValue");
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0002EA77 File Offset: 0x0002CC77
		public static Message0 Lines_RangeList_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Lines_RangeList_Invalid");
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0002EA83 File Offset: 0x0002CC83
		public static Message0 InvalidQuoteStyle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidQuoteStyle");
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0002EA8F File Offset: 0x0002CC8F
		public static Message0 Lines_Length_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Lines_Length_Invalid");
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0002EA9B File Offset: 0x0002CC9B
		public static Message0 Lines_Position_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Lines_Position_Invalid");
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0002EAA7 File Offset: 0x0002CCA7
		public static Message0 Splitter_Delimiter_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Splitter_Delimiter_Invalid");
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0002EAB3 File Offset: 0x0002CCB3
		public static Message0 List_Normalize_Count_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("List_Normalize_Count_Invalid");
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0002EABF File Offset: 0x0002CCBF
		public static Message1 ValueException_MissingColumn(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_MissingColumn", p0);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0002EACC File Offset: 0x0002CCCC
		public static Message2 ValueException_MissingCubeColumn(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_MissingCubeColumn", p0, p1);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0002EADA File Offset: 0x0002CCDA
		public static Message1 Skip_CountTooLarge(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Skip_CountTooLarge", p0);
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0002EAE7 File Offset: 0x0002CCE7
		public static Message0 TableSortMustHaveCriterion
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableSortMustHaveCriterion");
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0002EAF3 File Offset: 0x0002CCF3
		public static Message0 ColumnSelectorInvalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ColumnSelectorInvalid");
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0002EAFF File Offset: 0x0002CCFF
		public static Message0 TableDistinctInvalidDistinctCriteria
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableDistinctInvalidDistinctCriteria");
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0002EB0B File Offset: 0x0002CD0B
		public static Message0 TableSortInvalidSortCriteria
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableSortInvalidSortCriteria");
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0002EB17 File Offset: 0x0002CD17
		public static Message0 TableSortInvalidSortCriterion
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableSortInvalidSortCriterion");
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0002EB23 File Offset: 0x0002CD23
		public static Message0 TableSortInvalidSortDirection
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableSortInvalidSortDirection");
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0002EB2F File Offset: 0x0002CD2F
		public static Message0 InvalidJoinAlgorithm
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidJoinAlgorithm");
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0002EB3B File Offset: 0x0002CD3B
		public static Message0 InvalidJoinAlgorithmForJoinKind
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidJoinAlgorithmForJoinKind");
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0002EB47 File Offset: 0x0002CD47
		public static Message1 ExcelCellReferenceInvalid(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ExcelCellReferenceInvalid", p0);
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0002EB54 File Offset: 0x0002CD54
		public static Message0 TableExpandRecordColumn_FieldAndNewColumnNamesMustHaveSameCount
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableExpandRecordColumn_FieldAndNewColumnNamesMustHaveSameCount");
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0002EB60 File Offset: 0x0002CD60
		public static Message0 TableExpandTableColumn_ColumnAndNewColumnNamesMustHaveSameCount
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableExpandTableColumn_ColumnAndNewColumnNamesMustHaveSameCount");
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		public static Message0 CubeAddAndExpandDimensionColumn_AttributeNamesAndNewColumnNamesMustHaveSameCount
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CubeAddAndExpandDimensionColumn_AttributeNamesAndNewColumnNamesMustHaveSameCount");
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0002EB78 File Offset: 0x0002CD78
		public static Message0 HtmlPageProgress
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HtmlPageProgress");
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0002EB84 File Offset: 0x0002CD84
		public static Message0 XmlFileProgress
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("XmlFileProgress");
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0002EB90 File Offset: 0x0002CD90
		public static Message0 NonDomainUserCannotUseSAMName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NonDomainUserCannotUseSAMName");
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0002EB9C File Offset: 0x0002CD9C
		public static Message0 Resource_DomainName_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_DomainName_Invalid");
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0002EBA8 File Offset: 0x0002CDA8
		public static Message0 Resource_ExchangeCredental_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_ExchangeCredental_Invalid");
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0002EBB4 File Offset: 0x0002CDB4
		public static Message0 Resource_EmailAddressAndMailboxNull
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_EmailAddressAndMailboxNull");
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
		public static Message0 Resource_ExchangeServerVersion_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_ExchangeServerVersion_NotSupported");
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0002EBCC File Offset: 0x0002CDCC
		public static Message0 Resource_ServerAddress_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_ServerAddress_Invalid");
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		public static Message1 Resource_AttachmentType_NotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Resource_AttachmentType_NotSupported", p0);
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0002EBE5 File Offset: 0x0002CDE5
		public static Message0 Resource_AutoDiscoverService_Failed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_AutoDiscoverService_Failed");
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0002EBF1 File Offset: 0x0002CDF1
		public static Message0 Resource_AutoDiscoverService_GetEwsUrl_Failed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_AutoDiscoverService_GetEwsUrl_Failed");
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0002EBFD File Offset: 0x0002CDFD
		public static Message0 Resource_AutoDiscoverService_GetServerVersion_Failed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_AutoDiscoverService_GetServerVersion_Failed");
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0002EC09 File Offset: 0x0002CE09
		public static Message0 Resource_AutoDiscoverService_Https_Failed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_AutoDiscoverService_Https_Failed");
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0002EC15 File Offset: 0x0002CE15
		public static Message0 ServerDoesNotSupportWindowsCredentialTryMicrosoftAccount
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ServerDoesNotSupportWindowsCredentialTryMicrosoftAccount");
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0002EC21 File Offset: 0x0002CE21
		public static Message0 ActiveDirectory_NotDomainJoined
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ActiveDirectory_NotDomainJoined");
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0002EC2D File Offset: 0x0002CE2D
		public static Message1 ActiveDirectory_ObjectClassNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ActiveDirectory_ObjectClassNotFound", p0);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0002EC3A File Offset: 0x0002CE3A
		public static Message1 ActiveDirectory_ServerError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ActiveDirectory_ServerError", p0);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0002EC47 File Offset: 0x0002CE47
		public static Message2 ActiveDirectory_UnsupportedSyntax(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ActiveDirectory_UnsupportedSyntax", p0, p1);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0002EC55 File Offset: 0x0002CE55
		public static Message1 ActiveDirectory_DomainNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ActiveDirectory_DomainNotFound", p0);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0002EC62 File Offset: 0x0002CE62
		public static Message1 ActiveDirectory_ObjectNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ActiveDirectory_ObjectNotFound", p0);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0002EC6F File Offset: 0x0002CE6F
		public static Message1 FileAccessDenied(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FileAccessDenied", p0);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0002EC7C File Offset: 0x0002CE7C
		public static Message1 FileDirectoryNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FileDirectoryNotFound", p0);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0002EC89 File Offset: 0x0002CE89
		public static Message2 FileIOError(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("FileIOError", p0, p1);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0002EC97 File Offset: 0x0002CE97
		public static Message1 FileNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FileNotFound", p0);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0002ECA4 File Offset: 0x0002CEA4
		public static Message1 FilePathInvalid(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FilePathInvalid", p0);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0002ECB1 File Offset: 0x0002CEB1
		public static Message1 FilePathTooLong(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FilePathTooLong", p0);
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0002ECBE File Offset: 0x0002CEBE
		public static Message0 ODataQueryTooLong
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataQueryTooLong");
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0002ECCA File Offset: 0x0002CECA
		public static Message4 HDInsightFailed(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("HDInsightFailed", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0002ECEC File Offset: 0x0002CEEC
		public static Message0 HdfsEmptyUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HdfsEmptyUrl");
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0002ECF8 File Offset: 0x0002CEF8
		public static Message3 HdfsFailed(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("HdfsFailed", p0, p1, p2);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0002ED07 File Offset: 0x0002CF07
		public static Message2 HdfsFailedToResolveServer(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("HdfsFailedToResolveServer", p0, p1);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0002ED15 File Offset: 0x0002CF15
		public static Message3 HDInsightFailedXmlException(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("HDInsightFailedXmlException", p0, p1, p2);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0002ED24 File Offset: 0x0002CF24
		public static Message2 AzureStorageIOException(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("AzureStorageIOException", p0, p1);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0002ED32 File Offset: 0x0002CF32
		public static Message2 WebRequestFailed(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("WebRequestFailed", p0, p1);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0002ED40 File Offset: 0x0002CF40
		public static Message1 HdfsFileStatusExpected(object p0)
		{
			return Strings.ResourceLoader.GetMessage("HdfsFileStatusExpected", p0);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0002ED4D File Offset: 0x0002CF4D
		public static Message1 HdfsInvalidUrl(object p0)
		{
			return Strings.ResourceLoader.GetMessage("HdfsInvalidUrl", p0);
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0002ED5A File Offset: 0x0002CF5A
		public static Message0 ExpectedSingleDataSourceFunction
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ExpectedSingleDataSourceFunction");
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0002ED66 File Offset: 0x0002CF66
		public static Message2 ValueException_CastTypeMismatch_Complex(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_CastTypeMismatch_Complex", p0, p1);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0002ED74 File Offset: 0x0002CF74
		public static Message2 ValueException_CastTypeMismatch_Simple(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_CastTypeMismatch_Simple", p0, p1);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0002ED82 File Offset: 0x0002CF82
		public static Message1 ValueException_CastTypeMismatch_Types(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_CastTypeMismatch_Types", p0);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0002ED8F File Offset: 0x0002CF8F
		public static Message1 ValueException_CastTypeMismatch_OleDb(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_CastTypeMismatch_OleDb", p0);
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		public static Message0 PageReader_UnsupportedShape
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("PageReader_UnsupportedShape");
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		public static Message2 Table_FromColumns_ColumnsAndColumnNamesCountMismatch(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Table_FromColumns_ColumnsAndColumnNamesCountMismatch", p0, p1);
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0002EDB6 File Offset: 0x0002CFB6
		public static Message0 ValueException_UnderscoreOutsideEach
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_UnderscoreOutsideEach");
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0002EDC2 File Offset: 0x0002CFC2
		public static Message2 NewerCLRNeeded(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("NewerCLRNeeded", p0, p1);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0002EDD0 File Offset: 0x0002CFD0
		public static Message2 DatabaseClientMissingExceptionMessage32bit(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseClientMissingExceptionMessage32bit", p0, p1);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0002EDDE File Offset: 0x0002CFDE
		public static Message2 DatabaseClientMissingExceptionMessage64bit(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseClientMissingExceptionMessage64bit", p0, p1);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0002EDEC File Offset: 0x0002CFEC
		public static Message1 PrivateAdoNetDriverMissingAssemblyMessage(object p0)
		{
			return Strings.ResourceLoader.GetMessage("PrivateAdoNetDriverMissingAssemblyMessage", p0);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0002EDF9 File Offset: 0x0002CFF9
		public static Message1 NoOdacDriverIsFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("NoOdacDriverIsFound", p0);
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0002EE06 File Offset: 0x0002D006
		public static Message0 Table_FromList_ExtraValueListRequiresAtLeastOneColumn
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Table_FromList_ExtraValueListRequiresAtLeastOneColumn");
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0002EE12 File Offset: 0x0002D012
		public static Message0 SharePointInvalidUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointInvalidUrl");
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0002EE1E File Offset: 0x0002D01E
		public static Message0 SharePointInvalidFileUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointInvalidFileUrl");
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0002EE2A File Offset: 0x0002D02A
		public static Message0 SharePointInvalidFileUrlWebEqualsOne
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointInvalidFileUrlWebEqualsOne");
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0002EE36 File Offset: 0x0002D036
		public static Message1 ODataServiceDocumentCouldNotBeAccessed(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataServiceDocumentCouldNotBeAccessed", p0);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0002EE43 File Offset: 0x0002D043
		public static Message1 ODataUnsupportedUri(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataUnsupportedUri", p0);
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0002EE50 File Offset: 0x0002D050
		public static Message0 DocumentReader_InvalidPrimitiveType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DocumentReader_InvalidPrimitiveType");
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0002EE5C File Offset: 0x0002D05C
		public static Message0 DocumentReader_InvalidRequiredArgument
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DocumentReader_InvalidRequiredArgument");
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0002EE68 File Offset: 0x0002D068
		public static Message1 DocumentReader_DuplicateSection(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DocumentReader_DuplicateSection", p0);
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0002EE75 File Offset: 0x0002D075
		public static Message0 DocumentReader_BadCatch
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DocumentReader_BadCatch");
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0002EE81 File Offset: 0x0002D081
		public static Message0 TableSortInvalidColumnSelector
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableSortInvalidColumnSelector");
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0002EE8D File Offset: 0x0002D08D
		public static Message0 InvalidEmailAddress
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidEmailAddress");
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0002EE99 File Offset: 0x0002D099
		public static Message0 ActiveDirectory_QueryTooComplex
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ActiveDirectory_QueryTooComplex");
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0002EEA5 File Offset: 0x0002D0A5
		public static Message1 DatabaseProviderMissingExceptionMessage(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseProviderMissingExceptionMessage", p0);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0002EEB2 File Offset: 0x0002D0B2
		public static Message2 DatabaseProviderConfigurationErrorExceptionMessage(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseProviderConfigurationErrorExceptionMessage", p0, p1);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		public static Message3 DatabaseProviderUpgradeMessage(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseProviderUpgradeMessage", p0, p1, p2);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0002EECF File Offset: 0x0002D0CF
		public static Message1 DatabaseProviderIncompatibleNETVersionMessage(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseProviderIncompatibleNETVersionMessage", p0);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0002EEDC File Offset: 0x0002D0DC
		public static Message2 DatabaseProviderDowngradeMessage(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DatabaseProviderDowngradeMessage", p0, p1);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0002EEEA File Offset: 0x0002D0EA
		public static Message1 InvalidMetadataDocument(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidMetadataDocument", p0);
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0002EEF7 File Offset: 0x0002D0F7
		public static Message0 NoMetadataDocument
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NoMetadataDocument");
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0002EF03 File Offset: 0x0002D103
		public static Message0 TableSplitColumnArgumentTypeError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableSplitColumnArgumentTypeError");
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0002EF0F File Offset: 0x0002D10F
		public static Message0 InvalidJoinKind
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidJoinKind");
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0002EF1B File Offset: 0x0002D11B
		public static Message0 InvalidJoinSide
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidJoinSide");
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0002EF27 File Offset: 0x0002D127
		public static Message0 ExcelInvalidInput
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ExcelInvalidInput");
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0002EF33 File Offset: 0x0002D133
		public static Message1 ExcelCantFindPart(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ExcelCantFindPart", p0);
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0002EF40 File Offset: 0x0002D140
		public static Message0 InvalidMissingFieldMode_ErrorOrUseNull
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidMissingFieldMode_ErrorOrUseNull");
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0002EF4C File Offset: 0x0002D14C
		public static Message0 TableFromRecordsColumnTypesMustBeNullable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableFromRecordsColumnTypesMustBeNullable");
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0002EF58 File Offset: 0x0002D158
		public static Message1 ActiveDirectory_AttributeNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ActiveDirectory_AttributeNotFound", p0);
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0002EF65 File Offset: 0x0002D165
		public static Message0 AzureInvalidAccountURL
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AzureInvalidAccountURL");
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0002EF71 File Offset: 0x0002D171
		public static Message0 AzureInvalidAccountScheme
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AzureInvalidAccountScheme");
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0002EF7D File Offset: 0x0002D17D
		public static Message0 AzureInvalidAccountQueryParameters
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AzureInvalidAccountQueryParameters");
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0002EF89 File Offset: 0x0002D189
		public static Message0 AzureTableInvalidAccountUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AzureTableInvalidAccountUrl");
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0002EF95 File Offset: 0x0002D195
		public static Message0 AzureInvalidAccountName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AzureInvalidAccountName");
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0002EFA1 File Offset: 0x0002D1A1
		public static Message0 AzureInvalidContainerName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AzureInvalidContainerName");
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0002EFAD File Offset: 0x0002D1AD
		public static Message0 Binary_NotConvertibleToBinary
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Binary_NotConvertibleToBinary");
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0002EFB9 File Offset: 0x0002D1B9
		public static Message0 DateTimeZone_NotConvertibleToDateTimeZone
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTimeZone_NotConvertibleToDateTimeZone");
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0002EFC5 File Offset: 0x0002D1C5
		public static Message0 DateTime_NotConvertibleToDateTime
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DateTime_NotConvertibleToDateTime");
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0002EFD1 File Offset: 0x0002D1D1
		public static Message0 Date_NotConvertibleToDate
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_NotConvertibleToDate");
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0002EFDD File Offset: 0x0002D1DD
		public static Message0 Date_DaysMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_DaysMayNotBeNegative");
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0002EFE9 File Offset: 0x0002D1E9
		public static Message0 Date_WeeksMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_WeeksMayNotBeNegative");
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0002EFF5 File Offset: 0x0002D1F5
		public static Message0 Date_MonthsMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_MonthsMayNotBeNegative");
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0002F001 File Offset: 0x0002D201
		public static Message0 Date_QuartersMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_QuartersMayNotBeNegative");
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0002F00D File Offset: 0x0002D20D
		public static Message0 Date_YearsMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Date_YearsMayNotBeNegative");
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0002F019 File Offset: 0x0002D219
		public static Message0 Duration_NotConvertibleToDuration
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Duration_NotConvertibleToDuration");
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0002F025 File Offset: 0x0002D225
		public static Message0 Logical_NotConvertibleToLogical
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Logical_NotConvertibleToLogical");
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0002F031 File Offset: 0x0002D231
		public static Message0 Time_NotConvertibleToTime
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Time_NotConvertibleToTime");
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0002F03D File Offset: 0x0002D23D
		public static Message1 XmlError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("XmlError", p0);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0002F04A File Offset: 0x0002D24A
		public static Message1 ValueException_ElementAccessByKeyTypeMismatch(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_ElementAccessByKeyTypeMismatch", p0);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0002F057 File Offset: 0x0002D257
		public static Message1 ValueException_ExcelTableNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_ExcelTableNotFound", p0);
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x0002F064 File Offset: 0x0002D264
		public static Message0 Table_TransformColumnTypes_UnrecognizedType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Table_TransformColumnTypes_UnrecognizedType");
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0002F070 File Offset: 0x0002D270
		public static Message2 WebPageHttpError(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("WebPageHttpError", p0, p1);
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0002F07E File Offset: 0x0002D27E
		public static Message0 WebPageInvalidCertificate
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebPageInvalidCertificate");
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0002F08A File Offset: 0x0002D28A
		public static Message0 WebPageUnknownError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebPageUnknownError");
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0002F096 File Offset: 0x0002D296
		public static Message1 JS_Error(object p0)
		{
			return Strings.ResourceLoader.GetMessage("JS_Error", p0);
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0002F0A3 File Offset: 0x0002D2A3
		public static Message0 WebPageResourceNotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebPageResourceNotFound");
			}
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0002F0AF File Offset: 0x0002D2AF
		public static Message3 AccessDatabaseEngine2010Missing(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("AccessDatabaseEngine2010Missing", p0, p1, p2);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0002F0BE File Offset: 0x0002D2BE
		public static Message4 AccessDatabaseEngine2010Missing_WithFileName(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("AccessDatabaseEngine2010Missing_WithFileName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0002F0E0 File Offset: 0x0002D2E0
		public static Message3 AccessDatabaseEngine2010BitnessMismatch(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("AccessDatabaseEngine2010BitnessMismatch", p0, p1, p2);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0002F0EF File Offset: 0x0002D2EF
		public static Message4 AccessDatabaseEngine2010BitnessMismatch_WithFileName(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("AccessDatabaseEngine2010BitnessMismatch_WithFileName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0002F111 File Offset: 0x0002D311
		public static Message0 WebContentsRequestWithCredentialsError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebContentsRequestWithCredentialsError");
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0002F11D File Offset: 0x0002D31D
		public static Message2 WebContentsWithUnapprovedHeaders(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("WebContentsWithUnapprovedHeaders", p0, p1);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0002F12B File Offset: 0x0002D32B
		public static Message1 GenericInvalidOption(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GenericInvalidOption", p0);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0002F138 File Offset: 0x0002D338
		public static Message1 UnsupportedHeader(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedHeader", p0);
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0002F145 File Offset: 0x0002D345
		public static Message0 ValueException_DatabaseCannotBeEmpty
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_DatabaseCannotBeEmpty");
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0002F151 File Offset: 0x0002D351
		public static Message0 ValueException_HistogramBucketCountError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_HistogramBucketCountError");
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0002F15D File Offset: 0x0002D35D
		public static Message0 ValueException_ServerCannotBeEmpty
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_ServerCannotBeEmpty");
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0002F169 File Offset: 0x0002D369
		public static Message0 TableFromPartitions_InvalidPartition
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableFromPartitions_InvalidPartition");
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0002F175 File Offset: 0x0002D375
		public static Message0 Binary_UnexpectedPreamble
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Binary_UnexpectedPreamble");
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0002F181 File Offset: 0x0002D381
		public static Message0 BinaryFormat_7BitEncodedValueTooLarge
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_7BitEncodedValueTooLarge");
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0002F18D File Offset: 0x0002D38D
		public static Message0 BinaryFormat_CountMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_CountMayNotBeNegative");
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0002F199 File Offset: 0x0002D399
		public static Message0 BinaryFormat_CountOrConditionInvalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_CountOrConditionInvalid");
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0002F1A5 File Offset: 0x0002D3A5
		public static Message0 BinaryFormat_DoesNotProduceBinary
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_DoesNotProduceBinary");
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0002F1B1 File Offset: 0x0002D3B1
		public static Message0 BinaryFormat_DoesNotProduceList
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_DoesNotProduceList");
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0002F1BD File Offset: 0x0002D3BD
		public static Message1 BinaryFormat_EndOfInput(object p0)
		{
			return Strings.ResourceLoader.GetMessage("BinaryFormat_EndOfInput", p0);
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0002F1CA File Offset: 0x0002D3CA
		public static Message0 BinaryFormat_ExpectBinaryFormat
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_ExpectBinaryFormat");
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0002F1D6 File Offset: 0x0002D3D6
		public static Message0 BinaryFormat_InvalidType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_InvalidType");
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0002F1E2 File Offset: 0x0002D3E2
		public static Message0 BinaryFormat_LengthMayNotBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_LengthMayNotBeNegative");
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0002F1EE File Offset: 0x0002D3EE
		public static Message1 BinaryFormat_NotEnoughRead(object p0)
		{
			return Strings.ResourceLoader.GetMessage("BinaryFormat_NotEnoughRead", p0);
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0002F1FB File Offset: 0x0002D3FB
		public static Message0 Text_InvalidUnicodeCodePoints
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Text_InvalidUnicodeCodePoints");
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0002F207 File Offset: 0x0002D407
		public static Message0 BinaryFormat_InvalidDecimalBytes
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_InvalidDecimalBytes");
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0002F213 File Offset: 0x0002D413
		public static Message0 BinaryFormat_GroupItemInvalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_GroupItemInvalid");
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0002F21F File Offset: 0x0002D41F
		public static Message0 BinaryFormat_GroupItemRequired
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_GroupItemRequired");
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0002F22B File Offset: 0x0002D42B
		public static Message0 BinaryFormat_GroupItemUnknown
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_GroupItemUnknown");
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0002F237 File Offset: 0x0002D437
		public static Message0 BinaryFormat_LengthReadTooLarge
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_LengthReadTooLarge");
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0002F243 File Offset: 0x0002D443
		public static Message0 BinaryFormat_NotSupportedForLength
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_NotSupportedForLength");
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0002F24F File Offset: 0x0002D44F
		public static Message0 BinaryFormat_NotSupportedForCount
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_NotSupportedForCount");
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0002F25B File Offset: 0x0002D45B
		public static Message0 BinaryFormat_CountReadTooLarge
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_CountReadTooLarge");
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0002F267 File Offset: 0x0002D467
		public static Message0 BinaryFormat_InvalidTypeWhenCombining
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BinaryFormat_InvalidTypeWhenCombining");
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0002F273 File Offset: 0x0002D473
		public static Message0 NativeQuery_NullSchema
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NativeQuery_NullSchema");
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0002F27F File Offset: 0x0002D47F
		public static Message1 NativeQuery_TypeNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("NativeQuery_TypeNotSupported", p0);
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0002F28C File Offset: 0x0002D48C
		public static Message0 NativeQuery_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NativeQuery_NotSupported");
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0002F298 File Offset: 0x0002D498
		public static Message0 NativeQuery_NotSupported_CrossDatabaseFolding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NativeQuery_NotSupported_CrossDatabaseFolding");
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0002F2A4 File Offset: 0x0002D4A4
		public static Message0 NativeQuery_Unspecified
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NativeQuery_Unspecified");
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		public static Message0 WebPageJavascriptDisabled
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebPageJavascriptDisabled");
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		public static Message1 WebPageTimedOut(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebPageTimedOut", p0);
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0002F2C9 File Offset: 0x0002D4C9
		public static Message0 SapInvalidResponse
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapInvalidResponse");
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0002F2D5 File Offset: 0x0002D4D5
		public static Message0 SqlRelationshipQueryFailed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SqlRelationshipQueryFailed");
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0002F2E1 File Offset: 0x0002D4E1
		public static Message0 Cube_ValueNotACube
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_ValueNotACube");
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0002F2ED File Offset: 0x0002D4ED
		public static Message0 Cube_QueryNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_QueryNotSupported");
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0002F2F9 File Offset: 0x0002D4F9
		public static Message0 Cube_NotInSameMeasureGroup
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_NotInSameMeasureGroup");
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0002F305 File Offset: 0x0002D505
		public static Message0 Cube_FunctionNotAMeasure
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_FunctionNotAMeasure");
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0002F311 File Offset: 0x0002D511
		public static Message0 Cube_FunctionNotAParameter
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_FunctionNotAParameter");
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0002F31D File Offset: 0x0002D51D
		public static Message1 Cube_ColumnNotDimensionAttribute(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Cube_ColumnNotDimensionAttribute", p0);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0002F32A File Offset: 0x0002D52A
		public static Message2 Cube_PropertyRequiresDimensionAttribute(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Cube_PropertyRequiresDimensionAttribute", p0, p1);
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0002F338 File Offset: 0x0002D538
		public static Message0 SapUniversesAccessDeniedError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapUniversesAccessDeniedError");
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0002F344 File Offset: 0x0002D544
		public static Message0 WebContentsHtmlNotExpected
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebContentsHtmlNotExpected");
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0002F350 File Offset: 0x0002D550
		public static Message0 NoWebContentsWithFileAndImpersonation
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NoWebContentsWithFileAndImpersonation");
			}
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0002F35C File Offset: 0x0002D55C
		public static Message1 IoError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("IoError", p0);
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x0002F369 File Offset: 0x0002D569
		public static Message0 DocumentReader_ParseDepth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DocumentReader_ParseDepth");
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0002F375 File Offset: 0x0002D575
		public static Message0 SapServiceError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapServiceError");
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0002F381 File Offset: 0x0002D581
		public static Message1 SapServiceErrorWithMessage(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapServiceErrorWithMessage", p0);
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0002F38E File Offset: 0x0002D58E
		public static Message0 ValueException_CharacterError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_CharacterError");
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0002F39A File Offset: 0x0002D59A
		public static Message1 InvalidCellValue(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidCellValue", p0);
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0002F3A7 File Offset: 0x0002D5A7
		public static Message0 UnsupportedFunctionParameterType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnsupportedFunctionParameterType");
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0002F3B3 File Offset: 0x0002D5B3
		public static Message1 ODataFunctionParameterCollectionTypeMismatch(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataFunctionParameterCollectionTypeMismatch", p0);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0002F3C0 File Offset: 0x0002D5C0
		public static Message1 ODataFunctionParameterConversionNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataFunctionParameterConversionNotSupported", p0);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0002F3CD File Offset: 0x0002D5CD
		public static Message1 ODataFunctionParameterRecordTypeMismatch(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataFunctionParameterRecordTypeMismatch", p0);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0002F3DA File Offset: 0x0002D5DA
		public static Message2 ODataFunctionParameterRecordTypeMissingProperties(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataFunctionParameterRecordTypeMissingProperties", p0, p1);
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		public static Message0 MySqlFormatException
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("MySqlFormatException");
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		public static Message1 ODataBatchInnerPayloadKindNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataBatchInnerPayloadKindNotSupported", p0);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0002F401 File Offset: 0x0002D601
		public static Message1 ODataBatchPayloadStateNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataBatchPayloadStateNotSupported", p0);
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0002F40E File Offset: 0x0002D60E
		public static Message0 ODataBatchSupportsOnlyOneInnerPayload
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataBatchSupportsOnlyOneInnerPayload");
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0002F41A File Offset: 0x0002D61A
		public static Message1 ODataBatchMultiPartMimeContentTypeHeaderInvalid(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataBatchMultiPartMimeContentTypeHeaderInvalid", p0);
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0002F427 File Offset: 0x0002D627
		public static Message0 SqlUnsortedPagingNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SqlUnsortedPagingNotSupported");
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0002F433 File Offset: 0x0002D633
		public static Message0 PivotColumn_NestedData_Error
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("PivotColumn_NestedData_Error");
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0002F43F File Offset: 0x0002D63F
		public static Message1 ODataJsonLightContextUriInvalid(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataJsonLightContextUriInvalid", p0);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0002F44C File Offset: 0x0002D64C
		public static Message1 ODataJsonLightContextUriParserUnableToConvertEdmTypeToTypeReference(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataJsonLightContextUriParserUnableToConvertEdmTypeToTypeReference", p0);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0002F459 File Offset: 0x0002D659
		public static Message1 ODataJsonLightContextUriParserUnableToResolveByTypeName(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataJsonLightContextUriParserUnableToResolveByTypeName", p0);
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0002F466 File Offset: 0x0002D666
		public static Message0 HttpResponseDetectionFailed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("HttpResponseDetectionFailed");
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0002F472 File Offset: 0x0002D672
		public static Message2 InvalidHeaderValueType(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("InvalidHeaderValueType", p0, p1);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0002F480 File Offset: 0x0002D680
		public static Message1 VariableNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("VariableNotFound", p0);
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0002F48D File Offset: 0x0002D68D
		public static Message0 SapContextResolutionNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapContextResolutionNotSupported");
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0002F499 File Offset: 0x0002D699
		public static Message0 Oledb_ProviderPropertyRequiredError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Oledb_ProviderPropertyRequiredError");
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0002F4A5 File Offset: 0x0002D6A5
		public static Message1 GenericProviders_InvalidConnectionString(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_InvalidConnectionString", p0);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0002F4B2 File Offset: 0x0002D6B2
		public static Message4 GenericProviders_UnsupportedConnectionStringType(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_UnsupportedConnectionStringType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
		public static Message1 GenericProviders_InvalidSourceProperty(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_InvalidSourceProperty", p0);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0002F4E1 File Offset: 0x0002D6E1
		public static Message1 Odbc_InvalidDriverName(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Odbc_InvalidDriverName", p0);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0002F4EE File Offset: 0x0002D6EE
		public static Message1 GenericProviders_InvalidCredentialProperty(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_InvalidCredentialProperty", p0);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0002F4FB File Offset: 0x0002D6FB
		public static Message1 GenericProviders_WindowsAlternateAuthNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_WindowsAlternateAuthNotSupported", p0);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0002F508 File Offset: 0x0002D708
		public static Message4 GenericProviders_UnsupportedConnectionStringValueType(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_UnsupportedConnectionStringValueType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x0002F52A File Offset: 0x0002D72A
		public static Message0 GenericProvider_InvalidResourcePath
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GenericProvider_InvalidResourcePath");
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0002F536 File Offset: 0x0002D736
		public static Message1 GenericProviders_PropertyNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GenericProviders_PropertyNotSupported", p0);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0002F543 File Offset: 0x0002D743
		public static Message1 WebPageParseError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebPageParseError", p0);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0002F550 File Offset: 0x0002D750
		public static Message1 InvalidSalesforceUri(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidSalesforceUri", p0);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0002F55D File Offset: 0x0002D75D
		public static Message1 InvalidSalesforceApiVersion(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidSalesforceApiVersion", p0);
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0002F56A File Offset: 0x0002D76A
		public static Message0 SalesforceApiVersionRequired
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SalesforceApiVersionRequired");
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0002F576 File Offset: 0x0002D776
		public static Message1 ReadingFromProviderError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ReadingFromProviderError", p0);
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0002F583 File Offset: 0x0002D783
		public static Message0 ErrorFetchingSalesforceData
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ErrorFetchingSalesforceData");
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0002F58F File Offset: 0x0002D78F
		public static Message0 SalesforceChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SalesforceChallengeTitle");
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0002F59B File Offset: 0x0002D79B
		public static Message0 Salesforce_OAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Salesforce_OAuth");
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0002F5A7 File Offset: 0x0002D7A7
		public static Message0 Microsoft_OAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Microsoft_OAuth");
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0002F5B3 File Offset: 0x0002D7B3
		public static Message0 AnalysisServicesChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesChallengeTitle");
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0002F5BF File Offset: 0x0002D7BF
		public static Message0 AnalysisServices_WindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServices_WindowsAuth");
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0002F5CB File Offset: 0x0002D7CB
		public static Message1 AnalysisServicesInvalidServiceResponse(object p0)
		{
			return Strings.ResourceLoader.GetMessage("AnalysisServicesInvalidServiceResponse", p0);
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0002F5D8 File Offset: 0x0002D7D8
		public static Message0 AnalysisServicesKpiGoal
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesKpiGoal");
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0002F5E4 File Offset: 0x0002D7E4
		public static Message0 AnalysisServicesKpiStatus
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesKpiStatus");
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0002F5F0 File Offset: 0x0002D7F0
		public static Message0 AnalysisServicesKpiTrend
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesKpiTrend");
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0002F5FC File Offset: 0x0002D7FC
		public static Message0 AnalysisServicesKpiValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesKpiValue");
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0002F608 File Offset: 0x0002D808
		public static Message3 AnalysisServicesProviderMissing(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("AnalysisServicesProviderMissing", p0, p1, p2);
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0002F617 File Offset: 0x0002D817
		public static Message0 AnalysisServicesUnsupportedServiceVersion
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesUnsupportedServiceVersion");
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0002F623 File Offset: 0x0002D823
		public static Message0 WebPageContentsNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebPageContentsNotSupported");
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0002F62F File Offset: 0x0002D82F
		public static Message0 Pattern_ValueDidNotMatchPattern
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Pattern_ValueDidNotMatchPattern");
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0002F63B File Offset: 0x0002D83B
		public static Message0 Extensibility_MissingOrInvalidResourceKind
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Extensibility_MissingOrInvalidResourceKind");
			}
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0002F647 File Offset: 0x0002D847
		public static Message2 Extensibility_UnknownAuthenticationType(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Extensibility_UnknownAuthenticationType", p0, p1);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0002F655 File Offset: 0x0002D855
		public static Message1 Extensibility_NoAuthenticationTypes(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Extensibility_NoAuthenticationTypes", p0);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0002F662 File Offset: 0x0002D862
		public static Message2 Extensibility_MissingOrInvalidProperty(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Extensibility_MissingOrInvalidProperty", p0, p1);
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0002F670 File Offset: 0x0002D870
		public static Message0 Extensibility_NotAvailable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Extensibility_NotAvailable");
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0002F67C File Offset: 0x0002D87C
		public static Message0 Extensibility_InvalidCredentialType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Extensibility_InvalidCredentialType");
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0002F688 File Offset: 0x0002D888
		public static Message0 Extensibility_CantReplaceBuiltin
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Extensibility_CantReplaceBuiltin");
			}
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0002F694 File Offset: 0x0002D894
		public static Message2 Extensibility_PropertiesNotSupported(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Extensibility_PropertiesNotSupported", p0, p1);
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0002F6A2 File Offset: 0x0002D8A2
		public static Message0 Extensibility_NoResources
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Extensibility_NoResources");
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0002F6AE File Offset: 0x0002D8AE
		public static Message1 DecompileFunctionFailed(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DecompileFunctionFailed", p0);
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0002F6BB File Offset: 0x0002D8BB
		public static Message0 OdbcChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcChallengeTitle");
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0002F6C7 File Offset: 0x0002D8C7
		public static Message0 OdbcWindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcWindowsAuth");
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0002F6D3 File Offset: 0x0002D8D3
		public static Message0 OdbcSqlAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcSqlAuth");
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0002F6DF File Offset: 0x0002D8DF
		public static Message0 OleDbSqlAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbSqlAuth");
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0002F6EB File Offset: 0x0002D8EB
		public static Message0 OleDbChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbChallengeTitle");
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0002F6F7 File Offset: 0x0002D8F7
		public static Message0 OleDbWindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbWindowsAuth");
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0002F703 File Offset: 0x0002D903
		public static Message0 AdoDotNetChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdoDotNetChallengeTitle");
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0002F70F File Offset: 0x0002D90F
		public static Message0 AdoDotNetWindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdoDotNetWindowsAuth");
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0002F71B File Offset: 0x0002D91B
		public static Message0 AdoDotNetSqlAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdoDotNetSqlAuth");
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0002F727 File Offset: 0x0002D927
		public static Message0 AdoDotNetNoneAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdoDotNetNoneAuth");
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0002F733 File Offset: 0x0002D933
		public static Message0 OdbcNoneAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcNoneAuth");
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0002F73F File Offset: 0x0002D93F
		public static Message0 OleDbNoneAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbNoneAuth");
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0002F74B File Offset: 0x0002D94B
		public static Message0 GenericProvidersNoneAuthLabel
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GenericProvidersNoneAuthLabel");
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0002F757 File Offset: 0x0002D957
		public static Message1 WindowsNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WindowsNotSupported", p0);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0002F764 File Offset: 0x0002D964
		public static Message1 UsernamePasswordNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UsernamePasswordNotSupported", p0);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0002F771 File Offset: 0x0002D971
		public static Message1 OnlyAnonymousSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OnlyAnonymousSupported", p0);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0002F77E File Offset: 0x0002D97E
		public static Message1 ResourceAlreadyRegistered(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ResourceAlreadyRegistered", p0);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0002F78B File Offset: 0x0002D98B
		public static Message1 InvalidSalesforceLoginUri(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidSalesforceLoginUri", p0);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0002F798 File Offset: 0x0002D998
		public static Message2 DataSourceExceptionMessage(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DataSourceExceptionMessage", p0, p1);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0002F7A6 File Offset: 0x0002D9A6
		public static Message2 ODataCommonError(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataCommonError", p0, p1);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
		public static Message2 OptionsRecord_Unsupported(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("OptionsRecord_Unsupported", p0, p1);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0002F7C2 File Offset: 0x0002D9C2
		public static Message1 OptionsRecord_UnsupportedKey(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OptionsRecord_UnsupportedKey", p0);
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0002F7CF File Offset: 0x0002D9CF
		public static Message0 NumberOutOfRangeInt16
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NumberOutOfRangeInt16");
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0002F7DB File Offset: 0x0002D9DB
		public static Message0 NumberOutOfRangeInt8
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NumberOutOfRangeInt8");
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0002F7E7 File Offset: 0x0002D9E7
		public static Message1 UnsupportedODataVersion(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedODataVersion", p0);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
		public static Message1 UnsupportedSharePointVersionNumber(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedSharePointVersionNumber", p0);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0002F801 File Offset: 0x0002DA01
		public static Message1 UnsupportedSharePointVersion(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedSharePointVersion", p0);
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0002F80E File Offset: 0x0002DA0E
		public static Message0 NullCharNotallowedInUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NullCharNotallowedInUrl");
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0002F81A File Offset: 0x0002DA1A
		public static Message0 GenericProviders_EmptyConnectionString
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GenericProviders_EmptyConnectionString");
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0002F826 File Offset: 0x0002DA26
		public static Message0 AnalysisServicesInvalidServer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesInvalidServer");
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0002F832 File Offset: 0x0002DA32
		public static Message1 OdbcConnectionPoolingInitError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OdbcConnectionPoolingInitError", p0);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0002F83F File Offset: 0x0002DA3F
		public static Message2 ConfigurationErrorsExceptionMessage(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ConfigurationErrorsExceptionMessage", p0, p1);
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0002F84D File Offset: 0x0002DA4D
		public static Message0 ODataVersion2
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataVersion2");
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0002F859 File Offset: 0x0002DA59
		public static Message0 ODataVersion3
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataVersion3");
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0002F865 File Offset: 0x0002DA65
		public static Message0 ODataVersion3And4
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataVersion3And4");
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0002F871 File Offset: 0x0002DA71
		public static Message0 ODataVersion4
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataVersion4");
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0002F87D File Offset: 0x0002DA7D
		public static Message0 DataSourceLocation_Address_contentType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_contentType");
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0002F889 File Offset: 0x0002DA89
		public static Message0 DataSourceLocation_Address_database
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_database");
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0002F895 File Offset: 0x0002DA95
		public static Message0 DataSourceLocation_Address_domain
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_domain");
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0002F8A1 File Offset: 0x0002DAA1
		public static Message0 DataSourceLocation_Address_object
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_object");
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0002F8AD File Offset: 0x0002DAAD
		public static Message0 DataSourceLocation_Address_path
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_path");
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0002F8B9 File Offset: 0x0002DAB9
		public static Message0 DataSourceLocation_Address_resource
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_resource");
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0002F8C5 File Offset: 0x0002DAC5
		public static Message0 DataSourceLocation_Address_schema
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_schema");
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0002F8D1 File Offset: 0x0002DAD1
		public static Message0 DataSourceLocation_Address_server
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_server");
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0002F8DD File Offset: 0x0002DADD
		public static Message0 DataSourceLocation_Address_url
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_url");
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0002F8E9 File Offset: 0x0002DAE9
		public static Message0 DataSourceLocation_Address_objectId
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_objectId");
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0002F8F5 File Offset: 0x0002DAF5
		public static Message0 DataSourceLocation_Address_objectType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_objectType");
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x0002F901 File Offset: 0x0002DB01
		public static Message0 DataSourceLocation_Address_account
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_account");
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0002F90D File Offset: 0x0002DB0D
		public static Message0 DataSourceLocation_Address_container
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_container");
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0002F919 File Offset: 0x0002DB19
		public static Message0 DataSourceLocation_Address_prefix
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_prefix");
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0002F925 File Offset: 0x0002DB25
		public static Message0 DataSourceLocation_Address_name
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_name");
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0002F931 File Offset: 0x0002DB31
		public static Message0 DataSourceLocation_Address_emailAddress
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_emailAddress");
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0002F93D File Offset: 0x0002DB3D
		public static Message0 DataSourceLocation_Address_options
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_options");
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0002F949 File Offset: 0x0002DB49
		public static Message0 DataSourceLocation_Address_model
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_model");
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0002F955 File Offset: 0x0002DB55
		public static Message0 DataSourceLocation_Address_loginServer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_loginServer");
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0002F961 File Offset: 0x0002DB61
		public static Message0 DataSourceLocation_Address_itemName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_itemName");
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0002F96D File Offset: 0x0002DB6D
		public static Message0 DataSourceLocation_Address_class
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_class");
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0002F979 File Offset: 0x0002DB79
		public static Message0 DataSourceLocation_Address_property
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_property");
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0002F985 File Offset: 0x0002DB85
		public static Message0 DataSourceLocation_Address_view
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_view");
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0002F991 File Offset: 0x0002DB91
		public static Message0 DataSourceLocation_Address_systemNumber
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_systemNumber");
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0002F99D File Offset: 0x0002DB9D
		public static Message0 DataSourceLocation_Address_clientId
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_clientId");
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0002F9A9 File Offset: 0x0002DBA9
		public static Message0 DataSourceLocation_Address_systemId
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_systemId");
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0002F9B5 File Offset: 0x0002DBB5
		public static Message0 DataSourceLocation_Address_logonGroup
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_logonGroup");
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0002F9C1 File Offset: 0x0002DBC1
		public static Message0 GenericCredentialError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GenericCredentialError");
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0002F9CD File Offset: 0x0002DBCD
		public static Message1 InvalidTraceEventType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidTraceEventType", p0);
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0002F9DA File Offset: 0x0002DBDA
		public static Message0 ExchangeDeserializationException
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ExchangeDeserializationException");
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0002F9E6 File Offset: 0x0002DBE6
		public static Message0 GoogleAnalyticsChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GoogleAnalyticsChallengeTitle");
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0002F9F2 File Offset: 0x0002DBF2
		public static Message0 GoogleAnalytics_Error_MeasureRequired
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("GoogleAnalytics_Error_MeasureRequired");
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0002F9FE File Offset: 0x0002DBFE
		public static Message1 AnalysisServicesCultureNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("AnalysisServicesCultureNotSupported", p0);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0002FA0B File Offset: 0x0002DC0B
		public static Message0 TableHasNoVisibleColumns
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableHasNoVisibleColumns");
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0002FA17 File Offset: 0x0002DC17
		public static Message2 OdbcInvalidSQLGetInfoValue(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("OdbcInvalidSQLGetInfoValue", p0, p1);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0002FA25 File Offset: 0x0002DC25
		public static Message1 ModuleNameDuplicate(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ModuleNameDuplicate", p0);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0002FA32 File Offset: 0x0002DC32
		public static Message1 ModuleNameNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ModuleNameNotFound", p0);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0002FA3F File Offset: 0x0002DC3F
		public static Message1 ModuleLoadFailed(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ModuleLoadFailed", p0);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		public static Message1 ModuleCannotBeReplaced(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ModuleCannotBeReplaced", p0);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0002FA59 File Offset: 0x0002DC59
		public static Message1 ModuleDisabled(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ModuleDisabled", p0);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0002FA66 File Offset: 0x0002DC66
		public static Message2 ModuleNameMismatch(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ModuleNameMismatch", p0, p1);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0002FA74 File Offset: 0x0002DC74
		public static Message2 ModuleMultipleSourceFiles(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ModuleMultipleSourceFiles", p0, p1);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0002FA82 File Offset: 0x0002DC82
		public static Message1 InvalidCsvParameter(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidCsvParameter", p0);
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0002FA8F File Offset: 0x0002DC8F
		public static Message0 InvalidCsvStyle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidCsvStyle");
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0002FA9B File Offset: 0x0002DC9B
		public static Message1 OdbcDataSourceInvalidSqlCapability(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OdbcDataSourceInvalidSqlCapability", p0);
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0002FAA8 File Offset: 0x0002DCA8
		public static Message0 OdbcDataSourceUnknownSqlCapability
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcDataSourceUnknownSqlCapability");
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0002FAB4 File Offset: 0x0002DCB4
		public static Message0 OdbcDataSourceUnknownNamedOption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcDataSourceUnknownNamedOption");
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
		public static Message2 Extensibility_UnknownFieldType(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Extensibility_UnknownFieldType", p0, p1);
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0002FACE File Offset: 0x0002DCCE
		public static Message0 RequestRedirectionMaximumReach
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RequestRedirectionMaximumReach");
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0002FADA File Offset: 0x0002DCDA
		public static Message0 RequestRedirectLocationMissing
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RequestRedirectLocationMissing");
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0002FAE6 File Offset: 0x0002DCE6
		public static Message0 FunctionCannotBeInvoked
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FunctionCannotBeInvoked");
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0002FAF2 File Offset: 0x0002DCF2
		public static Message0 MaxColumnNameLengthCantBeNegative
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("MaxColumnNameLengthCantBeNegative");
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0002FAFE File Offset: 0x0002DCFE
		public static Message0 FailedToCreateUniqueColumnName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FailedToCreateUniqueColumnName");
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0002FB0A File Offset: 0x0002DD0A
		public static Message0 DelayedTableTypeMismatch
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DelayedTableTypeMismatch");
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0002FB16 File Offset: 0x0002DD16
		public static Message0 OdbcInvalidValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcInvalidValue");
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0002FB22 File Offset: 0x0002DD22
		public static Message1 OdbcInvalidDecimalValue(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OdbcInvalidDecimalValue", p0);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0002FB2F File Offset: 0x0002DD2F
		public static Message1 UnsupportedClrType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedClrType", p0);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0002FB3C File Offset: 0x0002DD3C
		public static Message1 GoogleAnalyticsUnexpectedResponse(object p0)
		{
			return Strings.ResourceLoader.GetMessage("GoogleAnalyticsUnexpectedResponse", p0);
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0002FB49 File Offset: 0x0002DD49
		public static Message0 ValueNotUpdatable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueNotUpdatable");
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0002FB55 File Offset: 0x0002DD55
		public static Message0 Value_UpdateNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Value_UpdateNotSupported");
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0002FB61 File Offset: 0x0002DD61
		public static Message0 Table_UpdateNotSupportedWithoutKey
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Table_UpdateNotSupportedWithoutKey");
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0002FB6D File Offset: 0x0002DD6D
		public static Message1 Table_UpdateNotSupportedWrongColumnType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Table_UpdateNotSupportedWrongColumnType", p0);
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0002FB7A File Offset: 0x0002DD7A
		public static Message0 Action_NativeStatementsNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Action_NativeStatementsNotSupported");
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0002FB86 File Offset: 0x0002DD86
		public static Message0 Catalog_InsertKindMustBeTableOrView
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_InsertKindMustBeTableOrView");
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0002FB92 File Offset: 0x0002DD92
		public static Message0 Catalog_InsertKindMustBeTable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_InsertKindMustBeTable");
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0002FB9E File Offset: 0x0002DD9E
		public static Message0 Catalog_InsertNameMustMatchSchemaItem
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_InsertNameMustMatchSchemaItem");
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0002FBAA File Offset: 0x0002DDAA
		public static Message1 Catalog_UnsupportedColumnType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Catalog_UnsupportedColumnType", p0);
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0002FBB7 File Offset: 0x0002DDB7
		public static Message0 Catalog_OnlyInsertAndDeleteSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_OnlyInsertAndDeleteSupported");
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0002FBC3 File Offset: 0x0002DDC3
		public static Message0 Catalog_CreateSchemaNoDataAllowed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_CreateSchemaNoDataAllowed");
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0002FBCF File Offset: 0x0002DDCF
		public static Message0 Catalog_CreateSchemaNoCrossDatabaseFolding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_CreateSchemaNoCrossDatabaseFolding");
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0002FBDB File Offset: 0x0002DDDB
		public static Message0 Catalog_CreateViewNoCrossDatabaseFolding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_CreateViewNoCrossDatabaseFolding");
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0002FBE7 File Offset: 0x0002DDE7
		public static Message0 Catalog_DropSchemaNoCrossDatabaseFolding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_DropSchemaNoCrossDatabaseFolding");
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0002FBF3 File Offset: 0x0002DDF3
		public static Message0 Catalog_DropViewNoCrossDatabaseFolding
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_DropViewNoCrossDatabaseFolding");
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0002FBFF File Offset: 0x0002DDFF
		public static Message1 File_FileExists(object p0)
		{
			return Strings.ResourceLoader.GetMessage("File_FileExists", p0);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0002FC0C File Offset: 0x0002DE0C
		public static Message1 File_PathMustBeSubPath(object p0)
		{
			return Strings.ResourceLoader.GetMessage("File_PathMustBeSubPath", p0);
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0002FC19 File Offset: 0x0002DE19
		public static Message0 File_ExtensionMustMatchName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_ExtensionMustMatchName");
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0002FC25 File Offset: 0x0002DE25
		public static Message0 File_SetDatesNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_SetDatesNotSupported");
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0002FC31 File Offset: 0x0002DE31
		public static Message0 File_SetAttributesNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_SetAttributesNotSupported");
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0002FC3D File Offset: 0x0002DE3D
		public static Message0 File_SetAttributesNotSupported_All
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_SetAttributesNotSupported_All");
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0002FC49 File Offset: 0x0002DE49
		public static Message0 File_SetFolderPathNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_SetFolderPathNotSupported");
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0002FC55 File Offset: 0x0002DE55
		public static Message0 File_NoPathInName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_NoPathInName");
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0002FC61 File Offset: 0x0002DE61
		public static Message0 Storage_EmptyDirectoryOnly
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Storage_EmptyDirectoryOnly");
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0002FC6D File Offset: 0x0002DE6D
		public static Message0 Storage_NoDirectoryCreation
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Storage_NoDirectoryCreation");
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0002FC79 File Offset: 0x0002DE79
		public static Message0 Storage_NoAttributes
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Storage_NoAttributes");
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0002FC85 File Offset: 0x0002DE85
		public static Message0 Storage_NeedsFolderPath
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Storage_NeedsFolderPath");
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0002FC91 File Offset: 0x0002DE91
		public static Message0 TextFormat_InvalidArguments
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TextFormat_InvalidArguments");
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0002FC9D File Offset: 0x0002DE9D
		public static Message1 TextFormat_InvalidReference(object p0)
		{
			return Strings.ResourceLoader.GetMessage("TextFormat_InvalidReference", p0);
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0002FCAA File Offset: 0x0002DEAA
		public static Message0 TextFormat_OpenWithoutClose
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TextFormat_OpenWithoutClose");
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0002FCB6 File Offset: 0x0002DEB6
		public static Message0 TextFormat_NotUnsignedInteger
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TextFormat_NotUnsignedInteger");
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0002FCC2 File Offset: 0x0002DEC2
		public static Message0 OdbcErrorWithNoDiagnosticRecord
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcErrorWithNoDiagnosticRecord");
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0002FCCE File Offset: 0x0002DECE
		public static Message0 MalformedQueryString
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("MalformedQueryString");
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0002FCDA File Offset: 0x0002DEDA
		public static Message0 OleDbMsdaSqlNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbMsdaSqlNotSupported");
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0002FCE6 File Offset: 0x0002DEE6
		public static Message0 AnalysisServicesInvalidDatabaseName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServicesInvalidDatabaseName");
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0002FCF2 File Offset: 0x0002DEF2
		public static Message0 UnsupportedJsonType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnsupportedJsonType");
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0002FCFE File Offset: 0x0002DEFE
		public static Message0 Cube_ParameterMissing
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_ParameterMissing");
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0002FD0A File Offset: 0x0002DF0A
		public static Message0 Cube_InvalidParameterFunction
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cube_InvalidParameterFunction");
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0002FD16 File Offset: 0x0002DF16
		public static Message0 RowExpression_CantGetRowExpression
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RowExpression_CantGetRowExpression");
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0002FD22 File Offset: 0x0002DF22
		public static Message0 Expression_CantGetExpression
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Expression_CantGetExpression");
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0002FD2E File Offset: 0x0002DF2E
		public static Message0 SapHanaChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaChallengeTitle");
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0002FD3A File Offset: 0x0002DF3A
		public static Message1 SapHanaHDICubeError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapHanaHDICubeError", p0);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0002FD47 File Offset: 0x0002DF47
		public static Message1 SapHana_InvalidRangeOperator(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapHana_InvalidRangeOperator", p0);
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0002FD54 File Offset: 0x0002DF54
		public static Message0 SapHanaRangeOperator_GreaterThanCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_GreaterThanCaption");
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0002FD60 File Offset: 0x0002DF60
		public static Message0 SapHanaRangeOperator_LessThanCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_LessThanCaption");
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0002FD6C File Offset: 0x0002DF6C
		public static Message0 SapHanaRangeOperator_GreaterThanOrEqualsCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_GreaterThanOrEqualsCaption");
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0002FD78 File Offset: 0x0002DF78
		public static Message0 SapHanaRangeOperator_LessThanOrEqualsCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_LessThanOrEqualsCaption");
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0002FD84 File Offset: 0x0002DF84
		public static Message0 SapHanaRangeOperator_NotEqualsCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_NotEqualsCaption");
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0002FD90 File Offset: 0x0002DF90
		public static Message0 SapHanaRangeOperator_BetweenCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_BetweenCaption");
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0002FD9C File Offset: 0x0002DF9C
		public static Message0 SapHanaRangeOperator_EqualsCaption
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaRangeOperator_EqualsCaption");
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		public static Message1 SapHanaSharedColumnVariableErrorTitle(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapHanaSharedColumnVariableErrorTitle", p0);
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0002FDB5 File Offset: 0x0002DFB5
		public static Message0 SapHanaSharedColumnVariableErrorDetail
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaSharedColumnVariableErrorDetail");
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0002FDC1 File Offset: 0x0002DFC1
		public static Message4 SapHanaColumnBindingError(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("SapHanaColumnBindingError", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0002FDE3 File Offset: 0x0002DFE3
		public static Message3 SapHanaOdbcColumnBindingError(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("SapHanaOdbcColumnBindingError", p0, p1, p2);
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0002FDF2 File Offset: 0x0002DFF2
		public static Message0 SslCryptoProvider
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SslCryptoProvider");
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0002FDFE File Offset: 0x0002DFFE
		public static Message0 ValidateServerCertificate
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValidateServerCertificate");
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0002FE0A File Offset: 0x0002E00A
		public static Message0 SslKeyStore
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SslKeyStore");
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0002FE16 File Offset: 0x0002E016
		public static Message0 SslTrustStore
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SslTrustStore");
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0002FE22 File Offset: 0x0002E022
		public static Message0 SapHanaAuthInvalidSslCryptoProvider
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaAuthInvalidSslCryptoProvider");
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0002FE2E File Offset: 0x0002E02E
		public static Message0 SapHanaAuthInvalidSslValidateServerCertificate
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaAuthInvalidSslValidateServerCertificate");
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0002FE3A File Offset: 0x0002E03A
		public static Message0 SapHanaAuthInvalidSslKeyStore
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaAuthInvalidSslKeyStore");
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0002FE46 File Offset: 0x0002E046
		public static Message0 SapHanaAuthInvalidSslTrustStore
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaAuthInvalidSslTrustStore");
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0002FE52 File Offset: 0x0002E052
		public static Message0 R_DataTypeNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("R_DataTypeNotSupported");
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0002FE5E File Offset: 0x0002E05E
		public static Message0 R_BrokenTimeDiff
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("R_BrokenTimeDiff");
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0002FE6A File Offset: 0x0002E06A
		public static Message1 R_DuplicateColumnNames(object p0)
		{
			return Strings.ResourceLoader.GetMessage("R_DuplicateColumnNames", p0);
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0002FE77 File Offset: 0x0002E077
		public static Message0 TypeFacets_NumericPrecisionBaseNotSpecified
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TypeFacets_NumericPrecisionBaseNotSpecified");
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0002FE83 File Offset: 0x0002E083
		public static Message0 TypeFacets_MustHaveNonNegativeValues
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TypeFacets_MustHaveNonNegativeValues");
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0002FE8F File Offset: 0x0002E08F
		public static Message1 TypeFacets_UnknownFacet(object p0)
		{
			return Strings.ResourceLoader.GetMessage("TypeFacets_UnknownFacet", p0);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0002FE9C File Offset: 0x0002E09C
		public static Message2 Odbc_MissingDriver32bit(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Odbc_MissingDriver32bit", p0, p1);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0002FEAA File Offset: 0x0002E0AA
		public static Message2 Odbc_MissingDriver64bit(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Odbc_MissingDriver64bit", p0, p1);
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0002FEB8 File Offset: 0x0002E0B8
		public static Message0 SapHanaWindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapHanaWindowsAuth");
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0002FEC4 File Offset: 0x0002E0C4
		public static Message0 SapBwChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwChallengeTitle");
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0002FED0 File Offset: 0x0002E0D0
		public static Message1 SapBwUnsupportedCulture(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapBwUnsupportedCulture", p0);
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0002FEDD File Offset: 0x0002E0DD
		public static Message0 SapBwInvalidSystemNumber
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidSystemNumber");
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0002FEE9 File Offset: 0x0002E0E9
		public static Message0 SapBwInvalidClientId
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidClientId");
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0002FEF5 File Offset: 0x0002E0F5
		public static Message0 SapBwInvalidServerName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidServerName");
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0002FF01 File Offset: 0x0002E101
		public static Message0 SapBwInvalidRouterString
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidRouterString");
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0002FF0D File Offset: 0x0002E10D
		public static Message0 SapBwPasswordInRouterString
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwPasswordInRouterString");
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0002FF19 File Offset: 0x0002E119
		public static Message0 SapBwRouterStringMustNotContainMessageServer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwRouterStringMustNotContainMessageServer");
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0002FF25 File Offset: 0x0002E125
		public static Message0 SapBwRouterStringMustContainMessageServer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwRouterStringMustContainMessageServer");
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0002FF31 File Offset: 0x0002E131
		public static Message2 SapBwRouterStringGroupMustMatch(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("SapBwRouterStringGroupMustMatch", p0, p1);
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0002FF3F File Offset: 0x0002E13F
		public static Message0 SapBwRouterInvalidMessageServerStation
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwRouterInvalidMessageServerStation");
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0002FF4B File Offset: 0x0002E14B
		public static Message0 SapBwInvalidSystemId
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidSystemId");
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0002FF57 File Offset: 0x0002E157
		public static Message0 SapBwInvalidLogonGroup
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidLogonGroup");
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0002FF63 File Offset: 0x0002E163
		public static Message0 SapBwInvalidQuery
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwInvalidQuery");
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0002FF6F File Offset: 0x0002E16F
		public static Message0 SapBwWindowsAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwWindowsAuth");
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0002FF7B File Offset: 0x0002E17B
		public static Message0 SapBwUnsupportedQuerySameDimensionMultipleHierarchies
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwUnsupportedQuerySameDimensionMultipleHierarchies");
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0002FF87 File Offset: 0x0002E187
		public static Message0 SapBwWindowsAuthInvalidSncPartnerName
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwWindowsAuthInvalidSncPartnerName");
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0002FF93 File Offset: 0x0002E193
		public static Message0 SapBwWindowsAuthInvalidSncLibrary
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwWindowsAuthInvalidSncLibrary");
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0002FF9F File Offset: 0x0002E19F
		public static Message1 SapBwWindowsAuthInvalidSncLibraryEnvironment(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapBwWindowsAuthInvalidSncLibraryEnvironment", p0);
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0002FFAC File Offset: 0x0002E1AC
		public static Message0 SapBwSapClientDeprecated
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwSapClientDeprecated");
			}
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		public static Message2 SapBwBapiExecutionError(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("SapBwBapiExecutionError", p0, p1);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0002FFC6 File Offset: 0x0002E1C6
		public static Message3 SapBwBapiExecutionErrorAdditionalInfo(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("SapBwBapiExecutionErrorAdditionalInfo", p0, p1, p2);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0002FFD5 File Offset: 0x0002E1D5
		public static Message4 SapBwDuplicateMeasure(object p0, object p1, object p2, object p3)
		{
			return Strings.ResourceLoader.GetMessage("SapBwDuplicateMeasure", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0002FFF7 File Offset: 0x0002E1F7
		public static Message2 SapBwAssemblyNotInGac(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("SapBwAssemblyNotInGac", p0, p1);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00030005 File Offset: 0x0002E205
		public static Message1 DataSourceChanged(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DataSourceChanged", p0);
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00030012 File Offset: 0x0002E212
		public static Message0 SapBwParameterValueNotValid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SapBwParameterValueNotValid");
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0003001E File Offset: 0x0002E21E
		public static Message3 OptionNotSupportedInImplementation(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("OptionNotSupportedInImplementation", p0, p1, p2);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0003002D File Offset: 0x0002E22D
		public static Message2 OptionOnlySupportedInImplementation(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("OptionOnlySupportedInImplementation", p0, p1);
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0003003B File Offset: 0x0002E23B
		public static Message0 OleDbCellBadAccessor
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellBadAccessor");
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00030047 File Offset: 0x0002E247
		public static Message0 OleDbCellCantConvertValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellCantConvertValue");
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00030053 File Offset: 0x0002E253
		public static Message0 OleDbCellCantCreate
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellCantCreate");
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0003005F File Offset: 0x0002E25F
		public static Message0 OleDbCellDataOverflow
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellDataOverflow");
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0003006B File Offset: 0x0002E26B
		public static Message0 OleDbCellSignMismatch
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellSignMismatch");
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00030077 File Offset: 0x0002E277
		public static Message0 OleDbCellTruncated
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellTruncated");
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00030083 File Offset: 0x0002E283
		public static Message0 OleDbCellUnavailable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellUnavailable");
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0003008F File Offset: 0x0002E28F
		public static Message0 OleDbCellUnknownStatus
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbCellUnknownStatus");
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0003009B File Offset: 0x0002E29B
		public static Message0 ActionNotPermitted
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ActionNotPermitted");
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000300A7 File Offset: 0x0002E2A7
		public static Message0 Delta_SinceNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Delta_SinceNotSupported");
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000300B3 File Offset: 0x0002E2B3
		public static Message1 Certificate_NoSingleThumbprint(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Certificate_NoSingleThumbprint", p0);
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000300C0 File Offset: 0x0002E2C0
		public static Message0 Certificate_NoPrivateKey
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Certificate_NoPrivateKey");
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000300CC File Offset: 0x0002E2CC
		public static Message0 Certificate_VerificationFailed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Certificate_VerificationFailed");
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000300D8 File Offset: 0x0002E2D8
		public static Message0 OAuth1_UnsupportedType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OAuth1_UnsupportedType");
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000300E4 File Offset: 0x0002E2E4
		public static Message0 OAuth_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OAuth_NotSupported");
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000300F0 File Offset: 0x0002E2F0
		public static Message0 OAuth_RequiresClientApplication
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OAuth_RequiresClientApplication");
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000300FC File Offset: 0x0002E2FC
		public static Message0 OAuth_InvalidAadSettings
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OAuth_InvalidAadSettings");
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00030108 File Offset: 0x0002E308
		public static Message0 NativeQuery_NoParameters
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NativeQuery_NoParameters");
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x00030114 File Offset: 0x0002E314
		public static Message0 NativeStatement_NoParameters
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("NativeStatement_NoParameters");
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00030120 File Offset: 0x0002E320
		public static Message1 SqlExpression_ExpressionNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SqlExpression_ExpressionNotSupported", p0);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0003012D File Offset: 0x0002E32D
		public static Message2 ConflictingProperties(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ConflictingProperties", p0, p1);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0003013B File Offset: 0x0002E33B
		public static Message1 SapBw_UnsupportedCulture(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapBw_UnsupportedCulture", p0);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00030148 File Offset: 0x0002E348
		public static Message1 AdoDotNetInvalidParameterType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("AdoDotNetInvalidParameterType", p0);
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00030155 File Offset: 0x0002E355
		public static Message0 AdoDotNetParametersMustBeRecordOrList
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdoDotNetParametersMustBeRecordOrList");
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00030161 File Offset: 0x0002E361
		public static Message0 AdoDotNetNoParameterValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdoDotNetNoParameterValue");
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0003016D File Offset: 0x0002E36D
		public static Message1 AdoDotNetParameterDirectionNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("AdoDotNetParameterDirectionNotSupported", p0);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0003017A File Offset: 0x0002E37A
		public static Message1 OdbcInvalidParameterType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OdbcInvalidParameterType", p0);
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00030187 File Offset: 0x0002E387
		public static Message0 OdbcParametersMustBeList
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcParametersMustBeList");
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00030193 File Offset: 0x0002E393
		public static Message0 OdbcNoParameterValue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcNoParameterValue");
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0003019F File Offset: 0x0002E39F
		public static Message0 Join_LocalEvaluationWithKeyEqualityComparersNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Join_LocalEvaluationWithKeyEqualityComparersNotSupported");
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000301AB File Offset: 0x0002E3AB
		public static Message1 InvalidBinaryCodePage(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidBinaryCodePage", p0);
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000301B8 File Offset: 0x0002E3B8
		public static Message0 ValueExpression_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueExpression_NotSupported");
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public static Message0 ValueException_ChannelCannotBeEmpty
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_ChannelCannotBeEmpty");
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x000301D0 File Offset: 0x0002E3D0
		public static Message0 ValueException_QueuemanagerCannotBeEmpty
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_QueuemanagerCannotBeEmpty");
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000301DC File Offset: 0x0002E3DC
		public static Message0 ValueException_QueueCannotBeEmpty
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_QueueCannotBeEmpty");
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x000301E8 File Offset: 0x0002E3E8
		public static Message0 Resource_Mq_Invalid
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Resource_Mq_Invalid");
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x000301F4 File Offset: 0x0002E3F4
		public static Message0 DataSourceLocation_Address_channel
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_channel");
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00030200 File Offset: 0x0002E400
		public static Message0 DataSourceLocation_Address_queue
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_queue");
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0003020C File Offset: 0x0002E40C
		public static Message0 DataSourceLocation_Address_queueManager
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataSourceLocation_Address_queueManager");
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00030218 File Offset: 0x0002E418
		public static Message2 MqUnsupportedQueryOption(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("MqUnsupportedQueryOption", p0, p1);
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00030226 File Offset: 0x0002E426
		public static Message0 Mq_NoMessagesToSend
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Mq_NoMessagesToSend");
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00030232 File Offset: 0x0002E432
		public static Message2 Mq_MessageInvalidValue(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Mq_MessageInvalidValue", p0, p1);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00030240 File Offset: 0x0002E440
		public static Message1 Mq_ColumnNotWritable(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Mq_ColumnNotWritable", p0);
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0003024D File Offset: 0x0002E44D
		public static Message0 Mq_MessageDataMissing
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Mq_MessageDataMissing");
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00030259 File Offset: 0x0002E459
		public static Message0 FoldableFunction_MustReturnTableType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FoldableFunction_MustReturnTableType");
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00030265 File Offset: 0x0002E465
		public static Message0 Odbc_InvalidSQLGetInfoOverride
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Odbc_InvalidSQLGetInfoOverride");
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00030271 File Offset: 0x0002E471
		public static Message0 Odbc_InvalidLimitClauseKind
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Odbc_InvalidLimitClauseKind");
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0003027D File Offset: 0x0002E47D
		public static Message0 Odbc_NoTableAvailable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Odbc_NoTableAvailable");
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00030289 File Offset: 0x0002E489
		public static Message0 Odbc_NoSupportTopAndLimitClauseKind
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Odbc_NoSupportTopAndLimitClauseKind");
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00030295 File Offset: 0x0002E495
		public static Message2 Extensibility_PropertyInvalidType(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Extensibility_PropertyInvalidType", p0, p1);
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x000302A3 File Offset: 0x0002E4A3
		public static Message0 OdbcImplicitTypeConversionDuplicate
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcImplicitTypeConversionDuplicate");
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x000302AF File Offset: 0x0002E4AF
		public static Message0 FoldingFailure
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FoldingFailure");
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x000302BB File Offset: 0x0002E4BB
		public static Message0 Sql_Aad_DotNetFourSix
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Sql_Aad_DotNetFourSix");
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x000302C7 File Offset: 0x0002E4C7
		public static Message0 AnalysisServices_AadIsPaasOnly
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AnalysisServices_AadIsPaasOnly");
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000302D3 File Offset: 0x0002E4D3
		public static Message0 DimensionContainsRollup
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DimensionContainsRollup");
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x000302DF File Offset: 0x0002E4DF
		public static Message0 OleDbOlapNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OleDbOlapNotSupported");
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000302EB File Offset: 0x0002E4EB
		public static Message0 WebPageUnsupportedAuth
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("WebPageUnsupportedAuth");
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000302F7 File Offset: 0x0002E4F7
		public static Message0 MultiSubnetFailover_DotNetFourFive
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("MultiSubnetFailover_DotNetFourFive");
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00030303 File Offset: 0x0002E503
		public static Message1 SqlContextInfoValueTooLong(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SqlContextInfoValueTooLong", p0);
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00030310 File Offset: 0x0002E510
		public static Message0 AdobeAnalyticsChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeAnalyticsChallengeTitle");
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0003031C File Offset: 0x0002E51C
		public static Message0 AdobeReportNotReady
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeReportNotReady");
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00030328 File Offset: 0x0002E528
		public static Message0 AdobeDefaultCompany
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeDefaultCompany");
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00030334 File Offset: 0x0002E534
		public static Message0 AdobeDateRangeParameterDescription
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeDateRangeParameterDescription");
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00030340 File Offset: 0x0002E540
		public static Message0 AdobeSegmentParameterDescription
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeSegmentParameterDescription");
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0003034C File Offset: 0x0002E54C
		public static Message0 AdobeTopParameterDescription
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeTopParameterDescription");
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00030358 File Offset: 0x0002E558
		public static Message0 AdobeTopDimensionParameterAllDimensions
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeTopDimensionParameterAllDimensions");
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00030364 File Offset: 0x0002E564
		public static Message2 UDTNotSupport(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("UDTNotSupport", p0, p1);
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00030372 File Offset: 0x0002E572
		public static Message0 InvalidDelimiterIndexParameter
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidDelimiterIndexParameter");
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0003037E File Offset: 0x0002E57E
		public static Message0 RelativePositionTypeDescription
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("RelativePositionTypeDescription");
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0003038A File Offset: 0x0002E58A
		public static Message2 DuplicateResourceMapping(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("DuplicateResourceMapping", p0, p1);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00030398 File Offset: 0x0002E598
		public static Message1 DuplicateProtocol(object p0)
		{
			return Strings.ResourceLoader.GetMessage("DuplicateProtocol", p0);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000303A5 File Offset: 0x0002E5A5
		public static Message2 UnexpectedProtocol(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("UnexpectedProtocol", p0, p1);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000303B3 File Offset: 0x0002E5B3
		public static Message1 UnableToInferResourcePathInfo(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnableToInferResourcePathInfo", p0);
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x000303C0 File Offset: 0x0002E5C0
		public static Message0 AadMustHaveResource
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AadMustHaveResource");
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x000303CC File Offset: 0x0002E5CC
		public static Message0 AadAuthorizationUriMustEndInAuthorize
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AadAuthorizationUriMustEndInAuthorize");
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000303D8 File Offset: 0x0002E5D8
		public static Message1 OleDbMultipleMetadataValuesError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OleDbMultipleMetadataValuesError", p0);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000303E5 File Offset: 0x0002E5E5
		public static Message1 OpenApiAuthNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiAuthNotSupported", p0);
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x000303F2 File Offset: 0x0002E5F2
		public static Message0 OpenApiSecurityDefinitionNotDefined
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiSecurityDefinitionNotDefined");
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x000303FE File Offset: 0x0002E5FE
		public static Message0 OpenApiHostNotDefined
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiHostNotDefined");
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0003040A File Offset: 0x0002E60A
		public static Message0 OpenApiArrayTypeMustHaveItems
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiArrayTypeMustHaveItems");
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00030416 File Offset: 0x0002E616
		public static Message1 OpenApiReferenceDefinitionNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiReferenceDefinitionNotFound", p0);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00030423 File Offset: 0x0002E623
		public static Message1 OpenApiReferenceNotTextType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiReferenceNotTextType", p0);
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00030430 File Offset: 0x0002E630
		public static Message0 OpenApiContentTypeNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiContentTypeNotSupported");
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0003043C File Offset: 0x0002E63C
		public static Message0 OpenApiApiKeyNotSupportedInHeader
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiApiKeyNotSupportedInHeader");
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00030448 File Offset: 0x0002E648
		public static Message1 OpenApiSecurityDefinitionNotFound(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiSecurityDefinitionNotFound", p0);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00030455 File Offset: 0x0002E655
		public static Message1 OpenApiApiAuthInFieldMustBeQueryOrHeader(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiApiAuthInFieldMustBeQueryOrHeader", p0);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00030462 File Offset: 0x0002E662
		public static Message1 OpenApiCollectionFormatNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiCollectionFormatNotSupported", p0);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0003046F File Offset: 0x0002E66F
		public static Message1 OpenApiParameterTypeNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiParameterTypeNotSupported", p0);
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0003047C File Offset: 0x0002E67C
		public static Message0 OpenApiObjectTypeOnlyPermittedInBodyParameterItem
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiObjectTypeOnlyPermittedInBodyParameterItem");
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00030488 File Offset: 0x0002E688
		public static Message1 OpenApiUnsupportedParameterPropertyIn(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiUnsupportedParameterPropertyIn", p0);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00030495 File Offset: 0x0002E695
		public static Message1 OpenApiDuplicateOperationIds(object p0)
		{
			return Strings.ResourceLoader.GetMessage("OpenApiDuplicateOperationIds", p0);
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x000304A2 File Offset: 0x0002E6A2
		public static Message0 OpenApiCyclicallyDefinedTypeCantBeCreated
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OpenApiCyclicallyDefinedTypeCantBeCreated");
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000304AE File Offset: 0x0002E6AE
		public static Message1 ExpectingMetadataToContainTypeInfoFor(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ExpectingMetadataToContainTypeInfoFor", p0);
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x000304BB File Offset: 0x0002E6BB
		public static Message0 Extensibility_TestConnectionFailed
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Extensibility_TestConnectionFailed");
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x000304C7 File Offset: 0x0002E6C7
		public static Message0 OdbcMismatchArchitectureInX86App
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcMismatchArchitectureInX86App");
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x000304D3 File Offset: 0x0002E6D3
		public static Message0 OdbcMismatchArchitectureInX64App
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("OdbcMismatchArchitectureInX64App");
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x000304DF File Offset: 0x0002E6DF
		public static Message0 CannotImportDataFromAPasswordProtectedWorkbook
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CannotImportDataFromAPasswordProtectedWorkbook");
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000304EB File Offset: 0x0002E6EB
		public static Message0 BuiltinIntrospectionNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("BuiltinIntrospectionNotSupported");
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x000304F7 File Offset: 0x0002E6F7
		public static Message0 IntrospectionNotAvailable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("IntrospectionNotAvailable");
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00030503 File Offset: 0x0002E703
		public static Message0 CacheManagerNotAvailable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CacheManagerNotAvailable");
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0003050F File Offset: 0x0002E70F
		public static Message0 CacheNotReplaceable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CacheNotReplaceable");
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0003051B File Offset: 0x0002E71B
		public static Message0 ODataNavigationPropertyWithoutUrl
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ODataNavigationPropertyWithoutUrl");
			}
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00030527 File Offset: 0x0002E727
		public static Message1 SapHanaUnsupportedParameterValueKind(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SapHanaUnsupportedParameterValueKind", p0);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00030534 File Offset: 0x0002E734
		public static Message1 UnsupportedODataImplementation(object p0)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedODataImplementation", p0);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00030541 File Offset: 0x0002E741
		public static Message2 ODataImplementationUnsupportedOption(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataImplementationUnsupportedOption", p0, p1);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0003054F File Offset: 0x0002E74F
		public static Message1 AuthenticationPropertyTypeNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("AuthenticationPropertyTypeNotSupported", p0);
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0003055C File Offset: 0x0002E75C
		public static Message0 InvalidPermission
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidPermission");
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00030568 File Offset: 0x0002E768
		public static Message3 ConfigurationPropertyTypeError(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("ConfigurationPropertyTypeError", p0, p1, p2);
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00030577 File Offset: 0x0002E777
		public static Message0 SncLibraryLabel
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SncLibraryLabel");
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x00030583 File Offset: 0x0002E783
		public static Message0 SncPartnerNameLabel
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SncPartnerNameLabel");
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0003058F File Offset: 0x0002E78F
		public static Message1 ODataMissingProperty(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataMissingProperty", p0);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0003059C File Offset: 0x0002E79C
		public static Message1 ODataNonTerminatingContextUrl(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataNonTerminatingContextUrl", p0);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000305A9 File Offset: 0x0002E7A9
		public static Message1 ODataNavigationPropertyWithoutUrlWithMessage(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataNavigationPropertyWithoutUrlWithMessage", p0);
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x000305B6 File Offset: 0x0002E7B6
		public static Message0 File_UpdateNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_UpdateNotSupported");
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x000305C2 File Offset: 0x0002E7C2
		public static Message0 ListParallelInvoke_ConcurrencyMustBePositive
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ListParallelInvoke_ConcurrencyMustBePositive");
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000305CE File Offset: 0x0002E7CE
		public static Message2 ODataMetadataDuplicatePropertyNames(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataMetadataDuplicatePropertyNames", p0, p1);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000305DC File Offset: 0x0002E7DC
		public static Message1 ODataMetadataDuplicatePropertyNamesUnknownTypeName(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataMetadataDuplicatePropertyNamesUnknownTypeName", p0);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000305E9 File Offset: 0x0002E7E9
		public static Message1 FuzzyUtilInvalidTransformationTableColumnType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FuzzyUtilInvalidTransformationTableColumnType", p0);
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000305F6 File Offset: 0x0002E7F6
		public static Message0 FuzzyUtilInvalidColumnKeysCount
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FuzzyUtilInvalidColumnKeysCount");
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00030602 File Offset: 0x0002E802
		public static Message1 FuzzyUtilInvalidColumnKeyType(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FuzzyUtilInvalidColumnKeyType", p0);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0003060F File Offset: 0x0002E80F
		public static Message2 FuzzyUtilInvalidConcurrentRequestCount(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("FuzzyUtilInvalidConcurrentRequestCount", p0, p1);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0003061D File Offset: 0x0002E81D
		public static Message1 FuzzyUtilInvalidFuzzyOptionArgument(object p0)
		{
			return Strings.ResourceLoader.GetMessage("FuzzyUtilInvalidFuzzyOptionArgument", p0);
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0003062A File Offset: 0x0002E82A
		public static Message0 UnsupportedJoinKindForFuzzyJoins
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnsupportedJoinKindForFuzzyJoins");
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00030636 File Offset: 0x0002E836
		public static Message0 KeyEqualityComparersNotSupportedForFuzzyJoins
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("KeyEqualityComparersNotSupportedForFuzzyJoins");
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00030642 File Offset: 0x0002E842
		public static Message0 UnsupportedConcurrentRequestsForLeftOuterJoin
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("UnsupportedConcurrentRequestsForLeftOuterJoin");
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0003064E File Offset: 0x0002E84E
		public static Message3 HDInsightFailedJsonException(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("HDInsightFailedJsonException", p0, p1, p2);
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0003065D File Offset: 0x0002E85D
		public static Message0 FunctionScalarVectorRowCountMismatch
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FunctionScalarVectorRowCountMismatch");
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00030669 File Offset: 0x0002E869
		public static Message0 FunctionScalarVectorMultipleEnumeration
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FunctionScalarVectorMultipleEnumeration");
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00030675 File Offset: 0x0002E875
		public static Message0 FunctionScalarVectorVectorFunctionSingleInput
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FunctionScalarVectorVectorFunctionSingleInput");
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00030681 File Offset: 0x0002E881
		public static Message0 CallbacksMustBeOnMainThread
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("CallbacksMustBeOnMainThread");
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0003068D File Offset: 0x0002E88D
		public static Message0 EssbaseChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("EssbaseChallengeTitle");
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00030699 File Offset: 0x0002E899
		public static Message0 EssbaseInvalidServer
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("EssbaseInvalidServer");
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000306A5 File Offset: 0x0002E8A5
		public static Message0 DurationToTextFormatUnsupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DurationToTextFormatUnsupported");
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000306B1 File Offset: 0x0002E8B1
		public static Message1 ODataConstantNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("ODataConstantNotSupported", p0);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000306BE File Offset: 0x0002E8BE
		public static Message2 ODataOptionOnlySupportedInVersion(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ODataOptionOnlySupportedInVersion", p0, p1);
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000306CC File Offset: 0x0002E8CC
		public static Message0 Identity_NotSupportedAtRuntime
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Identity_NotSupportedAtRuntime");
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x000306D8 File Offset: 0x0002E8D8
		public static Message0 IdentityProvider_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("IdentityProvider_NotSupported");
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x000306E4 File Offset: 0x0002E8E4
		public static Message0 AccessControlEntry_ConditionConversionToIdentitiesNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AccessControlEntry_ConditionConversionToIdentitiesNotSupported");
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000306F0 File Offset: 0x0002E8F0
		public static Message1 File_CannotRetrieveAccessControlEntryTable(object p0)
		{
			return Strings.ResourceLoader.GetMessage("File_CannotRetrieveAccessControlEntryTable", p0);
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x000306FD File Offset: 0x0002E8FD
		public static Message0 File_CannotUnderstandAccessControlRule
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_CannotUnderstandAccessControlRule");
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00030709 File Offset: 0x0002E909
		public static Message0 Odbc_InvalidConnectionAttributes
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Odbc_InvalidConnectionAttributes");
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00030715 File Offset: 0x0002E915
		public static Message0 Odbc_SetQueryNoPooling
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Odbc_SetQueryNoPooling");
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00030721 File Offset: 0x0002E921
		public static Message1 Cdpa_UriNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Cdpa_UriNotSupported", p0);
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0003072E File Offset: 0x0002E92E
		public static Message0 Cdpa_CantLoadMetadata
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cdpa_CantLoadMetadata");
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0003073A File Offset: 0x0002E93A
		public static Message0 Cdpa_CantCreateCube
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cdpa_CantCreateCube");
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00030746 File Offset: 0x0002E946
		public static Message0 Cdpa_TruncatedResult
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cdpa_TruncatedResult");
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00030752 File Offset: 0x0002E952
		public static Message1 Cdpa_GranularityNotSupported(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Cdpa_GranularityNotSupported", p0);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0003075F File Offset: 0x0002E95F
		public static Message2 Cdpa_RequestError(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Cdpa_RequestError", p0, p1);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0003076D File Offset: 0x0002E96D
		public static Message1 Cdpa_ResponseError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Cdpa_ResponseError", p0);
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0003077A File Offset: 0x0002E97A
		public static Message0 Cdpa_RequestId_None
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cdpa_RequestId_None");
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00030786 File Offset: 0x0002E986
		public static Message0 Cdpa_ChallengeTitle
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cdpa_ChallengeTitle");
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00030792 File Offset: 0x0002E992
		public static Message2 GeographyFunctionArgumentError(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("GeographyFunctionArgumentError", p0, p1);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000307A0 File Offset: 0x0002E9A0
		public static Message1 SpatialArgumentInvalidWKT(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SpatialArgumentInvalidWKT", p0);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000307AD File Offset: 0x0002E9AD
		public static Message1 SpatialInconsistenSRID(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SpatialInconsistenSRID", p0);
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x000307BA File Offset: 0x0002E9BA
		public static Message0 SpatialInvalidRecordFormat
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SpatialInvalidRecordFormat");
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000307C6 File Offset: 0x0002E9C6
		public static Message1 SpatialInvalidToken(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SpatialInvalidToken", p0);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000307D3 File Offset: 0x0002E9D3
		public static Message1 Unsupported_CodedCharSetId(object p0)
		{
			return Strings.ResourceLoader.GetMessage("Unsupported_CodedCharSetId", p0);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000307E0 File Offset: 0x0002E9E0
		public static Message2 SharePointList_Impl2_InvalidOptionsKey(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("SharePointList_Impl2_InvalidOptionsKey", p0, p1);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000307EE File Offset: 0x0002E9EE
		public static Message3 SharePointList_Impl2_InvalidOptionsValue(object p0, object p1, object p2)
		{
			return Strings.ResourceLoader.GetMessage("SharePointList_Impl2_InvalidOptionsValue", p0, p1, p2);
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x000307FD File Offset: 0x0002E9FD
		public static Message0 SharePointList_Impl2_ValidViewMode
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointList_Impl2_ValidViewMode");
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00030809 File Offset: 0x0002EA09
		public static Message2 UnsupportedSharePointListImplementation(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("UnsupportedSharePointListImplementation", p0, p1);
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00030817 File Offset: 0x0002EA17
		public static Message0 ValidSharePointListImplementation
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValidSharePointListImplementation");
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00030823 File Offset: 0x0002EA23
		public static Message0 ValueIsNotGraph
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueIsNotGraph");
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0003082F File Offset: 0x0002EA2F
		public static Message1 SP_InvalidDeltaTag(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SP_InvalidDeltaTag", p0);
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0003083C File Offset: 0x0002EA3C
		public static Message0 SharePointNotSupportedApi
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointNotSupportedApi");
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00030848 File Offset: 0x0002EA48
		public static Message1 File_SetAttributesNotSupported_But(object p0)
		{
			return Strings.ResourceLoader.GetMessage("File_SetAttributesNotSupported_But", p0);
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00030855 File Offset: 0x0002EA55
		public static Message0 File_SetAttributeContentTypeTooLarge
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("File_SetAttributeContentTypeTooLarge");
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00030861 File Offset: 0x0002EA61
		public static Message1 SP_DeltaTagDeserializationError(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SP_DeltaTagDeserializationError", p0);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0003086E File Offset: 0x0002EA6E
		public static Message1 SP_InvalidChangeToken(object p0)
		{
			return Strings.ResourceLoader.GetMessage("SP_InvalidChangeToken", p0);
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0003087B File Offset: 0x0002EA7B
		public static Message0 InvalidQueryParameter
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidQueryParameter");
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00030887 File Offset: 0x0002EA87
		public static Message1 InvalidExcelWorkbookParameter(object p0)
		{
			return Strings.ResourceLoader.GetMessage("InvalidExcelWorkbookParameter", p0);
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x00030894 File Offset: 0x0002EA94
		public static Message0 InvalidFileTypeForInferSheetDimensionsParameter
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("InvalidFileTypeForInferSheetDimensionsParameter");
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x000308A0 File Offset: 0x0002EAA0
		public static Message0 Percentile_ProbabilityOutOfRange
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Percentile_ProbabilityOutOfRange");
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x000308AC File Offset: 0x0002EAAC
		public static Message0 Percentile_MixedType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Percentile_MixedType");
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x000308B8 File Offset: 0x0002EAB8
		public static Message0 Percentile_UnsupportedType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Percentile_UnsupportedType");
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x000308C4 File Offset: 0x0002EAC4
		public static Message0 AdobeAnalyticsOrgCompaniesNotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeAnalyticsOrgCompaniesNotFound");
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000308D0 File Offset: 0x0002EAD0
		public static Message0 AdobeReportNotAvailable
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeReportNotAvailable");
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x000308DC File Offset: 0x0002EADC
		public static Message0 EncryptedWorkbook_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("EncryptedWorkbook_NotSupported");
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x000308E8 File Offset: 0x0002EAE8
		public static Message0 EncryptedWorkbook_AccessDenied
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("EncryptedWorkbook_AccessDenied");
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000308F4 File Offset: 0x0002EAF4
		public static Message0 EncryptedWorkbook_NoCredentials
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("EncryptedWorkbook_NoCredentials");
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00030900 File Offset: 0x0002EB00
		public static Message0 SingleInvocation_MultipleEvaluations
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SingleInvocation_MultipleEvaluations");
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0003090C File Offset: 0x0002EB0C
		public static Message0 TableAction_Tee_ErrorInAction
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableAction_Tee_ErrorInAction");
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00030918 File Offset: 0x0002EB18
		public static Message0 TableAction_Tee_InputTableMustHavePrimitiveValues
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("TableAction_Tee_InputTableMustHavePrimitiveValues");
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00030924 File Offset: 0x0002EB24
		public static Message0 AdobeCredentialDeprecated
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeCredentialDeprecated");
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x00030930 File Offset: 0x0002EB30
		public static Message0 AdobeAnalyticsServerError
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeAnalyticsServerError");
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0003093C File Offset: 0x0002EB3C
		public static Message0 MalformedFormat_Truncation
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("MalformedFormat_Truncation");
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00030948 File Offset: 0x0002EB48
		public static Message0 DataComponent_
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataComponent_");
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00030954 File Offset: 0x0002EB54
		public static Message0 DataComponent_Date
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataComponent_Date");
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00030960 File Offset: 0x0002EB60
		public static Message0 DataComponent_Time
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataComponent_Time");
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0003096C File Offset: 0x0002EB6C
		public static Message0 DataComponent_Zone
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DataComponent_Zone");
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00030978 File Offset: 0x0002EB78
		public static Message2 FormatValueDoesNotHaveComponent(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("FormatValueDoesNotHaveComponent", p0, p1);
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00030986 File Offset: 0x0002EB86
		public static Message0 FormatValueUnknownFailure
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FormatValueUnknownFailure");
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00030992 File Offset: 0x0002EB92
		public static Message0 ValueException_ApproximateLength_Unsupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_ApproximateLength_Unsupported");
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0003099E File Offset: 0x0002EB9E
		public static Message0 ValueException_ApproximateRowCount_Unsupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueException_ApproximateRowCount_Unsupported");
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x000309AA File Offset: 0x0002EBAA
		public static Message0 AdobeStartAndEndOnlySupportDateTimeWithV2
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeStartAndEndOnlySupportDateTimeWithV2");
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000309B6 File Offset: 0x0002EBB6
		public static Message0 AdobeStartOrEndMustBeDateOrDateTime
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeStartOrEndMustBeDateOrDateTime");
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x000309C2 File Offset: 0x0002EBC2
		public static Message0 AdobeStartAndEndMustBeOfSameType
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeStartAndEndMustBeOfSameType");
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000309CE File Offset: 0x0002EBCE
		public static Message2 ValueException_UnexpectedKind(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("ValueException_UnexpectedKind", p0, p1);
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000309DC File Offset: 0x0002EBDC
		public static Message0 EvaluationCanceled
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("EvaluationCanceled");
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x000309E8 File Offset: 0x0002EBE8
		public static Message0 AdobeAnalyticsGlobalCompanyIdIsNull
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("AdobeAnalyticsGlobalCompanyIdIsNull");
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000309F4 File Offset: 0x0002EBF4
		public static Message0 VolatileFunctionsNotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("VolatileFunctionsNotSupported");
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00030A00 File Offset: 0x0002EC00
		public static Message0 ValueAction_Transaction_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("ValueAction_Transaction_NotSupported");
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00030A0C File Offset: 0x0002EC0C
		public static Message0 Value_Versions_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Value_Versions_NotSupported");
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00030A18 File Offset: 0x0002EC18
		public static Message0 Value_Enumeration_NotSupported
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Value_Enumeration_NotSupported");
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00030A24 File Offset: 0x0002EC24
		public static Message0 Value_NotCache
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Value_NotCache");
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00030A30 File Offset: 0x0002EC30
		public static Message0 Cache_NotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Cache_NotFound");
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00030A3C File Offset: 0x0002EC3C
		public static Message0 Adls_FileNotFound
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Adls_FileNotFound");
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00030A48 File Offset: 0x0002EC48
		public static Message2 Salesforce_Warning(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("Salesforce_Warning", p0, p1);
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00030A56 File Offset: 0x0002EC56
		public static Message0 DuplicateContinuationToken
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("DuplicateContinuationToken");
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00030A62 File Offset: 0x0002EC62
		public static Message0 SharePointList_Impl2_DisableAppendNoteColumns
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("SharePointList_Impl2_DisableAppendNoteColumns");
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00030A6E File Offset: 0x0002EC6E
		public static Message0 FabricCapacityExceeded
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("FabricCapacityExceeded");
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00030A7A File Offset: 0x0002EC7A
		public static Message1 WebResponseMalformed(object p0)
		{
			return Strings.ResourceLoader.GetMessage("WebResponseMalformed", p0);
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00030A87 File Offset: 0x0002EC87
		public static Message0 Catalog_InsertKindMustHaveOneColumn
		{
			get
			{
				return Strings.ResourceLoader.GetMessage("Catalog_InsertKindMustHaveOneColumn");
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00030A93 File Offset: 0x0002EC93
		public static Message2 FileLoadingException(object p0, object p1)
		{
			return Strings.ResourceLoader.GetMessage("FileLoadingException", p0, p1);
		}

		// Token: 0x02000235 RID: 565
		private class ResourceLoader
		{
			// Token: 0x060018D6 RID: 6358 RVA: 0x00030AA1 File Offset: 0x0002ECA1
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Engine1.Strings", base.GetType().Assembly);
			}

			// Token: 0x060018D7 RID: 6359 RVA: 0x00030AC4 File Offset: 0x0002ECC4
			private static Strings.ResourceLoader GetLoader()
			{
				if (Strings.ResourceLoader.instance == null)
				{
					Strings.ResourceLoader resourceLoader = new Strings.ResourceLoader();
					Interlocked.CompareExchange<Strings.ResourceLoader>(ref Strings.ResourceLoader.instance, resourceLoader, null);
				}
				return Strings.ResourceLoader.instance;
			}

			// Token: 0x17000C9A RID: 3226
			// (get) Token: 0x060018D8 RID: 6360 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000C9B RID: 3227
			// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00030AF0 File Offset: 0x0002ECF0
			public static ResourceManager Resources
			{
				get
				{
					return Strings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x060018DA RID: 6362 RVA: 0x00030AFC File Offset: 0x0002ECFC
			public static Message0 GetMessage(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x060018DB RID: 6363 RVA: 0x00030B30 File Offset: 0x0002ED30
			public static Message1 GetMessage(string name, object arg0)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Strings.ResourceLoader.Culture), arg0);
			}

			// Token: 0x060018DC RID: 6364 RVA: 0x00030B68 File Offset: 0x0002ED68
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Strings.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x060018DD RID: 6365 RVA: 0x00030BA0 File Offset: 0x0002EDA0
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Strings.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x060018DE RID: 6366 RVA: 0x00030BDC File Offset: 0x0002EDDC
			public static Message4 GetMessage(string name, params object[] args)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Strings.ResourceLoader.Culture), args);
			}

			// Token: 0x060018DF RID: 6367 RVA: 0x00030C14 File Offset: 0x0002EE14
			public static string GetString(string name, params object[] args)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Strings.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x060018E0 RID: 6368 RVA: 0x00030C54 File Offset: 0x0002EE54
			public static string GetString(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x060018E1 RID: 6369 RVA: 0x00030C80 File Offset: 0x0002EE80
			public static object GetObject(string name)
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Strings.ResourceLoader.Culture);
			}

			// Token: 0x060018E2 RID: 6370 RVA: 0x00030CAC File Offset: 0x0002EEAC
			public static T GetObject<T>(string name) where T : class
			{
				Strings.ResourceLoader loader = Strings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Strings.ResourceLoader.Culture));
			}

			// Token: 0x04000698 RID: 1688
			private static Strings.ResourceLoader instance;

			// Token: 0x04000699 RID: 1689
			private ResourceManager resources;
		}
	}
}
