using System;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000056 RID: 86
	internal class InternetMessageFormatHeaderParser
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000B76F File Offset: 0x0000996F
		public InternetMessageFormatHeaderParser(HttpHeaders headers, int maxHeaderSize)
			: this(headers, maxHeaderSize, false)
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B77C File Offset: 0x0000997C
		public InternetMessageFormatHeaderParser(HttpHeaders headers, int maxHeaderSize, bool ignoreHeaderValidation)
		{
			if (maxHeaderSize < 2)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxHeaderSize", maxHeaderSize, 2);
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			this._headers = headers;
			this._maxHeaderSize = maxHeaderSize;
			this._ignoreHeaderValidation = ignoreHeaderValidation;
			this._currentHeader = new InternetMessageFormatHeaderParser.CurrentHeaderFieldStore();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState parserState = ParserState.NeedMoreData;
			if (bytesConsumed >= bytesReady)
			{
				return parserState;
			}
			try
			{
				parserState = InternetMessageFormatHeaderParser.ParseHeaderFields(buffer, bytesReady, ref bytesConsumed, ref this._headerState, this._maxHeaderSize, ref this._totalBytesConsumed, this._currentHeader, this._headers, this._ignoreHeaderValidation);
			}
			catch (Exception)
			{
				parserState = ParserState.Invalid;
			}
			return parserState;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B844 File Offset: 0x00009A44
		private static ParserState ParseHeaderFields(byte[] buffer, int bytesReady, ref int bytesConsumed, ref InternetMessageFormatHeaderParser.HeaderFieldState requestHeaderState, int maximumHeaderLength, ref int totalBytesConsumed, InternetMessageFormatHeaderParser.CurrentHeaderFieldStore currentField, HttpHeaders headers, bool ignoreHeaderValidation)
		{
			int num = bytesConsumed;
			ParserState parserState = ParserState.DataTooBig;
			int num2 = ((maximumHeaderLength <= 0) ? int.MaxValue : (maximumHeaderLength - totalBytesConsumed + num));
			if (bytesReady < num2)
			{
				parserState = ParserState.NeedMoreData;
				num2 = bytesReady;
			}
			switch (requestHeaderState)
			{
			case InternetMessageFormatHeaderParser.HeaderFieldState.Name:
				break;
			case InternetMessageFormatHeaderParser.HeaderFieldState.Value:
				goto IL_00F1;
			case InternetMessageFormatHeaderParser.HeaderFieldState.AfterCarriageReturn:
				goto IL_016B;
			case InternetMessageFormatHeaderParser.HeaderFieldState.FoldingLine:
				goto IL_019C;
			default:
				goto IL_01E8;
			}
			IL_0042:
			int num3 = bytesConsumed;
			int num4;
			while (buffer[bytesConsumed] != 58)
			{
				if (buffer[bytesConsumed] == 13)
				{
					if (!currentField.IsEmpty())
					{
						parserState = ParserState.Invalid;
						goto IL_01E8;
					}
					requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.AfterCarriageReturn;
					num4 = bytesConsumed + 1;
					bytesConsumed = num4;
					if (num4 == num2)
					{
						goto IL_01E8;
					}
					goto IL_016B;
				}
				else
				{
					num4 = bytesConsumed + 1;
					bytesConsumed = num4;
					if (num4 == num2)
					{
						string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
						currentField.Name.Append(@string);
						goto IL_01E8;
					}
				}
			}
			if (bytesConsumed > num3)
			{
				string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentField.Name.Append(string2);
			}
			requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.Value;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_01E8;
			}
			IL_00F1:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentField.Value.Append(string3);
					goto IL_01E8;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentField.Value.Append(string4);
			}
			requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.AfterCarriageReturn;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_01E8;
			}
			IL_016B:
			if (buffer[bytesConsumed] != 10)
			{
				parserState = ParserState.Invalid;
				goto IL_01E8;
			}
			if (currentField.IsEmpty())
			{
				parserState = ParserState.Done;
				bytesConsumed++;
				goto IL_01E8;
			}
			requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.FoldingLine;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_01E8;
			}
			IL_019C:
			if (buffer[bytesConsumed] != 32 && buffer[bytesConsumed] != 9)
			{
				currentField.CopyTo(headers, ignoreHeaderValidation);
				requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.Name;
				if (bytesConsumed != num2)
				{
					goto IL_0042;
				}
			}
			else
			{
				currentField.Value.Append(' ');
				requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.Value;
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 != num2)
				{
					goto IL_00F1;
				}
			}
			IL_01E8:
			totalBytesConsumed += bytesConsumed - num;
			return parserState;
		}

		// Token: 0x0400010E RID: 270
		internal const int MinHeaderSize = 2;

		// Token: 0x0400010F RID: 271
		private int _totalBytesConsumed;

		// Token: 0x04000110 RID: 272
		private int _maxHeaderSize;

		// Token: 0x04000111 RID: 273
		private InternetMessageFormatHeaderParser.HeaderFieldState _headerState;

		// Token: 0x04000112 RID: 274
		private HttpHeaders _headers;

		// Token: 0x04000113 RID: 275
		private InternetMessageFormatHeaderParser.CurrentHeaderFieldStore _currentHeader;

		// Token: 0x04000114 RID: 276
		private readonly bool _ignoreHeaderValidation;

		// Token: 0x02000087 RID: 135
		private enum HeaderFieldState
		{
			// Token: 0x040001E6 RID: 486
			Name,
			// Token: 0x040001E7 RID: 487
			Value,
			// Token: 0x040001E8 RID: 488
			AfterCarriageReturn,
			// Token: 0x040001E9 RID: 489
			FoldingLine
		}

		// Token: 0x02000088 RID: 136
		private class CurrentHeaderFieldStore
		{
			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000E99D File Offset: 0x0000CB9D
			public StringBuilder Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000E9A5 File Offset: 0x0000CBA5
			public StringBuilder Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
			public void CopyTo(HttpHeaders headers, bool ignoreHeaderValidation)
			{
				string text = this._name.ToString();
				string text2 = this._value.ToString().Trim(InternetMessageFormatHeaderParser.CurrentHeaderFieldStore._linearWhiteSpace);
				if (ignoreHeaderValidation)
				{
					headers.TryAddWithoutValidation(text, text2);
				}
				else
				{
					headers.Add(text, text2);
				}
				this.Clear();
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x0000E9FB File Offset: 0x0000CBFB
			public bool IsEmpty()
			{
				return this._name.Length == 0 && this._value.Length == 0;
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x0000EA1A File Offset: 0x0000CC1A
			private void Clear()
			{
				this._name.Clear();
				this._value.Clear();
			}

			// Token: 0x040001EA RID: 490
			private const int DefaultFieldNameAllocation = 128;

			// Token: 0x040001EB RID: 491
			private const int DefaultFieldValueAllocation = 2048;

			// Token: 0x040001EC RID: 492
			private static readonly char[] _linearWhiteSpace = new char[] { ' ', '\t' };

			// Token: 0x040001ED RID: 493
			private readonly StringBuilder _name = new StringBuilder(128);

			// Token: 0x040001EE RID: 494
			private readonly StringBuilder _value = new StringBuilder(2048);
		}
	}
}
