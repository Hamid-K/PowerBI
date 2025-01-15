using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.OData.Buffers;

namespace Microsoft.OData.Json
{
	// Token: 0x02000217 RID: 535
	[DebuggerDisplay("{NodeType}: {Value}")]
	internal class JsonReader : IJsonStreamReader, IJsonReader, IDisposable
	{
		// Token: 0x06001758 RID: 5976 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		public JsonReader(TextReader reader, bool isIeee754Compatible)
		{
			this.nodeType = JsonNodeType.None;
			this.nodeValue = null;
			this.reader = reader;
			this.storedCharacterCount = 0;
			this.tokenStartIndex = 0;
			this.endOfInputReached = false;
			this.isIeee754Compatible = isIeee754Compatible;
			this.allowAnnotations = true;
			this.scopes = new Stack<JsonReader.Scope>();
			this.scopes.Push(new JsonReader.Scope(JsonReader.ScopeType.Root));
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00041C23 File Offset: 0x0003FE23
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x00041C2B File Offset: 0x0003FE2B
		public ICharArrayPool ArrayPool { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00041C34 File Offset: 0x0003FE34
		public virtual object Value
		{
			get
			{
				if (this.readingStream)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_CannotAccessValueInStreamState);
				}
				if (this.canStream)
				{
					if (this.nodeType != JsonNodeType.Property)
					{
						this.canStream = false;
					}
					if (this.nodeType == JsonNodeType.PrimitiveValue)
					{
						if (this.characterBuffer[this.tokenStartIndex] == 'n')
						{
							this.nodeValue = this.ParseNullPrimitiveValue();
						}
						else
						{
							bool flag;
							this.nodeValue = this.ParseStringPrimitiveValue(out flag);
						}
					}
				}
				return this.nodeValue;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00041CA8 File Offset: 0x0003FEA8
		public virtual JsonNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x00041CB0 File Offset: 0x0003FEB0
		public virtual bool IsIeee754Compatible
		{
			get
			{
				return this.isIeee754Compatible;
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		public bool CanStream()
		{
			return this.canStream;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00041CC0 File Offset: 0x0003FEC0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Not really feasible to extract code to methods without introducing unnecessary complexity.")]
		public virtual bool Read()
		{
			if (this.readingStream)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_CannotCallReadInStreamState);
			}
			if (this.canStream)
			{
				this.canStream = false;
				if (this.nodeType == JsonNodeType.PrimitiveValue)
				{
					if (this.characterBuffer[this.tokenStartIndex] == 'n')
					{
						this.ParseNullPrimitiveValue();
					}
					else
					{
						this.ParseStringPrimitiveValue();
					}
				}
			}
			this.nodeValue = null;
			if (!this.SkipWhitespaces())
			{
				return this.EndOfInput();
			}
			JsonReader.Scope scope = this.scopes.Peek();
			bool flag = false;
			if (this.characterBuffer[this.tokenStartIndex] == ',')
			{
				flag = true;
				this.tokenStartIndex++;
				if (!this.SkipWhitespaces())
				{
					return this.EndOfInput();
				}
			}
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
				this.nodeType = this.ParseValue();
				break;
			case JsonReader.ScopeType.Array:
				if (flag && scope.ValueCount == 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Array));
				}
				if (this.characterBuffer[this.tokenStartIndex] == ']')
				{
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
					this.nodeType = this.ParseValue();
				}
				break;
			case JsonReader.ScopeType.Object:
				if (flag && scope.ValueCount == 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Object));
				}
				if (this.characterBuffer[this.tokenStartIndex] == '}')
				{
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
					this.nodeType = this.ParseProperty();
				}
				break;
			case JsonReader.ScopeType.Property:
				if (flag)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Property));
				}
				this.nodeType = this.ParseValue();
				break;
			default:
				throw JsonReaderExtensions.CreateException(Strings.General_InternalError(InternalErrorCodes.JsonReader_Read));
			}
			return true;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00041F18 File Offset: 0x00040118
		public Stream CreateReadStream()
		{
			if (!this.canStream)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_CannotCreateReadStream);
			}
			this.canStream = false;
			if ((this.streamOpeningQuoteCharacter = this.characterBuffer[this.tokenStartIndex]) == 'n')
			{
				this.ParseNullPrimitiveValue();
				JsonReader.Scope scope = this.scopes.Peek();
				int valueCount = scope.ValueCount;
				scope.ValueCount = valueCount + 1;
				this.Read();
				return new ODataBinaryStreamReader((char[] a, int b, int c) => 0);
			}
			this.tokenStartIndex++;
			this.readingStream = true;
			return new ODataBinaryStreamReader(new Func<char[], int, int, int>(this.ReadChars));
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00041FCC File Offset: 0x000401CC
		public TextReader CreateTextReader()
		{
			if (!this.canStream)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_CannotCreateTextReader);
			}
			this.canStream = false;
			this.SkipWhitespaces();
			if ((this.streamOpeningQuoteCharacter = this.characterBuffer[this.tokenStartIndex]) == 'n')
			{
				this.ParseNullPrimitiveValue();
				JsonReader.Scope scope = this.scopes.Peek();
				int valueCount = scope.ValueCount;
				scope.ValueCount = valueCount + 1;
				this.Read();
				return new ODataTextStreamReader((char[] a, int b, int c) => 0);
			}
			if (this.NodeType == JsonNodeType.PrimitiveValue)
			{
				this.tokenStartIndex++;
			}
			this.readingStream = true;
			return new ODataTextStreamReader(new Func<char[], int, int, int>(this.ReadChars));
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00042090 File Offset: 0x00040290
		public void Dispose()
		{
			if (this.ArrayPool != null && this.characterBuffer != null)
			{
				BufferUtils.ReturnToBuffer(this.ArrayPool, this.characterBuffer);
				this.characterBuffer = null;
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000420BA File Offset: 0x000402BA
		private static bool IsWhitespaceCharacter(char character)
		{
			return character <= ' ' && (character == ' ' || character == '\t' || character == '\n' || character == '\r');
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000420D8 File Offset: 0x000402D8
		private JsonNodeType ParseValue()
		{
			JsonReader.Scope scope = this.scopes.Peek();
			int valueCount = scope.ValueCount;
			scope.ValueCount = valueCount + 1;
			char c = this.characterBuffer[this.tokenStartIndex];
			if (c > '[')
			{
				if (c <= 'n')
				{
					if (c != 'f')
					{
						if (c != 'n')
						{
							goto IL_0102;
						}
						this.canStream = true;
						goto IL_012D;
					}
				}
				else if (c != 't')
				{
					if (c == '{')
					{
						this.PushScope(JsonReader.ScopeType.Object);
						this.tokenStartIndex++;
						return JsonNodeType.StartObject;
					}
					goto IL_0102;
				}
				this.nodeValue = this.ParseBooleanPrimitiveValue();
				goto IL_012D;
			}
			if (c == '"' || c == '\'')
			{
				this.canStream = true;
				goto IL_012D;
			}
			if (c == '[')
			{
				this.PushScope(JsonReader.ScopeType.Array);
				this.tokenStartIndex++;
				this.SkipWhitespaces();
				this.canStream = this.characterBuffer[this.tokenStartIndex] == '"' || this.characterBuffer[this.tokenStartIndex] == '\'' || this.characterBuffer[this.tokenStartIndex] == 'n';
				return JsonNodeType.StartArray;
			}
			IL_0102:
			if (!char.IsDigit(c) && c != '-' && c != '.')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedToken);
			}
			this.nodeValue = this.ParseNumberPrimitiveValue();
			IL_012D:
			this.TryPopPropertyScope();
			return JsonNodeType.PrimitiveValue;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0004221C File Offset: 0x0004041C
		private JsonNodeType ParseProperty()
		{
			JsonReader.Scope scope = this.scopes.Peek();
			int valueCount = scope.ValueCount;
			scope.ValueCount = valueCount + 1;
			this.PushScope(JsonReader.ScopeType.Property);
			this.nodeValue = this.ParseName();
			if (string.IsNullOrEmpty((string)this.nodeValue))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidPropertyNameOrUnexpectedComma((string)this.nodeValue));
			}
			if (!this.SkipWhitespaces() || this.characterBuffer[this.tokenStartIndex] != ':')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingColon((string)this.nodeValue));
			}
			this.tokenStartIndex++;
			this.SkipWhitespaces();
			this.canStream = this.characterBuffer[this.tokenStartIndex] == '{' || this.characterBuffer[this.tokenStartIndex] == '[';
			return JsonNodeType.Property;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000422F0 File Offset: 0x000404F0
		private string ParseStringPrimitiveValue()
		{
			bool flag;
			return this.ParseStringPrimitiveValue(out flag);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00042308 File Offset: 0x00040508
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Splitting the function would make it hard to understand.")]
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
					if (c2 <= '\\')
					{
						if (c2 <= '\'')
						{
							if (c2 != '"' && c2 != '\'')
							{
								goto IL_01D2;
							}
						}
						else if (c2 != '/' && c2 != '\\')
						{
							goto IL_01D2;
						}
						stringBuilder.Append(c2);
						continue;
					}
					if (c2 <= 'f')
					{
						if (c2 == 'b')
						{
							stringBuilder.Append('\b');
							continue;
						}
						if (c2 == 'f')
						{
							stringBuilder.Append('\f');
							continue;
						}
					}
					else
					{
						if (c2 == 'n')
						{
							stringBuilder.Append('\n');
							continue;
						}
						switch (c2)
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
							if (!int.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\u" + text));
							}
							stringBuilder.Append((char)num2);
							continue;
						}
						}
					}
					IL_01D2:
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\" + c2.ToString()));
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

		// Token: 0x06001768 RID: 5992 RVA: 0x00042564 File Offset: 0x00040764
		private object ParseNullPrimitiveValue()
		{
			string text = this.ParseName();
			if (!string.Equals(text, "null", StringComparison.Ordinal))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
			}
			return null;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00042594 File Offset: 0x00040794
		private object ParseBooleanPrimitiveValue()
		{
			string text = this.ParseName();
			if (string.Equals(text, "false", StringComparison.Ordinal))
			{
				return false;
			}
			if (string.Equals(text, "true", StringComparison.Ordinal))
			{
				return true;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000425E0 File Offset: 0x000407E0
		private object ParseNumberPrimitiveValue()
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
			int num2;
			if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
			{
				return num2;
			}
			decimal num3;
			if (!this.isIeee754Compatible && decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num3))
			{
				return num3;
			}
			double num4;
			if (double.TryParse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num4))
			{
				return num4;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidNumberFormat(text));
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000426AC File Offset: 0x000408AC
		private string ParseName()
		{
			char c = this.characterBuffer[this.tokenStartIndex];
			if (c == '"' || c == '\'')
			{
				return this.ParseStringPrimitiveValue();
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
			return this.ConsumeTokenToString(num);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00042734 File Offset: 0x00040934
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Splitting the function would make it hard to understand.")]
		private int ReadChars(char[] chars, int offset, int maxLength)
		{
			if (!this.readingStream)
			{
				return 0;
			}
			int num = 0;
			while (num < maxLength && (this.tokenStartIndex < this.storedCharacterCount || this.ReadInput()))
			{
				char c = this.characterBuffer[this.tokenStartIndex];
				bool flag = true;
				if (c == JsonReader.GetClosingQuoteCharacter(this.streamOpeningQuoteCharacter))
				{
					int num2 = this.balancedQuoteCount - 1;
					this.balancedQuoteCount = num2;
					if (num2 < 1)
					{
						if (c != '"')
						{
							chars[num + offset] = c;
							num++;
							this.scopes.Pop();
						}
						this.tokenStartIndex++;
						this.readingStream = false;
						JsonReader.Scope scope = this.scopes.Peek();
						num2 = scope.ValueCount;
						scope.ValueCount = num2 + 1;
						this.Read();
						return num;
					}
				}
				if (c == this.streamOpeningQuoteCharacter)
				{
					this.balancedQuoteCount++;
				}
				if (c == '\\')
				{
					this.tokenStartIndex++;
					if (!this.EnsureAvailableCharacters(1))
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\uXXXX"));
					}
					c = this.characterBuffer[this.tokenStartIndex];
					if (c <= '\\')
					{
						if (c <= '\'')
						{
							if (c == '"' || c == '\'')
							{
								goto IL_01FC;
							}
						}
						else if (c == '/' || c == '\\')
						{
							goto IL_01FC;
						}
					}
					else if (c <= 'f')
					{
						if (c == 'b')
						{
							c = '\b';
							goto IL_01FC;
						}
						if (c == 'f')
						{
							c = '\f';
							goto IL_01FC;
						}
					}
					else
					{
						if (c == 'n')
						{
							c = '\n';
							goto IL_01FC;
						}
						switch (c)
						{
						case 'r':
							c = '\r';
							goto IL_01FC;
						case 't':
							c = '\t';
							goto IL_01FC;
						case 'u':
						{
							this.tokenStartIndex++;
							if (!this.EnsureAvailableCharacters(4))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\uXXXX"));
							}
							string text = this.ConsumeTokenToString(4);
							int num3;
							if (!int.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\u" + text));
							}
							c = (char)num3;
							flag = false;
							goto IL_01FC;
						}
						}
					}
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\" + c.ToString()));
				}
				IL_01FC:
				chars[num + offset] = c;
				num++;
				if (flag)
				{
					this.tokenStartIndex++;
				}
			}
			if (num < maxLength)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedEndOfString);
			}
			return num;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00042988 File Offset: 0x00040B88
		private static char GetClosingQuoteCharacter(char openingCharacter)
		{
			if (openingCharacter == '[')
			{
				return ']';
			}
			if (openingCharacter == '{')
			{
				return '}';
			}
			return openingCharacter;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0004299C File Offset: 0x00040B9C
		private bool EndOfInput()
		{
			if (this.scopes.Count > 1)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
			}
			this.nodeType = JsonNodeType.EndOfInput;
			if (this.ArrayPool != null)
			{
				BufferUtils.ReturnToBuffer(this.ArrayPool, this.characterBuffer);
				this.characterBuffer = null;
			}
			return false;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000429EA File Offset: 0x00040BEA
		private void PushScope(JsonReader.ScopeType newScopeType)
		{
			this.scopes.Push(new JsonReader.Scope(newScopeType));
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000429FD File Offset: 0x00040BFD
		private void PopScope()
		{
			this.scopes.Pop();
			this.TryPopPropertyScope();
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00042A11 File Offset: 0x00040C11
		private void TryPopPropertyScope()
		{
			if (this.scopes.Peek().Type == JsonReader.ScopeType.Property)
			{
				this.scopes.Pop();
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00042A32 File Offset: 0x00040C32
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

		// Token: 0x06001773 RID: 6003 RVA: 0x00042A71 File Offset: 0x00040C71
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

		// Token: 0x06001774 RID: 6004 RVA: 0x00042A90 File Offset: 0x00040C90
		private string ConsumeTokenToString(int characterCount)
		{
			string text = new string(this.characterBuffer, this.tokenStartIndex, characterCount);
			this.tokenStartIndex += characterCount;
			return text;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00042AC0 File Offset: 0x00040CC0
		private bool ReadInput()
		{
			if (this.endOfInputReached)
			{
				return false;
			}
			if (this.characterBuffer == null)
			{
				this.characterBuffer = BufferUtils.RentFromBuffer(this.ArrayPool, 2040);
			}
			if (this.tokenStartIndex == this.storedCharacterCount)
			{
				this.tokenStartIndex = 0;
				this.storedCharacterCount = 0;
			}
			else if (this.storedCharacterCount == this.characterBuffer.Length)
			{
				if (this.tokenStartIndex < this.characterBuffer.Length / 4)
				{
					if (this.characterBuffer.Length == 2147483647)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_MaxBufferReached);
					}
					int num = this.characterBuffer.Length * 2;
					num = ((num < 0) ? int.MaxValue : num);
					char[] array = BufferUtils.RentFromBuffer(this.ArrayPool, num);
					Array.Copy(this.characterBuffer, this.tokenStartIndex, array, 0, this.storedCharacterCount - this.tokenStartIndex);
					this.storedCharacterCount -= this.tokenStartIndex;
					this.tokenStartIndex = 0;
					BufferUtils.ReturnToBuffer(this.ArrayPool, this.characterBuffer);
					this.characterBuffer = array;
				}
				else
				{
					Array.Copy(this.characterBuffer, this.tokenStartIndex, this.characterBuffer, 0, this.storedCharacterCount - this.tokenStartIndex);
					this.storedCharacterCount -= this.tokenStartIndex;
					this.tokenStartIndex = 0;
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

		// Token: 0x04000A91 RID: 2705
		private const int InitialCharacterBufferSize = 2040;

		// Token: 0x04000A92 RID: 2706
		private readonly TextReader reader;

		// Token: 0x04000A93 RID: 2707
		private readonly bool isIeee754Compatible;

		// Token: 0x04000A94 RID: 2708
		private readonly Stack<JsonReader.Scope> scopes;

		// Token: 0x04000A95 RID: 2709
		private readonly bool allowAnnotations;

		// Token: 0x04000A96 RID: 2710
		private bool endOfInputReached;

		// Token: 0x04000A97 RID: 2711
		private bool readingStream;

		// Token: 0x04000A98 RID: 2712
		private bool canStream;

		// Token: 0x04000A99 RID: 2713
		private char streamOpeningQuoteCharacter = '"';

		// Token: 0x04000A9A RID: 2714
		private char[] characterBuffer;

		// Token: 0x04000A9B RID: 2715
		private int storedCharacterCount;

		// Token: 0x04000A9C RID: 2716
		private int tokenStartIndex;

		// Token: 0x04000A9D RID: 2717
		private int balancedQuoteCount;

		// Token: 0x04000A9E RID: 2718
		private JsonNodeType nodeType;

		// Token: 0x04000A9F RID: 2719
		private object nodeValue;

		// Token: 0x04000AA0 RID: 2720
		private StringBuilder stringValueBuilder;

		// Token: 0x020003DB RID: 987
		private enum ScopeType
		{
			// Token: 0x04000F5C RID: 3932
			Root,
			// Token: 0x04000F5D RID: 3933
			Array,
			// Token: 0x04000F5E RID: 3934
			Object,
			// Token: 0x04000F5F RID: 3935
			Property
		}

		// Token: 0x020003DC RID: 988
		private sealed class Scope
		{
			// Token: 0x060020B8 RID: 8376 RVA: 0x0005C21E File Offset: 0x0005A41E
			public Scope(JsonReader.ScopeType type)
			{
				this.type = type;
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0005C22D File Offset: 0x0005A42D
			// (set) Token: 0x060020BA RID: 8378 RVA: 0x0005C235 File Offset: 0x0005A435
			public int ValueCount { get; set; }

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x060020BB RID: 8379 RVA: 0x0005C23E File Offset: 0x0005A43E
			public JsonReader.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000F60 RID: 3936
			private readonly JsonReader.ScopeType type;
		}
	}
}
