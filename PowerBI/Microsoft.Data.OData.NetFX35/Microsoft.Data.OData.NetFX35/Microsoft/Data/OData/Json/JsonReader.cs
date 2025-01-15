using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x0200016E RID: 366
	[DebuggerDisplay("{NodeType}: {Value}")]
	internal class JsonReader
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x00020A78 File Offset: 0x0001EC78
		public JsonReader(TextReader reader, ODataFormat jsonFormat)
		{
			this.nodeType = JsonNodeType.None;
			this.nodeValue = null;
			this.reader = reader;
			this.characterBuffer = new char[2040];
			this.storedCharacterCount = 0;
			this.tokenStartIndex = 0;
			this.endOfInputReached = false;
			this.allowAnnotations = jsonFormat == ODataFormat.Json;
			this.supportAspNetDateTimeFormat = jsonFormat == ODataFormat.VerboseJson;
			this.scopes = new Stack<JsonReader.Scope>();
			this.scopes.Push(new JsonReader.Scope(JsonReader.ScopeType.Root));
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00020AFD File Offset: 0x0001ECFD
		public virtual object Value
		{
			get
			{
				return this.nodeValue;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00020B05 File Offset: 0x0001ED05
		public virtual JsonNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00020B0D File Offset: 0x0001ED0D
		public virtual string RawValue
		{
			get
			{
				return this.nodeRawValue;
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00020B18 File Offset: 0x0001ED18
		public virtual bool Read()
		{
			this.nodeValue = null;
			this.nodeRawValue = null;
			if (!this.SkipWhitespaces())
			{
				return this.EndOfInput();
			}
			JsonReader.Scope scope = this.scopes.Peek();
			bool flag = false;
			if (this.characterBuffer[this.tokenStartIndex] == ',')
			{
				flag = true;
				this.TryAppendJsonRawValue(this.characterBuffer[this.tokenStartIndex]);
				this.tokenStartIndex++;
				if (!this.SkipWhitespaces())
				{
					return this.EndOfInput();
				}
			}
			string text = null;
			switch (scope.Type)
			{
			case JsonReader.ScopeType.Root:
				if (flag)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Root));
				}
				if (scope.ValueCount > 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_MultipleTopLevelValues);
				}
				this.nodeType = this.ParseValue(out text);
				this.TryAppendJsonRawValue(text);
				break;
			case JsonReader.ScopeType.Array:
				if (flag && scope.ValueCount == 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Array));
				}
				if (this.characterBuffer[this.tokenStartIndex] == ']')
				{
					this.TryAppendJsonRawValue(this.characterBuffer[this.tokenStartIndex]);
					this.tokenStartIndex++;
					if (flag)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Array));
					}
					this.PopScope();
					this.nodeType = JsonNodeType.EndArray;
				}
				else
				{
					if (!flag && scope.ValueCount > 0)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingComma(JsonReader.ScopeType.Array));
					}
					this.nodeType = this.ParseValue(out text);
					this.TryAppendJsonRawValue(text);
				}
				break;
			case JsonReader.ScopeType.Object:
				if (flag && scope.ValueCount == 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Object));
				}
				if (this.characterBuffer[this.tokenStartIndex] == '}')
				{
					this.TryAppendJsonRawValue(this.characterBuffer[this.tokenStartIndex]);
					this.tokenStartIndex++;
					if (flag)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Object));
					}
					this.PopScope();
					this.nodeType = JsonNodeType.EndObject;
				}
				else
				{
					if (!flag && scope.ValueCount > 0)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingComma(JsonReader.ScopeType.Object));
					}
					this.nodeType = this.ParseProperty(out text);
					this.TryAppendJsonRawValue(text);
				}
				break;
			case JsonReader.ScopeType.Property:
				if (flag)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Property));
				}
				this.nodeType = this.ParseValue(out text);
				this.TryAppendJsonRawValue(text);
				break;
			default:
				throw JsonReaderExtensions.CreateException(Strings.General_InternalError(InternalErrorCodes.JsonReader_Read));
			}
			return true;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00020D87 File Offset: 0x0001EF87
		private void TryAppendJsonRawValue(string rawValue)
		{
			this.nodeRawValue += rawValue;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00020D9B File Offset: 0x0001EF9B
		private void TryAppendJsonRawValue(char rawValue)
		{
			this.nodeRawValue += rawValue;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		private static bool IsWhitespaceCharacter(char character)
		{
			return character <= ' ' && (character == ' ' || character == '\t' || character == '\n' || character == '\r');
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		private static object TryParseDateTimePrimitiveValue(string stringValue)
		{
			if (!stringValue.StartsWith("/Date(", 4) || !stringValue.EndsWith(")/", 4))
			{
				return null;
			}
			string text = stringValue.Substring("/Date(".Length, stringValue.Length - ("/Date(".Length + ")/".Length));
			int num = text.IndexOfAny(new char[] { '+', '-' }, 1);
			if (num == -1)
			{
				long num2;
				if (long.TryParse(text, 7, NumberFormatInfo.InvariantInfo, ref num2))
				{
					return new DateTime(JsonValueUtils.JsonTicksToDateTimeTicks(num2), 1);
				}
			}
			else
			{
				string text2 = text.Substring(num);
				text = text.Substring(0, num);
				long num3;
				int num4;
				if (long.TryParse(text, 7, NumberFormatInfo.InvariantInfo, ref num3) && int.TryParse(text2, 7, NumberFormatInfo.InvariantInfo, ref num4))
				{
					return new DateTimeOffset(JsonValueUtils.JsonTicksToDateTimeTicks(num3), new TimeSpan(0, num4, 0));
				}
			}
			return null;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		private JsonNodeType ParseValue(out string rawValue)
		{
			this.scopes.Peek().ValueCount++;
			char c = this.characterBuffer[this.tokenStartIndex];
			char c2 = c;
			if (c2 > '[')
			{
				if (c2 <= 'n')
				{
					if (c2 != 'f')
					{
						if (c2 != 'n')
						{
							goto IL_0131;
						}
						this.nodeValue = this.ParseNullPrimitiveValue(out rawValue);
						goto IL_015D;
					}
				}
				else if (c2 != 't')
				{
					if (c2 == '{')
					{
						this.PushScope(JsonReader.ScopeType.Object);
						this.tokenStartIndex++;
						rawValue = c.ToString();
						return JsonNodeType.StartObject;
					}
					goto IL_0131;
				}
				this.nodeValue = this.ParseBooleanPrimitiveValue(out rawValue);
				goto IL_015D;
			}
			if (c2 != '"' && c2 != '\'')
			{
				if (c2 == '[')
				{
					this.PushScope(JsonReader.ScopeType.Array);
					this.tokenStartIndex++;
					rawValue = c.ToString();
					return JsonNodeType.StartArray;
				}
			}
			else
			{
				bool flag;
				string text;
				rawValue = (text = this.ParseStringPrimitiveValue(out flag));
				this.nodeValue = text;
				rawValue = string.Format(CultureInfo.InvariantCulture, "{0}{1}{0}", new object[] { c, rawValue });
				if (!flag || !this.supportAspNetDateTimeFormat)
				{
					goto IL_015D;
				}
				object obj = JsonReader.TryParseDateTimePrimitiveValue((string)this.nodeValue);
				if (obj != null)
				{
					this.nodeValue = obj;
					goto IL_015D;
				}
				goto IL_015D;
			}
			IL_0131:
			if (!char.IsDigit(c) && c != '-' && c != '.')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedToken);
			}
			this.nodeValue = this.ParseNumberPrimitiveValue(out rawValue);
			IL_015D:
			this.TryPopPropertyScope();
			return JsonNodeType.PrimitiveValue;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00021034 File Offset: 0x0001F234
		private JsonNodeType ParseProperty(out string rawValue)
		{
			this.scopes.Peek().ValueCount++;
			this.PushScope(JsonReader.ScopeType.Property);
			this.nodeValue = this.ParseName(out rawValue);
			if (string.IsNullOrEmpty((string)this.nodeValue))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidPropertyNameOrUnexpectedComma((string)this.nodeValue));
			}
			if (!this.SkipWhitespaces() || this.characterBuffer[this.tokenStartIndex] != ':')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingColon((string)this.nodeValue));
			}
			rawValue += ":";
			this.tokenStartIndex++;
			return JsonNodeType.Property;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000210E4 File Offset: 0x0001F2E4
		private string ParseStringPrimitiveValue()
		{
			bool flag;
			return this.ParseStringPrimitiveValue(out flag);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000210FC File Offset: 0x0001F2FC
		private string ParseStringPrimitiveValue(out bool hasLeadingBackslash)
		{
			hasLeadingBackslash = false;
			char c = this.characterBuffer[this.tokenStartIndex];
			this.tokenStartIndex++;
			StringBuilder stringBuilder = null;
			int num = 0;
			while (this.tokenStartIndex + num < this.storedCharacterCount || this.ReadInput())
			{
				char c2 = this.characterBuffer[this.tokenStartIndex + num];
				if (c2 == '\\')
				{
					if (num == 0 && stringBuilder == null)
					{
						hasLeadingBackslash = true;
					}
					if (stringBuilder == null)
					{
						if (this.stringValueBuilder == null)
						{
							this.stringValueBuilder = new StringBuilder();
						}
						else
						{
							this.stringValueBuilder.Length = 0;
						}
						stringBuilder = this.stringValueBuilder;
					}
					stringBuilder.Append(this.ConsumeTokenToString(num));
					num = 0;
					if (!this.EnsureAvailableCharacters(2))
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\"));
					}
					c2 = this.characterBuffer[this.tokenStartIndex + 1];
					this.tokenStartIndex += 2;
					char c3 = c2;
					if (c3 <= '\\')
					{
						if (c3 <= '\'')
						{
							if (c3 != '"' && c3 != '\'')
							{
								goto IL_01E0;
							}
						}
						else if (c3 != '/' && c3 != '\\')
						{
							goto IL_01E0;
						}
						stringBuilder.Append(c2);
						continue;
					}
					if (c3 <= 'f')
					{
						if (c3 == 'b')
						{
							stringBuilder.Append('\b');
							continue;
						}
						if (c3 == 'f')
						{
							stringBuilder.Append('\f');
							continue;
						}
					}
					else
					{
						if (c3 == 'n')
						{
							stringBuilder.Append('\n');
							continue;
						}
						switch (c3)
						{
						case 'r':
							stringBuilder.Append('\r');
							continue;
						case 't':
							stringBuilder.Append('\t');
							continue;
						case 'u':
						{
							if (!this.EnsureAvailableCharacters(4))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\uXXXX"));
							}
							string text = this.ConsumeTokenToString(4);
							int num2;
							if (!int.TryParse(text, 515, CultureInfo.InvariantCulture, ref num2))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\u" + text));
							}
							stringBuilder.Append((char)num2);
							continue;
						}
						}
					}
					IL_01E0:
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\" + c2));
				}
				else
				{
					if (c2 == c)
					{
						string text2 = this.ConsumeTokenToString(num);
						this.tokenStartIndex++;
						if (stringBuilder != null)
						{
							stringBuilder.Append(text2);
							text2 = stringBuilder.ToString();
						}
						return text2;
					}
					num++;
				}
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedEndOfString);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00021364 File Offset: 0x0001F564
		private object ParseNullPrimitiveValue(out string rawValue)
		{
			string text = this.ParseName(out rawValue);
			if (!string.Equals(text, "null"))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
			}
			return null;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00021394 File Offset: 0x0001F594
		private object ParseBooleanPrimitiveValue(out string rawValue)
		{
			string text = this.ParseName(out rawValue);
			if (string.Equals(text, "false"))
			{
				return false;
			}
			if (string.Equals(text, "true"))
			{
				return true;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000213DC File Offset: 0x0001F5DC
		private object ParseNumberPrimitiveValue(out string rawValue)
		{
			int num = 1;
			while (this.tokenStartIndex + num < this.storedCharacterCount || this.ReadInput())
			{
				char c = this.characterBuffer[this.tokenStartIndex + num];
				if (!char.IsDigit(c) && c != '.' && c != 'E' && c != 'e' && c != '-' && c != '+')
				{
					break;
				}
				num++;
			}
			string text = this.ConsumeTokenToString(num);
			rawValue = text;
			int num2;
			if (int.TryParse(text, 7, NumberFormatInfo.InvariantInfo, ref num2))
			{
				return num2;
			}
			double num3;
			if (double.TryParse(text, 167, NumberFormatInfo.InvariantInfo, ref num3))
			{
				return num3;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidNumberFormat(text));
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00021484 File Offset: 0x0001F684
		private string ParseName(out string rawValue)
		{
			char c = this.characterBuffer[this.tokenStartIndex];
			if (c == '"' || c == '\'')
			{
				string text = this.ParseStringPrimitiveValue();
				rawValue = string.Format(CultureInfo.InvariantCulture, "{0}{1}{0}", new object[] { c, text });
				return text;
			}
			int num = 0;
			do
			{
				char c2 = this.characterBuffer[this.tokenStartIndex + num];
				if (c2 != '_' && !char.IsLetterOrDigit(c2) && c2 != '$' && (!this.allowAnnotations || (c2 != '.' && c2 != '@')))
				{
					break;
				}
				num++;
			}
			while (this.tokenStartIndex + num < this.storedCharacterCount || this.ReadInput());
			rawValue = this.ConsumeTokenToString(num);
			return rawValue;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00021539 File Offset: 0x0001F739
		private bool EndOfInput()
		{
			if (this.scopes.Count > 1)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
			}
			this.nodeType = JsonNodeType.EndOfInput;
			return false;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002155C File Offset: 0x0001F75C
		private void PushScope(JsonReader.ScopeType newScopeType)
		{
			this.scopes.Push(new JsonReader.Scope(newScopeType));
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002156F File Offset: 0x0001F76F
		private void PopScope()
		{
			this.scopes.Pop();
			this.TryPopPropertyScope();
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00021583 File Offset: 0x0001F783
		private void TryPopPropertyScope()
		{
			if (this.scopes.Peek().Type == JsonReader.ScopeType.Property)
			{
				this.scopes.Pop();
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000215A4 File Offset: 0x0001F7A4
		private bool SkipWhitespaces()
		{
			for (;;)
			{
				if (this.tokenStartIndex >= this.storedCharacterCount)
				{
					if (!this.ReadInput())
					{
						return false;
					}
				}
				else
				{
					if (!JsonReader.IsWhitespaceCharacter(this.characterBuffer[this.tokenStartIndex]))
					{
						break;
					}
					this.tokenStartIndex++;
				}
			}
			return true;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000215E3 File Offset: 0x0001F7E3
		private bool EnsureAvailableCharacters(int characterCountAfterTokenStart)
		{
			while (this.tokenStartIndex + characterCountAfterTokenStart > this.storedCharacterCount)
			{
				if (!this.ReadInput())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00021604 File Offset: 0x0001F804
		private string ConsumeTokenToString(int characterCount)
		{
			string text = new string(this.characterBuffer, this.tokenStartIndex, characterCount);
			this.tokenStartIndex += characterCount;
			return text;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00021634 File Offset: 0x0001F834
		private bool ReadInput()
		{
			if (this.endOfInputReached)
			{
				return false;
			}
			if (this.storedCharacterCount == this.characterBuffer.Length)
			{
				if (this.tokenStartIndex == this.storedCharacterCount)
				{
					this.tokenStartIndex = 0;
					this.storedCharacterCount = 0;
				}
				else if (this.tokenStartIndex > this.characterBuffer.Length - 64)
				{
					Array.Copy(this.characterBuffer, this.tokenStartIndex, this.characterBuffer, 0, this.storedCharacterCount - this.tokenStartIndex);
					this.storedCharacterCount -= this.tokenStartIndex;
					this.tokenStartIndex = 0;
				}
				else
				{
					int num = this.characterBuffer.Length * 2;
					char[] array = new char[num];
					Array.Copy(this.characterBuffer, 0, array, 0, this.characterBuffer.Length);
					this.characterBuffer = array;
				}
			}
			int num2 = this.reader.Read(this.characterBuffer, this.storedCharacterCount, this.characterBuffer.Length - this.storedCharacterCount);
			if (num2 == 0)
			{
				this.endOfInputReached = true;
				return false;
			}
			this.storedCharacterCount += num2;
			return true;
		}

		// Token: 0x040003C7 RID: 967
		private const int InitialCharacterBufferSize = 2040;

		// Token: 0x040003C8 RID: 968
		private const int MaxCharacterCountToMove = 64;

		// Token: 0x040003C9 RID: 969
		private const string DateTimeFormatPrefix = "/Date(";

		// Token: 0x040003CA RID: 970
		private const string DateTimeFormatSuffix = ")/";

		// Token: 0x040003CB RID: 971
		private readonly TextReader reader;

		// Token: 0x040003CC RID: 972
		private readonly Stack<JsonReader.Scope> scopes;

		// Token: 0x040003CD RID: 973
		private readonly bool allowAnnotations;

		// Token: 0x040003CE RID: 974
		private readonly bool supportAspNetDateTimeFormat;

		// Token: 0x040003CF RID: 975
		private bool endOfInputReached;

		// Token: 0x040003D0 RID: 976
		private char[] characterBuffer;

		// Token: 0x040003D1 RID: 977
		private int storedCharacterCount;

		// Token: 0x040003D2 RID: 978
		private int tokenStartIndex;

		// Token: 0x040003D3 RID: 979
		private JsonNodeType nodeType;

		// Token: 0x040003D4 RID: 980
		private object nodeValue;

		// Token: 0x040003D5 RID: 981
		private string nodeRawValue;

		// Token: 0x040003D6 RID: 982
		private StringBuilder stringValueBuilder;

		// Token: 0x0200016F RID: 367
		private enum ScopeType
		{
			// Token: 0x040003D8 RID: 984
			Root,
			// Token: 0x040003D9 RID: 985
			Array,
			// Token: 0x040003DA RID: 986
			Object,
			// Token: 0x040003DB RID: 987
			Property
		}

		// Token: 0x02000170 RID: 368
		private sealed class Scope
		{
			// Token: 0x06000A1C RID: 2588 RVA: 0x00021744 File Offset: 0x0001F944
			public Scope(JsonReader.ScopeType type)
			{
				this.type = type;
			}

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00021753 File Offset: 0x0001F953
			// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0002175B File Offset: 0x0001F95B
			public int ValueCount { get; set; }

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00021764 File Offset: 0x0001F964
			public JsonReader.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x040003DC RID: 988
			private readonly JsonReader.ScopeType type;
		}
	}
}
