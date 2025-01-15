using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200005F RID: 95
	internal sealed class TextRes
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		internal TextRes()
		{
			this.resources = new ResourceManager("Microsoft.Data.Experimental.OData", base.GetType().Assembly);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000CD80 File Offset: 0x0000AF80
		private static TextRes GetLoader()
		{
			if (TextRes.loader == null)
			{
				TextRes textRes = new TextRes();
				Interlocked.CompareExchange<TextRes>(ref TextRes.loader, textRes, null);
			}
			return TextRes.loader;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000CDAF File Offset: 0x0000AFAF
		public static ResourceManager Resources
		{
			get
			{
				return TextRes.GetLoader().resources;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		public static string GetString(string name, params object[] args)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			string @string = textRes.resources.GetString(name, TextRes.Culture);
			if (args != null && args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000CE40 File Offset: 0x0000B040
		public static string GetString(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetString(name, TextRes.Culture);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000CE69 File Offset: 0x0000B069
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return TextRes.GetString(name);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000CE74 File Offset: 0x0000B074
		public static object GetObject(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetObject(name, TextRes.Culture);
		}

		// Token: 0x04000228 RID: 552
		internal const string ODataQueryUtils_DidNotFindServiceOperation = "ODataQueryUtils_DidNotFindServiceOperation";

		// Token: 0x04000229 RID: 553
		internal const string ODataQueryUtils_FoundMultipleServiceOperations = "ODataQueryUtils_FoundMultipleServiceOperations";

		// Token: 0x0400022A RID: 554
		internal const string ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType = "ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType";

		// Token: 0x0400022B RID: 555
		internal const string ODataQueryUtils_DidNotFindEntitySet = "ODataQueryUtils_DidNotFindEntitySet";

		// Token: 0x0400022C RID: 556
		internal const string BinaryOperatorQueryNode_InvalidOperandType = "BinaryOperatorQueryNode_InvalidOperandType";

		// Token: 0x0400022D RID: 557
		internal const string BinaryOperatorQueryNode_OperandsMustHaveSameTypes = "BinaryOperatorQueryNode_OperandsMustHaveSameTypes";

		// Token: 0x0400022E RID: 558
		internal const string QueryExpressionTranslator_UnsupportedQueryNodeKind = "QueryExpressionTranslator_UnsupportedQueryNodeKind";

		// Token: 0x0400022F RID: 559
		internal const string QueryExpressionTranslator_UnsupportedExtensionNode = "QueryExpressionTranslator_UnsupportedExtensionNode";

		// Token: 0x04000230 RID: 560
		internal const string QueryExpressionTranslator_NodeTranslatedToNull = "QueryExpressionTranslator_NodeTranslatedToNull";

		// Token: 0x04000231 RID: 561
		internal const string QueryExpressionTranslator_NodeTranslatedToWrongType = "QueryExpressionTranslator_NodeTranslatedToWrongType";

		// Token: 0x04000232 RID: 562
		internal const string QueryExpressionTranslator_CollectionQueryNodeWithoutItemType = "QueryExpressionTranslator_CollectionQueryNodeWithoutItemType";

		// Token: 0x04000233 RID: 563
		internal const string QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference = "QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference";

		// Token: 0x04000234 RID: 564
		internal const string QueryExpressionTranslator_ConstantNonPrimitive = "QueryExpressionTranslator_ConstantNonPrimitive";

		// Token: 0x04000235 RID: 565
		internal const string QueryExpressionTranslator_KeyLookupOnlyOnEntities = "QueryExpressionTranslator_KeyLookupOnlyOnEntities";

		// Token: 0x04000236 RID: 566
		internal const string QueryExpressionTranslator_KeyLookupOnlyOnQueryable = "QueryExpressionTranslator_KeyLookupOnlyOnQueryable";

		// Token: 0x04000237 RID: 567
		internal const string QueryExpressionTranslator_KeyLookupWithoutKeyProperty = "QueryExpressionTranslator_KeyLookupWithoutKeyProperty";

		// Token: 0x04000238 RID: 568
		internal const string QueryExpressionTranslator_KeyLookupWithNoKeyValues = "QueryExpressionTranslator_KeyLookupWithNoKeyValues";

		// Token: 0x04000239 RID: 569
		internal const string QueryExpressionTranslator_KeyPropertyValueWithoutProperty = "QueryExpressionTranslator_KeyPropertyValueWithoutProperty";

		// Token: 0x0400023A RID: 570
		internal const string QueryExpressionTranslator_KeyPropertyValueWithWrongValue = "QueryExpressionTranslator_KeyPropertyValueWithWrongValue";

		// Token: 0x0400023B RID: 571
		internal const string QueryExpressionTranslator_FilterCollectionOfWrongType = "QueryExpressionTranslator_FilterCollectionOfWrongType";

		// Token: 0x0400023C RID: 572
		internal const string QueryExpressionTranslator_FilterExpressionOfWrongType = "QueryExpressionTranslator_FilterExpressionOfWrongType";

		// Token: 0x0400023D RID: 573
		internal const string QueryExpressionTranslator_UnaryNotOperandNotBoolean = "QueryExpressionTranslator_UnaryNotOperandNotBoolean";

		// Token: 0x0400023E RID: 574
		internal const string QueryExpressionTranslator_PropertyAccessSourceWrongType = "QueryExpressionTranslator_PropertyAccessSourceWrongType";

		// Token: 0x0400023F RID: 575
		internal const string QueryExpressionTranslator_PropertyAccessSourceNotStructured = "QueryExpressionTranslator_PropertyAccessSourceNotStructured";

		// Token: 0x04000240 RID: 576
		internal const string QueryExpressionTranslator_ParameterNotDefinedInScope = "QueryExpressionTranslator_ParameterNotDefinedInScope";

		// Token: 0x04000241 RID: 577
		internal const string QueryExpressionTranslator_OrderByCollectionOfWrongType = "QueryExpressionTranslator_OrderByCollectionOfWrongType";

		// Token: 0x04000242 RID: 578
		internal const string QueryExpressionTranslator_UnknownFunction = "QueryExpressionTranslator_UnknownFunction";

		// Token: 0x04000243 RID: 579
		internal const string QueryExpressionTranslator_FunctionArgumentNotSingleValue = "QueryExpressionTranslator_FunctionArgumentNotSingleValue";

		// Token: 0x04000244 RID: 580
		internal const string QueryExpressionTranslator_NoApplicableFunctionFound = "QueryExpressionTranslator_NoApplicableFunctionFound";

		// Token: 0x04000245 RID: 581
		internal const string QueryDescriptorQueryToken_UriMustBeAbsolute = "QueryDescriptorQueryToken_UriMustBeAbsolute";

		// Token: 0x04000246 RID: 582
		internal const string QueryDescriptorQueryToken_MaxDepthInvalid = "QueryDescriptorQueryToken_MaxDepthInvalid";

		// Token: 0x04000247 RID: 583
		internal const string QueryDescriptorQueryToken_InvalidSkipQueryOptionValue = "QueryDescriptorQueryToken_InvalidSkipQueryOptionValue";

		// Token: 0x04000248 RID: 584
		internal const string QueryDescriptorQueryToken_InvalidTopQueryOptionValue = "QueryDescriptorQueryToken_InvalidTopQueryOptionValue";

		// Token: 0x04000249 RID: 585
		internal const string QueryDescriptorQueryToken_InvalidInlineCountQueryOptionValue = "QueryDescriptorQueryToken_InvalidInlineCountQueryOptionValue";

		// Token: 0x0400024A RID: 586
		internal const string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce = "QueryOptionUtils_QueryParameterMustBeSpecifiedOnce";

		// Token: 0x0400024B RID: 587
		internal const string UriBuilder_NotSupportedClrLiteral = "UriBuilder_NotSupportedClrLiteral";

		// Token: 0x0400024C RID: 588
		internal const string UriBuilder_NotSupportedQueryToken = "UriBuilder_NotSupportedQueryToken";

		// Token: 0x0400024D RID: 589
		internal const string UriQueryExpressionParser_TooDeep = "UriQueryExpressionParser_TooDeep";

		// Token: 0x0400024E RID: 590
		internal const string UriQueryExpressionParser_ExpressionExpected = "UriQueryExpressionParser_ExpressionExpected";

		// Token: 0x0400024F RID: 591
		internal const string UriQueryExpressionParser_OpenParenExpected = "UriQueryExpressionParser_OpenParenExpected";

		// Token: 0x04000250 RID: 592
		internal const string UriQueryExpressionParser_CloseParenOrCommaExpected = "UriQueryExpressionParser_CloseParenOrCommaExpected";

		// Token: 0x04000251 RID: 593
		internal const string UriQueryExpressionParser_CloseParenOrOperatorExpected = "UriQueryExpressionParser_CloseParenOrOperatorExpected";

		// Token: 0x04000252 RID: 594
		internal const string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri = "UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri";

		// Token: 0x04000253 RID: 595
		internal const string UriQueryPathParser_SyntaxError = "UriQueryPathParser_SyntaxError";

		// Token: 0x04000254 RID: 596
		internal const string UriQueryPathParser_TooManySegments = "UriQueryPathParser_TooManySegments";

		// Token: 0x04000255 RID: 597
		internal const string UriQueryPathParser_InvalidKeyValueLiteral = "UriQueryPathParser_InvalidKeyValueLiteral";

		// Token: 0x04000256 RID: 598
		internal const string PropertyInfoTypeAnnotation_CannotFindProperty = "PropertyInfoTypeAnnotation_CannotFindProperty";

		// Token: 0x04000257 RID: 599
		internal const string MetadataBinder_UnsupportedQueryTokenKind = "MetadataBinder_UnsupportedQueryTokenKind";

		// Token: 0x04000258 RID: 600
		internal const string MetadataBinder_UnsupportedExtensionToken = "MetadataBinder_UnsupportedExtensionToken";

		// Token: 0x04000259 RID: 601
		internal const string MetadataBinder_RootSegmentResourceNotFound = "MetadataBinder_RootSegmentResourceNotFound";

		// Token: 0x0400025A RID: 602
		internal const string MetadataBinder_KeyValueApplicableOnlyToEntityType = "MetadataBinder_KeyValueApplicableOnlyToEntityType";

		// Token: 0x0400025B RID: 603
		internal const string MetadataBinder_PropertyNotDeclared = "MetadataBinder_PropertyNotDeclared";

		// Token: 0x0400025C RID: 604
		internal const string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue = "MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue";

		// Token: 0x0400025D RID: 605
		internal const string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties = "MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties";

		// Token: 0x0400025E RID: 606
		internal const string MetadataBinder_DuplicitKeyPropertyInKeyValues = "MetadataBinder_DuplicitKeyPropertyInKeyValues";

		// Token: 0x0400025F RID: 607
		internal const string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues = "MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues";

		// Token: 0x04000260 RID: 608
		internal const string MetadataBinder_CannotConvertToType = "MetadataBinder_CannotConvertToType";

		// Token: 0x04000261 RID: 609
		internal const string MetadataBinder_NonQueryableServiceOperationWithKeyLookup = "MetadataBinder_NonQueryableServiceOperationWithKeyLookup";

		// Token: 0x04000262 RID: 610
		internal const string MetadataBinder_QueryServiceOperationOfNonEntityType = "MetadataBinder_QueryServiceOperationOfNonEntityType";

		// Token: 0x04000263 RID: 611
		internal const string MetadataBinder_ServiceOperationParameterMissing = "MetadataBinder_ServiceOperationParameterMissing";

		// Token: 0x04000264 RID: 612
		internal const string MetadataBinder_ServiceOperationParameterInvalidType = "MetadataBinder_ServiceOperationParameterInvalidType";

		// Token: 0x04000265 RID: 613
		internal const string MetadataBinder_FilterNotApplicable = "MetadataBinder_FilterNotApplicable";

		// Token: 0x04000266 RID: 614
		internal const string MetadataBinder_FilterExpressionNotSingleValue = "MetadataBinder_FilterExpressionNotSingleValue";

		// Token: 0x04000267 RID: 615
		internal const string MetadataBinder_OrderByNotApplicable = "MetadataBinder_OrderByNotApplicable";

		// Token: 0x04000268 RID: 616
		internal const string MetadataBinder_OrderByExpressionNotSingleValue = "MetadataBinder_OrderByExpressionNotSingleValue";

		// Token: 0x04000269 RID: 617
		internal const string MetadataBinder_SkipNotApplicable = "MetadataBinder_SkipNotApplicable";

		// Token: 0x0400026A RID: 618
		internal const string MetadataBinder_TopNotApplicable = "MetadataBinder_TopNotApplicable";

		// Token: 0x0400026B RID: 619
		internal const string MetadataBinder_PropertyAccessWithoutParentParameter = "MetadataBinder_PropertyAccessWithoutParentParameter";

		// Token: 0x0400026C RID: 620
		internal const string MetadataBinder_MultiValuePropertyNotSupportedInExpression = "MetadataBinder_MultiValuePropertyNotSupportedInExpression";

		// Token: 0x0400026D RID: 621
		internal const string MetadataBinder_BinaryOperatorOperandNotSingleValue = "MetadataBinder_BinaryOperatorOperandNotSingleValue";

		// Token: 0x0400026E RID: 622
		internal const string MetadataBinder_UnaryOperatorOperandNotSingleValue = "MetadataBinder_UnaryOperatorOperandNotSingleValue";

		// Token: 0x0400026F RID: 623
		internal const string MetadataBinder_PropertyAccessSourceNotSingleValue = "MetadataBinder_PropertyAccessSourceNotSingleValue";

		// Token: 0x04000270 RID: 624
		internal const string MetadataBinder_IncompatibleOperandsError = "MetadataBinder_IncompatibleOperandsError";

		// Token: 0x04000271 RID: 625
		internal const string MetadataBinder_IncompatibleOperandError = "MetadataBinder_IncompatibleOperandError";

		// Token: 0x04000272 RID: 626
		internal const string MetadataBinder_UnknownFunction = "MetadataBinder_UnknownFunction";

		// Token: 0x04000273 RID: 627
		internal const string MetadataBinder_FunctionArgumentNotSingleValue = "MetadataBinder_FunctionArgumentNotSingleValue";

		// Token: 0x04000274 RID: 628
		internal const string MetadataBinder_NoApplicableFunctionFound = "MetadataBinder_NoApplicableFunctionFound";

		// Token: 0x04000275 RID: 629
		internal const string MetadataBinder_UnsupportedSystemQueryOption = "MetadataBinder_UnsupportedSystemQueryOption";

		// Token: 0x04000276 RID: 630
		internal const string MetadataBinder_BoundNodeCannotBeNull = "MetadataBinder_BoundNodeCannotBeNull";

		// Token: 0x04000277 RID: 631
		internal const string MetadataBinder_TopRequiresNonNegativeInteger = "MetadataBinder_TopRequiresNonNegativeInteger";

		// Token: 0x04000278 RID: 632
		internal const string MetadataBinder_SkipRequiresNonNegativeInteger = "MetadataBinder_SkipRequiresNonNegativeInteger";

		// Token: 0x04000279 RID: 633
		internal const string MetadataBinder_ServiceOperationWithoutResultKind = "MetadataBinder_ServiceOperationWithoutResultKind";

		// Token: 0x0400027A RID: 634
		internal const string General_InternalError = "General_InternalError";

		// Token: 0x0400027B RID: 635
		internal const string ExceptionUtils_CheckIntegerNotNegative = "ExceptionUtils_CheckIntegerNotNegative";

		// Token: 0x0400027C RID: 636
		internal const string ExceptionUtils_CheckIntegerPositive = "ExceptionUtils_CheckIntegerPositive";

		// Token: 0x0400027D RID: 637
		internal const string ExceptionUtils_CheckLongPositive = "ExceptionUtils_CheckLongPositive";

		// Token: 0x0400027E RID: 638
		internal const string ExceptionUtils_ArgumentStringNullOrEmpty = "ExceptionUtils_ArgumentStringNullOrEmpty";

		// Token: 0x0400027F RID: 639
		internal const string ExpressionToken_IdentifierExpected = "ExpressionToken_IdentifierExpected";

		// Token: 0x04000280 RID: 640
		internal const string ExpressionLexer_UnterminatedStringLiteral = "ExpressionLexer_UnterminatedStringLiteral";

		// Token: 0x04000281 RID: 641
		internal const string ExpressionLexer_InvalidCharacter = "ExpressionLexer_InvalidCharacter";

		// Token: 0x04000282 RID: 642
		internal const string ExpressionLexer_SyntaxError = "ExpressionLexer_SyntaxError";

		// Token: 0x04000283 RID: 643
		internal const string ExpressionLexer_UnterminatedLiteral = "ExpressionLexer_UnterminatedLiteral";

		// Token: 0x04000284 RID: 644
		internal const string ExpressionLexer_DigitExpected = "ExpressionLexer_DigitExpected";

		// Token: 0x04000285 RID: 645
		internal const string ExpressionLexer_UnbalancedBracketExpression = "ExpressionLexer_UnbalancedBracketExpression";

		// Token: 0x04000286 RID: 646
		internal const string UriQueryExpressionParser_UnrecognizedLiteral = "UriQueryExpressionParser_UnrecognizedLiteral";

		// Token: 0x04000287 RID: 647
		private static TextRes loader;

		// Token: 0x04000288 RID: 648
		private ResourceManager resources;
	}
}
