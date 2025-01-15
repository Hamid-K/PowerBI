using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200006A RID: 106
	[NullableContext(2)]
	[Nullable(0)]
	internal struct StringBuffer
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00018C1D File Offset: 0x00016E1D
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00018C25 File Offset: 0x00016E25
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
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00018C2E File Offset: 0x00016E2E
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018C39 File Offset: 0x00016E39
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018C48 File Offset: 0x00016E48
		[NullableContext(0)]
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00018C58 File Offset: 0x00016E58
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

		// Token: 0x060005CC RID: 1484 RVA: 0x00018C98 File Offset: 0x00016E98
		[NullableContext(0)]
		public void Append([Nullable(2)] IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00018CE5 File Offset: 0x00016EE5
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00018D0C File Offset: 0x00016F0C
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

		// Token: 0x060005CF RID: 1487 RVA: 0x00018D57 File Offset: 0x00016F57
		[NullableContext(0)]
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018D66 File Offset: 0x00016F66
		[NullableContext(0)]
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00018D75 File Offset: 0x00016F75
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x04000202 RID: 514
		private char[] _buffer;

		// Token: 0x04000203 RID: 515
		private int _position;
	}
}
