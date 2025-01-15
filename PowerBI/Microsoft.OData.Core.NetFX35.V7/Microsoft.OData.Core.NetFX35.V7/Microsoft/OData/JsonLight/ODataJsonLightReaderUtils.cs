using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000217 RID: 535
	internal static class ODataJsonLightReaderUtils
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x00042921 File Offset: 0x00040B21
		internal static bool ErrorPropertyNotFound(ref ODataJsonLightReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataJsonLightReaderUtils.ErrorPropertyBitMask propertyFoundBitMask)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				return false;
			}
			propertiesFoundBitField |= propertyFoundBitMask;
			return true;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00042934 File Offset: 0x00040B34
		internal static object ConvertValue(object value, IEdmPrimitiveTypeReference primitiveTypeReference, ODataMessageReaderSettings messageReaderSettings, bool validateNullValue, string propertyName, ODataPayloadValueConverter converter)
		{
			if (value == null)
			{
				messageReaderSettings.Validator.ValidateNullValue(primitiveTypeReference, validateNullValue, propertyName, default(bool?));
				return null;
			}
			value = converter.ConvertFromPayloadValue(value, primitiveTypeReference);
			return value;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0004296A File Offset: 0x00040B6A
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00042984 File Offset: 0x00040B84
		internal static bool IsODataAnnotationName(string propertyName)
		{
			return propertyName.StartsWith("odata.", 4);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00042992 File Offset: 0x00040B92
		internal static bool IsAnnotationProperty(string propertyName)
		{
			return propertyName.IndexOf('.') >= 0;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000429A2 File Offset: 0x00040BA2
		internal static void ValidateAnnotationValue(object propertyValue, string annotationName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightReaderUtils_AnnotationWithNullValue(annotationName));
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000429B4 File Offset: 0x00040BB4
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
			ODataResource odataResource = payloadItem as ODataResource;
			if (odataResource != null)
			{
				return odataResource.TypeName;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightReader_ReadResourceStart));
		}

		// Token: 0x0200035A RID: 858
		[Flags]
		internal enum ErrorPropertyBitMask
		{
			// Token: 0x04000D95 RID: 3477
			None = 0,
			// Token: 0x04000D96 RID: 3478
			Error = 1,
			// Token: 0x04000D97 RID: 3479
			Code = 2,
			// Token: 0x04000D98 RID: 3480
			Message = 4,
			// Token: 0x04000D99 RID: 3481
			MessageValue = 16,
			// Token: 0x04000D9A RID: 3482
			InnerError = 32,
			// Token: 0x04000D9B RID: 3483
			TypeName = 64,
			// Token: 0x04000D9C RID: 3484
			StackTrace = 128,
			// Token: 0x04000D9D RID: 3485
			Target = 256,
			// Token: 0x04000D9E RID: 3486
			Details = 512
		}
	}
}
