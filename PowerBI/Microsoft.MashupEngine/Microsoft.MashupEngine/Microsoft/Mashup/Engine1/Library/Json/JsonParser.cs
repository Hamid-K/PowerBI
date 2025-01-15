using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Json
{
	// Token: 0x02000A41 RID: 2625
	internal static class JsonParser
	{
		// Token: 0x060048F6 RID: 18678 RVA: 0x000F3BE8 File Offset: 0x000F1DE8
		public static Value Parse(TextReader reader, Value value = null)
		{
			JsonTokenizer jsonTokenizer;
			using (EngineContext.Leave())
			{
				reader = reader.LeaveEngineContext<TextReader>();
				jsonTokenizer = new JsonTokenizer(reader, false, false, value);
			}
			return jsonTokenizer.Parse();
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x000F3C34 File Offset: 0x000F1E34
		public static Value Parse(this JsonTokenizer tokenizer)
		{
			Value value2;
			using (EngineContext.Leave())
			{
				Value value = tokenizer.ReadValue();
				if (!tokenizer.IsAtEnd())
				{
					throw JsonParser.UnexpectedCharacterException(Strings.JsonExtraCharactersAtTheEnd, tokenizer);
				}
				value2 = value;
			}
			return value2;
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x000F3C88 File Offset: 0x000F1E88
		public static bool TryParse(TextReader reader, out Value value)
		{
			bool flag;
			try
			{
				value = JsonParser.Parse(reader, null);
				flag = true;
			}
			catch (ValueException)
			{
				value = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x000F3CBC File Offset: 0x000F1EBC
		public static Value ReadValue(this JsonTokenizer tokenizer)
		{
			return JsonParser.ReadValue(tokenizer, null);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000F3CD8 File Offset: 0x000F1ED8
		public static void SkipValue(this JsonTokenizer tokenizer)
		{
			int num = 0;
			for (;;)
			{
				switch (tokenizer.GetNextToken())
				{
				case JsonToken.End:
					goto IL_0035;
				case JsonToken.RecordStart:
				case JsonToken.ListStart:
					num++;
					break;
				case JsonToken.RecordEnd:
				case JsonToken.ListEnd:
					num--;
					break;
				}
				if (num <= 0)
				{
					return;
				}
			}
			IL_0035:
			throw JsonParser.CreateInvalidFormatException(Strings.UnexpectedEndOfBuffer, Value.Null);
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x000F3D34 File Offset: 0x000F1F34
		public static void ReadRecordValues<T>(this JsonTokenizer tokenizer, T @object, Dictionary<string, Action<JsonTokenizer, T>> keys)
		{
			if (tokenizer.GetNextToken() != JsonToken.RecordStart)
			{
				throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidRecordFormatError, tokenizer);
			}
			JsonToken nextToken;
			while ((nextToken = tokenizer.GetNextToken()) == JsonToken.RecordKey)
			{
				string tokenText = tokenizer.GetTokenText();
				Action<JsonTokenizer, T> action;
				if (keys.TryGetValue(tokenText, out action))
				{
					action(tokenizer, @object);
				}
				else
				{
					tokenizer.SkipValue();
				}
			}
			if (nextToken != JsonToken.RecordEnd)
			{
				throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidRecordFormatError, tokenizer);
			}
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x000F3DA0 File Offset: 0x000F1FA0
		public static void ReadListValues<T>(this JsonTokenizer tokenizer, List<T> list, Func<JsonTokenizer, T> ctor)
		{
			if (tokenizer.GetNextToken() != JsonToken.ListStart)
			{
				throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidListFormatError, tokenizer);
			}
			while (tokenizer.PeekNextToken() != JsonToken.ListEnd)
			{
				T t = ctor(tokenizer);
				list.Add(t);
			}
			tokenizer.GetNextToken();
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x000F3DE8 File Offset: 0x000F1FE8
		public static bool ReadBoolean(this JsonTokenizer tokenizer)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken == JsonToken.True)
			{
				return true;
			}
			if (nextToken != JsonToken.False)
			{
				throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidBooleanError, tokenizer);
			}
			return false;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x000F3E1C File Offset: 0x000F201C
		public static int ReadInt32(this JsonTokenizer tokenizer)
		{
			if (tokenizer.GetNextToken() != JsonToken.Number)
			{
				throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidNumberError, tokenizer);
			}
			int num;
			if (tokenizer.TryParse(out num))
			{
				return num;
			}
			TextValue textValue = TextValue.New(tokenizer.GetTokenText());
			throw ValueException.NewDataFormatError<Message0>(Strings.Number_FromFunction_NotConvertibleToNumber, textValue, null);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x000F3E68 File Offset: 0x000F2068
		public static string ReadString(this JsonTokenizer tokenizer, bool allowNull = false)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			if (nextToken != JsonToken.String)
			{
				if (nextToken == JsonToken.Null)
				{
					if (allowNull)
					{
						return null;
					}
				}
				throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidJsonTextError, tokenizer);
			}
			return tokenizer.GetTokenText();
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x000F3EA4 File Offset: 0x000F20A4
		private static Value ReadValue(JsonTokenizer tokenizer, JsonToken? compositeEnd)
		{
			JsonToken nextToken = tokenizer.GetNextToken();
			switch (nextToken)
			{
			case JsonToken.End:
				throw JsonParser.UnexpectedCharacterException(Strings.UnexpectedEndOfBuffer, 0, tokenizer.GetPosition(), null, tokenizer.Value);
			case JsonToken.RecordStart:
				return JsonParser.ReadRecordValue(tokenizer);
			case JsonToken.ListStart:
				return JsonParser.ReadArrayValue(tokenizer);
			case JsonToken.String:
				return TextValue.New(tokenizer.GetTokenText());
			case JsonToken.True:
				return LogicalValue.True;
			case JsonToken.False:
				return LogicalValue.False;
			case JsonToken.Null:
				return Value.Null;
			case JsonToken.Number:
			{
				NumberValue numberValue;
				if (!tokenizer.TryParse(out numberValue))
				{
					throw JsonParser.UnexpectedCharacterException(Strings.JsonInvalidNumberError, tokenizer);
				}
				return numberValue;
			}
			}
			JsonToken? jsonToken = compositeEnd;
			JsonToken jsonToken2 = nextToken;
			if ((jsonToken.GetValueOrDefault() == jsonToken2) & (jsonToken != null))
			{
				return null;
			}
			throw JsonParser.UnexpectedCharacterException(tokenizer.GetLastErrorMessage() ?? Strings.JsonUnexpectedToken, (int)tokenizer.GetCharacter(), tokenizer.GetPosition(), tokenizer.GetLastErrorToken(), tokenizer.Value);
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x000F3FA0 File Offset: 0x000F21A0
		private static ListValue ReadArrayValue(JsonTokenizer tokenizer)
		{
			List<Value> list = new List<Value>();
			Value value;
			while ((value = JsonParser.ReadValue(tokenizer, new JsonToken?(JsonToken.ListEnd))) != null)
			{
				list.Add(value);
			}
			return ListValue.New(list.ToArray());
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x000F3FD8 File Offset: 0x000F21D8
		private static RecordValue ReadRecordValue(JsonTokenizer tokenizer)
		{
			List<string> list = new List<string>();
			List<Value> list2 = new List<Value>();
			HashSet<string> hashSet = new HashSet<string>();
			JsonToken nextToken;
			while ((nextToken = tokenizer.GetNextToken()) == JsonToken.RecordKey)
			{
				string tokenText = tokenizer.GetTokenText();
				if (!hashSet.Add(tokenText))
				{
					throw JsonParser.CreateInvalidFormatException(Strings.JsonDuplicateNameError(tokenText), Value.Null);
				}
				list.Add(tokenText);
				list2.Add(JsonParser.ReadValue(tokenizer, null));
			}
			if (nextToken != JsonToken.RecordEnd)
			{
				int num = (int)((nextToken == JsonToken.End) ? '\0' : tokenizer.GetCharacter());
				throw JsonParser.UnexpectedCharacterException(Strings.JsonExtraCharactersAtTheEnd, num, tokenizer.GetPosition(), null, tokenizer.Value);
			}
			return RecordValue.New(Keys.New(list.ToArray()), list2.ToArray());
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x000F4092 File Offset: 0x000F2292
		private static ValueException UnexpectedCharacterException(string errorMessage, JsonTokenizer tokenizer)
		{
			return JsonParser.UnexpectedCharacterException(errorMessage, (int)tokenizer.GetCharacter(), tokenizer.GetPosition(), null, tokenizer.Value);
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x000F40B0 File Offset: 0x000F22B0
		private static ValueException UnexpectedCharacterException(string errorMessage, int actualCh, int position, string errorToken = null, Value value = null)
		{
			if (actualCh == 0)
			{
				return JsonParser.CreateInvalidFormatException(Strings.UnexpectedEndOfBuffer, Value.Null);
			}
			if (position == 0 && value != null)
			{
				DataSource.EnsureNotHtmlResponse(value);
			}
			string text;
			if (!char.IsControl((char)actualCh))
			{
				text = (string.IsNullOrEmpty(errorToken) ? ((char)actualCh).ToString() : errorToken);
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "\\{0}", actualCh);
			}
			Keys keys = Keys.New("Value", "Position");
			Value[] array = new Value[]
			{
				TextValue.New(text),
				NumberValue.New(position)
			};
			RecordValue recordValue = RecordValue.New(keys, array);
			return JsonParser.CreateInvalidFormatException(errorMessage, recordValue);
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x000F4153 File Offset: 0x000F2353
		private static ValueException CreateInvalidFormatException(string message, Value value)
		{
			return ValueException.NewDataFormatError(message, value, null);
		}
	}
}
