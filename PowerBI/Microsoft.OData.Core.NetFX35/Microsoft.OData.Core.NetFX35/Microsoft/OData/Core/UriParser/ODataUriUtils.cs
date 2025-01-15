using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001AE RID: 430
	public static class ODataUriUtils
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0003756C File Offset: 0x0003576C
		public static object ConvertFromUriLiteral(string value, ODataVersion version)
		{
			return ODataUriUtils.ConvertFromUriLiteral(value, version, null, null);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00037578 File Offset: 0x00035778
		public static object ConvertFromUriLiteral(string value, ODataVersion version, IEdmModel model, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(value, "value");
			if (typeReference != null && model == null)
			{
				throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel);
			}
			if (model == null)
			{
				model = EdmCoreModel.Instance;
			}
			ExpressionLexer expressionLexer = new ExpressionLexer(value, false, false);
			ExpressionToken expressionToken;
			Exception ex;
			expressionLexer.TryPeekNextToken(out expressionToken, out ex);
			if (expressionToken.Kind == ExpressionTokenKind.BracketedExpression)
			{
				return ODataUriConversionUtils.ConvertFromComplexOrCollectionValue(value, model, typeReference);
			}
			QueryNode queryNode;
			if (expressionToken.Kind == ExpressionTokenKind.Identifier && EnumBinder.TryBindIdentifier(expressionLexer.ExpressionText, null, model, out queryNode))
			{
				return ((ConstantNode)queryNode).Value;
			}
			object obj = expressionLexer.ReadLiteralToken();
			if (typeReference != null)
			{
				obj = ODataUriConversionUtils.VerifyAndCoerceUriPrimitiveLiteral(obj, value, model, typeReference);
			}
			return obj;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00037613 File Offset: 0x00035813
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "designed to aid the creation on a URI, not create a full one")]
		public static string ConvertToUriLiteral(object value, ODataVersion version)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, null);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00037620 File Offset: 0x00035820
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "designed to aid the creation on a URI, not create a full one")]
		public static string ConvertToUriLiteral(object value, ODataVersion version, IEdmModel model)
		{
			if (value == null)
			{
				value = new ODataNullValue();
			}
			if (model == null)
			{
				model = EdmCoreModel.Instance;
			}
			ODataNullValue odataNullValue = value as ODataNullValue;
			if (odataNullValue != null)
			{
				return "null";
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriCollectionLiteral(odataCollectionValue, model, version);
			}
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriComplexLiteral(odataComplexValue, model, version);
			}
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriEnumLiteral(odataEnumValue, version);
			}
			ODataEntry odataEntry = value as ODataEntry;
			if (odataEntry != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntityLiteral(odataEntry, model);
			}
			ODataEntityReferenceLink odataEntityReferenceLink = value as ODataEntityReferenceLink;
			if (odataEntityReferenceLink != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntityReferenceLiteral(odataEntityReferenceLink, model);
			}
			ODataEntityReferenceLinks odataEntityReferenceLinks = value as ODataEntityReferenceLinks;
			if (odataEntityReferenceLinks != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntityReferencesLiteral(odataEntityReferenceLinks, model);
			}
			IEnumerable<ODataEntry> enumerable = value as IEnumerable<ODataEntry>;
			if (enumerable != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntitiesLiteral(enumerable, model);
			}
			value = model.ConvertToUnderlyingTypeIfUIntValue(value, null);
			return ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value, version);
		}
	}
}
