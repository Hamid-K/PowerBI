using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E5 RID: 485
	[DebuggerDisplay("{NodeType}: {Value}")]
	internal class JsonReader : IJsonReader
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0003645C File Offset: 0x0003465C
		public JsonReader(TextReader reader, bool isIeee754Compatible)
		{
			this.nodeType = JsonNodeType.None;
			this.nodeValue = null;
			this.reader = reader;
			this.characterBuffer = new char[2040];
			this.storedCharacterCount = 0;
			this.tokenStartIndex = 0;
			this.endOfInputReached = false;
			this.isIeee754Compatible = isIeee754Compatible;
			this.allowAnnotations = true;
			this.scopes = new Stack<JsonReader.Scope>();
			this.scopes.Push(new JsonReader.Scope(JsonReader.ScopeType.Root));
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x000364D3 File Offset: 0x000346D3
		public virtual object Value
		{
			get
			{
				return this.nodeValue;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000364DB File Offset: 0x000346DB
		public virtual JsonNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x000364E3 File Offset: 0x000346E3
		public virtual bool IsIeee754Compatible
		{
			get
			{
				return this.isIeee754Compatible;
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000364EC File Offset: 0x000346EC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Not really feasible to extract code to methods without introducing unnecessary complexity.")]
		public virtual bool Read()
		{
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

		// Token: 0x060012F6 RID: 4854 RVA: 0x000366F5 File Offset: 0x000348F5
		private static bool IsWhitespaceCharacter(char character)
		{
			return character <= ' ' && (character == ' ' || character == '\t' || character == '\n' || character == '\r');
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00036714 File Offset: 0x00034914
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
							goto IL_00B3;
						}
						this.nodeValue = this.ParseNullPrimitiveValue();
						goto IL_00DE;
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
					goto IL_00B3;
				}
				this.nodeValue = this.ParseBooleanPrimitiveValue();
				goto IL_00DE;
			}
			if (c == '"' || c == '\'')
			{
				bool flag;
				this.nodeValue = this.ParseStringPrimitiveValue(out flag);
				goto IL_00DE;
			}
			if (c == '[')
			{
				this.PushScope(JsonReader.ScopeType.Array);
				this.tokenStartIndex++;
				return JsonNodeType.StartArray;
			}
			IL_00B3:
			if (!char.IsDigit(c) && c != '-' && c != '.')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedToken);
			}
			this.nodeValue = this.ParseNumberPrimitiveValue();
			IL_00DE:
			this.TryPopPropertyScope();
			return JsonNodeType.PrimitiveValue;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00036808 File Offset: 0x00034A08
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
			return JsonNodeType.Property;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000368A8 File Offset: 0x00034AA8
		private string ParseStringPrimitiveValue()
		{
			bool flag;
			return this.ParseStringPrimitiveValue(out flag);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000368C0 File Offset: 0x00034AC0
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
							if (!int.TryParse(text, 515, CultureInfo.InvariantCulture, ref num2))
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

		// Token: 0x060012FB RID: 4859 RVA: 0x00036B1C File Offset: 0x00034D1C
		private object ParseNullPrimitiveValue()
		{
			string text = this.ParseName();
			if (!string.Equals(text, "null"))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
			}
			return null;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00036B4C File Offset: 0x00034D4C
		private object ParseBooleanPrimitiveValue()
		{
			string text = this.ParseName();
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

		// Token: 0x060012FD RID: 4861 RVA: 0x00036B94 File Offset: 0x00034D94
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
			if (int.TryParse(text, 7, NumberFormatInfo.InvariantInfo, ref num2))
			{
				return num2;
			}
			decimal num3;
			if (!this.isIeee754Compatible && decimal.TryParse(text, 111, NumberFormatInfo.InvariantInfo, ref num3))
			{
				return num3;
			}
			double num4;
			if (double.TryParse(text, 167, NumberFormatInfo.InvariantInfo, ref num4))
			{
				return num4;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidNumberFormat(text));
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00036C60 File Offset: 0x00034E60
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

		// Token: 0x060012FF RID: 4863 RVA: 0x00036CE5 File Offset: 0x00034EE5
		private bool EndOfInput()
		{
			if (this.scopes.Count > 1)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
			}
			this.nodeType = JsonNodeType.EndOfInput;
			return false;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00036D08 File Offset: 0x00034F08
		private void PushScope(JsonReader.ScopeType newScopeType)
		{
			this.scopes.Push(new JsonReader.Scope(newScopeType));
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00036D1B File Offset: 0x00034F1B
		private void PopScope()
		{
			this.scopes.Pop();
			this.TryPopPropertyScope();
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00036D2F File Offset: 0x00034F2F
		private void TryPopPropertyScope()
		{
			if (this.scopes.Peek().Type == JsonReader.ScopeType.Property)
			{
				this.scopes.Pop();
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00036D50 File Offset: 0x00034F50
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

		// Token: 0x06001304 RID: 4868 RVA: 0x00036D8F File Offset: 0x00034F8F
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

		// Token: 0x06001305 RID: 4869 RVA: 0x00036DB0 File Offset: 0x00034FB0
		private string ConsumeTokenToString(int characterCount)
		{
			string text = new string(this.characterBuffer, this.tokenStartIndex, characterCount);
			this.tokenStartIndex += characterCount;
			return text;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00036DE0 File Offset: 0x00034FE0
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

		// Token: 0x040009B1 RID: 2481
		private const int InitialCharacterBufferSize = 2040;

		// Token: 0x040009B2 RID: 2482
		private const int MaxCharacterCountToMove = 64;

		// Token: 0x040009B3 RID: 2483
		private readonly TextReader reader;

		// Token: 0x040009B4 RID: 2484
		private readonly bool isIeee754Compatible;

		// Token: 0x040009B5 RID: 2485
		private readonly Stack<JsonReader.Scope> scopes;

		// Token: 0x040009B6 RID: 2486
		private readonly bool allowAnnotations;

		// Token: 0x040009B7 RID: 2487
		private bool endOfInputReached;

		// Token: 0x040009B8 RID: 2488
		private char[] characterBuffer;

		// Token: 0x040009B9 RID: 2489
		private int storedCharacterCount;

		// Token: 0x040009BA RID: 2490
		private int tokenStartIndex;

		// Token: 0x040009BB RID: 2491
		private JsonNodeType nodeType;

		// Token: 0x040009BC RID: 2492
		private object nodeValue;

		// Token: 0x040009BD RID: 2493
		private StringBuilder stringValueBuilder;

		// Token: 0x02000314 RID: 788
		private enum ScopeType
		{
			// Token: 0x04000CDC RID: 3292
			Root,
			// Token: 0x04000CDD RID: 3293
			Array,
			// Token: 0x04000CDE RID: 3294
			Object,
			// Token: 0x04000CDF RID: 3295
			Property
		}

		// Token: 0x02000315 RID: 789
		private sealed class Scope
		{
			// Token: 0x06001A1F RID: 6687 RVA: 0x0004AF0A File Offset: 0x0004910A
			public Scope(JsonReader.ScopeType type)
			{
				this.type = type;
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0004AF19 File Offset: 0x00049119
			// (set) Token: 0x06001A21 RID: 6689 RVA: 0x0004AF21 File Offset: 0x00049121
			public int ValueCount { get; set; }

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0004AF2A File Offset: 0x0004912A
			public JsonReader.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000CE0 RID: 3296
			private readonly JsonReader.ScopeType type;
		}
	}
}
