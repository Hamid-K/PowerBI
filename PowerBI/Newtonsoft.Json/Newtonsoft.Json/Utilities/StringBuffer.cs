using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006A RID: 106
	[NullableContext(2)]
	[Nullable(0)]
	internal struct StringBuffer
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x000191D1 File Offset: 0x000173D1
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x000191D9 File Offset: 0x000173D9
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000191E2 File Offset: 0x000173E2
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000191ED File Offset: 0x000173ED
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000191FC File Offset: 0x000173FC
		[NullableContext(1)]
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001920C File Offset: 0x0001740C
		public void Append(IArrayPool<char> bufferPool, char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(bufferPool, 1);
			}
			char[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001924C File Offset: 0x0001744C
		[NullableContext(1)]
		public void Append([Nullable(2)] IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019299 File Offset: 0x00017499
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000192C0 File Offset: 0x000174C0
		private void EnsureSize(IArrayPool<char> bufferPool, int appendLength)
		{
			char[] array = BufferUtils.RentBuffer(bufferPool, (this._position + appendLength) * 2);
			if (this._buffer != null)
			{
				Array.Copy(this._buffer, array, this._position);
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
			}
			this._buffer = array;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001930B File Offset: 0x0001750B
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001931A File Offset: 0x0001751A
		[NullableContext(1)]
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00019329 File Offset: 0x00017529
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x0400021C RID: 540
		private char[] _buffer;

		// Token: 0x0400021D RID: 541
		private int _position;
	}
}
