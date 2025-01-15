using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000D5 RID: 213
	public static class ODataUriUtils
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x0001778A File Offset: 0x0001598A
		public static object ConvertFromUriLiteral(string value, ODataVersion version)
		{
			return ODataUriUtils.ConvertFromUriLiteral(value, version, null, null);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00017798 File Offset: 0x00015998
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
				return ODataUriConversionUtils.ConvertFromCollectionValue(value, model, typeReference);
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

		// Token: 0x06000842 RID: 2114 RVA: 0x00017832 File Offset: 0x00015A32
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "designed to aid the creation on a URI, not create a full one")]
		public static string ConvertToUriLiteral(object value, ODataVersion version)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, null);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001783C File Offset: 0x00015A3C
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
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriEnumLiteral(odataEnumValue, version);
			}
			ODataResource odataResource = value as ODataResource;
			if (odataResource != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntityLiteral(odataResource, model);
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
			IEnumerable<ODataResource> enumerable = value as IEnumerable<ODataResource>;
			if (enumerable != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntitiesLiteral(enumerable, model);
			}
			value = model.ConvertToUnderlyingTypeIfUIntValue(value, null);
			return ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value, version);
		}
	}
}
