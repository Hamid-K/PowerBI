using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace System.Text
{
	// Token: 0x0200001E RID: 30
	internal readonly struct Rune : IEquatable<Rune>
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002F8A File Offset: 0x0000118A
		public Rune(uint value)
		{
			if (!UnicodeUtility.IsValidUnicodeScalar(value))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			this._value = value;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002FA1 File Offset: 0x000011A1
		public Rune(int value)
		{
			this = new Rune((uint)value);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002FAA File Offset: 0x000011AA
		private Rune(uint scalarValue, bool _)
		{
			this._value = scalarValue;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FB3 File Offset: 0x000011B3
		public bool IsAscii
		{
			get
			{
				return UnicodeUtility.IsAsciiCodePoint(this._value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FC0 File Offset: 0x000011C0
		public bool IsBmp
		{
			get
			{
				return UnicodeUtility.IsBmpCodePoint(this._value);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FCD File Offset: 0x000011CD
		public static bool operator ==(Rune left, Rune right)
		{
			return left._value == right._value;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002FDD File Offset: 0x000011DD
		public static bool operator !=(Rune left, Rune right)
		{
			return left._value != right._value;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static bool IsControl(Rune value)
		{
			return ((value._value + 1U) & 4294967167U) <= 32U;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003007 File Offset: 0x00001207
		public static Rune ReplacementChar
		{
			get
			{
				return Rune.UnsafeCreate(65533U);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003014 File Offset: 0x00001214
		public int Utf16SequenceLength
		{
			get
			{
				return UnicodeUtility.GetUtf16SequenceLength(this._value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000302E File Offset: 0x0000122E
		public int Value
		{
			get
			{
				return (int)this._value;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003038 File Offset: 0x00001238
		public unsafe static OperationStatus DecodeFromUtf16(ReadOnlySpan<char> source, out Rune result, out int charsConsumed)
		{
			if (!source.IsEmpty)
			{
				char c = (char)(*source[0]);
				if (Rune.TryCreate(c, out result))
				{
					charsConsumed = 1;
					return OperationStatus.Done;
				}
				if (1 < source.Length)
				{
					char c2 = (char)(*source[1]);
					if (Rune.TryCreate(c, c2, out result))
					{
						charsConsumed = 2;
						return OperationStatus.Done;
					}
				}
				else if (char.IsHighSurrogate(c))
				{
					goto IL_004C;
				}
				charsConsumed = 1;
				result = Rune.ReplacementChar;
				return OperationStatus.InvalidData;
			}
			IL_004C:
			charsConsumed = source.Length;
			result = Rune.ReplacementChar;
			return OperationStatus.NeedMoreData;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030B8 File Offset: 0x000012B8
		public unsafe static OperationStatus DecodeFromUtf8(ReadOnlySpan<byte> source, out Rune result, out int bytesConsumed)
		{
			int num = 0;
			if (num < source.Length)
			{
				uint num2 = (uint)(*source[num]);
				if (!UnicodeUtility.IsAsciiCodePoint(num2))
				{
					if (UnicodeUtility.IsInRangeInclusive(num2, 194U, 244U))
					{
						num2 = num2 - 194U << 6;
						num++;
						if (num >= source.Length)
						{
							goto IL_0163;
						}
						int num3 = (int)((sbyte)(*source[num]));
						if (num3 < -64)
						{
							num2 += (uint)num3;
							num2 += 128U;
							num2 += 128U;
							if (num2 < 2048U)
							{
								goto IL_0021;
							}
							if (UnicodeUtility.IsInRangeInclusive(num2, 2080U, 3343U) && !UnicodeUtility.IsInRangeInclusive(num2, 2912U, 2943U) && !UnicodeUtility.IsInRangeInclusive(num2, 3072U, 3087U))
							{
								num++;
								if (num >= source.Length)
								{
									goto IL_0163;
								}
								num3 = (int)((sbyte)(*source[num]));
								if (num3 < -64)
								{
									num2 <<= 6;
									num2 += (uint)num3;
									num2 += 128U;
									num2 -= 131072U;
									if (num2 <= 65535U)
									{
										goto IL_0021;
									}
									num++;
									if (num >= source.Length)
									{
										goto IL_0163;
									}
									num3 = (int)((sbyte)(*source[num]));
									if (num3 < -64)
									{
										num2 <<= 6;
										num2 += (uint)num3;
										num2 += 128U;
										num2 -= 4194304U;
										goto IL_0021;
									}
								}
							}
						}
					}
					else
					{
						num = 1;
					}
					bytesConsumed = num;
					result = Rune.ReplacementChar;
					return OperationStatus.InvalidData;
				}
				IL_0021:
				bytesConsumed = num + 1;
				result = Rune.UnsafeCreate(num2);
				return OperationStatus.Done;
			}
			IL_0163:
			bytesConsumed = num;
			result = Rune.ReplacementChar;
			return OperationStatus.NeedMoreData;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003238 File Offset: 0x00001438
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is Rune)
			{
				Rune rune = (Rune)obj;
				return this.Equals(rune);
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000325D File Offset: 0x0000145D
		public bool Equals(Rune other)
		{
			return this == other;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000326B File Offset: 0x0000146B
		public override int GetHashCode()
		{
			return this.Value;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003274 File Offset: 0x00001474
		public static bool TryCreate(char ch, out Rune result)
		{
			if (!UnicodeUtility.IsSurrogateCodePoint((uint)ch))
			{
				result = Rune.UnsafeCreate((uint)ch);
				return true;
			}
			result = default(Rune);
			return false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000032A4 File Offset: 0x000014A4
		public static bool TryCreate(char highSurrogate, char lowSurrogate, out Rune result)
		{
			uint num = (uint)(highSurrogate - '\ud800');
			uint num2 = (uint)(lowSurrogate - '\udc00');
			if ((num | num2) <= 1023U)
			{
				result = Rune.UnsafeCreate((num << 10) + (uint)(lowSurrogate - '\udc00') + 65536U);
				return true;
			}
			result = default(Rune);
			return false;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000032F4 File Offset: 0x000014F4
		public unsafe bool TryEncodeToUtf16(Span<char> destination, out int charsWritten)
		{
			if (destination.Length >= 1)
			{
				if (this.IsBmp)
				{
					*destination[0] = (char)this._value;
					charsWritten = 1;
					return true;
				}
				if (destination.Length >= 2)
				{
					UnicodeUtility.GetUtf16SurrogatesFromSupplementaryPlaneScalar(this._value, destination[0], destination[1]);
					charsWritten = 2;
					return true;
				}
			}
			charsWritten = 0;
			return false;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003358 File Offset: 0x00001558
		public unsafe bool TryEncodeToUtf8(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length >= 1)
			{
				if (this.IsAscii)
				{
					*destination[0] = (byte)this._value;
					bytesWritten = 1;
					return true;
				}
				if (destination.Length >= 2)
				{
					if (this._value <= 2047U)
					{
						*destination[0] = (byte)(this._value + 12288U >> 6);
						*destination[1] = (byte)((this._value & 63U) + 128U);
						bytesWritten = 2;
						return true;
					}
					if (destination.Length >= 3)
					{
						if (this._value <= 65535U)
						{
							*destination[0] = (byte)(this._value + 917504U >> 12);
							*destination[1] = (byte)(((this._value & 4032U) >> 6) + 128U);
							*destination[2] = (byte)((this._value & 63U) + 128U);
							bytesWritten = 3;
							return true;
						}
						if (destination.Length >= 4)
						{
							*destination[0] = (byte)(this._value + 62914560U >> 18);
							*destination[1] = (byte)(((this._value & 258048U) >> 12) + 128U);
							*destination[2] = (byte)(((this._value & 4032U) >> 6) + 128U);
							*destination[3] = (byte)((this._value & 63U) + 128U);
							bytesWritten = 4;
							return true;
						}
					}
				}
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000034D0 File Offset: 0x000016D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Rune UnsafeCreate(uint scalarValue)
		{
			return new Rune(scalarValue, false);
		}

		// Token: 0x0400001C RID: 28
		private const int MaxUtf16CharsPerRune = 2;

		// Token: 0x0400001D RID: 29
		private const char HighSurrogateStart = '\ud800';

		// Token: 0x0400001E RID: 30
		private const char LowSurrogateStart = '\udc00';

		// Token: 0x0400001F RID: 31
		private const int HighSurrogateRange = 1023;

		// Token: 0x04000020 RID: 32
		private readonly uint _value;
	}
}
