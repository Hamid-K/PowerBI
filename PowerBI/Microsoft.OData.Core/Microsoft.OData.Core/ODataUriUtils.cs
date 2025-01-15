using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000D8 RID: 216
	public static class ODataUriUtils
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x0001ABDA File Offset: 0x00018DDA
		public static object ConvertFromUriLiteral(string value, ODataVersion version)
		{
			return ODataUriUtils.ConvertFromUriLiteral(value, version, null, null);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001ABE8 File Offset: 0x00018DE8
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
			if (expressionToken.Kind == ExpressionTokenKind.BracedExpression && typeReference != null && typeReference.IsStructured())
			{
				return ODataUriConversionUtils.ConvertFromResourceValue(value, model, typeReference);
			}
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

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "designed to aid the creation on a URI, not create a full one")]
		public static string ConvertToUriLiteral(object value, ODataVersion version)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, null);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001ACAC File Offset: 0x00018EAC
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
			ODataResourceValue odataResourceValue = value as ODataResourceValue;
			if (odataResourceValue != null)
			{
				return ODataUriConversionUtils.ConvertToResourceLiteral(odataResourceValue, model, version);
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
			ODataResourceBase odataResourceBase = value as ODataResourceBase;
			if (odataResourceBase != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntityLiteral(odataResourceBase, model);
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
			IEnumerable<ODataResourceBase> enumerable = value as IEnumerable<ODataResourceBase>;
			if (enumerable != null)
			{
				return ODataUriConversionUtils.ConvertToUriEntitiesLiteral(enumerable, model);
			}
			value = model.ConvertToUnderlyingTypeIfUIntValue(value, null);
			return ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value, version);
		}
	}
}
