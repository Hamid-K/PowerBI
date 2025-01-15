using System;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x02000060 RID: 96
	internal static class Strings
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		internal static string ODataQueryUtils_DidNotFindServiceOperation(object p0)
		{
			return TextRes.GetString("ODataQueryUtils_DidNotFindServiceOperation", new object[] { p0 });
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		internal static string ODataQueryUtils_FoundMultipleServiceOperations(object p0)
		{
			return TextRes.GetString("ODataQueryUtils_FoundMultipleServiceOperations", new object[] { p0 });
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000CEE7 File Offset: 0x0000B0E7
		internal static string ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType
		{
			get
			{
				return TextRes.GetString("ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType");
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		internal static string ODataQueryUtils_DidNotFindEntitySet(object p0)
		{
			return TextRes.GetString("ODataQueryUtils_DidNotFindEntitySet", new object[] { p0 });
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000CF18 File Offset: 0x0000B118
		internal static string BinaryOperatorQueryNode_InvalidOperandType(object p0, object p1)
		{
			return TextRes.GetString("BinaryOperatorQueryNode_InvalidOperandType", new object[] { p0, p1 });
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000CF40 File Offset: 0x0000B140
		internal static string BinaryOperatorQueryNode_OperandsMustHaveSameTypes(object p0, object p1)
		{
			return TextRes.GetString("BinaryOperatorQueryNode_OperandsMustHaveSameTypes", new object[] { p0, p1 });
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000CF68 File Offset: 0x0000B168
		internal static string QueryExpressionTranslator_UnsupportedQueryNodeKind(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_UnsupportedQueryNodeKind", new object[] { p0 });
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000CF8B File Offset: 0x0000B18B
		internal static string QueryExpressionTranslator_UnsupportedExtensionNode
		{
			get
			{
				return TextRes.GetString("QueryExpressionTranslator_UnsupportedExtensionNode");
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000CF98 File Offset: 0x0000B198
		internal static string QueryExpressionTranslator_NodeTranslatedToNull(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_NodeTranslatedToNull", new object[] { p0 });
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		internal static string QueryExpressionTranslator_NodeTranslatedToWrongType(object p0, object p1, object p2)
		{
			return TextRes.GetString("QueryExpressionTranslator_NodeTranslatedToWrongType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		internal static string QueryExpressionTranslator_CollectionQueryNodeWithoutItemType(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_CollectionQueryNodeWithoutItemType", new object[] { p0 });
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000D00C File Offset: 0x0000B20C
		internal static string QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference", new object[] { p0 });
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000D030 File Offset: 0x0000B230
		internal static string QueryExpressionTranslator_ConstantNonPrimitive(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_ConstantNonPrimitive", new object[] { p0 });
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000D054 File Offset: 0x0000B254
		internal static string QueryExpressionTranslator_KeyLookupOnlyOnEntities(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_KeyLookupOnlyOnEntities", new object[] { p0, p1 });
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000D07C File Offset: 0x0000B27C
		internal static string QueryExpressionTranslator_KeyLookupOnlyOnQueryable(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_KeyLookupOnlyOnQueryable", new object[] { p0, p1 });
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		internal static string QueryExpressionTranslator_KeyLookupWithoutKeyProperty(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_KeyLookupWithoutKeyProperty", new object[] { p0, p1 });
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000D0CB File Offset: 0x0000B2CB
		internal static string QueryExpressionTranslator_KeyLookupWithNoKeyValues
		{
			get
			{
				return TextRes.GetString("QueryExpressionTranslator_KeyLookupWithNoKeyValues");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
		internal static string QueryExpressionTranslator_KeyPropertyValueWithoutProperty
		{
			get
			{
				return TextRes.GetString("QueryExpressionTranslator_KeyPropertyValueWithoutProperty");
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		internal static string QueryExpressionTranslator_KeyPropertyValueWithWrongValue(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_KeyPropertyValueWithWrongValue", new object[] { p0 });
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D108 File Offset: 0x0000B308
		internal static string QueryExpressionTranslator_FilterCollectionOfWrongType(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_FilterCollectionOfWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000D130 File Offset: 0x0000B330
		internal static string QueryExpressionTranslator_FilterExpressionOfWrongType(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_FilterExpressionOfWrongType", new object[] { p0 });
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000D154 File Offset: 0x0000B354
		internal static string QueryExpressionTranslator_UnaryNotOperandNotBoolean(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_UnaryNotOperandNotBoolean", new object[] { p0 });
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000D178 File Offset: 0x0000B378
		internal static string QueryExpressionTranslator_PropertyAccessSourceWrongType(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_PropertyAccessSourceWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		internal static string QueryExpressionTranslator_PropertyAccessSourceNotStructured(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_PropertyAccessSourceNotStructured", new object[] { p0 });
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000D1C3 File Offset: 0x0000B3C3
		internal static string QueryExpressionTranslator_ParameterNotDefinedInScope
		{
			get
			{
				return TextRes.GetString("QueryExpressionTranslator_ParameterNotDefinedInScope");
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		internal static string QueryExpressionTranslator_OrderByCollectionOfWrongType(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_OrderByCollectionOfWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		internal static string QueryExpressionTranslator_UnknownFunction(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D21C File Offset: 0x0000B41C
		internal static string QueryExpressionTranslator_FunctionArgumentNotSingleValue(object p0)
		{
			return TextRes.GetString("QueryExpressionTranslator_FunctionArgumentNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000D240 File Offset: 0x0000B440
		internal static string QueryExpressionTranslator_NoApplicableFunctionFound(object p0, object p1)
		{
			return TextRes.GetString("QueryExpressionTranslator_NoApplicableFunctionFound", new object[] { p0, p1 });
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D268 File Offset: 0x0000B468
		internal static string QueryDescriptorQueryToken_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("QueryDescriptorQueryToken_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000D28B File Offset: 0x0000B48B
		internal static string QueryDescriptorQueryToken_MaxDepthInvalid
		{
			get
			{
				return TextRes.GetString("QueryDescriptorQueryToken_MaxDepthInvalid");
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D298 File Offset: 0x0000B498
		internal static string QueryDescriptorQueryToken_InvalidSkipQueryOptionValue(object p0)
		{
			return TextRes.GetString("QueryDescriptorQueryToken_InvalidSkipQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		internal static string QueryDescriptorQueryToken_InvalidTopQueryOptionValue(object p0)
		{
			return TextRes.GetString("QueryDescriptorQueryToken_InvalidTopQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		internal static string QueryDescriptorQueryToken_InvalidInlineCountQueryOptionValue(object p0, object p1)
		{
			return TextRes.GetString("QueryDescriptorQueryToken_InvalidInlineCountQueryOptionValue", new object[] { p0, p1 });
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000D308 File Offset: 0x0000B508
		internal static string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(object p0)
		{
			return TextRes.GetString("QueryOptionUtils_QueryParameterMustBeSpecifiedOnce", new object[] { p0 });
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000D32C File Offset: 0x0000B52C
		internal static string UriBuilder_NotSupportedClrLiteral(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedClrLiteral", new object[] { p0 });
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000D350 File Offset: 0x0000B550
		internal static string UriBuilder_NotSupportedQueryToken(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedQueryToken", new object[] { p0 });
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000D373 File Offset: 0x0000B573
		internal static string UriQueryExpressionParser_TooDeep
		{
			get
			{
				return TextRes.GetString("UriQueryExpressionParser_TooDeep");
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000D380 File Offset: 0x0000B580
		internal static string UriQueryExpressionParser_ExpressionExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_ExpressionExpected", new object[] { p0, p1 });
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		internal static string UriQueryExpressionParser_OpenParenExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_OpenParenExpected", new object[] { p0, p1 });
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		internal static string UriQueryExpressionParser_CloseParenOrCommaExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrCommaExpected", new object[] { p0, p1 });
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		internal static string UriQueryExpressionParser_CloseParenOrOperatorExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrOperatorExpected", new object[] { p0, p1 });
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D420 File Offset: 0x0000B620
		internal static string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(object p0, object p1)
		{
			return TextRes.GetString("UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri", new object[] { p0, p1 });
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000D447 File Offset: 0x0000B647
		internal static string UriQueryPathParser_SyntaxError
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_SyntaxError");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000D453 File Offset: 0x0000B653
		internal static string UriQueryPathParser_TooManySegments
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_TooManySegments");
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000D460 File Offset: 0x0000B660
		internal static string UriQueryPathParser_InvalidKeyValueLiteral(object p0)
		{
			return TextRes.GetString("UriQueryPathParser_InvalidKeyValueLiteral", new object[] { p0 });
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000D484 File Offset: 0x0000B684
		internal static string PropertyInfoTypeAnnotation_CannotFindProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("PropertyInfoTypeAnnotation_CannotFindProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		internal static string MetadataBinder_UnsupportedQueryTokenKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedQueryTokenKind", new object[] { p0 });
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000D4D3 File Offset: 0x0000B6D3
		internal static string MetadataBinder_UnsupportedExtensionToken
		{
			get
			{
				return TextRes.GetString("MetadataBinder_UnsupportedExtensionToken");
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		internal static string MetadataBinder_RootSegmentResourceNotFound(object p0)
		{
			return TextRes.GetString("MetadataBinder_RootSegmentResourceNotFound", new object[] { p0 });
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D504 File Offset: 0x0000B704
		internal static string MetadataBinder_KeyValueApplicableOnlyToEntityType(object p0)
		{
			return TextRes.GetString("MetadataBinder_KeyValueApplicableOnlyToEntityType", new object[] { p0 });
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000D528 File Offset: 0x0000B728
		internal static string MetadataBinder_PropertyNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000D550 File Offset: 0x0000B750
		internal static string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000D578 File Offset: 0x0000B778
		internal static string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties", new object[] { p0 });
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000D59C File Offset: 0x0000B79C
		internal static string MetadataBinder_DuplicitKeyPropertyInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_DuplicitKeyPropertyInKeyValues", new object[] { p0 });
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		internal static string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues", new object[] { p0 });
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		internal static string MetadataBinder_CannotConvertToType(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_CannotConvertToType", new object[] { p0, p1 });
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D60C File Offset: 0x0000B80C
		internal static string MetadataBinder_NonQueryableServiceOperationWithKeyLookup(object p0)
		{
			return TextRes.GetString("MetadataBinder_NonQueryableServiceOperationWithKeyLookup", new object[] { p0 });
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D630 File Offset: 0x0000B830
		internal static string MetadataBinder_QueryServiceOperationOfNonEntityType(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_QueryServiceOperationOfNonEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D65C File Offset: 0x0000B85C
		internal static string MetadataBinder_ServiceOperationParameterMissing(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_ServiceOperationParameterMissing", new object[] { p0, p1 });
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D684 File Offset: 0x0000B884
		internal static string MetadataBinder_ServiceOperationParameterInvalidType(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("MetadataBinder_ServiceOperationParameterInvalidType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D6B3 File Offset: 0x0000B8B3
		internal static string MetadataBinder_FilterNotApplicable
		{
			get
			{
				return TextRes.GetString("MetadataBinder_FilterNotApplicable");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000D6BF File Offset: 0x0000B8BF
		internal static string MetadataBinder_FilterExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_FilterExpressionNotSingleValue");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D6CB File Offset: 0x0000B8CB
		internal static string MetadataBinder_OrderByNotApplicable
		{
			get
			{
				return TextRes.GetString("MetadataBinder_OrderByNotApplicable");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000D6D7 File Offset: 0x0000B8D7
		internal static string MetadataBinder_OrderByExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_OrderByExpressionNotSingleValue");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D6E3 File Offset: 0x0000B8E3
		internal static string MetadataBinder_SkipNotApplicable
		{
			get
			{
				return TextRes.GetString("MetadataBinder_SkipNotApplicable");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D6EF File Offset: 0x0000B8EF
		internal static string MetadataBinder_TopNotApplicable
		{
			get
			{
				return TextRes.GetString("MetadataBinder_TopNotApplicable");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000D6FB File Offset: 0x0000B8FB
		internal static string MetadataBinder_PropertyAccessWithoutParentParameter
		{
			get
			{
				return TextRes.GetString("MetadataBinder_PropertyAccessWithoutParentParameter");
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D708 File Offset: 0x0000B908
		internal static string MetadataBinder_MultiValuePropertyNotSupportedInExpression(object p0)
		{
			return TextRes.GetString("MetadataBinder_MultiValuePropertyNotSupportedInExpression", new object[] { p0 });
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D72C File Offset: 0x0000B92C
		internal static string MetadataBinder_BinaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_BinaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D750 File Offset: 0x0000B950
		internal static string MetadataBinder_UnaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D774 File Offset: 0x0000B974
		internal static string MetadataBinder_PropertyAccessSourceNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_PropertyAccessSourceNotSingleValue", new object[] { p0 });
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D798 File Offset: 0x0000B998
		internal static string MetadataBinder_IncompatibleOperandsError(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandsError", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		internal static string MetadataBinder_IncompatibleOperandError(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandError", new object[] { p0, p1 });
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		internal static string MetadataBinder_UnknownFunction(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D810 File Offset: 0x0000BA10
		internal static string MetadataBinder_FunctionArgumentNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_FunctionArgumentNotSingleValue", new object[] { p0 });
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D834 File Offset: 0x0000BA34
		internal static string MetadataBinder_NoApplicableFunctionFound(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_NoApplicableFunctionFound", new object[] { p0, p1 });
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D85C File Offset: 0x0000BA5C
		internal static string MetadataBinder_UnsupportedSystemQueryOption(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedSystemQueryOption", new object[] { p0 });
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D880 File Offset: 0x0000BA80
		internal static string MetadataBinder_BoundNodeCannotBeNull(object p0)
		{
			return TextRes.GetString("MetadataBinder_BoundNodeCannotBeNull", new object[] { p0 });
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		internal static string MetadataBinder_TopRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_TopRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		internal static string MetadataBinder_SkipRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_SkipRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		internal static string MetadataBinder_ServiceOperationWithoutResultKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_ServiceOperationWithoutResultKind", new object[] { p0 });
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D910 File Offset: 0x0000BB10
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D934 File Offset: 0x0000BB34
		internal static string ExceptionUtils_CheckIntegerNotNegative(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerNotNegative", new object[] { p0 });
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D958 File Offset: 0x0000BB58
		internal static string ExceptionUtils_CheckIntegerPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerPositive", new object[] { p0 });
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D97C File Offset: 0x0000BB7C
		internal static string ExceptionUtils_CheckLongPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckLongPositive", new object[] { p0 });
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000D99F File Offset: 0x0000BB9F
		internal static string ExceptionUtils_ArgumentStringNullOrEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringNullOrEmpty");
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		internal static string ExpressionToken_IdentifierExpected(object p0)
		{
			return TextRes.GetString("ExpressionToken_IdentifierExpected", new object[] { p0 });
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		internal static string ExpressionLexer_UnterminatedStringLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedStringLiteral", new object[] { p0, p1 });
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		internal static string ExpressionLexer_InvalidCharacter(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidCharacter", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000DA24 File Offset: 0x0000BC24
		internal static string ExpressionLexer_SyntaxError(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_SyntaxError", new object[] { p0, p1 });
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		internal static string ExpressionLexer_UnterminatedLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedLiteral", new object[] { p0, p1 });
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000DA74 File Offset: 0x0000BC74
		internal static string ExpressionLexer_DigitExpected(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_DigitExpected", new object[] { p0, p1 });
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000DA9B File Offset: 0x0000BC9B
		internal static string ExpressionLexer_UnbalancedBracketExpression
		{
			get
			{
				return TextRes.GetString("ExpressionLexer_UnbalancedBracketExpression");
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		internal static string UriQueryExpressionParser_UnrecognizedLiteral(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteral", new object[] { p0, p1, p2, p3 });
		}
	}
}
