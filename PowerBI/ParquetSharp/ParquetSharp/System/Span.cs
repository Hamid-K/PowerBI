using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000CF RID: 207
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	internal ref struct Span<T>
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001C750 File Offset: 0x0001A950
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001C758 File Offset: 0x0001A958
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001C764 File Offset: 0x0001A964
		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001C770 File Offset: 0x0001A970
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(System.Memory189091.SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001C77C File Offset: 0x0001A97C
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(System.Memory189091.SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001C788 File Offset: 0x0001A988
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001C790 File Offset: 0x0001A990
		public static implicit operator Span<T>(ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001C85C File Offset: 0x0001AA5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Span<T> Create(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
			int num = array.Length - start;
			return new Span<T>(Unsafe.As<Pinnable<T>>(array), intPtr, num);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001C990 File Offset: 0x0001AB90
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
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

		// Token: 0x0600073A RID: 1850 RVA: 0x0001C9E4 File Offset: 0x0001ABE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x170000E0 RID: 224
		public ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		// Token: 0x0600073C RID: 1852 RVA: 0x0001CA60 File Offset: 0x0001AC60
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x0600073D RID: 1853 RVA: 0x0001CABC File Offset: 0x0001ACBC
		public unsafe void Clear()
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			UIntPtr uintPtr = (UIntPtr)((ulong)length * (ulong)((long)Unsafe.SizeOf<T>()));
			if ((Unsafe.SizeOf<T>() & (sizeof(IntPtr) - 1)) != 0)
			{
				if (this._pinnable == null)
				{
					byte* ptr = (byte*)this._byteOffset.ToPointer();
					SpanHelpers.ClearLessThanPointerSized(ptr, uintPtr);
					return;
				}
				ref byte ptr2 = ref Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
				SpanHelpers.ClearLessThanPointerSized(ref ptr2, uintPtr);
				return;
			}
			else
			{
				if (SpanHelpers.IsReferenceOrContainsReferences<T>())
				{
					UIntPtr uintPtr2 = (UIntPtr)((ulong)((long)(length * Unsafe.SizeOf<T>() / sizeof(IntPtr))));
					ref IntPtr ptr3 = ref Unsafe.As<T, IntPtr>(this.DangerousGetPinnableReference());
					SpanHelpers.ClearPointerSizedWithReferences(ref ptr3, uintPtr2);
					return;
				}
				ref byte ptr4 = ref Unsafe.As<T, byte>(this.DangerousGetPinnableReference());
				SpanHelpers.ClearPointerSizedWithoutReferences(ref ptr4, uintPtr);
				return;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001CB94 File Offset: 0x0001AD94
		public unsafe void Fill(T value)
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			if (Unsafe.SizeOf<T>() != 1)
			{
				ref T ptr = ref this.DangerousGetPinnableReference();
				int i;
				for (i = 0; i < (length & -8); i += 8)
				{
					*Unsafe.Add<T>(ref ptr, i) = value;
					*Unsafe.Add<T>(ref ptr, i + 1) = value;
					*Unsafe.Add<T>(ref ptr, i + 2) = value;
					*Unsafe.Add<T>(ref ptr, i + 3) = value;
					*Unsafe.Add<T>(ref ptr, i + 4) = value;
					*Unsafe.Add<T>(ref ptr, i + 5) = value;
					*Unsafe.Add<T>(ref ptr, i + 6) = value;
					*Unsafe.Add<T>(ref ptr, i + 7) = value;
				}
				if (i < (length & -4))
				{
					*Unsafe.Add<T>(ref ptr, i) = value;
					*Unsafe.Add<T>(ref ptr, i + 1) = value;
					*Unsafe.Add<T>(ref ptr, i + 2) = value;
					*Unsafe.Add<T>(ref ptr, i + 3) = value;
					i += 4;
				}
				while (i < length)
				{
					*Unsafe.Add<T>(ref ptr, i) = value;
					i++;
				}
				return;
			}
			byte b = *Unsafe.As<T, byte>(ref value);
			if (this._pinnable == null)
			{
				Unsafe.InitBlockUnaligned(this._byteOffset.ToPointer(), b, (uint)length);
				return;
			}
			ref byte ptr2 = ref Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
			Unsafe.InitBlockUnaligned(ref ptr2, b, (uint)length);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001CD28 File Offset: 0x0001AF28
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001CD3C File Offset: 0x0001AF3C
		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination._length;
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

		// Token: 0x06000741 RID: 1857 RVA: 0x0001CD88 File Offset: 0x0001AF88
		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001CDB0 File Offset: 0x0001AFB0
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pinnable, span._byteOffset, span._length);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this.DangerousGetPinnableReference()))
				{
					char* ptr2 = ptr;
					return new string(ptr2, 0, this._length);
				}
			}
			return string.Format("System.Span<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001CE3C File Offset: 0x0001B03C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			int num = this._length - start;
			return new Span<T>(this._pinnable, intPtr, num);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001CE84 File Offset: 0x0001B084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr intPtr = this._byteOffset.Add(start);
			return new Span<T>(this._pinnable, intPtr, length);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001CED0 File Offset: 0x0001B0D0
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

		// Token: 0x06000747 RID: 1863 RVA: 0x0001CF0C File Offset: 0x0001B10C
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001CF54 File Offset: 0x0001B154
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001CF5C File Offset: 0x0001B15C
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x04000233 RID: 563
		private readonly Pinnable<T> _pinnable;

		// Token: 0x04000234 RID: 564
		private readonly IntPtr _byteOffset;

		// Token: 0x04000235 RID: 565
		private readonly int _length;

		// Token: 0x02000145 RID: 325
		public ref struct Enumerator
		{
			// Token: 0x06000A14 RID: 2580 RVA: 0x0002C434 File Offset: 0x0002A634
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x0002C444 File Offset: 0x0002A644
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

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002C47C File Offset: 0x0002A67C
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04000323 RID: 803
			private readonly Span<T> _span;

			// Token: 0x04000324 RID: 804
			private int _index;
		}
	}
}
