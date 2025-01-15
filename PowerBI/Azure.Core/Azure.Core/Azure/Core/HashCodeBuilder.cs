using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000072 RID: 114
	[NullableContext(1)]
	[Nullable(0)]
	internal struct HashCodeBuilder
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000B006 File Offset: 0x00009206
		private static uint GenerateGlobalSeed()
		{
			return (uint)new Random().Next();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B014 File Offset: 0x00009214
		public static int Combine<[Nullable(2)] T1>(T1 value1)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.QueueRound(HashCodeBuilder.MixEmptyState() + 4U, num));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B04C File Offset: 0x0000924C
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2>(T1 value1, T2 value2)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.QueueRound(HashCodeBuilder.QueueRound(HashCodeBuilder.MixEmptyState() + 8U, num), num2));
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(T1 value1, T2 value2, T3 value3)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.QueueRound(HashCodeBuilder.QueueRound(HashCodeBuilder.QueueRound(HashCodeBuilder.MixEmptyState() + 12U, num), num2), num3));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000B11C File Offset: 0x0000931C
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5;
			uint num6;
			uint num7;
			uint num8;
			HashCodeBuilder.Initialize(out num5, out num6, out num7, out num8);
			num5 = HashCodeBuilder.Round(num5, num);
			num6 = HashCodeBuilder.Round(num6, num2);
			num7 = HashCodeBuilder.Round(num7, num3);
			num8 = HashCodeBuilder.Round(num8, num4);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.MixState(num5, num6, num7, num8) + 16U);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6;
			uint num7;
			uint num8;
			uint num9;
			HashCodeBuilder.Initialize(out num6, out num7, out num8, out num9);
			num6 = HashCodeBuilder.Round(num6, num);
			num7 = HashCodeBuilder.Round(num7, num2);
			num8 = HashCodeBuilder.Round(num8, num3);
			num9 = HashCodeBuilder.Round(num9, num4);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.QueueRound(HashCodeBuilder.MixState(num6, num7, num8, num9) + 20U, num5));
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num7;
			uint num8;
			uint num9;
			uint num10;
			HashCodeBuilder.Initialize(out num7, out num8, out num9, out num10);
			num7 = HashCodeBuilder.Round(num7, num);
			num8 = HashCodeBuilder.Round(num8, num2);
			num9 = HashCodeBuilder.Round(num9, num3);
			num10 = HashCodeBuilder.Round(num10, num4);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.QueueRound(HashCodeBuilder.QueueRound(HashCodeBuilder.MixState(num7, num8, num9, num10) + 24U, num5), num6));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B3B8 File Offset: 0x000095B8
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num7 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint num8;
			uint num9;
			uint num10;
			uint num11;
			HashCodeBuilder.Initialize(out num8, out num9, out num10, out num11);
			num8 = HashCodeBuilder.Round(num8, num);
			num9 = HashCodeBuilder.Round(num9, num2);
			num10 = HashCodeBuilder.Round(num10, num3);
			num11 = HashCodeBuilder.Round(num11, num4);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.QueueRound(HashCodeBuilder.QueueRound(HashCodeBuilder.QueueRound(HashCodeBuilder.MixState(num8, num9, num10, num11) + 28U, num5), num6), num7));
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B4DC File Offset: 0x000096DC
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num7 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint num8 = (uint)((value8 != null) ? value8.GetHashCode() : 0);
			uint num9;
			uint num10;
			uint num11;
			uint num12;
			HashCodeBuilder.Initialize(out num9, out num10, out num11, out num12);
			num9 = HashCodeBuilder.Round(num9, num);
			num10 = HashCodeBuilder.Round(num10, num2);
			num11 = HashCodeBuilder.Round(num11, num3);
			num12 = HashCodeBuilder.Round(num12, num4);
			num9 = HashCodeBuilder.Round(num9, num5);
			num10 = HashCodeBuilder.Round(num10, num6);
			num11 = HashCodeBuilder.Round(num11, num7);
			num12 = HashCodeBuilder.Round(num12, num8);
			return (int)HashCodeBuilder.MixFinal(HashCodeBuilder.MixState(num9, num10, num11, num12) + 32U);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B62F File Offset: 0x0000982F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
		{
			v1 = HashCodeBuilder.s_seed + 2654435761U + 2246822519U;
			v2 = HashCodeBuilder.s_seed + 2246822519U;
			v3 = HashCodeBuilder.s_seed;
			v4 = HashCodeBuilder.s_seed - 2654435761U;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B665 File Offset: 0x00009865
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Round(uint hash, uint input)
		{
			return HashCodeBuilder.RotateLeft(hash + input * 2246822519U, 13) * 2654435761U;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000B67D File Offset: 0x0000987D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint QueueRound(uint hash, uint queuedValue)
		{
			return HashCodeBuilder.RotateLeft(hash + queuedValue * 3266489917U, 17) * 668265263U;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B695 File Offset: 0x00009895
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixState(uint v1, uint v2, uint v3, uint v4)
		{
			return HashCodeBuilder.RotateLeft(v1, 1) + HashCodeBuilder.RotateLeft(v2, 7) + HashCodeBuilder.RotateLeft(v3, 12) + HashCodeBuilder.RotateLeft(v4, 18);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B6B8 File Offset: 0x000098B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 64 - offset);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B6CA File Offset: 0x000098CA
		private static uint MixEmptyState()
		{
			return HashCodeBuilder.s_seed + 374761393U;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B6D7 File Offset: 0x000098D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixFinal(uint hash)
		{
			hash ^= hash >> 15;
			hash *= 2246822519U;
			hash ^= hash >> 13;
			hash *= 3266489917U;
			hash ^= hash >> 16;
			return hash;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B704 File Offset: 0x00009904
		public void Add<[Nullable(2)] T>(T value)
		{
			this.Add((value != null) ? value.GetHashCode() : 0);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B724 File Offset: 0x00009924
		public void Add<[Nullable(2)] T>(T value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> comparer)
		{
			this.Add((value == null) ? 0 : ((comparer != null) ? comparer.GetHashCode(value) : value.GetHashCode()));
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B750 File Offset: 0x00009950
		private void Add(int value)
		{
			uint length = this._length;
			this._length = length + 1U;
			uint num = length;
			uint num2 = num % 4U;
			if (num2 == 0U)
			{
				this._queue1 = (uint)value;
				return;
			}
			if (num2 == 1U)
			{
				this._queue2 = (uint)value;
				return;
			}
			if (num2 == 2U)
			{
				this._queue3 = (uint)value;
				return;
			}
			if (num == 3U)
			{
				HashCodeBuilder.Initialize(out this._v1, out this._v2, out this._v3, out this._v4);
			}
			this._v1 = HashCodeBuilder.Round(this._v1, this._queue1);
			this._v2 = HashCodeBuilder.Round(this._v2, this._queue2);
			this._v3 = HashCodeBuilder.Round(this._v3, this._queue3);
			this._v4 = HashCodeBuilder.Round(this._v4, (uint)value);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B810 File Offset: 0x00009A10
		public int ToHashCode()
		{
			uint length = this._length;
			uint num = length % 4U;
			uint num2 = ((length < 4U) ? HashCodeBuilder.MixEmptyState() : HashCodeBuilder.MixState(this._v1, this._v2, this._v3, this._v4));
			num2 += length * 4U;
			if (num > 0U)
			{
				num2 = HashCodeBuilder.QueueRound(num2, this._queue1);
				if (num > 1U)
				{
					num2 = HashCodeBuilder.QueueRound(num2, this._queue2);
					if (num > 2U)
					{
						num2 = HashCodeBuilder.QueueRound(num2, this._queue3);
					}
				}
			}
			return (int)HashCodeBuilder.MixFinal(num2);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B892 File Offset: 0x00009A92
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B899 File Offset: 0x00009A99
		[NullableContext(2)]
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000187 RID: 391
		private static readonly uint s_seed = HashCodeBuilder.GenerateGlobalSeed();

		// Token: 0x04000188 RID: 392
		private const uint Prime1 = 2654435761U;

		// Token: 0x04000189 RID: 393
		private const uint Prime2 = 2246822519U;

		// Token: 0x0400018A RID: 394
		private const uint Prime3 = 3266489917U;

		// Token: 0x0400018B RID: 395
		private const uint Prime4 = 668265263U;

		// Token: 0x0400018C RID: 396
		private const uint Prime5 = 374761393U;

		// Token: 0x0400018D RID: 397
		private uint _v1;

		// Token: 0x0400018E RID: 398
		private uint _v2;

		// Token: 0x0400018F RID: 399
		private uint _v3;

		// Token: 0x04000190 RID: 400
		private uint _v4;

		// Token: 0x04000191 RID: 401
		private uint _queue1;

		// Token: 0x04000192 RID: 402
		private uint _queue2;

		// Token: 0x04000193 RID: 403
		private uint _queue3;

		// Token: 0x04000194 RID: 404
		private uint _length;
	}
}
