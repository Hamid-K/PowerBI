using System;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x0200001A RID: 26
	[SecuritySafeCritical]
	public readonly struct ActivityTraceId : IEquatable<ActivityTraceId>
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000460D File Offset: 0x0000280D
		internal ActivityTraceId(string hexString)
		{
			this._hexString = hexString;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004618 File Offset: 0x00002818
		public unsafe static ActivityTraceId CreateRandom()
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)16], 16);
			Span<byte> span2 = span;
			ActivityTraceId.SetToRandomBytes(span2);
			return ActivityTraceId.CreateFromBytes(span2);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004645 File Offset: 0x00002845
		public static ActivityTraceId CreateFromBytes(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 16)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivityTraceId(HexConverter.ToString(idData, HexConverter.Casing.Lower));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000466D File Offset: 0x0000286D
		public static ActivityTraceId CreateFromUtf8String(ReadOnlySpan<byte> idData)
		{
			return new ActivityTraceId(idData);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004675 File Offset: 0x00002875
		public static ActivityTraceId CreateFromString(ReadOnlySpan<char> idData)
		{
			if (idData.Length != 32 || !ActivityTraceId.IsLowerCaseHexAndNotAllZeros(idData))
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivityTraceId(idData.ToString());
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000046A7 File Offset: 0x000028A7
		[NullableContext(1)]
		public string ToHexString()
		{
			return this._hexString ?? "00000000000000000000000000000000";
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000046B8 File Offset: 0x000028B8
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToHexString();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000046C0 File Offset: 0x000028C0
		public static bool operator ==(ActivityTraceId traceId1, ActivityTraceId traceId2)
		{
			return traceId1._hexString == traceId2._hexString;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000046D3 File Offset: 0x000028D3
		public static bool operator !=(ActivityTraceId traceId1, ActivityTraceId traceId2)
		{
			return traceId1._hexString != traceId2._hexString;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000046E6 File Offset: 0x000028E6
		public bool Equals(ActivityTraceId traceId)
		{
			return this._hexString == traceId._hexString;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000046FC File Offset: 0x000028FC
		[NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is ActivityTraceId)
			{
				ActivityTraceId activityTraceId = (ActivityTraceId)obj;
				return this._hexString == activityTraceId._hexString;
			}
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000472B File Offset: 0x0000292B
		public override int GetHashCode()
		{
			return this.ToHexString().GetHashCode();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004738 File Offset: 0x00002938
		private unsafe ActivityTraceId(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 32)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			Span<ulong> span = new Span<ulong>(stackalloc byte[(UIntPtr)16], 2);
			Span<ulong> span2 = span;
			int num;
			if (!Utf8Parser.TryParse(idData.Slice(0, 16), span2[0], ref num, 'x'))
			{
				this._hexString = ActivityTraceId.CreateRandom()._hexString;
				return;
			}
			if (!Utf8Parser.TryParse(idData.Slice(16, 16), span2[1], ref num, 'x'))
			{
				this._hexString = ActivityTraceId.CreateRandom()._hexString;
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				*span2[0] = BinaryPrimitives.ReverseEndianness(*span2[0]);
				*span2[1] = BinaryPrimitives.ReverseEndianness(*span2[1]);
			}
			this._hexString = HexConverter.ToString(MemoryMarshal.AsBytes<ulong>(span2), HexConverter.Casing.Lower);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004816 File Offset: 0x00002A16
		public void CopyTo(Span<byte> destination)
		{
			ActivityTraceId.SetSpanFromHexChars(MemoryExtensions.AsSpan(this.ToHexString()), destination);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000482C File Offset: 0x00002A2C
		internal static void SetToRandomBytes(Span<byte> outBytes)
		{
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Current;
			Unsafe.WriteUnaligned<long>(outBytes[0], randomNumberGenerator.Next());
			if (outBytes.Length == 16)
			{
				Unsafe.WriteUnaligned<long>(outBytes[8], randomNumberGenerator.Next());
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004870 File Offset: 0x00002A70
		internal unsafe static void SetSpanFromHexChars(ReadOnlySpan<char> charData, Span<byte> outBytes)
		{
			for (int i = 0; i < outBytes.Length; i++)
			{
				*outBytes[i] = ActivityTraceId.HexByteFromChars((char)(*charData[i * 2]), (char)(*charData[i * 2 + 1]));
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000048B8 File Offset: 0x00002AB8
		internal static byte HexByteFromChars(char char1, char char2)
		{
			int num = HexConverter.FromLowerChar((int)char1);
			int num2 = HexConverter.FromLowerChar((int)char2);
			if ((num | num2) == 255)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return (byte)((num << 4) | num2);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048F0 File Offset: 0x00002AF0
		internal unsafe static bool IsLowerCaseHexAndNotAllZeros(ReadOnlySpan<char> idData)
		{
			bool flag = false;
			for (int i = 0; i < idData.Length; i++)
			{
				char c = (char)(*idData[i]);
				if (!HexConverter.IsHexLowerChar((int)c))
				{
					return false;
				}
				if (c != '0')
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0400004E RID: 78
		private readonly string _hexString;
	}
}
