using System;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020001EA RID: 490
	public static class ODataUriUtils
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x00033B33 File Offset: 0x00031D33
		public static object ConvertFromUriLiteral(string value, ODataVersion version)
		{
			return ODataUriUtils.ConvertFromUriLiteral(value, version, null, null);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00033B40 File Offset: 0x00031D40
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
				return ODataUriConversionUtils.ConvertFromComplexOrCollectionValue(value, version, model, typeReference);
			}
			object obj = expressionLexer.ReadLiteralToken();
			if (typeReference != null)
			{
				obj = ODataUriConversionUtils.VerifyAndCoerceUriPrimitiveLiteral(obj, model, typeReference, version);
			}
			if (obj is ISpatial)
			{
				ODataVersionChecker.CheckSpatialValue(version);
			}
			return obj;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00033BBF File Offset: 0x00031DBF
		public static string ConvertToUriLiteral(object value, ODataVersion version)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, null);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00033BC9 File Offset: 0x00031DC9
		public static string ConvertToUriLiteral(object value, ODataVersion version, IEdmModel model)
		{
			return ODataUriUtils.ConvertToUriLiteral(value, version, model, ODataFormat.VerboseJson);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00033BD8 File Offset: 0x00031DD8
		public static string ConvertToUriLiteral(object value, ODataVersion version, IEdmModel model, ODataFormat format)
		{
			if (value == null)
			{
				value = new ODataUriNullValue();
			}
			if (model == null)
			{
				model = EdmCoreModel.Instance;
			}
			ODataUriNullValue odataUriNullValue = value as ODataUriNullValue;
			if (odataUriNullValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriNullValue(odataUriNullValue);
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriCollectionLiteral(odataCollectionValue, model, version, format);
			}
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return ODataUriConversionUtils.ConvertToUriComplexLiteral(odataComplexValue, model, version, format);
			}
			return ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value, version);
		}
	}
}
