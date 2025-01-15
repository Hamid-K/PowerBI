using System;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001A1 RID: 417
	internal static class ODataJsonLightReaderUtils
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x0002A07C File Offset: 0x0002827C
		internal static object ConvertValue(object value, IEdmPrimitiveTypeReference primitiveTypeReference, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool validateNullValue, string propertyName)
		{
			if (value == null)
			{
				ReaderValidationUtils.ValidateNullValue(EdmCoreModel.Instance, primitiveTypeReference, messageReaderSettings, validateNullValue, version, propertyName);
				return null;
			}
			try
			{
				Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), false);
				string text = value as string;
				if (text != null)
				{
					return ODataJsonLightReaderUtils.ConvertStringValue(text, primitiveClrType);
				}
				if (value is int)
				{
					return ODataJsonLightReaderUtils.ConvertInt32Value((int)value, primitiveClrType, primitiveTypeReference);
				}
				if (value is double)
				{
					double num = (double)value;
					if (primitiveClrType == typeof(float))
					{
						return Convert.ToSingle(num);
					}
					if (primitiveClrType != typeof(double))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDouble(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is bool)
				{
					if (primitiveClrType != typeof(bool))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertBoolean(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is DateTime)
				{
					if (primitiveClrType != typeof(DateTime))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTime(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is DateTimeOffset && primitiveClrType != typeof(DateTimeOffset))
				{
					throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTimeOffset(primitiveTypeReference.ODataFullName()));
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(primitiveTypeReference, ex);
			}
			return value;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002A1D8 File Offset: 0x000283D8
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002A1F2 File Offset: 0x000283F2
		internal static bool IsODataAnnotationName(string propertyName)
		{
			return propertyName.StartsWith("odata.", 4);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002A200 File Offset: 0x00028400
		internal static bool IsAnnotationProperty(string propertyName)
		{
			return propertyName.IndexOf('.') >= 0;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002A210 File Offset: 0x00028410
		internal static void ValidateAnnotationStringValue(string propertyValue, string annotationName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightReaderUtils_AnnotationWithNullValue(annotationName));
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002A224 File Offset: 0x00028424
		internal static string GetPayloadTypeName(object payloadItem)
		{
			if (payloadItem == null)
			{
				return null;
			}
			TypeCode typeCode = PlatformHelper.GetTypeCode(payloadItem.GetType());
			TypeCode typeCode2 = typeCode;
			if (typeCode2 == 3)
			{
				return "Edm.Boolean";
			}
			if (typeCode2 == 9)
			{
				return "Edm.Int32";
			}
			switch (typeCode2)
			{
			case 14:
				return "Edm.Double";
			case 16:
				return "Edm.DateTime";
			case 18:
				return "Edm.String";
			}
			ODataComplexValue odataComplexValue = payloadItem as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return odataComplexValue.TypeName;
			}
			ODataCollectionValue odataCollectionValue = payloadItem as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return odataCollectionValue.TypeName;
			}
			ODataEntry odataEntry = payloadItem as ODataEntry;
			if (odataEntry != null)
			{
				return odataEntry.TypeName;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightReader_ReadEntryStart));
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002A2D4 File Offset: 0x000284D4
		private static object ConvertStringValue(string stringValue, Type targetType)
		{
			if (targetType == typeof(byte[]))
			{
				return Convert.FromBase64String(stringValue);
			}
			if (targetType == typeof(Guid))
			{
				return new Guid(stringValue);
			}
			if (targetType == typeof(TimeSpan))
			{
				return XmlConvert.ToTimeSpan(stringValue);
			}
			if (targetType == typeof(DateTime))
			{
				return PlatformHelper.ConvertStringToDateTime(stringValue);
			}
			if (targetType == typeof(DateTimeOffset))
			{
				return PlatformHelper.ConvertStringToDateTimeOffset(stringValue);
			}
			return Convert.ChangeType(stringValue, targetType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002A368 File Offset: 0x00028568
		private static object ConvertInt32Value(int intValue, Type targetType, IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			if (targetType == typeof(short))
			{
				return Convert.ToInt16(intValue);
			}
			if (targetType == typeof(byte))
			{
				return Convert.ToByte(intValue);
			}
			if (targetType == typeof(sbyte))
			{
				return Convert.ToSByte(intValue);
			}
			if (targetType == typeof(float))
			{
				return Convert.ToSingle(intValue);
			}
			if (targetType == typeof(double))
			{
				return Convert.ToDouble(intValue);
			}
			if (targetType == typeof(decimal) || targetType == typeof(long))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt64OrDecimal);
			}
			if (targetType != typeof(int))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt32(primitiveTypeReference.ODataFullName()));
			}
			return intValue;
		}
	}
}
