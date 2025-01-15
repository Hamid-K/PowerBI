using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers.Binary
{
	// Token: 0x020000EE RID: 238
	internal static class BinaryPrimitives
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x0002A814 File Offset: 0x00028A14
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte ReverseEndianness(sbyte value)
		{
			return value;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002A818 File Offset: 0x00028A18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReverseEndianness(short value)
		{
			return (short)(((int)(value & 255) << 8) | (((int)value & 65280) >> 8));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002A830 File Offset: 0x00028A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReverseEndianness(int value)
		{
			return (int)BinaryPrimitives.ReverseEndianness((uint)value);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002A838 File Offset: 0x00028A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReverseEndianness(long value)
		{
			return (long)BinaryPrimitives.ReverseEndianness((ulong)value);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002A840 File Offset: 0x00028A40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ReverseEndianness(byte value)
		{
			return value;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002A844 File Offset: 0x00028A44
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReverseEndianness(ushort value)
		{
			return (ushort)((value >> 8) + ((int)value << 8));
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002A850 File Offset: 0x00028A50
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseEndianness(uint value)
		{
			uint num = value & 16711935U;
			uint num2 = value & 4278255360U;
			return ((num >> 8) | (num << 24)) + ((num2 << 8) | (num2 >> 24));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002A884 File Offset: 0x00028A84
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReverseEndianness(ulong value)
		{
			return ((ulong)BinaryPrimitives.ReverseEndianness((uint)value) << 32) + (ulong)BinaryPrimitives.ReverseEndianness((uint)(value >> 32));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002A8CC File Offset: 0x00028ACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32BigEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002A8F8 File Offset: 0x00028AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64BigEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002A924 File Offset: 0x00028B24
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002A950 File Offset: 0x00028B50
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002A97C File Offset: 0x00028B7C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64BigEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt16BigEndian(ReadOnlySpan<byte> source, out short value)
		{
			bool flag = MemoryMarshal.TryRead<short>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002A9D8 File Offset: 0x00028BD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt32BigEndian(ReadOnlySpan<byte> source, out int value)
		{
			bool flag = MemoryMarshal.TryRead<int>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002AA08 File Offset: 0x00028C08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt64BigEndian(ReadOnlySpan<byte> source, out long value)
		{
			bool flag = MemoryMarshal.TryRead<long>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002AA38 File Offset: 0x00028C38
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			bool flag = MemoryMarshal.TryRead<ushort>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002AA68 File Offset: 0x00028C68
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt32BigEndian(ReadOnlySpan<byte> source, out uint value)
		{
			bool flag = MemoryMarshal.TryRead<uint>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002AA98 File Offset: 0x00028C98
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt64BigEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			bool flag = MemoryMarshal.TryRead<ulong>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002AAF4 File Offset: 0x00028CF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002AB20 File Offset: 0x00028D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002AB4C File Offset: 0x00028D4C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002AB78 File Offset: 0x00028D78
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt16LittleEndian(ReadOnlySpan<byte> source, out short value)
		{
			bool flag = MemoryMarshal.TryRead<short>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002AC00 File Offset: 0x00028E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt32LittleEndian(ReadOnlySpan<byte> source, out int value)
		{
			bool flag = MemoryMarshal.TryRead<int>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002AC30 File Offset: 0x00028E30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt64LittleEndian(ReadOnlySpan<byte> source, out long value)
		{
			bool flag = MemoryMarshal.TryRead<long>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002AC60 File Offset: 0x00028E60
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt16LittleEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			bool flag = MemoryMarshal.TryRead<ushort>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002AC90 File Offset: 0x00028E90
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt32LittleEndian(ReadOnlySpan<byte> source, out uint value)
		{
			bool flag = MemoryMarshal.TryRead<uint>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002ACC0 File Offset: 0x00028EC0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt64LittleEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			bool flag = MemoryMarshal.TryRead<ulong>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return flag;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002ACF0 File Offset: 0x00028EF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt16BigEndian(Span<byte> destination, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<short>(destination, ref value);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002AD0C File Offset: 0x00028F0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt32BigEndian(Span<byte> destination, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<int>(destination, ref value);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002AD28 File Offset: 0x00028F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt64BigEndian(Span<byte> destination, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<long>(destination, ref value);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002AD44 File Offset: 0x00028F44
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ushort>(destination, ref value);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002AD60 File Offset: 0x00028F60
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<uint>(destination, ref value);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002AD7C File Offset: 0x00028F7C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ulong>(destination, ref value);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002AD98 File Offset: 0x00028F98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt16BigEndian(Span<byte> destination, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<short>(destination, ref value);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt32BigEndian(Span<byte> destination, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<int>(destination, ref value);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002ADD0 File Offset: 0x00028FD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt64BigEndian(Span<byte> destination, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<long>(destination, ref value);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002ADEC File Offset: 0x00028FEC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ushort>(destination, ref value);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002AE08 File Offset: 0x00029008
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<uint>(destination, ref value);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002AE24 File Offset: 0x00029024
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ulong>(destination, ref value);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002AE40 File Offset: 0x00029040
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt16LittleEndian(Span<byte> destination, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<short>(destination, ref value);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002AE5C File Offset: 0x0002905C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt32LittleEndian(Span<byte> destination, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<int>(destination, ref value);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002AE78 File Offset: 0x00029078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt64LittleEndian(Span<byte> destination, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<long>(destination, ref value);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002AE94 File Offset: 0x00029094
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ushort>(destination, ref value);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002AEB0 File Offset: 0x000290B0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<uint>(destination, ref value);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002AECC File Offset: 0x000290CC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ulong>(destination, ref value);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002AEE8 File Offset: 0x000290E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt16LittleEndian(Span<byte> destination, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<short>(destination, ref value);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002AF04 File Offset: 0x00029104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt32LittleEndian(Span<byte> destination, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<int>(destination, ref value);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002AF20 File Offset: 0x00029120
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt64LittleEndian(Span<byte> destination, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<long>(destination, ref value);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002AF3C File Offset: 0x0002913C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ushort>(destination, ref value);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002AF58 File Offset: 0x00029158
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<uint>(destination, ref value);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002AF74 File Offset: 0x00029174
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ulong>(destination, ref value);
		}
	}
}
