using System;
using System.Globalization;
using Microsoft.Mashup.Engine1;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x0200020C RID: 524
	internal static class SalesforceTypes
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x00018510 File Offset: 0x00016710
		public static TypeValue TranslateType(string type, bool reportContext)
		{
			if (type != null)
			{
				switch (type.Length)
				{
				case 2:
					if (!(type == "id"))
					{
						goto IL_031B;
					}
					goto IL_0303;
				case 3:
				{
					char c = type[0];
					if (c != 'i')
					{
						if (c != 'u')
						{
							goto IL_031B;
						}
						if (!(type == "url"))
						{
							goto IL_031B;
						}
						goto IL_030B;
					}
					else
					{
						if (!(type == "int"))
						{
							goto IL_031B;
						}
						return TypeValue.Int32;
					}
					break;
				}
				case 4:
					if (!(type == "date"))
					{
						goto IL_031B;
					}
					return TypeValue.Date;
				case 5:
				{
					char c = type[0];
					if (c != 'e')
					{
						if (c != 'p')
						{
							goto IL_031B;
						}
						if (!(type == "phone"))
						{
							goto IL_031B;
						}
						goto IL_030B;
					}
					else
					{
						if (!(type == "email"))
						{
							goto IL_031B;
						}
						goto IL_030B;
					}
					break;
				}
				case 6:
				{
					char c = type[0];
					if (c != 'b')
					{
						if (c != 'd')
						{
							if (c != 's')
							{
								goto IL_031B;
							}
							if (!(type == "string"))
							{
								goto IL_031B;
							}
							goto IL_030B;
						}
						else if (!(type == "double"))
						{
							goto IL_031B;
						}
					}
					else
					{
						if (!(type == "base64"))
						{
							goto IL_031B;
						}
						return TypeValue.Text;
					}
					break;
				}
				case 7:
				{
					char c = type[0];
					if (c != 'a')
					{
						if (c != 'b')
						{
							if (c != 'p')
							{
								goto IL_031B;
							}
							if (!(type == "percent"))
							{
								goto IL_031B;
							}
						}
						else
						{
							if (!(type == "boolean"))
							{
								goto IL_031B;
							}
							return TypeValue.Logical;
						}
					}
					else
					{
						if (!(type == "anyType"))
						{
							goto IL_031B;
						}
						goto IL_031B;
					}
					break;
				}
				case 8:
				{
					char c = type[3];
					if (c <= 'k')
					{
						switch (c)
						{
						case 'a':
							if (!(type == "location"))
							{
								goto IL_031B;
							}
							return TypeValue.Record;
						case 'b':
							if (!(type == "combobox"))
							{
								goto IL_031B;
							}
							goto IL_030B;
						case 'c':
						case 'd':
							goto IL_031B;
						case 'e':
							if (!(type == "datetime"))
							{
								goto IL_031B;
							}
							return reportContext ? TypeValue.Text : TypeValue.DateTime;
						default:
							if (c != 'k')
							{
								goto IL_031B;
							}
							if (!(type == "picklist"))
							{
								goto IL_031B;
							}
							goto IL_030B;
						}
					}
					else if (c != 'r')
					{
						if (c != 't')
						{
							goto IL_031B;
						}
						if (!(type == "textarea"))
						{
							goto IL_031B;
						}
						goto IL_030B;
					}
					else
					{
						if (!(type == "currency"))
						{
							goto IL_031B;
						}
						return reportContext ? TypeValue.Record : TypeValue.Number;
					}
					break;
				}
				case 9:
					if (!(type == "reference"))
					{
						goto IL_031B;
					}
					goto IL_0303;
				case 10:
				case 11:
				case 12:
					goto IL_031B;
				case 13:
					if (!(type == "multipicklist"))
					{
						goto IL_031B;
					}
					return TypeValue.Any;
				default:
					goto IL_031B;
				}
				return TypeValue.Number;
				IL_0303:
				return TypeValue.Text;
				IL_030B:
				return TypeValue.Text;
			}
			IL_031B:
			return TypeValue.Any;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00018840 File Offset: 0x00016A40
		public static Func<Value, Value> MakeConverter(TypeValue type)
		{
			type = TypeServices.StripNullableAndMetadata(type);
			if (type.TypeKind == ValueKind.Date)
			{
				return new Func<Value, Value>(SalesforceTypes.ConvertDateValue);
			}
			if (type.TypeKind == ValueKind.DateTime)
			{
				return new Func<Value, Value>(SalesforceTypes.ConvertDateTimeValue);
			}
			if (type.NonNullable.Equals(TypeValue.Int32))
			{
				return delegate(Value value)
				{
					if (!value.IsNull)
					{
						return NumberValue.New(value.AsInteger32);
					}
					return value;
				};
			}
			return (Value value) => value;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000188D4 File Offset: 0x00016AD4
		public static Func<JsonTokenizer, IValueReference> MakeJsonConverter(TypeValue type)
		{
			type = TypeServices.StripNullableAndMetadata(type);
			switch (type.TypeKind)
			{
			case ValueKind.Date:
				return new Func<JsonTokenizer, IValueReference>(SalesforceTypes.FetchJsonDate);
			case ValueKind.DateTime:
				return new Func<JsonTokenizer, IValueReference>(SalesforceTypes.FetchJsonDateTime);
			case ValueKind.Number:
				if (type.Equals(TypeValue.Int32))
				{
					return new Func<JsonTokenizer, IValueReference>(SalesforceTypes.FetchJsonInt32);
				}
				return new Func<JsonTokenizer, IValueReference>(SalesforceTypes.FetchJsonDouble);
			case ValueKind.Logical:
				return new Func<JsonTokenizer, IValueReference>(SalesforceTypes.FetchJsonLogical);
			case ValueKind.Text:
				return new Func<JsonTokenizer, IValueReference>(SalesforceTypes.FetchJsonText);
			}
			return (JsonTokenizer t) => t.ReadValue();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00018990 File Offset: 0x00016B90
		public static bool IsScalar(TypeValue type)
		{
			return type.TypeKind != ValueKind.Record && type.TypeKind != ValueKind.Table;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000189AB File Offset: 0x00016BAB
		public static bool AreCompatible(TypeValue type1, TypeValue type2)
		{
			return type1.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Null || type1.TypeKind == type2.TypeKind;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000189D0 File Offset: 0x00016BD0
		private static IValueReference FetchJsonDate(JsonTokenizer tokenizer)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken == JsonToken.Null)
			{
				return Value.Null;
			}
			if (nextToken != JsonToken.String)
			{
				throw SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Date_CannotParseTextAsDateTimeError);
			}
			DateValue dateValue;
			if (!DateValue.TryParseFromText(tokenizer.GetTokenText(), CultureInfo.InvariantCulture, out dateValue))
			{
				return new ExceptionValueReference(SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Date_CannotParseTextAsDateTimeError));
			}
			return dateValue;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00018A39 File Offset: 0x00016C39
		private static Value ConvertDateValue(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return SalesforceTypes.ConvertDate(value.AsText.AsString);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00018A5C File Offset: 0x00016C5C
		private static Value ConvertDate(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return Value.Null;
			}
			DateValue dateValue;
			if (!DateValue.TryParseFromText(value, CultureInfo.InvariantCulture, out dateValue))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_CannotParseTextAsDateTimeError, TextValue.New(value), null);
			}
			return dateValue;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00018A9C File Offset: 0x00016C9C
		private static IValueReference FetchJsonDateTime(JsonTokenizer tokenizer)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken == JsonToken.Null)
			{
				return Value.Null;
			}
			if (nextToken != JsonToken.String)
			{
				throw SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Date_CannotParseTextAsDateTimeError);
			}
			DateTimeZoneValue dateTimeZoneValue;
			if (!DateTimeZoneValue.TryParseFromText(tokenizer.GetTokenText(), CultureInfo.InvariantCulture, out dateTimeZoneValue))
			{
				return new ExceptionValueReference(SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Date_CannotParseTextAsDateTimeError));
			}
			return DateTimeValue.New(dateTimeZoneValue.AsClrDateTimeOffset.Ticks);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00018B17 File Offset: 0x00016D17
		private static Value ConvertDateTimeValue(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return SalesforceTypes.ConvertDateTime(value.AsText.AsString);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00018B38 File Offset: 0x00016D38
		private static Value ConvertDateTime(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return Value.Null;
			}
			DateTimeZoneValue dateTimeZoneValue;
			if (!DateTimeZoneValue.TryParseFromText(value, CultureInfo.InvariantCulture, out dateTimeZoneValue))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_CannotParseTextAsDateTimeError, TextValue.New(value), null);
			}
			return DateTimeValue.New(dateTimeZoneValue.AsClrDateTimeOffset.Ticks);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00018B88 File Offset: 0x00016D88
		private static IValueReference FetchJsonInt32(JsonTokenizer tokenizer)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken == JsonToken.Null)
			{
				return Value.Null;
			}
			if (nextToken != JsonToken.Number)
			{
				throw SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Number_FromFunction_NotConvertibleToNumber);
			}
			int num;
			if (!tokenizer.TryParse(out num))
			{
				return new ExceptionValueReference(SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Number_FromFunction_NotConvertibleToNumber));
			}
			return NumberValue.New(num);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00018BF0 File Offset: 0x00016DF0
		private static IValueReference FetchJsonDouble(JsonTokenizer tokenizer)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken == JsonToken.Null)
			{
				return Value.Null;
			}
			if (nextToken != JsonToken.Number)
			{
				throw SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Number_FromFunction_NotConvertibleToNumber);
			}
			double num;
			if (!tokenizer.TryParse(out num))
			{
				return new ExceptionValueReference(SalesforceTypes.NewParseError(tokenizer.GetTokenText(), Strings.Number_FromFunction_NotConvertibleToNumber));
			}
			return NumberValue.New(num);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00018C58 File Offset: 0x00016E58
		private static Value FetchJsonLogical(JsonTokenizer tokenizer)
		{
			switch (tokenizer.GetNextToken())
			{
			case JsonToken.True:
				return LogicalValue.True;
			case JsonToken.False:
				return LogicalValue.False;
			case JsonToken.Null:
				return Value.Null;
			default:
				throw ValueException.NewDataFormatError<Message0>(Strings.Logical_NotConvertibleToLogical, TextValue.New(tokenizer.GetTokenText()), null);
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00018CAC File Offset: 0x00016EAC
		private static Value FetchJsonText(JsonTokenizer tokenizer)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken == JsonToken.Null)
			{
				return Value.Null;
			}
			if (nextToken == JsonToken.String)
			{
				return TextValue.New(tokenizer.GetTokenText());
			}
			throw ValueException.NewDataFormatError<Message0>(Strings.ValueException_CastTypeMismatch, TextValue.New(tokenizer.GetTokenText()), null);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00018CF4 File Offset: 0x00016EF4
		private static ValueException NewParseError(string token, string message)
		{
			TextValue textValue = TextValue.New(token);
			return ValueException.NewDataFormatError(message, textValue, null);
		}
	}
}
