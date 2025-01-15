using System;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x0200001B RID: 27
	[SecuritySafeCritical]
	public readonly struct ActivitySpanId : IEquatable<ActivitySpanId>
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000492D File Offset: 0x00002B2D
		internal ActivitySpanId(string hexString)
		{
			this._hexString = hexString;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004938 File Offset: 0x00002B38
		public unsafe static ActivitySpanId CreateRandom()
		{
			ulong num;
			ActivityTraceId.SetToRandomBytes(new Span<byte>((void*)(&num), 8));
			return new ActivitySpanId(HexConverter.ToString(new ReadOnlySpan<byte>((void*)(&num), 8), HexConverter.Casing.Lower));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000496B File Offset: 0x00002B6B
		public static ActivitySpanId CreateFromBytes(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 8)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivitySpanId(HexConverter.ToString(idData, HexConverter.Casing.Lower));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004992 File Offset: 0x00002B92
		public static ActivitySpanId CreateFromUtf8String(ReadOnlySpan<byte> idData)
		{
			return new ActivitySpanId(idData);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000499A File Offset: 0x00002B9A
		public static ActivitySpanId CreateFromString(ReadOnlySpan<char> idData)
		{
			if (idData.Length != 16 || !ActivityTraceId.IsLowerCaseHexAndNotAllZeros(idData))
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivitySpanId(idData.ToString());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000049CC File Offset: 0x00002BCC
		[NullableContext(1)]
		public string ToHexString()
		{
			return this._hexString ?? "0000000000000000";
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000049DD File Offset: 0x00002BDD
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToHexString();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000049E5 File Offset: 0x00002BE5
		public static bool operator ==(ActivitySpanId spanId1, ActivitySpanId spandId2)
		{
			return spanId1._hexString == spandId2._hexString;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000049F8 File Offset: 0x00002BF8
		public static bool operator !=(ActivitySpanId spanId1, ActivitySpanId spandId2)
		{
			return spanId1._hexString != spandId2._hexString;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004A0B File Offset: 0x00002C0B
		public bool Equals(ActivitySpanId spanId)
		{
			return this._hexString == spanId._hexString;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004A20 File Offset: 0x00002C20
		[NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is ActivitySpanId)
			{
				ActivitySpanId activitySpanId = (ActivitySpanId)obj;
				return this._hexString == activitySpanId._hexString;
			}
			return false;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004A4F File Offset: 0x00002C4F
		public override int GetHashCode()
		{
			return this.ToHexString().GetHashCode();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004A5C File Offset: 0x00002C5C
		private unsafe ActivitySpanId(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 16)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			ulong num;
			int num2;
			if (!Utf8Parser.TryParse(idData, ref num, ref num2, 'x'))
			{
				this._hexString = ActivitySpanId.CreateRandom()._hexString;
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			this._hexString = HexConverter.ToString(new ReadOnlySpan<byte>((void*)(&num), 8), HexConverter.Casing.Lower);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004AC5 File Offset: 0x00002CC5
		public void CopyTo(Span<byte> destination)
		{
			ActivityTraceId.SetSpanFromHexChars(MemoryExtensions.AsSpan(this.ToHexString()), destination);
		}

		// Token: 0x0400004F RID: 79
		private readonly string _hexString;
	}
}
