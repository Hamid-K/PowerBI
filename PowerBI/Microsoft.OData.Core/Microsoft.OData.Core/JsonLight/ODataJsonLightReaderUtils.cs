using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000250 RID: 592
	internal static class ODataJsonLightReaderUtils
	{
		// Token: 0x06001AA7 RID: 6823 RVA: 0x00050E05 File Offset: 0x0004F005
		internal static bool ErrorPropertyNotFound(ref ODataJsonLightReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataJsonLightReaderUtils.ErrorPropertyBitMask propertyFoundBitMask)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				return false;
			}
			propertiesFoundBitField |= propertyFoundBitMask;
			return true;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00050E18 File Offset: 0x0004F018
		internal static object ConvertValue(object value, IEdmPrimitiveTypeReference primitiveTypeReference, ODataMessageReaderSettings messageReaderSettings, bool validateNullValue, string propertyName, ODataPayloadValueConverter converter)
		{
			if (value == null)
			{
				messageReaderSettings.Validator.ValidateNullValue(primitiveTypeReference, validateNullValue, propertyName, null);
				return null;
			}
			value = converter.ConvertFromPayloadValue(value, primitiveTypeReference);
			return value;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00050E4E File Offset: 0x0004F04E
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00050E68 File Offset: 0x0004F068
		internal static bool IsODataAnnotationName(string propertyName)
		{
			return propertyName.StartsWith("odata.", StringComparison.Ordinal);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00050E76 File Offset: 0x0004F076
		internal static bool IsAnnotationProperty(string propertyName)
		{
			return propertyName.IndexOf('.') >= 0;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00050E86 File Offset: 0x0004F086
		internal static void ValidateAnnotationValue(object propertyValue, string annotationName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightReaderUtils_AnnotationWithNullValue(annotationName));
			}
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00050E98 File Offset: 0x0004F098
		internal static string GetPayloadTypeName(object payloadItem)
		{
			if (payloadItem == null)
			{
				return null;
			}
			if (payloadItem is bool)
			{
				return "Edm.Boolean";
			}
			if (payloadItem is string)
			{
				return "Edm.String";
			}
			if (payloadItem is int)
			{
				return "Edm.Int32";
			}
			if (payloadItem is double)
			{
				return "Edm.Double";
			}
			ODataCollectionValue odataCollectionValue = payloadItem as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return EdmLibraryExtensions.GetCollectionTypeFullName(odataCollectionValue.TypeName);
			}
			ODataResourceBase odataResourceBase = payloadItem as ODataResourceBase;
			if (odataResourceBase != null)
			{
				return odataResourceBase.TypeName;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightReader_ReadResourceStart));
		}

		// Token: 0x0200043B RID: 1083
		[Flags]
		internal enum ErrorPropertyBitMask
		{
			// Token: 0x0400104A RID: 4170
			None = 0,
			// Token: 0x0400104B RID: 4171
			Error = 1,
			// Token: 0x0400104C RID: 4172
			Code = 2,
			// Token: 0x0400104D RID: 4173
			Message = 4,
			// Token: 0x0400104E RID: 4174
			MessageValue = 16,
			// Token: 0x0400104F RID: 4175
			InnerError = 32,
			// Token: 0x04001050 RID: 4176
			TypeName = 64,
			// Token: 0x04001051 RID: 4177
			StackTrace = 128,
			// Token: 0x04001052 RID: 4178
			Target = 256,
			// Token: 0x04001053 RID: 4179
			Details = 512
		}
	}
}
