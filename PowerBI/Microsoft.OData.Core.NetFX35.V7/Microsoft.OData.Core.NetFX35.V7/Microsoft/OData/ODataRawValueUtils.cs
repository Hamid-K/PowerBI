using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000090 RID: 144
	internal static class ODataRawValueUtils
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0000F448 File Offset: 0x0000D648
		internal static bool TryConvertPrimitiveToString(object value, out string result)
		{
			if (value is bool)
			{
				result = ODataRawValueConverter.ToString((bool)value);
				return true;
			}
			if (value is byte)
			{
				result = ODataRawValueConverter.ToString((byte)value);
				return true;
			}
			if (value is decimal)
			{
				result = ODataRawValueConverter.ToString((decimal)value);
				return true;
			}
			if (value is double)
			{
				result = ((double)value).ToString();
				return true;
			}
			if (value is short)
			{
				result = ((short)value).ToString();
				return true;
			}
			if (value is int)
			{
				result = ((int)value).ToString();
				return true;
			}
			if (value is long)
			{
				result = ((long)value).ToString();
				return true;
			}
			if (value is sbyte)
			{
				result = ((sbyte)value).ToString();
				return true;
			}
			string text = value as string;
			if (text != null)
			{
				result = text;
				return true;
			}
			if (value is float)
			{
				result = ((float)value).ToString();
				return true;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				result = array.ToString();
				return true;
			}
			if (value is DateTimeOffset)
			{
				result = ODataRawValueConverter.ToString((DateTimeOffset)value);
				return true;
			}
			if (value is Guid)
			{
				result = ((Guid)value).ToString();
				return true;
			}
			if (value is TimeSpan)
			{
				result = ((TimeSpan)value).ToString();
				return true;
			}
			if (value is Date)
			{
				result = ODataRawValueConverter.ToString((Date)value);
				return true;
			}
			if (value is TimeOfDay)
			{
				result = ODataRawValueConverter.ToString((TimeOfDay)value);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		internal static object ConvertStringToPrimitive(string text, IEdmPrimitiveTypeReference targetTypeReference)
		{
			try
			{
				switch (targetTypeReference.PrimitiveKind())
				{
				case EdmPrimitiveTypeKind.Binary:
					return Convert.FromBase64String(text);
				case EdmPrimitiveTypeKind.Boolean:
					return ODataRawValueUtils.ConvertXmlBooleanValue(text);
				case EdmPrimitiveTypeKind.Byte:
					return XmlConvert.ToByte(text);
				case EdmPrimitiveTypeKind.DateTimeOffset:
					return PlatformHelper.ConvertStringToDateTimeOffset(text);
				case EdmPrimitiveTypeKind.Decimal:
					return XmlConvert.ToDecimal(text);
				case EdmPrimitiveTypeKind.Double:
					return XmlConvert.ToDouble(text);
				case EdmPrimitiveTypeKind.Guid:
					return new Guid(text);
				case EdmPrimitiveTypeKind.Int16:
					return XmlConvert.ToInt16(text);
				case EdmPrimitiveTypeKind.Int32:
					return XmlConvert.ToInt32(text);
				case EdmPrimitiveTypeKind.Int64:
					return XmlConvert.ToInt64(text);
				case EdmPrimitiveTypeKind.SByte:
					return XmlConvert.ToSByte(text);
				case EdmPrimitiveTypeKind.Single:
					return XmlConvert.ToSingle(text);
				case EdmPrimitiveTypeKind.String:
					return text;
				case EdmPrimitiveTypeKind.Duration:
					return EdmValueParser.ParseDuration(text);
				case EdmPrimitiveTypeKind.Date:
					return PlatformHelper.ConvertStringToDate(text);
				case EdmPrimitiveTypeKind.TimeOfDay:
					return PlatformHelper.ConvertStringToTimeOfDay(text);
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataRawValueUtils_ConvertStringToPrimitive));
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(targetTypeReference, ex, text);
			}
			object obj;
			return obj;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000F794 File Offset: 0x0000D994
		private static bool ConvertXmlBooleanValue(string text)
		{
			text = text.Trim(ODataRawValueUtils.XmlWhitespaceChars);
			return text == "true" || text == "True" || text == "1" || (!(text == "false") && !(text == "False") && !(text == "0") && XmlConvert.ToBoolean(text));
		}

		// Token: 0x040002A5 RID: 677
		private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
