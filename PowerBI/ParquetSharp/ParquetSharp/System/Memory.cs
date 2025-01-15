using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000CA RID: 202
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	internal struct Memory<T>
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x00019848 File Offset: 0x00017A48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array)
		{
			if (array == null)
			{
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000198B0 File Offset: 0x00017AB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = array.Length - start;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00019934 File Offset: 0x00017B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000199C4 File Offset: 0x00017BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int length)
		{
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = int.MinValue;
			this._length = length;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000199EC File Offset: 0x00017BEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int start, int length)
		{
			if (length < 0 || start < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = start | int.MinValue;
			this._length = length;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00019A1C File Offset: 0x00017C1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00019A34 File Offset: 0x00017C34
		public static implicit operator Memory<T>(T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00019A3C File Offset: 0x00017C3C
		public static implicit operator Memory<T>(ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00019A68 File Offset: 0x00017C68
		public unsafe static implicit operator ReadOnlyMemory<T>(Memory<T> memory)
		{
			return *Unsafe.As<Memory<T>, ReadOnlyMemory<T>>(ref memory);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00019A78 File Offset: 0x00017C78
		public static Memory<T> Empty
		{
			get
			{
				return default(Memory<T>);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00019A94 File Offset: 0x00017C94
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public bool IsEmpty
		{
			get
			{
				return (this._length & int.MaxValue) == 0;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00019AB8 File Offset: 0x00017CB8
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.Memory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text;
			if ((text = this._object as string) == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00019B54 File Offset: 0x00017D54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00019B98 File Offset: 0x00017D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = length2 & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00019BEC File Offset: 0x00017DEC
		public Span<T> Span
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
					return new Span<T>(Unsafe.As<Pinnable<T>>(text), MemoryExtensions.StringAdjustment, text.Length).Slice(this._index, this._length);
				}
				if (this._object != null)
				{
					return new Span<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(Span<T>);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00019CC8 File Offset: 0x00017EC8
		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00019CF0 File Offset: 0x00017EF0
		public bool TryCopyTo(Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00019D18 File Offset: 0x00017F18
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

		// Token: 0x060006A1 RID: 1697 RVA: 0x00019E30 File Offset: 0x00018030
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00019E50 File Offset: 0x00018050
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			if (obj is Memory<T>)
			{
				Memory<T> memory = (Memory<T>)obj;
				return this.Equals(memory);
			}
			return false;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00019EA4 File Offset: 0x000180A4
		public bool Equals(Memory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00019ED8 File Offset: 0x000180D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return Memory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00019F24 File Offset: 0x00018124
		private static int CombineHashCodes(int left, int right)
		{
			return ((left << 5) + left) ^ right;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00019F30 File Offset: 0x00018130
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Memory<T>.CombineHashCodes(Memory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x04000226 RID: 550
		private readonly object _object;

		// Token: 0x04000227 RID: 551
		private readonly int _index;

		// Token: 0x04000228 RID: 552
		private readonly int _length;

		// Token: 0x04000229 RID: 553
		private const int RemoveFlagsBitMask = 2147483647;
	}
}
