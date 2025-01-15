using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CE RID: 206
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	internal ref struct ReadOnlySpan<T>
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001C288 File Offset: 0x0001A488
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001C290 File Offset: 0x0001A490
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001C29C File Offset: 0x0001A49C
		public static bool operator !=(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		[Obsolete("Equals() on ReadOnlySpan will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(System.Memory189091.SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		[Obsolete("GetHashCode() on ReadOnlySpan will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(System.Memory189091.SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		public static implicit operator ReadOnlySpan<T>(ArraySegment<T> segment)
		{
			return new ReadOnlySpan<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
		public static ReadOnlySpan<T> Empty
		{
			get
			{
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001C310 File Offset: 0x0001A510
		public ReadOnlySpan<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySpan<T>.Enumerator(this);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001C320 File Offset: 0x0001A520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlySpan<T>);
				return;
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001C350 File Offset: 0x0001A550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				this = default(ReadOnlySpan<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ReadOnlySpan(void* pointer, int length)
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = null;
			this._byteOffset = new IntPtr(pointer);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001C414 File Offset: 0x0001A614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x170000DA RID: 218
		[System.Memory.IsReadOnly]
		public ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			[return: System.Memory.IsReadOnly]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				if (this._pinnable == null)
				{
					return Unsafe.Add<T>(Unsafe.AsRef<T>(this._byteOffset.ToPointer()), index);
				}
				return Unsafe.Add<T>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset), index);
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001C490 File Offset: 0x0001A690
		[EditorBrowsable(EditorBrowsableState.Never)]
		[return: System.Memory.IsReadOnly]
		public ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001C500 File Offset: 0x0001A700
		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination.Length;
			if (length == 0)
			{
				return true;
			}
			if (length > length2)
			{
				return false;
			}
			ref T ptr = ref this.DangerousGetPinnableReference();
			ref T ptr2 = ref destination.DangerousGetPinnableReference();
			SpanHelpers.CopyTo<T>(ref ptr2, length2, ref ptr, length);
			return true;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001C54C File Offset: 0x0001A74C
		public static bool operator ==(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001C574 File Offset: 0x0001A774
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				if (this._byteOffset == MemoryExtensions.StringAdjustment)
				{
					object obj = Unsafe.As<object>(this._pinnable);
					string text;
					if ((text = obj as string) != null && this._length == text.Length)
					{
						return text;
					}
				}
				fixed (char* ptr = Unsafe.As<T, char>(this.DangerousGetPinnableReference()))
				{
					char* ptr2 = ptr;
					return new string(ptr2, 0, this._length);
				}
			}
			return string.Format("System.ReadOnlySpan<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001C628 File Offset: 0x0001A828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			int num = this._length - start;
			return new ReadOnlySpan<T>(this._pinnable, intPtr, num);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001C670 File Offset: 0x0001A870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			return new ReadOnlySpan<T>(this._pinnable, intPtr, length);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return SpanHelpers.PerTypeValues<T>.EmptyArray;
			}
			T[] array = new T[this._length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref T DangerousGetPinnableReference()
		{
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001C740 File Offset: 0x0001A940
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001C748 File Offset: 0x0001A948
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x04000230 RID: 560
		private readonly Pinnable<T> _pinnable;

		// Token: 0x04000231 RID: 561
		private readonly IntPtr _byteOffset;

		// Token: 0x04000232 RID: 562
		private readonly int _length;

		// Token: 0x02000144 RID: 324
		public ref struct Enumerator
		{
			// Token: 0x06000A11 RID: 2577 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(ReadOnlySpan<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x0002C3E8 File Offset: 0x0002A5E8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._span.Length)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002C420 File Offset: 0x0002A620
			[System.Memory.IsReadOnly]
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				[return: System.Memory.IsReadOnly]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04000321 RID: 801
			private readonly ReadOnlySpan<T> _span;

			// Token: 0x04000322 RID: 802
			private int _index;
		}
	}
}
