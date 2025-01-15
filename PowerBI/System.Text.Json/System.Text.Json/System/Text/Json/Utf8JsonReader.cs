using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Text.Json
{
	// Token: 0x02000048 RID: 72
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public ref struct Utf8JsonReader
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000B0DE File Offset: 0x000092DE
		private bool IsLastSpan
		{
			get
			{
				return this._isFinalBlock && (!this._isMultiSegment || this._isLastSegment);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000B0FA File Offset: 0x000092FA
		internal ReadOnlySequence<byte> OriginalSequence
		{
			get
			{
				return this._sequence;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000B104 File Offset: 0x00009304
		internal ReadOnlySpan<byte> OriginalSpan
		{
			get
			{
				if (!this._sequence.IsEmpty)
				{
					return default(ReadOnlySpan<byte>);
				}
				return this._buffer;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000B130 File Offset: 0x00009330
		internal readonly int ValueLength
		{
			get
			{
				if (!this.HasValueSequence)
				{
					return this.ValueSpan.Length;
				}
				return checked((int)this.ValueSequence.Length);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000B163 File Offset: 0x00009363
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000B16B File Offset: 0x0000936B
		public ReadOnlySpan<byte> ValueSpan { readonly get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000B174 File Offset: 0x00009374
		public readonly long BytesConsumed
		{
			get
			{
				return this._totalConsumed + (long)this._consumed;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000B184 File Offset: 0x00009384
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000B18C File Offset: 0x0000938C
		public long TokenStartIndex { readonly get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000B198 File Offset: 0x00009398
		public readonly int CurrentDepth
		{
			get
			{
				BitStack bitStack = this._bitStack;
				int num = bitStack.CurrentDepth;
				if (this.TokenType == JsonTokenType.StartArray || this.TokenType == JsonTokenType.StartObject)
				{
					num--;
				}
				return num;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000B1CB File Offset: 0x000093CB
		internal bool IsInArray
		{
			get
			{
				return !this._inObject;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000B1D6 File Offset: 0x000093D6
		public readonly JsonTokenType TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000B1DE File Offset: 0x000093DE
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000B1E6 File Offset: 0x000093E6
		public bool HasValueSequence { readonly get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000B1EF File Offset: 0x000093EF
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000B1F7 File Offset: 0x000093F7
		public bool ValueIsEscaped { readonly get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000B200 File Offset: 0x00009400
		public readonly bool IsFinalBlock
		{
			get
			{
				return this._isFinalBlock;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000B208 File Offset: 0x00009408
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000B210 File Offset: 0x00009410
		public ReadOnlySequence<byte> ValueSequence { readonly get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000B21C File Offset: 0x0000941C
		public readonly SequencePosition Position
		{
			get
			{
				if (this._isInputSequence)
				{
					return this._sequence.GetPosition((long)this._consumed, this._currentPosition);
				}
				return default(SequencePosition);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000B254 File Offset: 0x00009454
		public readonly JsonReaderState CurrentState
		{
			get
			{
				return new JsonReaderState
				{
					_lineNumber = this._lineNumber,
					_bytePositionInLine = this._bytePositionInLine,
					_inObject = this._inObject,
					_isNotPrimitive = this._isNotPrimitive,
					_valueIsEscaped = this.ValueIsEscaped,
					_trailingCommaBeforeComment = this._trailingCommaBeforeComment,
					_tokenType = this._tokenType,
					_previousTokenType = this._previousTokenType,
					_readerOptions = this._readerOptions,
					_bitStack = this._bitStack
				};
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000B2EC File Offset: 0x000094EC
		public Utf8JsonReader(ReadOnlySpan<byte> jsonData, bool isFinalBlock, JsonReaderState state)
		{
			this._buffer = jsonData;
			this._isFinalBlock = isFinalBlock;
			this._isInputSequence = false;
			this._lineNumber = state._lineNumber;
			this._bytePositionInLine = state._bytePositionInLine;
			this._inObject = state._inObject;
			this._isNotPrimitive = state._isNotPrimitive;
			this.ValueIsEscaped = state._valueIsEscaped;
			this._trailingCommaBeforeComment = state._trailingCommaBeforeComment;
			this._tokenType = state._tokenType;
			this._previousTokenType = state._previousTokenType;
			this._readerOptions = state._readerOptions;
			if (this._readerOptions.MaxDepth == 0)
			{
				this._readerOptions.MaxDepth = 64;
			}
			this._bitStack = state._bitStack;
			this._consumed = 0;
			this.TokenStartIndex = 0L;
			this._totalConsumed = 0L;
			this._isLastSegment = this._isFinalBlock;
			this._isMultiSegment = false;
			this.ValueSpan = ReadOnlySpan<byte>.Empty;
			this._currentPosition = default(SequencePosition);
			this._nextPosition = default(SequencePosition);
			this._sequence = default(ReadOnlySequence<byte>);
			this.HasValueSequence = false;
			this.ValueSequence = ReadOnlySequence<byte>.Empty;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000B40B File Offset: 0x0000960B
		public Utf8JsonReader(ReadOnlySpan<byte> jsonData, JsonReaderOptions options = default(JsonReaderOptions))
		{
			this = new Utf8JsonReader(jsonData, true, new JsonReaderState(options));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000B41C File Offset: 0x0000961C
		public bool Read()
		{
			bool flag = (this._isMultiSegment ? this.ReadMultiSegment() : this.ReadSingleSegment());
			if (!flag && this._isFinalBlock && this.TokenType == JsonTokenType.None)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedJsonTokens, 0, default(ReadOnlySpan<byte>));
			}
			return flag;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000B466 File Offset: 0x00009666
		public void Skip()
		{
			if (!this._isFinalBlock)
			{
				ThrowHelper.ThrowInvalidOperationException_CannotSkipOnPartial();
			}
			this.SkipHelper();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000B47C File Offset: 0x0000967C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SkipHelper()
		{
			if (this.TokenType == JsonTokenType.PropertyName)
			{
				bool flag = this.Read();
			}
			if (this.TokenType == JsonTokenType.StartObject || this.TokenType == JsonTokenType.StartArray)
			{
				int currentDepth = this.CurrentDepth;
				do
				{
					bool flag2 = this.Read();
				}
				while (currentDepth < this.CurrentDepth);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000B4C2 File Offset: 0x000096C2
		public bool TrySkip()
		{
			if (this._isFinalBlock)
			{
				this.SkipHelper();
				return true;
			}
			return this.TrySkipHelper();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000B4DC File Offset: 0x000096DC
		private bool TrySkipHelper()
		{
			Utf8JsonReader utf8JsonReader = this;
			if (this.TokenType != JsonTokenType.PropertyName || this.Read())
			{
				if (this.TokenType == JsonTokenType.StartObject || this.TokenType == JsonTokenType.StartArray)
				{
					int currentDepth = this.CurrentDepth;
					while (this.Read())
					{
						if (currentDepth >= this.CurrentDepth)
						{
							return true;
						}
					}
					goto IL_0044;
				}
				return true;
			}
			IL_0044:
			this = utf8JsonReader;
			return false;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000B535 File Offset: 0x00009735
		public readonly bool ValueTextEquals(ReadOnlySpan<byte> utf8Text)
		{
			if (!Utf8JsonReader.IsTokenTypeString(this.TokenType))
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedStringComparison(this.TokenType);
			}
			return this.TextEqualsHelper(utf8Text);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000B556 File Offset: 0x00009756
		[NullableContext(2)]
		public readonly bool ValueTextEquals(string text)
		{
			return this.ValueTextEquals(text.AsSpan());
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B564 File Offset: 0x00009764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly bool TextEqualsHelper(ReadOnlySpan<byte> otherUtf8Text)
		{
			if (this.HasValueSequence)
			{
				return this.CompareToSequence(otherUtf8Text);
			}
			if (this.ValueIsEscaped)
			{
				return this.UnescapeAndCompare(otherUtf8Text);
			}
			return otherUtf8Text.SequenceEqual(this.ValueSpan);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000B594 File Offset: 0x00009794
		public unsafe readonly bool ValueTextEquals(ReadOnlySpan<char> text)
		{
			if (!Utf8JsonReader.IsTokenTypeString(this.TokenType))
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedStringComparison(this.TokenType);
			}
			if (this.MatchNotPossible(text.Length))
			{
				return false;
			}
			byte[] array = null;
			int num = checked(text.Length * 3);
			Span<byte> span;
			if (num > 256)
			{
				array = ArrayPool<byte>.Shared.Rent(num);
				span = array;
			}
			else
			{
				Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span = span2;
			}
			int num2;
			OperationStatus operationStatus = JsonWriterHelper.ToUtf8(text, span, out num2);
			bool flag = operationStatus != OperationStatus.InvalidData && this.TextEqualsHelper(span.Slice(0, num2));
			if (array != null)
			{
				span.Slice(0, num2).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B658 File Offset: 0x00009858
		private readonly bool CompareToSequence(ReadOnlySpan<byte> other)
		{
			if (this.ValueIsEscaped)
			{
				return this.UnescapeSequenceAndCompare(other);
			}
			ReadOnlySequence<byte> valueSequence = this.ValueSequence;
			if (valueSequence.Length != (long)other.Length)
			{
				return false;
			}
			int num = 0;
			foreach (ReadOnlyMemory<byte> readOnlyMemory in valueSequence)
			{
				ReadOnlySpan<byte> span = readOnlyMemory.Span;
				if (!other.Slice(num).StartsWith(span))
				{
					return false;
				}
				num += span.Length;
			}
			return true;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B6D8 File Offset: 0x000098D8
		private readonly bool UnescapeAndCompare(ReadOnlySpan<byte> other)
		{
			ReadOnlySpan<byte> valueSpan = this.ValueSpan;
			if (valueSpan.Length < other.Length || valueSpan.Length / 6 > other.Length)
			{
				return false;
			}
			int num = valueSpan.IndexOf(92);
			return other.StartsWith(valueSpan.Slice(0, num)) && JsonReaderHelper.UnescapeAndCompare(valueSpan.Slice(num), other.Slice(num));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B744 File Offset: 0x00009944
		private readonly bool UnescapeSequenceAndCompare(ReadOnlySpan<byte> other)
		{
			ReadOnlySequence<byte> readOnlySequence = this.ValueSequence;
			long length = readOnlySequence.Length;
			if (length < (long)other.Length || length / 6L > (long)other.Length)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (ReadOnlyMemory<byte> readOnlyMemory in readOnlySequence)
			{
				ReadOnlySpan<byte> span = readOnlyMemory.Span;
				int num2 = span.IndexOf(92);
				if (num2 != -1)
				{
					if (!other.Slice(num).StartsWith(span.Slice(0, num2)))
					{
						break;
					}
					num += num2;
					other = other.Slice(num);
					readOnlySequence = readOnlySequence.Slice((long)num);
					if (readOnlySequence.IsSingleSegment)
					{
						flag = JsonReaderHelper.UnescapeAndCompare(readOnlySequence.First.Span, other);
						break;
					}
					flag = JsonReaderHelper.UnescapeAndCompare(readOnlySequence, other);
					break;
				}
				else
				{
					if (!other.Slice(num).StartsWith(span))
					{
						break;
					}
					num += span.Length;
				}
			}
			return flag;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B835 File Offset: 0x00009A35
		private static bool IsTokenTypeString(JsonTokenType tokenType)
		{
			return tokenType == JsonTokenType.PropertyName || tokenType == JsonTokenType.String;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B844 File Offset: 0x00009A44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly bool MatchNotPossible(int charTextLength)
		{
			if (this.HasValueSequence)
			{
				return this.MatchNotPossibleSequence(charTextLength);
			}
			int length = this.ValueSpan.Length;
			return length < charTextLength || length / (this.ValueIsEscaped ? 6 : 3) > charTextLength;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B888 File Offset: 0x00009A88
		[MethodImpl(MethodImplOptions.NoInlining)]
		private readonly bool MatchNotPossibleSequence(int charTextLength)
		{
			long length = this.ValueSequence.Length;
			return length < (long)charTextLength || length / (this.ValueIsEscaped ? 6L : 3L) > (long)charTextLength;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		private void StartObject()
		{
			if (this._bitStack.CurrentDepth >= this._readerOptions.MaxDepth)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ObjectDepthTooLarge, 0, default(ReadOnlySpan<byte>));
			}
			this._bitStack.PushTrue();
			this.ValueSpan = this._buffer.Slice(this._consumed, 1);
			this._consumed++;
			this._bytePositionInLine += 1L;
			this._tokenType = JsonTokenType.StartObject;
			this._inObject = true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B948 File Offset: 0x00009B48
		private void EndObject()
		{
			if (!this._inObject || this._bitStack.CurrentDepth <= 0)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.MismatchedObjectArray, 125, default(ReadOnlySpan<byte>));
			}
			if (this._trailingCommaBeforeComment)
			{
				if (!this._readerOptions.AllowTrailingCommas)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
				}
				this._trailingCommaBeforeComment = false;
			}
			this._tokenType = JsonTokenType.EndObject;
			this.ValueSpan = this._buffer.Slice(this._consumed, 1);
			this.UpdateBitStackOnEndToken();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		private void StartArray()
		{
			if (this._bitStack.CurrentDepth >= this._readerOptions.MaxDepth)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ArrayDepthTooLarge, 0, default(ReadOnlySpan<byte>));
			}
			this._bitStack.PushFalse();
			this.ValueSpan = this._buffer.Slice(this._consumed, 1);
			this._consumed++;
			this._bytePositionInLine += 1L;
			this._tokenType = JsonTokenType.StartArray;
			this._inObject = false;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000BA58 File Offset: 0x00009C58
		private void EndArray()
		{
			if (this._inObject || this._bitStack.CurrentDepth <= 0)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.MismatchedObjectArray, 93, default(ReadOnlySpan<byte>));
			}
			if (this._trailingCommaBeforeComment)
			{
				if (!this._readerOptions.AllowTrailingCommas)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
				}
				this._trailingCommaBeforeComment = false;
			}
			this._tokenType = JsonTokenType.EndArray;
			this.ValueSpan = this._buffer.Slice(this._consumed, 1);
			this.UpdateBitStackOnEndToken();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000BAE1 File Offset: 0x00009CE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void UpdateBitStackOnEndToken()
		{
			this._consumed++;
			this._bytePositionInLine += 1L;
			this._inObject = this._bitStack.Pop();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000BB14 File Offset: 0x00009D14
		private unsafe bool ReadSingleSegment()
		{
			bool flag = false;
			this.ValueSpan = default(ReadOnlySpan<byte>);
			this.ValueIsEscaped = false;
			if (this.HasMoreData())
			{
				byte b = *this._buffer[this._consumed];
				if (b <= 32)
				{
					this.SkipWhiteSpace();
					if (!this.HasMoreData())
					{
						return flag;
					}
					b = *this._buffer[this._consumed];
				}
				this.TokenStartIndex = (long)this._consumed;
				if (this._tokenType != JsonTokenType.None)
				{
					if (b == 47)
					{
						flag = this.ConsumeNextTokenOrRollback(b);
					}
					else
					{
						if (this._tokenType == JsonTokenType.StartObject)
						{
							if (b == 125)
							{
								this.EndObject();
							}
							else
							{
								if (b != 34)
								{
									ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
								}
								int consumed = this._consumed;
								long bytePositionInLine = this._bytePositionInLine;
								long lineNumber = this._lineNumber;
								flag = this.ConsumePropertyName();
								if (!flag)
								{
									this._consumed = consumed;
									this._tokenType = JsonTokenType.StartObject;
									this._bytePositionInLine = bytePositionInLine;
									this._lineNumber = lineNumber;
									return flag;
								}
								return flag;
							}
						}
						else if (this._tokenType == JsonTokenType.StartArray)
						{
							if (b != 93)
							{
								return this.ConsumeValue(b);
							}
							this.EndArray();
						}
						else
						{
							if (this._tokenType == JsonTokenType.PropertyName)
							{
								return this.ConsumeValue(b);
							}
							return this.ConsumeNextTokenOrRollback(b);
						}
						flag = true;
					}
				}
				else
				{
					flag = this.ReadFirstToken(b);
				}
			}
			return flag;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000BC68 File Offset: 0x00009E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HasMoreData()
		{
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
			{
				if (this._isNotPrimitive && this.IsLastSpan)
				{
					if (this._bitStack.CurrentDepth != 0)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ZeroDepthAtEnd, 0, default(ReadOnlySpan<byte>));
					}
					if (this._readerOptions.CommentHandling == JsonCommentHandling.Allow && this._tokenType == JsonTokenType.Comment)
					{
						return false;
					}
					if (this._tokenType != JsonTokenType.EndArray && this._tokenType != JsonTokenType.EndObject)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidEndOfJsonNonPrimitive, 0, default(ReadOnlySpan<byte>));
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000BCFC File Offset: 0x00009EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HasMoreData(ExceptionResource resource)
		{
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
			{
				if (this.IsLastSpan)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, resource, 0, default(ReadOnlySpan<byte>));
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000BD3C File Offset: 0x00009F3C
		private bool ReadFirstToken(byte first)
		{
			if (first == 123)
			{
				this._bitStack.SetFirstBit();
				this._tokenType = JsonTokenType.StartObject;
				this.ValueSpan = this._buffer.Slice(this._consumed, 1);
				this._consumed++;
				this._bytePositionInLine += 1L;
				this._inObject = true;
				this._isNotPrimitive = true;
			}
			else if (first == 91)
			{
				this._bitStack.ResetFirstBit();
				this._tokenType = JsonTokenType.StartArray;
				this.ValueSpan = this._buffer.Slice(this._consumed, 1);
				this._consumed++;
				this._bytePositionInLine += 1L;
				this._isNotPrimitive = true;
			}
			else
			{
				ReadOnlySpan<byte> buffer = this._buffer;
				if (JsonHelpers.IsDigit(first) || first == 45)
				{
					int num;
					if (!this.TryGetNumber(buffer.Slice(this._consumed), out num))
					{
						return false;
					}
					this._tokenType = JsonTokenType.Number;
					this._consumed += num;
					this._bytePositionInLine += (long)num;
					return true;
				}
				else
				{
					if (!this.ConsumeValue(first))
					{
						return false;
					}
					if (this._tokenType == JsonTokenType.StartObject || this._tokenType == JsonTokenType.StartArray)
					{
						this._isNotPrimitive = true;
					}
				}
			}
			return true;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000BE78 File Offset: 0x0000A078
		private unsafe void SkipWhiteSpace()
		{
			ReadOnlySpan<byte> buffer = this._buffer;
			while (this._consumed < buffer.Length)
			{
				byte b = *buffer[this._consumed];
				if (b != 32 && b != 13 && b != 10 && b != 9)
				{
					break;
				}
				if (b == 10)
				{
					this._lineNumber += 1L;
					this._bytePositionInLine = 0L;
				}
				else
				{
					this._bytePositionInLine += 1L;
				}
				this._consumed++;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		private unsafe bool ConsumeValue(byte marker)
		{
			for (;;)
			{
				this._trailingCommaBeforeComment = false;
				if (marker == 34)
				{
					break;
				}
				if (marker == 123)
				{
					goto Block_1;
				}
				if (marker == 91)
				{
					goto Block_2;
				}
				if (JsonHelpers.IsDigit(marker) || marker == 45)
				{
					goto IL_0040;
				}
				if (marker == 102)
				{
					goto Block_4;
				}
				if (marker == 116)
				{
					goto Block_5;
				}
				if (marker == 110)
				{
					goto Block_6;
				}
				JsonCommentHandling commentHandling = this._readerOptions.CommentHandling;
				if (commentHandling == JsonCommentHandling.Disallow)
				{
					goto IL_0154;
				}
				if (commentHandling == JsonCommentHandling.Allow)
				{
					goto Block_8;
				}
				if (marker != 47)
				{
					goto IL_0154;
				}
				if (!this.SkipComment())
				{
					return false;
				}
				if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
				{
					goto Block_12;
				}
				marker = *this._buffer[this._consumed];
				if (marker <= 32)
				{
					this.SkipWhiteSpace();
					if (!this.HasMoreData())
					{
						return false;
					}
					marker = *this._buffer[this._consumed];
				}
				this.TokenStartIndex = (long)this._consumed;
			}
			return this.ConsumeString();
			Block_1:
			this.StartObject();
			return true;
			Block_2:
			this.StartArray();
			return true;
			IL_0040:
			return this.ConsumeNumber();
			Block_4:
			return this.ConsumeLiteral(JsonConstants.FalseValue, JsonTokenType.False);
			Block_5:
			return this.ConsumeLiteral(JsonConstants.TrueValue, JsonTokenType.True);
			Block_6:
			return this.ConsumeLiteral(JsonConstants.NullValue, JsonTokenType.Null);
			Block_8:
			if (marker == 47)
			{
				return this.ConsumeComment();
			}
			goto IL_0154;
			Block_12:
			if (this._isNotPrimitive && this.IsLastSpan && this._tokenType != JsonTokenType.EndArray && this._tokenType != JsonTokenType.EndObject)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidEndOfJsonNonPrimitive, 0, default(ReadOnlySpan<byte>));
			}
			return false;
			IL_0154:
			ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfValueNotFound, marker, default(ReadOnlySpan<byte>));
			return true;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000C070 File Offset: 0x0000A270
		private bool ConsumeLiteral(ReadOnlySpan<byte> literal, JsonTokenType tokenType)
		{
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed);
			if (!readOnlySpan.StartsWith(literal))
			{
				return this.CheckLiteral(readOnlySpan, literal);
			}
			this.ValueSpan = readOnlySpan.Slice(0, literal.Length);
			this._tokenType = tokenType;
			this._consumed += literal.Length;
			this._bytePositionInLine += (long)literal.Length;
			return true;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		private unsafe bool CheckLiteral(ReadOnlySpan<byte> span, ReadOnlySpan<byte> literal)
		{
			int num = 0;
			for (int i = 1; i < literal.Length; i++)
			{
				if (span.Length <= i)
				{
					num = i;
					break;
				}
				if (*span[i] != *literal[i])
				{
					this._bytePositionInLine += (long)i;
					this.ThrowInvalidLiteral(span);
				}
			}
			if (this.IsLastSpan)
			{
				this._bytePositionInLine += (long)num;
				this.ThrowInvalidLiteral(span);
			}
			return false;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000C164 File Offset: 0x0000A364
		private unsafe void ThrowInvalidLiteral(ReadOnlySpan<byte> span)
		{
			byte b = *span[0];
			ExceptionResource exceptionResource;
			if (b != 102)
			{
				if (b == 116)
				{
					exceptionResource = ExceptionResource.ExpectedTrue;
				}
				else
				{
					exceptionResource = ExceptionResource.ExpectedNull;
				}
			}
			else
			{
				exceptionResource = ExceptionResource.ExpectedFalse;
			}
			ThrowHelper.ThrowJsonReaderException(ref this, exceptionResource, 0, span);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000C19C File Offset: 0x0000A39C
		private unsafe bool ConsumeNumber()
		{
			int num;
			if (!this.TryGetNumber(this._buffer.Slice(this._consumed), out num))
			{
				return false;
			}
			this._tokenType = JsonTokenType.Number;
			this._consumed += num;
			this._bytePositionInLine += (long)num;
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length) && this._isNotPrimitive)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndOfDigitNotFound, *this._buffer[this._consumed - 1], default(ReadOnlySpan<byte>));
			}
			return true;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000C22C File Offset: 0x0000A42C
		private unsafe bool ConsumePropertyName()
		{
			this._trailingCommaBeforeComment = false;
			if (!this.ConsumeString())
			{
				return false;
			}
			if (!this.HasMoreData(ExceptionResource.ExpectedValueAfterPropertyNameNotFound))
			{
				return false;
			}
			byte b = *this._buffer[this._consumed];
			if (b <= 32)
			{
				this.SkipWhiteSpace();
				if (!this.HasMoreData(ExceptionResource.ExpectedValueAfterPropertyNameNotFound))
				{
					return false;
				}
				b = *this._buffer[this._consumed];
			}
			if (b != 58)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedSeparatorAfterPropertyNameNotFound, b, default(ReadOnlySpan<byte>));
			}
			this._consumed++;
			this._bytePositionInLine += 1L;
			this._tokenType = JsonTokenType.PropertyName;
			return true;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		private unsafe bool ConsumeString()
		{
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed + 1);
			int num = readOnlySpan.IndexOfQuoteOrAnyControlOrBackSlash();
			if (num < 0)
			{
				if (this.IsLastSpan)
				{
					this._bytePositionInLine += (long)(readOnlySpan.Length + 1);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
				}
				return false;
			}
			byte b = *readOnlySpan[num];
			if (b == 34)
			{
				this._bytePositionInLine += (long)(num + 2);
				this.ValueSpan = readOnlySpan.Slice(0, num);
				this.ValueIsEscaped = false;
				this._tokenType = JsonTokenType.String;
				this._consumed += num + 2;
				return true;
			}
			return this.ConsumeStringAndValidate(readOnlySpan, num);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000C388 File Offset: 0x0000A588
		private unsafe bool ConsumeStringAndValidate(ReadOnlySpan<byte> data, int idx)
		{
			long bytePositionInLine = this._bytePositionInLine;
			long lineNumber = this._lineNumber;
			this._bytePositionInLine += (long)(idx + 1);
			bool flag = false;
			while (idx < data.Length)
			{
				byte b = *data[idx];
				if (b == 34)
				{
					if (!flag)
					{
						IL_0119:
						this._bytePositionInLine += 1L;
						this.ValueSpan = data.Slice(0, idx);
						this.ValueIsEscaped = true;
						this._tokenType = JsonTokenType.String;
						this._consumed += idx + 2;
						return true;
					}
					flag = false;
				}
				else if (b == 92)
				{
					flag = !flag;
				}
				else if (flag)
				{
					int num = JsonConstants.EscapableChars.IndexOf(b);
					if (num == -1)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterAfterEscapeWithinString, b, default(ReadOnlySpan<byte>));
					}
					if (b == 117)
					{
						this._bytePositionInLine += 1L;
						if (!this.ValidateHexDigits(data, idx + 1))
						{
							idx = data.Length;
							break;
						}
						idx += 4;
					}
					flag = false;
				}
				else if (b < 32)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterWithinString, b, default(ReadOnlySpan<byte>));
				}
				this._bytePositionInLine += 1L;
				idx++;
			}
			if (idx >= data.Length)
			{
				if (this.IsLastSpan)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
				}
				this._lineNumber = lineNumber;
				this._bytePositionInLine = bytePositionInLine;
				return false;
			}
			goto IL_0119;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		private unsafe bool ValidateHexDigits(ReadOnlySpan<byte> data, int idx)
		{
			for (int i = idx; i < data.Length; i++)
			{
				byte b = *data[i];
				if (!JsonReaderHelper.IsHexDigit(b))
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidHexCharacterWithinString, b, default(ReadOnlySpan<byte>));
				}
				if (i - idx >= 3)
				{
					return true;
				}
				this._bytePositionInLine += 1L;
			}
			return false;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C548 File Offset: 0x0000A748
		private unsafe bool TryGetNumber(ReadOnlySpan<byte> data, out int consumed)
		{
			consumed = 0;
			int num = 0;
			ConsumeNumberResult consumeNumberResult = this.ConsumeNegativeSign(ref data, ref num);
			if (consumeNumberResult == ConsumeNumberResult.NeedMoreData)
			{
				return false;
			}
			byte b = *data[num];
			if (b == 48)
			{
				ConsumeNumberResult consumeNumberResult2 = this.ConsumeZero(ref data, ref num);
				if (consumeNumberResult2 == ConsumeNumberResult.NeedMoreData)
				{
					return false;
				}
				if (consumeNumberResult2 == ConsumeNumberResult.Success)
				{
					goto IL_0152;
				}
				b = *data[num];
			}
			else
			{
				num++;
				ConsumeNumberResult consumeNumberResult3 = this.ConsumeIntegerDigits(ref data, ref num);
				if (consumeNumberResult3 == ConsumeNumberResult.NeedMoreData)
				{
					return false;
				}
				if (consumeNumberResult3 == ConsumeNumberResult.Success)
				{
					goto IL_0152;
				}
				b = *data[num];
				if (b != 46 && b != 69 && b != 101)
				{
					this._bytePositionInLine += (long)num;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndOfDigitNotFound, b, default(ReadOnlySpan<byte>));
				}
			}
			if (b == 46)
			{
				num++;
				ConsumeNumberResult consumeNumberResult4 = this.ConsumeDecimalDigits(ref data, ref num);
				if (consumeNumberResult4 == ConsumeNumberResult.NeedMoreData)
				{
					return false;
				}
				if (consumeNumberResult4 == ConsumeNumberResult.Success)
				{
					goto IL_0152;
				}
				b = *data[num];
				if (b != 69 && b != 101)
				{
					this._bytePositionInLine += (long)num;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedNextDigitEValueNotFound, b, default(ReadOnlySpan<byte>));
				}
			}
			num++;
			consumeNumberResult = this.ConsumeSign(ref data, ref num);
			if (consumeNumberResult == ConsumeNumberResult.NeedMoreData)
			{
				return false;
			}
			num++;
			ConsumeNumberResult consumeNumberResult5 = this.ConsumeIntegerDigits(ref data, ref num);
			if (consumeNumberResult5 == ConsumeNumberResult.NeedMoreData)
			{
				return false;
			}
			if (consumeNumberResult5 != ConsumeNumberResult.Success)
			{
				this._bytePositionInLine += (long)num;
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndOfDigitNotFound, *data[num], default(ReadOnlySpan<byte>));
			}
			IL_0152:
			this.ValueSpan = data.Slice(0, num);
			consumed = num;
			return true;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		private unsafe ConsumeNumberResult ConsumeNegativeSign(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i)
		{
			byte b = *data[i];
			if (b == 45)
			{
				i++;
				if (i >= data.Length)
				{
					if (this.IsLastSpan)
					{
						this._bytePositionInLine += (long)i;
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
					}
					return ConsumeNumberResult.NeedMoreData;
				}
				b = *data[i];
				if (!JsonHelpers.IsDigit(b))
				{
					this._bytePositionInLine += (long)i;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundAfterSign, b, default(ReadOnlySpan<byte>));
				}
			}
			return ConsumeNumberResult.OperationIncomplete;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C748 File Offset: 0x0000A948
		private unsafe ConsumeNumberResult ConsumeZero(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i)
		{
			i++;
			if (i < data.Length)
			{
				byte b = *data[i];
				if (JsonConstants.Delimiters.IndexOf(b) >= 0)
				{
					return ConsumeNumberResult.Success;
				}
				b = *data[i];
				if (b != 46 && b != 69 && b != 101)
				{
					this._bytePositionInLine += (long)i;
					ThrowHelper.ThrowJsonReaderException(ref this, JsonHelpers.IsInRangeInclusive((int)b, 48, 57) ? ExceptionResource.InvalidLeadingZeroInNumber : ExceptionResource.ExpectedEndOfDigitNotFound, b, default(ReadOnlySpan<byte>));
				}
				return ConsumeNumberResult.OperationIncomplete;
			}
			else
			{
				if (this.IsLastSpan)
				{
					return ConsumeNumberResult.Success;
				}
				return ConsumeNumberResult.NeedMoreData;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		private unsafe ConsumeNumberResult ConsumeIntegerDigits(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i)
		{
			byte b = 0;
			while (i < data.Length)
			{
				b = *data[i];
				if (!JsonHelpers.IsDigit(b))
				{
					break;
				}
				i++;
			}
			if (i >= data.Length)
			{
				if (this.IsLastSpan)
				{
					return ConsumeNumberResult.Success;
				}
				return ConsumeNumberResult.NeedMoreData;
			}
			else
			{
				if (JsonConstants.Delimiters.IndexOf(b) >= 0)
				{
					return ConsumeNumberResult.Success;
				}
				return ConsumeNumberResult.OperationIncomplete;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C834 File Offset: 0x0000AA34
		private unsafe ConsumeNumberResult ConsumeDecimalDigits(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i)
		{
			if (i >= data.Length)
			{
				if (this.IsLastSpan)
				{
					this._bytePositionInLine += (long)i;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
				}
				return ConsumeNumberResult.NeedMoreData;
			}
			byte b = *data[i];
			if (!JsonHelpers.IsDigit(b))
			{
				this._bytePositionInLine += (long)i;
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundAfterDecimal, b, default(ReadOnlySpan<byte>));
			}
			i++;
			return this.ConsumeIntegerDigits(ref data, ref i);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		private unsafe ConsumeNumberResult ConsumeSign(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i)
		{
			if (i >= data.Length)
			{
				if (this.IsLastSpan)
				{
					this._bytePositionInLine += (long)i;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
				}
				return ConsumeNumberResult.NeedMoreData;
			}
			byte b = *data[i];
			if (b == 43 || b == 45)
			{
				i++;
				if (i >= data.Length)
				{
					if (this.IsLastSpan)
					{
						this._bytePositionInLine += (long)i;
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
					}
					return ConsumeNumberResult.NeedMoreData;
				}
				b = *data[i];
			}
			if (!JsonHelpers.IsDigit(b))
			{
				this._bytePositionInLine += (long)i;
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundAfterSign, b, default(ReadOnlySpan<byte>));
			}
			return ConsumeNumberResult.OperationIncomplete;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C980 File Offset: 0x0000AB80
		private bool ConsumeNextTokenOrRollback(byte marker)
		{
			int consumed = this._consumed;
			long bytePositionInLine = this._bytePositionInLine;
			long lineNumber = this._lineNumber;
			JsonTokenType tokenType = this._tokenType;
			bool trailingCommaBeforeComment = this._trailingCommaBeforeComment;
			ConsumeTokenResult consumeTokenResult = this.ConsumeNextToken(marker);
			if (consumeTokenResult == ConsumeTokenResult.Success)
			{
				return true;
			}
			if (consumeTokenResult == ConsumeTokenResult.NotEnoughDataRollBackState)
			{
				this._consumed = consumed;
				this._tokenType = tokenType;
				this._bytePositionInLine = bytePositionInLine;
				this._lineNumber = lineNumber;
				this._trailingCommaBeforeComment = trailingCommaBeforeComment;
			}
			return false;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		private unsafe ConsumeTokenResult ConsumeNextToken(byte marker)
		{
			if (this._readerOptions.CommentHandling != JsonCommentHandling.Disallow)
			{
				if (this._readerOptions.CommentHandling != JsonCommentHandling.Allow)
				{
					return this.ConsumeNextTokenUntilAfterAllCommentsAreSkipped(marker);
				}
				if (marker == 47)
				{
					if (!this.ConsumeComment())
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					return ConsumeTokenResult.Success;
				}
				else if (this._tokenType == JsonTokenType.Comment)
				{
					return this.ConsumeNextTokenFromLastNonCommentToken();
				}
			}
			if (this._bitStack.CurrentDepth == 0)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndAfterSingleJson, marker, default(ReadOnlySpan<byte>));
			}
			if (marker != 44)
			{
				if (marker == 125)
				{
					this.EndObject();
				}
				else if (marker == 93)
				{
					this.EndArray();
				}
				else
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.FoundInvalidCharacter, marker, default(ReadOnlySpan<byte>));
				}
				return ConsumeTokenResult.Success;
			}
			this._consumed++;
			this._bytePositionInLine += 1L;
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
			{
				if (this.IsLastSpan)
				{
					this._consumed--;
					this._bytePositionInLine -= 1L;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
				}
				return ConsumeTokenResult.NotEnoughDataRollBackState;
			}
			byte b = *this._buffer[this._consumed];
			if (b <= 32)
			{
				this.SkipWhiteSpace();
				if (!this.HasMoreData(ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				b = *this._buffer[this._consumed];
			}
			this.TokenStartIndex = (long)this._consumed;
			if (this._readerOptions.CommentHandling == JsonCommentHandling.Allow && b == 47)
			{
				this._trailingCommaBeforeComment = true;
				if (!this.ConsumeComment())
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
			else if (this._inObject)
			{
				if (b != 34)
				{
					if (b == 125)
					{
						if (this._readerOptions.AllowTrailingCommas)
						{
							this.EndObject();
							return ConsumeTokenResult.Success;
						}
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
					}
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
				}
				if (!this.ConsumePropertyName())
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
			else
			{
				if (b == 93)
				{
					if (this._readerOptions.AllowTrailingCommas)
					{
						this.EndArray();
						return ConsumeTokenResult.Success;
					}
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.ConsumeValue(b))
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		private unsafe ConsumeTokenResult ConsumeNextTokenFromLastNonCommentToken()
		{
			if (JsonReaderHelper.IsTokenTypePrimitive(this._previousTokenType))
			{
				this._tokenType = (this._inObject ? JsonTokenType.StartObject : JsonTokenType.StartArray);
			}
			else
			{
				this._tokenType = this._previousTokenType;
			}
			if (this.HasMoreData())
			{
				byte b = *this._buffer[this._consumed];
				if (b <= 32)
				{
					this.SkipWhiteSpace();
					if (!this.HasMoreData())
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					b = *this._buffer[this._consumed];
				}
				if (this._bitStack.CurrentDepth == 0 && this._tokenType != JsonTokenType.None)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndAfterSingleJson, b, default(ReadOnlySpan<byte>));
				}
				this.TokenStartIndex = (long)this._consumed;
				if (b == 44)
				{
					if (this._previousTokenType <= JsonTokenType.StartObject || this._previousTokenType == JsonTokenType.StartArray || this._trailingCommaBeforeComment)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueAfterComment, b, default(ReadOnlySpan<byte>));
					}
					this._consumed++;
					this._bytePositionInLine += 1L;
					if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
					{
						if (this.IsLastSpan)
						{
							this._consumed--;
							this._bytePositionInLine -= 1L;
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					else
					{
						b = *this._buffer[this._consumed];
						if (b <= 32)
						{
							this.SkipWhiteSpace();
							if (!this.HasMoreData(ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
							{
								return ConsumeTokenResult.NotEnoughDataRollBackState;
							}
							b = *this._buffer[this._consumed];
						}
						this.TokenStartIndex = (long)this._consumed;
						if (b == 47)
						{
							this._trailingCommaBeforeComment = true;
							if (!this.ConsumeComment())
							{
								return ConsumeTokenResult.NotEnoughDataRollBackState;
							}
						}
						else if (this._inObject)
						{
							if (b != 34)
							{
								if (b == 125)
								{
									if (this._readerOptions.AllowTrailingCommas)
									{
										this.EndObject();
										return ConsumeTokenResult.Success;
									}
									ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
								}
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
							}
							if (!this.ConsumePropertyName())
							{
								return ConsumeTokenResult.NotEnoughDataRollBackState;
							}
						}
						else
						{
							if (b == 93)
							{
								if (this._readerOptions.AllowTrailingCommas)
								{
									this.EndArray();
									return ConsumeTokenResult.Success;
								}
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
							}
							if (!this.ConsumeValue(b))
							{
								return ConsumeTokenResult.NotEnoughDataRollBackState;
							}
						}
					}
				}
				else if (b == 125)
				{
					this.EndObject();
				}
				else if (b == 93)
				{
					this.EndArray();
				}
				else if (this._tokenType == JsonTokenType.None)
				{
					if (!this.ReadFirstToken(b))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._tokenType == JsonTokenType.StartObject)
				{
					if (b != 34)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
					}
					int consumed = this._consumed;
					long bytePositionInLine = this._bytePositionInLine;
					long lineNumber = this._lineNumber;
					if (!this.ConsumePropertyName())
					{
						this._consumed = consumed;
						this._tokenType = JsonTokenType.StartObject;
						this._bytePositionInLine = bytePositionInLine;
						this._lineNumber = lineNumber;
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._tokenType == JsonTokenType.StartArray)
				{
					if (!this.ConsumeValue(b))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._tokenType == JsonTokenType.PropertyName)
				{
					if (!this.ConsumeValue(b))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._inObject)
				{
					if (b != 34)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
					}
					if (!this.ConsumePropertyName())
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (!this.ConsumeValue(b))
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
			return ConsumeTokenResult.NotEnoughDataRollBackState;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000CF50 File Offset: 0x0000B150
		private unsafe bool SkipAllComments([ScopedRef] ref byte marker)
		{
			while (marker == 47)
			{
				if (this.SkipComment() && this.HasMoreData())
				{
					marker = *this._buffer[this._consumed];
					if (marker > 32)
					{
						continue;
					}
					this.SkipWhiteSpace();
					if (this.HasMoreData())
					{
						marker = *this._buffer[this._consumed];
						continue;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		private unsafe bool SkipAllComments([ScopedRef] ref byte marker, ExceptionResource resource)
		{
			while (marker == 47)
			{
				if (this.SkipComment() && this.HasMoreData(resource))
				{
					marker = *this._buffer[this._consumed];
					if (marker > 32)
					{
						continue;
					}
					this.SkipWhiteSpace();
					if (this.HasMoreData(resource))
					{
						marker = *this._buffer[this._consumed];
						continue;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D01C File Offset: 0x0000B21C
		private unsafe ConsumeTokenResult ConsumeNextTokenUntilAfterAllCommentsAreSkipped(byte marker)
		{
			if (this.SkipAllComments(ref marker))
			{
				this.TokenStartIndex = (long)this._consumed;
				if (this._tokenType == JsonTokenType.StartObject)
				{
					if (marker == 125)
					{
						this.EndObject();
					}
					else
					{
						if (marker != 34)
						{
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, marker, default(ReadOnlySpan<byte>));
						}
						int consumed = this._consumed;
						long bytePositionInLine = this._bytePositionInLine;
						long lineNumber = this._lineNumber;
						if (!this.ConsumePropertyName())
						{
							this._consumed = consumed;
							this._tokenType = JsonTokenType.StartObject;
							this._bytePositionInLine = bytePositionInLine;
							this._lineNumber = lineNumber;
							return ConsumeTokenResult.IncompleteNoRollBackNecessary;
						}
					}
				}
				else if (this._tokenType == JsonTokenType.StartArray)
				{
					if (marker == 93)
					{
						this.EndArray();
					}
					else if (!this.ConsumeValue(marker))
					{
						return ConsumeTokenResult.IncompleteNoRollBackNecessary;
					}
				}
				else if (this._tokenType == JsonTokenType.PropertyName)
				{
					if (!this.ConsumeValue(marker))
					{
						return ConsumeTokenResult.IncompleteNoRollBackNecessary;
					}
				}
				else if (this._bitStack.CurrentDepth == 0)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndAfterSingleJson, marker, default(ReadOnlySpan<byte>));
				}
				else if (marker == 44)
				{
					this._consumed++;
					this._bytePositionInLine += 1L;
					if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
					{
						if (this.IsLastSpan)
						{
							this._consumed--;
							this._bytePositionInLine -= 1L;
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
						}
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					marker = *this._buffer[this._consumed];
					if (marker <= 32)
					{
						this.SkipWhiteSpace();
						if (!this.HasMoreData(ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						marker = *this._buffer[this._consumed];
					}
					if (!this.SkipAllComments(ref marker, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					this.TokenStartIndex = (long)this._consumed;
					if (this._inObject)
					{
						if (marker != 34)
						{
							if (marker == 125)
							{
								if (this._readerOptions.AllowTrailingCommas)
								{
									this.EndObject();
									return ConsumeTokenResult.Success;
								}
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
							}
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, marker, default(ReadOnlySpan<byte>));
						}
						if (!this.ConsumePropertyName())
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						return ConsumeTokenResult.Success;
					}
					else
					{
						if (marker == 93)
						{
							if (this._readerOptions.AllowTrailingCommas)
							{
								this.EndArray();
								return ConsumeTokenResult.Success;
							}
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
						}
						if (!this.ConsumeValue(marker))
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						return ConsumeTokenResult.Success;
					}
				}
				else if (marker == 125)
				{
					this.EndObject();
				}
				else if (marker == 93)
				{
					this.EndArray();
				}
				else
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.FoundInvalidCharacter, marker, default(ReadOnlySpan<byte>));
				}
				return ConsumeTokenResult.Success;
			}
			return ConsumeTokenResult.IncompleteNoRollBackNecessary;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		private unsafe bool SkipComment()
		{
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed + 1);
			if (readOnlySpan.Length > 0)
			{
				byte b = *readOnlySpan[0];
				if (b == 47)
				{
					int num;
					return this.SkipSingleLineComment(readOnlySpan.Slice(1), out num);
				}
				if (b == 42)
				{
					int num;
					return this.SkipMultiLineComment(readOnlySpan.Slice(1), out num);
				}
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfValueNotFound, 47, default(ReadOnlySpan<byte>));
			}
			if (this.IsLastSpan)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfValueNotFound, 47, default(ReadOnlySpan<byte>));
			}
			return false;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D340 File Offset: 0x0000B540
		private unsafe bool SkipSingleLineComment(ReadOnlySpan<byte> localBuffer, out int idx)
		{
			idx = this.FindLineSeparator(localBuffer);
			int num;
			if (idx != -1)
			{
				num = idx;
				if (*localBuffer[idx] != 10)
				{
					if (idx < localBuffer.Length - 1)
					{
						if (*localBuffer[idx + 1] == 10)
						{
							num++;
						}
					}
					else if (!this.IsLastSpan)
					{
						return false;
					}
				}
				num++;
				this._bytePositionInLine = 0L;
				this._lineNumber += 1L;
			}
			else
			{
				if (!this.IsLastSpan)
				{
					return false;
				}
				idx = localBuffer.Length;
				num = idx;
				this._bytePositionInLine += (long)(2 + localBuffer.Length);
			}
			this._consumed += 2 + num;
			return true;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		private unsafe int FindLineSeparator(ReadOnlySpan<byte> localBuffer)
		{
			int num = 0;
			for (;;)
			{
				int num2 = localBuffer.IndexOfAny(10, 13, 226);
				if (num2 == -1)
				{
					break;
				}
				num += num2;
				if (*localBuffer[num2] != 226)
				{
					return num;
				}
				num++;
				localBuffer = localBuffer.Slice(num2 + 1);
				this.ThrowOnDangerousLineSeparator(localBuffer);
			}
			return -1;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000D448 File Offset: 0x0000B648
		private unsafe void ThrowOnDangerousLineSeparator(ReadOnlySpan<byte> localBuffer)
		{
			if (localBuffer.Length < 2)
			{
				return;
			}
			byte b = *localBuffer[1];
			if (*localBuffer[0] == 128 && (b == 168 || b == 169))
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfLineSeparator, 0, default(ReadOnlySpan<byte>));
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000D49C File Offset: 0x0000B69C
		private unsafe bool SkipMultiLineComment(ReadOnlySpan<byte> localBuffer, out int idx)
		{
			idx = 0;
			int num;
			for (;;)
			{
				num = localBuffer.Slice(idx).IndexOf(47);
				if (num == -1)
				{
					break;
				}
				if (num != 0 && *localBuffer[num + idx - 1] == 42)
				{
					goto Block_4;
				}
				idx += num + 1;
			}
			if (this.IsLastSpan)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfCommentNotFound, 0, default(ReadOnlySpan<byte>));
			}
			return false;
			Block_4:
			idx += num - 1;
			this._consumed += 4 + idx;
			global::System.ValueTuple<int, int> valueTuple = JsonReaderHelper.CountNewLines(localBuffer.Slice(0, idx));
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			this._lineNumber += (long)item;
			if (item2 != -1)
			{
				this._bytePositionInLine = (long)(idx - item2 + 1);
			}
			else
			{
				this._bytePositionInLine += (long)(4 + idx);
			}
			return true;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D568 File Offset: 0x0000B768
		private unsafe bool ConsumeComment()
		{
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed + 1);
			if (readOnlySpan.Length > 0)
			{
				byte b = *readOnlySpan[0];
				if (b == 47)
				{
					return this.ConsumeSingleLineComment(readOnlySpan.Slice(1), this._consumed);
				}
				if (b == 42)
				{
					return this.ConsumeMultiLineComment(readOnlySpan.Slice(1), this._consumed);
				}
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterAtStartOfComment, b, default(ReadOnlySpan<byte>));
			}
			if (this.IsLastSpan)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
			}
			return false;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000D600 File Offset: 0x0000B800
		private bool ConsumeSingleLineComment(ReadOnlySpan<byte> localBuffer, int previousConsumed)
		{
			int num;
			if (!this.SkipSingleLineComment(localBuffer, out num))
			{
				return false;
			}
			this.ValueSpan = this._buffer.Slice(previousConsumed + 2, num);
			if (this._tokenType != JsonTokenType.Comment)
			{
				this._previousTokenType = this._tokenType;
			}
			this._tokenType = JsonTokenType.Comment;
			return true;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D64C File Offset: 0x0000B84C
		private bool ConsumeMultiLineComment(ReadOnlySpan<byte> localBuffer, int previousConsumed)
		{
			int num;
			if (!this.SkipMultiLineComment(localBuffer, out num))
			{
				return false;
			}
			this.ValueSpan = this._buffer.Slice(previousConsumed + 2, num);
			if (this._tokenType != JsonTokenType.Comment)
			{
				this._previousTokenType = this._tokenType;
			}
			this._tokenType = JsonTokenType.Comment;
			return true;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000D698 File Offset: 0x0000B898
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("TokenType = {0}, TokenStartIndex = {1}, Consumed = {2}", this.DebugTokenType, this.TokenStartIndex, this.BytesConsumed);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		[Nullable(1)]
		private string DebugTokenType
		{
			get
			{
				string text;
				switch (this.TokenType)
				{
				case JsonTokenType.None:
					text = "None";
					break;
				case JsonTokenType.StartObject:
					text = "StartObject";
					break;
				case JsonTokenType.EndObject:
					text = "EndObject";
					break;
				case JsonTokenType.StartArray:
					text = "StartArray";
					break;
				case JsonTokenType.EndArray:
					text = "EndArray";
					break;
				case JsonTokenType.PropertyName:
					text = "PropertyName";
					break;
				case JsonTokenType.Comment:
					text = "Comment";
					break;
				case JsonTokenType.String:
					text = "String";
					break;
				case JsonTokenType.Number:
					text = "Number";
					break;
				case JsonTokenType.True:
					text = "True";
					break;
				case JsonTokenType.False:
					text = "False";
					break;
				case JsonTokenType.Null:
					text = "Null";
					break;
				default:
					text = ((byte)this.TokenType).ToString();
					break;
				}
				return text;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000D77C File Offset: 0x0000B97C
		private ReadOnlySpan<byte> GetUnescapedSpan()
		{
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			if (this.ValueIsEscaped)
			{
				readOnlySpan2 = JsonReaderHelper.GetUnescapedSpan(readOnlySpan2);
			}
			return readOnlySpan2;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public Utf8JsonReader(ReadOnlySequence<byte> jsonData, bool isFinalBlock, JsonReaderState state)
		{
			ReadOnlyMemory<byte> first = jsonData.First;
			this._buffer = first.Span;
			this._isFinalBlock = isFinalBlock;
			this._isInputSequence = true;
			this._lineNumber = state._lineNumber;
			this._bytePositionInLine = state._bytePositionInLine;
			this._inObject = state._inObject;
			this._isNotPrimitive = state._isNotPrimitive;
			this.ValueIsEscaped = state._valueIsEscaped;
			this._trailingCommaBeforeComment = state._trailingCommaBeforeComment;
			this._tokenType = state._tokenType;
			this._previousTokenType = state._previousTokenType;
			this._readerOptions = state._readerOptions;
			if (this._readerOptions.MaxDepth == 0)
			{
				this._readerOptions.MaxDepth = 64;
			}
			this._bitStack = state._bitStack;
			this._consumed = 0;
			this.TokenStartIndex = 0L;
			this._totalConsumed = 0L;
			this.ValueSpan = ReadOnlySpan<byte>.Empty;
			this._sequence = jsonData;
			this.HasValueSequence = false;
			this.ValueSequence = ReadOnlySequence<byte>.Empty;
			if (jsonData.IsSingleSegment)
			{
				this._nextPosition = default(SequencePosition);
				this._currentPosition = jsonData.Start;
				this._isLastSegment = isFinalBlock;
				this._isMultiSegment = false;
				return;
			}
			this._currentPosition = jsonData.Start;
			this._nextPosition = this._currentPosition;
			bool flag = this._buffer.Length == 0;
			if (flag)
			{
				SequencePosition sequencePosition = this._nextPosition;
				ReadOnlyMemory<byte> readOnlyMemory;
				while (jsonData.TryGet(ref this._nextPosition, out readOnlyMemory, true))
				{
					this._currentPosition = sequencePosition;
					if (readOnlyMemory.Length != 0)
					{
						this._buffer = readOnlyMemory.Span;
						break;
					}
					sequencePosition = this._nextPosition;
				}
			}
			this._isLastSegment = !jsonData.TryGet(ref this._nextPosition, out first, !flag) && isFinalBlock;
			this._isMultiSegment = true;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000D97F File Offset: 0x0000BB7F
		public Utf8JsonReader(ReadOnlySequence<byte> jsonData, JsonReaderOptions options = default(JsonReaderOptions))
		{
			this = new Utf8JsonReader(jsonData, true, new JsonReaderState(options));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D990 File Offset: 0x0000BB90
		private unsafe bool ReadMultiSegment()
		{
			bool flag = false;
			this.HasValueSequence = false;
			this.ValueIsEscaped = false;
			this.ValueSpan = default(ReadOnlySpan<byte>);
			this.ValueSequence = default(ReadOnlySequence<byte>);
			if (this.HasMoreDataMultiSegment())
			{
				byte b = *this._buffer[this._consumed];
				if (b <= 32)
				{
					this.SkipWhiteSpaceMultiSegment();
					if (!this.HasMoreDataMultiSegment())
					{
						return flag;
					}
					b = *this._buffer[this._consumed];
				}
				this.TokenStartIndex = this.BytesConsumed;
				if (this._tokenType != JsonTokenType.None)
				{
					if (b == 47)
					{
						flag = this.ConsumeNextTokenOrRollbackMultiSegment(b);
					}
					else
					{
						if (this._tokenType == JsonTokenType.StartObject)
						{
							if (b == 125)
							{
								this.EndObject();
							}
							else
							{
								if (b != 34)
								{
									ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
								}
								long totalConsumed = this._totalConsumed;
								int consumed = this._consumed;
								long bytePositionInLine = this._bytePositionInLine;
								long lineNumber = this._lineNumber;
								SequencePosition currentPosition = this._currentPosition;
								flag = this.ConsumePropertyNameMultiSegment();
								if (!flag)
								{
									this._consumed = consumed;
									this._tokenType = JsonTokenType.StartObject;
									this._bytePositionInLine = bytePositionInLine;
									this._lineNumber = lineNumber;
									this._totalConsumed = totalConsumed;
									this._currentPosition = currentPosition;
									return flag;
								}
								return flag;
							}
						}
						else if (this._tokenType == JsonTokenType.StartArray)
						{
							if (b != 93)
							{
								return this.ConsumeValueMultiSegment(b);
							}
							this.EndArray();
						}
						else
						{
							if (this._tokenType == JsonTokenType.PropertyName)
							{
								return this.ConsumeValueMultiSegment(b);
							}
							return this.ConsumeNextTokenOrRollbackMultiSegment(b);
						}
						flag = true;
					}
				}
				else
				{
					flag = this.ReadFirstTokenMultiSegment(b);
				}
			}
			return flag;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		private bool ValidateStateAtEndOfData()
		{
			if (this._bitStack.CurrentDepth != 0)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ZeroDepthAtEnd, 0, default(ReadOnlySpan<byte>));
			}
			if (this._readerOptions.CommentHandling == JsonCommentHandling.Allow && this._tokenType == JsonTokenType.Comment)
			{
				return false;
			}
			if (this._tokenType != JsonTokenType.EndArray && this._tokenType != JsonTokenType.EndObject)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidEndOfJsonNonPrimitive, 0, default(ReadOnlySpan<byte>));
			}
			return true;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000DB88 File Offset: 0x0000BD88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HasMoreDataMultiSegment()
		{
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
			{
				if (this._isNotPrimitive && this.IsLastSpan && !this.ValidateStateAtEndOfData())
				{
					return false;
				}
				if (!this.GetNextSpan())
				{
					if (this._isNotPrimitive && this.IsLastSpan)
					{
						this.ValidateStateAtEndOfData();
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HasMoreDataMultiSegment(ExceptionResource resource)
		{
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
			{
				if (this.IsLastSpan)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, resource, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.GetNextSpan())
				{
					if (this.IsLastSpan)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, resource, 0, default(ReadOnlySpan<byte>));
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000DC48 File Offset: 0x0000BE48
		private bool GetNextSpan()
		{
			SequencePosition currentPosition;
			ReadOnlyMemory<byte> readOnlyMemory;
			for (;;)
			{
				currentPosition = this._currentPosition;
				this._currentPosition = this._nextPosition;
				bool flag = !this._sequence.TryGet(ref this._nextPosition, out readOnlyMemory, true);
				if (flag)
				{
					break;
				}
				if (readOnlyMemory.Length != 0)
				{
					goto IL_0050;
				}
				this._currentPosition = currentPosition;
			}
			this._currentPosition = currentPosition;
			this._isLastSegment = true;
			return false;
			IL_0050:
			if (this._isFinalBlock)
			{
				ReadOnlyMemory<byte> readOnlyMemory2;
				this._isLastSegment = !this._sequence.TryGet(ref this._nextPosition, out readOnlyMemory2, false);
			}
			this._buffer = readOnlyMemory.Span;
			this._totalConsumed += (long)this._consumed;
			this._consumed = 0;
			return true;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		private bool ReadFirstTokenMultiSegment(byte first)
		{
			if (first == 123)
			{
				this._bitStack.SetFirstBit();
				this._tokenType = JsonTokenType.StartObject;
				this.ValueSpan = this._buffer.Slice(this._consumed, 1);
				this._consumed++;
				this._bytePositionInLine += 1L;
				this._inObject = true;
				this._isNotPrimitive = true;
			}
			else if (first == 91)
			{
				this._bitStack.ResetFirstBit();
				this._tokenType = JsonTokenType.StartArray;
				this.ValueSpan = this._buffer.Slice(this._consumed, 1);
				this._consumed++;
				this._bytePositionInLine += 1L;
				this._isNotPrimitive = true;
			}
			else if (JsonHelpers.IsDigit(first) || first == 45)
			{
				int num;
				if (!this.TryGetNumberMultiSegment(this._buffer.Slice(this._consumed), out num))
				{
					return false;
				}
				this._tokenType = JsonTokenType.Number;
				this._consumed += num;
				return true;
			}
			else
			{
				if (!this.ConsumeValueMultiSegment(first))
				{
					return false;
				}
				if (this._tokenType == JsonTokenType.StartObject || this._tokenType == JsonTokenType.StartArray)
				{
					this._isNotPrimitive = true;
				}
			}
			return true;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000DE1B File Offset: 0x0000C01B
		private void SkipWhiteSpaceMultiSegment()
		{
			do
			{
				this.SkipWhiteSpace();
			}
			while (this._consumed >= this._buffer.Length && this.GetNextSpan());
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000DE40 File Offset: 0x0000C040
		private unsafe bool ConsumeValueMultiSegment(byte marker)
		{
			SequencePosition currentPosition;
			for (;;)
			{
				this._trailingCommaBeforeComment = false;
				if (marker == 34)
				{
					break;
				}
				if (marker == 123)
				{
					goto Block_1;
				}
				if (marker == 91)
				{
					goto Block_2;
				}
				if (JsonHelpers.IsDigit(marker) || marker == 45)
				{
					goto IL_0040;
				}
				if (marker == 102)
				{
					goto Block_4;
				}
				if (marker == 116)
				{
					goto Block_5;
				}
				if (marker == 110)
				{
					goto Block_6;
				}
				JsonCommentHandling commentHandling = this._readerOptions.CommentHandling;
				if (commentHandling == JsonCommentHandling.Disallow)
				{
					goto IL_01C2;
				}
				if (commentHandling == JsonCommentHandling.Allow)
				{
					goto Block_8;
				}
				if (marker != 47)
				{
					goto IL_01C2;
				}
				currentPosition = this._currentPosition;
				int num;
				if (!this.SkipCommentMultiSegment(out num))
				{
					goto IL_01B9;
				}
				if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
				{
					if (this._isNotPrimitive && this.IsLastSpan && this._tokenType != JsonTokenType.EndArray && this._tokenType != JsonTokenType.EndObject)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidEndOfJsonNonPrimitive, 0, default(ReadOnlySpan<byte>));
					}
					if (!this.GetNextSpan())
					{
						goto Block_18;
					}
				}
				marker = *this._buffer[this._consumed];
				if (marker <= 32)
				{
					this.SkipWhiteSpaceMultiSegment();
					if (!this.HasMoreDataMultiSegment())
					{
						goto Block_24;
					}
					marker = *this._buffer[this._consumed];
				}
				this.TokenStartIndex = this.BytesConsumed;
			}
			return this.ConsumeStringMultiSegment();
			Block_1:
			this.StartObject();
			return true;
			Block_2:
			this.StartArray();
			return true;
			IL_0040:
			return this.ConsumeNumberMultiSegment();
			Block_4:
			return this.ConsumeLiteralMultiSegment(JsonConstants.FalseValue, JsonTokenType.False);
			Block_5:
			return this.ConsumeLiteralMultiSegment(JsonConstants.TrueValue, JsonTokenType.True);
			Block_6:
			return this.ConsumeLiteralMultiSegment(JsonConstants.NullValue, JsonTokenType.Null);
			Block_8:
			if (marker != 47)
			{
				goto IL_01C2;
			}
			SequencePosition currentPosition2 = this._currentPosition;
			if (!this.SkipOrConsumeCommentMultiSegmentWithRollback())
			{
				this._currentPosition = currentPosition2;
				return false;
			}
			return true;
			Block_18:
			if (this._isNotPrimitive && this.IsLastSpan && this._tokenType != JsonTokenType.EndArray && this._tokenType != JsonTokenType.EndObject)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidEndOfJsonNonPrimitive, 0, default(ReadOnlySpan<byte>));
			}
			this._currentPosition = currentPosition;
			return false;
			Block_24:
			this._currentPosition = currentPosition;
			return false;
			IL_01B9:
			this._currentPosition = currentPosition;
			return false;
			IL_01C2:
			ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfValueNotFound, marker, default(ReadOnlySpan<byte>));
			return true;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E024 File Offset: 0x0000C224
		private bool ConsumeLiteralMultiSegment(ReadOnlySpan<byte> literal, JsonTokenType tokenType)
		{
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed);
			int length = literal.Length;
			if (!readOnlySpan.StartsWith(literal))
			{
				int consumed = this._consumed;
				if (!this.CheckLiteralMultiSegment(readOnlySpan, literal, out length))
				{
					this._consumed = consumed;
					return false;
				}
			}
			else
			{
				this.ValueSpan = readOnlySpan.Slice(0, literal.Length);
				this.HasValueSequence = false;
			}
			this._tokenType = tokenType;
			this._consumed += length;
			this._bytePositionInLine += (long)length;
			return true;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		private unsafe bool CheckLiteralMultiSegment(ReadOnlySpan<byte> span, ReadOnlySpan<byte> literal, out int consumed)
		{
			Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)5], 5);
			Span<byte> span3 = span2;
			int num = 0;
			long totalConsumed = this._totalConsumed;
			SequencePosition currentPosition = this._currentPosition;
			if (span.Length >= literal.Length || this.IsLastSpan)
			{
				this._bytePositionInLine += (long)Utf8JsonReader.FindMismatch(span, literal);
				int num2 = Math.Min(span.Length, (int)this._bytePositionInLine + 1);
				span.Slice(0, num2).CopyTo(span3);
				num += num2;
			}
			else if (!literal.StartsWith(span))
			{
				this._bytePositionInLine += (long)Utf8JsonReader.FindMismatch(span, literal);
				int num3 = Math.Min(span.Length, (int)this._bytePositionInLine + 1);
				span.Slice(0, num3).CopyTo(span3);
				num += num3;
			}
			else
			{
				ReadOnlySpan<byte> readOnlySpan = literal.Slice(span.Length);
				SequencePosition currentPosition2 = this._currentPosition;
				int consumed2 = this._consumed;
				int num4 = literal.Length - readOnlySpan.Length;
				int num5;
				for (;;)
				{
					this._totalConsumed += (long)num4;
					this._bytePositionInLine += (long)num4;
					if (!this.GetNextSpan())
					{
						break;
					}
					num5 = Math.Min(span.Length, span3.Length - num);
					span.Slice(0, num5).CopyTo(span3.Slice(num));
					num += num5;
					span = this._buffer;
					if (span.StartsWith(readOnlySpan))
					{
						goto Block_5;
					}
					if (!readOnlySpan.StartsWith(span))
					{
						goto Block_6;
					}
					readOnlySpan = readOnlySpan.Slice(span.Length);
					num4 = span.Length;
				}
				this._totalConsumed = totalConsumed;
				consumed = 0;
				this._currentPosition = currentPosition;
				if (!this.IsLastSpan)
				{
					return false;
				}
				goto IL_026F;
				Block_5:
				this.HasValueSequence = true;
				SequencePosition sequencePosition = new SequencePosition(currentPosition2.GetObject(), currentPosition2.GetInteger() + consumed2);
				SequencePosition sequencePosition2 = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + readOnlySpan.Length);
				this.ValueSequence = this._sequence.Slice(sequencePosition, sequencePosition2);
				consumed = readOnlySpan.Length;
				return true;
				Block_6:
				this._bytePositionInLine += (long)Utf8JsonReader.FindMismatch(span, readOnlySpan);
				num5 = Math.Min(span.Length, (int)this._bytePositionInLine + 1);
				span.Slice(0, num5).CopyTo(span3.Slice(num));
				num += num5;
			}
			IL_026F:
			this._totalConsumed = totalConsumed;
			consumed = 0;
			this._currentPosition = currentPosition;
			throw this.GetInvalidLiteralMultiSegment(span3.Slice(0, num).ToArray());
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E360 File Offset: 0x0000C560
		private unsafe static int FindMismatch(ReadOnlySpan<byte> span, ReadOnlySpan<byte> literal)
		{
			int num = Math.Min(span.Length, literal.Length);
			int num2 = 0;
			while (num2 < num && *span[num2] == *literal[num2])
			{
				num2++;
			}
			return num2;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		private unsafe JsonException GetInvalidLiteralMultiSegment(ReadOnlySpan<byte> span)
		{
			byte b = *span[0];
			ExceptionResource exceptionResource;
			if (b != 102)
			{
				if (b == 116)
				{
					exceptionResource = ExceptionResource.ExpectedTrue;
				}
				else
				{
					exceptionResource = ExceptionResource.ExpectedNull;
				}
			}
			else
			{
				exceptionResource = ExceptionResource.ExpectedFalse;
			}
			return ThrowHelper.GetJsonReaderException(ref this, exceptionResource, 0, span);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		private unsafe bool ConsumeNumberMultiSegment()
		{
			int num;
			if (!this.TryGetNumberMultiSegment(this._buffer.Slice(this._consumed), out num))
			{
				return false;
			}
			this._tokenType = JsonTokenType.Number;
			this._consumed += num;
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length) && this._isNotPrimitive)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndOfDigitNotFound, *this._buffer[this._consumed - 1], default(ReadOnlySpan<byte>));
			}
			return true;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E460 File Offset: 0x0000C660
		private unsafe bool ConsumePropertyNameMultiSegment()
		{
			this._trailingCommaBeforeComment = false;
			if (!this.ConsumeStringMultiSegment())
			{
				return false;
			}
			if (!this.HasMoreDataMultiSegment(ExceptionResource.ExpectedValueAfterPropertyNameNotFound))
			{
				return false;
			}
			byte b = *this._buffer[this._consumed];
			if (b <= 32)
			{
				this.SkipWhiteSpaceMultiSegment();
				if (!this.HasMoreDataMultiSegment(ExceptionResource.ExpectedValueAfterPropertyNameNotFound))
				{
					return false;
				}
				b = *this._buffer[this._consumed];
			}
			if (b != 58)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedSeparatorAfterPropertyNameNotFound, b, default(ReadOnlySpan<byte>));
			}
			this._consumed++;
			this._bytePositionInLine += 1L;
			this._tokenType = JsonTokenType.PropertyName;
			return true;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E504 File Offset: 0x0000C704
		private unsafe bool ConsumeStringMultiSegment()
		{
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed + 1);
			int num = readOnlySpan.IndexOfQuoteOrAnyControlOrBackSlash();
			if (num < 0)
			{
				if (this.IsLastSpan)
				{
					this._bytePositionInLine += (long)(readOnlySpan.Length + 1);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
				}
				return this.ConsumeStringNextSegment();
			}
			byte b = *readOnlySpan[num];
			if (b == 34)
			{
				this._bytePositionInLine += (long)(num + 2);
				this.ValueSpan = readOnlySpan.Slice(0, num);
				this.HasValueSequence = false;
				this.ValueIsEscaped = false;
				this._tokenType = JsonTokenType.String;
				this._consumed += num + 2;
				return true;
			}
			return this.ConsumeStringAndValidateMultiSegment(readOnlySpan, num);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		private unsafe bool ConsumeStringNextSegment()
		{
			Utf8JsonReader.PartialStateForRollback partialStateForRollback = this.CaptureState();
			this.HasValueSequence = true;
			int num = this._buffer.Length - this._consumed;
			while (this.GetNextSpan())
			{
				ReadOnlySpan<byte> readOnlySpan = this._buffer;
				int num2 = readOnlySpan.IndexOfQuoteOrAnyControlOrBackSlash();
				if (num2 >= 0)
				{
					byte b = *readOnlySpan[num2];
					SequencePosition sequencePosition;
					if (b == 34)
					{
						sequencePosition = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + num2);
						this._bytePositionInLine += (long)(num + num2 + 1);
						this._totalConsumed += (long)num;
						this._consumed = num2 + 1;
						this.ValueIsEscaped = false;
					}
					else
					{
						this._bytePositionInLine += (long)(num + num2);
						this.ValueIsEscaped = true;
						bool flag = false;
						for (;;)
						{
							if (num2 >= readOnlySpan.Length)
							{
								if (!this.GetNextSpan())
								{
									goto Block_18;
								}
								this._totalConsumed += (long)readOnlySpan.Length;
								readOnlySpan = this._buffer;
								num2 = 0;
							}
							else
							{
								byte b2 = *readOnlySpan[num2];
								if (b2 == 34)
								{
									if (!flag)
									{
										goto IL_02FE;
									}
									flag = false;
								}
								else if (b2 == 92)
								{
									flag = !flag;
								}
								else if (flag)
								{
									int num3 = JsonConstants.EscapableChars.IndexOf(b2);
									if (num3 == -1)
									{
										this.RollBackState(in partialStateForRollback, true);
										ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterAfterEscapeWithinString, b2, default(ReadOnlySpan<byte>));
									}
									if (b2 == 117)
									{
										this._bytePositionInLine += 1L;
										int num4 = 0;
										int num5 = num2 + 1;
										for (;;)
										{
											if (num5 >= readOnlySpan.Length)
											{
												if (!this.GetNextSpan())
												{
													goto Block_14;
												}
												this._totalConsumed += (long)readOnlySpan.Length;
												readOnlySpan = this._buffer;
												num5 = 0;
											}
											else
											{
												byte b3 = *readOnlySpan[num5];
												if (!JsonReaderHelper.IsHexDigit(b3))
												{
													this.RollBackState(in partialStateForRollback, true);
													ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidHexCharacterWithinString, b3, default(ReadOnlySpan<byte>));
												}
												num4++;
												this._bytePositionInLine += 1L;
												if (num4 >= 4)
												{
													break;
												}
												num5++;
											}
										}
										flag = false;
										num2 = num5 + 1;
										continue;
									}
									flag = false;
								}
								else if (b2 < 32)
								{
									this.RollBackState(in partialStateForRollback, true);
									ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterWithinString, b2, default(ReadOnlySpan<byte>));
								}
								this._bytePositionInLine += 1L;
								num2++;
							}
						}
						Block_14:
						if (this.IsLastSpan)
						{
							this.RollBackState(in partialStateForRollback, true);
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
						}
						this.RollBackState(in partialStateForRollback, false);
						return false;
						Block_18:
						if (this.IsLastSpan)
						{
							this.RollBackState(in partialStateForRollback, true);
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
						}
						this.RollBackState(in partialStateForRollback, false);
						return false;
						IL_02FE:
						this._bytePositionInLine += 1L;
						this._consumed = num2 + 1;
						this._totalConsumed += (long)num;
						sequencePosition = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + num2);
					}
					SequencePosition startPosition = partialStateForRollback.GetStartPosition(1);
					this.ValueSequence = this._sequence.Slice(startPosition, sequencePosition);
					this._tokenType = JsonTokenType.String;
					return true;
				}
				this._totalConsumed += (long)readOnlySpan.Length;
				this._bytePositionInLine += (long)readOnlySpan.Length;
			}
			if (this.IsLastSpan)
			{
				this._bytePositionInLine += (long)num;
				this.RollBackState(in partialStateForRollback, true);
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
			}
			this.RollBackState(in partialStateForRollback, false);
			return false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E970 File Offset: 0x0000CB70
		private unsafe bool ConsumeStringAndValidateMultiSegment(ReadOnlySpan<byte> data, int idx)
		{
			Utf8JsonReader.PartialStateForRollback partialStateForRollback = this.CaptureState();
			this.HasValueSequence = false;
			int num = this._buffer.Length - this._consumed;
			this._bytePositionInLine += (long)(idx + 1);
			bool flag = false;
			for (;;)
			{
				if (idx >= data.Length)
				{
					if (!this.GetNextSpan())
					{
						goto Block_15;
					}
					if (this.HasValueSequence)
					{
						this._totalConsumed += (long)data.Length;
					}
					data = this._buffer;
					idx = 0;
					this.HasValueSequence = true;
				}
				else
				{
					byte b = *data[idx];
					if (b == 34)
					{
						if (!flag)
						{
							goto IL_023F;
						}
						flag = false;
					}
					else if (b == 92)
					{
						flag = !flag;
					}
					else if (flag)
					{
						int num2 = JsonConstants.EscapableChars.IndexOf(b);
						if (num2 == -1)
						{
							this.RollBackState(in partialStateForRollback, true);
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterAfterEscapeWithinString, b, default(ReadOnlySpan<byte>));
						}
						if (b == 117)
						{
							this._bytePositionInLine += 1L;
							int num3 = 0;
							int num4 = idx + 1;
							for (;;)
							{
								if (num4 >= data.Length)
								{
									if (!this.GetNextSpan())
									{
										goto Block_10;
									}
									if (this.HasValueSequence)
									{
										this._totalConsumed += (long)data.Length;
									}
									data = this._buffer;
									num4 = 0;
									this.HasValueSequence = true;
								}
								else
								{
									byte b2 = *data[num4];
									if (!JsonReaderHelper.IsHexDigit(b2))
									{
										this.RollBackState(in partialStateForRollback, true);
										ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidHexCharacterWithinString, b2, default(ReadOnlySpan<byte>));
									}
									num3++;
									this._bytePositionInLine += 1L;
									if (num3 >= 4)
									{
										break;
									}
									num4++;
								}
							}
							flag = false;
							idx = num4 + 1;
							continue;
						}
						flag = false;
					}
					else if (b < 32)
					{
						this.RollBackState(in partialStateForRollback, true);
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterWithinString, b, default(ReadOnlySpan<byte>));
					}
					this._bytePositionInLine += 1L;
					idx++;
				}
			}
			Block_10:
			if (this.IsLastSpan)
			{
				this.RollBackState(in partialStateForRollback, true);
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
			}
			this.RollBackState(in partialStateForRollback, false);
			return false;
			Block_15:
			if (this.IsLastSpan)
			{
				this.RollBackState(in partialStateForRollback, true);
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.EndOfStringNotFound, 0, default(ReadOnlySpan<byte>));
			}
			this.RollBackState(in partialStateForRollback, false);
			return false;
			IL_023F:
			if (this.HasValueSequence)
			{
				this._bytePositionInLine += 1L;
				this._consumed = idx + 1;
				this._totalConsumed += (long)num;
				SequencePosition sequencePosition = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + idx);
				SequencePosition startPosition = partialStateForRollback.GetStartPosition(1);
				this.ValueSequence = this._sequence.Slice(startPosition, sequencePosition);
			}
			else
			{
				this._bytePositionInLine += 1L;
				this._consumed += idx + 2;
				this.ValueSpan = data.Slice(0, idx);
			}
			this.ValueIsEscaped = true;
			this._tokenType = JsonTokenType.String;
			return true;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000EC67 File Offset: 0x0000CE67
		private void RollBackState([ScopedRef] in Utf8JsonReader.PartialStateForRollback state, bool isError = false)
		{
			this._totalConsumed = state._prevTotalConsumed;
			if (!isError)
			{
				this._bytePositionInLine = state._prevBytePositionInLine;
			}
			this._consumed = state._prevConsumed;
			this._currentPosition = state._prevCurrentPosition;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		private unsafe bool TryGetNumberMultiSegment(ReadOnlySpan<byte> data, out int consumed)
		{
			Utf8JsonReader.PartialStateForRollback partialStateForRollback = this.CaptureState();
			consumed = 0;
			int num = 0;
			ConsumeNumberResult consumeNumberResult = this.ConsumeNegativeSignMultiSegment(ref data, ref num, in partialStateForRollback);
			if (consumeNumberResult == ConsumeNumberResult.NeedMoreData)
			{
				this.RollBackState(in partialStateForRollback, false);
				return false;
			}
			byte b = *data[num];
			if (b == 48)
			{
				ConsumeNumberResult consumeNumberResult2 = this.ConsumeZeroMultiSegment(ref data, ref num, in partialStateForRollback);
				if (consumeNumberResult2 == ConsumeNumberResult.NeedMoreData)
				{
					this.RollBackState(in partialStateForRollback, false);
					return false;
				}
				if (consumeNumberResult2 == ConsumeNumberResult.Success)
				{
					goto IL_01B1;
				}
				b = *data[num];
			}
			else
			{
				ConsumeNumberResult consumeNumberResult3 = this.ConsumeIntegerDigitsMultiSegment(ref data, ref num);
				if (consumeNumberResult3 == ConsumeNumberResult.NeedMoreData)
				{
					this.RollBackState(in partialStateForRollback, false);
					return false;
				}
				if (consumeNumberResult3 == ConsumeNumberResult.Success)
				{
					goto IL_01B1;
				}
				b = *data[num];
				if (b != 46 && b != 69 && b != 101)
				{
					this.RollBackState(in partialStateForRollback, true);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndOfDigitNotFound, b, default(ReadOnlySpan<byte>));
				}
			}
			if (b == 46)
			{
				num++;
				this._bytePositionInLine += 1L;
				ConsumeNumberResult consumeNumberResult4 = this.ConsumeDecimalDigitsMultiSegment(ref data, ref num, in partialStateForRollback);
				if (consumeNumberResult4 == ConsumeNumberResult.NeedMoreData)
				{
					this.RollBackState(in partialStateForRollback, false);
					return false;
				}
				if (consumeNumberResult4 == ConsumeNumberResult.Success)
				{
					goto IL_01B1;
				}
				b = *data[num];
				if (b != 69 && b != 101)
				{
					this.RollBackState(in partialStateForRollback, true);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedNextDigitEValueNotFound, b, default(ReadOnlySpan<byte>));
				}
			}
			num++;
			this._bytePositionInLine += 1L;
			consumeNumberResult = this.ConsumeSignMultiSegment(ref data, ref num, in partialStateForRollback);
			if (consumeNumberResult == ConsumeNumberResult.NeedMoreData)
			{
				this.RollBackState(in partialStateForRollback, false);
				return false;
			}
			num++;
			this._bytePositionInLine += 1L;
			ConsumeNumberResult consumeNumberResult5 = this.ConsumeIntegerDigitsMultiSegment(ref data, ref num);
			if (consumeNumberResult5 == ConsumeNumberResult.NeedMoreData)
			{
				this.RollBackState(in partialStateForRollback, false);
				return false;
			}
			if (consumeNumberResult5 != ConsumeNumberResult.Success)
			{
				this.RollBackState(in partialStateForRollback, true);
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndOfDigitNotFound, *data[num], default(ReadOnlySpan<byte>));
			}
			IL_01B1:
			if (this.HasValueSequence)
			{
				SequencePosition startPosition = partialStateForRollback.GetStartPosition(0);
				SequencePosition sequencePosition = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + num);
				this.ValueSequence = this._sequence.Slice(startPosition, sequencePosition);
				consumed = num;
			}
			else
			{
				this.ValueSpan = data.Slice(0, num);
				consumed = num;
			}
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		private unsafe ConsumeNumberResult ConsumeNegativeSignMultiSegment(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i, [ScopedRef] in Utf8JsonReader.PartialStateForRollback rollBackState)
		{
			byte b = *data[i];
			if (b == 45)
			{
				i++;
				this._bytePositionInLine += 1L;
				if (i >= data.Length)
				{
					if (this.IsLastSpan)
					{
						this.RollBackState(in rollBackState, true);
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
					}
					if (!this.GetNextSpan())
					{
						if (this.IsLastSpan)
						{
							this.RollBackState(in rollBackState, true);
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
						}
						return ConsumeNumberResult.NeedMoreData;
					}
					this._totalConsumed += (long)i;
					this.HasValueSequence = true;
					i = 0;
					data = this._buffer;
				}
				b = *data[i];
				if (!JsonHelpers.IsDigit(b))
				{
					this.RollBackState(in rollBackState, true);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundAfterSign, b, default(ReadOnlySpan<byte>));
				}
			}
			return ConsumeNumberResult.OperationIncomplete;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000EF94 File Offset: 0x0000D194
		private unsafe ConsumeNumberResult ConsumeZeroMultiSegment(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i, [ScopedRef] in Utf8JsonReader.PartialStateForRollback rollBackState)
		{
			i++;
			this._bytePositionInLine += 1L;
			byte b;
			if (i < data.Length)
			{
				b = *data[i];
				if (JsonConstants.Delimiters.IndexOf(b) >= 0)
				{
					return ConsumeNumberResult.Success;
				}
			}
			else
			{
				if (this.IsLastSpan)
				{
					return ConsumeNumberResult.Success;
				}
				if (!this.GetNextSpan())
				{
					if (this.IsLastSpan)
					{
						return ConsumeNumberResult.Success;
					}
					return ConsumeNumberResult.NeedMoreData;
				}
				else
				{
					this._totalConsumed += (long)i;
					this.HasValueSequence = true;
					i = 0;
					data = this._buffer;
					b = *data[i];
					if (JsonConstants.Delimiters.IndexOf(b) >= 0)
					{
						return ConsumeNumberResult.Success;
					}
				}
			}
			b = *data[i];
			if (b != 46 && b != 69 && b != 101)
			{
				this.RollBackState(in rollBackState, true);
				ThrowHelper.ThrowJsonReaderException(ref this, JsonHelpers.IsInRangeInclusive((int)b, 48, 57) ? ExceptionResource.InvalidLeadingZeroInNumber : ExceptionResource.ExpectedEndOfDigitNotFound, b, default(ReadOnlySpan<byte>));
			}
			return ConsumeNumberResult.OperationIncomplete;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000F07C File Offset: 0x0000D27C
		private unsafe ConsumeNumberResult ConsumeIntegerDigitsMultiSegment(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i)
		{
			byte b = 0;
			int num = 0;
			while (i < data.Length)
			{
				b = *data[i];
				if (!JsonHelpers.IsDigit(b))
				{
					break;
				}
				num++;
				i++;
			}
			if (i >= data.Length)
			{
				if (this.IsLastSpan)
				{
					this._bytePositionInLine += (long)num;
					return ConsumeNumberResult.Success;
				}
				while (this.GetNextSpan())
				{
					this._totalConsumed += (long)i;
					this._bytePositionInLine += (long)num;
					num = 0;
					this.HasValueSequence = true;
					i = 0;
					data = this._buffer;
					while (i < data.Length)
					{
						b = *data[i];
						if (!JsonHelpers.IsDigit(b))
						{
							break;
						}
						i++;
					}
					this._bytePositionInLine += (long)i;
					if (i < data.Length)
					{
						goto IL_0106;
					}
					if (this.IsLastSpan)
					{
						return ConsumeNumberResult.Success;
					}
				}
				if (this.IsLastSpan)
				{
					this._bytePositionInLine += (long)num;
					return ConsumeNumberResult.Success;
				}
				return ConsumeNumberResult.NeedMoreData;
			}
			else
			{
				this._bytePositionInLine += (long)num;
			}
			IL_0106:
			if (JsonConstants.Delimiters.IndexOf(b) >= 0)
			{
				return ConsumeNumberResult.Success;
			}
			return ConsumeNumberResult.OperationIncomplete;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		private unsafe ConsumeNumberResult ConsumeDecimalDigitsMultiSegment(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i, [ScopedRef] in Utf8JsonReader.PartialStateForRollback rollBackState)
		{
			if (i >= data.Length)
			{
				if (this.IsLastSpan)
				{
					this.RollBackState(in rollBackState, true);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.GetNextSpan())
				{
					if (this.IsLastSpan)
					{
						this.RollBackState(in rollBackState, true);
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
					}
					return ConsumeNumberResult.NeedMoreData;
				}
				this._totalConsumed += (long)i;
				this.HasValueSequence = true;
				i = 0;
				data = this._buffer;
			}
			byte b = *data[i];
			if (!JsonHelpers.IsDigit(b))
			{
				this.RollBackState(in rollBackState, true);
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundAfterDecimal, b, default(ReadOnlySpan<byte>));
			}
			i++;
			this._bytePositionInLine += 1L;
			return this.ConsumeIntegerDigitsMultiSegment(ref data, ref i);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000F274 File Offset: 0x0000D474
		private unsafe ConsumeNumberResult ConsumeSignMultiSegment(ref ReadOnlySpan<byte> data, [ScopedRef] ref int i, [ScopedRef] in Utf8JsonReader.PartialStateForRollback rollBackState)
		{
			if (i >= data.Length)
			{
				if (this.IsLastSpan)
				{
					this.RollBackState(in rollBackState, true);
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.GetNextSpan())
				{
					if (this.IsLastSpan)
					{
						this.RollBackState(in rollBackState, true);
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
					}
					return ConsumeNumberResult.NeedMoreData;
				}
				this._totalConsumed += (long)i;
				this.HasValueSequence = true;
				i = 0;
				data = this._buffer;
			}
			byte b = *data[i];
			if (b == 43 || b == 45)
			{
				i++;
				this._bytePositionInLine += 1L;
				if (i >= data.Length)
				{
					if (this.IsLastSpan)
					{
						this.RollBackState(in rollBackState, true);
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
					}
					if (!this.GetNextSpan())
					{
						if (this.IsLastSpan)
						{
							this.RollBackState(in rollBackState, true);
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundEndOfData, 0, default(ReadOnlySpan<byte>));
						}
						return ConsumeNumberResult.NeedMoreData;
					}
					this._totalConsumed += (long)i;
					this.HasValueSequence = true;
					i = 0;
					data = this._buffer;
				}
				b = *data[i];
			}
			if (!JsonHelpers.IsDigit(b))
			{
				this.RollBackState(in rollBackState, true);
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.RequiredDigitNotFoundAfterSign, b, default(ReadOnlySpan<byte>));
			}
			return ConsumeNumberResult.OperationIncomplete;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		private bool ConsumeNextTokenOrRollbackMultiSegment(byte marker)
		{
			long totalConsumed = this._totalConsumed;
			int consumed = this._consumed;
			long bytePositionInLine = this._bytePositionInLine;
			long lineNumber = this._lineNumber;
			JsonTokenType tokenType = this._tokenType;
			SequencePosition currentPosition = this._currentPosition;
			bool trailingCommaBeforeComment = this._trailingCommaBeforeComment;
			ConsumeTokenResult consumeTokenResult = this.ConsumeNextTokenMultiSegment(marker);
			if (consumeTokenResult == ConsumeTokenResult.Success)
			{
				return true;
			}
			if (consumeTokenResult == ConsumeTokenResult.NotEnoughDataRollBackState)
			{
				this._consumed = consumed;
				this._tokenType = tokenType;
				this._bytePositionInLine = bytePositionInLine;
				this._lineNumber = lineNumber;
				this._totalConsumed = totalConsumed;
				this._currentPosition = currentPosition;
				this._trailingCommaBeforeComment = trailingCommaBeforeComment;
			}
			return false;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F460 File Offset: 0x0000D660
		private unsafe ConsumeTokenResult ConsumeNextTokenMultiSegment(byte marker)
		{
			if (this._readerOptions.CommentHandling != JsonCommentHandling.Disallow)
			{
				if (this._readerOptions.CommentHandling != JsonCommentHandling.Allow)
				{
					return this.ConsumeNextTokenUntilAfterAllCommentsAreSkippedMultiSegment(marker);
				}
				if (marker == 47)
				{
					if (!this.SkipOrConsumeCommentMultiSegmentWithRollback())
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					return ConsumeTokenResult.Success;
				}
				else if (this._tokenType == JsonTokenType.Comment)
				{
					return this.ConsumeNextTokenFromLastNonCommentTokenMultiSegment();
				}
			}
			if (this._bitStack.CurrentDepth == 0)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndAfterSingleJson, marker, default(ReadOnlySpan<byte>));
			}
			if (marker != 44)
			{
				if (marker == 125)
				{
					this.EndObject();
				}
				else if (marker == 93)
				{
					this.EndArray();
				}
				else
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.FoundInvalidCharacter, marker, default(ReadOnlySpan<byte>));
				}
				return ConsumeTokenResult.Success;
			}
			this._consumed++;
			this._bytePositionInLine += 1L;
			if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
			{
				if (this.IsLastSpan)
				{
					this._consumed--;
					this._bytePositionInLine -= 1L;
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.GetNextSpan())
				{
					if (this.IsLastSpan)
					{
						this._consumed--;
						this._bytePositionInLine -= 1L;
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
					}
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
			}
			byte b = *this._buffer[this._consumed];
			if (b <= 32)
			{
				this.SkipWhiteSpaceMultiSegment();
				if (!this.HasMoreDataMultiSegment(ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				b = *this._buffer[this._consumed];
			}
			this.TokenStartIndex = this.BytesConsumed;
			if (this._readerOptions.CommentHandling == JsonCommentHandling.Allow && b == 47)
			{
				this._trailingCommaBeforeComment = true;
				if (!this.SkipOrConsumeCommentMultiSegmentWithRollback())
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
			else if (this._inObject)
			{
				if (b != 34)
				{
					if (b == 125)
					{
						if (this._readerOptions.AllowTrailingCommas)
						{
							this.EndObject();
							return ConsumeTokenResult.Success;
						}
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
					}
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
				}
				if (!this.ConsumePropertyNameMultiSegment())
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
			else
			{
				if (b == 93)
				{
					if (this._readerOptions.AllowTrailingCommas)
					{
						this.EndArray();
						return ConsumeTokenResult.Success;
					}
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.ConsumeValueMultiSegment(b))
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		private unsafe ConsumeTokenResult ConsumeNextTokenFromLastNonCommentTokenMultiSegment()
		{
			if (JsonReaderHelper.IsTokenTypePrimitive(this._previousTokenType))
			{
				this._tokenType = (this._inObject ? JsonTokenType.StartObject : JsonTokenType.StartArray);
			}
			else
			{
				this._tokenType = this._previousTokenType;
			}
			if (this.HasMoreDataMultiSegment())
			{
				byte b = *this._buffer[this._consumed];
				if (b <= 32)
				{
					this.SkipWhiteSpaceMultiSegment();
					if (!this.HasMoreDataMultiSegment())
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					b = *this._buffer[this._consumed];
				}
				if (this._bitStack.CurrentDepth == 0 && this._tokenType != JsonTokenType.None)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndAfterSingleJson, b, default(ReadOnlySpan<byte>));
				}
				this.TokenStartIndex = this.BytesConsumed;
				if (b == 44)
				{
					if (this._previousTokenType <= JsonTokenType.StartObject || this._previousTokenType == JsonTokenType.StartArray || this._trailingCommaBeforeComment)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueAfterComment, b, default(ReadOnlySpan<byte>));
					}
					this._consumed++;
					this._bytePositionInLine += 1L;
					if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
					{
						if (this.IsLastSpan)
						{
							this._consumed--;
							this._bytePositionInLine -= 1L;
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
						}
						if (!this.GetNextSpan())
						{
							if (this.IsLastSpan)
							{
								this._consumed--;
								this._bytePositionInLine -= 1L;
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
								return ConsumeTokenResult.NotEnoughDataRollBackState;
							}
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
					}
					b = *this._buffer[this._consumed];
					if (b <= 32)
					{
						this.SkipWhiteSpaceMultiSegment();
						if (!this.HasMoreDataMultiSegment(ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						b = *this._buffer[this._consumed];
					}
					this.TokenStartIndex = this.BytesConsumed;
					if (b == 47)
					{
						this._trailingCommaBeforeComment = true;
						if (!this.SkipOrConsumeCommentMultiSegmentWithRollback())
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
					}
					else if (this._inObject)
					{
						if (b != 34)
						{
							if (b == 125)
							{
								if (this._readerOptions.AllowTrailingCommas)
								{
									this.EndObject();
									return ConsumeTokenResult.Success;
								}
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
							}
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
						}
						if (!this.ConsumePropertyNameMultiSegment())
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
					}
					else
					{
						if (b == 93)
						{
							if (this._readerOptions.AllowTrailingCommas)
							{
								this.EndArray();
								return ConsumeTokenResult.Success;
							}
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
						}
						if (!this.ConsumeValueMultiSegment(b))
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
					}
				}
				else if (b == 125)
				{
					this.EndObject();
				}
				else if (b == 93)
				{
					this.EndArray();
				}
				else if (this._tokenType == JsonTokenType.None)
				{
					if (!this.ReadFirstTokenMultiSegment(b))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._tokenType == JsonTokenType.StartObject)
				{
					if (b != 34)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
					}
					long totalConsumed = this._totalConsumed;
					int consumed = this._consumed;
					long bytePositionInLine = this._bytePositionInLine;
					long lineNumber = this._lineNumber;
					if (!this.ConsumePropertyNameMultiSegment())
					{
						this._consumed = consumed;
						this._tokenType = JsonTokenType.StartObject;
						this._bytePositionInLine = bytePositionInLine;
						this._lineNumber = lineNumber;
						this._totalConsumed = totalConsumed;
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._tokenType == JsonTokenType.StartArray)
				{
					if (!this.ConsumeValueMultiSegment(b))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._tokenType == JsonTokenType.PropertyName)
				{
					if (!this.ConsumeValueMultiSegment(b))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (this._inObject)
				{
					if (b != 34)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, b, default(ReadOnlySpan<byte>));
					}
					if (!this.ConsumePropertyNameMultiSegment())
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
				}
				else if (!this.ConsumeValueMultiSegment(b))
				{
					return ConsumeTokenResult.NotEnoughDataRollBackState;
				}
				return ConsumeTokenResult.Success;
			}
			return ConsumeTokenResult.NotEnoughDataRollBackState;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000FA54 File Offset: 0x0000DC54
		private unsafe bool SkipAllCommentsMultiSegment([ScopedRef] ref byte marker)
		{
			while (marker == 47)
			{
				if (this.SkipOrConsumeCommentMultiSegmentWithRollback() && this.HasMoreDataMultiSegment())
				{
					marker = *this._buffer[this._consumed];
					if (marker > 32)
					{
						continue;
					}
					this.SkipWhiteSpaceMultiSegment();
					if (this.HasMoreDataMultiSegment())
					{
						marker = *this._buffer[this._consumed];
						continue;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		private unsafe bool SkipAllCommentsMultiSegment([ScopedRef] ref byte marker, ExceptionResource resource)
		{
			while (marker == 47)
			{
				if (this.SkipOrConsumeCommentMultiSegmentWithRollback() && this.HasMoreDataMultiSegment(resource))
				{
					marker = *this._buffer[this._consumed];
					if (marker > 32)
					{
						continue;
					}
					this.SkipWhiteSpaceMultiSegment();
					if (this.HasMoreDataMultiSegment(resource))
					{
						marker = *this._buffer[this._consumed];
						continue;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000FB20 File Offset: 0x0000DD20
		private unsafe ConsumeTokenResult ConsumeNextTokenUntilAfterAllCommentsAreSkippedMultiSegment(byte marker)
		{
			if (this.SkipAllCommentsMultiSegment(ref marker))
			{
				this.TokenStartIndex = this.BytesConsumed;
				if (this._tokenType == JsonTokenType.StartObject)
				{
					if (marker == 125)
					{
						this.EndObject();
					}
					else
					{
						if (marker != 34)
						{
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, marker, default(ReadOnlySpan<byte>));
						}
						long totalConsumed = this._totalConsumed;
						int consumed = this._consumed;
						long bytePositionInLine = this._bytePositionInLine;
						long lineNumber = this._lineNumber;
						SequencePosition currentPosition = this._currentPosition;
						if (!this.ConsumePropertyNameMultiSegment())
						{
							this._consumed = consumed;
							this._tokenType = JsonTokenType.StartObject;
							this._bytePositionInLine = bytePositionInLine;
							this._lineNumber = lineNumber;
							this._totalConsumed = totalConsumed;
							this._currentPosition = currentPosition;
							return ConsumeTokenResult.IncompleteNoRollBackNecessary;
						}
					}
				}
				else if (this._tokenType == JsonTokenType.StartArray)
				{
					if (marker == 93)
					{
						this.EndArray();
					}
					else if (!this.ConsumeValueMultiSegment(marker))
					{
						return ConsumeTokenResult.IncompleteNoRollBackNecessary;
					}
				}
				else if (this._tokenType == JsonTokenType.PropertyName)
				{
					if (!this.ConsumeValueMultiSegment(marker))
					{
						return ConsumeTokenResult.IncompleteNoRollBackNecessary;
					}
				}
				else if (this._bitStack.CurrentDepth == 0)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedEndAfterSingleJson, marker, default(ReadOnlySpan<byte>));
				}
				else if (marker == 44)
				{
					this._consumed++;
					this._bytePositionInLine += 1L;
					if ((long)this._consumed >= (long)((ulong)this._buffer.Length))
					{
						if (this.IsLastSpan)
						{
							this._consumed--;
							this._bytePositionInLine -= 1L;
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
						}
						if (!this.GetNextSpan())
						{
							if (this.IsLastSpan)
							{
								this._consumed--;
								this._bytePositionInLine -= 1L;
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound, 0, default(ReadOnlySpan<byte>));
							}
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
					}
					marker = *this._buffer[this._consumed];
					if (marker <= 32)
					{
						this.SkipWhiteSpaceMultiSegment();
						if (!this.HasMoreDataMultiSegment(ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						marker = *this._buffer[this._consumed];
					}
					if (!this.SkipAllCommentsMultiSegment(ref marker, ExceptionResource.ExpectedStartOfPropertyOrValueNotFound))
					{
						return ConsumeTokenResult.NotEnoughDataRollBackState;
					}
					this.TokenStartIndex = this.BytesConsumed;
					if (this._inObject)
					{
						if (marker != 34)
						{
							if (marker == 125)
							{
								if (this._readerOptions.AllowTrailingCommas)
								{
									this.EndObject();
									return ConsumeTokenResult.Success;
								}
								ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd, 0, default(ReadOnlySpan<byte>));
							}
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.ExpectedStartOfPropertyNotFound, marker, default(ReadOnlySpan<byte>));
						}
						if (!this.ConsumePropertyNameMultiSegment())
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						return ConsumeTokenResult.Success;
					}
					else
					{
						if (marker == 93)
						{
							if (this._readerOptions.AllowTrailingCommas)
							{
								this.EndArray();
								return ConsumeTokenResult.Success;
							}
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd, 0, default(ReadOnlySpan<byte>));
						}
						if (!this.ConsumeValueMultiSegment(marker))
						{
							return ConsumeTokenResult.NotEnoughDataRollBackState;
						}
						return ConsumeTokenResult.Success;
					}
				}
				else if (marker == 125)
				{
					this.EndObject();
				}
				else if (marker == 93)
				{
					this.EndArray();
				}
				else
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.FoundInvalidCharacter, marker, default(ReadOnlySpan<byte>));
				}
				return ConsumeTokenResult.Success;
			}
			return ConsumeTokenResult.IncompleteNoRollBackNecessary;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000FE18 File Offset: 0x0000E018
		private bool SkipOrConsumeCommentMultiSegmentWithRollback()
		{
			long bytesConsumed = this.BytesConsumed;
			SequencePosition sequencePosition = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + this._consumed);
			int num;
			bool flag = this.SkipCommentMultiSegment(out num);
			if (flag)
			{
				if (this._readerOptions.CommentHandling == JsonCommentHandling.Allow)
				{
					SequencePosition sequencePosition2 = new SequencePosition(this._currentPosition.GetObject(), this._currentPosition.GetInteger() + this._consumed);
					ReadOnlySequence<byte> readOnlySequence = this._sequence.Slice(sequencePosition, sequencePosition2);
					readOnlySequence = readOnlySequence.Slice(2L, readOnlySequence.Length - 2L - (long)num);
					this.HasValueSequence = !readOnlySequence.IsSingleSegment;
					if (this.HasValueSequence)
					{
						this.ValueSequence = readOnlySequence;
					}
					else
					{
						this.ValueSpan = readOnlySequence.First.Span;
					}
					if (this._tokenType != JsonTokenType.Comment)
					{
						this._previousTokenType = this._tokenType;
					}
					this._tokenType = JsonTokenType.Comment;
				}
			}
			else
			{
				this._totalConsumed = bytesConsumed;
				this._consumed = 0;
			}
			return flag;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000FF24 File Offset: 0x0000E124
		private unsafe bool SkipCommentMultiSegment(out int tailBytesToIgnore)
		{
			this._consumed++;
			this._bytePositionInLine += 1L;
			ReadOnlySpan<byte> readOnlySpan = this._buffer.Slice(this._consumed);
			if (readOnlySpan.Length == 0)
			{
				if (this.IsLastSpan)
				{
					ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
				}
				if (!this.GetNextSpan())
				{
					if (this.IsLastSpan)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
					}
					tailBytesToIgnore = 0;
					return false;
				}
				readOnlySpan = this._buffer;
			}
			byte b = *readOnlySpan[0];
			if (b != 47 && b != 42)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.InvalidCharacterAtStartOfComment, b, default(ReadOnlySpan<byte>));
			}
			bool flag = b == 42;
			this._consumed++;
			this._bytePositionInLine += 1L;
			readOnlySpan = readOnlySpan.Slice(1);
			if (readOnlySpan.Length == 0)
			{
				if (this.IsLastSpan)
				{
					tailBytesToIgnore = 0;
					if (flag)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
					}
					return true;
				}
				if (!this.GetNextSpan())
				{
					tailBytesToIgnore = 0;
					if (this.IsLastSpan)
					{
						if (flag)
						{
							ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
						}
						return true;
					}
					return false;
				}
				else
				{
					readOnlySpan = this._buffer;
				}
			}
			if (flag)
			{
				tailBytesToIgnore = 2;
				return this.SkipMultiLineCommentMultiSegment(readOnlySpan);
			}
			return this.SkipSingleLineCommentMultiSegment(readOnlySpan, out tailBytesToIgnore);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010078 File Offset: 0x0000E278
		private unsafe bool SkipSingleLineCommentMultiSegment(ReadOnlySpan<byte> localBuffer, out int tailBytesToSkip)
		{
			bool flag = false;
			int num = 0;
			tailBytesToSkip = 0;
			while (!flag)
			{
				int num2 = this.FindLineSeparatorMultiSegment(localBuffer, ref num);
				if (num2 != -1)
				{
					tailBytesToSkip++;
					this._consumed += num2 + 1;
					this._bytePositionInLine += (long)(num2 + 1);
					if (*localBuffer[num2] == 10)
					{
						goto IL_0119;
					}
					if (num2 < localBuffer.Length - 1)
					{
						if (*localBuffer[num2 + 1] == 10)
						{
							tailBytesToSkip++;
							this._consumed++;
							this._bytePositionInLine += 1L;
							goto IL_0119;
						}
						goto IL_0119;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					this._consumed += localBuffer.Length;
					this._bytePositionInLine += (long)localBuffer.Length;
				}
				if (this.IsLastSpan)
				{
					if (!flag)
					{
						return true;
					}
				}
				else
				{
					if (this.GetNextSpan())
					{
						localBuffer = this._buffer;
						continue;
					}
					if (!this.IsLastSpan)
					{
						return false;
					}
					if (!flag)
					{
						return true;
					}
				}
				IL_0119:
				this._bytePositionInLine = 0L;
				this._lineNumber += 1L;
				return true;
			}
			if (*localBuffer[0] == 10)
			{
				tailBytesToSkip++;
				this._consumed++;
				goto IL_0119;
			}
			goto IL_0119;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000101B8 File Offset: 0x0000E3B8
		private unsafe int FindLineSeparatorMultiSegment(ReadOnlySpan<byte> localBuffer, [ScopedRef] ref int dangerousLineSeparatorBytesConsumed)
		{
			if (dangerousLineSeparatorBytesConsumed != 0)
			{
				this.ThrowOnDangerousLineSeparatorMultiSegment(localBuffer, ref dangerousLineSeparatorBytesConsumed);
				if (dangerousLineSeparatorBytesConsumed != 0)
				{
					return -1;
				}
			}
			int num = 0;
			int num2;
			for (;;)
			{
				num2 = localBuffer.IndexOfAny(10, 13, 226);
				dangerousLineSeparatorBytesConsumed = 0;
				if (num2 == -1)
				{
					break;
				}
				if (*localBuffer[num2] != 226)
				{
					goto Block_4;
				}
				int num3 = num2 + 1;
				localBuffer = localBuffer.Slice(num3);
				num += num3;
				dangerousLineSeparatorBytesConsumed++;
				this.ThrowOnDangerousLineSeparatorMultiSegment(localBuffer, ref dangerousLineSeparatorBytesConsumed);
				if (dangerousLineSeparatorBytesConsumed != 0)
				{
					return -1;
				}
			}
			return -1;
			Block_4:
			return num + num2;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001022C File Offset: 0x0000E42C
		private unsafe void ThrowOnDangerousLineSeparatorMultiSegment(ReadOnlySpan<byte> localBuffer, [ScopedRef] ref int dangerousLineSeparatorBytesConsumed)
		{
			if (localBuffer.IsEmpty)
			{
				return;
			}
			if (dangerousLineSeparatorBytesConsumed == 1)
			{
				if (*localBuffer[0] != 128)
				{
					dangerousLineSeparatorBytesConsumed = 0;
					return;
				}
				localBuffer = localBuffer.Slice(1);
				dangerousLineSeparatorBytesConsumed++;
				if (localBuffer.IsEmpty)
				{
					return;
				}
			}
			if (dangerousLineSeparatorBytesConsumed != 2)
			{
				return;
			}
			byte b = *localBuffer[0];
			if (b == 168 || b == 169)
			{
				ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfLineSeparator, 0, default(ReadOnlySpan<byte>));
				return;
			}
			dangerousLineSeparatorBytesConsumed = 0;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000102AC File Offset: 0x0000E4AC
		private unsafe bool SkipMultiLineCommentMultiSegment(ReadOnlySpan<byte> localBuffer)
		{
			bool flag = false;
			bool flag2 = false;
			for (;;)
			{
				if (flag)
				{
					if (*localBuffer[0] == 47)
					{
						break;
					}
					flag = false;
				}
				if (flag2)
				{
					if (*localBuffer[0] == 10)
					{
						this._consumed++;
						localBuffer = localBuffer.Slice(1);
					}
					flag2 = false;
				}
				int num = localBuffer.IndexOfAny(42, 10, 13);
				if (num != -1)
				{
					int num2 = num + 1;
					byte b = *localBuffer[num];
					localBuffer = localBuffer.Slice(num2);
					this._consumed += num2;
					if (b != 10)
					{
						if (b == 42)
						{
							flag = true;
							this._bytePositionInLine += (long)num2;
						}
						else
						{
							this._bytePositionInLine = 0L;
							this._lineNumber += 1L;
							flag2 = true;
						}
					}
					else
					{
						this._bytePositionInLine = 0L;
						this._lineNumber += 1L;
					}
				}
				else
				{
					this._consumed += localBuffer.Length;
					this._bytePositionInLine += (long)localBuffer.Length;
					localBuffer = ReadOnlySpan<byte>.Empty;
				}
				if (localBuffer.IsEmpty)
				{
					if (this.IsLastSpan)
					{
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
					}
					if (!this.GetNextSpan())
					{
						if (!this.IsLastSpan)
						{
							return false;
						}
						ThrowHelper.ThrowJsonReaderException(ref this, ExceptionResource.UnexpectedEndOfDataWhileReadingComment, 0, default(ReadOnlySpan<byte>));
					}
					localBuffer = this._buffer;
				}
			}
			this._consumed++;
			this._bytePositionInLine += 1L;
			return true;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001042D File Offset: 0x0000E62D
		private Utf8JsonReader.PartialStateForRollback CaptureState()
		{
			return new Utf8JsonReader.PartialStateForRollback(this._totalConsumed, this._bytePositionInLine, this._consumed, this._currentPosition);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001044C File Offset: 0x0000E64C
		[NullableContext(2)]
		public string GetString()
		{
			if (this.TokenType == JsonTokenType.Null)
			{
				return null;
			}
			if (this.TokenType != JsonTokenType.String && this.TokenType != JsonTokenType.PropertyName)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			if (this.ValueIsEscaped)
			{
				return JsonReaderHelper.GetUnescapedString(readOnlySpan2);
			}
			return JsonReaderHelper.TranscodeHelper(readOnlySpan2);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000104BC File Offset: 0x0000E6BC
		public readonly int CopyString(Span<byte> utf8Destination)
		{
			JsonTokenType tokenType = this._tokenType;
			if (tokenType != JsonTokenType.PropertyName && tokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this._tokenType);
			}
			return this.CopyValue(utf8Destination);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000104F4 File Offset: 0x0000E6F4
		internal readonly int CopyValue(Span<byte> utf8Destination)
		{
			int num;
			if (this.ValueIsEscaped)
			{
				if (!this.TryCopyEscapedString(utf8Destination, out num))
				{
					utf8Destination.Slice(0, num).Clear();
					ThrowHelper.ThrowArgumentException_DestinationTooShort();
				}
			}
			else if (this.HasValueSequence)
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				(in valueSequence).CopyTo(utf8Destination);
				num = (int)valueSequence.Length;
			}
			else
			{
				ReadOnlySpan<byte> valueSpan = this.ValueSpan;
				valueSpan.CopyTo(utf8Destination);
				num = valueSpan.Length;
			}
			JsonReaderHelper.ValidateUtf8(utf8Destination.Slice(0, num));
			return num;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001057C File Offset: 0x0000E77C
		public readonly int CopyString(Span<char> destination)
		{
			JsonTokenType tokenType = this._tokenType;
			if (tokenType != JsonTokenType.PropertyName && tokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this._tokenType);
			}
			return this.CopyValue(destination);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000105B4 File Offset: 0x0000E7B4
		internal unsafe readonly int CopyValue(Span<char> destination)
		{
			byte[] array = null;
			ReadOnlySpan<byte> readOnlySpan;
			if (this.ValueIsEscaped)
			{
				int num = this.ValueLength;
				Span<byte> span2;
				if (num <= 256)
				{
					Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span2 = span;
				}
				else
				{
					span2 = (array = ArrayPool<byte>.Shared.Rent(num));
				}
				Span<byte> span3 = span2;
				int num2;
				bool flag = this.TryCopyEscapedString(span3, out num2);
				readOnlySpan = span3.Slice(0, num2);
			}
			else if (this.HasValueSequence)
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				int num = checked((int)valueSequence.Length);
				Span<byte> span4;
				if (num <= 256)
				{
					Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span4 = span;
				}
				else
				{
					span4 = (array = ArrayPool<byte>.Shared.Rent(num));
				}
				Span<byte> span5 = span4;
				(in valueSequence).CopyTo(span5);
				readOnlySpan = span5.Slice(0, num);
			}
			else
			{
				readOnlySpan = this.ValueSpan;
			}
			int num3 = JsonReaderHelper.TranscodeHelper(readOnlySpan, destination);
			if (array != null)
			{
				new Span<byte>(array, 0, readOnlySpan.Length).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return num3;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000106D0 File Offset: 0x0000E8D0
		private unsafe readonly bool TryCopyEscapedString(Span<byte> destination, out int bytesWritten)
		{
			byte[] array = null;
			ReadOnlySpan<byte> readOnlySpan;
			if (this.HasValueSequence)
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				int num = checked((int)valueSequence.Length);
				Span<byte> span2;
				if (num <= 256)
				{
					Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span2 = span;
				}
				else
				{
					span2 = (array = ArrayPool<byte>.Shared.Rent(num));
				}
				Span<byte> span3 = span2;
				(in valueSequence).CopyTo(span3);
				readOnlySpan = span3.Slice(0, num);
			}
			else
			{
				readOnlySpan = this.ValueSpan;
			}
			bool flag = JsonReaderHelper.TryUnescape(readOnlySpan, destination, out bytesWritten);
			if (array != null)
			{
				new Span<byte>(array, 0, readOnlySpan.Length).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010788 File Offset: 0x0000E988
		[NullableContext(1)]
		public string GetComment()
		{
			if (this.TokenType != JsonTokenType.Comment)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedComment(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return JsonReaderHelper.TranscodeHelper(readOnlySpan2);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public bool GetBoolean()
		{
			JsonTokenType tokenType = this.TokenType;
			if (tokenType == JsonTokenType.True)
			{
				return true;
			}
			if (tokenType != JsonTokenType.False)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedBoolean(this.TokenType);
			}
			return false;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010800 File Offset: 0x0000EA00
		[NullableContext(1)]
		public byte[] GetBytesFromBase64()
		{
			byte[] array;
			if (!this.TryGetBytesFromBase64(out array))
			{
				ThrowHelper.ThrowFormatException(DataType.Base64String);
			}
			return array;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010820 File Offset: 0x0000EA20
		public byte GetByte()
		{
			byte b;
			if (!this.TryGetByte(out b))
			{
				ThrowHelper.ThrowFormatException(NumericType.Byte);
			}
			return b;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010840 File Offset: 0x0000EA40
		internal byte GetByteWithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			byte b;
			if (!Utf8JsonReader.TryGetByteCore(out b, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.Byte);
			}
			return b;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010868 File Offset: 0x0000EA68
		[CLSCompliant(false)]
		public sbyte GetSByte()
		{
			sbyte b;
			if (!this.TryGetSByte(out b))
			{
				ThrowHelper.ThrowFormatException(NumericType.SByte);
			}
			return b;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010888 File Offset: 0x0000EA88
		internal sbyte GetSByteWithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			sbyte b;
			if (!Utf8JsonReader.TryGetSByteCore(out b, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.SByte);
			}
			return b;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public short GetInt16()
		{
			short num;
			if (!this.TryGetInt16(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Int16);
			}
			return num;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000108D0 File Offset: 0x0000EAD0
		internal short GetInt16WithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			short num;
			if (!Utf8JsonReader.TryGetInt16Core(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.Int16);
			}
			return num;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public int GetInt32()
		{
			int num;
			if (!this.TryGetInt32(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Int32);
			}
			return num;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010918 File Offset: 0x0000EB18
		internal int GetInt32WithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			int num;
			if (!Utf8JsonReader.TryGetInt32Core(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.Int32);
			}
			return num;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010940 File Offset: 0x0000EB40
		public long GetInt64()
		{
			long num;
			if (!this.TryGetInt64(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Int64);
			}
			return num;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010960 File Offset: 0x0000EB60
		internal long GetInt64WithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			long num;
			if (!Utf8JsonReader.TryGetInt64Core(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.Int64);
			}
			return num;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010988 File Offset: 0x0000EB88
		[CLSCompliant(false)]
		public ushort GetUInt16()
		{
			ushort num;
			if (!this.TryGetUInt16(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.UInt16);
			}
			return num;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000109A8 File Offset: 0x0000EBA8
		internal ushort GetUInt16WithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			ushort num;
			if (!Utf8JsonReader.TryGetUInt16Core(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.UInt16);
			}
			return num;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000109D0 File Offset: 0x0000EBD0
		[CLSCompliant(false)]
		public uint GetUInt32()
		{
			uint num;
			if (!this.TryGetUInt32(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.UInt32);
			}
			return num;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000109F0 File Offset: 0x0000EBF0
		internal uint GetUInt32WithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			uint num;
			if (!Utf8JsonReader.TryGetUInt32Core(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.UInt32);
			}
			return num;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010A18 File Offset: 0x0000EC18
		[CLSCompliant(false)]
		public ulong GetUInt64()
		{
			ulong num;
			if (!this.TryGetUInt64(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.UInt64);
			}
			return num;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010A38 File Offset: 0x0000EC38
		internal ulong GetUInt64WithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			ulong num;
			if (!Utf8JsonReader.TryGetUInt64Core(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.UInt64);
			}
			return num;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010A60 File Offset: 0x0000EC60
		public float GetSingle()
		{
			float num;
			if (!this.TryGetSingle(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Single);
			}
			return num;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010A80 File Offset: 0x0000EC80
		internal float GetSingleWithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			float num;
			if (JsonReaderHelper.TryGetFloatingPointConstant(unescapedSpan, out num))
			{
				return num;
			}
			int num2;
			if (!Utf8Parser.TryParse(unescapedSpan, out num, out num2, '\0') || unescapedSpan.Length != num2 || !JsonHelpers.IsFinite(num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Single);
			}
			return num;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		internal float GetSingleFloatingPointConstant()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			float num;
			if (!JsonReaderHelper.TryGetFloatingPointConstant(unescapedSpan, out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Single);
			}
			return num;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		public double GetDouble()
		{
			double num;
			if (!this.TryGetDouble(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Double);
			}
			return num;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010B10 File Offset: 0x0000ED10
		internal double GetDoubleWithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			double num;
			if (JsonReaderHelper.TryGetFloatingPointConstant(unescapedSpan, out num))
			{
				return num;
			}
			int num2;
			if (!Utf8Parser.TryParse(unescapedSpan, out num, out num2, '\0') || unescapedSpan.Length != num2 || !JsonHelpers.IsFinite(num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Double);
			}
			return num;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010B58 File Offset: 0x0000ED58
		internal double GetDoubleFloatingPointConstant()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			double num;
			if (!JsonReaderHelper.TryGetFloatingPointConstant(unescapedSpan, out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Double);
			}
			return num;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010B80 File Offset: 0x0000ED80
		public decimal GetDecimal()
		{
			decimal num;
			if (!this.TryGetDecimal(out num))
			{
				ThrowHelper.ThrowFormatException(NumericType.Decimal);
			}
			return num;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		internal decimal GetDecimalWithQuotes()
		{
			ReadOnlySpan<byte> unescapedSpan = this.GetUnescapedSpan();
			decimal num;
			if (!Utf8JsonReader.TryGetDecimalCore(out num, unescapedSpan))
			{
				ThrowHelper.ThrowFormatException(NumericType.Decimal);
			}
			return num;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public DateTime GetDateTime()
		{
			DateTime dateTime;
			if (!this.TryGetDateTime(out dateTime))
			{
				ThrowHelper.ThrowFormatException(DataType.DateTime);
			}
			return dateTime;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		internal DateTime GetDateTimeNoValidation()
		{
			DateTime dateTime;
			if (!this.TryGetDateTimeCore(out dateTime))
			{
				ThrowHelper.ThrowFormatException(DataType.DateTime);
			}
			return dateTime;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010C08 File Offset: 0x0000EE08
		public DateTimeOffset GetDateTimeOffset()
		{
			DateTimeOffset dateTimeOffset;
			if (!this.TryGetDateTimeOffset(out dateTimeOffset))
			{
				ThrowHelper.ThrowFormatException(DataType.DateTimeOffset);
			}
			return dateTimeOffset;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010C28 File Offset: 0x0000EE28
		internal DateTimeOffset GetDateTimeOffsetNoValidation()
		{
			DateTimeOffset dateTimeOffset;
			if (!this.TryGetDateTimeOffsetCore(out dateTimeOffset))
			{
				ThrowHelper.ThrowFormatException(DataType.DateTimeOffset);
			}
			return dateTimeOffset;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010C48 File Offset: 0x0000EE48
		public Guid GetGuid()
		{
			Guid guid;
			if (!this.TryGetGuid(out guid))
			{
				ThrowHelper.ThrowFormatException(DataType.Guid);
			}
			return guid;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010C68 File Offset: 0x0000EE68
		internal Guid GetGuidNoValidation()
		{
			Guid guid;
			if (!this.TryGetGuidCore(out guid))
			{
				ThrowHelper.ThrowFormatException(DataType.Guid);
			}
			return guid;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010C88 File Offset: 0x0000EE88
		[NullableContext(2)]
		public bool TryGetBytesFromBase64([NotNullWhen(true)] out byte[] value)
		{
			if (this.TokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			if (this.ValueIsEscaped)
			{
				return JsonReaderHelper.TryGetUnescapedBase64Bytes(readOnlySpan2, out value);
			}
			return JsonReaderHelper.TryDecodeBase64(readOnlySpan2, out value);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		public bool TryGetByte(out byte value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetByteCore(out value, readOnlySpan2);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010D30 File Offset: 0x0000EF30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetByteCore(out byte value, ReadOnlySpan<byte> span)
		{
			byte b;
			int num;
			if (Utf8Parser.TryParse(span, out b, out num, '\0') && span.Length == num)
			{
				value = b;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010D60 File Offset: 0x0000EF60
		[CLSCompliant(false)]
		public bool TryGetSByte(out sbyte value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetSByteCore(out value, readOnlySpan2);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00010DAC File Offset: 0x0000EFAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetSByteCore(out sbyte value, ReadOnlySpan<byte> span)
		{
			sbyte b;
			int num;
			if (Utf8Parser.TryParse(span, out b, out num, '\0') && span.Length == num)
			{
				value = b;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public bool TryGetInt16(out short value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetInt16Core(out value, readOnlySpan2);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010E28 File Offset: 0x0000F028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetInt16Core(out short value, ReadOnlySpan<byte> span)
		{
			short num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010E58 File Offset: 0x0000F058
		public bool TryGetInt32(out int value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetInt32Core(out value, readOnlySpan2);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetInt32Core(out int value, ReadOnlySpan<byte> span)
		{
			int num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public bool TryGetInt64(out long value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetInt64Core(out value, readOnlySpan2);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010F20 File Offset: 0x0000F120
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetInt64Core(out long value, ReadOnlySpan<byte> span)
		{
			long num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0L;
			return false;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010F50 File Offset: 0x0000F150
		[CLSCompliant(false)]
		public bool TryGetUInt16(out ushort value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetUInt16Core(out value, readOnlySpan2);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010F9C File Offset: 0x0000F19C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetUInt16Core(out ushort value, ReadOnlySpan<byte> span)
		{
			ushort num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010FCC File Offset: 0x0000F1CC
		[CLSCompliant(false)]
		public bool TryGetUInt32(out uint value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetUInt32Core(out value, readOnlySpan2);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011018 File Offset: 0x0000F218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetUInt32Core(out uint value, ReadOnlySpan<byte> span)
		{
			uint num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0U;
			return false;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00011048 File Offset: 0x0000F248
		[CLSCompliant(false)]
		public bool TryGetUInt64(out ulong value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetUInt64Core(out value, readOnlySpan2);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011094 File Offset: 0x0000F294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetUInt64Core(out ulong value, ReadOnlySpan<byte> span)
		{
			ulong num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0UL;
			return false;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public bool TryGetSingle(out float value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			float num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan2, out num, out num2, '\0') && readOnlySpan2.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0f;
			return false;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00011130 File Offset: 0x0000F330
		public bool TryGetDouble(out double value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			double num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan2, out num, out num2, '\0') && readOnlySpan2.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0.0;
			return false;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000111A0 File Offset: 0x0000F3A0
		public bool TryGetDecimal(out decimal value)
		{
			if (this.TokenType != JsonTokenType.Number)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedNumber(this.TokenType);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!this.HasValueSequence)
			{
				readOnlySpan = this.ValueSpan;
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = this.ValueSequence;
				readOnlySpan = (in valueSequence).ToArray<byte>();
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
			return Utf8JsonReader.TryGetDecimalCore(out value, readOnlySpan2);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000111EC File Offset: 0x0000F3EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetDecimalCore(out decimal value, ReadOnlySpan<byte> span)
		{
			decimal num;
			int num2;
			if (Utf8Parser.TryParse(span, out num, out num2, '\0') && span.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0m;
			return false;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00011221 File Offset: 0x0000F421
		public bool TryGetDateTime(out DateTime value)
		{
			if (this.TokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this.TokenType);
			}
			return this.TryGetDateTimeCore(out value);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011240 File Offset: 0x0000F440
		internal unsafe bool TryGetDateTimeCore(out DateTime value)
		{
			ReadOnlySpan<byte> readOnlySpan;
			if (this.HasValueSequence)
			{
				ReadOnlySequence<byte> readOnlySequence = this.ValueSequence;
				long length = readOnlySequence.Length;
				if (!JsonHelpers.IsInRangeInclusive(length, 10L, 252L))
				{
					value = default(DateTime);
					return false;
				}
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)252], 252);
				Span<byte> span2 = span;
				readOnlySequence = this.ValueSequence;
				(in readOnlySequence).CopyTo(span2);
				readOnlySpan = span2.Slice(0, (int)length);
			}
			else
			{
				if (!JsonHelpers.IsInRangeInclusive(this.ValueSpan.Length, 10, 252))
				{
					value = default(DateTime);
					return false;
				}
				readOnlySpan = this.ValueSpan;
			}
			if (this.ValueIsEscaped)
			{
				return JsonReaderHelper.TryGetEscapedDateTime(readOnlySpan, out value);
			}
			DateTime dateTime;
			if (JsonHelpers.TryParseAsISO(readOnlySpan, out dateTime))
			{
				value = dateTime;
				return true;
			}
			value = default(DateTime);
			return false;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011310 File Offset: 0x0000F510
		public bool TryGetDateTimeOffset(out DateTimeOffset value)
		{
			if (this.TokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this.TokenType);
			}
			return this.TryGetDateTimeOffsetCore(out value);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011330 File Offset: 0x0000F530
		internal unsafe bool TryGetDateTimeOffsetCore(out DateTimeOffset value)
		{
			ReadOnlySpan<byte> readOnlySpan;
			if (this.HasValueSequence)
			{
				ReadOnlySequence<byte> readOnlySequence = this.ValueSequence;
				long length = readOnlySequence.Length;
				if (!JsonHelpers.IsInRangeInclusive(length, 10L, 252L))
				{
					value = default(DateTimeOffset);
					return false;
				}
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)252], 252);
				Span<byte> span2 = span;
				readOnlySequence = this.ValueSequence;
				(in readOnlySequence).CopyTo(span2);
				readOnlySpan = span2.Slice(0, (int)length);
			}
			else
			{
				if (!JsonHelpers.IsInRangeInclusive(this.ValueSpan.Length, 10, 252))
				{
					value = default(DateTimeOffset);
					return false;
				}
				readOnlySpan = this.ValueSpan;
			}
			if (this.ValueIsEscaped)
			{
				return JsonReaderHelper.TryGetEscapedDateTimeOffset(readOnlySpan, out value);
			}
			DateTimeOffset dateTimeOffset;
			if (JsonHelpers.TryParseAsISO(readOnlySpan, out dateTimeOffset))
			{
				value = dateTimeOffset;
				return true;
			}
			value = default(DateTimeOffset);
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011400 File Offset: 0x0000F600
		public bool TryGetGuid(out Guid value)
		{
			if (this.TokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(this.TokenType);
			}
			return this.TryGetGuidCore(out value);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011420 File Offset: 0x0000F620
		internal unsafe bool TryGetGuidCore(out Guid value)
		{
			ReadOnlySpan<byte> readOnlySpan;
			if (this.HasValueSequence)
			{
				ReadOnlySequence<byte> readOnlySequence = this.ValueSequence;
				long length = readOnlySequence.Length;
				if (length > 216L)
				{
					value = default(Guid);
					return false;
				}
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)216], 216);
				Span<byte> span2 = span;
				readOnlySequence = this.ValueSequence;
				(in readOnlySequence).CopyTo(span2);
				readOnlySpan = span2.Slice(0, (int)length);
			}
			else
			{
				if (this.ValueSpan.Length > 216)
				{
					value = default(Guid);
					return false;
				}
				readOnlySpan = this.ValueSpan;
			}
			if (this.ValueIsEscaped)
			{
				return JsonReaderHelper.TryGetEscapedGuid(readOnlySpan, out value);
			}
			Guid guid;
			int num;
			if (readOnlySpan.Length == 36 && Utf8Parser.TryParse(readOnlySpan, out guid, out num, 'D'))
			{
				value = guid;
				return true;
			}
			value = default(Guid);
			return false;
		}

		// Token: 0x0400016B RID: 363
		private ReadOnlySpan<byte> _buffer;

		// Token: 0x0400016C RID: 364
		private readonly bool _isFinalBlock;

		// Token: 0x0400016D RID: 365
		private readonly bool _isInputSequence;

		// Token: 0x0400016E RID: 366
		private long _lineNumber;

		// Token: 0x0400016F RID: 367
		private long _bytePositionInLine;

		// Token: 0x04000170 RID: 368
		private int _consumed;

		// Token: 0x04000171 RID: 369
		private bool _inObject;

		// Token: 0x04000172 RID: 370
		private bool _isNotPrimitive;

		// Token: 0x04000173 RID: 371
		private JsonTokenType _tokenType;

		// Token: 0x04000174 RID: 372
		private JsonTokenType _previousTokenType;

		// Token: 0x04000175 RID: 373
		private JsonReaderOptions _readerOptions;

		// Token: 0x04000176 RID: 374
		private BitStack _bitStack;

		// Token: 0x04000177 RID: 375
		private long _totalConsumed;

		// Token: 0x04000178 RID: 376
		private bool _isLastSegment;

		// Token: 0x04000179 RID: 377
		private readonly bool _isMultiSegment;

		// Token: 0x0400017A RID: 378
		private bool _trailingCommaBeforeComment;

		// Token: 0x0400017B RID: 379
		private SequencePosition _nextPosition;

		// Token: 0x0400017C RID: 380
		private SequencePosition _currentPosition;

		// Token: 0x0400017D RID: 381
		private readonly ReadOnlySequence<byte> _sequence;

		// Token: 0x0200011D RID: 285
		private readonly struct PartialStateForRollback
		{
			// Token: 0x06000D82 RID: 3458 RVA: 0x0003441F File Offset: 0x0003261F
			public PartialStateForRollback(long totalConsumed, long bytePositionInLine, int consumed, SequencePosition currentPosition)
			{
				this._prevTotalConsumed = totalConsumed;
				this._prevBytePositionInLine = bytePositionInLine;
				this._prevConsumed = consumed;
				this._prevCurrentPosition = currentPosition;
			}

			// Token: 0x06000D83 RID: 3459 RVA: 0x0003443E File Offset: 0x0003263E
			public SequencePosition GetStartPosition(int offset = 0)
			{
				return new SequencePosition(this._prevCurrentPosition.GetObject(), this._prevCurrentPosition.GetInteger() + this._prevConsumed + offset);
			}

			// Token: 0x04000471 RID: 1137
			public readonly long _prevTotalConsumed;

			// Token: 0x04000472 RID: 1138
			public readonly long _prevBytePositionInLine;

			// Token: 0x04000473 RID: 1139
			public readonly int _prevConsumed;

			// Token: 0x04000474 RID: 1140
			public readonly SequencePosition _prevCurrentPosition;
		}
	}
}
