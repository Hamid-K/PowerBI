using System;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.OData.Json;

namespace Microsoft.ReportingServices.OData.Query
{
	// Token: 0x0200000F RID: 15
	internal static class ODataUriConversionUtils
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002504 File Offset: 0x00000704
		internal static string ConvertToUriPrimitiveLiteral(object value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type type = value.GetType();
			type = Nullable.GetUnderlyingType(type) ?? type;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					throw new InvalidOperationException(Errors.UriLiteralUnsupportedType(type.ToString()));
				}
				if (type == typeof(Guid))
				{
					stringBuilder.Append("guid");
					stringBuilder.Append("'");
					stringBuilder.Append(value.ToString());
					stringBuilder.Append("'");
				}
				else if (type == typeof(DateTimeOffset))
				{
					stringBuilder.Append("datetimeoffset");
					stringBuilder.Append("'");
					stringBuilder.Append(XmlConvert.ToString((DateTimeOffset)value));
					stringBuilder.Append("'");
				}
				else
				{
					if (!(type == typeof(TimeSpan)))
					{
						throw new InvalidOperationException(Errors.UnsupportedType(type.ToString()));
					}
					stringBuilder.Append("time");
					stringBuilder.Append("'");
					stringBuilder.Append(XmlConvert.ToString((TimeSpan)value));
					stringBuilder.Append("'");
				}
				return stringBuilder.ToString();
			case TypeCode.Boolean:
				stringBuilder.Append(XmlConvert.ToString((bool)value));
				return stringBuilder.ToString();
			case TypeCode.SByte:
				stringBuilder.Append(XmlConvert.ToString((sbyte)value));
				return stringBuilder.ToString();
			case TypeCode.Byte:
				stringBuilder.Append(XmlConvert.ToString((byte)value));
				return stringBuilder.ToString();
			case TypeCode.Int16:
				stringBuilder.Append(XmlConvert.ToString((short)value));
				return stringBuilder.ToString();
			case TypeCode.Int32:
				stringBuilder.Append(XmlConvert.ToString((int)value));
				return stringBuilder.ToString();
			case TypeCode.Int64:
				stringBuilder.Append(XmlConvert.ToString((long)value));
				stringBuilder.Append("L");
				return stringBuilder.ToString();
			case TypeCode.Single:
				stringBuilder.Append(ODataUriConversionUtils.AppendDecimalMarker(XmlConvert.ToString((float)value)));
				stringBuilder.Append("f");
				return stringBuilder.ToString();
			case TypeCode.Double:
				stringBuilder.Append(ODataUriConversionUtils.AppendDecimalMarker(XmlConvert.ToString((double)value)));
				stringBuilder.Append("D");
				return stringBuilder.ToString();
			case TypeCode.Decimal:
				stringBuilder.Append(XmlConvert.ToString((decimal)value));
				stringBuilder.Append("M");
				return stringBuilder.ToString();
			case TypeCode.DateTime:
				stringBuilder.Append("datetime");
				stringBuilder.Append("'");
				stringBuilder.Append(XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.Unspecified));
				stringBuilder.Append("'");
				return stringBuilder.ToString();
			case TypeCode.String:
				stringBuilder.Append("'");
				stringBuilder.Append(((string)value).Replace("'", "''"));
				stringBuilder.Append("'");
				return stringBuilder.ToString();
			}
			throw new InvalidOperationException(Errors.UnsupportedType(type.ToString()));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002844 File Offset: 0x00000A44
		private static string AppendDecimalMarker(string input)
		{
			if (input.Contains(".") || input.Contains("INF") || input.Contains("NaN"))
			{
				return input;
			}
			int num = input.IndexOf('E');
			if (num < 0)
			{
				num = input.IndexOf('e');
			}
			if (num < 0)
			{
				return input + ".0";
			}
			return input.Substring(0, num) + ".0" + input.Substring(num);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028BC File Offset: 0x00000ABC
		private static string ConvertByteArrayToKeyString(byte[] byteArray)
		{
			StringBuilder stringBuilder = new StringBuilder(3 + byteArray.Length * 2);
			stringBuilder.Append("X");
			stringBuilder.Append("'");
			for (int i = 0; i < byteArray.Length; i++)
			{
				stringBuilder.Append(byteArray[i].ToString("X2", CultureInfo.InvariantCulture));
			}
			stringBuilder.Append("'");
			return stringBuilder.ToString();
		}
	}
}
