using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000105 RID: 261
	[DebuggerDisplay("{NodeType}: {Value}")]
	internal class JsonReader
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00023E40 File Offset: 0x00022040
		public JsonReader(TextReader reader, ODataFormat jsonFormat, bool isIeee754Compatible)
		{
			this.nodeType = JsonNodeType.None;
			this.nodeValue = null;
			this.reader = reader;
			this.characterBuffer = new char[2040];
			this.storedCharacterCount = 0;
			this.tokenStartIndex = 0;
			this.endOfInputReached = false;
			this.isIeee754Compatible = isIeee754Compatible;
			this.allowAnnotations = jsonFormat == ODataFormat.Json;
			this.scopes = new Stack<JsonReader.Scope>();
			this.scopes.Push(new JsonReader.Scope(JsonReader.ScopeType.Root));
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00023EBE File Offset: 0x000220BE
		public virtual object Value
		{
			get
			{
				return this.nodeValue;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00023EC6 File Offset: 0x000220C6
		public virtual JsonNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00023ECE File Offset: 0x000220CE
		public virtual bool IsIeee754Compatible
		{
			get
			{
				return this.isIeee754Compatible;
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00023ED8 File Offset: 0x000220D8
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

		// Token: 0x060009DD RID: 2525 RVA: 0x000240E1 File Offset: 0x000222E1
		private static bool IsWhitespaceCharacter(char character)
		{
			return character <= ' ' && (character == ' ' || character == '\t' || character == '\n' || character == '\r');
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00024100 File Offset: 0x00022300
		private JsonNodeType ParseValue()
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
							goto IL_00B3;
						}
						this.nodeValue = this.ParseNullPrimitiveValue();
						goto IL_00DE;
					}
				}
				else if (c2 != 't')
				{
					if (c2 == '{')
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
			if (c2 == '"' || c2 == '\'')
			{
				bool flag;
				this.nodeValue = this.ParseStringPrimitiveValue(out flag);
				goto IL_00DE;
			}
			if (c2 == '[')
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

		// Token: 0x060009DF RID: 2527 RVA: 0x000241F4 File Offset: 0x000223F4
		private JsonNodeType ParseProperty()
		{
			this.scopes.Peek().ValueCount++;
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

		// Token: 0x060009E0 RID: 2528 RVA: 0x00024294 File Offset: 0x00022494
		private string ParseStringPrimitiveValue()
		{
			bool flag;
			return this.ParseStringPrimitiveValue(out flag);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000242AC File Offset: 0x000224AC
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

		// Token: 0x060009E2 RID: 2530 RVA: 0x00024514 File Offset: 0x00022714
		private object ParseNullPrimitiveValue()
		{
			string text = this.ParseName();
			if (!string.Equals(text, "null"))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
			}
			return null;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00024544 File Offset: 0x00022744
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

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002458C File Offset: 0x0002278C
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

		// Token: 0x060009E5 RID: 2533 RVA: 0x00024654 File Offset: 0x00022854
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

		// Token: 0x060009E6 RID: 2534 RVA: 0x000246D9 File Offset: 0x000228D9
		private bool EndOfInput()
		{
			if (this.scopes.Count > 1)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
			}
			this.nodeType = JsonNodeType.EndOfInput;
			return false;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000246FC File Offset: 0x000228FC
		private void PushScope(JsonReader.ScopeType newScopeType)
		{
			this.scopes.Push(new JsonReader.Scope(newScopeType));
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002470F File Offset: 0x0002290F
		private void PopScope()
		{
			this.scopes.Pop();
			this.TryPopPropertyScope();
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00024723 File Offset: 0x00022923
		private void TryPopPropertyScope()
		{
			if (this.scopes.Peek().Type == JsonReader.ScopeType.Property)
			{
				this.scopes.Pop();
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00024744 File Offset: 0x00022944
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

		// Token: 0x060009EB RID: 2539 RVA: 0x00024783 File Offset: 0x00022983
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

		// Token: 0x060009EC RID: 2540 RVA: 0x000247A4 File Offset: 0x000229A4
		private string ConsumeTokenToString(int characterCount)
		{
			string text = new string(this.characterBuffer, this.tokenStartIndex, characterCount);
			this.tokenStartIndex += characterCount;
			return text;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000247D4 File Offset: 0x000229D4
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

		// Token: 0x040003E5 RID: 997
		private const int InitialCharacterBufferSize = 2040;

		// Token: 0x040003E6 RID: 998
		private const int MaxCharacterCountToMove = 64;

		// Token: 0x040003E7 RID: 999
		private readonly TextReader reader;

		// Token: 0x040003E8 RID: 1000
		private readonly bool isIeee754Compatible;

		// Token: 0x040003E9 RID: 1001
		private readonly Stack<JsonReader.Scope> scopes;

		// Token: 0x040003EA RID: 1002
		private readonly bool allowAnnotations;

		// Token: 0x040003EB RID: 1003
		private bool endOfInputReached;

		// Token: 0x040003EC RID: 1004
		private char[] characterBuffer;

		// Token: 0x040003ED RID: 1005
		private int storedCharacterCount;

		// Token: 0x040003EE RID: 1006
		private int tokenStartIndex;

		// Token: 0x040003EF RID: 1007
		private JsonNodeType nodeType;

		// Token: 0x040003F0 RID: 1008
		private object nodeValue;

		// Token: 0x040003F1 RID: 1009
		private StringBuilder stringValueBuilder;

		// Token: 0x02000106 RID: 262
		private enum ScopeType
		{
			// Token: 0x040003F3 RID: 1011
			Root,
			// Token: 0x040003F4 RID: 1012
			Array,
			// Token: 0x040003F5 RID: 1013
			Object,
			// Token: 0x040003F6 RID: 1014
			Property
		}

		// Token: 0x02000107 RID: 263
		private sealed class Scope
		{
			// Token: 0x060009EE RID: 2542 RVA: 0x000248E4 File Offset: 0x00022AE4
			public Scope(JsonReader.ScopeType type)
			{
				this.type = type;
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x060009EF RID: 2543 RVA: 0x000248F3 File Offset: 0x00022AF3
			// (set) Token: 0x060009F0 RID: 2544 RVA: 0x000248FB File Offset: 0x00022AFB
			public int ValueCount { get; set; }

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00024904 File Offset: 0x00022B04
			public JsonReader.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x040003F7 RID: 1015
			private readonly JsonReader.ScopeType type;
		}
	}
}
