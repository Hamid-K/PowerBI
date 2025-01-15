using System;

namespace System
{
	// Token: 0x02000009 RID: 9
	internal static class DecimalDecCalc
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000232C File Offset: 0x0000052C
		private static uint D32DivMod1E9(uint hi32, ref uint lo32)
		{
			ulong num = ((ulong)hi32 << 32) | (ulong)lo32;
			lo32 = (uint)(num / 1000000000UL);
			return (uint)(num % 1000000000UL);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002357 File Offset: 0x00000557
		internal static uint DecDivMod1E9(ref MutableDecimal value)
		{
			return DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(0U, ref value.High), ref value.Mid), ref value.Low);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000237B File Offset: 0x0000057B
		internal static void DecAddInt32(ref MutableDecimal value, uint i)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, i) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000023A8 File Offset: 0x000005A8
		private static bool D32AddCarry(ref uint value, uint i)
		{
			uint num = value;
			uint num2 = num + i;
			value = num2;
			return num2 < num || num2 < i;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000023CC File Offset: 0x000005CC
		internal static void DecMul10(ref MutableDecimal value)
		{
			MutableDecimal mutableDecimal = value;
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecAdd(ref value, mutableDecimal);
			DecimalDecCalc.DecShiftLeft(ref value);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000023FC File Offset: 0x000005FC
		private static void DecShiftLeft(ref MutableDecimal value)
		{
			uint num = (((value.Low & 2147483648U) != 0U) ? 1U : 0U);
			uint num2 = (((value.Mid & 2147483648U) != 0U) ? 1U : 0U);
			value.Low <<= 1;
			value.Mid = (value.Mid << 1) | num;
			value.High = (value.High << 1) | num2;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002460 File Offset: 0x00000660
		private static void DecAdd(ref MutableDecimal value, MutableDecimal d)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, d.Low) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			if (DecimalDecCalc.D32AddCarry(ref value.Mid, d.Mid))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			DecimalDecCalc.D32AddCarry(ref value.High, d.High);
		}
	}
}
