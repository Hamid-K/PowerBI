using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000033 RID: 51
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	internal struct Decimal128
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000548C File Offset: 0x0000368C
		public unsafe Decimal128(decimal value, decimal multiplier)
		{
			decimal num;
			try
			{
				num = decimal.Truncate(value * multiplier);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(string.Format("value {0:E} is too large for decimal scale {1}", value, Math.Log10((double)multiplier)), ex);
			}
			uint* ptr = (uint*)(&num);
			fixed (uint* ptr2 = &this._uints.FixedElementField)
			{
				uint* ptr3 = ptr2;
				*ptr3 = ptr[2];
				ptr3[1] = ptr[3];
				ptr3[2] = ptr[1];
				ptr3[3] = 0U;
				if (*ptr == 2147483648U)
				{
					Decimal128.TwosComplement(ptr3);
					ptr3[3] = uint.MaxValue;
				}
				Decimal128.ReverseByteOrder(ptr3);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000554C File Offset: 0x0000374C
		public unsafe decimal ToDecimal(decimal multiplier)
		{
			decimal num = 0m;
			uint* ptr = (uint*)(&num);
			uint* ptr2 = stackalloc uint[(UIntPtr)16];
			*ptr2 = this._uints.FixedElementField;
			ptr2[1] = *((ref this._uints.FixedElementField) + 4);
			ptr2[2] = *((ref this._uints.FixedElementField) + (IntPtr)2 * 4);
			ptr2[3] = *((ref this._uints.FixedElementField) + (IntPtr)3 * 4);
			Decimal128.ReverseByteOrder(ptr2);
			if (ptr2[3] != 0U)
			{
				Decimal128.TwosComplement(ptr2);
				ptr2[3] = 2147483648U;
			}
			ptr[2] = *ptr2;
			ptr[3] = ptr2[1];
			ptr[1] = ptr2[2];
			*ptr = ptr2[3];
			return num / multiplier;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005614 File Offset: 0x00003814
		public static decimal GetScaleMultiplier(int scale)
		{
			if (scale < 0 || scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", "scale must be a value in [0, 28]");
			}
			return (decimal)Math.Pow(10.0, (double)scale);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000564C File Offset: 0x0000384C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void TwosComplement(uint* ptr)
		{
			uint num = 0U;
			*ptr = Decimal128.AddCarry(~(*ptr), 1U, ref num);
			ptr[1] = Decimal128.AddCarry(~ptr[1], 0U, ref num);
			ptr[2] = Decimal128.AddCarry(~ptr[2], 0U, ref num);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005694 File Offset: 0x00003894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void ReverseByteOrder(uint* ptr)
		{
			for (int num = 0; num != sizeof(Decimal128) / 2; num++)
			{
				byte b = *(byte*)(ptr + num / 4);
				*(byte*)(ptr + num / 4) = *(byte*)(ptr + (sizeof(Decimal128) - num - 1) / 4);
				*(byte*)(ptr + (sizeof(Decimal128) - num - 1) / 4) = b;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000056E4 File Offset: 0x000038E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint AddCarry(uint left, uint right, ref uint carry)
		{
			ulong num = (ulong)left + (ulong)right + (ulong)carry;
			carry = (uint)(num >> 32);
			return (uint)num;
		}

		// Token: 0x0400004F RID: 79
		private const uint SignMask = 2147483648U;

		// Token: 0x04000050 RID: 80
		[FixedBuffer(typeof(uint), 4)]
		private Decimal128.<_uints>e__FixedBuffer _uints;

		// Token: 0x02000100 RID: 256
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <_uints>e__FixedBuffer
		{
			// Token: 0x040002C9 RID: 713
			public uint FixedElementField;
		}
	}
}
