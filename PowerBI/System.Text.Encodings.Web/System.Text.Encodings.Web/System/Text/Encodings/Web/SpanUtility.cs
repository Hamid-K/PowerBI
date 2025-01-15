using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000027 RID: 39
	internal static class SpanUtility
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005515 File Offset: 0x00003715
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidIndex<T>(ReadOnlySpan<T> span, int index)
		{
			return index < span.Length;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005521 File Offset: 0x00003721
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidIndex<T>(Span<T> span, int index)
		{
			return index < span.Length;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005530 File Offset: 0x00003730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteBytes(Span<byte> span, byte a, byte b, byte c, byte d)
		{
			if (span.Length >= 4)
			{
				uint num;
				if (BitConverter.IsLittleEndian)
				{
					num = (uint)(((int)d << 24) | ((int)c << 16) | ((int)b << 8) | (int)a);
				}
				else
				{
					num = (uint)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | (int)d);
				}
				Unsafe.WriteUnaligned<uint>(MemoryMarshal.GetReference<byte>(span), num);
				return true;
			}
			return false;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005584 File Offset: 0x00003784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool TryWriteBytes(Span<byte> span, byte a, byte b, byte c, byte d, byte e)
		{
			if (span.Length >= 5)
			{
				uint num;
				if (BitConverter.IsLittleEndian)
				{
					num = (uint)(((int)d << 24) | ((int)c << 16) | ((int)b << 8) | (int)a);
				}
				else
				{
					num = (uint)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | (int)d);
				}
				ref byte reference = ref MemoryMarshal.GetReference<byte>(span);
				Unsafe.WriteUnaligned<uint>(ref reference, num);
				*Unsafe.Add<byte>(ref reference, 4) = e;
				return true;
			}
			return false;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000055E4 File Offset: 0x000037E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteBytes(Span<byte> span, byte a, byte b, byte c, byte d, byte e, byte f)
		{
			if (span.Length >= 6)
			{
				uint num;
				uint num2;
				if (BitConverter.IsLittleEndian)
				{
					num = (uint)(((int)d << 24) | ((int)c << 16) | ((int)b << 8) | (int)a);
					num2 = (uint)(((int)f << 8) | (int)e);
				}
				else
				{
					num = (uint)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | (int)d);
					num2 = (uint)(((int)e << 8) | (int)f);
				}
				ref byte reference = ref MemoryMarshal.GetReference<byte>(span);
				Unsafe.WriteUnaligned<uint>(ref reference, num);
				Unsafe.WriteUnaligned<ushort>(Unsafe.Add<byte>(ref reference, 4), (ushort)num2);
				return true;
			}
			return false;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005658 File Offset: 0x00003858
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteChars(Span<char> span, char a, char b, char c, char d)
		{
			if (span.Length >= 4)
			{
				ulong num;
				if (BitConverter.IsLittleEndian)
				{
					num = ((ulong)d << 48) | ((ulong)c << 32) | ((ulong)b << 16) | (ulong)a;
				}
				else
				{
					num = ((ulong)a << 48) | ((ulong)b << 32) | ((ulong)c << 16) | (ulong)d;
				}
				Unsafe.WriteUnaligned<ulong>(Unsafe.As<char, byte>(MemoryMarshal.GetReference<char>(span)), num);
				return true;
			}
			return false;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000056B8 File Offset: 0x000038B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool TryWriteChars(Span<char> span, char a, char b, char c, char d, char e)
		{
			if (span.Length >= 5)
			{
				ulong num;
				if (BitConverter.IsLittleEndian)
				{
					num = ((ulong)d << 48) | ((ulong)c << 32) | ((ulong)b << 16) | (ulong)a;
				}
				else
				{
					num = ((ulong)a << 48) | ((ulong)b << 32) | ((ulong)c << 16) | (ulong)d;
				}
				ref char reference = ref MemoryMarshal.GetReference<char>(span);
				Unsafe.WriteUnaligned<ulong>(Unsafe.As<char, byte>(ref reference), num);
				*Unsafe.Add<char>(ref reference, 4) = e;
				return true;
			}
			return false;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005724 File Offset: 0x00003924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteChars(Span<char> span, char a, char b, char c, char d, char e, char f)
		{
			if (span.Length >= 6)
			{
				ulong num;
				uint num2;
				if (BitConverter.IsLittleEndian)
				{
					num = ((ulong)d << 48) | ((ulong)c << 32) | ((ulong)b << 16) | (ulong)a;
					num2 = (uint)(((uint)f << 16) | e);
				}
				else
				{
					num = ((ulong)a << 48) | ((ulong)b << 32) | ((ulong)c << 16) | (ulong)d;
					num2 = (uint)(((uint)e << 16) | f);
				}
				ref byte ptr = ref Unsafe.As<char, byte>(MemoryMarshal.GetReference<char>(span));
				Unsafe.WriteUnaligned<ulong>(ref ptr, num);
				Unsafe.WriteUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref ptr, (IntPtr)8), num2);
				return true;
			}
			return false;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000057A6 File Offset: 0x000039A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64LittleEndian(Span<byte> span, int offset, ulong value)
		{
			if (SpanUtility.AreValidIndexAndLength(span.Length, offset, 8))
			{
				if (!BitConverter.IsLittleEndian)
				{
					value = BinaryPrimitives.ReverseEndianness(value);
				}
				Unsafe.WriteUnaligned<ulong>(Unsafe.Add<byte>(MemoryMarshal.GetReference<byte>(span), (IntPtr)((UIntPtr)offset)), value);
				return true;
			}
			return false;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000057DD File Offset: 0x000039DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool AreValidIndexAndLength(int spanRealLength, int requestedOffset, int requestedLength)
		{
			if (IntPtr.Size == 4)
			{
				if (requestedOffset > spanRealLength)
				{
					return false;
				}
				if (requestedLength > spanRealLength - requestedOffset)
				{
					return false;
				}
			}
			else if ((ulong)spanRealLength < (ulong)requestedOffset + (ulong)requestedLength)
			{
				return false;
			}
			return true;
		}
	}
}
