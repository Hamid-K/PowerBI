using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000F9 RID: 249
	internal static class ODataJsonLightReaderUtils
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x000227C5 File Offset: 0x000209C5
		internal static bool ErrorPropertyNotFound(ref ODataJsonLightReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataJsonLightReaderUtils.ErrorPropertyBitMask propertyFoundBitMask)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				return false;
			}
			propertiesFoundBitField |= propertyFoundBitMask;
			return true;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000227D8 File Offset: 0x000209D8
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal static object ConvertValue(object value, IEdmPrimitiveTypeReference primitiveTypeReference, ODataMessageReaderSettings messageReaderSettings, bool validateNullValue, string propertyName, ODataPayloadValueConverter converter)
		{
			if (value == null)
			{
				ReaderValidationUtils.ValidateNullValue(EdmCoreModel.Instance, primitiveTypeReference, messageReaderSettings, validateNullValue, propertyName, default(bool?));
				return null;
			}
			value = converter.ConvertFromPayloadValue(value, primitiveTypeReference);
			return value;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002280E File Offset: 0x00020A0E
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00022828 File Offset: 0x00020A28
		internal static bool IsODataAnnotationName(string propertyName)
		{
			return propertyName.StartsWith("odata.", 4);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00022836 File Offset: 0x00020A36
		internal static bool IsAnnotationProperty(string propertyName)
		{
			return propertyName.IndexOf('.') >= 0;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00022846 File Offset: 0x00020A46
		internal static void ValidateAnnotationValue(object propertyValue, string annotationName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightReaderUtils_AnnotationWithNullValue(annotationName));
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00022858 File Offset: 0x00020A58
		internal static string GetPayloadTypeName(object payloadItem)
		{
			if (payloadItem == null)
			{
				return null;
			}
			TypeCode typeCode = PlatformHelper.GetTypeCode(payloadItem.GetType());
			TypeCode typeCode2 = typeCode;
			if (typeCode2 <= 9)
			{
				if (typeCode2 == 3)
				{
					return "Edm.Boolean";
				}
				if (typeCode2 == 9)
				{
					return "Edm.Int32";
				}
			}
			else
			{
				if (typeCode2 == 14)
				{
					return "Edm.Double";
				}
				if (typeCode2 == 18)
				{
					return "Edm.String";
				}
			}
			ODataComplexValue odataComplexValue = payloadItem as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return odataComplexValue.TypeName;
			}
			ODataCollectionValue odataCollectionValue = payloadItem as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return EdmLibraryExtensions.GetCollectionTypeFullName(odataCollectionValue.TypeName);
			}
			ODataEntry odataEntry = payloadItem as ODataEntry;
			if (odataEntry != null)
			{
				return odataEntry.TypeName;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightReader_ReadEntryStart));
		}

		// Token: 0x020000FA RID: 250
		[Flags]
		internal enum ErrorPropertyBitMask
		{
			// Token: 0x040003C7 RID: 967
			None = 0,
			// Token: 0x040003C8 RID: 968
			Error = 1,
			// Token: 0x040003C9 RID: 969
			Code = 2,
			// Token: 0x040003CA RID: 970
			Message = 4,
			// Token: 0x040003CB RID: 971
			MessageValue = 16,
			// Token: 0x040003CC RID: 972
			InnerError = 32,
			// Token: 0x040003CD RID: 973
			TypeName = 64,
			// Token: 0x040003CE RID: 974
			StackTrace = 128,
			// Token: 0x040003CF RID: 975
			Target = 256,
			// Token: 0x040003D0 RID: 976
			Details = 512
		}
	}
}
