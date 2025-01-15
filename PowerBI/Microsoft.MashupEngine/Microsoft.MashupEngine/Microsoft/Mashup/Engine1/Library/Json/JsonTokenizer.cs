using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Json
{
	// Token: 0x02000A43 RID: 2627
	public class JsonTokenizer : IDisposable
	{
		// Token: 0x06004906 RID: 18694 RVA: 0x000F4160 File Offset: 0x000F2360
		public JsonTokenizer(TextReader reader, bool disposeReader = true, bool flushStream = true, Value value = null)
		{
			this.disposeReader = disposeReader;
			this.flushStream = flushStream;
			this.value = value;
			this.reader = reader;
			this.buffer = new char[1024];
			this.scopes = new Stack<JsonTokenizer.JsonState>(20);
			this.currentScope = JsonTokenizer.JsonState.TopLevel;
			this.FillBuffer(0, 0);
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x000F41BD File Offset: 0x000F23BD
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x000F41C8 File Offset: 0x000F23C8
		public void Dispose()
		{
			if (this.reader != null)
			{
				if (this.flushStream)
				{
					this.reader.ReadToEnd();
				}
				if (this.disposeReader)
				{
					this.reader.Dispose();
				}
				this.reader = null;
				this.buffer = null;
				this.scopes = null;
			}
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x000F421C File Offset: 0x000F241C
		public bool TokenTextMatches(string literal)
		{
			if (literal.Length != this.lastLength)
			{
				return false;
			}
			for (int i = 0; i < this.lastLength; i++)
			{
				if (this.buffer[this.lastPosition + i] != literal[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x000F4265 File Offset: 0x000F2465
		public string GetLastErrorMessage()
		{
			return this.lastErrorMessage;
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x000F426D File Offset: 0x000F246D
		public string GetLastErrorToken()
		{
			return this.lastErrorToken;
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x000F4275 File Offset: 0x000F2475
		public string GetTokenText()
		{
			return new string(this.buffer, this.lastPosition, this.lastLength);
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x000F428E File Offset: 0x000F248E
		public char GetCharacter()
		{
			return this.buffer[this.lastPosition];
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x000F429D File Offset: 0x000F249D
		public int GetPosition()
		{
			return this.bufferOffset + this.lastPosition;
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x000F42AC File Offset: 0x000F24AC
		public int GetInputPosition()
		{
			return this.bufferOffset + this.position;
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x000F42BB File Offset: 0x000F24BB
		public bool TryParse(out int result)
		{
			return int.TryParse(this.GetTokenText(), NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x000F42CF File Offset: 0x000F24CF
		public bool TryParse(out double result)
		{
			return double.TryParse(this.GetTokenText(), NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x000F42E7 File Offset: 0x000F24E7
		public bool TryParse(out NumberValue result)
		{
			return NumberValue.TryParse(this.GetTokenText(), NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000F4300 File Offset: 0x000F2500
		public JsonToken GetNextToken()
		{
			JsonToken jsonToken;
			try
			{
				jsonToken = this.TryGetNextToken();
			}
			catch (JsonTokenizer.UnexpectedCharacterException ex)
			{
				this.currentScope = JsonTokenizer.JsonState.Complete;
				this.lastLength = 1;
				this.lastPosition = ex.Position;
				this.buffer[ex.Position] = (char)ex.Character;
				jsonToken = JsonToken.Error;
			}
			catch (EndOfStreamException)
			{
				this.currentScope = JsonTokenizer.JsonState.Complete;
				this.lastLength = 0;
				this.lastPosition = this.end;
				jsonToken = JsonToken.End;
			}
			return jsonToken;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x000F4388 File Offset: 0x000F2588
		public JsonToken PeekNextToken()
		{
			if (this.lastToken == null)
			{
				this.lastToken = new JsonToken?(this.GetNextToken());
			}
			return this.lastToken.Value;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000F43B4 File Offset: 0x000F25B4
		public bool IsAtEnd()
		{
			if (this.lastToken != null)
			{
				JsonToken? jsonToken = this.lastToken;
				JsonToken jsonToken2 = JsonToken.End;
				return (jsonToken.GetValueOrDefault() == jsonToken2) & (jsonToken != null);
			}
			if (this.closed)
			{
				while (this.position < this.end && JsonTokenizer.IsWhite((int)this.buffer[this.position]))
				{
					this.position++;
				}
				return this.position == this.end;
			}
			return this.GetNextToken() == JsonToken.End;
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x000F443C File Offset: 0x000F263C
		private JsonToken TryGetNextToken()
		{
			JsonToken jsonToken;
			if (this.lastToken != null)
			{
				jsonToken = this.lastToken.Value;
				this.lastToken = null;
				return jsonToken;
			}
			this.lastPosition = this.SkipWhitespace(false);
			switch (this.currentScope)
			{
			case JsonTokenizer.JsonState.TopLevel:
				jsonToken = this.GetNextValue(out this.lastLength);
				break;
			case JsonTokenizer.JsonState.RecordStart:
				jsonToken = this.GetNextRecordKey(out this.lastLength);
				break;
			case JsonTokenizer.JsonState.BeforeRecordValue:
				if (this.current == -1)
				{
					return JsonToken.End;
				}
				if (this.current != 58)
				{
					throw this.UnexpectedCharacter(Strings.JsonInvalidCharacterBeforeRecordError);
				}
				this.lastPosition = this.SkipWhitespace(true);
				this.currentScope = JsonTokenizer.JsonState.AfterRecordValue;
				jsonToken = this.GetNextValue(out this.lastLength);
				break;
			case JsonTokenizer.JsonState.AfterRecordValue:
				if (this.current == 44)
				{
					this.lastPosition = this.SkipWhitespace(true);
				}
				else if (this.current != 125)
				{
					throw this.UnexpectedCharacter(Strings.JsonInvalidRecordFormatError);
				}
				jsonToken = this.GetNextRecordKey(out this.lastLength);
				break;
			case JsonTokenizer.JsonState.ListStart:
				this.currentScope = JsonTokenizer.JsonState.AfterListValue;
				jsonToken = this.GetNextValue(out this.lastLength);
				break;
			case JsonTokenizer.JsonState.AfterListValue:
				if (this.current == 44)
				{
					this.lastPosition = this.SkipWhitespace(true);
				}
				else if (this.current != 93)
				{
					throw this.UnexpectedCharacter(Strings.JsonInvalidListFormatError);
				}
				jsonToken = this.GetNextValue(out this.lastLength);
				break;
			case JsonTokenizer.JsonState.Complete:
				jsonToken = JsonToken.End;
				this.lastLength = 0;
				break;
			default:
				throw new InvalidOperationException();
			}
			return jsonToken;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x000F45CC File Offset: 0x000F27CC
		private JsonToken GetNextValue(out int length)
		{
			int num = this.current;
			JsonToken jsonToken;
			if (num <= 93)
			{
				if (num <= 34)
				{
					if (num == -1)
					{
						length = 0;
						return JsonToken.End;
					}
					if (num == 34)
					{
						length = this.ReadQuotedString();
						return JsonToken.String;
					}
				}
				else
				{
					if (num == 91)
					{
						jsonToken = JsonToken.ListStart;
						length = 1;
						this.scopes.Push(this.currentScope);
						this.currentScope = JsonTokenizer.JsonState.ListStart;
						this.position++;
						return jsonToken;
					}
					if (num == 93)
					{
						jsonToken = JsonToken.ListEnd;
						length = 1;
						if (this.scopes.Count == 0)
						{
							throw this.UnexpectedCharacter(Strings.JsonExtraCloseBraceError(']'));
						}
						this.currentScope = this.scopes.Pop();
						this.position++;
						return jsonToken;
					}
				}
			}
			else if (num <= 110)
			{
				if (num == 102)
				{
					length = this.ReadLiteral("alse");
					return JsonToken.False;
				}
				if (num == 110)
				{
					length = this.ReadLiteral("ull");
					return JsonToken.Null;
				}
			}
			else
			{
				if (num == 116)
				{
					length = this.ReadLiteral("rue");
					return JsonToken.True;
				}
				if (num == 123)
				{
					jsonToken = JsonToken.RecordStart;
					length = 1;
					this.scopes.Push(this.currentScope);
					this.currentScope = JsonTokenizer.JsonState.RecordStart;
					this.position++;
					return jsonToken;
				}
				if (num == 125)
				{
					jsonToken = JsonToken.RecordEnd;
					length = 1;
					if (this.scopes.Count == 0)
					{
						throw this.UnexpectedCharacter(Strings.JsonExtraCloseBraceError('}'));
					}
					this.currentScope = this.scopes.Pop();
					this.position++;
					return jsonToken;
				}
			}
			if ((this.current >= 48 && this.current <= 57) || this.current == 45)
			{
				length = this.ReadNumberValue();
				jsonToken = JsonToken.Number;
			}
			else
			{
				if (this.position == 0)
				{
					throw this.UnexpectedCharacter(Strings.JsonUnexpectedToken);
				}
				throw this.UnexpectedCharacter(Strings.JsonExtraCharactersAtTheEnd);
			}
			return jsonToken;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x000F47E0 File Offset: 0x000F29E0
		private JsonToken GetNextRecordKey(out int length)
		{
			JsonToken jsonToken;
			if (this.current == 125)
			{
				jsonToken = JsonToken.RecordEnd;
				length = 1;
				if (this.scopes.Count == 0)
				{
					throw this.UnexpectedCharacter(Strings.JsonExtraCloseBraceError('}'));
				}
				this.currentScope = this.scopes.Pop();
				this.position++;
			}
			else if (this.current == 34)
			{
				length = this.ReadQuotedString();
				jsonToken = JsonToken.RecordKey;
				this.currentScope = JsonTokenizer.JsonState.BeforeRecordValue;
			}
			else if (this.IsJavaScriptIdentifier(this.current))
			{
				length = this.ReadIdentifier();
				jsonToken = JsonToken.RecordKey;
				this.currentScope = JsonTokenizer.JsonState.BeforeRecordValue;
			}
			else
			{
				if (this.current != -1)
				{
					throw this.UnexpectedCharacter(Strings.JsonInvalidRecordKeyError);
				}
				length = 0;
				jsonToken = JsonToken.End;
			}
			return jsonToken;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x000F48A4 File Offset: 0x000F2AA4
		private int ReadLiteral(string remaining)
		{
			this.Ensure(this.position, remaining.Length + 1);
			for (int i = 0; i < remaining.Length; i++)
			{
				char[] array = this.buffer;
				int num = this.position + 1;
				this.position = num;
				if (array[num] != remaining[i])
				{
					throw this.UnexpectedCharacter(Strings.JsonLiteralUnsupported);
				}
			}
			this.position++;
			return remaining.Length + 1;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x000F4920 File Offset: 0x000F2B20
		private int SkipWhitespace(bool skipChar)
		{
			if (skipChar && this.Ensure(this.position, 1))
			{
				this.position++;
			}
			int num = -1;
			while (this.TryEnsure(this.position, 1))
			{
				num = (int)this.buffer[this.position];
				if (!JsonTokenizer.IsWhite(num))
				{
					break;
				}
				num = -1;
				this.position++;
			}
			this.current = num;
			return this.position;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x000F4993 File Offset: 0x000F2B93
		private bool Ensure(int bufferPosition, int minimumCount)
		{
			if (this.end - bufferPosition < minimumCount && this.FillBuffer(bufferPosition, minimumCount) < minimumCount)
			{
				throw new EndOfStreamException();
			}
			return true;
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x000F49B2 File Offset: 0x000F2BB2
		private bool TryEnsure(int bufferPosition, int minimumCount)
		{
			return this.end - bufferPosition >= minimumCount || this.FillBuffer(bufferPosition, minimumCount) >= minimumCount;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x000F49D0 File Offset: 0x000F2BD0
		private void EnsureOrFill(int minimumCount)
		{
			if (this.end - this.position < minimumCount && this.FillBuffer(this.position, minimumCount) < minimumCount)
			{
				for (int i = this.end; i < this.position + minimumCount; i++)
				{
					this.buffer[i] = '\0';
				}
			}
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x000F4A1E File Offset: 0x000F2C1E
		private void LockBuffer()
		{
			this.locked = true;
			if (this.position > 512)
			{
				this.CompressBuffer();
			}
			this.lastPosition = this.position;
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x000F4A46 File Offset: 0x000F2C46
		private void UnlockBuffer()
		{
			this.locked = false;
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x000F4A50 File Offset: 0x000F2C50
		private void CompressBuffer()
		{
			int num = this.end - this.position;
			if (num > 0)
			{
				Buffer.BlockCopy(this.buffer, 2 * this.position, this.buffer, 0, 2 * num);
			}
			this.bufferOffset += this.position;
			this.position = 0;
			this.end = num;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x000F4AB0 File Offset: 0x000F2CB0
		private int FillBuffer(int bufferPosition, int minimumCount)
		{
			if (this.closed)
			{
				return this.end - bufferPosition;
			}
			if (!this.locked)
			{
				this.CompressBuffer();
				bufferPosition = this.position;
			}
			else if (this.end > this.buffer.Length - 512)
			{
				char[] array = new char[this.buffer.Length + 1024];
				Buffer.BlockCopy(this.buffer, 0, array, 0, 2 * this.end);
				this.buffer = array;
			}
			int num2;
			do
			{
				int num = this.reader.Read(this.buffer, this.end, this.buffer.Length - this.end);
				this.end += num;
				if (num == 0)
				{
					this.closed = true;
				}
			}
			while ((num2 = this.end - bufferPosition) < minimumCount && !this.closed);
			return num2;
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x000F4B84 File Offset: 0x000F2D84
		private int ReadQuotedString()
		{
			this.Ensure(this.position, 1);
			this.position++;
			this.LockBuffer();
			int num = this.position;
			int num2 = this.position;
			try
			{
				char c;
				while (this.Ensure(num, 1) && (c = this.buffer[num]) != '"')
				{
					if (c == '\\')
					{
						this.Ensure(++num, 1);
						c = this.buffer[num];
						if (c <= '\\')
						{
							if (c == '"' || c == '/' || c == '\\')
							{
								goto IL_0100;
							}
						}
						else if (c <= 'f')
						{
							if (c == 'b')
							{
								c = '\b';
								goto IL_0100;
							}
							if (c == 'f')
							{
								c = '\f';
								goto IL_0100;
							}
						}
						else
						{
							if (c == 'n')
							{
								c = '\n';
								goto IL_0100;
							}
							switch (c)
							{
							case 'r':
								c = '\r';
								goto IL_0100;
							case 't':
								c = '\t';
								goto IL_0100;
							case 'u':
								this.Ensure(++num, 4);
								c = (char)this.ReadHex4(num);
								num += 3;
								goto IL_0100;
							}
						}
						this.lastErrorToken = "\\" + c.ToString();
						throw this.UnexpectedCharacter(Strings.JsonInvalidEscapeSequence, num);
					}
					IL_0100:
					if (num2 < num)
					{
						this.buffer[num2] = c;
					}
					num++;
					num2++;
				}
			}
			catch (EndOfStreamException)
			{
				throw this.UnexpectedCharacter(Strings.UnexpectedEndOfBuffer, num, 0);
			}
			this.UnlockBuffer();
			int num3 = num2 - this.position;
			this.position = num + 1;
			return num3;
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x000F4D0C File Offset: 0x000F2F0C
		private int ReadHexDigit(int position)
		{
			char c = this.buffer[position];
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)('\n' + c - 'a');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)('\n' + c - 'A');
			}
			throw this.UnexpectedCharacter(Strings.JsonInvalidHexDigitError, position);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x000F4D68 File Offset: 0x000F2F68
		private int ReadHex4(int position)
		{
			int num = this.ReadHexDigit(position++);
			int num2 = this.ReadHexDigit(position++);
			int num3 = this.ReadHexDigit(position++);
			int num4 = this.ReadHexDigit(position);
			return (num << 12) | (num2 << 8) | (num3 << 4) | num4;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x000F4DB0 File Offset: 0x000F2FB0
		private int ReadIdentifier()
		{
			this.LockBuffer();
			int num = this.position;
			while (this.Ensure(num, 1) && this.IsJavaScriptIdentifier((int)this.buffer[num]))
			{
				num++;
			}
			this.UnlockBuffer();
			return this.UpdatePosition(num);
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x000F4DF7 File Offset: 0x000F2FF7
		private bool IsJavaScriptIdentifier(int ch)
		{
			return ch > 0 && (char.IsLetterOrDigit((char)ch) || ch == 36 || ch == 95);
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x000F4E14 File Offset: 0x000F3014
		private int ReadNumberValue()
		{
			this.Ensure(this.position, 1);
			this.LockBuffer();
			this.lastPosition = this.position;
			int num = this.position;
			bool flag = true;
			if (this.buffer[num] == '-')
			{
				this.Ensure(++num, 1);
			}
			if (this.buffer[num] == '0')
			{
				num++;
			}
			else if (this.buffer[num] >= '1' && this.buffer[num] <= '9')
			{
				char c;
				while (this.TryEnsure(++num, 1) && (c = this.buffer[num]) >= '0')
				{
					if (c > '9')
					{
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			if (flag && this.TryEnsure(num, 1) && this.buffer[num] == '.')
			{
				char c2;
				while (this.TryEnsure(++num, 1) && (c2 = this.buffer[num]) >= '0' && c2 <= '9')
				{
				}
			}
			if (flag && this.TryEnsure(num, 1) && (this.buffer[num] == 'e' || this.buffer[num] == 'E'))
			{
				this.Ensure(++num, 1);
				char c3 = this.buffer[num];
				if (c3 == '+' || c3 == '-')
				{
					this.Ensure(++num, 1);
					c3 = this.buffer[num];
				}
				flag = c3 >= '0' && c3 <= '9';
				while (this.TryEnsure(++num, 1) && (c3 = this.buffer[num]) >= '0' && c3 <= '9')
				{
				}
			}
			if (!flag)
			{
				throw this.UnexpectedCharacter(Strings.UnexpectedEndOfBuffer, num);
			}
			this.UnlockBuffer();
			return this.UpdatePosition(num);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x000F4FA9 File Offset: 0x000F31A9
		private int UpdatePosition(int newPosition)
		{
			int num = newPosition - this.position;
			this.position = newPosition;
			return num;
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x000F4FBA File Offset: 0x000F31BA
		public static bool IsWhite(int c)
		{
			return c == 32 || c == 9 || c == 10 || c == 13 || c == 160;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x000F4FDA File Offset: 0x000F31DA
		private JsonTokenizer.UnexpectedCharacterException UnexpectedCharacter(string errorMessage)
		{
			return this.UnexpectedCharacter(errorMessage, this.position, this.current);
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x000F4FEF File Offset: 0x000F31EF
		private JsonTokenizer.UnexpectedCharacterException UnexpectedCharacter(string errorMessage, int bufferPosition)
		{
			return this.UnexpectedCharacter(errorMessage, bufferPosition, (int)this.buffer[bufferPosition]);
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x000F5001 File Offset: 0x000F3201
		private JsonTokenizer.UnexpectedCharacterException UnexpectedCharacter(string errorMessage, int bufferPosition, int bufferCharacter)
		{
			this.lastErrorMessage = errorMessage;
			this.lastErrorToken = this.lastErrorToken ?? new string(this.buffer, this.lastPosition, this.position - this.lastPosition);
			return new JsonTokenizer.UnexpectedCharacterException(bufferPosition, bufferCharacter);
		}

		// Token: 0x04002719 RID: 10009
		private const int bufferSize = 1024;

		// Token: 0x0400271A RID: 10010
		private const int stackSize = 20;

		// Token: 0x0400271B RID: 10011
		private readonly bool disposeReader;

		// Token: 0x0400271C RID: 10012
		private readonly bool flushStream;

		// Token: 0x0400271D RID: 10013
		private readonly Value value;

		// Token: 0x0400271E RID: 10014
		private TextReader reader;

		// Token: 0x0400271F RID: 10015
		private char[] buffer;

		// Token: 0x04002720 RID: 10016
		private Stack<JsonTokenizer.JsonState> scopes;

		// Token: 0x04002721 RID: 10017
		private JsonTokenizer.JsonState currentScope;

		// Token: 0x04002722 RID: 10018
		private bool locked;

		// Token: 0x04002723 RID: 10019
		private bool closed;

		// Token: 0x04002724 RID: 10020
		private int bufferOffset;

		// Token: 0x04002725 RID: 10021
		private int position;

		// Token: 0x04002726 RID: 10022
		private int end;

		// Token: 0x04002727 RID: 10023
		private int current;

		// Token: 0x04002728 RID: 10024
		private int lastPosition;

		// Token: 0x04002729 RID: 10025
		private int lastLength;

		// Token: 0x0400272A RID: 10026
		private string lastErrorMessage;

		// Token: 0x0400272B RID: 10027
		private string lastErrorToken;

		// Token: 0x0400272C RID: 10028
		private JsonToken? lastToken;

		// Token: 0x02000A44 RID: 2628
		private enum JsonState
		{
			// Token: 0x0400272E RID: 10030
			TopLevel,
			// Token: 0x0400272F RID: 10031
			RecordStart,
			// Token: 0x04002730 RID: 10032
			BeforeRecordValue,
			// Token: 0x04002731 RID: 10033
			AfterRecordValue,
			// Token: 0x04002732 RID: 10034
			ListStart,
			// Token: 0x04002733 RID: 10035
			AfterListValue,
			// Token: 0x04002734 RID: 10036
			Complete
		}

		// Token: 0x02000A45 RID: 2629
		[Serializable]
		private class UnexpectedCharacterException : Exception
		{
			// Token: 0x0600492D RID: 18733 RVA: 0x000F503F File Offset: 0x000F323F
			public UnexpectedCharacterException(int position, int character)
			{
				this.position = position;
				this.character = character;
			}

			// Token: 0x0600492E RID: 18734 RVA: 0x000F5055 File Offset: 0x000F3255
			protected UnexpectedCharacterException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				this.position = info.GetInt32("position");
				this.character = info.GetInt32("character");
			}

			// Token: 0x17001713 RID: 5907
			// (get) Token: 0x0600492F RID: 18735 RVA: 0x000F5081 File Offset: 0x000F3281
			public int Position
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x17001714 RID: 5908
			// (get) Token: 0x06004930 RID: 18736 RVA: 0x000F5089 File Offset: 0x000F3289
			public int Character
			{
				get
				{
					return this.character;
				}
			}

			// Token: 0x06004931 RID: 18737 RVA: 0x000F5091 File Offset: 0x000F3291
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("position", this.position);
				info.AddValue("character", this.character);
				base.GetObjectData(info, context);
			}

			// Token: 0x04002735 RID: 10037
			private readonly int position;

			// Token: 0x04002736 RID: 10038
			private readonly int character;
		}
	}
}
