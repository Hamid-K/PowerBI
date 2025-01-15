using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000CD RID: 205
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	internal struct ReadOnlyMemory<T>
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlyMemory<T>);
				return;
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001BCF8 File Offset: 0x00019EF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(ReadOnlyMemory<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001BD58 File Offset: 0x00019F58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlyMemory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001BD70 File Offset: 0x00019F70
		public static implicit operator ReadOnlyMemory<T>(T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001BD78 File Offset: 0x00019F78
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public static ReadOnlyMemory<T> Empty
		{
			get
			{
				return default(ReadOnlyMemory<T>);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		public bool IsEmpty
		{
			get
			{
				return (this._length & int.MaxValue) == 0;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.ReadOnlyMemory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text;
			if ((text = this._object as string) == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001BE80 File Offset: 0x0001A080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = this._length & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001BF1C File Offset: 0x0001A11C
		public ReadOnlySpan<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._index < 0)
				{
					return ((MemoryManager<T>)this._object).GetSpan().Slice(this._index & int.MaxValue, this._length);
				}
				string text;
				if (typeof(T) == typeof(char) && (text = this._object as string) != null)
				{
					return new ReadOnlySpan<T>(Unsafe.As<Pinnable<T>>(text), MemoryExtensions.StringAdjustment, text.Length).Slice(this._index, this._length);
				}
				if (this._object != null)
				{
					return new ReadOnlySpan<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001C024 File Offset: 0x0001A224
		public bool TryCopyTo(Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C04C File Offset: 0x0001A24C
		public unsafe MemoryHandle Pin()
		{
			if (this._index < 0)
			{
				return ((MemoryManager<T>)this._object).Pin(this._index & int.MaxValue);
			}
			string text;
			if (typeof(T) == typeof(char) && (text = this._object as string) != null)
			{
				GCHandle gchandle = GCHandle.Alloc(text, GCHandleType.Pinned);
				void* ptr = Unsafe.Add<T>((void*)gchandle.AddrOfPinnedObject(), this._index);
				return new MemoryHandle(ptr, gchandle, null);
			}
			T[] array;
			if ((array = this._object as T[]) == null)
			{
				return default(MemoryHandle);
			}
			if (this._length < 0)
			{
				void* ptr2 = Unsafe.Add<T>(Unsafe.AsPointer<T>(MemoryMarshal.GetReference<T>(array)), this._index);
				return new MemoryHandle(ptr2, default(GCHandle), null);
			}
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			void* ptr3 = Unsafe.Add<T>((void*)gchandle2.AddrOfPinnedObject(), this._index);
			return new MemoryHandle(ptr3, gchandle2, null);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001C164 File Offset: 0x0001A364
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001C184 File Offset: 0x0001A384
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				ReadOnlyMemory<T> readOnlyMemory = (ReadOnlyMemory<T>)obj;
				return this.Equals(readOnlyMemory);
			}
			if (obj is Memory<T>)
			{
				Memory<T> memory = (Memory<T>)obj;
				return this.Equals(memory);
			}
			return false;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		public bool Equals(ReadOnlyMemory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001C208 File Offset: 0x0001A408
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return ReadOnlyMemory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001C254 File Offset: 0x0001A454
		private static int CombineHashCodes(int left, int right)
		{
			return ((left << 5) + left) ^ right;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001C260 File Offset: 0x0001A460
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return ReadOnlyMemory<T>.CombineHashCodes(ReadOnlyMemory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001C270 File Offset: 0x0001A470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal object GetObjectStartLength(out int start, out int length)
		{
			start = this._index;
			length = this._length;
			return this._object;
		}

		// Token: 0x0400022C RID: 556
		private readonly object _object;

		// Token: 0x0400022D RID: 557
		private readonly int _index;

		// Token: 0x0400022E RID: 558
		private readonly int _length;

		// Token: 0x0400022F RID: 559
		internal const int RemoveFlagsBitMask = 2147483647;
	}
}
