using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200001D RID: 29
	internal ref struct ValueStringBuilder
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002976 File Offset: 0x00000B76
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000298D File Offset: 0x00000B8D
		public ValueStringBuilder(int initialCapacity)
		{
			this._arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialCapacity);
			this._chars = this._arrayToReturnToPool;
			this._pos = 0;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000029C0 File Offset: 0x00000BC0
		public int Length
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000029C9 File Offset: 0x00000BC9
		public int Capacity
		{
			get
			{
				return this._chars.Length;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000029D6 File Offset: 0x00000BD6
		public void EnsureCapacity(int capacity)
		{
			if (capacity > this._chars.Length)
			{
				this.Grow(capacity - this._pos);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000029F4 File Offset: 0x00000BF4
		public ref char GetPinnableReference()
		{
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002A01 File Offset: 0x00000C01
		public unsafe ref char GetPinnableReference(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x17000013 RID: 19
		public ref char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A40 File Offset: 0x00000C40
		public override string ToString()
		{
			string text = this._chars.Slice(0, this._pos).ToString();
			this.Dispose();
			return text;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002A75 File Offset: 0x00000C75
		public Span<char> RawChars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A7D File Offset: 0x00000C7D
		public unsafe ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002ABA File Offset: 0x00000CBA
		public ReadOnlySpan<char> AsSpan()
		{
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002AD3 File Offset: 0x00000CD3
		public ReadOnlySpan<char> AsSpan(int start)
		{
			return this._chars.Slice(start, this._pos - start);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002AEE File Offset: 0x00000CEE
		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return this._chars.Slice(start, length);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002B04 File Offset: 0x00000D04
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B48 File Offset: 0x00000D48
		public void Insert(int index, char value, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + count));
			this._chars.Slice(index, count).Fill(value);
			this._pos += count;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public void Insert(int index, string s)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + length));
			s.AsSpan().CopyTo(this._chars.Slice(index));
			this._pos += length;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C4C File Offset: 0x00000E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			Span<char> chars = this._chars;
			if (pos < chars.Length)
			{
				*chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002C8C File Offset: 0x00000E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			if (s == null)
			{
				return;
			}
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002CE0 File Offset: 0x00000EE0
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D44 File Offset: 0x00000F44
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002DAC File Offset: 0x00000FAC
		public unsafe void Append(char* value, int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E1C File Offset: 0x0000101C
		public void Append(ReadOnlySpan<char> value)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this._chars.Slice(this._pos));
			this._pos += value.Length;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E80 File Offset: 0x00001080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002EC1 File Offset: 0x000010C1
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002ED4 File Offset: 0x000010D4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int additionalCapacityBeyondPos)
		{
			int num = (int)Math.Max((uint)(this._pos + additionalCapacityBeyondPos), Math.Min((uint)(this._chars.Length * 2), 2147483591U));
			char[] array = ArrayPool<char>.Shared.Rent(num);
			this._chars.Slice(0, this._pos).CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F60 File Offset: 0x00001160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04000019 RID: 25
		private char[] _arrayToReturnToPool;

		// Token: 0x0400001A RID: 26
		private Span<char> _chars;

		// Token: 0x0400001B RID: 27
		private int _pos;
	}
}
