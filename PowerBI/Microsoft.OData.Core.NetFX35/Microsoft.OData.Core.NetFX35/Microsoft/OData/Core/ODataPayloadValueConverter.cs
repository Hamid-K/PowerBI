using System;
using System.Globalization;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x02000190 RID: 400
	public class ODataPayloadValueConverter
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x00034821 File Offset: 0x00032A21
		public virtual object ConvertToPayloadValue(object value, IEdmTypeReference edmTypeReference)
		{
			return value;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00034824 File Offset: 0x00032A24
		public virtual object ConvertFromPayloadValue(object value, IEdmTypeReference edmTypeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = edmTypeReference as IEdmPrimitiveTypeReference;
			try
			{
				Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType(edmPrimitiveTypeReference.PrimitiveDefinition(), false);
				string text = value as string;
				if (text != null)
				{
					return ODataPayloadValueConverter.ConvertStringValue(text, primitiveClrType);
				}
				if (value is int)
				{
					return ODataPayloadValueConverter.ConvertInt32Value((int)value, primitiveClrType, edmPrimitiveTypeReference);
				}
				if (value is decimal)
				{
					decimal num = (decimal)value;
					if (primitiveClrType == typeof(long))
					{
						return Convert.ToInt64(num);
					}
					if (primitiveClrType == typeof(double))
					{
						return Convert.ToDouble(num);
					}
					if (primitiveClrType == typeof(float))
					{
						return Convert.ToSingle(num);
					}
					if (primitiveClrType != typeof(decimal))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDecimal(edmPrimitiveTypeReference.FullName()));
					}
				}
				else if (value is double)
				{
					double num2 = (double)value;
					if (primitiveClrType == typeof(float))
					{
						return Convert.ToSingle(num2);
					}
					if (primitiveClrType != typeof(double))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDouble(edmPrimitiveTypeReference.FullName()));
					}
				}
				else if (value is bool)
				{
					if (primitiveClrType != typeof(bool))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertBoolean(edmPrimitiveTypeReference.FullName()));
					}
				}
				else if (value is DateTime)
				{
					if (primitiveClrType != typeof(DateTime))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTime(edmPrimitiveTypeReference.FullName()));
					}
				}
				else if (value is DateTimeOffset && primitiveClrType != typeof(DateTimeOffset))
				{
					throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTimeOffset(edmPrimitiveTypeReference.FullName()));
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(edmPrimitiveTypeReference, ex, value.ToString());
			}
			return value;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00034A0C File Offset: 0x00032C0C
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
				return EdmValueParser.ParseDuration(stringValue);
			}
			if (targetType == typeof(Date))
			{
				return PlatformHelper.ConvertStringToDate(stringValue);
			}
			if (targetType == typeof(TimeOfDay))
			{
				return PlatformHelper.ConvertStringToTimeOfDay(stringValue);
			}
			if (targetType == typeof(DateTimeOffset))
			{
				return PlatformHelper.ConvertStringToDateTimeOffset(stringValue);
			}
			if (targetType == typeof(double) || targetType == typeof(float))
			{
				if (stringValue == CultureInfo.InvariantCulture.NumberFormat.PositiveInfinitySymbol)
				{
					stringValue = JsonValueUtils.ODataJsonPositiveInfinitySymbol;
				}
				else if (stringValue == CultureInfo.InvariantCulture.NumberFormat.NegativeInfinitySymbol)
				{
					stringValue = JsonValueUtils.ODataJsonNegativeInfinitySymbol;
				}
				return Convert.ChangeType(stringValue, targetType, JsonValueUtils.ODataNumberFormatInfo);
			}
			return Convert.ChangeType(stringValue, targetType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00034B1C File Offset: 0x00032D1C
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
			if (targetType == typeof(decimal))
			{
				return Convert.ToDecimal(intValue);
			}
			if (targetType == typeof(long))
			{
				return Convert.ToInt64(intValue);
			}
			if (targetType != typeof(int))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt32(primitiveTypeReference.FullName()));
			}
			return intValue;
		}

		// Token: 0x0400068A RID: 1674
		internal static readonly ODataPayloadValueConverter Default = new ODataPayloadValueConverter();
	}
}
