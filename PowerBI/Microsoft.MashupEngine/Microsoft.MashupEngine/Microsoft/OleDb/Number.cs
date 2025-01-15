using System;
using System.Numerics;

namespace Microsoft.OleDb
{
	// Token: 0x02001EB1 RID: 7857
	public struct Number
	{
		// Token: 0x0600C234 RID: 49716 RVA: 0x002700FC File Offset: 0x0026E2FC
		static Number()
		{
			double num = 1.0;
			for (int i = 0; i < Number.powersOfTen.Length; i++)
			{
				Number.powersOfTen[i] = num;
				num *= 10.0;
			}
		}

		// Token: 0x0600C235 RID: 49717 RVA: 0x00270168 File Offset: 0x0026E368
		public unsafe Number(double value)
		{
			Number.NumberBuffer numberBuffer = default(Number.NumberBuffer);
			*(double*)(&numberBuffer) = value;
			this.kind = NumberKind.Double;
			this.buffer = numberBuffer;
		}

		// Token: 0x0600C236 RID: 49718 RVA: 0x00270190 File Offset: 0x0026E390
		public unsafe Number(decimal value)
		{
			Number.NumberBuffer numberBuffer = default(Number.NumberBuffer);
			*(decimal*)(&numberBuffer) = value;
			this.kind = NumberKind.Decimal;
			this.buffer = numberBuffer;
		}

		// Token: 0x0600C237 RID: 49719 RVA: 0x002701BC File Offset: 0x0026E3BC
		public unsafe Number(NUMERIC value)
		{
			Number.NumberBuffer numberBuffer = default(Number.NumberBuffer);
			*(NUMERIC*)(&numberBuffer) = value;
			this.kind = NumberKind.Numeric;
			this.buffer = numberBuffer;
		}

		// Token: 0x17002F69 RID: 12137
		// (get) Token: 0x0600C238 RID: 49720 RVA: 0x002701E8 File Offset: 0x0026E3E8
		public NumberKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x0600C239 RID: 49721 RVA: 0x002701F0 File Offset: 0x0026E3F0
		public double ToDouble()
		{
			switch (this.kind)
			{
			case NumberKind.Decimal:
				return (double)this.GetDecimal();
			case NumberKind.Double:
				return this.GetDouble();
			case NumberKind.Numeric:
				return Number.ToDouble(this.GetNumeric());
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600C23A RID: 49722 RVA: 0x00270240 File Offset: 0x0026E440
		public decimal ToDecimal()
		{
			switch (this.kind)
			{
			case NumberKind.Decimal:
				return this.GetDecimal();
			case NumberKind.Double:
				return (decimal)this.GetDouble();
			case NumberKind.Numeric:
				return Number.ToDecimal(this.GetNumeric());
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600C23B RID: 49723 RVA: 0x0027028C File Offset: 0x0026E48C
		public NUMERIC ToNumeric()
		{
			NumberKind numberKind = this.kind;
			if (numberKind <= NumberKind.Double)
			{
				throw new NotImplementedException();
			}
			if (numberKind != NumberKind.Numeric)
			{
				throw new InvalidOperationException();
			}
			return this.GetNumeric();
		}

		// Token: 0x0600C23C RID: 49724 RVA: 0x002702BC File Offset: 0x0026E4BC
		private unsafe double GetDouble()
		{
			Number.NumberBuffer numberBuffer = this.buffer;
			return *(double*)(&numberBuffer);
		}

		// Token: 0x0600C23D RID: 49725 RVA: 0x002702D4 File Offset: 0x0026E4D4
		private unsafe decimal GetDecimal()
		{
			Number.NumberBuffer numberBuffer = this.buffer;
			return *(decimal*)(&numberBuffer);
		}

		// Token: 0x0600C23E RID: 49726 RVA: 0x002702F0 File Offset: 0x0026E4F0
		private unsafe NUMERIC GetNumeric()
		{
			Number.NumberBuffer numberBuffer = this.buffer;
			return *(NUMERIC*)(&numberBuffer);
		}

		// Token: 0x0600C23F RID: 49727 RVA: 0x0027030C File Offset: 0x0026E50C
		internal unsafe static double ToDouble(NUMERIC numeric)
		{
			byte* ptr = (byte*)(&numeric);
			if (Number.ToInt32(ptr + 15) == 0)
			{
				return (double)Number.ToDecimalNoOverflow(ptr);
			}
			return Number.ToDoubleWithOverflow(ptr);
		}

		// Token: 0x0600C240 RID: 49728 RVA: 0x0027033C File Offset: 0x0026E53C
		internal unsafe static decimal ToDecimal(NUMERIC numeric)
		{
			byte* ptr = (byte*)(&numeric);
			if (Number.ToInt32(ptr + 15) == 0)
			{
				return Number.ToDecimalNoOverflow(ptr);
			}
			return Number.ToDecimalWithOverflow(ptr);
		}

		// Token: 0x0600C241 RID: 49729 RVA: 0x00270368 File Offset: 0x0026E568
		private unsafe static double ToDoubleWithOverflow(byte* pNumeric)
		{
			int num = (int)pNumeric[1];
			BigInteger mantissa = Number.GetMantissa(pNumeric);
			double num2;
			if (num < Number.powersOfTen.Length)
			{
				num2 = (double)mantissa / Number.powersOfTen[num];
			}
			else
			{
				num2 = (double)mantissa / Math.Pow(10.0, (double)num);
			}
			if (pNumeric[2] == 0)
			{
				num2 = -num2;
			}
			return num2;
		}

		// Token: 0x0600C242 RID: 49730 RVA: 0x002703C0 File Offset: 0x0026E5C0
		private unsafe static decimal ToDecimalNoOverflow(byte* pNumeric)
		{
			decimal num = 0.0m;
			int* ptr = (int*)(&num);
			ptr[2] = Number.ToInt32(pNumeric + 3);
			ptr[3] = Number.ToInt32(pNumeric + 7);
			ptr[1] = Number.ToInt32(pNumeric + 11);
			int num2 = (int)pNumeric[1];
			*ptr = num2 << 16;
			if (pNumeric[2] == 0)
			{
				*ptr |= int.MinValue;
			}
			return num;
		}

		// Token: 0x0600C243 RID: 49731 RVA: 0x00270424 File Offset: 0x0026E624
		private unsafe static decimal ToDecimalWithOverflow(byte* pNumeric)
		{
			decimal num = 0.0m;
			int* ptr = (int*)(&num);
			int num2 = (int)(*pNumeric);
			int num3 = (int)pNumeric[1];
			BigInteger bigInteger = Number.GetMantissa(pNumeric);
			while (num3 >= 0 && (bigInteger > Number.maxDecimal || num2 > 29))
			{
				bigInteger = BigInteger.Divide(bigInteger, Number.ten);
				num3--;
				num2--;
			}
			if (num3 < 0 || num2 - num3 > 29)
			{
				throw new OverflowException();
			}
			num = (decimal)bigInteger;
			*ptr = num3 << 16;
			if (pNumeric[2] == 0)
			{
				*ptr |= int.MinValue;
			}
			return num;
		}

		// Token: 0x0600C244 RID: 49732 RVA: 0x002704B0 File Offset: 0x0026E6B0
		private unsafe static int ToInt32(byte* pbyte)
		{
			if ((long)((IntPtr)((void*)pbyte)) % 4L == 0L)
			{
				return *(int*)pbyte;
			}
			return (int)(*pbyte) | ((int)pbyte[1] << 8) | ((int)pbyte[2] << 16) | ((int)pbyte[3] << 24);
		}

		// Token: 0x0600C245 RID: 49733 RVA: 0x002704E0 File Offset: 0x0026E6E0
		private unsafe static BigInteger GetMantissa(byte* pNumeric)
		{
			byte[] array2;
			byte[] array = (array2 = new byte[16]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			ulong* ptr2 = (ulong*)(pNumeric + 3);
			ulong* ptr3 = (ulong*)ptr;
			*ptr3 = *ptr2;
			ptr3[1] = ptr2[1];
			array2 = null;
			return new BigInteger(array);
		}

		// Token: 0x040061BB RID: 25019
		private const byte MaxDecimalPrecision = 29;

		// Token: 0x040061BC RID: 25020
		private static readonly BigInteger maxDecimal = new BigInteger(decimal.MaxValue);

		// Token: 0x040061BD RID: 25021
		private static readonly BigInteger ten = new BigInteger(10);

		// Token: 0x040061BE RID: 25022
		private static readonly double[] powersOfTen = new double[30];

		// Token: 0x040061BF RID: 25023
		private NumberKind kind;

		// Token: 0x040061C0 RID: 25024
		private Number.NumberBuffer buffer;

		// Token: 0x02001EB2 RID: 7858
		private struct NumberBuffer
		{
			// Token: 0x040061C1 RID: 25025
			private ulong u0;

			// Token: 0x040061C2 RID: 25026
			private ulong u1;

			// Token: 0x040061C3 RID: 25027
			private ulong u2;
		}
	}
}
