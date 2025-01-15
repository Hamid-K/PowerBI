using System;
using System.Text;
using System.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Json
{
	// Token: 0x02000A35 RID: 2613
	internal static class JsonFormatter
	{
		// Token: 0x060048C2 RID: 18626 RVA: 0x000F3300 File Offset: 0x000F1500
		public static string FormatValue(Value value)
		{
			switch (value.Kind)
			{
			case ValueKind.Null:
				return "null";
			case ValueKind.Time:
			{
				string text = JsonFormatter.FormatDateTime(DateTime.Today + value.AsTime.AsClrTimeSpan);
				return JsonFormatter.FormatString(text.Substring(text.IndexOf('T') + 1));
			}
			case ValueKind.Date:
			{
				string text = JsonFormatter.FormatDateTime(value.AsDate.AsClrDateTime);
				return JsonFormatter.FormatString(text.Substring(0, text.IndexOf('T')));
			}
			case ValueKind.DateTime:
				return JsonFormatter.FormatString(JsonFormatter.FormatDateTime(value.AsDateTime.AsClrDateTime));
			case ValueKind.DateTimeZone:
				return JsonFormatter.FormatString(XmlConvert.ToString(value.AsDateTimeZone.AsClrDateTimeOffset));
			case ValueKind.Duration:
				return JsonFormatter.FormatString(XmlConvert.ToString(value.AsDuration.AsClrTimeSpan));
			case ValueKind.Number:
				return JsonFormatter.FormatNumber(value.AsNumber);
			case ValueKind.Logical:
				return value.ToString();
			case ValueKind.Text:
				return JsonFormatter.FormatString(value.AsText.AsString);
			case ValueKind.Binary:
				return JsonFormatter.FormatString(Library.Binary.ToText.Invoke(value).AsString);
			}
			throw ValueException.NewExpressionError<Message0>(Strings.UnsupportedJsonType, value, null);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x000F3440 File Offset: 0x000F1640
		public static string FormatString(string s)
		{
			StringBuilder stringBuilder = new StringBuilder(s.Length + 20);
			stringBuilder.Append('"');
			foreach (char c in s)
			{
				if (c == '"' || c == '\\' || c == '/')
				{
					stringBuilder.Append('\\');
					stringBuilder.Append(c);
				}
				else if (c < ' ' || c > '\u007f')
				{
					stringBuilder.Append("\\u");
					stringBuilder.Append("0123456789abcdef"[(int)((c >> 12) & '\u000f')]);
					stringBuilder.Append("0123456789abcdef"[(int)((c >> 8) & '\u000f')]);
					stringBuilder.Append("0123456789abcdef"[(int)((c >> 4) & '\u000f')]);
					stringBuilder.Append("0123456789abcdef"[(int)(c & '\u000f')]);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x000F3530 File Offset: 0x000F1730
		private static string FormatNumber(NumberValue value)
		{
			double asDouble = value.AsDouble;
			if (double.IsNaN(asDouble) || double.IsInfinity(asDouble))
			{
				return "null";
			}
			return value.ToString();
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x000F3560 File Offset: 0x000F1760
		private static string FormatDateTime(DateTime datetime)
		{
			return XmlConvert.ToString(datetime, XmlDateTimeSerializationMode.Unspecified);
		}

		// Token: 0x040026EF RID: 9967
		private const string hexChars = "0123456789abcdef";

		// Token: 0x040026F0 RID: 9968
		private const string nullText = "null";
	}
}
