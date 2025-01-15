using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Extensions;

namespace AngleSharp
{
	// Token: 0x02000017 RID: 23
	public sealed class TextSource : IDisposable
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003D5C File Offset: 0x00001F5C
		private TextSource(Encoding encoding)
		{
			this._buffer = new byte[4096];
			this._chars = new char[4097];
			this._raw = new MemoryStream();
			this._index = 0;
			this._encoding = encoding ?? TextEncoding.Utf8;
			this._decoder = this._encoding.GetDecoder();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public TextSource(string source)
			: this(null, TextEncoding.Utf8)
		{
			this._finished = true;
			this._content.Append(source);
			this._confidence = TextSource.EncodingConfidence.Irrelevant;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003DEB File Offset: 0x00001FEB
		public TextSource(Stream baseStream, Encoding encoding = null)
			: this(encoding)
		{
			this._baseStream = baseStream;
			this._content = Pool.NewStringBuilder();
			this._confidence = TextSource.EncodingConfidence.Tentative;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003E0D File Offset: 0x0000200D
		public string Text
		{
			get
			{
				return this._content.ToString();
			}
		}

		// Token: 0x17000023 RID: 35
		public char this[int index]
		{
			get
			{
				return TextSource.Replace(this._content[index]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003E2D File Offset: 0x0000202D
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003E38 File Offset: 0x00002038
		public Encoding CurrentEncoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				if (this._confidence != TextSource.EncodingConfidence.Tentative)
				{
					return;
				}
				if (this._encoding.IsUnicode())
				{
					this._confidence = TextSource.EncodingConfidence.Certain;
					return;
				}
				if (value.IsUnicode())
				{
					value = TextEncoding.Utf8;
				}
				if (value == this._encoding)
				{
					this._confidence = TextSource.EncodingConfidence.Certain;
					return;
				}
				this._encoding = value;
				this._decoder = value.GetDecoder();
				byte[] array = this._raw.ToArray();
				char[] array2 = new char[this._encoding.GetMaxCharCount(array.Length)];
				int chars = this._decoder.GetChars(array, 0, array.Length, array2, 0);
				string text = new string(array2, 0, chars);
				int num = Math.Min(this._index, text.Length);
				if (text.Substring(0, num).Is(this._content.ToString(0, num)))
				{
					this._confidence = TextSource.EncodingConfidence.Certain;
					this._content.Remove(num, this._content.Length - num);
					this._content.Append(text.Substring(num));
					return;
				}
				this._index = 0;
				this._content.Clear().Append(text);
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003F59 File Offset: 0x00002159
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003F61 File Offset: 0x00002161
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003F6A File Offset: 0x0000216A
		public int Length
		{
			get
			{
				return this._content.Length;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F77 File Offset: 0x00002177
		public void Dispose()
		{
			if (this._content != null)
			{
				this._raw.Dispose();
				this._content.Clear().ToPool();
				this._content = null;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FA8 File Offset: 0x000021A8
		public char ReadCharacter()
		{
			int num;
			if (this._index < this._content.Length)
			{
				StringBuilder content = this._content;
				num = this._index;
				this._index = num + 1;
				return TextSource.Replace(content[num]);
			}
			this.ExpandBuffer(4096L);
			num = this._index;
			this._index = num + 1;
			int num2 = num;
			if (num2 >= this._content.Length)
			{
				return char.MaxValue;
			}
			return TextSource.Replace(this._content[num2]);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004030 File Offset: 0x00002230
		public string ReadCharacters(int characters)
		{
			int index = this._index;
			if (index + characters <= this._content.Length)
			{
				this._index += characters;
				return this._content.ToString(index, characters);
			}
			this.ExpandBuffer((long)Math.Max(4096, characters));
			this._index += characters;
			characters = Math.Min(characters, this._content.Length - index);
			return this._content.ToString(index, characters);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000040B2 File Offset: 0x000022B2
		public Task PrefetchAsync(int length, CancellationToken cancellationToken)
		{
			return this.ExpandBufferAsync((long)length, cancellationToken);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040C0 File Offset: 0x000022C0
		public async Task PrefetchAllAsync(CancellationToken cancellationToken)
		{
			if (this._content.Length == 0)
			{
				await this.DetectByteOrderMarkAsync(cancellationToken).ConfigureAwait(false);
			}
			while (!this._finished)
			{
				await this.ReadIntoBufferAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004110 File Offset: 0x00002310
		public void InsertText(string content)
		{
			if (this._index >= 0 && this._index < this._content.Length)
			{
				this._content.Insert(this._index, content);
			}
			else
			{
				this._content.Append(content);
			}
			this._index += content.Length;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000416E File Offset: 0x0000236E
		private static char Replace(char c)
		{
			if (c != '\uffff')
			{
				return c;
			}
			return '\ufffd';
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004180 File Offset: 0x00002380
		private async Task DetectByteOrderMarkAsync(CancellationToken cancellationToken)
		{
			int num = await this._baseStream.ReadAsync(this._buffer, 0, 4096).ConfigureAwait(false);
			int num2 = 0;
			if (num > 2 && this._buffer[0] == 239 && this._buffer[1] == 187 && this._buffer[2] == 191)
			{
				this._encoding = TextEncoding.Utf8;
				num2 = 3;
			}
			else if (num > 3 && this._buffer[0] == 255 && this._buffer[1] == 254 && this._buffer[2] == 0 && this._buffer[3] == 0)
			{
				this._encoding = TextEncoding.Utf32Le;
				num2 = 4;
			}
			else if (num > 3 && this._buffer[0] == 0 && this._buffer[1] == 0 && this._buffer[2] == 254 && this._buffer[3] == 255)
			{
				this._encoding = TextEncoding.Utf32Be;
				num2 = 4;
			}
			else if (num > 1 && this._buffer[0] == 254 && this._buffer[1] == 255)
			{
				this._encoding = TextEncoding.Utf16Be;
				num2 = 2;
			}
			else if (num > 1 && this._buffer[0] == 255 && this._buffer[1] == 254)
			{
				this._encoding = TextEncoding.Utf16Le;
				num2 = 2;
			}
			else if (num > 3 && this._buffer[0] == 132 && this._buffer[1] == 49 && this._buffer[2] == 149 && this._buffer[3] == 51)
			{
				this._encoding = TextEncoding.Gb18030;
				num2 = 4;
			}
			if (num2 > 0)
			{
				num -= num2;
				Array.Copy(this._buffer, num2, this._buffer, 0, num);
				this._decoder = this._encoding.GetDecoder();
				this._confidence = TextSource.EncodingConfidence.Certain;
			}
			this.AppendContentFromBuffer(num);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000041C8 File Offset: 0x000023C8
		private async Task ExpandBufferAsync(long size, CancellationToken cancellationToken)
		{
			if (!this._finished && this._content.Length == 0)
			{
				await this.DetectByteOrderMarkAsync(cancellationToken).ConfigureAwait(false);
			}
			while (size + (long)this._index > (long)this._content.Length && !this._finished)
			{
				await this.ReadIntoBufferAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004220 File Offset: 0x00002420
		private async Task ReadIntoBufferAsync(CancellationToken cancellationToken)
		{
			int num = await this._baseStream.ReadAsync(this._buffer, 0, 4096, cancellationToken).ConfigureAwait(false);
			this.AppendContentFromBuffer(num);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004270 File Offset: 0x00002470
		private void ExpandBuffer(long size)
		{
			if (!this._finished && this._content.Length == 0)
			{
				this.DetectByteOrderMarkAsync(CancellationToken.None).Wait();
			}
			while (size + (long)this._index > (long)this._content.Length && !this._finished)
			{
				this.ReadIntoBuffer();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000042CC File Offset: 0x000024CC
		private void ReadIntoBuffer()
		{
			int num = this._baseStream.Read(this._buffer, 0, 4096);
			this.AppendContentFromBuffer(num);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000042F8 File Offset: 0x000024F8
		private void AppendContentFromBuffer(int size)
		{
			this._finished = size == 0;
			int chars = this._decoder.GetChars(this._buffer, 0, size, this._chars, 0);
			if (this._confidence != TextSource.EncodingConfidence.Certain)
			{
				this._raw.Write(this._buffer, 0, size);
			}
			this._content.Append(this._chars, 0, chars);
		}

		// Token: 0x04000032 RID: 50
		private const int BufferSize = 4096;

		// Token: 0x04000033 RID: 51
		private readonly Stream _baseStream;

		// Token: 0x04000034 RID: 52
		private readonly MemoryStream _raw;

		// Token: 0x04000035 RID: 53
		private readonly byte[] _buffer;

		// Token: 0x04000036 RID: 54
		private readonly char[] _chars;

		// Token: 0x04000037 RID: 55
		private StringBuilder _content;

		// Token: 0x04000038 RID: 56
		private TextSource.EncodingConfidence _confidence;

		// Token: 0x04000039 RID: 57
		private bool _finished;

		// Token: 0x0400003A RID: 58
		private Encoding _encoding;

		// Token: 0x0400003B RID: 59
		private Decoder _decoder;

		// Token: 0x0400003C RID: 60
		private int _index;

		// Token: 0x0200041C RID: 1052
		private enum EncodingConfidence : byte
		{
			// Token: 0x04000D4F RID: 3407
			Tentative,
			// Token: 0x04000D50 RID: 3408
			Certain,
			// Token: 0x04000D51 RID: 3409
			Irrelevant
		}
	}
}
