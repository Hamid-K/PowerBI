using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000011 RID: 17
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly ref struct Span<T>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000056FF File Offset: 0x000038FF
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005707 File Offset: 0x00003907
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005712 File Offset: 0x00003912
		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000052F1 File Offset: 0x000034F1
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000052FD File Offset: 0x000034FD
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000571E File Offset: 0x0000391E
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005726 File Offset: 0x00003926
		public static implicit operator Span<T>(ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005744 File Offset: 0x00003944
		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000575A File Offset: 0x0000395A
		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005768 File Offset: 0x00003968
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

		// Token: 0x060000F7 RID: 247 RVA: 0x000057CC File Offset: 0x000039CC
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

		// Token: 0x060000F8 RID: 248 RVA: 0x00005848 File Offset: 0x00003A48
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

		// Token: 0x060000F9 RID: 249 RVA: 0x000058D0 File Offset: 0x00003AD0
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

		// Token: 0x060000FA RID: 250 RVA: 0x0000590C File Offset: 0x00003B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x17000016 RID: 22
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

		// Token: 0x060000FC RID: 252 RVA: 0x00005980 File Offset: 0x00003B80
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

		// Token: 0x060000FD RID: 253 RVA: 0x000059D0 File Offset: 0x00003BD0
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
				ref byte ptr2 = Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
				SpanHelpers.ClearLessThanPointerSized(ref ptr2, uintPtr);
				return;
			}
			else
			{
				if (SpanHelpers.IsReferenceOrContainsReferences<T>())
				{
					UIntPtr uintPtr2 = (UIntPtr)((ulong)((long)(length * Unsafe.SizeOf<T>() / sizeof(IntPtr))));
					ref IntPtr ptr3 = Unsafe.As<T, IntPtr>(this.DangerousGetPinnableReference());
					SpanHelpers.ClearPointerSizedWithReferences(ref ptr3, uintPtr2);
					return;
				}
				ref byte ptr4 = Unsafe.As<T, byte>(this.DangerousGetPinnableReference());
				SpanHelpers.ClearPointerSizedWithoutReferences(ref ptr4, uintPtr);
				return;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A98 File Offset: 0x00003C98
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
			ref byte ptr2 = Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
			Unsafe.InitBlockUnaligned(ref ptr2, b, (uint)length);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005C17 File Offset: 0x00003E17
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005C28 File Offset: 0x00003E28
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

		// Token: 0x06000101 RID: 257 RVA: 0x00005C67 File Offset: 0x00003E67
		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005C8C File Offset: 0x00003E8C
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pinnable, span._byteOffset, span._length);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005CA8 File Offset: 0x00003EA8
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

		// Token: 0x06000104 RID: 260 RVA: 0x00005D14 File Offset: 0x00003F14
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

		// Token: 0x06000105 RID: 261 RVA: 0x00005D54 File Offset: 0x00003F54
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

		// Token: 0x06000106 RID: 262 RVA: 0x00005D98 File Offset: 0x00003F98
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

		// Token: 0x06000107 RID: 263 RVA: 0x00005DCC File Offset: 0x00003FCC
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005E0B File Offset: 0x0000400B
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005E13 File Offset: 0x00004013
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly Pinnable<T> _pinnable;

		// Token: 0x0400005C RID: 92
		private readonly IntPtr _byteOffset;

		// Token: 0x0400005D RID: 93
		private readonly int _length;

		// Token: 0x02000034 RID: 52
		public ref struct Enumerator
		{
			// Token: 0x060002A7 RID: 679 RVA: 0x00012819 File Offset: 0x00010A19
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x0001282C File Offset: 0x00010A2C
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

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001285A File Offset: 0x00010A5A
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x040000D8 RID: 216
			private readonly Span<T> _span;

			// Token: 0x040000D9 RID: 217
			private int _index;
		}
	}
}
