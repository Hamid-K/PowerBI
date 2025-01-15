using System;
using System.Globalization;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000086 RID: 134
	public class ODataPayloadValueConverter
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public virtual object ConvertToPayloadValue(object value, IEdmTypeReference edmTypeReference)
		{
			return value;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
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

		// Token: 0x06000534 RID: 1332 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		internal static ODataPayloadValueConverter GetPayloadValueConverter(IServiceProvider container)
		{
			if (container == null)
			{
				return ODataPayloadValueConverter.Default;
			}
			return container.GetRequiredService<ODataPayloadValueConverter>();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
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

		// Token: 0x06000536 RID: 1334 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
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

		// Token: 0x04000279 RID: 633
		private static readonly ODataPayloadValueConverter Default = new ODataPayloadValueConverter();
	}
}
